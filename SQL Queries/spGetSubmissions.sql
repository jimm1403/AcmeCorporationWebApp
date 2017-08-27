USE [master]
GO

CREATE PROCEDURE spGetSubmissions
AS
BEGIN
SELECT  FirstName, LastName, Email, PhoneNumber, DateOfBirth,
	ProductSerialNumber FROM  FORMSUB
END