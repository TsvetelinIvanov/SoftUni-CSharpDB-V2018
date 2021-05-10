--ALTER TABLE Payments
--DROP CONSTRAINT ch_TaxAmount

UPDATE Payments
SET TaxRate -= (TaxRate * 0.03)

SELECT TaxRate FROM Payments