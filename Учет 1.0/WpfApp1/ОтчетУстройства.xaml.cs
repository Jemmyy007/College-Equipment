using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для ОтчетУстройства.xaml
    /// </summary>
    public partial class ОтчетУстройства : Window
    {
        OleDbConnection con;  // Задаем осн. команды для использования
        OleDbCommand cmd1, cmd2, cmd3, cmd4, cmd5, cmd6;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string date = DateTime.Today.ToString("G");
            PrintDialog pr = new PrintDialog();
            if (pr.ShowDialog() == true)
            {
                pr.PrintVisual(logoContainer1, $"{date}");
            }
        }

        OleDbDataReader dr;

        public ОтчетУстройства()
        {
            InitializeComponent();

            con = new OleDbConnection(@"Provider = Microsoft.ACE.Oledb.12.0; Data Source = ДопингБД.accdb"); // Создаем подключение к БД для этой функции
            con.Open();

            string quary1 = "SELECT COUNT(*) FROM Устройства";
            string quary2 = "SELECT COUNT(*) FROM Устройства WHERE Тип = 'Мышь'";
            string quary3 = "SELECT COUNT(*) FROM Устройства WHERE Тип = 'Клавиатура'";
            string quary4 = "SELECT COUNT(*) FROM Устройства WHERE Тип = 'Монитор'";
            string quary5 = "SELECT COUNT(*) FROM Устройства WHERE Тип = 'Бесперебойник'";
            string quary6 = "SELECT COUNT(*) FROM Устройства WHERE Тип = 'Блок'";

            cmd1 = new OleDbCommand(quary1, con);
            cmd2 = new OleDbCommand(quary2, con);
            cmd3 = new OleDbCommand(quary3, con);
            cmd4 = new OleDbCommand(quary4, con);
            cmd5 = new OleDbCommand(quary5, con);
            cmd6 = new OleDbCommand(quary6, con);

            string all = cmd1.ExecuteScalar().ToString();
            string mouse = cmd2.ExecuteScalar().ToString();
            string keyboard = cmd3.ExecuteScalar().ToString();
            string monitor = cmd4.ExecuteScalar().ToString();
            string bes = cmd5.ExecuteScalar().ToString();
            string block = cmd6.ExecuteScalar().ToString();

            lAll.Content += all;
            lBes.Content += bes;
            lBlock.Content += block;
            lMonitor.Content += monitor;
            lMouse.Content += mouse;
            lKeyboard.Content += keyboard;
            lDate.Content += DateTime.Now.ToShortDateString();

        }
    }
}
