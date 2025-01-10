using System;
using System.Collections.Generic;

namespace KitapYonetimSistemi
{
    class Program
    {
        static void Main(string[] args)
        {
            // Örnek veri oluşturulması
            KitapYonetimi kitapYonetimi = new KitapYonetimi();
            KullaniciYonetimi kullaniciYonetimi = new KullaniciYonetimi();

            kitapYonetimi.KitapEkle(new Kitap { Id = 1, Ad = "Sefiller", Yazar = "Viktor Hugo", Kategori = "Roman", Stok = 500000 });
            kullaniciYonetimi.KullaniciEkle(new Ogrenci { Id = 1, AdSoyad = "Ismayil Piriyev", OkulNo = "23104172" });

            while (true)
            {
                Console.WriteLine("\nKitap Yönetim Sistemi");
                Console.WriteLine("1. Kitap İşlemleri");
                Console.WriteLine("2. Kullanıcı İşlemleri");
                Console.WriteLine("3. Çıkış");
                Console.Write("Seçiminiz: ");
                int secim = int.Parse(Console.ReadLine());

                switch (secim)
                {
                    case 1:
                        kitapYonetimi.KitapMenu();
                        break;
                    case 2:
                        kullaniciYonetimi.KullaniciMenu();
                        break;
                    case 3:
                        Console.WriteLine("Çıkış yapılıyor...");
                        return;
                    default:
                        Console.WriteLine("Geçersiz seçim.");
                        break;
                }
            }
        }
    }

    public class Kitap
    {
        public int Id { get; set; }
        public string Ad { get; set; }
        public string Yazar { get; set; }
        public string Kategori { get; set; }
        public int Stok { get; set; }

        public void Yazdir()
        {
            Console.WriteLine($"ID: {Id}, Ad: {Ad}, Yazar: {Yazar}, Kategori: {Kategori}, Stok: {Stok}");
        }
    }

    public class KitapYonetimi
    {
        private List<Kitap> kitaplar = new List<Kitap>();

        public void KitapEkle(Kitap kitap)
        {
            kitaplar.Add(kitap);
            Console.WriteLine("Kitap başarıyla eklendi.");
        }

        public void KitapSil(int id)
        {
            Kitap kitap = kitaplar.Find(k => k.Id == id);
            if (kitap != null)
            {
                kitaplar.Remove(kitap);
                Console.WriteLine("Kitap silindi.");
            }
            else
            {
                Console.WriteLine("Kitap bulunamadı.");
            }
        }

        public void KitapListele()
        {
            foreach (var kitap in kitaplar)
            {
                kitap.Yazdir();
            }
        }

        public void KitapMenu()
        {
            Console.WriteLine("\nKitap İşlemleri");
            Console.WriteLine("1. Kitap Ekle");
            Console.WriteLine("2. Kitap Sil");
            Console.WriteLine("3. Kitap Listele");
            Console.Write("Seçiminiz: ");
            int secim = int.Parse(Console.ReadLine());

            switch (secim)
            {
                case 1:
                    Console.Write("Kitap Adı: ");
                    string ad = Console.ReadLine();
                    Console.Write("Yazar: ");
                    string yazar = Console.ReadLine();
                    Console.Write("Kategori: ");
                    string kategori = Console.ReadLine();
                    Console.Write("Stok: ");
                    int stok = int.Parse(Console.ReadLine());

                    Kitap yeniKitap = new Kitap
                    {
                        Id = kitaplar.Count + 1,
                        Ad = ad,
                        Yazar = yazar,
                        Kategori = kategori,
                        Stok = stok
                    };
                    KitapEkle(yeniKitap);
                    break;

                case 2:
                    Console.Write("Silmek istediğiniz kitabın ID'si: ");
                    int id = int.Parse(Console.ReadLine());
                    KitapSil(id);
                    break;

                case 3:
                    KitapListele();
                    break;

                default:
                    Console.WriteLine("Geçersiz seçim.");
                    break;
            }
        }
    }

    public abstract class Kullanici
    {
        public int Id { get; set; }
        public string AdSoyad { get; set; }

        public virtual void Yazdir()
        {
            Console.WriteLine($"ID: {Id}, AdSoyad: {AdSoyad}");
        }
    }

    public class Ogrenci : Kullanici
    {
        public string OkulNo { get; set; }

        public override void Yazdir()
        {
            base.Yazdir();
            Console.WriteLine($"Okul No: {OkulNo}");
        }
    }

    public class Personel : Kullanici
    {
        public string Departman { get; set; }

        public override void Yazdir()
        {
            base.Yazdir();
            Console.WriteLine($"Departman: {Departman}");
        }
    }

    public class KullaniciYonetimi
    {
        private List<Kullanici> kullanicilar = new List<Kullanici>();

        public void KullaniciEkle(Kullanici kullanici)
        {
            kullanicilar.Add(kullanici);
            Console.WriteLine("Kullanıcı başarıyla eklendi.");
        }

        public void KullaniciListele()
        {
            foreach (var kullanici in kullanicilar)
            {
                kullanici.Yazdir();
            }
        }

        public void KullaniciMenu()
        {
            Console.WriteLine("\nKullanıcı İşlemleri");
            Console.WriteLine("1. Kullanıcı Ekle");
            Console.WriteLine("2. Kullanıcı Listele");
            Console.Write("Seçiminiz: ");
            int secim = int.Parse(Console.ReadLine());

            switch (secim)
            {
                case 1:
                    Console.Write("Kullanıcı Adı: ");
                    string adSoyad = Console.ReadLine();
                    Console.WriteLine("Kullanıcı Türü (1- Öğrenci, 2- Personel): ");
                    int turSecim = int.Parse(Console.ReadLine());

                    if (turSecim == 1)
                    {
                        Console.Write("Okul No: ");
                        string okulNo = Console.ReadLine();
                        Kullanici ogrenci = new Ogrenci { Id = kullanicilar.Count + 1, AdSoyad = adSoyad, OkulNo = okulNo };
                        KullaniciEkle(ogrenci);
                    }
                    else if (turSecim == 2)
                    {
                        Console.Write("Departman: ");
                        string departman = Console.ReadLine();
                        Kullanici personel = new Personel { Id = kullanicilar.Count + 1, AdSoyad = adSoyad, Departman = departman };
                        KullaniciEkle(personel);
                    }
                    else
                    {
                        Console.WriteLine("Geçersiz tür seçimi.");
                    }
                    break;

                case 2:
                    KullaniciListele();
                    break;

                default:
                    Console.WriteLine("Geçersiz seçim.");
                    break;
            }
        }
    }
}