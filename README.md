# Welcome to the VirtualLibrary!

## Technologies Used
![C#](https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=c-sharp&logoColor=white)
![.Net](https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white)
![MicrosoftSQLServer](https://img.shields.io/badge/Microsoft%20SQL%20Sever-CC2927?style=for-the-badge&logo=microsoft%20sql%20server&logoColor=white)


## Application Architecture
VirtualLibrary utilizes a Razor Pages viewport and ASP.NET 6 coded in C#. The database was constructed with Microsoft's SQL Server.

## Getting Started

1. Download [Visual Studio 2022](https://visualstudio.microsoft.com/). Once installed, open Visual Studio and select "Clone a repository"

***

![Screenshot_1](https://user-images.githubusercontent.com/99216796/193136853-4bda80c7-636a-4b25-a292-a46c950a2d64.png)

***
2. Copy and paste the following repo URL into the "Repository location" field then select "Clone": https://github.com/pm24mcgi/VirtualLibrary.git
***

![Screenshot_2](https://user-images.githubusercontent.com/99216796/193137320-36ebf2b4-48a1-4f12-a598-7d2ebaf3860c.png)

***
3. Once the project has opened and pulled down this repo from GitHub, navigate to microsoft.com and download the Developer version of [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads).
***

![Screenshot_3](https://user-images.githubusercontent.com/99216796/193137923-36b5eef8-5064-47f8-acd1-a0585a45ec68.png)

***
4. Finally, in order to interact with Microsoft SQL Server, we will need to downloads Microsoft's ORM, [SQL Server Management Studio(SSMS)](https://learn.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver16). Click the following link for the latest version:
***

![image](https://user-images.githubusercontent.com/99216796/193138401-8cbd2f80-8489-471e-bde7-4cc1af648dd0.png)

***
5. Once all programs have been installed, navigate back to the Visual Studio project solution and launch the application with debugging by selecting the filled in play button:
***

![Screenshot_4](https://user-images.githubusercontent.com/99216796/193138796-eeebc02f-ab9b-4d66-806b-7db517bb04bd.png)

***
6. The application will launch via local host on the port defined in your launchSettings.json file
***

![Screenshot_5](https://user-images.githubusercontent.com/99216796/193140212-3fcf5b45-2956-4736-8cbd-14cb15e11f9b.png)

***
7. The code base has automatically seeded 500 random books and two user profiles with separate role permissions.

User
 - username: user@vl.com
 - password: Password@123

Librarian
 - username: librarian@vl.com
 - password: Password@123

Please use either to access the system and login, or register an account of your own.
***

## Landing Page
![Screenshot_6](https://user-images.githubusercontent.com/99216796/193142044-f5c063b7-037c-4be8-955f-3953efc74eba.png)

## Login
![Screenshot_7](https://user-images.githubusercontent.com/99216796/193142187-e9c2d945-1675-41da-aca8-57900c04e555.png)

## Library
![Screenshot_8](https://user-images.githubusercontent.com/99216796/193142283-b9d6ef14-1fbf-467f-9c6d-2a130f60b556.png)

## Feature List
### 1. New account creation, log in and log out
* Users can log in and log out
* New users can register and select their library role

### 2. Books
* Logged in users can view all books and checkout books that are available.
* Logged in librarians can view all books, checkout books that are available and check in books that have been returned.
* Logged in librarians can create, edit and delete books.

## Contact
[View my GitHub](https://github.com/pm24mcgi) |
[Connect on LinkedIn](https://www.linkedin.com/in/patrickmcginn-1358b76b/) |
[Personal Portfolio Site](https://www.pmmcginn.com/)



