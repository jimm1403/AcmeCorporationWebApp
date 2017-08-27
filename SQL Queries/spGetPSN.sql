USE master
GO
CREATE PROC spGetPSN
AS
BEGIN
	SELECT ProductSerialNumber, Uses, Valid
	FROM SERIALS
END