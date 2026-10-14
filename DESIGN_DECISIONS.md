# Design notes

I used test-driven development for the conversion logic. The expected wording and edge cases are concrete, so writing tests first gave me a way to pin down the behavior before changing the algorithm. It also made refactoring safer: I could reorganize the converter and dictionary while checking that the same cases still passed. The tests focus on observable results, such as singular currency names, negative values, and invalid input, rather than on internal implementation details.

## How a request is handled

The `/convert` endpoint passes the supplied text to `ConvertAmountCommand`. The command checks the input and parses it using invariant culture. It then calls `NumberToWordsConverter`, which turns the dollar and cent values into words. `NumberWordsDictionary` holds the word lists and handles each three-digit group.

Each part has one job: the command deals with input, the converter deals with monetary wording, and the dictionary supplies the words. That keeps validation changes from getting tangled up with the conversion rules.

## Errors

Invalid input is an expected outcome, so the command returns a `ConversionResult` with either the converted text or an error message. The endpoint turns a failed result into HTTP 400 Problem Details, which the browser page can display to the user. Throwing an exception for ordinary input mistakes would make that path harder to follow.

Exceptions are still useful for unexpected failures. ASP.NET Core’s exception handler returns HTTP 500 for those, rather than presenting them as invalid input.

## Turning amounts into words

The converter uses `decimal` so monetary values are not affected by binary floating-point rounding. It separates the whole-dollar and cent portions, then spells each portion independently. Inputs with more than two decimal places are rejected; the converter does not silently round them.

The dictionary works in groups of three digits: units, thousands, millions, and so on. A single routine handles the hundreds, tens, and ones within each group, while lookup arrays supply the words. This avoids maintaining a separate block of conversion code for every scale. The supported range is less than 10²¹ in absolute value, matching the available scale names.

The wording follows the requested English currency format, including “AND” between hundreds and tens and singular “DOLLAR” or “CENT” when the amount is one. Negative values receive “MINUS”.

## API and browser page

The endpoint uses GET because it is easy to try from a browser or a command line. The input is not sensitive in this exercise; if this API were used for private financial data, a POST body would avoid putting the amount in the URL, where it can appear in logs and history.

Successful responses are plain text, which keeps the endpoint’s output parameter straightforward. Errors use Problem Details so the frontend can show the message without having to interpret a successful response as a failure.

The React page calls the same endpoint and shows either the returned words or the error message. Vite provides the local development server and proxy. In a hosted setup, the frontend’s static files and the API need to be served or routed by the deployment environment.

The .NET tests use xUnit. That dependency is limited to the test project; the application itself has no third-party conversion library or NuGet package for the algorithm.
