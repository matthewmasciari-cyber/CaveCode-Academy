(function () {
    "use strict";
    const ownedKey = "cavecode.owned-emblems.v1";
    const moduleCrystalKey = "cavecode.module-crystals.v1";
    const FREE = ["crystal", "cave", "terminal", "code"];
    const CATALOG = [
        { id: "crystal", name: "Crystal", icon: "gem", rarity: "common", cost: 0 },
        { id: "cave", name: "Cave", icon: "mountain", rarity: "common", cost: 0 },
        { id: "terminal", name: "Terminal", icon: "terminal", rarity: "common", cost: 0 },
        { id: "code", name: "Code", icon: "code-2", rarity: "common", cost: 0 },
        { id: "torch", name: "Torch", icon: "flame", rarity: "uncommon", cost: 25 },
        { id: "sparkles", name: "Sparkles", icon: "sparkles", rarity: "uncommon", cost: 25 },
        { id: "bulb", name: "Bulb", icon: "lightbulb", rarity: "uncommon", cost: 25 },
        { id: "star", name: "Star", icon: "star", rarity: "uncommon", cost: 25 },
        { id: "activity", name: "Activity", icon: "activity", rarity: "uncommon", cost: 25 },
        { id: "cpu", name: "CPU", icon: "cpu", rarity: "rare", cost: 50 },
        { id: "circuit", name: "Circuit", icon: "circuit-board", rarity: "rare", cost: 50 },
        { id: "plug", name: "Plug", icon: "plug-zap", rarity: "rare", cost: 50 },
        { id: "bot", name: "Bot", icon: "bot", rarity: "rare", cost: 50 },
        { id: "gauge", name: "Gauge", icon: "gauge", rarity: "rare", cost: 50 },
        { id: "window", name: "Window", icon: "app-window", rarity: "rare", cost: 50 },
        { id: "pointer", name: "Pointer", icon: "mouse-pointer-click", rarity: "rare", cost: 50 },
        { id: "zap", name: "Zap", icon: "zap", rarity: "epic", cost: 100 },
        { id: "fan", name: "Fan", icon: "fan", rarity: "epic", cost: 100 },
        { id: "thermo", name: "Thermo", icon: "thermometer", rarity: "epic", cost: 100 },
        { id: "sliders", name: "Sliders", icon: "sliders-horizontal", rarity: "epic", cost: 100 },
        { id: "shield", name: "Shield", icon: "shield-check", rarity: "epic", cost: 100 },
        { id: "rocket", name: "Rocket", icon: "rocket", rarity: "epic", cost: 100 },
        { id: "ghost", name: "Ghost", icon: "ghost", rarity: "legendary", cost: 150 },
        { id: "crown", name: "Crown", icon: "crown", rarity: "legendary", cost: 150 },
        { id: "cat", name: "Cat", icon: "cat", rarity: "legendary", cost: 150 },
        { id: "pizza", name: "Pizza", icon: "pizza", rarity: "legendary", cost: 150 }
    ];
    function loadOwned() {
        try {
            const parsed = JSON.parse(localStorage.getItem(ownedKey) || "[]");
            const list = Array.isArray(parsed) ? parsed.map(String) : [];
            return Array.from(new Set(FREE.concat(list)));
        } catch { return FREE.slice(); }
    }
    function saveOwned(list) { localStorage.setItem(ownedKey, JSON.stringify(Array.from(new Set(list)))); }
    function loadModuleGrants() {
        try {
            const parsed = JSON.parse(localStorage.getItem(moduleCrystalKey) || "[]");
            return Array.isArray(parsed) ? parsed.map(String) : [];
        } catch { return []; }
    }
    function crystalBalance() {
        try {
            if (window.caveCodeAchievements && window.caveCodeAchievements.getState) {
                return Math.max(0, Math.floor(Number(window.caveCodeAchievements.getState().crystals) || 0));
            }
        } catch (e) {}
        try {
            const raw = JSON.parse(localStorage.getItem("cavecode.achievements.v1") || "{}");
            return Math.max(0, Math.floor(Number(raw.crystals) || 0));
        } catch (e) { return 0; }
    }
    function spend(amount) {
        const cost = Math.max(0, Math.floor(Number(amount) || 0));
        if (cost === 0) return { success: true, balance: crystalBalance() };
        if (window.caveCodeAchievements && window.caveCodeAchievements.spendCrystals) {
            return window.caveCodeAchievements.spendCrystals(cost, "emblem-shop");
        }
        try {
            const raw = JSON.parse(localStorage.getItem("cavecode.achievements.v1") || "{}");
            const bal = Math.max(0, Math.floor(Number(raw.crystals) || 0));
            if (bal < cost) return { success: false, balance: bal };
            raw.crystals = bal - cost;
            localStorage.setItem("cavecode.achievements.v1", JSON.stringify(raw));
            return { success: true, balance: raw.crystals };
        } catch (e) { return { success: false, balance: 0 }; }
    }
    window.caveCodeCrystalShop = {
        getCatalog: function () {
            const owned = loadOwned();
            const balance = crystalBalance();
            return {
                balance: balance,
                items: CATALOG.map(function (item) {
                    return Object.assign({}, item, {
                        owned: owned.indexOf(item.id) >= 0,
                        affordable: balance >= item.cost
                    });
                })
            };
        },
        getOwned: function () { return loadOwned(); },
        buy: function (emblemId) {
            const id = String(emblemId || "");
            const item = CATALOG.filter(function (x) { return x.id === id; })[0];
            if (!item) return { success: false, message: "Unknown emblem." };
            const owned = loadOwned();
            if (owned.indexOf(id) >= 0) return { success: true, alreadyOwned: true, balance: crystalBalance(), owned: owned };
            if (item.cost <= 0) {
                owned.push(id); saveOwned(owned);
                return { success: true, balance: crystalBalance(), owned: owned };
            }
            const result = spend(item.cost);
            if (!result.success) return { success: false, message: "Not enough Code Crystals.", balance: result.balance, cost: item.cost };
            owned.push(id); saveOwned(owned);
            return { success: true, balance: result.balance, owned: owned, spent: item.cost };
        },
        grantModuleCrystals: function (course, moduleIndex) {
            const key = String(course) + ":" + Number(moduleIndex);
            const granted = loadModuleGrants();
            if (granted.indexOf(key) >= 0) return { awarded: 0, balance: crystalBalance() };
            const amount = Number(moduleIndex) % 8 === 7 ? 8 : 3;
            granted.push(key);
            localStorage.setItem(moduleCrystalKey, JSON.stringify(granted));
            try {
                if (window.caveCodeAchievements && window.caveCodeAchievements.awardMinigameCrystals) {
                    window.caveCodeAchievements.awardMinigameCrystals("module-crystal:" + key, amount);
                } else {
                    const raw = JSON.parse(localStorage.getItem("cavecode.achievements.v1") || "{}");
                    raw.crystals = Math.max(0, Math.floor(Number(raw.crystals) || 0)) + amount;
                    localStorage.setItem("cavecode.achievements.v1", JSON.stringify(raw));
                }
            } catch (e) {}
            return { awarded: amount, balance: crystalBalance() };
        }
    };
})();
