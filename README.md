# E-Commerce Backend (Task 2)

Bu proje, Onion Architecture prensipleri kullanılarak geliştirilmiş, ölçeklenebilir bir E-Ticaret backend servisidir.

## 🚀 Kullanılan Teknolojiler
* **.NET 7** (Web API)
* **PostgreSQL** (Veritabanı)
* **Entity Framework Core** (ORM)
* **Redis** (Distributed Caching)
* **MediatR** (CQRS Pattern)
* **JWT** (Authentication)
* **AutoMapper** (Mapping)
* **Docker** (Redis servisi için)

## 🛠 Kurulum ve Çalıştırma

1.  **Projeyi Klonlayın:**
    ```bash
    git clone https://github.com/balcireal/Task2_ECommerce.git
    cd Task2_ECommerce
    ```

2.  **Redis'i Ayağa Kaldırın (Docker):**
    ```bash
    docker run -d -p 6379:6379 --name redis_db redis
    ```

3.  **Veritabanı Ayarı:**
    `API/appsettings.json` dosyasındaki Connection String'i kendi PostgreSQL bilgilerinizle güncelleyin.

4.  **Veritabanını Oluşturun:**
    ```bash
    dotnet ef database update --project ECommerceTask.Infrastructure --startup-project ECommerceTask.API
    ```

5.  **Uygulamayı Başlatın:**
    ```bash
    dotnet run --project ECommerceTask.API
    ```

## 🧪 Test (Swagger)
Uygulama çalıştığında `https://localhost:xxxx/swagger` adresinden API dokümantasyonuna erişebilirsiniz.