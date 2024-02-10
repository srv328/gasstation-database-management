Database Setup Instructions
---------------------------

To begin using the database in this C# application, follow these steps:

### 1\. Execute Initial SQL Query

Run the `query.sql` script in your MySQL database to set up the necessary tables and structure. This script contains the initial schema required for the application.

### 2\. Configure Database Connection

Open the `DBConnect.cs` file and provide your own database connection details. Modify the connection string to include the correct server, username, password, and database name for your local MySQL setup.

```csharp
string connectionString = "Server=<your_server>;Database=<your_database>;User Id=<your_username>;Password=<your_password>;";
```

### 3\. Install MySql.Data Package

Ensure that the `MySql.Data` package is installed. You can do this using the NuGet Package Manager. If not installed, execute the following command in the NuGet Package Manager Console:

```bash code
Install-Package MySql.Data
```
### Note:

*   Replace `<your_server>`, `<your_database>`, `<your_username>`, and `<your_password>` with your actual MySQL server details.

Now you should be ready to use the C# application with your local MySQL database. Make sure your MySQL server is running before running the application. If any issues arise during setup, refer to error messages for troubleshooting.

### Database Management in C# Application

The C# application utilizes a MySQL database to manage data related to users and gas stations in a gas station system. The implementation involves several key aspects of database management:

#### 1\. **Database Connection**

*   The application establishes a connection to the MySQL database using the `MySqlConnection` class.
*   A connection string containing information such as the database server, username, password, and database name is used to establish the connection.

#### 2\. **SQL Queries**

*   SQL queries are employed to interact with the database and perform operations such as user deletion, gas station addition, and data validation.
*   Parameterized queries are used to prevent SQL injection attacks and enhance security.
*   Queries are executed using `MySqlCommand` objects, and the results are processed as needed.

#### 3\. **User Deletion**

*   The user deletion module involves checking for associated data in the "FuelSale" table before proceeding with deletion.
*   Users are prompted for confirmation, and based on the user's response, either a regular user deletion or a cascading deletion is performed.

#### 4\. **Gas Station Management**

*   The gas station management module allows users to add new gas stations to the system.
*   Input validation ensures that the gas station address follows a correct format and is unique.
*   The existence of the associated firm is verified through validation methods, and SQL queries are executed to insert new gas stations.

#### 5\. **Data Validation**

*   Several methods are dedicated to validating data integrity, such as checking the uniqueness of gas station addresses and verifying the existence of firms.
*   Validation is crucial during user interactions to ensure the consistency and accuracy of the data entered into the system.

#### 6\. **Event Handling with DataGridView**

*   The application utilizes events provided by the DataGridView control, such as `CellBeginEdit` and `CellEndEdit`, to manage updates to gas station records.
*   Users can edit gas station information directly within the DataGridView, and updates to the database are performed asynchronously to improve responsiveness.

#### 7\. **Error Handling**

*   The application includes error handling mechanisms to manage exceptions that may occur during database operations.
*   Users are provided with informative error messages in case an operation fails, enhancing the user experience.

### Conclusion

The C# application effectively integrates with a MySQL database to provide robust functionalities for managing user and gas station data. The implementation showcases best practices such as parameterized queries, data validation, and asynchronous updates, contributing to a secure and responsive database management system.
