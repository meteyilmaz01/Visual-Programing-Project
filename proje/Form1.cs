using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Data.SQLite; 
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace proje
{
    public partial class Form1 : Form
    {
        string username;
        string password;
        string connectionString;
        public Form1()
        {
            InitializeComponent();
            
            connectionString = "Data Source=Data.db;Version=3;";  
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!File.Exists("Data.db"))
            {
                MessageBox.Show("Veritabanı dosyası bulunamadı.");
                Application.Exit();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            username = maskedTextBox1.Text;
            password = maskedTextBox2.Text;

           
            if (username == "admin" && password == "admin123")
            {
                this.Hide();
                Form2 form2 = new Form2();
                form2.Show();
            }
            else
            {
               
                string query = "SELECT COUNT(1) FROM Users WHERE Name = @Name AND Password = @Password";

                using (SQLiteConnection conn = new SQLiteConnection(connectionString)) 
                {
                    try
                    {
                        conn.Open(); 

                        using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                        {
                            
                            cmd.Parameters.AddWithValue("@Name", username); 
                            cmd.Parameters.AddWithValue("@Password", password);

                            int userCount = Convert.ToInt32(cmd.ExecuteScalar()); 

                            if (userCount > 0)
                            {
                                

                                Form3 fr =new Form3();
                                fr.bilgi = username;
                                fr.ShowDialog();   
                                this.Close();
                               
                        
                                Form3 form3 = new Form3();
                                form3.Show();
                            }
                            else
                            {
                                
                                MessageBox.Show("Böyle bir kullanıcı bulunamadı. Lütfen bilgilerinizi kontrol edin.");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Veritabanı bağlantı hatası: " + ex.Message);
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form4 form4 = new Form4();
            form4.Show();

        }
    }
}
