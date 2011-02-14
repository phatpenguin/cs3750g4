-- =============================================
-- Script Template
-- =============================================
select top 0 *
into #OrderType
from (select * from [dbo].[OrderType]
union select top 0 * from [dbo].[OrderType]) a

insert into #OrderType
values 
(1, 'Phone', 'P'),
(2, 'Walk In','I'),
(3, 'Website','W')

update [dbo].[OrderType]
set Description = Source.Description, Code = Source.Code
from [dbo].[OrderType] Target inner join #OrderType Source on Target.id = Source.id

insert into [dbo].[OrderType] (Id, Description,Code)
select Source.*
from [dbo].[OrderType] Target right join #OrderType Source on Target.id = Source.id
where Target.id is null