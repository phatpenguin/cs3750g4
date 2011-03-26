CREATE TABLE [dbo].[MasterInventory]
(
	Id				int	identity(1,1),
	Name			nvarchar(MAX) NOT NULL,
	LocationId		int not NULL,
	UnitQty			int NOT NULL,
	UnitOfMeasure	nvarchar(max) NOT NULL,
	MinQuantity		int not null,
	MaxQuantity		int not null,
	IsActive		bit NOT NULL Default(1)
)
