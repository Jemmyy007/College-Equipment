using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для ClassRoom.xaml
    /// </summary>
    public partial class ClassRoom : Window
    {
        OleDbConnection con;  // Задаем осн. команды для использования
        OleDbCommand cmd, cmd2, cmd3, cmd4, cmd5;
        OleDbDataReader dr;

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        OleDbDataAdapter da;
        DataSet ds;

        

        public ClassRoom()
        {
            InitializeComponent();

            txtClassNaz.MaxLength = 5;

            con = new OleDbConnection(@"Provider = Microsoft.ACE.Oledb.12.0; Data Source = ДопингБД.accdb"); // Создаем подключение к БД для этой функции
            cmd = new OleDbCommand(); // Создаем команду
            con.Open();

            GRID();

            

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            this.Hide();
            doping8 doping8 = new doping8();
            doping8.Show();
            this.Close();
        }

        private void txtKol_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            con = new OleDbConnection(@"Provider = Microsoft.ACE.Oledb.12.0; Data Source = ДопингБД.accdb"); // Создаем подключение к БД для этой функции
            cmd = new OleDbCommand(); // Создаем команду
            con.Open();

            string name = txtClassNaz.Text;
            if (name != "") 
            {

                string sel = $"SELECT * FROM Кабинеты WHERE [Номер кабинета] = '{name}'";
                OleDbDataAdapter ad = new OleDbDataAdapter(sel, con);
                DataTable dt = new DataTable();
                ad.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show("Такой кабинет уже добавлен!");
                    txtClassNaz.Text = "";
                }
                else
                {
                    string quary = $"INSERT INTO Кабинеты ([Номер кабинета], Комплект) VALUES ('{name}', '-')";
                    cmd = new OleDbCommand(quary, con);
                    cmd.ExecuteNonQuery();

                    GRID();
                   
                    txtClassNaz.Text = "";
                }
            }
            else
            {
                MessageBox.Show("Введите номер кабинета для его добавления!");
            }
            

            con.Close();
        }

        

        
        private void GRID()
        {
            
            da = new OleDbDataAdapter("select * from Кабинеты", con); // Представляет набор команд данных и подключение к базе данных, которые используются для заполнения DataSet и обновления источника данных.
            ds = new DataSet();
            da.Fill(ds, "Кабинеты");

            gridClass.ItemsSource = ds.Tables["Кабинеты"].DefaultView; // Заполнение грида нужной базой
        }
       
    }
}
