using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using WebDönemProjesi.Models;

namespace WebDönemProjesi.Pages
{
    public class PersonModel : PageModel
    {
        public static List<PersonInfo> listPerson = new List<PersonInfo>();

        public void OnGet()
        {
            
                string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM UserProfiles ";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                PersonInfo person = new PersonInfo();
                                person.Id = reader.GetString(0);
                                person.Isim = "" + reader.GetString(1);
                                person.Soyad = reader.GetString(2);
                                person.DogumTarihi = reader.GetDateTime(3);
                                person.Cinsiyet = reader.GetString(4);
                                person.Fotograf = reader.GetString(5);

                                listPerson.Add(person);

                            }
                        }
                    }
                }

            
            
        }
    }


}

