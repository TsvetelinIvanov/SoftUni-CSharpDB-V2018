CREATE TABLE People
(
Id INT NOT NULL UNIQUE IDENTITY,
[Name] NVARCHAR(200) NOT NULL,
Picture VARBINARY(MAX),
Height NUMERIC(6, 2),
[Weight] NUMERIC(6, 2),
Gender CHAR(1) CHECK([Gender] IN('m', 'f')) NOT NULL,
Birthdate DATE NOT NULL,
Biography NVARCHAR(MAX)
)

ALTER TABLE People
ADD PRIMARY KEY(Id)

ALTER TABLE PEOPLE
ADD CONSTRAINT	ch_PictureSize CHECK(DATALENGTH(Picture) <= 2 * 1024 * 1024)

INSERT INTO People([Name], Height, [Weight], Gender, Birthdate) VALUES
('Petur Petroff', 1.70, 68, 'm', '01-08-1979'),
('Georgy Potnich', 1.80, 98, 'm', '01-08-1990'),
('Maria Yankova', 1.60, 60, 'f', '01-08-1991'),
('Plamena Berova', 1.74, 69, 'f', '01-08-1987'),
('Heinrich Bergman', 1.78, 68, 'm', '01-08-1965')

