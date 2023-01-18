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
using System.Configuration;

namespace SecurityTest.Controllers
{
    public class TestController : Controller
    {
        string email = HomeController.email;
        // GET: Test

        public ActionResult Index()
        {
            List<Results> FriendList = new List<Results>();
            using (IDbConnection db = new SqlConnection("Data Source=DESKTOP-CNJT2HB\\SQLEXPRESS;Initial Catalog= SecurityS&Y;Integrated Security=True"))
            {

                FriendList = db.Query<Results>("Select * From Result R inner join Customer C on C.CustomerID = R.CustomerID where C.Email = '"+Session["Email"].ToString()+"'").ToList();
                
            }
            return View(FriendList);
        }

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
            string connectionString = "Data Source=DESKTOP-CNJT2HB\\SQLEXPRESS;Initial Catalog= SecurityS&Y;Integrated Security=True";
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
                    using (SqlConnection con = new SqlConnection("Data Source=DESKTOP-CNJT2HB\\SQLEXPRESS;Initial Catalog= SecurityS&Y;Integrated Security=True"))
                    {
                        using (SqlCommand cmd = new SqlCommand("INSERT INTO Result (TestID,CustomerID,Result,State) VALUES ('T1136.001'," + GetData("select CustomerID from Customer where Email = '" + Session["Email"].ToString() + "'")+ ", 'Test has succeeded a new user with the username #(username) and password #(password) has been created' , 1)", con))
                        {


                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                }

                return RedirectToAction("Index");
            }

            catch (SqlException ee)
            {
                Response.Write("<script>alert('" + ee + "');</script>");
                return RedirectToAction("Index");
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
                    using (SqlConnection con = new SqlConnection("Data Source=DESKTOP-CNJT2HB\\SQLEXPRESS;Initial Catalog= SecurityS&Y;Integrated Security=True"))
                    {
                        using (SqlCommand cmd = new SqlCommand("update Result set State = 0 where ID =" + GetData("select ID from Result where TestID = 'T1136.001' and CustomerID = " + GetData("select CustomerID from Customer where Email = '" + Session["Email"].ToString() + "'")), con))
                        {
                            con.Open();
                            ViewData["result"] = cmd.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                }
                

                return RedirectToAction("Index");
            }




            catch (SqlException ee)
            {
                Response.Write("<script>alert('" + ee + "');</script>");
                return RedirectToAction("Index");
            }
        }
        public bool ReadData(string query)
        {
            bool check = false;
            string connectionString = "Data Source=DESKTOP-CNJT2HB\\SQLEXPRESS;Initial Catalog= SecurityS&Y;Integrated Security=True";
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
                        using (SqlConnection con = new SqlConnection("Data Source=DESKTOP-CNJT2HB\\SQLEXPRESS;Initial Catalog= SecurityS&Y;Integrated Security=True"))
                        {
                            using (SqlCommand cmd = new SqlCommand("INSERT INTO Result (TestID,CustomerID,Result,State) VALUES ('T1115'," + GetData("select CustomerID from Customer where Email = '" + Session["Email"].ToString() + "'") + ", 'A new T" + id + ".txt file has been created and can be found in %temp% file',1)", con))
                            {


                                con.Open();
                                ViewData["result"] = cmd.ExecuteNonQuery();
                                con.Close();

                            }
                        }
                    }

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('" + ex + "');</script>");
                    return RedirectToAction("Index");
                }




            }
            catch (SqlException ee)
            {
                Response.Write("<script>alert('" + ee + "');</script>");
                return RedirectToAction("Index");
            }
        }
        /*        public ActionResult Welcome(Results r)
                {
                    Results result = new Results();
                    DataSet ds = new DataSet();

            using (SqlConnection con = new SqlConnection("Data Source=PRIYANKA\\SQLEXPRESS;Integrated Security=true;Initial Catalog=Sample"))
            {
                using (SqlCommand cmd = new SqlCommand("select * from customer where email = @Email", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Email", SqlDbType.VarChar, 30).Value = Session["Email"].ToString();
                    con.Open();
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    sda.Fill(ds);
                    List<Enroll> userlist = new List<Enroll>();
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        Enroll uobj = new Enroll();
                        uobj.ID = Convert.ToInt32(ds.Tables[0].Rows[i]["ID"].ToString());
                        uobj.FullName = ds.Tables[0].Rows[i]["FullName"].ToString();
                        uobj.Password = ds.Tables[0].Rows[i]["Password"].ToString();
                        uobj.Email = ds.Tables[0].Rows[i]["Email"].ToString();
                        uobj.Gender = ds.Tables[0].Rows[i]["Gender"].ToString();

                        userlist.Add(uobj);

                    }
                    result.Enrollsinfo = userlist;
                }
                con.Close();

            }
            return View(user);
        }
*/
    }
}//