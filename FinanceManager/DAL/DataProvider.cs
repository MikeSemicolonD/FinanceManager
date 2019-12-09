using System;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;

namespace FinanceManager.DAL
{
    public class DataProvider
    {

        public DataProvider()
        {
            try
            {
                string ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
            }
            catch(Exception e)
            {
                throw e;
            }
        }

    }
}