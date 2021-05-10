CREATE TABLE Users
(
Id BIGINT NOT NULL UNIQUE IDENTITY,
Username VARCHAR(30) NOT NULL UNIQUE,
[Password] VARCHAR(26) NOT NULL,
ProfilePicture VARBINARY(MAX),
LastLoginTime DATETIME2,
IsDeleted BIT NOT NULL DEFAULT(0)
)

ALTER TABLE Users
ADD CONSTRAINT pk_Users PRIMARY KEY(Id)

ALTER TABLE Users
ADD CONSTRAINT ch_ProfilePicture CHECK(DATALENGTH(ProfilePicture) <= 900 * 1024)

/* TEST ProfilePicture CONSTRAINT 
DECLARE @C VARCHAR(MAX) = '|'
DECLARE @ProfilePicture VARBINARY(MAX) = CONVERT(VARBINARY(MAX), REPLICATE(@C, (921600))) --Must throw an exception
--DECLARE @ProfilePicture VARBINARY(MAX) = CONVERT(VARBINARY(MAX), REPLICATE(@C, (921599))) --Must past through size check
*/

INSERT INTO Users(username, [Password]) VALUES
('tsendy000', '000000'),
('tsendy001', '000001'),
('tsendy010', '000002'),
('tsendy011', '000003'),
('tsendy100', '000004')
