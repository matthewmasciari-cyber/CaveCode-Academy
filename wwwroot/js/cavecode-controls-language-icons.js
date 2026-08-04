(() => {
    const ICONS = {
        "GCL+ Control Systems Lab": {
            abbreviation: "GCL+",
            label: "GCL+ building automation controller icon",
            svg: `
                <svg viewBox="0 0 40 40"
                     role="img"
                     aria-hidden="true"
                     focusable="false">
                    <rect class="cc-icon-soft"
                          x="10" y="8"
                          width="20" height="24"
                          rx="3"></rect>

                    <path class="cc-icon-primary"
                          d="M14 13h12M14 18h7M14 23h12"></path>

                    <circle class="cc-icon-led"
                            cx="25.5" cy="18"
                            r="1.5"></circle>

                    <circle class="cc-icon-led"
                            cx="16" cy="27"
                            r="1.35"></circle>

                    <circle class="cc-icon-led"
                            cx="21" cy="27"
                            r="1.35"></circle>

                    <circle class="cc-icon-led"
                            cx="26" cy="27"
                            r="1.35"></circle>

                    <path class="cc-icon-primary"
                          d="M7 13h3M7 19h3M7 25h3
                             M30 13h3M30 19h3M30 25h3"></path>

                    <path class="cc-icon-primary"
                          d="M4.5 33c3.2-3.2 6.4-3.2 9.6 0
                             3.2 3.2 6.4 3.2 9.6 0
                             3.2-3.2 6.4-3.2 11.8 0"></path>
                </svg>
            `,
        },

        "PG Python Controls Studio": {
            abbreviation: "PG Py",
            label: "PG Python controls and diagnostics icon",
            svg: `
                <svg viewBox="0 0 40 40"
                     role="img"
                     aria-hidden="true"
                     focusable="false">
                    <path class="cc-icon-soft"
                          d="M12 9.5c0-3 2.3-5 5.4-5h5.2
                             c3.1 0 5.4 2 5.4 5v7.1
                             H17.8c-3.2 0-5.8 2.2-5.8 5.3z"></path>

                    <path class="cc-icon-soft"
                          d="M28 30.5c0 3-2.3 5-5.4 5h-5.2
                             c-3.1 0-5.4-2-5.4-5v-7.1
                             h10.2c3.2 0 5.8-2.2 5.8-5.3z"></path>

                    <circle class="cc-icon-led"
                            cx="17" cy="10.5"
                            r="1.4"></circle>

                    <circle class="cc-icon-led"
                            cx="23" cy="29.5"
                            r="1.4"></circle>

                    <path class="cc-icon-primary"
                          d="M5 21h5l2.2-4.2 3.1 8.4 2.8-5.3
                             2.6 3.4 2.7-6.1 2.8 3.8H35"></path>

                    <circle class="cc-icon-primary"
                            cx="5" cy="21"
                            r="1.5"></circle>

                    <circle class="cc-icon-primary"
                            cx="35" cy="21"
                            r="1.5"></circle>
                </svg>
            `,
        },
    };

    function normalize(value) {
        return (value || "")
            .replace(/\s+/g, " ")
            .trim();
    }

    function findCard(title) {
        const headings = document.querySelectorAll(
            "h1, h2, h3, h4, strong, [class*='title']"
        );

        for (const heading of headings) {
            if (normalize(heading.textContent) !== title) {
                continue;
            }

            const card = heading.closest(
                "[data-learning-difficulty-level], " +
                "article, section, [class*='language-card'], " +
                "[class*='path-card'], [class*='card']"
            );

            if (card) {
                return card;
            }
        }

        return null;
    }

    function findExistingIcon(card, abbreviation) {
        const candidates = card.querySelectorAll(
            "[class*='icon'], [class*='badge'], " +
            "[class*='symbol'], [class*='logo'], " +
            "span, div"
        );

        for (const element of candidates) {
            if (element.children.length > 2) {
                continue;
            }

            if (normalize(element.textContent) === abbreviation) {
                return element;
            }
        }

        return null;
    }

    function applyIcon(title, config) {
        const card = findCard(title);

        if (!card) {
            return;
        }

        if (
            card.querySelector(
                `.cavecode-controls-language-icon[data-icon-title="${title}"]`
            )
        ) {
            return;
        }

        const existingIcon =
            findExistingIcon(card, config.abbreviation);

        if (!existingIcon) {
            return;
        }

        existingIcon.textContent = "";
        existingIcon.classList.add(
            "cavecode-controls-language-icon"
        );
        existingIcon.dataset.iconTitle = title;
        existingIcon.setAttribute("role", "img");
        existingIcon.setAttribute("aria-label", config.label);
        existingIcon.innerHTML = config.svg;
    }

    function applyAllIcons() {
        Object.entries(ICONS).forEach(
            ([title, config]) => applyIcon(title, config)
        );
    }

    let scheduled = false;

    function scheduleApply() {
        if (scheduled) {
            return;
        }

        scheduled = true;

        window.requestAnimationFrame(() => {
            scheduled = false;
            applyAllIcons();
        });
    }

    applyAllIcons();

    const observer = new MutationObserver(scheduleApply);

    observer.observe(document.documentElement, {
        childList: true,
        subtree: true,
    });

    window.addEventListener("pageshow", applyAllIcons);
})();
