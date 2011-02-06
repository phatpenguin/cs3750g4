-- =============================================
-- Script Template
-- =============================================
select top 0 *
into #PaymentType
from (select * from [dbo].[PaymentType]
union select top 0 * from [dbo].[PaymentType]) a

insert into #PaymentType
values 
(1, 'Cash', 'C'),
(2, 'Credit Card','CC'),
(3, 'Check','CK')

update [dbo].[PaymentType]
set Description = Source.Description, Code = Source.Code
from [dbo].[PaymentType] Target inner join #PaymentType Source on Target.id = Source.id

insert into [dbo].[PaymentType] (Id, Description,Code)
select Source.*
from [dbo].[PaymentType] Target right join #PaymentType Source on Target.id = Source.id
where Target.id is null
