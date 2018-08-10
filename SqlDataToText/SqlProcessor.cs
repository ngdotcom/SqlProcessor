using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlDataToText
{
    public class SqlProcessor
    {
        private const string db = "CRELiquorStore";

        public void CreateShelfReport(string d1, string d2)
        {
            using (SqlConnection con = new SqlConnection(GlobalConfig.CnnString(db)))
            {
                var tableName = "liqTemp";
                var fileName = tableName + ".csv";
                var recordCount = 0;
                var fileCount = 0;

                SqlCommand scCmd = new SqlCommand("dbo.spGetInventory_Liquor", con);
                scCmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader;

                con.Open();

                scCmd.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = d1;
                scCmd.Parameters.Add("@EndDate", SqlDbType.DateTime).Value = d2;

                reader = scCmd.ExecuteReader();

                StreamWriter writer = null;

                try
                {
                    while (reader.Read())
                    {
                        if (writer == null || recordCount == 50000)
                        {
                            if (writer != null)
                            {
                                writer.Close();
                                writer.Dispose();
                            }
                            fileName = tableName + "_" + (++fileCount).ToString() + ".csv";

                            writer = new StreamWriter(fileName);
                        }
                        recordCount++;
                        writer.WriteLine("\t{0}\t{1}", reader.GetDecimal(0), reader.GetString(1));
                    }
                    reader.NextResult();
                }
                finally
                {
                    if (writer != null)
                    {
                        writer.Dispose();
                    }
                }
            }
        }
    }
}
