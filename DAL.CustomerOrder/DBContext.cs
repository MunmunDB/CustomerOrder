using DAL.CustomerOrderDemo.Repositories;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace DAL.CustomerOrderDemo
{
    /// <summary>
    /// This is the interface which allows access point to Database via SQLClient 
    /// </summary>
    public class DBContext :IDBContext
    {
        /// <summary>
        /// This establishes a connection to DB  & retreives the records as per the problem statement
        /// This code needs to be futher refactored , if time permits
        /// </summary>
        DataTable ConnectTODb_Retreive(string customerID)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

            builder.DataSource = "tcp:mmt-sse-test.database.windows.net";
            builder.UserID = "mmt-sse-test";
            builder.Password = "database-user-01";
            builder.InitialCatalog = "SSE_Test";

            String sql = $@"With TopRows as (
select O.CUSTOMERID, O.ORDERID,Convert(varchar,O.ORDERDATE,106) as OrderDate,
Convert(varchar, O.DELIVERYEXPECTED,106) as DeliveryExpected,O.CONTAINSGIFT,

ROW_NUMBER()Over(Partition By CustomerID order by OrderDate Desc) AS Row_Num
from 
Orders O with(nolock)

)
select TP.*, OI.PRICE, OI.QUANTITY, P.PRODUCTNAME from TopRows TP
left join OrderItems OI with(nolock) on TP.ORDERID=OI.ORDERID
left join PRODUCTS P with (nolock) on OI.PRODUCTID= P.PRODUCTID
 where Row_Num=1 and CustomerID=@CustomerID";

            DataTable records = new DataTable();
            using (SqlConnection connection = new SqlConnection(
               builder.ConnectionString))
            {
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@CustomerID", customerID);
               
                command.Connection.Open();
                var sqlreaderobj= command.ExecuteReader();
                records.Load(sqlreaderobj);
            }
            return records;
        }
       
       public DataTable GetLatestOrderDetailsForACustomer(string customerID)
        {
            try
            {
                return this.ConnectTODb_Retreive(customerID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }

        public DBContext()
        {

        }
    }
    /// <summary>
    /// This is interface for the Database class
    /// </summary>
    interface IDBContext {
        DataTable GetLatestOrderDetailsForACustomer(string customerID);
    }


}
