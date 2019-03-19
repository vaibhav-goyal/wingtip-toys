# wingtip-toys

## Prerequisites
- [.NET Core SDK 2.2](https://dotnet.microsoft.com/download/dotnet-core/2.2)
- [Node and NPM](https://nodejs.org/en/download/) 

## How to Build and Run
1. From root run `dotnet restore`
2. From root run `dotnet build --no-restore`
3. Go To `.\src\WingtipToys.API`
4. Run `dotnet run --no-build`
5. Open new command prompt and go to `.\src\WingtipToys.UI\client-app`
6. Run `npm install`
7. Go to `.\src\WingtipToys.UI`
8. Run `dotnet run --no-build`
9. Open in browser http://localhost:5001

TODO : Publish intructions.

## Run Unit Tests
1. From root run `dotnet restore`
2. From root run `dotnet build --no-restore`
3. From root run `dotnet test --no-build`


## TODO
1. Logging
2. Global Exception Handling in API project


