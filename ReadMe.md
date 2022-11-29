Step 1: Create local databases on Sql server with the name 'Payroll' and 'Security'
Step 2: Run the attached sql files to create the table, indexes and populate the data
Step 3: Change the connection strings for the databases in the AppSettings.json file in the Payroll.Domain.Api project.
Step 4: Run the solution with default Api project set as the startup project.
Step 5: Call the Authenticate endpoint in the user controller with the below request body

//for employee
{
  "username": "emily",
  "password": "employee"
}

//for admin
{
  "username": "arron",
  "password": "admin"
}

the values you can read from the Users table in Security Database.
Step 6: copy the token value from the response of step 5 and paste in the Authentication Modal popup of swagger by clicking on the Authorize button on top right corner of swagger index file.
Step 7: run the endpoints on employee deductions controller.

## Tech Stack

.Net 6.0 | Dapper


## Project Structure

This project tries to follow Clean Architecture and DDD

|Project Name 		          |Dependent On                   |Remarks                      |
|-----------------------------|------------------------|-----------------------------|
|Payroll.Domain.API           |`Shared and Business`   | As the name implies this will have all the endpoints and configuration for retriveing data,including security |
|Payroll.Domain.Business      |`Data`                  | this holds all the business rules and logic to compute the employee deductions and aggregate|
|Payroll.Domain.Data  		  |`Shared`                | holds all the repositories to read data from the data source|
|Payroll.Domain.Shared        |``              		   | this holds all entities, models, DTOS and enums|

