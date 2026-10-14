using System;
using System.Collections.Generic;

namespace SistemRumahSakit
{
    public class Orang
    {
        public string Nama { get; set; }
        public int Umur { get; set; }

        public Orang(string nama, int umur)
        {
            Nama = nama;
            Umur = umur;
        }

        public virtual void Aktivitas()
        {
            Console.WriteLine($"{Nama} sedang melakukan aktivitas");
        }

        public virtual void InfoOrang()
        {
            Console.WriteLine($"Nama  : {Nama}");
            Console.WriteLine($"Umur  : {Umur} tahun");
        }
    }

    public class Pasien : Orang
    {
        public string Keluhan { get; set; }

        public Pasien(string nama, int umur, string keluhan) : base(nama, umur)
        {
            Keluhan = keluhan;
        }

        public void CekKeluhan()
        {
            Console.WriteLine($"Keluhan {Nama}: {Keluhan}");
        }

        public override void Aktivitas()
        {
            Console.WriteLine($"Pasien {Nama} sedang tiduran di ruang perawatan");
        }

        public override void InfoOrang()
        {
            base.InfoOrang();
            Console.WriteLine($"Keluhan : {Keluhan}");
        }
    }

    public class TenagaMedis : Orang
    {
        public string Spesialisasi { get; set; }

        public TenagaMedis(string nama, int umur, string spesialisasi) : base(nama, umur)
        {
            Spesialisasi = spesialisasi;
        }

        public void CekSpesialis()
        {
            Console.WriteLine($"Spesialisasi {Nama}: {Spesialisasi}");
        }

        public override void Aktivitas()
        {
            Console.WriteLine($"Tendis {Nama} sedang bertugas di rumah sakit");
        }

        public override void InfoOrang()
        {
            base.InfoOrang();
            Console.WriteLine($"Spesialisasi : {Spesialisasi}");
        }
    }

    public class Dokter : TenagaMedis
    {
        public Dokter(string nama, int umur, string spesialisasi) : base(nama, umur, spesialisasi) { }

        public void Diagnosa()
        {
            Console.WriteLine($"Dr {Nama} sedang mendiagnosa pasien sesuai bidang {Spesialisasi}.");
        }

        public override void Aktivitas()
        {
            Console.WriteLine($"Dr. {Nama} sedang memeriksa dan mendiagnosa pasien.");
        }
    }

    public class Perawat : TenagaMedis
    {
        public Perawat(string nama, int umur, string spesialisasi) : base(nama, umur, spesialisasi) { }

        public void CekPasien()
        {
            Console.WriteLine($"Perawat {Nama} sedang mengecek kondisi pasien.");
        }

        public override void Aktivitas()
        {
            Console.WriteLine($"Perawat {Nama} sedang merawat dan memantau kondisi pasien.");
        }
    }

    public class PasienAnak : Pasien
    {
        public PasienAnak(string nama, int umur, string keluhan) : base(nama, umur, keluhan) { }

        public void Menangis()
        {
            Console.WriteLine($"{Nama} sedang menangis");
        }

        public override void Aktivitas()
        {
            Console.WriteLine($"Pasien anak {Nama} sedang bermain");
        }
    }

    public class PasienDewasa : Pasien
    {
        public PasienDewasa(string nama, int umur, string keluhan) : base(nama, umur, keluhan) { }

        public void Konsultasi()
        {
            Console.WriteLine($"{Nama} sedang berkonsultasi dengan dokter mengenai keluhan: {Keluhan}");
        }

        public override void Aktivitas()
        {
            Console.WriteLine($"Pasien dewasa {Nama} sedang ngantri untuk konsultasi");
        }
    }

    public class RumahSakit
    {
        public string NamaRS { get; set; }
        private List<Orang> daftarOrang = new List<Orang>();

        public RumahSakit(string namaRS)
        {
            NamaRS = namaRS;
        }

        public void TambahOrang(Orang orang)
        {
            daftarOrang.Add(orang);
            Console.WriteLine($"{orang.Nama} berhasil ditambahkan ke {NamaRS}.");
        }

        public void DaftarOrang()
        {
            Console.WriteLine($"\nDaftar Orang di {NamaRS}");
            int no = 1;
            foreach (Orang o in daftarOrang)
            {
                Console.WriteLine($"\n#{no++} [{o.GetType().Name}]");
                o.InfoOrang();
            }
        }

        public List<Orang> AmbilSemuaOrang()
        {
            return daftarOrang;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            RumahSakit rs = new RumahSakit("RS Ojok Loro Gaenak");

            Dokter dokter = new Dokter("Tirta", 40, "Penyakit Luar");
            Perawat perawat = new Perawat("Mariati", 28, "Perawat Umum");
            PasienAnak pasienAnak = new PasienAnak("Yuki", 7, "Demam tinggi");
            PasienDewasa pasienDewasa = new PasienDewasa("Wowok", 39, "Darah Tinggi");

            Console.WriteLine("Menambahkan Orang ke RS");
            rs.TambahOrang(dokter);
            rs.TambahOrang(perawat);
            rs.TambahOrang(pasienAnak);
            rs.TambahOrang(pasienDewasa);

            rs.DaftarOrang();

            Console.WriteLine("\nDemonstrasi Polymorphism (Aktivitas)");
            foreach (Orang o in rs.AmbilSemuaOrang())
            {
                o.Aktivitas();
            }

            Console.WriteLine("\nPanggil Method Khusus");
            foreach (Orang o in rs.AmbilSemuaOrang())
            {
                if (o is Dokter d)
                {
                    d.Diagnosa();
                    d.CekSpesialis();
                }
                else if (o is Perawat p)
                {
                    p.CekPasien();
                    p.CekSpesialis();
                }
                else if (o is PasienAnak pa)
                {
                    pa.Menangis();
                    pa.CekKeluhan();
                }
                else if (o is PasienDewasa pd)
                {
                    pd.Konsultasi();
                    pd.CekKeluhan();
                }
            }

            Console.WriteLine("\n\nBUAT KASUS DI SOAL");

            Console.WriteLine("\n--- Soal 1: Aktivitas() pada Dokter dan Pasien ---");
            dokter.Aktivitas();
            Pasien pasien = new Pasien("Dewi", 50, "Pusing");
            pasien.Aktivitas();

            Console.WriteLine("\n--- Soal 2: Diagnosa() pada Dokter ---");
            dokter.Diagnosa();

            Console.WriteLine("\n--- Soal 3: Info lengkap Dokter ---");
            dokter.InfoOrang();

            Console.WriteLine("\n--- Soal 4: Menangis() pada PasienAnak ---");
            pasienAnak.Menangis();

            Console.WriteLine("\n--- Soal 5: Orang berisi objek Perawat ---");
            Orang orangPerawat = new Perawat("Lia", 30, "Perawat Anak");
            orangPerawat.Aktivitas();
            ((Perawat)orangPerawat).CekPasien();
        }
    }
}