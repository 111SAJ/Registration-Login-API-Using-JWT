Registration & Login API on ASP.NET Core using JWT

List and Create Employee is Secured with Token - If token is valid then only list and will create work.

1. appsettings.json - setup Key, Issuer, Audience, DurationInMinutes
2. Created JWT Service
3. Inject JWT in Program.cs file
4. LoginController > Call and created the JWT Constructor and if username and password matches generate the token and
   send it in response also.
5. Make the EmployeeController [Authorize] or a particular method such as GetEmployee or CreateEmployee

----

POSTMAN:

1. Login: http://localhost:5024/login
2. List: http://localhost:5024/Employee/index
3. Create: http://localhost:5024/Employee/create

