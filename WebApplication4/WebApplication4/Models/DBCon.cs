using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebApplication4.Models
{
    public class DBCon
    {
        private readonly string ConnStr = "Data Source=B35F-Y1997NB\\SQLEXPRESS;Initial Catalog=StoredProcedures;Integrated Security=True";

        public List<Teacher> GetTeacher()
        {
            List<Teacher> listteacher = new List<Teacher>();
            SqlConnection sqlConnection = new SqlConnection(ConnStr);
            SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Teacher");
            sqlCommand.Connection = sqlConnection;
            sqlConnection.Open();

            SqlDataReader reader = sqlCommand.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Teacher teachers = new Teacher
                    {
                        TId = reader.GetString(reader.GetOrdinal("TId")),
                        Tname = reader.GetString(reader.GetOrdinal("Tname")),
 
                    };
                    listteacher.Add(teachers);
                }
            }
            else
            {
                Console.WriteLine("資料庫為空！");
            }
            sqlConnection.Close();
            return listteacher;
        }

        public void NewTeacher(Teacher teacher)
        {
            SqlConnection sqlConnection = new SqlConnection(ConnStr);
            SqlCommand sqlCommand = new SqlCommand(
                @"Insert Into Teacher(TId, TName) Values (@TId, @TName)"
                );
            sqlCommand.Connection = sqlConnection;
            sqlCommand.Parameters.Add(new SqlParameter("@TId",teacher.TId));
            sqlCommand.Parameters.Add(new SqlParameter("@TName", teacher.Tname));
            sqlConnection.Open();
            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
        }

        public Teacher GetTeacherByID(string id)
        {
            Teacher teacher = new Teacher();
            SqlConnection sqlConnection = new SqlConnection(ConnStr);
            SqlCommand sqlCommand = new SqlCommand(
                @"Select * from Teacher Where TId = @TId"
                );
            sqlCommand.Connection = sqlConnection;
            sqlCommand.Parameters.Add(new SqlParameter("@TId", id));

            sqlConnection.Open();
            SqlDataReader reader = sqlCommand.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    teacher = new Teacher
                    {
                        TId = reader.GetString(reader.GetOrdinal("TId")),
                        Tname = reader.GetString(reader.GetOrdinal("Tname")),
                    };
                }
            }
            else
            {
                teacher.Tname = "Nothing!!!";
            }
            sqlConnection.Close();
            return teacher;
        }
        public void UpdateTeacherByID(Teacher teacher)
        {
            SqlConnection sqlConnection = new SqlConnection(ConnStr);
            SqlCommand sqlCommand = new SqlCommand(
                @"Update Teacher Set Tname = @Tname Where TId = @TId"
                );
            sqlCommand.Connection = sqlConnection;
            sqlCommand.Parameters.Add(new SqlParameter("@TId", teacher.TId));
            sqlCommand.Parameters.Add(new SqlParameter("@Tname", teacher.Tname));
            sqlConnection.Open();
            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
        }
    }
}