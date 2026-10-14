# Number to Words

This project turns monetary amounts into English words. It includes an ASP.NET Core API and a small React page for trying conversions in a browser. The conversion code is written for this project and does not use a number-to-words package.

## To Run this Application

You’ll need the .NET 10 SDK, Node.js, and npm.

From the repository root, start the API:

```powershell
dotnet restore .\NumberToWords.sln
dotnet run --project .\NumberToWords\NumberToWords.csproj --urls http://localhost:5000
```

In another terminal, start the web page:

```powershell
cd .\NumberToWords\Web\convert-number-to-words
npm ci
npm run dev
```

Open <http://localhost:5173>. During development, Vite forwards `/convert` requests to the API on port 5000. To use a different API address, set `VITE_API_URL` before starting the frontend.



The manual checks for the API and browser page are in [TEST_PLAN.md](TEST_PLAN.md). [DESIGN_DECISIONS.md](DESIGN_DECISIONS.md) explains how the conversion is organized and why these choices were made.

## Hosting

The API and React frontend are separate applications. For deployment, build the frontend with `npm run build` and serve its generated files from a static web server. Host the ASP.NET Core API separately and configure the web server to forward `/convert` to it, or set `VITE_API_URL` to the API’s address.
