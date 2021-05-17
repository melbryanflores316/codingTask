# codingTask


API
- go inside "cleverbit-codingtask-master\Cleverbit.CodingTask", open "Cleverbit.CodingTask.sln" to open it via Visual Studio
- Before you run api solution, please check appsettings.development.json
  currently it is set to: "DefaultConnection": "Server=localhost;Database=CleverbitCodingaTasks;Trusted_Connection=True;"
  
- restore packages
- make sure the starting project is set to Cleverbit.CodingTask.Host, if not,
  - in the right side, right-click Cleverbit.CodingTask.Host, then set it to startup project

APP
- go inside 'cleverbit-task' , and run "npm install"
- please check environment apiUrl it is the same as the url emitted in .NETCORE CLI
- ng serve


upon logging in, you can create matches with default expiry of
DateTime.Now + 10minutes


