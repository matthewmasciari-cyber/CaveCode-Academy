(() => {
    "use strict";

    const VERSION = "whole-program-contrast-v1";
    const THEMES = ["cave-classic", "midnight-crystal", "light-workshop", "high-contrast", "ember-cave", "rose-quartz", "teal-blossom", "salmon-sunset", "orchid-glow", "blush-garden", "HTML", "JS", "SQL", "cave", "crystal", "csharp-event-1", "csharp-event-2", "csharp-event-3", "csharp-event-4", "csharp-event-5", "dark", "extra-large", "gear", "large", "light", "normal", "python-event-1", "python-event-2", "python-event-3", "python-event-4", "python-event-5", "system", "terminal"];
    const ROUTES = ["/", "/achievements", "/counter", "/csharp", "/leaderboard", "/minigames", "/minigames/csharp", "/minigames/python", "/not-found", "/python", "/settings", "/weather"];
    const AUDIT_QUERY = "contrast-audit";
    const AUDIT_STORAGE_KEY = "cavecode.contrast.audit.enabled";

    let panel = null;
    let lastReport = null;
    let observer = null;
    let auditQueued = false;

    function auditEnabled() {
        const params = new URLSearchParams(location.search);
        return params.get(AUDIT_QUERY) === "1" ||
               localStorage.getItem(AUDIT_STORAGE_KEY) === "true";
    }

    function parseColor(value) {
        if (!value) return null;

        const match = value.match(
            /rgba?\(\s*([\d.]+)[,\s]+([\d.]+)[,\s]+([\d.]+)(?:\s*[,/]\s*([\d.]+))?\s*\)/
        );

        if (!match) return null;

        return {
            r: Number(match[1]),
            g: Number(match[2]),
            b: Number(match[3]),
            a: match[4] === undefined ? 1 : Number(match[4])
        };
    }

    function blend(foreground, background) {
        const alpha = foreground.a + background.a * (1 - foreground.a);

        if (alpha <= 0) {
            return { r: 255, g: 255, b: 255, a: 1 };
        }

        return {
            r: (
                foreground.r * foreground.a +
                background.r * background.a * (1 - foreground.a)
            ) / alpha,
            g: (
                foreground.g * foreground.a +
                background.g * background.a * (1 - foreground.a)
            ) / alpha,
            b: (
                foreground.b * foreground.a +
                background.b * background.a * (1 - foreground.a)
            ) / alpha,
            a: alpha
        };
    }

    function linearChannel(value) {
        const channel = value / 255;
        return channel <= 0.04045
            ? channel / 12.92
            : Math.pow((channel + 0.055) / 1.055, 2.4);
    }

    function luminance(color) {
        return (
            0.2126 * linearChannel(color.r) +
            0.7152 * linearChannel(color.g) +
            0.0722 * linearChannel(color.b)
        );
    }

    function contrastRatio(first, second) {
        const l1 = luminance(first);
        const l2 = luminance(second);
        const lighter = Math.max(l1, l2);
        const darker = Math.min(l1, l2);
        return (lighter + 0.05) / (darker + 0.05);
    }

    function isVisible(element) {
        if (!(element instanceof HTMLElement)) return false;
        const style = getComputedStyle(element);

        if (
            style.display === "none" ||
            style.visibility === "hidden" ||
            Number(style.opacity) <= 0.02
        ) {
            return false;
        }

        const rect = element.getBoundingClientRect();
        return rect.width > 0 && rect.height > 0;
    }

    function directText(element) {
        return [...element.childNodes]
            .filter(node => node.nodeType === Node.TEXT_NODE)
            .map(node => node.textContent || "")
            .join(" ")
            .replace(/\s+/g, " ")
            .trim();
    }

    function effectiveBackground(element) {
        let current = element;
        let result = { r: 255, g: 255, b: 255, a: 0 };
        let complex = false;

        while (current instanceof HTMLElement) {
            const style = getComputedStyle(current);
            const color = parseColor(style.backgroundColor);

            if (style.backgroundImage && style.backgroundImage !== "none") {
                complex = true;
            }

            if (color && color.a > 0) {
                result = blend(color, result);

                if (result.a >= 0.98) break;
            }

            current = current.parentElement;
        }

        if (result.a < 1) {
            const rootStyle = getComputedStyle(document.documentElement);
            const bodyStyle = getComputedStyle(document.body);
            const fallback =
                parseColor(bodyStyle.backgroundColor) ||
                parseColor(rootStyle.backgroundColor) ||
                { r: 255, g: 255, b: 255, a: 1 };

            result = blend(result, fallback);
        }

        return { color: result, complex };
    }

    function isLargeText(style) {
        const size = Number.parseFloat(style.fontSize || "0");
        const weight = Number.parseInt(style.fontWeight || "400", 10);
        return size >= 24 || (size >= 18.66 && weight >= 700);
    }

    function candidateElements(root = document) {
        return [...root.querySelectorAll(
            "body *:not(script):not(style):not(svg):not(path):not(template)"
        )].filter(element => {
            if (!isVisible(element)) return false;
            if (!directText(element)) return false;
            if (element.closest("#cavecode-contrast-audit-panel")) return false;
            if (element.getAttribute("aria-hidden") === "true") return false;
            return true;
        });
    }

    function clearHighlights() {
        document
            .querySelectorAll(".cavecode-contrast-fail, .cavecode-contrast-warn")
            .forEach(element => {
                element.classList.remove(
                    "cavecode-contrast-fail",
                    "cavecode-contrast-warn"
                );
                delete element.dataset.cavecodeContrastRatio;
            });
    }

    function inspectCurrentPage(options = {}) {
        const highlight = options.highlight !== false;
        const elements = candidateElements();
        const failures = [];
        const warnings = [];
        let passed = 0;

        if (highlight) clearHighlights();

        for (const element of elements) {
            const style = getComputedStyle(element);
            const foreground = parseColor(style.color);
            const background = effectiveBackground(element);

            if (!foreground || !background.color) {
                warnings.push({
                    element,
                    text: directText(element),
                    reason: "Unresolved color"
                });

                if (highlight) {
                    element.classList.add("cavecode-contrast-warn");
                }

                continue;
            }

            const effectiveForeground = foreground.a < 1
                ? blend(foreground, background.color)
                : foreground;

            const ratio = contrastRatio(
                effectiveForeground,
                background.color
            );

            const threshold = isLargeText(style) ? 3 : 4.5;
            const disabled =
                element.matches(":disabled, [aria-disabled='true']") ||
                Boolean(element.closest(":disabled, [aria-disabled='true']"));
            const required = disabled ? 3 : threshold;

            const record = {
                element,
                text: directText(element).slice(0, 120),
                ratio: Number(ratio.toFixed(2)),
                required,
                complexBackground: background.complex,
                selector: describeElement(element)
            };

            if (ratio + 0.01 < required) {
                failures.push(record);

                if (highlight) {
                    element.classList.add("cavecode-contrast-fail");
                    element.dataset.cavecodeContrastRatio =
                        `${record.ratio}:1`;
                }
            } else {
                passed += 1;

                if (background.complex && ratio < required + 1) {
                    warnings.push({
                        ...record,
                        reason: "Near threshold on gradient/image"
                    });

                    if (highlight) {
                        element.classList.add("cavecode-contrast-warn");
                    }
                }
            }
        }

        lastReport = {
            version: VERSION,
            path: location.pathname,
            theme: document.documentElement.dataset.theme || "cave-classic",
            mode: document.documentElement.dataset.mode || "unknown",
            tested: elements.length,
            passed,
            failures,
            warnings,
            generatedAt: new Date().toISOString()
        };

        if (panel && options.render !== false) renderPanel(lastReport);
        return lastReport;
    }

    function describeElement(element) {
        const parts = [element.tagName.toLowerCase()];

        if (element.id) {
            parts.push(`#${element.id}`);
        }

        if (element.classList.length) {
            parts.push(
                "." +
                [...element.classList]
                    .filter(name => !name.startsWith("cavecode-contrast-"))
                    .slice(0, 4)
                    .join(".")
            );
        }

        return parts.join("");
    }

    function waitForPaint() {
        return new Promise(resolve =>
            requestAnimationFrame(() =>
                requestAnimationFrame(resolve)
            )
        );
    }

    async function sweepThemes() {
        const root = document.documentElement;
        const originalTheme = root.dataset.theme;
        const originalMode = root.dataset.mode;
        const originalModeSetting = root.dataset.modeSetting;
        const results = [];

        clearHighlights();

        for (const mode of ["light", "dark"]) {
            for (const theme of THEMES) {
                root.dataset.theme = theme;
                root.dataset.mode = mode;
                root.dataset.modeSetting = mode;
                root.style.colorScheme = mode;

                await waitForPaint();

                const report = inspectCurrentPage({ highlight: false, render: false });
                results.push({
                    theme,
                    mode,
                    tested: report.tested,
                    failures: report.failures.length,
                    warnings: report.warnings.length
                });
            }
        }

        root.dataset.theme = originalTheme || "cave-classic";
        root.dataset.mode = originalMode || "dark";
        root.dataset.modeSetting = originalModeSetting || originalMode || "dark";
        root.style.colorScheme = root.dataset.mode;

        await waitForPaint();
        inspectCurrentPage({ highlight: true });
        renderSweep(results);

        return results;
    }

    function createPanel() {
        panel = document.createElement("aside");
        panel.id = "cavecode-contrast-audit-panel";
        panel.setAttribute("aria-label", "CaveCode contrast audit");
        document.body.appendChild(panel);
        renderPanel(inspectCurrentPage({ highlight: true }));
    }

    function renderPanel(report) {
        if (!panel) return;

        panel.innerHTML = `
            <h2>Contrast audit</h2>
            <p>
                ${escapeHtml(report.path)} ·
                ${escapeHtml(report.theme)} ·
                ${escapeHtml(report.mode)}
            </p>

            <div class="cc-audit-summary">
                <div><strong>${report.tested}</strong>tested</div>
                <div><strong>${report.failures.length}</strong>failures</div>
                <div><strong>${report.warnings.length}</strong>warnings</div>
            </div>

            <div class="cc-audit-actions">
                <button type="button" data-action="refresh">Audit page</button>
                <button type="button" data-action="sweep">Sweep themes</button>
                <button type="button" class="secondary" data-action="copy">Copy report</button>
                <button type="button" class="secondary" data-action="close">Close</button>
            </div>

            <p>
                ${ROUTES.length} routes and ${THEMES.length} themes were
                discovered when this pass was installed.
            </p>

            ${failureTable(report.failures)}
        `;

        panel.querySelector("[data-action='refresh']")
            ?.addEventListener("click", () =>
                inspectCurrentPage({ highlight: true })
            );

        panel.querySelector("[data-action='sweep']")
            ?.addEventListener("click", async event => {
                const button = event.currentTarget;
                button.disabled = true;
                button.textContent = "Sweeping…";

                try {
                    await sweepThemes();
                } finally {
                    button.disabled = false;
                    button.textContent = "Sweep themes";
                }
            });

        panel.querySelector("[data-action='copy']")
            ?.addEventListener("click", copyReport);

        panel.querySelector("[data-action='close']")
            ?.addEventListener("click", disableAudit);
    }

    function failureTable(failures) {
        if (!failures.length) {
            return `<p class="cc-audit-pass">No direct text failures found on this page.</p>`;
        }

        const rows = failures.slice(0, 20).map(item => `
            <tr>
                <td>${escapeHtml(item.selector)}</td>
                <td class="cc-audit-fail">${item.ratio}:1</td>
                <td>${escapeHtml(item.text)}</td>
            </tr>
        `).join("");

        return `
            <table>
                <thead>
                    <tr><th>Element</th><th>Ratio</th><th>Text</th></tr>
                </thead>
                <tbody>${rows}</tbody>
            </table>
        `;
    }

    function renderSweep(results) {
        if (!panel) return;

        const rows = results.map(item => `
            <tr>
                <td>${escapeHtml(item.theme)}</td>
                <td>${escapeHtml(item.mode)}</td>
                <td class="${item.failures ? "cc-audit-fail" : "cc-audit-pass"}">
                    ${item.failures}
                </td>
                <td>${item.warnings}</td>
            </tr>
        `).join("");

        const section = document.createElement("div");
        section.innerHTML = `
            <table>
                <thead>
                    <tr>
                        <th>Theme</th>
                        <th>Mode</th>
                        <th>Fails</th>
                        <th>Warn</th>
                    </tr>
                </thead>
                <tbody>${rows}</tbody>
            </table>
        `;

        panel.appendChild(section);
    }

    async function copyReport() {
        const safeReport = lastReport
            ? {
                ...lastReport,
                failures: lastReport.failures.map(stripElement),
                warnings: lastReport.warnings.map(stripElement)
            }
            : null;

        await navigator.clipboard.writeText(
            JSON.stringify(safeReport, null, 2)
        );
    }

    function stripElement(record) {
        const { element, ...rest } = record;
        return rest;
    }

    function escapeHtml(value) {
        return String(value)
            .replaceAll("&", "&amp;")
            .replaceAll("<", "&lt;")
            .replaceAll(">", "&gt;")
            .replaceAll('"', "&quot;")
            .replaceAll("'", "&#039;");
    }

    function queueAudit() {
        if (!auditEnabled() || auditQueued) return;
        auditQueued = true;

        requestAnimationFrame(() => {
            auditQueued = false;
            inspectCurrentPage({ highlight: true });
        });
    }

    function enableAudit() {
        localStorage.setItem(AUDIT_STORAGE_KEY, "true");
        document.documentElement.classList.add("cavecode-contrast-audit");

        if (!panel) createPanel();

        if (!observer) {
            observer = new MutationObserver(mutations => {
                const externalMutation = mutations.some(mutation =>
                    !panel || !panel.contains(mutation.target)
                );

                if (externalMutation) queueAudit();
            });

            observer.observe(document.documentElement, {
                childList: true,
                subtree: true
            });
        }

        return inspectCurrentPage({ highlight: true });
    }

    function disableAudit() {
        localStorage.removeItem(AUDIT_STORAGE_KEY);
        document.documentElement.classList.remove("cavecode-contrast-audit");
        clearHighlights();

        observer?.disconnect();
        observer = null;
        panel?.remove();
        panel = null;
    }

    window.caveCodeContrastAudit = Object.freeze({
        version: VERSION,
        themes: [...THEMES],
        routes: [...ROUTES],
        enable: enableAudit,
        disable: disableAudit,
        run: () => inspectCurrentPage({ highlight: true }),
        sweepThemes,
        getLastReport: () => lastReport
    });

    if (document.readyState === "loading") {
        document.addEventListener("DOMContentLoaded", () => {
            if (auditEnabled()) enableAudit();
        }, { once: true });
    } else if (auditEnabled()) {
        enableAudit();
    }
})();
