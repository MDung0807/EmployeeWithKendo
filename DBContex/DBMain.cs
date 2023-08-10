using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace KendoUIApp2.DBContex
{
    public class DBMain
    {
        private string strConn = "Data Source=DESKTOP-GDQT4C6;Initial Catalog=Office;Integrated Security=True;TrustServerCertificate=True";

        private SqlConnection sqlConnection = null;
        private SqlCommand cmd = null;
        private SqlDataAdapter adapter = null;
        private DataTable dataTable = null;
        public DBMain()
        {
            sqlConnection = new SqlConnection(strConn);
            dataTable = new DataTable();
            cmd = sqlConnection.CreateCommand();
        }

        // Call view
        public DataTable loadData(string nameView)
        {
            dataTable = new DataTable();
            if (sqlConnection.State == ConnectionState.Open)
            {
                sqlConnection.Close();
            }
            sqlConnection.Open();
            string query = "select * from " + nameView;
            using (SqlCommand cmd = new SqlCommand(query, sqlConnection))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        dataTable.Columns.Add(reader.GetName(i), reader.GetFieldType(i));
                    }

                    // Đọc dữ liệu từ SqlDataReader và thêm vào DataTable
                    while (reader.Read())
                    {
                        DataRow dataRow = dataTable.NewRow();
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            dataRow[i] = reader[i];
                        }
                        dataTable.Rows.Add(dataRow);
                    }
                    reader.Close();
                }
            }
            return dataTable;

        }

        //Call Procedure
        public bool executeProc(string procName, CommandType cmdType, List<SqlParameter> parameters)
        {
            bool status = false;
            if (sqlConnection.State == ConnectionState.Open)
            {
                sqlConnection.Close();
            }
            sqlConnection.Open();
            cmd.CommandText = procName;
            cmd.CommandType = cmdType;
            cmd.Parameters.Clear();

            foreach (SqlParameter param in parameters)
            {
                cmd.Parameters.Add(param);
            }
            try
            {
                cmd.ExecuteNonQuery();
                status = true;
            }
            catch (Exception ex)
            {
                status = false;
            }
            return status;
        }

        //Call Function
        public DataTable executeFunc( string nameFunc, CommandType commandType, List<SqlParameter> parameters)
        {
            dataTable = new DataTable();
            try
            {
                // Mở kết nối
                sqlConnection.Open();

                // Tạo đối tượng SqlCommand và thiết lập kiểu command là StoredProcedure
                SqlCommand command = new SqlCommand(nameFunc, sqlConnection);
                command.CommandType = CommandType.StoredProcedure;

                // (Tùy chọn) Nếu function có tham số, thêm các tham số vào SqlCommand
                foreach(SqlParameter param in parameters)
                {
                    command.Parameters.AddWithValue(param.ParameterName, param.Value);
                }

                // Sử dụng SqlDataReader để đọc dữ liệu từ function
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    // Đổ dữ liệu vào DataTable
                    dataTable.Load(reader);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            return dataTable;
        }

        public bool executeFunc(string nameFunc)
        {
            dataTable = new DataTable();
            try
            {
                // Mở kết nối
                sqlConnection.Open();

                // Tạo đối tượng SqlCommand và thiết lập kiểu command là StoredProcedure
                SqlCommand command = new SqlCommand(nameFunc, sqlConnection);
                command.CommandType = CommandType.Text;
                string status = command.ExecuteScalar().ToString() ;
                return bool.Parse(status);
                // Sử dụng SqlDataReader để đọc dữ liệu từ function
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            return true;
        }

    }

}
