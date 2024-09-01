# CustomerManagementPortal

flowchart TD
    A[Start] --> B[Check Product Availability]
    B --> C{Product Available?}
    C -->|Yes| D[Place Order]
    C -->|No| E[End Process]
    D --> F[End: Order Placed]


Environment Setup

-- DB on Docker 
1. Install Docker Desktop. (See docker documentation on how to install docker https://docs.docker.com/desktop/)
2. After installation, open Power Shell as Admin, and run the below command. This will Pull the mssql image and start an sqlserver container
    docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=yourStrong(!)Password" -p 1433:1433 -d mcr.microsoft.com/mssql/server

-- Running the application
1. Clone the application from https://github.com/malotho/CustomerManagement.git
2. On Windows explorer, navigate to the repo ("...\CustomerManagementPortal-master\CustomerManagementPortal-master\CustomerManagementPortal")
3. run the below commands
      - dotnet build
      - dotnet ef database update - (this will restore the db from the migration)
      - dotnet run - (this will start the application)
4. on you browser https://localhost:5001

How this Works

To use the application, you ned to register firt. The home url (https://localhost:5001) will redirect you to the Login/Register page.
The navigation bar will have items that can not be opened until user has registered and logged in.
1. Click on Register, and fill in the form
      This form has Client-Side validation.
      All fields are required
      Username should be an email address
      Password has a length validatio
2. After restering, the user will be redirected to login form
    Login using the Username and password you just created.
3. On the Navigation bar, 
    click on MANAGE CATEGORIES, this will show an empty list of categories
    click "New Category" and fill in the category form
    Category form is  VALIDATED on both Server and Client side
        Required values is validate on the client-side
        CategoryCode format is validated on the Server.
    fill in the form corectly, and Save.
4. A new catgory is added in the Category list.
   You can delete, or Edit this category.
5. On the Navigation bar,
   click on MANAGE PRODUCT, this will show an empty list of products
   click "New Product" and fill in the form
   Select Category from the list
   Product Code is disabled because, this is a genereted value
   fill in the form corectly, and Save.




























