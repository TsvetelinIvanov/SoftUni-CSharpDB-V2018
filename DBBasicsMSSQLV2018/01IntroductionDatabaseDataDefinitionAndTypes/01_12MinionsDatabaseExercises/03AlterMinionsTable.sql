--ALTER TABLE Minions
--ADD TownId INT NOT NULL FOREIGN KEY REFERENCES Towns(Id)

ALTER TABLE Minions
ADD TownId INT NOT NULL

ALTER TABLE Minions
ADD CONSTRAINT fk_TownId FOREIGN KEY(TownId) REFERENCES Towns(Id)
