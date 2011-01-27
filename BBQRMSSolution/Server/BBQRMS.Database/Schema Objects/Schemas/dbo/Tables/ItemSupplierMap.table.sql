CREATE TABLE [dbo].[ItemSupplierMap]
(
	Id					int	identity,
	MasterInventoryId	int NOT NULL,
	SupplierId			int NOT NULL
)
