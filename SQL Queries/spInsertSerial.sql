USE master
GO

CREATE PROC spInsertPSN
	@ProductSerialNumber Char(36),
	@Uses int,
	@Valid bit
AS
BEGIN
	INSERT INTO SERIALS(ProductSerialNumber, Uses, Valid)
	VALUES(@ProductSerialNumber, @Uses, @Valid);
END