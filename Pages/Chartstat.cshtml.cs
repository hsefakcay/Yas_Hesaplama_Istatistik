using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using WebDönemProjesi.Models;

namespace WebDönemProjesi.Pages.Shared
{
    public class ChartstatcshtmlModel : PageModel
    {
        public List<PersonInfo> listPeople = new List<PersonInfo>();

        //PersonInfo personInfo = new PersonInfo();
        public PersonInfo person { get; set; }

        public void OnGet()
        {
            String connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=WebFinalProje;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                String sql = "SELECT * FROM people";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            PersonInfo personInfo = new PersonInfo();
                            personInfo.Id = "" + reader.GetInt32(0);
                            personInfo.Isim = reader.GetString(1);
                            personInfo.Soyad = reader.GetString(2);
                            personInfo.Yas = reader.GetString(3);
                            personInfo.Cinsiyet = reader.GetString(4);
                            personInfo.Fotograf = reader.ToString();
                            personInfo.DogumTarihi = reader.GetDateTime(6);

                            listPeople.Add(personInfo);
                        }
                    }
                }
            }
        }
    }
}
