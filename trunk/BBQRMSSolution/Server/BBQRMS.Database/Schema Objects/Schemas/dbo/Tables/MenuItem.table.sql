CREATE TABLE [dbo].[MenuItem]
(
	id		int identity,
	menuId	int NOT NULL,
	name	nvarchar NOT NULL,
	descr	nvarchar(MAX) NOT NULL
)
