CREATE TABLE [dbo].[PaymentType]
(
	Id				int				NOT NULL,
	Description		nvarchar(MAX)	NOT NULL,
	Code			nvarchar(2)		NOT NULL,
	IsCreditCard	bit				NOT NULL	DEFAULT 0
)
