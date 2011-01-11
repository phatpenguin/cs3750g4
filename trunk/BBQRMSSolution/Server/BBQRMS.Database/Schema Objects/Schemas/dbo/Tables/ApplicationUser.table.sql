CREATE TABLE [dbo].[ApplicationUser]
(
	ID int identity(1,1) NOT NULL, 
	PIN varchar(50) NOT NULL,
	DisplayName varchar(100) NOT NULL
)
