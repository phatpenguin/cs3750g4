CREATE TABLE [dbo].[MenuItem]
(
	Id		int identity,
	MenuId	int NOT NULL,
	Name	nvarchar NOT NULL,
	Descr	nvarchar(MAX) NOT NULL
)
