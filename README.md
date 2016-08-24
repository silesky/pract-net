 Data/PracticeTimerContextSeeder.cs -> creates the seeder
 Controllers/HomeController.cs
 Controllers/PracticeTimerController.cs
 Controllers/TimerController.cs
 Data/PracticeTimerContext.cs
 Entities/Timer.cs
 Entities/TimerGroup
 Entities/TimerGroup.cs


-------------
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




