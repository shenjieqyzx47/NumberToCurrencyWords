# Test plan

This plan covers the converter, the HTTP endpoint, and the browser page. The .NET test project has automated unit tests for conversion and input validation. Endpoint and browser checks below are manual; the solution does not currently include HTTP integration tests.

## Run the automated checks

From the repository root:

```powershell
dotnet test .\NumberToWords.sln
```

The tests cover ordinary amounts, zero, negative amounts, singular dollars and cents, values with no cents, and values near supported scale boundaries. They also check rejected input such as blank text, invalid characters, commas, exponent notation, and amounts with more than two fractional digits.

Check the frontend separately:

```powershell
cd .\NumberToWords\Web\convert-number-to-words
npm ci
npm run lint
npm run build
```

## Check the API by hand

Start the API on port 5000 using the commands in the README, then try these requests. Use a browser or `curl.exe`.

| Input | Expected result |
| --- | --- |
| `123.45` | HTTP 200 and `ONE HUNDRED AND TWENTY-THREE DOLLARS AND FORTY-FIVE CENTS` |
| `68092` | HTTP 200 and words for sixty-eight thousand ninety-two dollars |
| `-235.07` | HTTP 200, starting with `MINUS` and ending with `SEVEN CENTS` |
| `1.01` | HTTP 200 with singular `DOLLAR` and `CENT` |
| `0` | HTTP 200 with zero dollars |
| no `input` parameter | HTTP 400 with a Problem Details error |
| `abc`, `1,000`, or `1.234` | HTTP 400 with a Problem Details error |

For example:

```powershell
curl.exe -i "http://localhost:5000/convert?input=123.45"
curl.exe -i "http://localhost:5000/convert?input=1.234"
```

Confirm that successful responses have a plain-text body and invalid requests have status 400 with a useful error detail. The frontend reads that detail from the Problem Details response.

The API does not currently have a dedicated automated test for unexpected exceptions. If checking that path, use a temporary test route or a test substitute that throws, verify the response is HTTP 500, then remove the temporary setup.

## Check the browser page

With the API and Vite server running, open <http://localhost:5173> and check that:

- Entering `123.45` and submitting shows the expected wording.
- Submitting an invalid amount shows the API’s error message.
- The page remains usable after an error and accepts another amount.
- The layout and controls remain usable at a narrow browser width.

`npm run lint` and `npm run build` check the frontend source; these do not replace the browser checks above.
