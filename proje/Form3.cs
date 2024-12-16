using System;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace proje
{
    public partial class Form3 : Form
    {
        string databasePath;
        string connectionString;
        public string bilgi = "";

        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            listBox1.Items.Add("İsim");
            listBox1.Items.Add("Soyisim");
            listBox1.Items.Add("Telefon Numarası");
            listBox1.Items.Add("Şifre");

            databasePath = @"C:\Users\metek\source\repos\proje\proje\bin\Debug\Data.db";
            connectionString = $"Data Source={databasePath}; Version=3;";
            label1.Text = bilgi;

            LoadUserData();
            LoadAdditionalData();
        }

        private void LoadUserData()
        {
            string query = "SELECT Surname, Phoneno, Id, Licace ,Age FROM Users WHERE Name = @Name";

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SQLiteCommand command = new SQLiteCommand(query, connection);
                    command.Parameters.AddWithValue("@Name", bilgi);

                    SQLiteDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        label2.Text = reader["Surname"].ToString();
                        label3.Text = reader["Phoneno"].ToString();
                        label4.Text = reader["Id"].ToString();
                        label13.Text= reader["Age"].ToString() ;

                        byte[] imageBytes = (byte[])reader["Licace"];
                        using (MemoryStream ms = new MemoryStream(imageBytes))
                        {
                            pictureBox1.Image = Image.FromStream(ms);
                        }
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Bir hata oluştu: " + ex.Message);
                }
            }
        }

        private void LoadAdditionalData()
        {
            string query = "SELECT Car, s_date, e_date FROM Users WHERE Name = @Name";

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SQLiteCommand command = new SQLiteCommand(query, connection);
                    command.Parameters.AddWithValue("@Name", bilgi);

                    SQLiteDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        label8.Text = reader["Car"].ToString();
                        label10.Text = reader["s_date"].ToString();
                        label12.Text = reader["e_date"].ToString();
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Bir hata oluştu: " + ex.Message);
                }
            }
        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Kullanıcıdan alınan değerler
            string searchValue = textBox1.Text; //Kullanıcı güvenlik sorusu cevabı
            string newValue = textBox2.Text;   
            string selectedField = listBox1.Text; 
            string currentUser = label1.Text;  

            if (string.IsNullOrWhiteSpace(searchValue) || string.IsNullOrWhiteSpace(newValue))
            {
                MessageBox.Show("Lütfen tüm alanları doldurun!");
                return;
            }

           
            string columnName = "";
            switch (selectedField) // ListBox'da seçilen veriye göre bilgi güncellemek için
            {
                case "İsim":
                    columnName = "Name";
                    break;
                case "Soyisim":
                    columnName = "Surname";
                    break;
                case "Telefon Numarası":
                    columnName = "Phoneno";
                    break;
                case "Şifre":
                    columnName = "Password";
                    break;
                default:
                    MessageBox.Show("Lütfen geçerli bir kriter seçin!");
                    return;
            }

            
            string checkQuery = "SELECT Quastion FROM Users WHERE Name = @Name"; // İsime göre güvenlik sorusu 
            string updateQuery = $"UPDATE Users SET {columnName} = @NewValue WHERE Name = @Name";

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    
                    using (SQLiteCommand checkCommand = new SQLiteCommand(checkQuery, connection))//Kontrol işlemi
                    {
                        checkCommand.Parameters.AddWithValue("@Name", currentUser);
                        object quastionValue = checkCommand.ExecuteScalar();

                        if (quastionValue == null || quastionValue.ToString() != searchValue)
                        {
                            MessageBox.Show("Girilen Quastion değeri doğru değil. Güncelleme yapılamaz.");
                            return;
                        }
                    }

                    // Güncelleme sorgusunu çalıştır
                    using (SQLiteCommand updateCommand = new SQLiteCommand(updateQuery, connection))
                    {
                        updateCommand.Parameters.AddWithValue("@NewValue", newValue);
                        updateCommand.Parameters.AddWithValue("@Name", currentUser);

                        int rowsAffected = updateCommand.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Veri başarıyla güncellendi!");
                            LoadUserData();
                        }
                        else
                        {
                            MessageBox.Show("Güncelleme işlemi başarısız oldu. Lütfen bilgileri kontrol edin.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Bir hata oluştu: " + ex.Message);
                }
            }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form1 = new Form1();
            form1.Show();
        }
    }
}
