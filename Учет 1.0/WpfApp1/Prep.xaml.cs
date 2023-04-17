using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
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
    /// Логика взаимодействия для Prep.xaml
    /// </summary>
    public partial class Prep : Window
    {
        OleDbConnection con;  // Задаем осн. команды для использования
        OleDbCommand cmd;
        OleDbDataReader dr;
        OleDbDataAdapter da;
        DataSet ds;

        public Prep()
        { 
            InitializeComponent();
            con = new OleDbConnection(@"Provider = Microsoft.ACE.Oledb.12.0; Data Source = ДопингБД.accdb"); // Создаем подключение к БД для этой функции
            cmd = new OleDbCommand(); // Создаем команду
            con.Open();

            GRID();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            con = new OleDbConnection(@"Provider = Microsoft.ACE.Oledb.12.0; Data Source = ДопингБД.accdb"); // Создаем подключение к БД для этой функции
            cmd = new OleDbCommand(); // Создаем команду
            con.Open();

            string search = txtSearch.Text;

            string quary = $"SELECT * FROM Кабинеты WHERE Код LIKE '%{search}%' OR [Номер кабинета] LIKE '%{search}%' OR Комплект  LIKE '%{search}%' OR Колличество  LIKE '%{search}%'";
            OleDbDataAdapter da2 = new OleDbDataAdapter(quary, con);
            DataSet ds2 = new DataSet();
            da2.Fill(ds2, "Кабинеты");
            GridClass.ItemsSource = ds2.Tables["Кабинеты"].DefaultView;

            con.Close();
        }

        private void GRID()
        {

            da = new OleDbDataAdapter("select * from Кабинеты", con); // Представляет набор команд данных и подключение к базе данных, которые используются для заполнения DataSet и обновления источника данных.
            ds = new DataSet();
            da.Fill(ds, "Кабинеты");

            GridClass.ItemsSource = ds.Tables["Кабинеты"].DefaultView; // Заполнение грида нужной базой
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ОтчетКласс classroom = new ОтчетКласс();
            classroom.Show();
        }
    }
}
