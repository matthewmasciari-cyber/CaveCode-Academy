(() => {
    "use strict";

    const PASS_NAME = "page-header-contrast-v1";
    const INTRO_CLASS = "cavecode-page-intro";

    function isVisible(element) {
        if (!(element instanceof HTMLElement)) return false;
        const style = getComputedStyle(element);
        return style.display !== "none" &&
               style.visibility !== "hidden" &&
               element.getClientRects().length > 0;
    }

    function nextParagraphAfter(heading) {
        let sibling = heading.nextElementSibling;

        while (sibling) {
            if (sibling.matches("h1, h2, h3, section, article")) {
                return null;
            }

            if (sibling.matches("p")) {
                return sibling;
            }

            sibling = sibling.nextElementSibling;
        }

        return null;
    }

    function markStandardHeaders() {
        const roots = document.querySelectorAll([
            "header",
            ".page-header",
            ".hero-header",
            ".header-copy",
            ".settings-header",
            ".achievement-header",
            ".leaderboard-header",
            ".minigame-header",
            ".learning-path-header",
            ".lesson-header"
        ].join(","));

        roots.forEach(root => {
            const headings = root.querySelectorAll("h1, h2");

            headings.forEach(heading => {
                const paragraph = nextParagraphAfter(heading);

                if (paragraph && isVisible(paragraph)) {
                    paragraph.classList.add(INTRO_CLASS);
                    paragraph.dataset.cavecodeContrastPass = PASS_NAME;
                }
            });
        });
    }

    function markPrimaryPageHeading() {
        // Future pages may use an unfamiliar header class. Mark the paragraph
        // following the first visible H1 without touching card descriptions.
        const firstHeading = [...document.querySelectorAll("h1")]
            .find(isVisible);

        if (!firstHeading) return;

        const paragraph = nextParagraphAfter(firstHeading);

        if (paragraph && isVisible(paragraph)) {
            paragraph.classList.add(INTRO_CLASS);
            paragraph.dataset.cavecodeContrastPass = PASS_NAME;
        }
    }

    let queued = false;

    function apply() {
        markStandardHeaders();
        markPrimaryPageHeading();
    }

    function queueApply() {
        if (queued) return;
        queued = true;

        requestAnimationFrame(() => {
            queued = false;
            apply();
        });
    }

    function start() {
        apply();

        const observer = new MutationObserver(queueApply);
        observer.observe(document.documentElement, {
            childList: true,
            subtree: true
        });

        window.addEventListener("pageshow", queueApply);
        window.addEventListener("popstate", queueApply);

        window.caveCodeHeaderContrast = Object.freeze({
            version: PASS_NAME,
            refresh: apply
        });
    }

    if (document.readyState === "loading") {
        document.addEventListener("DOMContentLoaded", start, { once: true });
    } else {
        start();
    }
})();
