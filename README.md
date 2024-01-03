# Project Overview
This project implements scaffold technique, which scaffold the database table into model and load related data, call stored procedures and get data from views from MySQL Database for ASP.Net Core web API project.

# Note:
Scaffolding is commonly used in web development frameworks, such as ASP.NET, Ruby on Rails, Django, and Laravel, to streamline the process of creating CRUD (Create, Read, Update, Delete) operations, controllers, views, database migrations, and other components. It helps developers get started quickly by generating boilerplate code and reducing the manual effort required to set up the initial project structure.

The scaffolded code often follows best practices and conventions established by the framework, ensuring consistency and adherence to coding standards. It provides a foundation that developers can customize and extend according to their specific requirements.

# Steps to Set Up :
1) Add Connection String in appsetting to connect with your MySQL database:
``` Server=<server>;Database=<database>;Uid=<username>;Pwd=<password> ```

2) Packages to install:
-Microsoft.EntityFrameworkCore.Design
-Pomelo.EntityFrameworkCore.MySql

3) Install & Update dotnet EntityFramework tool to prevent incompatible version:
``` dotnet tool install --global dotnet-ef ```
``` dotnet tool update --global dotnet-ef ```

4) Scaffold MySQL Database:
``` dotnet ef dbcontext scaffold Name=UserDB Pomelo.EntityFrameworkCore.MySql --output-dir Models --context-dir Data --namespace CompleteDeveloperNetwork.Models --context-namespace CompleteDeveloperNetwork.Data --context UserDBContext -f ```
