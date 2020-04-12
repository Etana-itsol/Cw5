using Cw5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Cw5.DTOs.Requests;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Cw5.DTOs.Responses;
using System.Globalization;

namespace Cw5.Services
{
    public class SqlServerStudentDbServices : IStudentDbServices
    {
        public Enrollment EnrollStudent(EnrollStudentRequest request)
        {
            using (var con = new SqlConnection("Data Source=db-mssql;Initial Catalog=s18725;Integrated Security=True"))
            using (var com = new SqlCommand())
            {
                com.Connection = con;
                con.Open();
                var tran = con.BeginTransaction();
                try
                {
                    com.CommandText = "Exec EnrollStudent @IndexNumber, @FirstName, @LastName, @BirthDate, @Studies";
                    //DateTime date = DateTime.ParseExact(request.Birthdate, "dd.mm.yyyy", CultureInfo.InvariantCulture);
                    //DateTime date = DateTime.ToDate("dd.mm.yyyy");
                    //DateTime d = request.Birthdate.GetDateTimeFormats; 
                    //var test2 = System.Text.Json.JsonSerializer.Deserialize<Test>(request.Birthdate);
                    //Student st = new Student();
                    //st.Birthdate = DateTime.Parse(request.Birthdate);
                    com.Parameters.AddWithValue("IndexNumber", request.IndexNumber);
                    com.Parameters.AddWithValue("FirstName", request.FirstName);
                    com.Parameters.AddWithValue("LastName", request.LastName);
                    com.Parameters.AddWithValue("BirthDate", request.Birthdate);
                    com.Parameters.AddWithValue("Studies", request.Studies);
                }
                catch 
                {
                    tran.Rollback();
                }
                tran.Commit();
                com.ExecuteNonQuery();

            }
            return new Enrollment() { Studies = request.Studies, Semester = 1, StartDate = DateTime.Today };
        }

        public PromoteStudentRequest PromoteStudents(PromoteStudentRequest request)
        {
            using (var con = new SqlConnection("Data Source=db-mssql;Initial Catalog=s18725;Integrated Security=True"))
            using (var com = new SqlCommand("Execute PromoteStudents @name, @semester;", con))
            {
                Console.WriteLine("open");
                con.Open();

                var tran = con.BeginTransaction();
                try 
                {
                    Console.WriteLine("try");
            
                    com.Parameters.AddWithValue("name", request.Studies);
                    com.Parameters.AddWithValue("semester", request.Semester);

                    tran.Commit();
                    com.ExecuteNonQuery();
                }
                catch (SqlException exc)
                {
                    tran.Rollback();
                    Console.WriteLine("err");         
                }
            }
           return request;
        }
    }
}
