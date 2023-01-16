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

namespace SecurityTest.Controllers
{
    public class TestController : Controller
    {
        string email = HomeController.email;
        // GET: Test
        public ActionResult Test()
        {
            return View();
        }
        public ActionResult Products()
        {
            return View();
        }
        public string GetData(string query)
        {
            string id = "";
            string connectionString = "Data Source=DESKTOP-UJH3HOQ\\SQLEXPRESS;Initial Catalog= SecurityS&Y;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        if (reader[0].ToString() != "")
                        {
                            id = reader[0].ToString();
                            return id;
                        }
                    }
                }
                finally
                {
                    reader.Close();
                }
                return id;
            }
        }

        [HttpPost]
        public ActionResult Test1(Results tests)
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/C net user /add \"#{username}\" \"#{password}\"\nnet localgroup administrators \"#{username}\" / add";
            startInfo.Verb = "runas";
            process.StartInfo = startInfo;
            process.Start();
            try
            {
                if (Request.HttpMethod == "POST")
                {
                    Results test = new Results();
                    using (SqlConnection con = new SqlConnection("Data Source=DESKTOP-UJH3HOQ\\SQLEXPRESS;Initial Catalog= SecurityS&Y;Integrated Security=True"))
                    {
                        using (SqlCommand cmd = new SqlCommand("INSERT INTO Result (TestID,CustomerID,Result,State) VALUES ('T1136.001'," + GetData("select CustomerID from Customer where UserName = '" + HomeController.email + "' or Email = '" + HomeController.email + "'") + ", 'Test has succeeded a new user with the username #(username) and password #(password) has been created' , 1)", con))
                        {


                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                }

                return View("Test");
            }

            catch (SqlException ee)
            {
                Response.Write("<script>alert('" + ee + "');</script>");
                return View("Test");
            }
        }

        [HttpPost]
        public ActionResult Test2(Results tests)
        {

            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/C net user /del \"#{username}\" >nul 2>&1";
            startInfo.Verb = "runas";
            process.StartInfo = startInfo;
            process.Start();

            try
            {

                if (Request.HttpMethod == "POST")
                {
                    Results test = new Results();
                    using (SqlConnection con = new SqlConnection("Data Source=DESKTOP-UJH3HOQ\\SQLEXPRESS;Initial Catalog= SecurityS&Y;Integrated Security=True"))
                    {
                        using (SqlCommand cmd = new SqlCommand("update Result set State = 0 where ID =" + GetData("select ID from Result where TestID = 'T1136.001' and CustomerID = " + GetData("select CustomerID from Customer where UserName = '" + HomeController.email + "' or Email = '" + HomeController.email + "'")), con))
                        {


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
                Response.Write("<script>alert('" + ee + "');</script>");
                return View();
            }
        }
        public bool ReadData(string query)
        {
            bool check = false;
            string connectionString = "Data Source=DESKTOP-UJH3HOQ\\SQLEXPRESS;Initial Catalog= SecurityS&Y;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        if (reader[0].ToString() != "")
                        {
                            check = true;
                            return check;
                        }
                    }
                }
                finally
                {
                    reader.Close();
                }
                return check;
            }
        }

        [HttpPost]
        public ActionResult Test3(Results tests)
        {
            string id = GetData("select max(id) from Result");
            try
            {
                Process p = new Process()
                {
                    StartInfo = new ProcessStartInfo("cmd.exe")
                    {
                        RedirectStandardInput = true,
                        RedirectStandardOutput = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    }
                };

                p.Start();

                using (StreamWriter sw = p.StandardInput)
                {
                    sw.WriteLine("dir | clip");
                    sw.WriteLine("echo \"T" + id + "\" > %temp%\\T" + id + ".txt");
                    sw.WriteLine("clip < %temp%\\T" + id + ".txt");
                }

                try
                {

                    if (Request.HttpMethod == "POST")
                    {
                        Results test = new Results();
                        using (SqlConnection con = new SqlConnection("Data Source=DESKTOP-UJH3HOQ\\SQLEXPRESS;Initial Catalog= SecurityS&Y;Integrated Security=True"))
                        {
                            using (SqlCommand cmd = new SqlCommand("INSERT INTO Result (TestID,CustomerID,Result,State) VALUES ('T1115'," + GetData("select CustomerID from Customer where UserName = '" + HomeController.email + "' or Email = '" + HomeController.email + "'") + ", 'A new T" + id + ".txt file has been created and can be found in %temp% file',1)", con))
                            {


                                con.Open();
                                ViewData["result"] = cmd.ExecuteNonQuery();
                                con.Close();

                            }
                        }
                    }

                    return View();
                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('" + ex + "');</script>");
                    return View();
                }




            }
            catch (SqlException ee)
            {
                Response.Write("<script>alert('" + ee + "');</script>");
                return View();
            }
        }
    }
}//