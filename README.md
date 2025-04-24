# Setur Telefon Rehberi Değerlendirme Uygulaması

Bu proje, .NET 9 ile geliştirilmiş, Kafka destekli ve PostgreSQL kullanan bir mikroservis mimarisine sahip telefon rehberi uygulamasıdır.

## 📦 Proje Yapısı

- **PersonService**: Kişi ve iletişim bilgileri yönetimi (CRUD API)
- **ReportService**: Lokasyon bazlı rapor üretimi (Kafka üzerinden asenkron)
- **Application**: DTO'lar, servis arayüzleri ve business
- **Domain**: Entityler ve arayüzler
- **Infrastructure**: Entity Framework Core ve veritabanı connection bilgileri
- **Tests**: Unit testler (xUnit)

---

## 🚀 Kurulum ve Çalıştırma

### Gereksinimler

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [Docker Desktop](https://www.docker.com/products/docker-desktop)

---



---

## 🌐 API Dökümantasyonu

Swagger arayüzleri:

- http://localhost:5000/swagger – PersonService
- http://localhost:5001/swagger – ReportService

---

## 🧪 Unit Test Çalıştırma
- dotnet test


---

## 🛠️ Özellikler

- Kişi oluşturma, silme, listeleme
- Kişiye iletişim bilgisi ekleme ve silme
- Kafka ile mesajlaşma üzerinden rapor üretimi
- PostgreSQL veritabanı yönetimi
- EF Core migration desteği
- xUnit test altyapısı

---

## 📎 Notlar

- Rapor talepleri Kafka üzerinden asenkron şekilde işlenir.
- ReportWorker background service olarak sürekli Kafka'dan dinleme yapar.
- Migration klasörü Infrastructure projesi altındadır.


