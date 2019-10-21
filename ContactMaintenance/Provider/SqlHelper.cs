using ContactMaintenance.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ContactMaintenance.Provider
{
    public class SqlHelper
    {
        private string ConnectionString { get; set; }

        public SqlHelper(string connectionStr)
        {
            ConnectionString = connectionStr;
        }

        private bool ExecuteNonQuery(string commandStr, List<SqlParameter> paramList)
        {
            bool result = false;
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                if (conn.State != System.Data.ConnectionState.Open)
                {
                    conn.Open();
                }

                using (SqlCommand command = new SqlCommand(commandStr, conn))
                {
                    command.Parameters.AddRange(paramList.ToArray());
                    int count = command.ExecuteNonQuery();
                    result = count > 0;
                }
            }
            return result;
        }

        public bool InsertLog(LogRecord log)
        {
            string command = $@"INSERT INTO [dbo].[LogRecord] ([LogLevel],[Message],[CreatedTime]) VALUES (@LogLevel, @Message, @CreatedTime)";
            List<SqlParameter> paramList = new List<SqlParameter>
            {
                //new SqlParameter("EventID", log.EventID),
                new SqlParameter("LogLevel", log.LogLevel),
                new SqlParameter("Message", log.Message),
                new SqlParameter("CreatedTime", log.CreatedTime)
            };
            return ExecuteNonQuery(command, paramList);
        }
    }
}
