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
using Dapper;

namespace SecurityTest.Controllers
{
    public class AccountController : Controller
    {
        string email = HomeController.email;
        SecurityEntities db;
        // GET: EditProfile
        public ActionResult EditAccount()
        {
            db = new SecurityEntities();
            List<Customer> customers = db.Customers.ToList();

            return View(customers.Find(p => p.Email == Session["Email"].ToString()));
        }
        public ActionResult UpdateAccount(Enroll e)
        {
            try
            {
                if (Request.HttpMethod == "POST")
                {
                    Enroll er = new Enroll();
                    using (SqlConnection con = new SqlConnection("Data Source=DESKTOP-CNJT2HB\\SQLEXPRESS;Initial Catalog= SecurityS&Y;Integrated Security=True"))
                    {
                        using (SqlCommand cmd = new SqlCommand("UPDATE Customer SET FullName  = '" + e.FullName.Trim() + "', BirthDate ='" + e.BirthDate.Trim() + "', Gender = @gender WHERE Email = @email ", con))
                        {
                            cmd.Parameters.AddWithValue("@em", HomeController.email);
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

public string value = "";

  /*      [HttpPost]
        public ActionResult Account(Enroll e)
        {
*//*            string female = "Female";
            string male = "Male";*//*
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
                                EditAccount.txtname.text = dr["Email"].ToString();
                                e.UserName = dr["UserName"].ToString();

                                e.FullName = dr["FullName"].ToString();
                                e.BirthDate = dr["BirthDate"].ToString();
                                e.Password = dr["Password"].ToString();

                                con.Close();



                                *//*
                                                            if (dr["Gender"].ToString().Trim() == female)
                                                            {
                                                                    List<SelectListItem> items = PopulateFruits();
                                                                    var selectedItem = items.Find(p => p.Value == fruit);
                                                                    if (selectedItem != null)
                                                                    {
                                                                        selectedItem.Selected = true;
                                                                    }
                                                            else if (dr["Gender"].ToString().Trim() == male)
                                                            {
                                                                Male.Checked = true;
                                                                Female.Checked = false;

                                                                NotToSay.Checked = false;
                                                            }
                                                            else if (dr["Gender"].ToString().Trim() == "Rether Not To Say")
                                                            {
                                                                Female.Checked = false;
                                                                Male.Checked = false;
                                                                NotToSay.Checked = true;

                                                            }*//*

                                Response.Write("<script>alert('sucsseful ');</script>");

                            }
                            else
                            {
                                Response.Write("<script>alert('couldnt load data');</script>");

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
        }*/


    }
}