# MyChat Project
This project aims to create a dynamic chat where people can register and talk to each other. 
Use bot to get some information about stock.

## Prerequisite
- Sql Server
- .NET 5.0

## Libraries used in the project
- RestSharp - To call Stock api
- Microsoft Identity
- Entity Framework

## How to set-up
- Using Visual Studio or Visual Studio Code:
    1. Clone the project.
    2. Open "src\MyChat.Web\appsettings.json" or "src\MyChat.Web\appsettings.Development.json" 
    3. Edit "DefaultConnection" key with your sql server identity

## Using the application in the browser
With the application running you can register your username, and start chatting.
You can use "/stock=SYMBOL" to call a "stock bot" and get information about the stock marketing of symbol you just typed.
You can enter a group name and join a private group.