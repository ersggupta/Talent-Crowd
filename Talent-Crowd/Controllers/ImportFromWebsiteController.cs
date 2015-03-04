using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySql.Data.MySqlClient;
using System.Text;
using Talent_Crowd.Models;
using System.Data.Entity.Core;
using System.Data.Entity.Validation;
using System.Data.Entity.Infrastructure;

namespace Talent_Crowd.Controllers
{
    public class ImportFromWebsiteController : Controller
    {
        // GET: ImportFromWebsite
        MyDbContext db = new MyDbContext();
        MySqlConnection mySqlConnection = new MySqlConnection();

        public void Process()
        {
            List<decimal> submit_times = new List<decimal>();
            using (MySqlCommand cmd = new MySqlCommand("select distinct submit_time from wp_cf7dbplugin_submits where form_name = 'Worker form'  and (migrated is null or migrated = '');", mySqlConnection))
            {
                using (MySqlDataReader readerlist = cmd.ExecuteReader())
                {
                    while (readerlist.Read())
                    {
                        submit_times.Add(readerlist.GetDecimal("submit_time"));
                    }
                }
            }

            int cuenta = submit_times.Count;
            for (int item = 0; item < cuenta ; item++) {
                WholeRecord(submit_times[item]);
            }
        }

        public void WholeRecord(decimal submit_time)
        {
            using (MySqlCommand cmd = new MySqlCommand("select submit_time, field_name, field_value from wp_cf7dbplugin_submits where submit_time = @submit_time order by field_order", mySqlConnection))
            {
                cmd.Parameters.AddWithValue("@submit_time", submit_time);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {

                    Worker worker = new Worker();
                    while (reader.Read())
                    {                            
                        string columnname = reader.GetString("field_name").ToString();
                            
                        switch (columnname)
                        {
                            case "your-name":
                                worker.First_Name= reader.GetString("field_value").ToString();
                                break;
                            case "EmployeeorFreelancer":
                                worker.EmployeeFreelancer = reader.GetString("field_value").ToString();
                                break;
                            case "company":
                                worker.Company = reader.GetString("field_value").ToString();
                                break;
                            case "job-title":
                                worker.JobTitle = reader.GetString("field_value").ToString();
                                break;
                            case "your-email":
                                worker.Email = reader.GetString("field_value").ToString();
                                break;
                            case "linkedin":
                                worker.LinkedIn_Public_URL = reader.GetString("field_value").ToString();
                                break;
                            case "twitter":
                                worker.Twitter = reader.GetString("field_value").ToString();
                                break;
                            case "hourly-rate3":
                                worker.HourlyRate = reader.GetDecimal("field_value");
                                break;
                            case "hours-available":
                                worker.AvailableHoursperWeek = Convert.ToInt32(reader.GetDecimal("field_value"));
                                break;
                            case "native-language":
                                worker.NativeLanguage = reader.GetString("field_value");
                                break;
                            case "spoken-english":
                                worker.SpokenEnglishLevel = reader.GetString("field_value");
                                break;
                            case "area-of-expertise":
                                worker.AreaofExpertise = reader.GetString("field_value");
                                break;
                            case "product-access":
                                worker.ProductAccess = reader.GetString("field_value");
                                break;
                            case "other-products":
                                worker.OtherProducts = reader.GetString("field_value");
                                break;
                            case "interest":
                                worker.Interest = reader.GetString("field_value");
                                break;
                            case "Submitted Login":
                            case "Submitted From":                                 
                            default:
                                break;
                        }  // switch
                    } //while


                    //db.Workers.Add(new Worker() {First_Name = "Jorge"});
                    db.Workers.Add(worker);

                    User user = new User();
                    user.UserName = worker.Email;
                    user.Password = System.Web.Security.Membership.GeneratePassword(10, 5);

                    db.Users.Add(user);

                    try
                    {
                        db.SaveChanges();
                    }
                    catch (DbEntityValidationException ex)
                    {
                        foreach (DbEntityValidationResult item in ex.EntityValidationErrors)
                        {
                            // Get entry

                            DbEntityEntry entry = item.Entry;
                            string entityTypeName = entry.Entity.GetType().Name;

                            // Display or log error messages

                            foreach (DbValidationError subItem in item.ValidationErrors)
                            {
                                string message = string.Format("Error '{0}' occurred in {1} at {2}", subItem.ErrorMessage, entityTypeName, subItem.PropertyName);
                                throw new InvalidOperationException(message, ex);
                            }
                        }
                    }
     
                }//using MySQLDataReader
            }//using MySqlCommand 

            MySqlCommand cmd2 = new MySqlCommand();
            cmd2.Connection = mySqlConnection;
            cmd2.CommandText = "UPDATE wp_cf7dbplugin_submits SET migrated = 'Y' WHERE submit_time = @submit_time";
            cmd2.Parameters.AddWithValue("@submit_time", submit_time);
            int numRowsUpdated = cmd2.ExecuteNonQuery();
            cmd2.Dispose(); 

        }

        public void Index()
        {
            
            mySqlConnection.ConnectionString = "Server=188.165.254.37; User Id=wordpress_43; Password=Tgh1M2cR!9; Database=wordpress_fc";

            try
            {
                   mySqlConnection.Open();

                   switch (mySqlConnection.State)
                   {
                          case System.Data.ConnectionState.Open:
                                  // Connection has been made
                                  break;
                          case System.Data.ConnectionState.Closed:
                                  // Connection could not be made, throw an error
                                  throw new Exception("The database connection state is Closed");
                                  break;
                          default:
                                  // Connection is actively doing something else
                                  break;
                   }

                   // Place Your Code Here to Process Data //
                  Process();
            }
            catch (MySqlException mySqlException)
            {
                   // Use the mySqlException object to handle specific MySql errors
                throw new InvalidOperationException("Exception", mySqlException);
            }
            catch (Exception exception)
            {
                   // Use the exception object to handle all other non-MySql specific errors
                throw new InvalidOperationException("Exception", exception);
            }
            finally
            {
                   // Make sure to only close connections that are not in a closed state
                   if (mySqlConnection.State != System.Data.ConnectionState.Closed)
                   {
                          // Close the connection as a good Garbage Collecting practice
                          mySqlConnection.Close();
                   }
            }

        }
    }
}