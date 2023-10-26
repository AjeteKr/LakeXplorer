# LakeXplorerAlbiSoft

LakeXplorer is a web application that allows users to discover different lakes, view lake details, and share their lake sightings. It provides features for user registration, authentication using JWT tokens, and interacting with the API.

## Technologies Used
- C#
- ASP.NET Core
- MVC, Razor, Blazor
- HTML5, CSS3, JavaScript
- Entity Framework
- Microsoft SQL Server
- JWT for Authentication

## Features
- User Model with fields: email, username, password.
- JWT-based authentication for secure communication.
- Lake Model with properties: name, image, and description.
- Lake Sighting Model with properties: longitude, latitude, user reference, lake reference, image, and a fun fact of the day.
- Like Model with user reference and lake sighting reference.
- Scenarios:
  - Unregistered users can register.
  - Not logged-in users can log in and request an authentication token.
  - Users can see a list of lakes.
  - Users can see sightings for a specific lake.
  - Logged-in users can create a lake sighting.
  - Logged-in users can like a lake sighting.
  - Logged-in users can delete their lake sightings and likes.

## Installation and Setup
1. Clone this repository.
2. Ensure you have the .NET SDK and Visual Studio (or any code editor) installed.
3. Configure your database connection in `appsettings.json`.
4. Run the migration with `dotnet ef database update` to apply the database schema.
5. Start the application with `dotnet run`.

## Usage
- Register and log in to start exploring lakes.
- Create new lake sightings and like the sightings you find interesting.
- Enjoy exploring and sharing your favorite lakes with others!

## Tests
The project includes comprehensive tests to cover all the listed requirements. You can run tests by executing `dotnet test` in the terminal.

## Contributors
- Ajete Krasniq [email: ajetekr@gmail.com]

