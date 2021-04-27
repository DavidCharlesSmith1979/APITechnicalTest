** Create Database
In package Manager Console set the default project to TechnicalTest.Data

In Visual Studio Package Manager Console run:
Add-Migration InitialCreate
Update-Database

** Test Tasks
1. Run the existing unit tests, GivenANewUser_IShouldSeeTheUserIsCreated should pass
2. Fix the test: GivenUsers_IShouldSeeTheUsers
3. Write the test: GivenAUserIsDeleted_IShouldNoLongerBeAbleToGetTheUser

** Notes
You may need to update the appsettings for the location of the sqllite db file