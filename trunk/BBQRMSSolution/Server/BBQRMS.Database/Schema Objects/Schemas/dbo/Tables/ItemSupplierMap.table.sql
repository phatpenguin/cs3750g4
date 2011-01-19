CREATE TABLE [dbo].[ItemSupplierMap]
(
	id					int	identity,
	masterInventoryId	int NOT NULL,
	supplierId			int NOT NULL
)
