<img src="public/icon_main.png" />

# PPassword-Manager
***
PPassword manager is a new solution designed to change the way you think about your passwords. Using Ppassword manager, you can generate much more secure passwords for all your websites using a single master key. And the cool thing is that you do not have to worry about your password being stolen or hacked, since they are not stored at all.
## How does it work
Everytime you request a password for any website, the PPassword manager **chrome extension** takes the website's title and your master key and generates a secure password for you. For every website-masterkey pair, it would generate the same password over and over again. So instead of storing your passwords directly in some database, it takes a more secure approach by regenarating your passwords based on some parameters, the parameters begin your site's title/url and your master password.
The extension provided will automatically guess the site's name for you everytime you visit some website, however you can modify that input as you wish in case the extension gets that name wrong.
## Architecture
### Core functionality
![1](public/core.png)
> In case you are not satisfied with a minimal extension and want a full-fledged password manager that remembers your generation history as well, you can use the **Blazor Webassembly** client. You would need to register yourself and for every password that you create, the backend api would store the website's url/title in the database. It would be provided to you at login so you can view your websites and regenerate the password.
### User authentication
![2](public/authentication.png)
### Blazor client
![3](public/auth_user.png)
***
## Screenshots
### Extension
<!-- ![Alt text](public/image1_ext.png) -->
![Alt text](public/main_ext.png)
![Alt text](public/main_gen.png)
<!-- ### Blazor client -->
## Technologies used
- C#, .NET
- Minimal API
- SQLite DB
- Entity Framework Core
- Javascript
- Blazor
- .NET Identity Framework
- K6 (For load testing)