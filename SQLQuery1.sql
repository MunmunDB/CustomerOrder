
With TopRows as (
select O.CUSTOMERID, O.ORDERID,Convert(varchar,O.ORDERDATE,106) as OrderDate,
Convert(varchar, O.DELIVERYEXPECTED,106) as DeliveryExpected,O.CONTAINSGIFT,

ROW_NUMBER()Over(Partition By CustomerID order by OrderDate Desc) AS Row_Num
from 
Orders O with(nolock)

)
select TP.*, OI.PRICE, OI.QUANTITY, 
case when TP.CONTAINSGIFT=1 then 'Gift' else P.PRODUCTNAME end as ProductName from TopRows TP
left join OrderItems OI with(nolock) on TP.ORDERID=OI.ORDERID
left join PRODUCTS P with (nolock) on OI.PRODUCTID= P.PRODUCTID
 where Row_Num=1

select * from ORDERITEMS where ORDERID=9


