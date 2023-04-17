using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Security.Cryptography;
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
    /// Логика взаимодействия для ОтчетКласс.xaml
    /// </summary>
    public partial class ОтчетКласс : Window
    {
        OleDbConnection con;  // Задаем осн. команды для использования
        OleDbCommand cmd1, cmd2, cmd3, cmd4, cmd5, cmd6;

        public ОтчетКласс()
        {
            InitializeComponent();

            con = new OleDbConnection(@"Provider = Microsoft.ACE.Oledb.12.0; Data Source = ДопингБД.accdb"); // Создаем подключение к БД для этой функции
            con.Open();

            string quary1 = "SELECT COUNT(*) FROM Кабинеты";
            string quary2 = "SELECT COUNT(*) FROM Кабинеты WHERE Колличество <> 0";
            string quary3 = "SELECT COUNT(*) FROM Кабинеты WHERE Колличество = 0";
            

            cmd1 = new OleDbCommand(quary1, con);
            cmd2 = new OleDbCommand(quary2, con);
            cmd3 = new OleDbCommand(quary3, con);
            

            string all = cmd1.ExecuteScalar().ToString();
            string Full = cmd2.ExecuteScalar().ToString();
            string NotFull = cmd3.ExecuteScalar().ToString();
            

            lAll.Content += all;
            lFull.Content += Full;
            lNotFull.Content += NotFull;
            
            lDate.Content += DateTime.Now.ToLongDateString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string date = DateTime.Today.ToString("G");
            PrintDialog pr = new PrintDialog();
            if (pr.ShowDialog() == true)
            {
                pr.PrintVisual(logoContainer1, $"{date}");
            }
        }
    }
}
