ALTER TABLE [dbo].[SupplierContactMap]
	ADD CONSTRAINT [FK_SupplierId_SupplierContactMap] 
	FOREIGN KEY (SupplierId)
	REFERENCES Supplier (Id)	

