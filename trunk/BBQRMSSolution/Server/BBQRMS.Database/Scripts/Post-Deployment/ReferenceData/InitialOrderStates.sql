-- =============================================
-- Script Template
-- =============================================
select top 0 *
into #OrderState
from (select * from [dbo].[OrderState]
union select top 0 * from [dbo].[OrderState]) a

insert into #OrderState
values 
(1, 'Cooking', 'K'),
(2, 'Cancelled','X'),
(3, 'Completed','D'),
(4, 'Saved','S'),
(5, 'In Progress','IP'),
(6, 'Closed','C')

update [dbo].[OrderState]
set Description = Source.Description, Code = Source.Code
from [dbo].[OrderState] Target inner join #OrderState Source on Target.id = Source.id

insert into [dbo].[OrderState] (Id, Description,Code)
select Source.*
from [dbo].[OrderState] Target right join #OrderState Source on Target.id = Source.id
where Target.id is null