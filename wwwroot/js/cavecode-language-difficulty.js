(() => {
    const SELECTOR =
        "[data-learning-difficulty-stars][data-learning-difficulty-level]";

    function createDifficultyRow(card) {
        if (
            card.querySelector(
                ":scope > .cavecode-learning-difficulty"
            )
        ) {
            return;
        }

        const rawStars =
            Number.parseInt(
                card.dataset.learningDifficultyStars || "0",
                10
            );

        const filledStars =
            Number.isFinite(rawStars)
                ? Math.min(4, Math.max(0, rawStars))
                : 0;

        const level =
            (card.dataset.learningDifficultyLevel || "Unrated").trim();

        const row = document.createElement("div");
        row.className = "cavecode-learning-difficulty";
        row.setAttribute(
            "aria-label",
            `Learning difficulty: ${level}, ${filledStars} of 4 stars`
        );
        row.title =
            `Learning difficulty: ${level} — ${filledStars} of 4 stars`;

        const copy = document.createElement("div");
        copy.className = "cavecode-learning-difficulty-copy";

        const label = document.createElement("span");
        label.className = "cavecode-learning-difficulty-label";
        label.textContent = "Learning difficulty";

        const levelText = document.createElement("strong");
        levelText.className = "cavecode-learning-difficulty-level";
        levelText.textContent = level;

        copy.append(label, levelText);

        const stars = document.createElement("span");
        stars.className = "cavecode-learning-difficulty-stars";
        stars.setAttribute("aria-hidden", "true");

        for (let index = 1; index <= 4; index += 1) {
            const star = document.createElement("span");
            const isFilled = index <= filledStars;

            star.className =
                "cavecode-learning-difficulty-star " +
                (isFilled ? "is-filled" : "is-empty");

            star.textContent = isFilled ? "★" : "☆";
            stars.appendChild(star);
        }

        row.append(copy, stars);

        const usesHeading = Array.from(
            card.querySelectorAll("h2, h3, h4, strong, p")
        ).find((element) =>
            element.textContent
                ?.trim()
                .toLowerCase()
                .includes("what this language is used for")
        );

        if (usesHeading) {
            const usesSection =
                usesHeading.closest(
                    "section, div, article"
                );

            if (
                usesSection &&
                usesSection !== card &&
                usesSection.parentElement
            ) {
                usesSection.insertAdjacentElement(
                    "beforebegin",
                    row
                );
                return;
            }

            usesHeading.insertAdjacentElement("beforebegin", row);
            return;
        }

        const description =
            card.querySelector(
                "h2 + p, h3 + p, .path-description, " +
                ".language-description, [class*='description']"
            );

        if (description) {
            description.insertAdjacentElement("afterend", row);
            return;
        }

        card.appendChild(row);
    }

    function applyDifficultyRows(root = document) {
        root.querySelectorAll(SELECTOR).forEach(createDifficultyRow);
    }

    let scheduled = false;

    function scheduleRefresh() {
        if (scheduled) {
            return;
        }

        scheduled = true;

        window.requestAnimationFrame(() => {
            scheduled = false;
            applyDifficultyRows();
        });
    }

    applyDifficultyRows();

    const observer = new MutationObserver(scheduleRefresh);

    observer.observe(document.documentElement, {
        childList: true,
        subtree: true,
    });

    window.addEventListener(
        "pageshow",
        () => applyDifficultyRows()
    );
})();
