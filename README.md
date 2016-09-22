
# deploy:

Install React Frontend stuff
* go to /wwwroot:
  * `npm install`
  * `webpack`

Install SQLLite DB + seed DB
* go to site root:
  * `dotnet restore`
  * `dotnet ef database update`
  * `dotnet run`

View App @
* http://127.0.0.1:5000/app/index.html


Refresh
- delete bin folderr
- dotnet ef database update
- dotnet run
