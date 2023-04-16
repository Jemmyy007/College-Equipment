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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            OleDbConnection con = new OleDbConnection(@"Provider = Microsoft.ACE.Oledb.12.0; Data Source = ДопингБД.accdb"); // Создаем подключение к БД для этой функции
      
            con.Open();

            string user = txtLogin.Text;
            string password = txtPassword.Password;

            

            string quary = $"SELECT Должность FROM Авторизация WHERE Логин = '{user}' AND Пароль = '{password}'";

            OleDbDataAdapter da = new OleDbDataAdapter(quary, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("Неправильно введены данные");
            }
            else if (dt.Rows.Count == 1 && dt.Rows[0][0].ToString() == "Админ")
            {
                string quary1 = $"SELECT ФИО FROM Авторизация WHERE Логин = '{user}' AND Пароль = '{password}'";
                OleDbCommand fio1 = new OleDbCommand(quary1, con);
                string FIO1 = fio1.ExecuteScalar().ToString();

                this.Hide();
                MessageBox.Show($"Добро пожаловать, {FIO1}!");
                doping8 doping8 = new doping8();
                doping8.ShowDialog();
                this.Close();
            }
            else if (dt.Rows.Count == 1 && dt.Rows[0][0].ToString() == "Специалист")
            {
                string quary2 = $"SELECT ФИО FROM Авторизация WHERE Логин = '{user}' AND Пароль = '{password}'";
                OleDbCommand fio2 = new OleDbCommand(quary2, con);
                string FIO2 = fio2.ExecuteScalar().ToString();

                this.Hide();
                MessageBox.Show($"Добро пожаловать, {FIO2}!");
                doping2 doping2 = new doping2();
                doping2.ShowDialog();
                this.Close();
            }
            else if (dt.Rows.Count == 1 && dt.Rows[0][0].ToString() == "Преподаватель")
            {
                string quary3 = $"SELECT ФИО FROM Авторизация WHERE Логин = '{user}' AND Пароль = '{password}'";
                OleDbCommand fio3 = new OleDbCommand(quary3, con);
                string FIO3 = fio3.ExecuteScalar().ToString();

                this.Hide();
                MessageBox.Show($"Добро пожаловать, {FIO3}!");
                Prep prep = new Prep();
                prep.ShowDialog();
                this.Close();
            }
        }
    }
}
