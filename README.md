# Restaurant Management App (C# .NETCore)
<ul>
  <li> A Web Application that allows users to manage employees, dining tables, foods, orders and bills in the restaurant with authentication and authorization </li>
  <li> The app was built with ASP.NET Core3.1 MVC with front-end Views created by Razor and Controllers implemented with Dependency Injection and Async/Await </li>
  <li> Back-end data storage and access was implemented with Microsoft SQL Server through Class Library calls using Dapper </li>
</ul>


## How to set up
1. Make sure to select RMUI as the startup project
2. Open the package manager console and run "Update Database" (this is for the user auth. data)
3. Select the RMData project and click Publish. 
4. Edit the connection string to a database (this is for the order objects)


## Localization

You can localize this application by:

Adding resource files for each controller. These resource files are accessed by the IStringLocalizer<Type> class.
After accessing them you can pass them to the view using ViewBag. 
