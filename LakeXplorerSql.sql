create database LAKEXPLORER_Database

use LAKEXPLORER_Database

CREATE TABLE Users (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Email NVARCHAR(255) NOT NULL,
    Username NVARCHAR(50) NOT NULL,
    Password NVARCHAR(255) NOT NULL
);

CREATE TABLE Lakes (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    Image NVARCHAR(255),
    Description NVARCHAR(1000)
);

CREATE TABLE LakeSightings (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Longitude FLOAT NOT NULL,
    Latitude FLOAT NOT NULL,
    UserId INT,
    LakeId INT,
    Image NVARCHAR(255),
    FunFact NVARCHAR(1000),
    FOREIGN KEY (UserId) REFERENCES Users(Id),
    FOREIGN KEY (LakeId) REFERENCES Lakes(Id)
);

CREATE TABLE Likes (
    Id INT PRIMARY KEY IDENTITY(1,1),
    UserId INT,
    SightingId INT,
    FOREIGN KEY (UserId) REFERENCES Users(Id),
    FOREIGN KEY (SightingId) REFERENCES LakeSightings(Id)
);



insert into Users (Email, Username, Password)
VALUES ('user1@gmail.com', 'user1', 'user1123'),
       ('user2@gmail.com', 'user2', 'user2456');

insert into Lakes (Name, Image, Description)
VALUES ('Lake A', 'lake_a.jpg', 'Beautiful lake in the mountains'),
       ('Lake B', 'lake_b.jpg', 'Serene lake surrounded by trees');

INSERT INTO LakeSightings (Longitude, Latitude, UserId, LakeId, Image, FunFact)
VALUES (45.1234, -78.5678, 1, 1, 'sighting_image1.jpg', 'Interesting fact about Lake A sighting'),
       (46.4321, -79.8765, 2, 2, 'sighting_image2.jpg', 'Interesting fact about Lake B sighting');


INSERT INTO Likes (UserId, SightingId)
VALUES (1, 1),
       (2, 1),
       (1, 2);
