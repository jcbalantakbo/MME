# MME Application Setup Guide

## Prerequisites

Before starting, ensure the following tools and services are installed on your machine:

1. **Docker Desktop** - To run SQL Server in Docker.
2. **.NET SDK** - To build and run the .NET application.
3. **SQL Server Docker Image** - Used for the database.
4. **EF Core CLI Tool** - Used for managing database migrations.

## Steps to Set Up the Application

### 1. **Install Docker Desktop**

   If you don't have Docker Desktop installed, follow these steps:
   - **Download Docker Desktop** from [Docker's official site](https://www.docker.com/products/docker-desktop).
   - Install Docker Desktop on your system.
   - Ensure Docker is running after installation.

### 2. **Run SQL Server in Docker**

   Open a terminal (Command Prompt, PowerShell, or any terminal) and execute the following command to run SQL Server in Docker:

   docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=P@ss321!" -p 1433:1433 --name sql1 -d mcr.microsoft.com/mssql/server:2022-latest



## Run to install dotnet ef

dotnet tool install --global dotnet-ef

## Go to directory where you cloned the project ex. C:\Users\<user>\source\repos\MME
cd /d C:\Users\<user>\source\repos\MME

## Once in the directory run migration commands
dotnet ef migrations add InitialMigration --project MME.Persistence --startup-project MME.Api

## Update database
dotnet ef database update --project MME.Persistence --startup-project MME.Api

## Run the application


## This can benefit from integration test and test for custom mapping and validation which i didn't have time to implement