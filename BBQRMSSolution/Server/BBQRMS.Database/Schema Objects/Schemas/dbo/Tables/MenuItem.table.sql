CREATE TABLE [dbo].[MenuItem]
(
	Id				int identity,
	Price			numeric(18,2) NOT NULL,
	Name			nvarchar(MAX) NOT NULL,
	Description		nvarchar(MAX) NOT NULL,
	Image			varbinary(MAX) 
)
