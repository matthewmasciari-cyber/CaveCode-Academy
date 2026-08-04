const { test, expect } = require("@playwright/test");

const routes = [
  "/",
  "/achievements",
  "/cpp",
  "/csharp",
  "/html-css",
  "/leaderboard",
  "/minigames",
  "/minigames/cpp",
  "/minigames/cpp/mission-2",
  "/minigames/cpp/mission-3",
  "/minigames/cpp/mission-4",
  "/minigames/cpp/mission-5",
  "/minigames/csharp",
  "/minigames/html-css",
  "/minigames/html-css/campaign",
  "/minigames/html-css/endless",
  "/minigames/html-css/mission-2",
  "/minigames/html-css/mission-3",
  "/minigames/html-css/mission-4",
  "/minigames/html-css/mission-5",
  "/minigames/python",
  "/python",
  "/settings",
];

const ignoredConsolePatterns = [
  /favicon/i,
  /third[- ]party cookie/i,
  /downloadable font/i,
  /lucide/i,
];

function shouldIgnoreConsole(message) {
  return ignoredConsolePatterns.some((pattern) => pattern.test(message));
}

async function waitForBlazor(page) {
  await page.waitForSelector("#app", { state: "attached" });

  await page.waitForFunction(() => {
    const app = document.querySelector("#app");
    if (!app) return false;

    const text = (app.textContent || "").replace(/\s+/g, " ").trim();
    const loadingScreen = app.querySelector(
      ".loading-progress, .loading-progress-text, .cavecode-loading-screen"
    );

    return text.length > 20 && !loadingScreen;
  }, null, { timeout: 60_000 });

  await page.waitForTimeout(750);
}

async function verifyHealthyPage(page, route) {
  const consoleErrors = [];
  const pageErrors = [];
  const failedRequests = [];

  page.on("console", (message) => {
    if (message.type() !== "error") return;

    const text = message.text();

    if (
      text.includes("Blocked script execution in 'about:srcdoc'") ||
      shouldIgnoreConsole(text)
    ) {
      return;
    }

    consoleErrors.push(text);
  });

  page.on("response", (response) => {
    if (response.status() >= 400) {
      consoleErrors.push(
        `HTTP ${response.status()} ${response.request().resourceType()} ${response.url()}`
      );
    }
  });

  page.on("pageerror", (error) => {
    pageErrors.push(error.message);
  });

  page.on("requestfailed", (request) => {
    const failure = request.failure();
    const url = request.url();

    if (
      failure &&
      !url.startsWith("data:") &&
      !url.includes("google-analytics") &&
      !url.includes("googletagmanager")
    ) {
      failedRequests.push(`${failure.errorText}: ${url}`);
    }
  });

  const response = await page.goto(route, {
    waitUntil: "domcontentloaded",
  });

  // GitHub Pages can return the Blazor 404 shell for direct client routes.
  expect([200, 404]).toContain(response ? response.status() : 200);

  await waitForBlazor(page);

  await expect(page.locator("body")).toBeVisible();
  await expect(page.locator("#app")).not.toBeEmpty();

  const errorUi = page.locator("#blazor-error-ui");
  if (await errorUi.count()) {
    await expect(errorUi).not.toBeVisible();
  }

  const appText = ((await page.locator("#app").innerText()) || "").trim();
  expect(appText.length).toBeGreaterThan(20);
  expect(appText).not.toContain("An unhandled error has occurred");

  await page.screenshot({
    path: `test-results/screenshots/${route === "/" ? "home" : route.slice(1).replaceAll("/", "--")}.png`,
    fullPage: true,
  });

  expect(
    pageErrors,
    `Runtime page errors on ${route}:\n${pageErrors.join("\n")}`
  ).toEqual([]);

  expect(
    consoleErrors,
    `Console errors on ${route}:\n${consoleErrors.join("\n")}`
  ).toEqual([]);

  expect(
    failedRequests,
    `Failed network requests on ${route}:\n${failedRequests.join("\n")}`
  ).toEqual([]);
}

for (const route of routes) {
  test(`production route loads: ${route}`, async ({ page }) => {
    await verifyHealthyPage(page, route);
  });
}

test("homepage internal navigation links work through Blazor", async ({ page }) => {
  const errors = [];

  page.on("pageerror", (error) => errors.push(error.message));
  page.on("console", (message) => {
    if (message.type() !== "error") return;

    const text = message.text();

    if (
      text.includes("Blocked script execution in 'about:srcdoc'") ||
      shouldIgnoreConsole(text)
    ) {
      return;
    }

    errors.push(text);
  });

  await page.goto("/", { waitUntil: "domcontentloaded" });
  await waitForBlazor(page);

  const targetRoutes = ["/csharp", "/python", "/cpp", "/html-css", "/minigames"];

  for (const route of targetRoutes) {
    const link = page.locator(`a[href="${route}"]:visible`).first();

    if (await link.count()) {
      await link.click();
      await waitForBlazor(page);
      await expect(page).toHaveURL(new RegExp(`${route.replace("/", "\\/")}(?:[?#].*)?$`));
      await page.goto("/", { waitUntil: "domcontentloaded" });
      await waitForBlazor(page);
    } else {
      await page.goto(route, { waitUntil: "domcontentloaded" });
      await waitForBlazor(page);
      await expect(page).toHaveURL(new RegExp(`${route.replace("/", "\\/")}(?:[?#].*)?$`));
      await page.goto("/", { waitUntil: "domcontentloaded" });
      await waitForBlazor(page);
    }
  }

  expect(errors, `Navigation errors:\n${errors.join("\n")}`).toEqual([]);
});
