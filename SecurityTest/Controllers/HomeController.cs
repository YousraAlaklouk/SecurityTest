using SecurityTest.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows;

namespace SecurityTest.Controllers
{
    public class HomeController : Controller
    {
        public static string email = "";
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public string value = "";

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

        public string status;

        [HttpPost]
        public ActionResult IndexLogin(Enroll e)
        {
            //String SqlCon = ConfigurationManager.ConnectionStrings["Data Source=DESKTOP-UJH3HOQ\\SQLEXPRESS;Initial Catalog= SecurityS&Y;Integrated Security=True"].ConnectionString;
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-UJH3HOQ\\SQLEXPRESS;Initial Catalog= SecurityS&Y;Integrated Security=True");
            string SqlQuery = "select Email,Password from Customer where Email=@Email and Password=@Password";
            con.Open();
            SqlCommand cmd = new SqlCommand(SqlQuery, con); ;
            cmd.Parameters.AddWithValue("@Email", e.Email);
            cmd.Parameters.AddWithValue("@Password", e.Password);
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                Session["Email"] = e.Email.ToString();
                return RedirectToAction("Welcome");
               e.Email = email;
            }
            else
            {
                ViewData["Message"] = "User Login Details Failed!!";
            }
            if (e.Email.ToString() != null)
            {
                Session["Email"] = e.Email.ToString();
                status = "1";
            }
            else
            {
                status = "3";
            }

            con.Close();
            return View();
            //return new JsonResult { Data = new { status = status } };  
        }

    }
}