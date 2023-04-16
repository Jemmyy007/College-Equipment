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
    /// Логика взаимодействия для ОтчетКомплект.xaml
    /// </summary>
    public partial class ОтчетКомплект : Window
    {
        OleDbConnection con;  // Задаем осн. команды для использования
        OleDbCommand cmd1, cmd2, cmd3, cmd4, cmd5, cmd6;

        public ОтчетКомплект()
        {
            InitializeComponent();

            con = new OleDbConnection(@"Provider = Microsoft.ACE.Oledb.12.0; Data Source = ДопингБД.accdb"); // Создаем подключение к БД для этой функции
            con.Open();

            string quary1 = "SELECT COUNT(*) FROM Комплект";
            string quary2 = "SELECT COUNT(*) FROM Комплект WHERE Блок <> 0 AND Мышь <> 0 AND Клавиатура <> 0 AND Монитор <> 0 AND Бесперебойник <> 0";
            string quary3 = "SELECT COUNT(*) FROM Комплект WHERE Блок = 0 OR Мышь = 0 OR Клавиатура = 0 OR Монитор = 0 OR Бесперебойник = 0";
            string quary4 = "SELECT COUNT(*) FROM Комплект WHERE Класс <> '-'";
            string quary5 = "SELECT COUNT(*) FROM Комплект WHERE Класс = '-'";

            cmd1 = new OleDbCommand(quary1, con);
            cmd2 = new OleDbCommand(quary2, con);
            cmd3 = new OleDbCommand(quary3, con);
            cmd4 = new OleDbCommand(quary4, con);
            cmd5 = new OleDbCommand(quary5, con);

            string all = cmd1.ExecuteScalar().ToString();
            string Full = cmd2.ExecuteScalar().ToString();
            string NotFull = cmd3.ExecuteScalar().ToString();
            string Class = cmd4.ExecuteScalar().ToString();
            string NotClass = cmd5.ExecuteScalar().ToString();

            lAll.Content += all;
            lFull.Content += Full;
            lNotFull.Content += NotFull;
            lClass.Content += Class;
            lNotClass.Content += NotClass;
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
