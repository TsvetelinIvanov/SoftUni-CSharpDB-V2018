DELETE FROM Reports WHERE StatusId = 4

--Only one query must be pasted in Judge

DELETE FROM Reports 
WHERE StatusId = (SELECT Id FROM [Status] WHERE Label = 'blocked')
