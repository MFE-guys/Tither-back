# Payments and Expenses Manager for Tither

This project aims to develop an API capable of managing Tither's payments and expenses. The API will allow the registration of income and expenses, as well as the monitoring of this information through a dashboard.

The API is being developed using .NET 7 version, which offers advanced web development features and a more simplified and enhanced development experience.

The Payments and Expenses Manager for Tither will be an important project to keep track of finances and help make strategic decisions regarding money. We hope this solution will help Tither and other users maintain effective and simplified financial control.

# Requirements
 - NET 7 SDK

# Getting Started
- Clone the repository
- Open the solution in Visual Studio or your preferred IDE
- Build and run the solution
- Use a tool such as Postman to interact with the API

# Folder structure
This folder structure aims to organize the code in a logical and cohesive way, facilitating the maintenance and understanding of the project as a whole.

In the "Shared" folder, we have components that are shared by different layers of the application, such as DTOs, Enums, Constants, Exceptions, Requests, Utils, Validators, ValueObjects, and ViewModels. This folder is important to reduce code duplication and increase the reuse of components.

In the "Domain" folder, we have files that are directly related to the business logic of the application. For example, MappingProfiles, Models, Handlers, Pipelines, and Repositories (Interfaces/Contracts) files are important to define how the application will work in terms of business rules, information flow, and interactions between different parts of the application. It is also common to have Services (Interfaces) files in the "Domain" folder, which define the business operations that can be executed in different layers of the application.

Finally, we have the "Data" folder, which contains files related to the data layer of the application. This folder is common in projects that use some kind of ORM (Object-Relational Mapping), such as Entity Framework. Here we have Context and Repositories (Implementations) files, which define how the application will interact with the database.

Please refer to this folder structure to understand how the code is organized and how to find specific files related to different parts of the application.

![folder structure](https://i.imgur.com/8imHxS5.png)