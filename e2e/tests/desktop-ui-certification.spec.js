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

function screenshotName(route) {
  return route === "/"
    ? "home"
    : route.slice(1).replaceAll("/", "--");
}

async function waitForCaveCode(page) {
  await page.waitForFunction(() => {
    const app = document.querySelector("#app");
    if (!app) return false;

    const loading = app.querySelector(
      ".loading-progress, .loading-progress-text, .cavecode-loading-screen, .cavecode-startup-shell"
    );

    const text = (app.textContent || "").replace(/\s+/g, " ").trim();

    return text.length > 20 && !loading;
  }, null, { timeout: 60_000 });

  await page.waitForTimeout(500);
}

for (const route of routes) {
  test(`desktop UI certification: ${route}`, async ({ page }) => {
    await page.setViewportSize({
      width: 1440,
      height: 1000,
    });

    const pageErrors = [];
    const failedRequests = [];

    page.on("pageerror", error => {
      pageErrors.push(error.message);
    });

    page.on("requestfailed", request => {
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

    expect([200, 404]).toContain(response ? response.status() : 200);

    await waitForCaveCode(page);

    await expect(page.locator("body")).toBeVisible();
    await expect(page.locator("#app")).not.toBeEmpty();

    const errorUi = page.locator("#blazor-error-ui");
    if (await errorUi.count()) {
      await expect(errorUi).not.toBeVisible();
    }

    const appText = ((await page.locator("#app").innerText()) || "").trim();

    expect(appText.length).toBeGreaterThan(20);
    expect(appText).not.toContain("An unhandled error has occurred");

    const visualProblems = await page.evaluate(() => {
      const viewportWidth = document.documentElement.clientWidth;
      const viewportHeight = document.documentElement.clientHeight;

      const visibleElements = [...document.querySelectorAll("body *")]
        .filter(element => {
          const style = getComputedStyle(element);
          const rect = element.getBoundingClientRect();

          return (
            style.display !== "none" &&
            style.visibility !== "hidden" &&
            Number(style.opacity) !== 0 &&
            rect.width > 0 &&
            rect.height > 0
          );
        });

      const horizontalOverflow = visibleElements
        .filter(element => {
          const rect = element.getBoundingClientRect();

          return (
            rect.right > viewportWidth + 4 ||
            rect.left < -4
          );
        })
        .slice(0, 20)
        .map(element => ({
          tag: element.tagName.toLowerCase(),
          id: element.id || "",
          className:
            typeof element.className === "string"
              ? element.className
              : "",
          left: Math.round(element.getBoundingClientRect().left),
          right: Math.round(element.getBoundingClientRect().right),
          viewportWidth,
        }));

      const clippedText = visibleElements
        .filter(element => {
          const text = (element.textContent || "").trim();

          if (!text || element.children.length > 0) {
            return false;
          }

          const style = getComputedStyle(element);

          return (
            element.scrollWidth > element.clientWidth + 3 &&
            style.overflowX !== "visible"
          );
        })
        .slice(0, 20)
        .map(element => ({
          tag: element.tagName.toLowerCase(),
          text: (element.textContent || "").trim().slice(0, 100),
          clientWidth: element.clientWidth,
          scrollWidth: element.scrollWidth,
        }));

      const tinyInteractiveTargets = visibleElements
        .filter(element => {
          const tag = element.tagName.toLowerCase();
          const role = element.getAttribute("role");

          if (
            !["a", "button", "input", "select", "textarea"].includes(tag) &&
            role !== "button" &&
            role !== "link"
          ) {
            return false;
          }

          const rect = element.getBoundingClientRect();

          return (
            rect.width > 0 &&
            rect.height > 0 &&
            (rect.width < 32 || rect.height < 32)
          );
        })
        .slice(0, 20)
        .map(element => ({
          tag: element.tagName.toLowerCase(),
          text: (element.textContent || element.getAttribute("aria-label") || "")
            .trim()
            .slice(0, 100),
          width: Math.round(element.getBoundingClientRect().width),
          height: Math.round(element.getBoundingClientRect().height),
        }));

      return {
        viewportWidth,
        viewportHeight,
        documentWidth: document.documentElement.scrollWidth,
        horizontalOverflow,
        clippedText,
        tinyInteractiveTargets,
      };
    });

    await page.screenshot({
      path: `test-results/desktop-ui/${screenshotName(route)}.png`,
      fullPage: true,
    });

    console.log(
      `DESKTOP UI REPORT ${route}\n${JSON.stringify(visualProblems, null, 2)}`
    );

    expect(
      pageErrors,
      `Runtime errors on ${route}:\n${pageErrors.join("\n")}`
    ).toEqual([]);

    expect(
      failedRequests,
      `Failed requests on ${route}:\n${failedRequests.join("\n")}`
    ).toEqual([]);

    expect(
      visualProblems.documentWidth,
      `Page-level horizontal overflow on ${route}`
    ).toBeLessThanOrEqual(1444);

    expect(
      visualProblems.horizontalOverflow,
      `Overflowing elements on ${route}:\n${JSON.stringify(
        visualProblems.horizontalOverflow,
        null,
        2
      )}`
    ).toEqual([]);

    expect(
      visualProblems.clippedText,
      `Clipped text on ${route}:\n${JSON.stringify(
        visualProblems.clippedText,
        null,
        2
      )}`
    ).toEqual([]);
  });
}
