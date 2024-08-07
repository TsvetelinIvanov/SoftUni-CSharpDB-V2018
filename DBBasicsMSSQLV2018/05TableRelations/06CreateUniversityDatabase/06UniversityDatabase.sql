CREATE DATABASE University

--In Judge must be pasted without this above

CREATE TABLE Subjects
(
SubjectID INT NOT NULL CONSTRAINT PK_Subjects PRIMARY KEY,
SubjectName NVARCHAR(50) NOT NULL
)

CREATE TABLE Majors
(
MajorID INT NOT NULL CONSTRAINT PK_Majors PRIMARY KEY,
[Name] NVARCHAR(50) NOT NULL
)

CREATE TABLE Students
(
StudentID INT NOT NULL CONSTRAINT PK_Students PRIMARY KEY,
StudentNumber NVARCHAR(50) NOT NULL,
StudentName NVARCHAR(50) NOT NULL,
MajorID INT NOT NULL CONSTRAINT FK_Sudents_Majors FOREIGN KEY REFERENCES Majors(MajorID)
)

CREATE TABLE Agenda
(
StudentID INT NOT NULL,
SubjectID INT NOT NULL,
CONSTRAINT PK_Agenda PRIMARY KEY(StudentID, SubjectID),
CONSTRAINT FK_Agenda_Students FOREIGN KEY(StudentID) REFERENCES Students(StudentID),
CONSTRAINT FK_Agenda_Subjects FOREIGN KEY(SubjectID) REFERENCES Subjects(SubjectID)
) 

CREATE TABLE Payments
(
PaymentID INT NOT NULL CONSTRAINT PK_Payments PRIMARY KEY,
PaymentDate DATETIME NOT NULL,
PaymentAmount DECIMAL(15, 2) NOT NULL,
StudentID INT NOT NULL CONSTRAINT FK_Payments_Students FOREIGN KEY REFERENCES Students(StudentID)
)
