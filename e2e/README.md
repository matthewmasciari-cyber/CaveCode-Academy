# CaveCode Playwright Production Smoke Tests

This package tests the live CaveCode Academy site after each successful GitHub Pages deployment.

It verifies:

- Every current production route hydrates into a working Blazor page.
- JavaScript console errors and uncaught runtime errors fail the test.
- Failed network requests fail the test.
- The Blazor error UI remains hidden.
- Desktop Chromium and a Pixel 7-sized Chromium viewport both pass.
- Screenshots, traces, videos, and an HTML report are uploaded by GitHub Actions.

## Local command

```bash
cd e2e
npm install
npx playwright install chromium
npx playwright test
```
