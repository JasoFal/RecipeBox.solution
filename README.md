# _Recipe Box_

#### By _**Jason Falk**_

#### _A Fidgetech project._

## Technologies Used

* _Html/C#/Bootstrap_
* _MySql Workbench_
* _Dotnet_

## Description

_Read, create, edit and delete recipes. Add ingredients and category tags to those recipes. Has full validation functionality._

## Setup/Installation Requirements

1. _Open Git Bash/Open terminal of your choice navigate to directory of your choice and run this command `git clone https://github.com/JasoFal/RecipeBox.solution.git`_
2. _Once you have cloned the project, navigate to project folder using Git Bash/ a terminal of your choice using the `cd` command and run `code .` within project folder. Or you can use file explorer to open the project manually._

##### Installing Dependencies

3. _Navigate to the project directory, in this case **RecipeBox** using command `cd RecipeBox` in terminal._
4. _Then once in the Factory directory, run: `dotnet restore` and `dotnet build`._

##### Setting up database

5. _Within the **RecipeBox** directory create file named appsetting.json._
6. _locate file named `appsettings.example.json` and add example code to your appsettings.json adding your UserID and Password (Brackets [] not needed when adding UserId and Password). **Warning:** Do not rename the example file as it could have complications with git._
7. _Run `dotnet ef database update` to create database._

##### To run the project do the following:
1. _To run the app type `dotnet watch run` in terminal within **RecipeBox** directory._
2. _Then using a browser of your choice enter https://localhost:5001 into search bar._

## Known Bugs

* _Does not function properly with google chrome._

## License

[MIT](License.md)

Copyright (c) _5/13/2024_ _Jason Falk_