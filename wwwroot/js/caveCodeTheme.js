(function () {
    const storageKey = "cavecode.appearance.v1";
    const defaults = {
        theme: "cave-classic",
        mode: "system",
        textSize: "normal",
        reducedMotion: false
    };

    let current = load();
    let mediaQuery = null;

    function load() {
        try {
            const saved = JSON.parse(localStorage.getItem(storageKey) || "{}");
            return { ...defaults, ...saved };
        } catch {
            return { ...defaults };
        }
    }

    function effectiveMode(mode) {
        if (mode !== "system") return mode;
        return window.matchMedia("(prefers-color-scheme: light)").matches ? "light" : "dark";
    }

    function apply(preferences) {
        const root = document.documentElement;
        root.dataset.theme = preferences.theme;
        root.dataset.modeSetting = preferences.mode;
        root.dataset.mode = effectiveMode(preferences.mode);
        root.dataset.textSize = preferences.textSize;
        root.dataset.reducedMotion = preferences.reducedMotion ? "true" : "false";
        root.style.colorScheme = root.dataset.mode;
    }

    function save() {
        localStorage.setItem(storageKey, JSON.stringify(current));
        apply(current);
        return { ...current };
    }

    function listenForSystemMode() {
        mediaQuery = window.matchMedia("(prefers-color-scheme: light)");
        mediaQuery.addEventListener?.("change", function () {
            if (current.mode === "system") apply(current);
        });
    }

    window.caveCodeTheme = {
        bootstrap: function () {
            current = load();
            apply(current);
            listenForSystemMode();
        },
        getPreferences: function () {
            current = load();
            apply(current);
            return { ...current };
        },
        setPreference: function (name, value) {
            if (!(name in defaults)) throw new Error("Unknown appearance preference: " + name);
            current = { ...current, [name]: value };
            return save();
        },
        reset: function () {
            current = { ...defaults };
            localStorage.removeItem(storageKey);
            apply(current);
            return { ...current };
        }
    };

    window.caveCodeTheme.bootstrap();
})();
