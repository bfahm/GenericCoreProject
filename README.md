# Generic .Net Core Project

A generic .NET 5 empty template, ready for new projects right away.

### Key Features
The projects these components out of the box
- JWT Authentication
- Swagger
- Serilog
	- Email Logging
	- Console Logging
	- File Logging
- Automapper
- API Response templates (Success and Failure)

### Setup Guide

1. Make sure SQL Server is set up and ready for new databases
2. Migrations are needed to build up the database, so run `Add-Migration init`, followed by `Update-Database`
3. Check SQLServer to see if a database named "GenericCore" was added.
4. Clean, Rebuild, then Run