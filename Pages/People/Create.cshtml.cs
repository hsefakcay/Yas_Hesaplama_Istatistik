using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using WebDönemProjesi.Models;

namespace WebDönemProjesi.Pages.People
{
    public class CreateModel : PageModel
    {
       
        
        public PersonInfo? newPerson =new PersonInfo();

        public String successMessage = "";
        public String errorMessage = "";

        public const int MIN_USER_COUNT = 20;
        public const int MAX_USER_COUNT = 25;

        public void OnGet()
        {
        }
        public void OnPost()
        {
            newPerson.Isim = Request.Form["name"];
            newPerson.Soyad = Request.Form["surname"];   
            newPerson.Cinsiyet = Request.Form["gender"];
            newPerson.Fotograf = Request.Form["image"];
            // Doğum tarihini alınan değerden DateTime tipine dönüştürerek atandı
            if (DateTime.TryParse(Request.Form["birthDate"], out DateTime birthdate))
            {
                newPerson.DogumTarihi = birthdate;
            }



            if (newPerson.Isim.Length == 0 || newPerson.Soyad.Length == 0)
            {
                errorMessage = "Lütfen gerekli alanları giriniz";
                return;
            }

            if (newPerson.DogumTarihi >= DateTime.Today)
            {
                errorMessage = "Doğum tarihi bugünün tarihinden ileri olamaz.";
                return;
            }
            if (newPerson.DogumTarihi <= DateTime.Today.AddYears(-150))
            {
                errorMessage = "Doğum tarihi çok eski bir tarih olamaz.";
                return;
            }
            if (PersonModel.listPerson.Count >= MAX_USER_COUNT)
            {
                errorMessage = "En fazla 25 kişi ekleyebilirsiniz";
                return;

            }
            //eklenen lişinin database kaydedilmesi
            String connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=WebFinalProje;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                String sql = "INSERT INTO people " +
                            "(name, surname, age, gender, image, birthDay) VALUES" +
                            "(@name, @surname, @age, @gender, @image, @birthDay);";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@name", newPerson.Isim);
                    command.Parameters.AddWithValue("@surname", newPerson.Soyad);
                    command.Parameters.AddWithValue("@age", newPerson.Yas);
                    command.Parameters.AddWithValue("@gender", newPerson.Cinsiyet);
                    command.Parameters.AddWithValue("@image", newPerson.Fotograf);
                    command.Parameters.AddWithValue("@birthDay", newPerson.DogumTarihi);

                    command.ExecuteNonQuery();
                }

            }

            newPerson = new PersonInfo(); // Yeni giriş için alanları temizle
            successMessage = "Yeni kişi başarılı bir şekilde eklendi";

            Response.Redirect("/People");

        }
    }
}
