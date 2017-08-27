USE master
GO

CREATE PROC	spInsertFormSub
	@FirstName varchar(50),
	@LastName varchar(50),
	@Email varchar(254),
	@PhoneNumber Char(8),
	@DateOfBirth Char(10),
	@ProductSerialNumber Char(36)
AS
BEGIN
	INSERT INTO FORMSUB(FirstName, LastName, Email, PhoneNumber,
		DateOfBirth, ProductSerialNumber)
	VALUES(@FirstName, @LastName, @Email, @PhoneNumber,
		@DateOfBirth, @ProductSerialNumber);
END