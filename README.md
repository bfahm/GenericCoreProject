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

### Customization Guide

1. (Optional) Delete `.git`, `.vs`, `bin`, `obj` for a clean start
	- Don't forget to modify `.gitignore` to your needs
2. Rename `.sln` to your desired name
3. Rename `/GenericCore` folder and `.csproj` file to your project name
4. Replace "GenericCore" in `.sln` with your project name
5. Search and Replace ".GenericCore" (with Match Case and Match Word turned on) with ".{your_project_name}"
6. Search and Replace "namespacee GenericCore" (with Match Case and Match Word turned on) with "namspace {your_project_name}"
7. Search and configure any "generic" keywords left (with Match Case and Match Word turned off) references