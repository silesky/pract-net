dotnet restore
cd wwwroot && npm install && webpack && cd ..
dotnet ef database update && dotnet run
