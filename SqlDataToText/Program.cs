using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;

namespace SqlDataToText
{
    class Program
    {
        static void Main(string[] args)
        {
            string startTime = "2018-04-17 00:00:00.000";
            string endTime = "2018-04-17 23:59:59.997";

            SqlProcessor test = new SqlProcessor();

            test.CreateShelfReport(startTime, endTime);

            
        }
    }
}
