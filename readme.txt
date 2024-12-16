# Araç Kiralama Otomasyon Projesi

Bu proje, Visual Programming (C#) kullanılarak geliştirilen bir araç kiralama otomasyonu uygulamasıdır. SQLite veritabanı ile kullanıcı ve araç kiralama bilgilerini saklama, güncelleme, listeleme ve raporlama işlemlerini gerçekleştirir.

## Özellikler

1. **Kullanıcı Yönetimi**:
   - Kullanıcılar `Users` tablosunda saklanır.
   - Kullanıcı bilgileri: İsim, Soyisim, ID, Şifre, Güvenlik Sorusu, Yaş, Telefon Numarası.
   - Kullanıcıların seçtiği araç, kiralama başlangıç ve bitiş tarihleri de aynı tabloda tutulur.

2. **Araç Yönetimi**:
   - Kullanıcıların tercih edebileceği araçlar:
     - BMW M3
     - Mercedes CLA200
     - Fiat Egea
     - Honda Civic
     - Ford Fiesta
     - Fiat Doblo
   - Araç kiralama ücretleri ve sigorta seçenekleri hesaplanarak fiyatlandırma yapılır.

3. **Veritabanı Yönetimi**:
   - SQLite kullanılarak veri güvenliği sağlanır.
   - Kullanıcı bilgileri veritabanına kaydedilir, güncellenir ve SQL sorguları ile listelenir.

4. **Arayüz Özellikleri**:
   - Kullanıcı dostu bir form uygulaması.
   - Kullanıcı, araç seçimi yapabilir ve kiralama bilgilerini görüntüleyebilir.
   - OpenFileDialog ile kullanıcıdan fotoğraf seçme ve kaydetme özelliği.

## Projede Kullanılan Teknolojiler
- **C#**: Visual Studio ortamında Visual Programming kullanılmıştır.
- **SQLite**: Veritabanı yönetimi için kullanılmıştır.
- **Windows Forms**: Kullanıcı arayüzü oluşturmak için kullanılmıştır.

## Proje Yapısı
- **Users Tablosu**:
  - `Id`: Kullanıcı kimliği.
  - `Name`: Kullanıcı adı.
  - `Surname`: Kullanıcı soyadı.
  - `Password`: Kullanıcı şifresi.
  - `Quastion`: Güvenlik sorusu.
  - `Age`: Kullanıcının yaşı.
  - `Phoneno`: Telefon numarası.
  - `Car`: Seçilen araç.
  - `s_date`: Kiralama başlangıç tarihi.
  - `e_date`: Kiralama bitiş tarihi.

## Çalıştırma Adımları
1. Proje dosyasını Visual Studio'da açın.
2. SQLite bağlantı dizesini (`connectionString`) kendi sisteminize göre düzenleyin.
3. Uygulamayı çalıştırın ve kullanıcı ekleme, araç seçme gibi işlemleri gerçekleştirin.
4. Verileri listelemek için SQL komutlarını kullanabilirsiniz.

---

# Car Rental Automation Project

This project is a car rental automation application developed using Visual Programming (C#). It manages storing, updating, listing, and reporting user and car rental information using an SQLite database.

## Features

1. **User Management**:
   - Users are stored in the `Users` table.
   - User details include: Name, Surname, ID, Password, Security Question, Age, Phone Number.
   - Selected car, rental start, and end dates are also recorded in the same table.

2. **Car Management**:
   - Available cars for selection:
     - BMW M3
     - Mercedes CLA200
     - Fiat Egea
     - Honda Civic
     - Ford Fiesta
     - Fiat Doblo
   - Rental fees and insurance options are calculated and displayed to the user.

3. **Database Management**:
   - SQLite is used for secure data storage.
   - User details are saved, updated, and listed using SQL queries.

4. **Interface Features**:
   - User-friendly Windows Forms application.
   - Allows users to select a car and view rental details.
   - Supports image selection and saving using OpenFileDialog.

## Technologies Used
- **C#**: Developed in Visual Studio using Visual Programming.
- **SQLite**: For database management.
- **Windows Forms**: For building the user interface.


## Project Structure
- **Users Table**:
  - `Id`: User ID.
  - `Name`: User's first name.
  - `Surname`: User's last name.
  - `Password`: User password.
  - `Quastion`: Security question.
  - `Age`: User's age.
  - `Phoneno`: Phone number.
  - `Car`: Selected car.
  - `s_date`: Rental start date.
  - `e_date`: Rental end date.

## Steps to Run
1. Open the project in Visual Studio.
2. Adjust the SQLite connection string (`connectionString`) according to your system.
3. Run the application and perform operations such as adding users or selecting cars.
4. Use SQL commands to list data from the database.
