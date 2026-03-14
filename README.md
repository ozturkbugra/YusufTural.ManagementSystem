# YusufTural.ManagementSystem

Bu proje, çocukluk arkadaşım Yusuf Tural’ın tanker su ve lojistik işletmesi için geliştirdiğim kurumsal yönetim sistemidir.  
Mevcut web sitesini daha modern, yönetilebilir ve dinamik bir yapıya dönüştürmek amacıyla geliştirilmiştir.

## 🚀 Mimari Tercih

Paralel olarak geliştirdiğim Mini ERP projesinde **Onion Architecture** ve **CQRS** gibi daha kompleks mimariler kullanıyorum.  
Ancak bu projede **N-Tier (3 Katmanlı) Mimari** tercih ettim. 

Bunun temel sebepleri:

- **Hızlı geliştirme:** Projenin kısa sürede yayına alınması gerekiyordu.
- **Pratiklik:** Küçük ve orta ölçekli projeler için anlaşılır ve yönetilebilir bir yapı.
- **KISS prensibi:** Gereksiz karmaşıklıktan kaçınarak ihtiyaca uygun çözüm üretmek.

## 🛠️ Teknik Altyapı

- **Framework:** .NET 8.0 (ASP.NET Core MVC)
- **ORM:** Entity Framework Core
- **Database:** SQL Server
- **Frontend:** Bootstrap 5 & Dewi Template (customized)

## 🌟 Özellikler

- **Dinamik İçerik Yönetimi** Hizmetler, S.S.S. ve referanslar panel üzerinden güncellenebilir.

- **Ziyaretçi Takibi** Cookie tabanlı benzersiz ziyaretçi sayacı.

- **Script Yönetimi** Tag Manager, Pixel vb. scriptlerin panelden eklenebilmesi.

- **SEO Yönetimi** Sayfa bazlı meta tag yönetimi ve SEO dostu URL yapısı.

## 🏗️ Katman Yapısı

```text
YusufTural.ManagementSystem
│
├── WebUI        → Kullanıcı arayüzü ve admin paneli
├── Business     → İş mantığı ve servisler
├── DataAccess   → Veritabanı işlemleri
└── Entities     → Veritabanı modelleri
