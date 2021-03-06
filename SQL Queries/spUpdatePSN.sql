USE master
GO

CREATE PROC spUpdatePSN
	@ProductSerialNumber	char(36),
	@Uses int,
	@Valid bit
AS
	IF EXISTS (SELECT ProductSerialNumber, Uses, Valid FROM SERIALS
		WHERE ProductSerialNumber = @ProductSerialNumber)
		
    BEGIN
        UPDATE SERIALS
		SET
			Uses = @Uses,
			Valid = @Valid
		WHERE
			ProductSerialNumber = @ProductSerialNumber
    END