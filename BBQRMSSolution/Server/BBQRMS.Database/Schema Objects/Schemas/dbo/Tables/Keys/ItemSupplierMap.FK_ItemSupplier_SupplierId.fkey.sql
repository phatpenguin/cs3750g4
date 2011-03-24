ALTER TABLE [dbo].[ItemSupplierMap]
	ADD CONSTRAINT [FK_ItemSupplier_SupplierId] 
	FOREIGN KEY (SupplierId)
	REFERENCES Supplier (Id)	

