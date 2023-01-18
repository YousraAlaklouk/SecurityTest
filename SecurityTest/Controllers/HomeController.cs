using SecurityTest.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Helpers;
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
        public ActionResult Registered()
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
        [ActionName("Index")]
        [Obsolete]
        public ActionResult Index(Enroll en)
        {
            try
            {
                if (Request.HttpMethod == "POST")
                {
                    Enroll er = new Enroll();
                    using (SqlConnection con = new SqlConnection("Data Source=DESKTOP-CNJT2HB\\SQLEXPRESS;Initial Catalog= SecurityS&Y;Integrated Security=True"))
                    {
                        using (SqlCommand cmd = new SqlCommand("INSERT INTO Customer (Username,Email ,Fullname, BirthDate, Gender,Password) VALUES (@Username,@Email,@Fullname, @BirthDate,@Gender,@Password)", con))
                        {
                            cmd.Parameters.AddWithValue("@FullName", en.FullName);
                            cmd.Parameters.AddWithValue("@Username", en.UserName);
                            cmd.Parameters.AddWithValue("@Password", en.Password);
                            cmd.Parameters.AddWithValue("@Gender", en.Gender);
                            cmd.Parameters.AddWithValue("@Email", en.Email);
                            cmd.Parameters.AddWithValue("@BirthDate", en.BirthDate);
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

        [HttpPost]
        [ActionName("IndexLogin")]
        public ActionResult IndexLogin(Enroll e)
        {
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-CNJT2HB\\SQLEXPRESS;Initial Catalog= SecurityS&Y;Integrated Security=True");
            string SqlQuery = "select Email,Password from Customer where Email=@Email and Password=@Password";
            con.Open();
            SqlCommand cmd = new SqlCommand(SqlQuery, con); ;
            cmd.Parameters.AddWithValue("@Email", e.Email);
            cmd.Parameters.AddWithValue("@Password", e.Password);
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                ViewBag.message = "Login Successfull!";
                Session["Email"] = e.Email.ToString();
                return View("Index");
               e.Email = email;
            }
            else
            {
                ViewBag.message = "Login Failed!";
                ViewData["Message"] = "User Login Details Failed!!";
            }

            con.Close();
            return View("Index");
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }

    }
}