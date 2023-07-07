using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.SignalR;
using WebApplication1.Models;

using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using Microsoft.Extensions.Configuration;




namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public TeacherController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]

        public JsonResult Get()
        {
            string query = @"select TeacherId, TeacherName from Teacher";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("Myappcon");
            SqlDataReader myReader;
            using (SqlConnection mycon = new SqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (SqlCommand mycmd = new SqlCommand(query, mycon))
                {
                    myReader = mycmd.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult(table);

        }
        [HttpPost]

        public JsonResult Post(Teacher Tea)
        {
            string query = @"Insert into Teacher (TeacherName) Values(@TeacherName)";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("Myappcon");
            SqlDataReader myReader;
            using (SqlConnection mycon = new SqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (SqlCommand mycmd = new SqlCommand(query, mycon))
                {
                    mycmd.Parameters.AddWithValue("@TeacherName", Tea.TeacherName);
                    myReader = mycmd.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult("Added Successfully");

        }
        [HttpPut]
        public JsonResult Put(Teacher Tea)
        {
            string query = @"update Teacher
                             set TeacherName = @TeacherName
                             where TeacherId = @TeacherId";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("Myappcon");
            SqlDataReader myReader;
            using (SqlConnection mycon = new SqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (SqlCommand mycmd = new SqlCommand(query, mycon))
                {
                    mycmd.Parameters.AddWithValue("@TeacherId", Tea.TeacherId);
                    mycmd.Parameters.AddWithValue("@TeacherName", Tea.TeacherName);
                    myReader = mycmd.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult("Edited Successfully");

        }

        [HttpDelete("{Id}")]
        public JsonResult Delete(int Id)
        {
            string query = @"Delete from Teacher
                             where TeacherId = @TeacherId";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("Myappcon");
            SqlDataReader myReader;
            using (SqlConnection mycon = new SqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (SqlCommand mycmd = new SqlCommand(query, mycon))
                {
                    mycmd.Parameters.AddWithValue("@TeacherID", Id);
                    myReader = mycmd.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult("Delete Successfully");

        }
    }
}



