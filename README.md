# 🚀 E-Commerce Microservices Case Study

Bu proje; **.NET 7**, **Docker**, **RabbitMQ**, **Redis** ve **Seq** teknolojileri kullanılarak geliştirilmiş, **Onion Architecture** ve **CQRS** desenlerini uygulayan modern bir mikroservis mimarisi çözümüdür.

Proje, **12 Faktör Uygulama (12-Factor App)** prensiplerine uygun olarak tasarlanmış ve tam konteynerizasyon (Dockerization) sağlanmıştır.

---

## 🏗️ Mimari ve Teknolojiler

Proje, bağımsız çalışabilen ancak birbiriyle haberleşen 4 ana servisten oluşur:

| Servis | Teknoloji / Kütüphane | Görevi |
| :--- | :--- | :--- |
| **API Gateway** | YARP (Reverse Proxy) | Tüm dış istekleri karşılar ve ilgili mikroservise yönlendirir. |
| **Product Service** | .NET 7, PostgreSQL, Redis | Ürün ekleme/listeleme işlemlerini yönetir. CQRS kullanır. |
| **Auth Service** | .NET 7, JWT | Kimlik doğrulama işlemlerini yönetir (Taslak). |
| **Log Service** | .NET 7, RabbitMQ, Seq | Sistemdeki olayları kuyruktan dinler ve merkezi log sunucusuna yazar. |

### 🛠️ Kullanılan Altyapı Araçları
* **Veritabanı:** PostgreSQL 15 (Alpine)
* **Cache:** Redis (Distributed Cache)
* **Message Broker:** RabbitMQ (MassTransit)
* **Logging:** Serilog & Datalust Seq
* **Containerization:** Docker & Docker Compose

---

## 🚀 Kurulum ve Çalıştırma (Tek Komut)

Proje, veritabanı kurulumları ve migration işlemleri dahil olmak üzere **tam otomatize** edilmiştir. Bilgisayarınızda **Docker Desktop**'ın kurulu olması yeterlidir.

1.  **Projeyi Klonlayın:**
    ```bash
    git clone https://github.com/balcireal/Task2_ECommerce.git
    cd Task2_ECommerce
    ```

2.  **Sistemi Ayağa Kaldırın:**
    Terminalde proje ana dizinindeyken şu komutu çalıştırın:
    ```bash
    docker-compose up --build
    ```

    *Bu işlem; PostgreSQL, Redis, RabbitMQ ve Seq sunucularını kuracak, ardından mikroservisleri derleyip başlatacaktır. Product Service açılırken veritabanı tablolarını **otomatik olarak (Auto-Migration)** oluşturur.*

---

## 📡 Servis Adresleri ve Erişim

Sistem ayağa kalktığında aşağıdaki adreslerden servislere erişebilirsiniz:

| Panel / Servis | Adres | Açıklama |
| :--- | :--- | :--- |
| **API Gateway (Ana Giriş)** | `http://localhost:5000` | Tüm API istekleri buraya atılmalıdır. |
| **Seq (Log Paneli)** | `http://localhost:5341` | Tüm logları buradan izleyebilirsiniz. |
| **RabbitMQ Paneli** | `http://localhost:15672` | Kullanıcı: `guest` / Şifre: `guest` |
| **Product Service (Internal)**| `http://localhost:5002` | Sadece geliştirme/debug amaçlıdır. |

---

## 🧪 Test Senaryoları (Postman)

Sistemin çalıştığını doğrulamak için aşağıdaki adımları izleyebilirsiniz:

### 1. Ürün Ekleme (CQRS + RabbitMQ Testi)
Gateway üzerinden ürün eklediğinizde, ürün veritabanına yazılır ve RabbitMQ'ya bir "ProductCreated" eventi fırlatılır.

* **URL:** `POST http://localhost:5000/api/products`
* **Body (JSON):**
    ```json
    {
      "name": "Docker Test Urunu",
      "description": "Mikroservis mimarisi ile eklendi.",
      "price": 1500,
      "stock": 50
    }
    ```
* **Beklenen Sonuç:** `200 OK` veya `201 Created`.

### 2. Log Kontrolü (Async Communication Testi)
Ürün eklendikten sonra **Seq Paneline (`http://localhost:5341`)** gidin. Events ekranında şu logu görmelisiniz:
> `[RabbitMQ] Yeni ürün yakalandı! ID: ..., İsim: Docker Test Urunu...`

Bu log, Product Service ile Log Service'in RabbitMQ üzerinden başarıyla konuştuğunu kanıtlar.

### 3. Cache Kontrolü (Redis Testi)
* **URL:** `GET http://localhost:5000/api/products`
* **Sonuç:** Eklediğiniz ürün listelenmelidir. İlk istek veritabanından, sonraki istekler **Redis Cache** üzerinden gelir.

---

## 📂 Proje Yapısı (Onion Architecture)

```text
Task2_ECommerce/
├── Gateways/
│   └── ApiGateway       # YARP Reverse Proxy
├── Services/
│   ├── AuthService      # Kimlik Doğrulama Servisi
│   ├── LogService       # Consumer (RabbitMQ Dinleyicisi)
│   └── ProductService/  # Ana İş Mantığı
│       ├── .API         # Sunum Katmanı (Controllers)
│       ├── .Application # CQRS, DTOs, Mappers
│       ├── .Core        # Domain Entities, Interfaces
│       └── .Infrastructure # Data Access, Migrations, External Services
├── docker-compose.yml   # Orkestrasyon Dosyası
└── README.md