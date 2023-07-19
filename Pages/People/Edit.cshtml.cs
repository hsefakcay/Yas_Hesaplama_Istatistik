using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using WebDönemProjesi.Models;

namespace WebDönemProjesi.Pages.People
{
    public class EditModel : PageModel
    {
        public PersonInfo personInfo = new PersonInfo();
        public String errorMessage = "";
        public String successMessage = "";

        public void OnGet()
        {
            String id = Request.Query["id"];

            String connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=WebFinalProje;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                String sql = "SELECT * FROM people WHERE id=@id";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("id", id);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            personInfo.Id = "" + reader.GetInt32(0);
                            personInfo.Isim = reader.GetString(1);
                            personInfo.Soyad = reader.GetString(2);
                            personInfo.Yas = reader.GetString(3);
                            personInfo.Cinsiyet = reader.GetString(4);
                            personInfo.Fotograf = reader.ToString();
                            personInfo.DogumTarihi = reader.GetDateTime(6);
                        }
                    }
                }
            }
        }
        public void OnPost()
        {
            personInfo.Id = Request.Form["id"];
            personInfo.Isim = Request.Form["name"];
            personInfo.Soyad = Request.Form["surname"];
            personInfo.Cinsiyet = Request.Form["gender"];
            personInfo.Fotograf = Request.Form["image"];
            // Doğum tarihini alınan değerden DateTime tipine dönüştürerek atandı
            if (DateTime.TryParse(Request.Form["birthDate"], out DateTime birthdate))
            {
                personInfo.DogumTarihi = birthdate;
            }
            if (personInfo.Isim.Length == 0 || personInfo.Soyad.Length == 0 )
            {
                errorMessage = "Lütfen gerekli alanları giriniz";
                return;
            }
            String connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=WebFinalProje;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                String sql = "UPDATE people " +
                            "SET name=@name, surname=@surname, age=@age, gender=@gender, image=@image, birthDay=@birthDay " +
                            "WHERE id=@id";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@name", personInfo.Isim);
                    command.Parameters.AddWithValue("@surname", personInfo.Soyad);
                    command.Parameters.AddWithValue("@age", personInfo.Yas);
                    command.Parameters.AddWithValue("@gender", personInfo.Cinsiyet);
                    command.Parameters.AddWithValue("@image", personInfo.Fotograf);
                    command.Parameters.AddWithValue("@birthDay", personInfo.DogumTarihi);
                    command.Parameters.AddWithValue("@id", personInfo.Id);

                    command.ExecuteNonQuery();
                }
            }
            Response.Redirect("/People/Index");
        }
    }
}

