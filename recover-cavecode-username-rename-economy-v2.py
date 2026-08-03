#!/usr/bin/env python3
from pathlib import Path
import re
import shutil

root = Path.cwd()

required = [
    root / "Pages" / "Settings.razor",
    root / "Services" / "ProfileService.cs",
    root / "wwwroot" / "js" / "caveCodeProfile.js",
    root / "wwwroot" / "js" / "caveCodeAchievements.js",
    root / "wwwroot" / "index.html",
]

missing = [
    str(path.relative_to(root))
    for path in required
    if not path.exists()
]

if missing:
    raise SystemExit(
        "Run this installer from /workspaces/CaveCode-Academy. "
        "Missing: " + ", ".join(missing)
    )

original_backup = root / ".username-rename-economy-backup"
recovery_snapshot = root / ".username-rename-partial-state-backup"

for path in required:
    source = original_backup / path.relative_to(root)

    if not source.exists():
        raise SystemExit(
            "The original username-pass backup is missing: "
            + str(source.relative_to(root))
        )

    snapshot = recovery_snapshot / path.relative_to(root)
    snapshot.parent.mkdir(parents=True, exist_ok=True)
    shutil.copy2(path, snapshot)

    shutil.copy2(source, path)

backup = root / ".username-rename-economy-v2-backup"

for path in required:
    destination = backup / path.relative_to(root)
    destination.parent.mkdir(parents=True, exist_ok=True)
    shutil.copy2(path, destination)

# ---------------------------------------------------------------------
# Profile service and rename-result models.
# ---------------------------------------------------------------------
profile_service = r'''using Microsoft.JSInterop;
using System.Text.Json.Serialization;

namespace CaveCode.Services;

public sealed class ProfileService(IJSRuntime js)
{
    public ValueTask<ProfilePreferences> GetPreferencesAsync() =>
        js.InvokeAsync<ProfilePreferences>(
            "caveCodeProfile.getPreferences"
        );

    // Retained for compatibility with older components. Direct display-name
    // changes are blocked in JavaScript so the rename economy cannot be
    // bypassed through the old preference setter.
    public ValueTask<ProfilePreferences> SetDisplayNameAsync(
        string displayName
    ) =>
        js.InvokeAsync<ProfilePreferences>(
            "caveCodeProfile.setPreference",
            "displayName",
            displayName
        );

    public ValueTask<ProfileRenameStatus> GetRenameStatusAsync() =>
        js.InvokeAsync<ProfileRenameStatus>(
            "caveCodeProfile.getRenameStatus"
        );

    public ValueTask<ProfileRenameResult> RenameDisplayNameAsync(
        string displayName
    ) =>
        js.InvokeAsync<ProfileRenameResult>(
            "caveCodeProfile.renameDisplayName",
            displayName
        );

    public ValueTask<ProfilePreferences> SetTitleAsync(string title) =>
        js.InvokeAsync<ProfilePreferences>(
            "caveCodeProfile.setPreference",
            "title",
            title
        );

    public ValueTask<ProfilePreferences> SetEmblemAsync(string emblem) =>
        js.InvokeAsync<ProfilePreferences>(
            "caveCodeProfile.setPreference",
            "emblem",
            emblem
        );

    public ValueTask<ProfilePreferences> SetFeaturedAchievementAsync(
        string achievement
    ) =>
        js.InvokeAsync<ProfilePreferences>(
            "caveCodeProfile.setPreference",
            "featuredAchievement",
            achievement
        );

    public ValueTask<ProfilePreferences> ResetAsync() =>
        js.InvokeAsync<ProfilePreferences>(
            "caveCodeProfile.reset"
        );
}

public sealed class ProfilePreferences
{
    [JsonPropertyName("displayName")]
    public string DisplayName { get; set; } = "";

    [JsonPropertyName("title")]
    public string Title { get; set; } = "Cave Explorer";

    [JsonPropertyName("emblem")]
    public string Emblem { get; set; } = "crystal";

    [JsonPropertyName("featuredAchievement")]
    public string FeaturedAchievement { get; set; } =
        "Control Terminal Online";

    [JsonPropertyName("previousDisplayName")]
    public string PreviousDisplayName { get; set; } = "";

    [JsonPropertyName("displayNameChangeCount")]
    public int DisplayNameChangeCount { get; set; }

    [JsonPropertyName("lastDisplayNameChangedAt")]
    public string? LastDisplayNameChangedAt { get; set; }

    [JsonPropertyName("nextDisplayNameChangeAt")]
    public string? NextDisplayNameChangeAt { get; set; }
}

public sealed class ProfileRenameStatus
{
    [JsonPropertyName("currentDisplayName")]
    public string CurrentDisplayName { get; set; } = "";

    [JsonPropertyName("previousDisplayName")]
    public string PreviousDisplayName { get; set; } = "";

    [JsonPropertyName("isFirstChangeFree")]
    public bool IsFirstChangeFree { get; set; } = true;

    [JsonPropertyName("canChangeNow")]
    public bool CanChangeNow { get; set; } = true;

    [JsonPropertyName("currentCost")]
    public int CurrentCost { get; set; }

    [JsonPropertyName("futureCost")]
    public int FutureCost { get; set; } = 500;

    [JsonPropertyName("cooldownDays")]
    public int CooldownDays { get; set; } = 5;

    [JsonPropertyName("crystalBalance")]
    public int CrystalBalance { get; set; }

    [JsonPropertyName("canAfford")]
    public bool CanAfford { get; set; } = true;

    [JsonPropertyName("nextAvailableAt")]
    public string? NextAvailableAt { get; set; }

    [JsonPropertyName("remainingSeconds")]
    public long RemainingSeconds { get; set; }

    [JsonPropertyName("changeCount")]
    public int ChangeCount { get; set; }
}

public sealed class ProfileRenameResult
{
    [JsonPropertyName("success")]
    public bool Success { get; set; }

    [JsonPropertyName("message")]
    public string Message { get; set; } = "";

    [JsonPropertyName("costCharged")]
    public int CostCharged { get; set; }

    [JsonPropertyName("usedFreeChange")]
    public bool UsedFreeChange { get; set; }

    [JsonPropertyName("preferences")]
    public ProfilePreferences Preferences { get; set; } = new();

    [JsonPropertyName("status")]
    public ProfileRenameStatus Status { get; set; } = new();
}
'''

(root / "Services" / "ProfileService.cs").write_text(
    profile_service,
    encoding="utf-8",
)

# ---------------------------------------------------------------------
# Add a supported crystal-spending transaction to the achievement wallet.
# ---------------------------------------------------------------------
achievements_path = root / "wwwroot" / "js" / "caveCodeAchievements.js"
achievements = achievements_path.read_text(encoding="utf-8")

if "spendCrystals: function" not in achievements:
    anchor = "        getTitleOptions: function () {"

    spend_method = r'''        spendCrystals: function (
            amount,
            reason
        ) {
            const cost = Math.max(
                0,
                Math.floor(Number(amount) || 0)
            );

            const state = syncFromCourseProgress(load());

            if (cost === 0) {
                return {
                    success: true,
                    amountSpent: 0,
                    balance: state.crystals,
                    reason: String(reason || "")
                };
            }

            if (state.crystals < cost) {
                return {
                    success: false,
                    amountSpent: 0,
                    balance: state.crystals,
                    reason: String(reason || ""),
                    message:
                        `You need ${cost} Code Crystals but have ${state.crystals}.`
                };
            }

            state.crystals -= cost;
            save(state);

            return {
                success: true,
                amountSpent: cost,
                balance: state.crystals,
                reason: String(reason || "")
            };
        },

'''

    if anchor not in achievements:
        raise SystemExit(
            "Could not add spendCrystals to caveCodeAchievements.js."
        )

    achievements = achievements.replace(
        anchor,
        spend_method + anchor,
        1,
    )

achievements_path.write_text(
    achievements,
    encoding="utf-8",
)

# ---------------------------------------------------------------------
# Replace the browser profile store with rename history, cooldown,
# free-first-change logic, and 500-crystal subsequent changes.
# ---------------------------------------------------------------------
profile_js = r'''(function () {
    const storageKey = "cavecode.profile.v1";
    const renameCost = 500;
    const cooldownDays = 5;
    const cooldownMilliseconds =
        cooldownDays * 24 * 60 * 60 * 1000;

    const defaults = {
        displayName: "",
        title: "Cave Explorer",
        emblem: "crystal",
        featuredAchievement: "Control Terminal Online",
        previousDisplayName: "",
        displayNameChangeCount: 0,
        lastDisplayNameChangedAt: null,
        nextDisplayNameChangeAt: null
    };

    let current = load();

    function load() {
        try {
            const saved = JSON.parse(
                localStorage.getItem(storageKey) || "{}"
            );

            return normalizeStoredState({
                ...defaults,
                ...saved
            });
        } catch {
            return {
                ...defaults
            };
        }
    }

    function normalizeStoredState(value) {
        return {
            ...defaults,
            ...value,
            displayName:
                normalizeDisplayName(value.displayName),
            previousDisplayName:
                normalizeDisplayName(
                    value.previousDisplayName
                ),
            displayNameChangeCount:
                Number.isFinite(
                    Number(value.displayNameChangeCount)
                )
                    ? Math.max(
                        0,
                        Math.floor(
                            Number(
                                value.displayNameChangeCount
                            )
                        )
                    )
                    : 0,
            lastDisplayNameChangedAt:
                validDateOrNull(
                    value.lastDisplayNameChangedAt
                ),
            nextDisplayNameChangeAt:
                validDateOrNull(
                    value.nextDisplayNameChangeAt
                )
        };
    }

    function validDateOrNull(value) {
        if (!value) {
            return null;
        }

        const date = new Date(value);

        return Number.isNaN(date.getTime())
            ? null
            : date.toISOString();
    }

    function normalizeDisplayName(value) {
        return String(value ?? "")
            .replace(/\s+/g, " ")
            .trim()
            .slice(0, 24);
    }

    function normalizePreference(name, value) {
        if (name === "displayName") {
            return normalizeDisplayName(value);
        }

        return String(value ?? "")
            .slice(0, 80);
    }

    function save() {
        localStorage.setItem(
            storageKey,
            JSON.stringify(current)
        );

        window.dispatchEvent(
            new CustomEvent(
                "cavecode-profile-changed",
                {
                    detail: {
                        ...current
                    }
                }
            )
        );

        return {
            ...current
        };
    }

    function crystalBalance() {
        if (
            !window.caveCodeAchievements ||
            typeof window.caveCodeAchievements
                .getState !== "function"
        ) {
            return 0;
        }

        return Math.max(
            0,
            Math.floor(
                Number(
                    window.caveCodeAchievements
                        .getState()
                        .crystals
                ) || 0
            )
        );
    }

    function renameStatus() {
        current = load();

        const now = Date.now();
        const nextTimestamp =
            current.nextDisplayNameChangeAt
                ? new Date(
                    current.nextDisplayNameChangeAt
                ).getTime()
                : 0;
        const remainingMilliseconds =
            Math.max(
                0,
                nextTimestamp - now
            );
        const firstFree =
            current.displayNameChangeCount === 0;
        const cost =
            firstFree ? 0 : renameCost;
        const balance =
            crystalBalance();

        return {
            currentDisplayName:
                current.displayName,
            previousDisplayName:
                current.previousDisplayName,
            isFirstChangeFree:
                firstFree,
            canChangeNow:
                remainingMilliseconds === 0,
            currentCost:
                cost,
            futureCost:
                renameCost,
            cooldownDays,
            crystalBalance:
                balance,
            canAfford:
                cost === 0 ||
                balance >= cost,
            nextAvailableAt:
                remainingMilliseconds === 0
                    ? null
                    : current.nextDisplayNameChangeAt,
            remainingSeconds:
                Math.ceil(
                    remainingMilliseconds / 1000
                ),
            changeCount:
                current.displayNameChangeCount
        };
    }

    function sanitizeProgressionLocks(preferences) {
        if (!window.caveCodeAchievements) {
            return preferences;
        }

        const titleOptions =
            window.caveCodeAchievements
                .getTitleOptions();
        const featureOptions =
            window.caveCodeAchievements
                .getFeatureOptions();

        const titleAllowed = titleOptions.some(
            option =>
                option.title === preferences.title &&
                option.unlocked
        );

        const featureAllowed = featureOptions.some(
            option =>
                option.name ===
                    preferences.featuredAchievement &&
                option.unlocked
        );

        return {
            ...preferences,
            title: titleAllowed
                ? preferences.title
                : "Cave Explorer",
            featuredAchievement: featureAllowed
                ? preferences.featuredAchievement
                : "First Steps"
        };
    }

    window.caveCodeProfile = {
        getPreferences: function () {
            current =
                sanitizeProgressionLocks(
                    load()
                );

            localStorage.setItem(
                storageKey,
                JSON.stringify(current)
            );

            return {
                ...current
            };
        },

        getRenameStatus: function () {
            return renameStatus();
        },

        renameDisplayName: function (
            requestedName
        ) {
            current = load();

            const nextName =
                normalizeDisplayName(
                    requestedName
                );
            const statusBefore =
                renameStatus();

            if (
                nextName === current.displayName
            ) {
                return {
                    success: false,
                    message:
                        "Enter a different display name.",
                    costCharged: 0,
                    usedFreeChange: false,
                    preferences: {
                        ...current
                    },
                    status: statusBefore
                };
            }

            if (!statusBefore.canChangeNow) {
                return {
                    success: false,
                    message:
                        "Your display name is still in its five-day cooldown.",
                    costCharged: 0,
                    usedFreeChange: false,
                    preferences: {
                        ...current
                    },
                    status: statusBefore
                };
            }

            const cost =
                statusBefore.currentCost;

            if (
                cost > 0 &&
                !statusBefore.canAfford
            ) {
                return {
                    success: false,
                    message:
                        `This name change costs ${cost} Code Crystals. ` +
                        `Your current balance is ${statusBefore.crystalBalance}.`,
                    costCharged: 0,
                    usedFreeChange: false,
                    preferences: {
                        ...current
                    },
                    status: statusBefore
                };
            }

            if (cost > 0) {
                if (
                    !window.caveCodeAchievements ||
                    typeof window
                        .caveCodeAchievements
                        .spendCrystals !== "function"
                ) {
                    return {
                        success: false,
                        message:
                            "The Code Crystal wallet is unavailable. No crystals were charged.",
                        costCharged: 0,
                        usedFreeChange: false,
                        preferences: {
                            ...current
                        },
                        status: statusBefore
                    };
                }

                const spend =
                    window.caveCodeAchievements
                        .spendCrystals(
                            cost,
                            "Display name change"
                        );

                if (!spend.success) {
                    return {
                        success: false,
                        message:
                            spend.message ||
                            "The Code Crystal charge could not be completed.",
                        costCharged: 0,
                        usedFreeChange: false,
                        preferences: {
                            ...current
                        },
                        status:
                            renameStatus()
                    };
                }
            }

            const changedAt =
                new Date();
            const nextAvailable =
                new Date(
                    changedAt.getTime() +
                    cooldownMilliseconds
                );

            current = {
                ...current,
                previousDisplayName:
                    current.displayName,
                displayName:
                    nextName,
                displayNameChangeCount:
                    current.displayNameChangeCount + 1,
                lastDisplayNameChangedAt:
                    changedAt.toISOString(),
                nextDisplayNameChangeAt:
                    nextAvailable.toISOString()
            };

            const preferences = save();
            const statusAfter =
                renameStatus();

            return {
                success: true,
                message:
                    cost === 0
                        ? `Your free display-name change was used. ` +
                          `The next change costs ${renameCost} Code Crystals ` +
                          `and becomes available in ${cooldownDays} days.`
                        : `Display name changed for ${renameCost} Code Crystals. ` +
                          `Another change becomes available in ${cooldownDays} days.`,
                costCharged:
                    cost,
                usedFreeChange:
                    cost === 0,
                preferences,
                status:
                    statusAfter
            };
        },

        setPreference: function (
            name,
            value
        ) {
            if (!(name in defaults)) {
                throw new Error(
                    "Unknown profile preference: " +
                    name
                );
            }

            if (name === "displayName") {
                throw new Error(
                    "Use the confirmed display-name change flow."
                );
            }

            const nextValue =
                normalizePreference(
                    name,
                    value
                );

            if (
                name === "title" &&
                window.caveCodeAchievements
            ) {
                const allowed =
                    window
                        .caveCodeAchievements
                        .getTitleOptions()
                        .some(
                            option =>
                                option.title ===
                                    nextValue &&
                                option.unlocked
                        );

                if (!allowed) {
                    throw new Error(
                        "Claim the matching achievement first."
                    );
                }
            }

            if (
                name ===
                    "featuredAchievement" &&
                window.caveCodeAchievements
            ) {
                const allowed =
                    window
                        .caveCodeAchievements
                        .getFeatureOptions()
                        .some(
                            option =>
                                option.name ===
                                    nextValue &&
                                option.unlocked
                        );

                if (!allowed) {
                    throw new Error(
                        "Claim the matching achievement first."
                    );
                }
            }

            current = {
                ...current,
                [name]:
                    nextValue
            };

            return save();
        },

        reset: function () {
            current = load();

            // Reset vanity selections without bypassing the display-name
            // cost, cooldown, previous-name, or rename-count rules.
            current = {
                ...current,
                title:
                    defaults.title,
                emblem:
                    defaults.emblem,
                featuredAchievement:
                    defaults.featuredAchievement
            };

            return save();
        }
    };
})();
'''

(root / "wwwroot" / "js" / "caveCodeProfile.js").write_text(
    profile_js,
    encoding="utf-8",
)

# ---------------------------------------------------------------------
# Settings UI: explicit draft, rule explanation, confirmation modal,
# balance, and cooldown. Display names no longer auto-save on every key.
# ---------------------------------------------------------------------
settings_path = root / "Pages" / "Settings.razor"
settings = settings_path.read_text(encoding="utf-8")

display_block_old = r'''                        <label class="field-label">
                            <span>Display name</span>
                            <input type="text"
                                   maxlength="24"
                                   value="@Profile.DisplayName"
                                   placeholder="Use account name"
                                   @oninput="UpdateDisplayName" />
                            <small>
                                Leave blank to use your Google or GitHub name.
                            </small>
                        </label>
'''

display_block = r'''                        <div class="field-label name-change-field">
                            <span>Display name</span>

                            <div class="display-name-controls">
                                <input type="text"
                                       maxlength="24"
                                       autocomplete="off"
                                       autocapitalize="none"
                                       value="@DisplayNameDraft"
                                       disabled="@(!RenameStatus.CanChangeNow)"
                                       placeholder="Use account name"
                                       @oninput="UpdateDisplayNameDraft" />

                                <button type="button"
                                        class="review-name-button"
                                        disabled="@(!CanReviewNameChange)"
                                        @onclick="OpenRenameConfirmation">
                                    Review change
                                </button>
                            </div>

                            <small>
                                Leave blank to use your Google or GitHub name.
                                Clearing the name still counts as a name change.
                            </small>

                            <div class="rename-rule-card @(RenameStatus.IsFirstChangeFree ? "free" : "")">
                                @if (RenameStatus.IsFirstChangeFree)
                                {
                                    <strong>FIRST NAME CHANGE: FREE</strong>
                                    <span>
                                        Your first confirmed change costs 0 ◆.
                                        After using it, every name change costs
                                        500 ◆ and can occur only once every
                                        five days.
                                    </span>
                                }
                                else if (!RenameStatus.CanChangeNow)
                                {
                                    <strong>NAME CHANGE COOLDOWN ACTIVE</strong>
                                    <span>
                                        Your next change costs 500 ◆ and becomes
                                        available @NextRenameAvailability.
                                    </span>
                                }
                                else
                                {
                                    <strong>NEXT NAME CHANGE: 500 ◆</strong>
                                    <span>
                                        Confirming a new name deducts 500 Code
                                        Crystals and starts another five-day
                                        cooldown.
                                    </span>
                                }

                                <div class="rename-rule-meta">
                                    <span>Balance: ◆ @RenameStatus.CrystalBalance</span>
                                    <span>Current cost: @CurrentRenameCostLabel</span>
                                    <span>Cooldown: 5 days</span>
                                </div>
                            </div>

                            @if (!string.IsNullOrWhiteSpace(RenameError))
                            {
                                <div class="rename-inline-error">
                                    @RenameError
                                </div>
                            }
                        </div>
'''

if display_block_old in settings:
    settings = settings.replace(
        display_block_old,
        display_block,
        1,
    )
elif "name-change-field" not in settings:
    raise SystemExit(
        "Could not find the existing Display name field in Settings.razor."
    )

# Add confirmation modal before the Settings page closes.
modal_marker = "class=\"rename-confirmation-modal\""

if modal_marker not in settings:
    style_index = settings.find("\n<style>")

    if style_index < 0:
        raise SystemExit(
            "Could not find the Settings style block."
        )

    page_close = settings.rfind("</div>", 0, style_index)

    if page_close < 0:
        raise SystemExit(
            "Could not find the closing settings-page element."
        )

    modal = r'''
    @if (ShowRenameConfirmation)
    {
        <div class="rename-modal-overlay"
             role="presentation">
            <section class="rename-confirmation-modal"
                     role="dialog"
                     aria-modal="true"
                     aria-labelledby="rename-modal-title">
                <div class="rename-modal-icon">◆</div>

                <p class="eyebrow">
                    @(RenameStatus.IsFirstChangeFree
                        ? "FREE FIRST NAME CHANGE"
                        : "DISPLAY NAME CHANGE")
                </p>

                <h2 id="rename-modal-title">
                    @(RenameStatus.IsFirstChangeFree
                        ? "Use your free name change?"
                        : "Change your name for 500 ◆?")
                </h2>

                <div class="rename-name-preview">
                    <span>Current</span>
                    <strong>@CurrentNameForModal</strong>

                    <i>→</i>

                    <span>New</span>
                    <strong>@RequestedNameForModal</strong>
                </div>

                @if (RenameStatus.IsFirstChangeFree)
                {
                    <p>
                        This first confirmed change costs nothing. After it is
                        used, your next display-name change will cost
                        <strong>500 Code Crystals</strong>. You also will not
                        be able to change the name again for
                        <strong>five days</strong>.
                    </p>
                }
                else
                {
                    <p>
                        Confirming this change deducts
                        <strong>500 Code Crystals</strong> from your current
                        balance of <strong>◆ @RenameStatus.CrystalBalance</strong>.
                        You will not be able to change the name again for
                        <strong>five days</strong>.
                    </p>
                }

                <div class="rename-modal-summary">
                    <div>
                        <span>Cost now</span>
                        <strong>@CurrentRenameCostLabel</strong>
                    </div>

                    <div>
                        <span>Next change</span>
                        <strong>After 5 days</strong>
                    </div>

                    <div>
                        <span>Later cost</span>
                        <strong>500 ◆</strong>
                    </div>
                </div>

                <div class="rename-modal-actions">
                    <button type="button"
                            class="secondary-name-action"
                            @onclick="CloseRenameConfirmation">
                        Keep current name
                    </button>

                    <button type="button"
                            class="confirm-name-action"
                            disabled="@RenameSubmitting"
                            @onclick="ConfirmDisplayNameChange">
                        @(RenameSubmitting
                            ? "Changing…"
                            : RenameStatus.IsFirstChangeFree
                                ? "Confirm free change"
                                : "Confirm for 500 ◆")
                    </button>
                </div>
            </section>
        </div>
    }
'''

    settings = (
        settings[:page_close] +
        modal +
        settings[page_close:]
    )

# Add CSS before the first media query.
css_marker = "    .name-change-field {"

if css_marker not in settings:
    css_anchor = "    @@media (max-width: 780px) {"

    css = r'''    .name-change-field {
        gap: 8px;
    }

    .display-name-controls {
        display: grid;
        grid-template-columns: minmax(0, 1fr) auto;
        gap: 8px;
    }

    .review-name-button {
        min-width: 122px;
        padding: 9px 12px;
        color: var(--accent-contrast);
        background: var(--accent);
        border: 1px solid var(--accent);
        border-radius: 9px;
        font-weight: 900;
        cursor: pointer;
    }

    .review-name-button:disabled {
        color: var(--text-dim);
        background: var(--surface-strong);
        border-color: var(--border);
        cursor: not-allowed;
        opacity: .72;
    }

    .rename-rule-card {
        display: grid;
        gap: 7px;
        margin-top: 3px;
        padding: 12px;
        color: var(--text);
        background: var(--surface-soft);
        border: 1px solid var(--border);
        border-left: 4px solid var(--accent);
        border-radius: 9px;
    }

    .rename-rule-card.free {
        background:
            linear-gradient(
                135deg,
                var(--accent-surface),
                var(--surface-soft)
            );
        border-color: var(--accent-border);
    }

    .rename-rule-card > strong {
        color: var(--accent);
        font-size: 9px;
        letter-spacing: .7px;
    }

    .rename-rule-card > span {
        color: var(--text-muted);
        font-size: 10px;
        line-height: 1.55;
    }

    .rename-rule-meta {
        display: flex;
        flex-wrap: wrap;
        gap: 6px;
    }

    .rename-rule-meta span {
        padding: 5px 7px;
        color: var(--text-muted);
        background: var(--surface);
        border: 1px solid var(--border);
        border-radius: 6px;
        font-size: 8px;
        font-weight: 850;
    }

    .rename-inline-error {
        padding: 9px 10px;
        color: var(--danger-text);
        background: var(--danger-surface);
        border: 1px solid var(--danger-border);
        border-radius: 8px;
        font-size: 10px;
        line-height: 1.45;
    }

    .rename-modal-overlay {
        position: fixed;
        z-index: 160;
        inset: 0;
        display: grid;
        padding: 20px;
        place-items: center;
        background: rgba(3, 7, 10, .8);
        backdrop-filter: blur(8px);
    }

    .rename-confirmation-modal {
        width: min(590px, 100%);
        padding: 26px;
        color: var(--text);
        text-align: center;
        background: var(--surface);
        border: 1px solid var(--accent-border);
        border-radius: 16px;
        box-shadow: 0 30px 100px rgba(0, 0, 0, .5);
    }

    .rename-modal-icon {
        display: grid;
        width: 54px;
        height: 54px;
        margin: 0 auto 12px;
        place-items: center;
        color: var(--accent-contrast);
        background: var(--accent);
        border-radius: 50%;
        box-shadow: 0 0 24px var(--accent-glow);
        font-size: 21px;
        font-weight: 950;
    }

    .rename-confirmation-modal h2 {
        margin: 0;
        font-size: 24px;
    }

    .rename-confirmation-modal > p:not(.eyebrow) {
        margin: 14px auto 0;
        color: var(--text-muted);
        line-height: 1.65;
    }

    .rename-name-preview {
        display: grid;
        grid-template-columns: 1fr auto 1fr;
        gap: 8px;
        align-items: center;
        margin-top: 18px;
        padding: 12px;
        background: var(--surface-soft);
        border: 1px solid var(--border);
        border-radius: 10px;
        text-align: left;
    }

    .rename-name-preview span {
        display: block;
        color: var(--text-dim);
        font-size: 8px;
        text-transform: uppercase;
    }

    .rename-name-preview strong {
        display: block;
        overflow: hidden;
        font-size: 13px;
        text-overflow: ellipsis;
        white-space: nowrap;
    }

    .rename-name-preview i {
        grid-row: 1 / span 2;
        grid-column: 2;
        color: var(--accent);
        font-style: normal;
        font-weight: 950;
    }

    .rename-name-preview span:nth-of-type(2),
    .rename-name-preview strong:nth-of-type(2) {
        grid-column: 3;
    }

    .rename-modal-summary {
        display: grid;
        grid-template-columns: repeat(3, 1fr);
        gap: 7px;
        margin-top: 18px;
    }

    .rename-modal-summary div {
        padding: 10px;
        background: var(--surface-soft);
        border: 1px solid var(--border);
        border-radius: 8px;
        text-align: left;
    }

    .rename-modal-summary span,
    .rename-modal-summary strong {
        display: block;
    }

    .rename-modal-summary span {
        color: var(--text-dim);
        font-size: 8px;
        text-transform: uppercase;
    }

    .rename-modal-summary strong {
        margin-top: 4px;
        font-size: 11px;
    }

    .rename-modal-actions {
        display: grid;
        grid-template-columns: 1fr 1fr;
        gap: 9px;
        margin-top: 20px;
    }

    .rename-modal-actions button {
        width: 100%;
        padding: 11px 13px;
        border-radius: 9px;
        font-weight: 900;
        cursor: pointer;
    }

    .secondary-name-action {
        color: var(--text-muted);
        background: var(--surface-soft);
        border: 1px solid var(--border);
    }

    .confirm-name-action {
        color: var(--accent-contrast);
        background: var(--accent);
        border: 1px solid var(--accent);
    }

    .confirm-name-action:disabled {
        opacity: .65;
        cursor: wait;
    }

'''

    if css_anchor not in settings:
        raise SystemExit(
            "Could not find the Settings responsive CSS anchor."
        )

    settings = settings.replace(
        css_anchor,
        css + css_anchor,
        1,
    )

# Add mobile layout refinements.
if "display-name-controls" in settings and \
   ".display-name-controls {" in settings and \
   "grid-template-columns: 1fr;" not in settings[
       settings.find("@@media (max-width: 520px)"):
   ]:
    mobile_anchor = '''        .settings-footer,
        .panel-footer {
'''
    mobile_addition = '''        .display-name-controls,
        .rename-modal-actions,
        .rename-modal-summary {
            grid-template-columns: 1fr;
        }

'''
    settings = settings.replace(
        mobile_anchor,
        mobile_addition + mobile_anchor,
        1,
    )

# Add rename fields at the beginning of the Razor code block instead of
# depending on the existing field order.
field_marker = "    private ProfileRenameStatus RenameStatus"

if field_marker not in settings:
    code_anchor = "@code {"

    if code_anchor not in settings:
        raise SystemExit(
            "Could not find the @code block in Settings.razor."
        )

    rename_fields = """@code {
    private ProfileRenameStatus RenameStatus = new();
    private bool ShowRenameConfirmation;
    private bool RenameSubmitting;
    private bool CanReviewNameChange;
    private string DisplayNameDraft = "";
    private string RenameError = "";

"""

    settings = settings.replace(
        code_anchor,
        rename_fields,
        1,
    )

# Replace the automatic display-name method with a guarded draft handler.
method_pattern = re.compile(
    r'''    private async Task UpdateDisplayName\(ChangeEventArgs args\)
    \{
.*?
    \}

''',
    re.DOTALL,
)

rename_methods = r'''    private Task UpdateDisplayNameDraft(
        ChangeEventArgs args
    )
    {
        try
        {
            DisplayNameDraft =
                args.Value?.ToString() ?? "";

            RenameError = "";
            RefreshRenameButtonState();
        }
        catch (Exception exception)
        {
            CanReviewNameChange = false;
            RenameError =
                $"The name field could not be updated: {exception.Message}";
        }

        return Task.CompletedTask;
    }

    private void RefreshRenameButtonState()
    {
        string draft =
            NormalizeDisplayName(
                DisplayNameDraft
            );

        string currentName =
            NormalizeDisplayName(
                Profile?.DisplayName
            );

        CanReviewNameChange =
            RenameStatus is not null &&
            RenameStatus.CanChangeNow &&
            (
                RenameStatus.CurrentCost == 0 ||
                RenameStatus.CanAfford
            ) &&
            !string.Equals(
                draft,
                currentName,
                StringComparison.Ordinal
            );
    }

    private void OpenRenameConfirmation()
    {
        RenameError = "";
        RefreshRenameButtonState();

        if (!RenameStatus.CanChangeNow)
        {
            RenameError =
                $"Your display name can be changed again {NextRenameAvailability}.";

            return;
        }

        if (!CanReviewNameChange)
        {
            RenameError =
                NormalizeDisplayName(DisplayNameDraft) ==
                NormalizeDisplayName(Profile?.DisplayName)
                    ? "Enter a different display name."
                    : $"This change costs 500 ◆, but your balance is ◆ {RenameStatus.CrystalBalance}.";

            return;
        }

        ShowRenameConfirmation = true;
    }

    private void CloseRenameConfirmation()
    {
        ShowRenameConfirmation = false;
        RenameSubmitting = false;
    }

    private async Task ConfirmDisplayNameChange()
    {
        if (RenameSubmitting)
        {
            return;
        }

        RenameSubmitting = true;
        RenameError = "";

        try
        {
            ProfileRenameResult result =
                await ProfileService
                    .RenameDisplayNameAsync(
                        DisplayNameDraft
                    );

            Profile = result.Preferences;
            RenameStatus = result.Status;
            ProfileSaveMessage = result.Message;

            if (result.Success)
            {
                DisplayNameDraft =
                    Profile.DisplayName;

                RefreshRenameButtonState();
                ShowRenameConfirmation = false;
            }
            else
            {
                RenameError =
                    result.Message;
            }
        }
        catch (Exception exception)
        {
            RenameError =
                $"The name change could not be completed: {exception.Message}";
        }
        finally
        {
            RenameSubmitting = false;
        }
    }

'''

if method_pattern.search(settings):
    settings = method_pattern.sub(
        rename_methods,
        settings,
        count=1,
    )
elif "ConfirmDisplayNameChange" not in settings:
    raise SystemExit(
        "Could not replace UpdateDisplayName in Settings.razor."
    )

# Load rename status and draft after profile preferences.
load_anchor = '''        Profile =
            await ProfileService.GetPreferencesAsync();

        Ready = true;
'''

load_replacement = '''        Profile =
            await ProfileService.GetPreferencesAsync();

        RenameStatus =
            await ProfileService.GetRenameStatusAsync();

        DisplayNameDraft =
            Profile.DisplayName;

        RefreshRenameButtonState();

        Ready = true;
'''

if load_anchor in settings:
    settings = settings.replace(
        load_anchor,
        load_replacement,
        1,
    )
elif "await ProfileService.GetRenameStatusAsync()" not in settings:
    raise SystemExit(
        "Could not add rename-status loading to Settings.razor."
    )

# Make reset refresh the status and explain that the name was preserved.
reset_pattern = re.compile(
    r'''    private async Task ResetProfile\(\)
    \{
        Profile = await ProfileService\.ResetAsync\(\);
        ProfileSaveMessage = "Profile settings reset\.";
    \}
''',
    re.DOTALL,
)

reset_replacement = r'''    private async Task ResetProfile()
    {
        Profile =
            await ProfileService.ResetAsync();

        RenameStatus =
            await ProfileService
                .GetRenameStatusAsync();

        DisplayNameDraft =
            Profile.DisplayName;

        RefreshRenameButtonState();

        ProfileSaveMessage =
            "Title and emblem reset. Display name and rename history were preserved.";
    }
'''

if reset_pattern.search(settings):
    settings = reset_pattern.sub(
        reset_replacement,
        settings,
        count=1,
    )

# Add computed helper properties before OnAfterRenderAsync.
computed_anchor = '''    protected override async Task OnAfterRenderAsync(bool firstRender)
'''

computed = r'''    private string CurrentRenameCostLabel =>
        RenameStatus.CurrentCost == 0
            ? "FREE"
            : "500 ◆";

    private string CurrentNameForModal =>
        string.IsNullOrWhiteSpace(
            Profile?.DisplayName
        )
            ? "Account name"
            : Profile.DisplayName;

    private string RequestedNameForModal =>
        string.IsNullOrWhiteSpace(
            NormalizeDisplayName(
                DisplayNameDraft
            )
        )
            ? "Account name"
            : NormalizeDisplayName(
                DisplayNameDraft
            );

    private string NextRenameAvailability
    {
        get
        {
            if (
                RenameStatus.CanChangeNow ||
                string.IsNullOrWhiteSpace(
                    RenameStatus.NextAvailableAt
                )
            )
            {
                return "now";
            }

            if (
                DateTimeOffset.TryParse(
                    RenameStatus.NextAvailableAt,
                    out DateTimeOffset available
                )
            )
            {
                return available
                    .ToLocalTime()
                    .ToString(
                        "MMM d, yyyy 'at' h:mm tt"
                    );
            }

            return "after the five-day cooldown";
        }
    }

    private static string NormalizeDisplayName(
        string? value
    )
    {
        string normalized =
            string.Join(
                " ",
                (value ?? string.Empty)
                    .Split(
                        ' ',
                        StringSplitOptions
                            .RemoveEmptyEntries
                    )
            )
            .Trim();

        return normalized.Length <= 24
            ? normalized
            : normalized.Substring(0, 24);
    }

'''

if computed_anchor not in settings:
    raise SystemExit(
        "Could not find OnAfterRenderAsync in Settings.razor."
    )

settings = settings.replace(
    computed_anchor,
    computed + computed_anchor,
    1,
)

# Update default save text.
settings = settings.replace(
    '    private string ProfileSaveMessage =\n        "Profile changes save automatically.";',
    '    private string ProfileSaveMessage =\n        "Names require confirmation. Titles and emblems save automatically.";',
    1,
)

settings_path.write_text(
    settings,
    encoding="utf-8",
)

# ---------------------------------------------------------------------
# Cache bust both updated scripts.
# ---------------------------------------------------------------------
index_path = root / "wwwroot" / "index.html"
index = index_path.read_text(encoding="utf-8")

index = re.sub(
    r'js/caveCodeProfile\.js\?v=\d+',
    'js/caveCodeProfile.js?v=2',
    index,
    count=1,
)

index = re.sub(
    r'js/caveCodeAchievements\.js\?v=\d+',
    'js/caveCodeAchievements.js?v=9',
    index,
    count=1,
)

index_path.write_text(
    index,
    encoding="utf-8",
)

print("CaveCode display-name economy recovered and installed cleanly.")
print()
print("Rename rules:")
print("  - Existing users receive one free confirmed name change")
print("  - The first successful change costs 0 Code Crystals")
print("  - The first change immediately starts a five-day cooldown")
print("  - Every later successful change costs 500 Code Crystals")
print("  - Every later change also starts a five-day cooldown")
print("  - Clearing the custom name counts as a change")
print("  - Failed, unchanged, unaffordable, or cooldown-blocked attempts cost nothing")
print()
print("Settings UI:")
print("  - Display names no longer save while typing")
print("  - Review Change opens a confirmation modal")
print("  - The free-change warning explicitly explains the later 500-crystal cost")
print("  - Current balance, current cost, and cooldown are visible")
print("  - The modal repeats the cost and five-day restriction before confirmation")
print("  - Reset Profile no longer clears the name or rename history")
print()
print("Profile data retained for later leaderboard work:")
print("  - Previous display name")
print("  - Rename count")
print("  - Last change timestamp")
print("  - Next available timestamp")
print()
print("Prototype limitation:")
print("  - Cost and cooldown are enforced in this browser's local storage")
print("  - Global uniqueness still requires a shared server-side profile database")
print()
print("Recovered the exact pre-username files before applying v2.")
print("Partial-state snapshot saved in .username-rename-partial-state-backup/")
print("Fresh v2 backups saved in .username-rename-economy-v2-backup/")
print("Next command: dotnet build")
