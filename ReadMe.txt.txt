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