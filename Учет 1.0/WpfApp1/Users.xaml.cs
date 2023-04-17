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
    /// Логика взаимодействия для Users.xaml
    /// </summary>
    public partial class Users : Window
    {
        OleDbConnection con;
        OleDbCommand cmd;
        OleDbDataReader dr;
        OleDbDataAdapter da;
        DataSet ds;

        public Users()
        {
            InitializeComponent();
            comPostAdd.SelectedIndex = 2;
            txtLoginAdd.MaxLength = 15;
            txtPassAdd.MaxLength = 15;
            txtLoginChange.MaxLength = 15;
            txtPassChange.MaxLength = 15;
            GRID();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            con = new OleDbConnection(@"Provider = Microsoft.ACE.Oledb.12.0; Data Source = ДопингБД.accdb"); // Создаем подключение к БД для этой функции

            con.Open();

            string Логин = txtLoginAdd.Text;
            string Пароль = txtPassAdd.Text;
            string fio = txtFIOadd.Text;
            string Должность = comPostAdd.Text;

            if (Логин == "" || Пароль == "" || fio == "")
            {
                MessageBox.Show("Введите данные для добавления!");
            }
            else
            {
                string count = $"SELECT COUNT(*) FROM Авторизация WHERE Логин = '{Логин}'";
                OleDbCommand cmdCount = new OleDbCommand(count, con);

                int a = Convert.ToInt32(cmdCount.ExecuteScalar());

                if (a < 1)
                {
                    string quary = $"INSERT INTO Авторизация (Логин, Пароль, Должность, ФИО) VALUES ('{Логин}', '{Пароль}', '{Должность}', '{fio}')";
                    cmd = new OleDbCommand(quary, con);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Пользователь добавлен!");

                    GRID();
                    txtLoginAdd.Text = "";
                    txtPassAdd.Text = "";
                    txtFIOadd.Text = "";
                }
                else
                {
                    MessageBox.Show("Такой Логин уже существует!");
                    txtLoginAdd.Text = "";
                    txtPassAdd.Text = "";
                }   
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            con = new OleDbConnection(@"Provider = Microsoft.ACE.Oledb.12.0; Data Source = ДопингБД.accdb");

            cmd = new OleDbCommand();
            con.Open();

            try
            {
                int kod = Convert.ToInt32(txtKod.Text);
                string Логин = txtLoginChange.Text;
                string Пароль = txtPassChange.Text;
                string fio = txtFIOchange.Text;
                string Должность = comPostChange.Text;

                string quaey = $"SELECT COUNT(*) FROM Авторизация WHERE Код = {kod}";
                OleDbCommand cmdSel = new OleDbCommand(quaey, con);
                int a = Convert.ToInt32(cmdSel.ExecuteScalar());

                if (a > 0)
                {
                    string update = $"DELETE * FROM Авторизация WHERE Код = {kod}";
                    OleDbCommand cmdUp = new OleDbCommand(update, con);
                    cmdUp.ExecuteNonQuery();

                    MessageBox.Show("Запись Удалена!");

                    txtKod.Text = "";
                    txtFIOchange.Text = "";
                    txtPassChange.Text = "";
                    txtLoginChange.Text = "";
                    comPostChange.SelectedIndex = -1;

                    GRID();
                }
                else
                {
                    MessageBox.Show("Такой записи не существует!");
                    txtKod.Text = "";
                    txtFIOchange.Text = "";
                    txtPassChange.Text = "";
                    txtLoginChange.Text = "";
                    comPostChange.SelectedIndex = -1;
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Введите код!");
                txtKod.Text = "";
                txtFIOchange.Text = "";
                txtPassChange.Text = "";
                txtLoginChange.Text = "";
                comPostChange.SelectedIndex = -1;
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            con = new OleDbConnection(@"Provider = Microsoft.ACE.Oledb.12.0; Data Source = ДопингБД.accdb");

            cmd = new OleDbCommand();
            con.Open();
            try
            {
                int kod = Convert.ToInt32(txtKod.Text);
                string Логин = txtLoginChange.Text;
                string Пароль = txtPassChange.Text;
                string fio = txtFIOchange.Text;
                string Должность = comPostChange.Text;

                string quaey = $"SELECT COUNT(*) FROM Авторизация WHERE Код = {kod}";
                OleDbCommand cmdSel = new OleDbCommand(quaey, con);
                int a = Convert.ToInt32(cmdSel.ExecuteScalar());

                if (a > 0)
                {
                    string update = $"UPDATE Авторизация SET Логин = '{Логин}', Пароль = '{Пароль}', Должность = '{Должность}', ФИО = '{fio}' WHERE Код = {kod}";
                    OleDbCommand cmdUp = new OleDbCommand(update, con);
                    cmdUp.ExecuteNonQuery();

                    MessageBox.Show("Запись изменена!");

                    txtKod.Text = "";
                    txtFIOchange.Text = "";
                    txtPassChange.Text = "";
                    txtLoginChange.Text = "";
                    comPostChange.SelectedIndex = -1;

                    GRID();
                }
                else
                {
                    MessageBox.Show("Такой записи не существует!");
                    txtKod.Text = "";
                    txtFIOchange.Text = "";
                    txtPassChange.Text = "";
                    txtLoginChange.Text = "";
                    comPostChange.SelectedIndex = -1;
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Введите код!");
                txtKod.Text = "";
                txtFIOchange.Text = "";
                txtPassChange.Text = "";
                txtLoginChange.Text = "";
                comPostChange.SelectedIndex = -1;
            }
        }

        private void txtKod_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                con = new OleDbConnection(@"Provider = Microsoft.ACE.Oledb.12.0; Data Source = ДопингБД.accdb");

                cmd = new OleDbCommand();
                con.Open();

                int kod = Convert.ToInt32(txtKod.Text);


                string quary = $"SELECT COUNT(*) FROM Авторизация WHERE Код = {kod}";
                cmd = new OleDbCommand(quary, con);
                int a = Convert.ToInt32(cmd.ExecuteScalar());

                if (a > 0)
                {


                    string quary3 = $"SELECT Логин FROM Авторизация WHERE Код = {kod}";
                    string quary4 = $"SELECT Пароль FROM Авторизация WHERE Код = {kod}";
                    string quary6 = $"SELECT ФИО FROM Авторизация WHERE Код = {kod}";

                    OleDbCommand cmd3 = new OleDbCommand(quary3, con);
                    OleDbCommand cmd4 = new OleDbCommand(quary4, con);
                    OleDbCommand cmd6 = new OleDbCommand(quary6, con);

                    string g3 = cmd3.ExecuteScalar().ToString();
                    string g4 = cmd4.ExecuteScalar().ToString();
                    string g6 = cmd6.ExecuteScalar().ToString();

                    if (g3 != null && g4 != null && g6 != null)
                    {


                        txtFIOchange.Text = g6;
                        txtLoginChange.Text = g3;
                        txtPassChange.Text = g4;

                        string quary5 = $"SELECT Должность FROM Авторизация WHERE Код = {kod}";
                        OleDbCommand cmd5 = new OleDbCommand(quary5, con);
                        string g5 = cmd5.ExecuteScalar().ToString();



                        //int ind = comДолжностьChange.Items.IndexOf(comДолжностьChange.FindName(g5));

                        comPostChange.Text = g5;



                    }


                }

            }
            catch (Exception ex)
            {

            }
        }

        private void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtKod.Text == "")
            {
                txtFIOchange.Text = "";
                txtLoginChange.Text = "";
                txtPassChange.Text = "";
                comPostChange.SelectedIndex = -1;
            }
        }
        private void GRID()
        {
            con = new OleDbConnection(@"Provider = Microsoft.ACE.Oledb.12.0; Data Source = ДопингБД.accdb");

            con.Open();

            da = new OleDbDataAdapter("select * from Авторизация", con);
            ds = new DataSet();
            da.Fill(ds, "Авторизация");

            GridStaff.ItemsSource = ds.Tables["Авторизация"].DefaultView;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            doping8 doping8 = new doping8();
            doping8.Show();
            this.Close();
        }

        private void comPostAdd_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
