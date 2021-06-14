# Catalog Merger API
## Design Decisions
### Why .net core/.net 5.0.?
.net core is a great cross platform tool that will work on Mac, Windows and Linux.
.net core is greate for data processing and so it seemed like a perfect fit. 

### CQRS and Proxies?
Cqrs helps with the [single-responsibility principle](https://en.wikipedia.org/wiki/Single-responsibility_principle). Requests will usually have a focused task so there won't be a muddle of code for different purposes.
Downsides? It is easy for commands and queries to do the same thing when the project is scaled up (different developers may not know the request is already created).
Proxies are great way to help with the [open–closed principle](https://en.wikipedia.org/wiki/Open%E2%80%93closed_principle) and [interface segregation principle](https://en.wikipedia.org/wiki/Interface_segregation_principle). Currently all the proxies interact with csv's but it could be just as easy to add new classes that inherit the proxies interfaces to connect with apis or other file types.

### Why a list of companies as input instead of two?
It seems like this would be the logical progression of this merger. It seems just as easy to make it a list now then to do it in the future.

## Running the project
- Download or pull this repo
### If you have Visual Studio installed
1. Open the soloution
2. Hit the start button and a browser should open, navigating to the swagger url. If it doesn't it will be located here: https://localhost:44359/index.html.
3. Goto excute heading below.
### If you DON'T have Visual studio installed
1. Open up a command console or terminal window and naviate to the root of the project.
2. Run the "dotnet restore" command.
3. Navigate to "catalog.merger.api" project folder in terminal/cmd.
4. Run the "dotnet run" command which will launch the api.
5. In a browser navigate to the swagger page: https://localhost:5001/index.html.
6. Goto excute heading below.
### Execute
1. In the "CatalogMerger" post endpoint, click "try it out".
2. In the request body, add companies in to a json array named "companynames", like so:
```json
{
  "companyNames": [
    "A",
    "B"
  ]
}
```
3. Press the "Execute" button.
4. The output will be displayed in the "Responses" section. There will also be a csv file located in the "./OutputFiles" directory named "results_output_{datetime}".
5. You are done!

## Running the tests
### If you have Visual Studio installed
1. In the test explorer, hit the run all tests button.
2. The 6 tests should pass.
### If you DON'T have Visual studio installed
1. Open up a command console or terminal window and naviate to the root of the project.
2. Run the "dotnet restore" command.
3. Run the `dotnet test` command.
4. The 6 tests should pass -> Displayed in green text.