(() => {
    "use strict";

    const STORAGE_KEY = "cavecode.languageFinder.autoPrompt.v1";
    const SECTION_SELECTOR = "#paths";
    const BUTTON_SELECTOR = "[data-language-finder-open]";

    let sectionObserver = null;
    let domObserver = null;
    let triggered = false;

    function wasShown() {
        try {
            return localStorage.getItem(STORAGE_KEY) === "shown";
        } catch {
            return false;
        }
    }

    function markShown() {
        try {
            localStorage.setItem(STORAGE_KEY, "shown");
        } catch {
            // The automatic prompt still works for the current page.
        }
    }

    function stopWatching() {
        sectionObserver?.disconnect();
        domObserver?.disconnect();
        sectionObserver = null;
        domObserver = null;
    }

    function triggerPrompt() {
        if (triggered || wasShown()) {
            stopWatching();
            return;
        }

        const button = document.querySelector(BUTTON_SELECTOR);

        if (!button) {
            return;
        }

        triggered = true;
        markShown();
        stopWatching();

        window.setTimeout(() => {
            button.click();
        }, 260);
    }

    function observeSection(section) {
        if (sectionObserver || triggered || wasShown()) {
            return;
        }

        sectionObserver = new IntersectionObserver(entries => {
            const visible = entries.some(entry =>
                entry.isIntersecting &&
                entry.intersectionRatio >= 0.12
            );

            if (visible) {
                triggerPrompt();
            }
        }, {
            threshold: [0.12, 0.25, 0.5]
        });

        sectionObserver.observe(section);
    }

    function findAndObserve() {
        if (triggered || wasShown()) {
            stopWatching();
            return true;
        }

        const section = document.querySelector(SECTION_SELECTOR);

        if (!section) {
            return false;
        }

        observeSection(section);
        return true;
    }

    function start() {
        if (wasShown()) {
            return;
        }

        if (findAndObserve()) {
            return;
        }

        // Blazor may insert the Home page after this script has already loaded.
        domObserver = new MutationObserver(() => {
            findAndObserve();
        });

        domObserver.observe(document.documentElement, {
            childList: true,
            subtree: true
        });

        // Fallback for unusual render timing or navigation restoration.
        window.setTimeout(findAndObserve, 500);
        window.setTimeout(findAndObserve, 1500);
    }

    if (document.readyState === "loading") {
        document.addEventListener("DOMContentLoaded", start, { once: true });
    } else {
        start();
    }
})();
