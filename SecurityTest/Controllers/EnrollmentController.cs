using SecurityTest.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Windows;

namespace SecurityTest.Controllers
{
    public class EnrollmentController : Controller
    {
        public string value = "";

        [HttpGet]
        // GET: Enrollment
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Enroll e)
        {
            try
            {
                if (Request.HttpMethod == "POST")
                {
                    Enroll er = new Enroll();
                    using (SqlConnection con = new SqlConnection("Data Source=DESKTOP-UJH3HOQ\\SQLEXPRESS;Initial Catalog= SecurityS&Y;Integrated Security=True"))
                    {
                        using (SqlCommand cmd = new SqlCommand("INSERT INTO Customer (Username,Email ,Fullname, BirthDate, Gender,Password) VALUES (@Username,@Email,@Fullname, @BirthDate,@Gender,@Password)", con))
                        {
                            cmd.Parameters.AddWithValue("@FullName", e.FullName);
                            cmd.Parameters.AddWithValue("@Username", e.UserName);
                            cmd.Parameters.AddWithValue("@Password", e.Password);
                            cmd.Parameters.AddWithValue("@Gender", e.Gender);
                            cmd.Parameters.AddWithValue("@Email", e.Email);
                            cmd.Parameters.AddWithValue("@BirthDate", e.BirthDate);
                            con.Open();
                            ViewData["result"] = cmd.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                }
                return View();
            }
            catch (SqlException ee)
            {
                MessageBox.Show(ee.Message);
                return View();
            }
            
        }
    }
}