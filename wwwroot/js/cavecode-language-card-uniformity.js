(() => {
    const CARD_SELECTOR =
        "[data-learning-difficulty-stars]" +
        "[data-learning-difficulty-level]";

    const DESKTOP_MIN_WIDTH = 761;
    const ROW_TOLERANCE = 8;

    let scheduled = false;
    let observerPaused = false;

    function cardsAreVisible(cards) {
        return cards.filter((card) => {
            const rect = card.getBoundingClientRect();

            return (
                rect.width > 0 &&
                rect.height > 0 &&
                getComputedStyle(card).display !== "none"
            );
        });
    }

    function clearCardHeights(cards) {
        observerPaused = true;

        cards.forEach((card) => {
            card.style.removeProperty("min-height");
        });

        observerPaused = false;
    }

    function groupCardsByVisualRow(cards) {
        const rows = [];

        cards.forEach((card) => {
            const top = Math.round(
                card.getBoundingClientRect().top +
                window.scrollY
            );

            let row = rows.find(
                (candidate) =>
                    Math.abs(candidate.top - top) <= ROW_TOLERANCE
            );

            if (!row) {
                row = {
                    top,
                    cards: [],
                };

                rows.push(row);
            }

            row.cards.push(card);
        });

        return rows;
    }

    function equalizeRows() {
        const cards = cardsAreVisible(
            Array.from(document.querySelectorAll(CARD_SELECTOR))
        );

        clearCardHeights(cards);

        if (
            window.innerWidth < DESKTOP_MIN_WIDTH ||
            cards.length < 2
        ) {
            return;
        }

        const rows = groupCardsByVisualRow(cards);

        observerPaused = true;

        rows.forEach((row) => {
            if (row.cards.length < 2) {
                return;
            }

            const tallest = Math.ceil(
                Math.max(
                    ...row.cards.map(
                        (card) =>
                            card.getBoundingClientRect().height
                    )
                )
            );

            row.cards.forEach((card) => {
                card.style.minHeight = `${tallest}px`;
            });
        });

        observerPaused = false;
    }

    function scheduleEqualize() {
        if (scheduled) {
            return;
        }

        scheduled = true;

        window.requestAnimationFrame(() => {
            scheduled = false;
            equalizeRows();
        });
    }

    function reportLearningPathStatus() {
        const cards = Array.from(
            document.querySelectorAll(CARD_SELECTOR)
        );

        const gclCard = cards.find((card) =>
            card.textContent?.includes(
                "GCL+ Control Systems Lab"
            )
        );

        const pgPythonCard = cards.find((card) =>
            card.textContent?.includes(
                "PG Python Controls Studio"
            )
        );

        document.documentElement.dataset
            .cavecodeLanguageCardsReady =
                cards.length >= 12 &&
                Boolean(gclCard) &&
                Boolean(pgPythonCard)
                    ? "true"
                    : "false";
    }

    function refresh() {
        reportLearningPathStatus();
        scheduleEqualize();
    }

    const observer = new MutationObserver(() => {
        if (!observerPaused) {
            refresh();
        }
    });

    observer.observe(document.documentElement, {
        childList: true,
        subtree: true,
        characterData: true,
    });

    window.addEventListener("resize", scheduleEqualize);
    window.addEventListener("orientationchange", scheduleEqualize);
    window.addEventListener("pageshow", refresh);

    if (document.fonts?.ready) {
        document.fonts.ready.then(refresh);
    }

    refresh();
})();
