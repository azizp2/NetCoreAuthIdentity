# NetCoreAuthIdentity

![.NET 7](https://img.shields.io/badge/.NET-7.0-blue)
![License](https://img.shields.io/github/license/azizp2/NetCoreAuthIdentity)

**NetCoreAuthIdentity** adalah sebuah Web API berbasis .NET 7 yang menyediakan fitur autentikasi menggunakan **ASP.NET Core Identity** dan kemampuan export data ke file PDF menggunakan **QuestPDF**.

---

## 🚀 Fitur Utama

- **Authentication & Authorization**  
  Implementasi autentikasi user lengkap dengan manajemen user, role, dan policy menggunakan ASP.NET Core Identity.  
  Mendukung pendaftaran user, login, logout, serta proteksi endpoint API.

- **Export Data ke PDF dengan QuestPDF**  
  API mampu menghasilkan file PDF secara dinamis menggunakan library [QuestPDF](https://www.questpdf.com/), cocok untuk laporan, invoice, dan dokumen lain.

- **Solid Principal & Modular**  
  Struktur proyek yang terpisah ke dalam lapisan seperti Controllers, Services, Repository, dan Data untuk memudahkan pengembangan dan pemeliharaan.

---

## 🛠️ Teknologi yang Digunakan

- **.NET 7** dengan ASP.NET Core Web API  
- **ASP.NET Core Identity** untuk autentikasi dan manajemen user  
- **Entity Framework Core** dengan migrasi database  
- **QuestPDF** untuk pembuatan PDF yang fleksibel dan mudah dikustomisasi  
- **AutoMapper** (atau mapper lain) untuk mapping DTO dan entity  
- **RESTful API** sesuai standar terbaik

---

## 📦 Cara Menjalankan Proyek

1. Clone repository:

```bash
git clone https://github.com/azizp2/NetCoreAuthIdentity.git
cd NetCoreAuthIdentity
