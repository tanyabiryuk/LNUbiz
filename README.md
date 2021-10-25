# LNUbiz

# 1. About LNUbiz
The application to manage administration processes in Ivan Franko National University of Lviv. 
The immediate goal of the project at the moment is to create and manage applications for business trips of teachers and students.

# 2. Technologies
- React
- ASP.NET Core 5.0
- Microsoft SQL Server

3. Getting started
3.1. Clone or download the project from **https://github.com/tanyabiryuk/LNUbiz**

3.2 Install [ASP.NET Core Runtime 5.0](https://dotnet.microsoft.com/download/dotnet-core/5.0) and [ASP.NET Core SDK 5.0](https://dotnet.microsoft.com/download/dotnet/5.0)

3.3 Install [Microsoft SQL Server 2017+](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

3.4 Install [Node.js v10.19.0](https://nodejs.org/en/blog/release/v10.19.0/)+

3.5 Create local database from LNUbiz.DAL migrations.

(Open Package Manager Console, change default project to "LNUbiz.DAL"(make sure that Solution Explorer default project LNUbiz.Web) and write "Update-Database"),
then make sure that the database [LNUBiz] is created,if no again write "Update-Database" 
