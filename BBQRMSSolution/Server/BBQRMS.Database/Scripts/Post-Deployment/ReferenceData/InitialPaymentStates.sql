-- =============================================
-- Script Template
-- =============================================
select top 0 *
into #PaymentState
from (select * from [dbo].[PaymentState]
union select top 0 * from [dbo].[PaymentState]) a

insert into #PaymentState
values 
(1, 'Unpaid', 'U'),
(2, 'Partially Paid','I'),
(3, 'Paid','P')

update [dbo].[PaymentState]
set Description = Source.Description, Code = Source.Code
from [dbo].[PaymentState] Target inner join #PaymentState Source on Target.id = Source.id

insert into [dbo].[PaymentState] (Id, Description,Code)
select Source.*
from [dbo].[PaymentState] Target right join #PaymentState Source on Target.id = Source.id
where Target.id is null