using System.ComponentModel.DataAnnotations;

namespace WebDönemProjesi.Models
{
    public class PersonInfo
    {
        public string? Id { get; set; }

        [Required(ErrorMessage = "İsim alanı boş geçilemez")]
        public string? Isim { get; set; }

        [Required(ErrorMessage = "Soyad alanı boş geçilemez")]
        public string? Soyad { get; set; }

        [Required(ErrorMessage = "Doğum tarihi boş geçilemez")]
        public DateTime DogumTarihi { get; set; }

        public string? Cinsiyet { get; set; }

        public enum Cinsiyetler
        {
            Erkek,
            Kadın
        }

        public string? Fotograf { get; set; }

        public DateTime simdikiTarih = DateTime.Now;
        public string Yas
        {
            get
            {
                int yas = simdikiTarih.Year - DogumTarihi.Year;
                if (simdikiTarih.Month < DogumTarihi.Month || simdikiTarih.Month == DogumTarihi.Month && simdikiTarih.Day < DogumTarihi.Day)
                {
                    yas--;
                }
                return yas.ToString();
            }
            set
            {

            }
        }
    }
}