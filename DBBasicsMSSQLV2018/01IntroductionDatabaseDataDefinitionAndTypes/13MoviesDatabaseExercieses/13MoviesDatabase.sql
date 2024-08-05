CREATE DATABASE Movies
--It must be pasted in Judge without this above

CREATE TABLE Directors
(
Id INT NOT NULL PRIMARY KEY,
DirectorName NVARCHAR(50) NOT NULL,
Notes NTEXT
)

INSERT INTO Directors(Id, DirectorName) VALUES
(1, 'Petur Penchev'),
(2, 'Ivan Petrov'),
(3, 'Igna Gesheva'),
(4, 'Vasil Romanov'),
(5, 'George Zimmerman')

CREATE TABLE Genres
(
Id INT NOT NULL PRIMARY KEY,
GenreName NVARCHAR(50) NOT NULL,
Notes NTEXT
)

INSERT INTO Genres(Id, GenreName) VALUES
(1, 'Action'),
(2, 'Comedy'),
(3, 'Science Fiction'),
(4, 'Fantasy'),
(5, 'Horror')

CREATE TABLE Categories
(
Id INT NOT NULL PRIMARY KEY,
CategoryName NVARCHAR(50) NOT NULL,
Notes NTEXT
)

INSERT INTO Categories(Id, CategoryName) VALUES
(1, 'Film'),
(2, 'Series'),
(3, 'Documentary'),
(4, 'Reality'),
(5, 'Concert')

CREATE TABLE Movies
(
Id INT NOT NULL PRIMARY KEY,
Title NVARCHAR(255) NOT NULL,
DirectorId INT FOREIGN KEY REFERENCES Directors(Id),
CopyrightYear INT,
[Length] NVARCHAR(50),
GenreId INT FOREIGN KEY REFERENCES Genres(Id),
CategoryId INT FOREIGN KEY REFERENCES Categories(Id),
Rating INT,
Notes NTEXT
)

INSERT INTO Movies(Id, Title, DirectorId, CopyrightYear, GenreId, CategoryId) VALUES
(1, 'Dark Star VIII', 2, 2027, 3, 1),
(2, 'FantasyLand', 1, 2019, 4, 1),
(3, 'Cool Ice', 4, 2021, 1, 2),
(4, 'The Last Village', 3, 2031, 1, 3),
(5, 'Hangovers XI', 5, 2026, 2, 1)
