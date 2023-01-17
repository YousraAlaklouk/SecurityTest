using SecurityTest.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Data;

namespace SecurityTest.Controllers
{
    public class AccountController : Controller
    {
        string email = HomeController.email;

        // GET: EditProfile
        public ActionResult EditAccount()
        {
            return View();
        }
        public string value = "";

        [HttpPost]
        public ActionResult Account(Enroll e)
        {
            try
            {
                if (Request.HttpMethod == "POST")
                {
                    Enroll er = new Enroll();
                    using (SqlConnection con = new SqlConnection("Data Source=DESKTOP-UJH3HOQ\\SQLEXPRESS;Initial Catalog= SecurityS&Y;Integrated Security=True"))
                    {
                        using (SqlCommand cmd = new SqlCommand("SELECT Email,UserName , FullName,BirthDate, Gender,Password FROM Customer WHERE Email = @em ", con))
                        {
                            cmd.Parameters.AddWithValue("@em", HomeController.email);

                            con.Open();
                            SqlDataReader dr;

                            cmd.CommandType = CommandType.Text;
                            dr = cmd.ExecuteReader();
                            if (dr.Read())
                            {
                                //txtUserName.Text = dr["UserName"].ToString();
                                //txtFullName.Text = dr["FullName"].ToString();
                                //txtBirthDate.Text = dr["BirthDate"].ToString();

                                ViewData["result"] = cmd.ExecuteNonQuery();
                                con.Close();
                            }
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