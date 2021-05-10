UPDATE Reports
SET StatusId = 2 WHERE StatusId = 1 AND CategoryId = 4

--Only one query must be paste in Judge

UPDATE Reports
SET StatusId = (SELECT Id FROM [Status] WHERE Label = 'in progress') 
WHERE StatusId = (SELECT Id FROM [Status] WHERE Label = 'waiting') AND
CategoryId = (SELECT Id FROM Categories WHERE [Name] = 'Streetlight')
