

      [Client]
          |
  [Data Transfer Object (for sending JSON)]
          |
    [Controllers]
  ... minimal, glue code, just your
      implementation of the behaviors
  ... getTimers(userID);
          |
  [ Business Layer ]
  ... utility functions,
  where I declare 'function getTimers()'
          |
[Data Layer/Persistence Layer/Data Provider (kind like a model)...every entity must have an id etc)...]
  .... CRUD is behavior but it isn't business logic
  ... this is also where you put your CRUD functions (deleteTimer, AddTimer, etc)
  ... because they also describe what your thing is going to look like when you perform certain transformations...
  ... kind of like your surgeon toolkit
  ... this is where you describe what things should like that you're going to save forever ]
  |
  |
  |
[[DB]]]


 Data/PracticeTimerContextSeeder.cs -> creates the seeder
 Data/PracticeTimerContext.cs

 Entities/Timer.cs
 Entities/TimerGroup //closes to the database
 Entities/TimerGroup.cs
//
// you want a service layer between your data transfer objects (i.e where you declare getAllTimers)



 Controllers/HomeController.cs //implementation of getAllTimers()
 Controllers/PracticeTimerController.cs
 Controllers/TimerController.cs

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

View App @
* http://127.0.0.1:5000/app/index.html


Refresh
- delete bin folderr
- dotnet ef database update
- dotnet run
