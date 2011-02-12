CREATE TABLE [dbo].[Order]
(
	Id				int				identity,
	Number			int				NOT NULL,
	Date			datetime		NOT NULL,
	OrderTypeId		int				NOT NULL,
	DinerTypeId		int				NOT NULL,
	PaymentStateId	int				NOT NULL,
	OrderStateId	int				NOT NULL,
	Memo			text			
)
