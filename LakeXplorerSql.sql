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
	CloudinaryAssetId NVARCHAR(100),
    Description NVARCHAR(1000)
);

CREATE TABLE LakeSightings (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Longitude FLOAT NOT NULL,
    Latitude FLOAT NOT NULL,
    UserId INT,
    LakeId INT,
	CloudinaryAssetId NVARCHAR(100),
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

