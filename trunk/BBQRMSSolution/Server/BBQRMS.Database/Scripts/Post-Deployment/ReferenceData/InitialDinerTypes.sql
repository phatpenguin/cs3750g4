-- =============================================
-- Script Template
-- =============================================
select top 0 *
into #DinerType
from (select * from [dbo].[DinerType]
union select top 0 * from [dbo].[DinerType]) a

insert into #DinerType
values 
(1, 'Dine In', 'I'),
(2, 'Carry Out','O'),
(3, 'Delivery','D')

update [dbo].[DinerType]
set Description = Source.Description, Code = Source.Code
from [dbo].[DinerType] Target inner join #DinerType Source on Target.id = Source.id

insert into [dbo].[DinerType] (Id, Description,Code)
select Source.*
from [dbo].[DinerType] Target right join #DinerType Source on Target.id = Source.id
where Target.id is null