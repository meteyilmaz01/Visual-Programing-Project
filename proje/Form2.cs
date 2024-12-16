    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using System.Data.SQLite;
    using System.IO;
    using static System.Windows.Forms.VisualStyles.VisualStyleElement;
    using System.Diagnostics;
    using System.Threading;
    using NDbUnit.Core.SqlLite;
    using Microsoft.SqlServer.Server;
    using System.Data.SqlClient;
using System.Windows.Forms.DataVisualization.Charting;

    namespace proje
    {

    public partial class Form2 : Form
    {
        string databasePath;
        public string connectionString;
        double fiyat;
    
            
            private void UpdateComboBox2()
            {
                try
                {
                    
                    using (SQLiteConnection conn = new SQLiteConnection(connectionString))
                    {
                        conn.Open();

                        
                        string query = "SELECT Name, Surname FROM Users";

                        using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            
                            comboBox2.Items.Clear();

                            
                            while (reader.Read())
                            {
                                string fullName = reader["Name"].ToString() + " " + reader["Surname"].ToString();
                                comboBox2.Items.Add(fullName);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Bir hata oluştu: {ex.Message}");
                }
            }

            public Form2()
            {
                InitializeComponent();
            }
            private void Form2_Load(object sender, EventArgs e)
            {
            maskedTextBox2.Text = DateTime.Now.ToString("dd/MM/yyyy");
            databasePath = @"C:\Users\metek\source\repos\proje\proje\bin\Debug\Data.db";
                connectionString = $"Data Source={databasePath};";
                comboBox1.Enabled = true;
                comboBox2.Enabled = true;      
                UpdateComboBox2();
            LoadCarStatistics();
            LoadAgeDistribution();
        }

        
        private void LoadCarStatistics() // Araba tablosu için fonksiyonum
        {
            
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    
                    string query = @"
                SELECT car, COUNT(*) AS CarCount
                FROM Users
                WHERE car IN ('BMW M3', 'Mercedes CLA200', 'Fiat Egea', 'Honda Civic', 'Ford Fiesta', 'Fiat Doblo')
                GROUP BY car";

                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conn);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    
                    chart1.Series.Clear();
                    chart1.ChartAreas.Clear();
                    ChartArea chartArea = new ChartArea();
                    chart1.ChartAreas.Add(chartArea);

                    Series series = new Series
                    {
                        Name = "CarStatistics",
                        ChartType = SeriesChartType.Column,
                        XValueType = ChartValueType.String
                    };

                    chart1.Series.Add(series);

                    
                    foreach (DataRow row in dataTable.Rows)//Verileri grafiğe yüklemek
                    {
                        string carName = row["car"].ToString();
                        int count = Convert.ToInt32(row["CarCount"]);
                        series.Points.AddXY(carName, count);
                    }

                    
                    chart1.Width = 300;
                    chart1.Height = 200; 

                    chart1.Dock = DockStyle.None; // Boyutları manuel ayarlamak için Dock özelliğini devre dışı bırak
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Hata: {ex.Message}");
                }
            }
        }




    private void label9_Click(object sender, EventArgs e)
            {

            }

            private void button1_Click(object sender, EventArgs e)
            {
            

        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }









        private void label13_Click(object sender, EventArgs e)
            {

            }

        private void button3_Click(object sender, EventArgs e)
        {
            double fiyat = 0;
            int adet = 0;

            adet=Convert.ToInt32(textBox4.Text);
 

           
            if (comboBox1.SelectedItem == null) //Seçilen araca göre fiyat güncellemesi
            {
                MessageBox.Show("Lütfen geçerli bir araç seçin.");
                return;
            }

            
            if (comboBox1.SelectedItem.ToString() == "BMW M3")
            {
                fiyat = 750 * adet;
            }
            else if (comboBox1.SelectedItem.ToString() == "Mercedes CLA200")
            {
                fiyat = 650 * adet;
            }
            else if (comboBox1.SelectedItem.ToString() == "Fiat Egea")
            {
                fiyat = 400 * adet;
            }
            else if (comboBox1.SelectedItem.ToString() == "Honda Civic")
            {
                fiyat = 500 * adet;
            }
            else if (comboBox1.SelectedItem.ToString() == "Ford Fiesta")
            {
                fiyat = 350 * adet;
            }
            else if (comboBox1.SelectedItem.ToString() == "Fiat Doblo")
            {
                fiyat = 600 * adet;
            }
            else
            {
                MessageBox.Show("Lütfen geçerli bir araç seçin.");
                return;
            }

           
            if (radioButton1.Checked) //radiobox ile yüzdelik fiyat artışı
            {
                fiyat = fiyat * 1.50;
            }
            else if (radioButton2.Checked)
            {
                fiyat = fiyat * 1.20;
            }
            else
            {
                MessageBox.Show("Bir sigorta seçiniz.");
                return;
            } 
            label7.Text = fiyat.ToString();
        }
        private void label14_Click(object sender, EventArgs e)
            {

            }

            private void button5_Click(object sender, EventArgs e)
            {
                this.Hide();
                Form1 form1 = new Form1();
                form1.Show();
            }

        private void maskedTextBox2_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void maskedTextBox3_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)//Tarih bilgilerini tabloma eklemek için
        {
            try
            {
                
                DateTime startDate = DateTime.ParseExact(maskedTextBox2.Text, "dd/MM/yyyy", null);

                
                int daysToAdd = int.Parse(textBox4.Text); 
                DateTime endDate = startDate.AddDays(daysToAdd);

               
                maskedTextBox3.Text = endDate.ToString("dd/MM/yyyy");

                
                if (comboBox2.SelectedItem == null)
                {
                    MessageBox.Show("Lütfen bir kullanıcı seçin.");
                    return;
                }

                string selectedUser = comboBox2.SelectedItem.ToString();
                string[] nameParts = selectedUser.Split(' ');
                string name = nameParts[0];
                string surname = nameParts[1];

               
                if (comboBox1.SelectedItem == null)
                {
                    MessageBox.Show("Lütfen bir araç seçin.");
                    return;
                }

                string selectedCar = comboBox1.SelectedItem.ToString();

                
                using (SQLiteConnection conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();

                   
                    string query = "SELECT COUNT(*) FROM Users WHERE Name = @Name AND Surname = @Surname";
                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Name", name);
                        cmd.Parameters.AddWithValue("@Surname", surname);

                        int userExists = Convert.ToInt32(cmd.ExecuteScalar());

                        if (userExists == 0)
                        {
                            MessageBox.Show("Seçilen kullanıcı bulunamadı.");
                            return;
                        }
                    }

                    
                    string insertQuery = "UPDATE Users SET Car = @Car, s_date = @StartDate, e_date = @EndDate WHERE Name = @Name AND Surname = @Surname";
                    using (SQLiteCommand cmd = new SQLiteCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@Car", selectedCar);
                        cmd.Parameters.AddWithValue("@StartDate", startDate.ToString("yyyy-MM-dd"));
                        cmd.Parameters.AddWithValue("@EndDate", endDate.ToString("yyyy-MM-dd"));
                        cmd.Parameters.AddWithValue("@Name", name);
                        cmd.Parameters.AddWithValue("@Surname", surname);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Kullanıcı bilgileri güncellendi.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bir hata oluştu: {ex.Message}");
            }
            LoadCarStatistics();
            LoadAgeDistribution();
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                using (SQLiteConnection SQLconnect = new SQLiteConnection(connectionString))
                {
                    SQLconnect.Open();
                    using (SQLiteCommand SQLcommand = SQLconnect.CreateCommand())
                    {
                        SQLcommand.CommandText = "INSERT INTO Users(Id,Name,Surname,Password,Licace,Quastion,Phoneno,Age) VALUES (@Id,@Name,@Surname,@Password,@Licace,@Quastion,@Phoneno,@Age)";
                        SQLcommand.Parameters.AddWithValue("@Id", textBox1.Text);
                        SQLcommand.Parameters.AddWithValue("@Name", textBox2.Text);
                        SQLcommand.Parameters.AddWithValue("@Surname", textBox3.Text);
                        SQLcommand.Parameters.AddWithValue("@Password", textBox5.Text);
                        SQLcommand.Parameters.AddWithValue("@Quastion", textBox6.Text);
                        SQLcommand.Parameters.AddWithValue("@Phoneno", maskedTextBox1.Text);
                        SQLcommand.Parameters.AddWithValue("@Age", textBox7.Text);


                        // Resim verisini byte dizisine dönüştür
                        using (MemoryStream ms = new MemoryStream())
                        {
                            pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
                            byte[] imageBytes = ms.ToArray();
                            SQLcommand.Parameters.AddWithValue("@Licace", imageBytes);
                        }

                        SQLcommand.ExecuteNonQuery();
                        MessageBox.Show("Kişi bilgileri veritabanına eklendi");




                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}");
            }
            UpdateComboBox2();
        }

        private void button2_Click_1(object sender, EventArgs e)// Ehliyet fotoğrafı yüklemek için 
        {
            
            OpenFileDialog openFileDialog = new OpenFileDialog();

           
            openFileDialog.Filter = "Resim Dosyaları|*.jpg;*.jpeg;*.png;*.bmp;*.gif";

            
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string selectedFilePath = openFileDialog.FileName;

                try
                {
                    
                    pictureBox1.Image = Image.FromFile(selectedFilePath);

                   
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                }
                catch (Exception ex)
                {
                    
                    MessageBox.Show("Bir hata oluştu: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Dosya seçilmedi.");
            }
        }
        private void LoadAgeDistribution() //Kullanıcıların yaşlarını chart2 ye eklemek için fonksiyonum
        {
            chart2.Legends.Clear();

            
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    
                    string query = @"
                SELECT Age, COUNT(*) AS Count
                FROM Users
                GROUP BY Age
                ORDER BY Age";

                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conn);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    
                    chart2.Series.Clear();
                    chart2.ChartAreas.Clear();

                    ChartArea chartArea = new ChartArea();
                    chart2.ChartAreas.Add(chartArea);

                    Series series = new Series
                    {
                        Name = "AgeDistribution",
                        ChartType = SeriesChartType.Pie,
                        XValueType = ChartValueType.Int32
                    };

                    chart2.Series.Add(series);

                    
                    foreach (DataRow row in dataTable.Rows) // DataBase'den alınan verileri tabloma eklemek için
                    {
                        int age = Convert.ToInt32(row["Age"]);
                        int count = Convert.ToInt32(row["Count"]);
                        series.Points.AddXY(age, count);
                    }

                    
                    chart2.Legends.Clear();
                    Legend legend = new Legend
                    {
                        Name = "AgeLegend",
                        Docking = Docking.Right
                    };
                    chart2.Legends.Add(legend);

                   
                    chart2.Dock = DockStyle.None; 
                    chart2.Width = 300;
                    chart2.Height = 200;

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Hata: {ex.Message}");
                }
            }
        }


        private void button5_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form1 = new Form1();
            form1.Show();
        }
    }

    }


    
