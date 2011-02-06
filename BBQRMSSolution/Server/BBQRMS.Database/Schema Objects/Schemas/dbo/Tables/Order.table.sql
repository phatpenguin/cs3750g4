CREATE TABLE [dbo].[Order]
(
	Id				int				identity,
	Number			int				NOT NULL,
	Date			datetime		NOT NULL,
	OrderTypeId		int				NOT NULL,
	DinerTypeId		int				NOT NULL,
	PaymentStatusId	int				NOT NULL,
	OrderStatusId	int				NOT NULL,
	Memo			text			
)
