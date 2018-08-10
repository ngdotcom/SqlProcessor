using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace SqlDataToText
{
    class Program
    {
        static void Main(string[] args)
        {
            string db = "CRELiquorStore";
            string startTime = "2018-04-17 00:00:00.000";
            string endTime = "2018-04-17 23:59:59.997";

            using (SqlConnection con = new SqlConnection(GlobalConfig.CnnString(db)))
            {
                SqlCommand scCmd = new SqlCommand("dbo.spGetInventory_Liquor", con);
                scCmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader;

                con.Open();

                scCmd.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = startTime;
                scCmd.Parameters.Add("@EndDate", SqlDbType.DateTime).Value = endTime;

                reader = scCmd.ExecuteReader();

                while (reader.HasRows)
                {
                    Console.WriteLine("\t{0}\t{1}", reader.GetName(0), reader.GetName(1));

                    while (reader.Read())
                    {
                        Console.WriteLine("\t{0}\t{1}", reader.GetDecimal(0),
                    reader.GetString(1));
                    }
                    reader.NextResult();
                }
            }
            Console.ReadLine();
        }
    }
}
