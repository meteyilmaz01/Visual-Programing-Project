using System;
using System.Data.SQLite;
using System.Windows.Forms;

namespace proje
{
    public partial class Form4 : Form
    {
        string databasePath;
        string connectionString;

        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {

            databasePath = @"C:\Users\metek\source\repos\proje\proje\bin\Debug\Data.db";
            connectionString = $"Data Source={databasePath}; Version=3;";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string userName = textBox1.Text;
            string question = textBox2.Text;
            string newPassword = textBox3.Text;
            string confirmPassword = textBox4.Text;

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    
                    string query = "SELECT Quastion, Password FROM Users WHERE Name = @Name"; //Kullanıcı adına göre şifre ve soru değerini döndürür
                    SQLiteCommand cmd = new SQLiteCommand(query, connection);
                    cmd.Parameters.AddWithValue("@Name", userName);

                    SQLiteDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        string storedQuestion = reader["Quastion"].ToString();

                        if (storedQuestion == question)
                        {
                            
                            if (newPassword == confirmPassword)
                            {
                                reader.Close();

                                
                                string updateQuery = "UPDATE Users SET Password = @newPassword WHERE Name = @Name";//Yeni şifreyi update eder
                                SQLiteCommand updateCmd = new SQLiteCommand(updateQuery, connection);
                                updateCmd.Parameters.AddWithValue("@newPassword", newPassword);
                                updateCmd.Parameters.AddWithValue("@Name", userName);

                                int rowsAffected = updateCmd.ExecuteNonQuery();

                                if (rowsAffected > 0)
                                {
                                    MessageBox.Show("Şifre başarıyla değiştirildi.");
                                    Form1 form1 = new Form1();
                                    form1.Show();
                                    this.Hide();
                                }
                                else
                                {
                                    MessageBox.Show("Bir hata oluştu.");
                                }
                            }
                            else
                            {
                                MessageBox.Show("Yeni şifre ve onay şifresi uyuşmuyor.");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Soru cevabı yanlış.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Kullanıcı adı bulunamadı.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form1 = new Form1();
            form1.Show();
        }
    }
}
