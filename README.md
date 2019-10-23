Prerequisites:
---------------
1. Visual Stdio 2019
2. .NET Core 2.1

Please clone the repository using following command:

git clone "https://github.com/parthapratimsarma/ContactMaintenanceCRUD.git"

Instructions to run:
--------------------
1. Open the ContactMaintenance solution inside ContactMaintenance folder
2. It contains two projects ContactMaintenance (API) and ContactMaintenanceTest (xUnit).
3. Set the ContactMaintenance API project as startup project.
4. Open appsettings.json and replace the connection string with appropriate one.
5. Run following commands in package manager console for creating database and necessary tables.
  
    a. Add-Migration dbCreateContact -Context ContactContext  
    
    b. Add-Migration dbCreateLog -Context LogContext
    
    c. update-database -Context ContactContext
    
    d. update-database -Context LogContext
    
6. Save and run the project.

Test with Swagger
--------------------
1. Replace with deault url in browser with https://localhost:{port number}/swagger
2. Test diferent API. 
3. Check dbo.Contacts and dbo.LogRecord tables to check data.

Validations for creating and updating contact
---------------------------------------------
1. Phone Number should match "123-123-1234" or "(123)-123-1234" or "(312) 123 1234" format
2. Email should match "abc@abc.com" or "abc.abc@abc.com"
3. FirstName, Email, PhoneNumber and Status are required fields.
4. Status field takes "Active" or "Inactive" value. If user enters 1, values stored as "Active" and i user enters 2, value stored as "Inactive"
