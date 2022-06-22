# CurrencyExchange

This is an online currency exchange website made in Blazor server application as an interview task for [Future Processing](https://www.future-processing.pl/).
Main goal of this task was to create a product that allows multiple users to log in and exchange various currencies with the currency exchange.

## Setup

1. Clone repository
2. Open the the project in Visual Studio 2019
3. Open SQL Server Object Explorer (View -> SQL Server Object Explorer or Ctrl+/, Ctrl+S)
4. SQL Server -> LocalDB
5. Right click the "Databases" folder and choose "Add New Database"
6. Name the database "aspnet-CurrencyExchange-CADF373D-ACB0-47B2-8FD5-63BC2613A5BF"
7. Right click the newly created database and choose "New Query"
8. Paste the contents of the CurrencyExchangeDBScript.sql file and execute the query

## Functionalities

* User accounts.
* Multi-user support.
* Exchange rate updated using HTTP with SignalR.
* Separate project for database access.

## Technologies

* [ASP.Net Core](https://www.selenium.dev/documentation/webdriver/)
* [Blazor](https://dotnet.microsoft.com/apps/aspnet/web-apps/blazor)
* [SignalR](https://docs.microsoft.com/en-us/aspnet/core/signalr/introduction?view=aspnetcore-6.0)
* [MSSQL](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
* [Dapper](https://github.com/DapperLib/Dapper)
