(() => {
    "use strict";

    const PASS_NAME = "light-mode-notice-icon-contrast-v1";
    const NOTICE_CLASS = "cavecode-light-cooldown-notice";
    const TITLE_TEXT = "name-change cooldown active";
    const DETAIL_TEXT = "your next change costs";

    function normalizedText(element) {
        return String(element?.textContent || "")
            .replace(/\s+/g, " ")
            .trim()
            .toLowerCase();
    }

    function containsCooldownCopy(element) {
        const text = normalizedText(element);
        return text.includes(TITLE_TEXT) && text.includes(DETAIL_TEXT);
    }

    function findSmallestNoticeContainer() {
        const settingsPage = document.querySelector(".settings-page");
        if (!settingsPage) return null;

        const candidates = [
            ...settingsPage.querySelectorAll("div, aside, section, article, li")
        ].filter(containsCooldownCopy);

        if (!candidates.length) return null;

        // Prefer the smallest element whose direct descendants do not also
        // contain both lines. This normally selects the visible notice box.
        const smallest = candidates.find(candidate =>
            ![...candidate.children].some(containsCooldownCopy)
        );

        return smallest || candidates[candidates.length - 1];
    }

    function applyContrastClass() {
        const notice = findSmallestNoticeContainer();
        if (!notice) return false;

        document
            .querySelectorAll(`.${NOTICE_CLASS}`)
            .forEach(element => {
                if (element !== notice) {
                    element.classList.remove(NOTICE_CLASS);
                }
            });

        notice.classList.add(NOTICE_CLASS);
        notice.dataset.cavecodeContrastPass = PASS_NAME;
        return true;
    }

    let queued = false;

    function queueApply() {
        if (queued) return;
        queued = true;

        requestAnimationFrame(() => {
            queued = false;
            applyContrastClass();
        });
    }

    function start() {
        applyContrastClass();

        const observer = new MutationObserver(queueApply);
        observer.observe(document.documentElement, {
            childList: true,
            subtree: true,
            characterData: true
        });

        window.addEventListener("pageshow", queueApply);
        window.addEventListener("popstate", queueApply);

        window.caveCodeContrastHotfix = Object.freeze({
            version: PASS_NAME,
            refresh: applyContrastClass
        });
    }

    if (document.readyState === "loading") {
        document.addEventListener("DOMContentLoaded", start, { once: true });
    } else {
        start();
    }
})();
