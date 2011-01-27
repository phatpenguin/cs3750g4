-- =============================================
-- Script Template
-- =============================================
select top 0 *
into #PaymentType
from (select * from [dbo].[PaymentType]
union select top 0 * from [dbo].[PaymentType]) a

insert into #PaymentType
values 
(1, 'Cash'),
(2, 'Credit Card'),
(3, 'Debit Card')

set identity_insert [dbo].[PaymentType] on

update [dbo].[PaymentType]
set Id = Source.Id,
Descr = Source.Descr
from [dbo].[PaymentType] Target inner join #PaymentType Source on Target.id = Source.id

insert into [dbo].[PaymentType] (Id, Descr)
select Source.*
from [dbo].[PaymentType] Target right join #PaymentType Source on Target.id = Source.id
where Target.id is null

set identity_insert [dbo].[PaymentType] off
