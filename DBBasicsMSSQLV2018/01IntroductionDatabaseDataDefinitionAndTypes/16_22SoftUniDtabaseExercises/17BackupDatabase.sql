BACKUP DATABASE SoftUni TO DISK = 'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\Backup\softuni-backup.bak'

USE CarRental

DROP DATABASE SoftUni

RESTORE DATABASE SoftUni FROM DISK = 'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\Backup\softuni-backup.bak'
