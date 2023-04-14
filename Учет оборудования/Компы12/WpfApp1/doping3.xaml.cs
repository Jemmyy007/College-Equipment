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
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace WpfApp1
{
    


    public partial class doping3 : Window
    {   
        

        private OleDbConnection myConnection;
        public static string connectString = @"Provider = Microsoft.ACE.Oledb.12.0; Data Source = ДопингБД.accdb";  // Подключаем БД

        OleDbConnection con;  // Задаем осн. команды для использования
        OleDbCommand cmd;
        OleDbDataReader dr;
        OleDbDataAdapter da;
        DataSet ds;

        public doping3()
        {
            

            

            InitializeComponent();
            txtKod.MaxLength = 6;
            txtCol.MaxLength = 5;

            con = new OleDbConnection(@"Provider = Microsoft.ACE.Oledb.12.0; Data Source = ДопингБД.accdb"); // Создаем подключение к БД для этой функции
            cmd = new OleDbCommand(); // Создаем команду
            con.Open(); // Открываем соединение с базой

            da = new OleDbDataAdapter("select * from Устройства", con); // Представляет набор команд данных и подключение к базе данных, которые используются для заполнения DataSet и обновления источника данных.
            ds = new DataSet();
            da.Fill(ds, "Устройства");

            SkladGrid.ItemsSource = ds.Tables["Устройства"].DefaultView; // Заполнение грида нужной базой
            con.Close();


            /* da = new OleDbDataAdapter("select * from Комплект", con); // Представляет набор команд данных и подключение к базе данных, которые используются для заполнения DataSet и обновления источника данных.
             ds = new DataSet();
             da.Fill(ds, "Комплект");

             ComplGrid.ItemsSource = ds.Tables["Комплект"].DefaultView; // Заполнение грида нужной базой
             con.Close(); */


            TipCombo.SelectedIndex = 0;
            SostCombo.SelectedIndex = 0;

            myConnection = new OleDbConnection(connectString);


            myConnection.Open();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {          
            this.Close();
        }

        private void Dob_Click(object sender, RoutedEventArgs e)
        {
            con = new OleDbConnection(@"Provider = Microsoft.ACE.Oledb.12.0; Data Source = ДопингБД.accdb"); // Создаем подключение к БД для этой функции
            cmd = new OleDbCommand(); // Создаем команду
            con.Open(); // Открываем соединение с базой
            string naz = NazvanieTxt.Text;
            if (naz == "")
            {
                MessageBox.Show("Введите название устройств!");
            }
            else 
            {
                try
                {
                    string sost = SostCombo.Text;
                    string tip = TipCombo.Text;
                    int col = Convert.ToInt32(txtCol.Text);

                        for (int i = 0; i < col; i++)
                        {



                            string query = $"INSERT INTO Устройства (Название, Тип, Состояние, [Номер комплекта]) VALUES ('{naz}', '{tip}', '{sost}', 0)";
                            cmd = new OleDbCommand(query, myConnection);
                            cmd.ExecuteNonQuery();

                        }

                        MessageBox.Show("Устройства добавлены");

                        da = new OleDbDataAdapter("select * from Устройства", con); // Представляет набор команд данных и подключение к базе данных, которые используются для заполнения DataSet и обновления источника данных.
                        ds = new DataSet();
                        da.Fill(ds, "Устройства");

                        NazvanieTxt.Text = "";
                        SkladGrid.ItemsSource = ds.Tables["Устройства"].DefaultView; // Заполнение грида нужной базой
                        con.Close();
                    
                }
                catch (FormatException)
                {
                    MessageBox.Show("Введите количество!");
                }
            }
        }

        private void txtCol_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            con = new OleDbConnection(@"Provider = Microsoft.ACE.Oledb.12.0; Data Source = ДопингБД.accdb"); // Создаем подключение к БД для этой функции
            cmd = new OleDbCommand(); // Создаем команду
            con.Open();


            string queary = "SELECT * FROM Устройства WHERE Тип = 'Блок'";
            OleDbDataAdapter com = new OleDbDataAdapter(queary, con);
            DataSet ds = new DataSet();
            com.Fill(ds, "Устройства");
            SkladGrid.ItemsSource = ds.Tables["Устройства"].DefaultView;
            con.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            con = new OleDbConnection(@"Provider = Microsoft.ACE.Oledb.12.0; Data Source = ДопингБД.accdb"); // Создаем подключение к БД для этой функции
            cmd = new OleDbCommand(); // Создаем команду
            con.Open();


            string queary = "SELECT * FROM Устройства WHERE Тип = 'Мышь'";
            OleDbDataAdapter com = new OleDbDataAdapter(queary, con);
            DataSet ds = new DataSet();
            com.Fill(ds, "Устройства");
            SkladGrid.ItemsSource = ds.Tables["Устройства"].DefaultView;
            con.Close();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            con = new OleDbConnection(@"Provider = Microsoft.ACE.Oledb.12.0; Data Source = ДопингБД.accdb"); // Создаем подключение к БД для этой функции
            cmd = new OleDbCommand(); // Создаем команду
            con.Open();


            string queary = "SELECT * FROM Устройства WHERE Тип = 'Клавиатура'";
            OleDbDataAdapter com = new OleDbDataAdapter(queary, con);
            DataSet ds = new DataSet();
            com.Fill(ds, "Устройства");
            SkladGrid.ItemsSource = ds.Tables["Устройства"].DefaultView;
            con.Close();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            con = new OleDbConnection(@"Provider = Microsoft.ACE.Oledb.12.0; Data Source = ДопингБД.accdb"); // Создаем подключение к БД для этой функции
            cmd = new OleDbCommand(); // Создаем команду
            con.Open();


            string queary = "SELECT * FROM Устройства WHERE Тип = 'Монитор'";
            OleDbDataAdapter com = new OleDbDataAdapter(queary, con);
            DataSet ds = new DataSet();
            com.Fill(ds, "Устройства");
            SkladGrid.ItemsSource = ds.Tables["Устройства"].DefaultView;
            con.Close();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            con = new OleDbConnection(@"Provider = Microsoft.ACE.Oledb.12.0; Data Source = ДопингБД.accdb"); // Создаем подключение к БД для этой функции
            cmd = new OleDbCommand(); // Создаем команду
            con.Open();


            string queary = "SELECT * FROM Устройства WHERE Тип = 'Бесперебойник'";
            OleDbDataAdapter com = new OleDbDataAdapter(queary, con);
            DataSet ds = new DataSet();
            com.Fill(ds, "Устройства");
            SkladGrid.ItemsSource = ds.Tables["Устройства"].DefaultView;
            con.Close();
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            con = new OleDbConnection(@"Provider = Microsoft.ACE.Oledb.12.0; Data Source = ДопингБД.accdb"); // Создаем подключение к БД для этой функции
            cmd = new OleDbCommand(); // Создаем команду
            con.Open();


            string queary = "SELECT * FROM Устройства";
            OleDbDataAdapter com = new OleDbDataAdapter(queary, con);
            DataSet ds = new DataSet();
            com.Fill(ds, "Устройства");
            SkladGrid.ItemsSource = ds.Tables["Устройства"].DefaultView;
            con.Close();
        }

        private void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtKod.Text == "")
            {
                txtName.Text = "";
                SostCombo2.SelectedIndex = -1;
            }
        }

        private void txtKod_TextChanged(object sender, TextChangedEventArgs e)
        {
            

            try
            {
                con = new OleDbConnection(@"Provider = Microsoft.ACE.Oledb.12.0; Data Source = ДопингБД.accdb"); // Создаем подключение к БД для этой функции
                cmd = new OleDbCommand(); // Создаем команду
                con.Open();

                int kod = Convert.ToInt32(txtKod.Text);


                string quary = $"SELECT Название FROM Устройства WHERE Код = {kod}";
                DataTable dt = new DataTable();
                OleDbDataAdapter da = new OleDbDataAdapter(quary, con);
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {


                    string quary3 = $"SELECT Название FROM Устройства WHERE Код = {kod}";
                    OleDbCommand cmd3 = new OleDbCommand(quary3, con);


                    string g = cmd3.ExecuteScalar().ToString();

                    if (g != null)
                    {


                        txtName.Text = g;

                        string quary2 = $"SELECT Состояние FROM Устройства WHERE Код = {kod}";
                        cmd = new OleDbCommand(quary2, con);
                        string g2 = cmd.ExecuteScalar().ToString();
                        

                        SostCombo2.Text = g2;



                    }


                }

            }
            catch (Exception ex)
            {

            }

            con.Close();
        }

        private void txtKod_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true;
            }
        }

        private void txtKod_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            con = new OleDbConnection(@"Provider = Microsoft.ACE.Oledb.12.0; Data Source = ДопингБД.accdb"); // Создаем подключение к БД для этой функции
            cmd = new OleDbCommand(); // Создаем команду
            con.Open();

            try
            {
                con = new OleDbConnection(@"Provider = Microsoft.ACE.Oledb.12.0; Data Source = ДопингБД.accdb"); // Создаем подключение к БД для этой функции
                cmd = new OleDbCommand(); // Создаем команду
                con.Open();

                int kod = Convert.ToInt32(txtKod.Text);


                string quary = $"SELECT Название FROM Устройства WHERE id = {kod}";
                DataTable dt = new DataTable();
                OleDbDataAdapter da = new OleDbDataAdapter(quary, con);
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {


                    string quary3 = $"SELECT Название FROM Устройства WHERE id = {kod}";
                    OleDbCommand cmd3 = new OleDbCommand(quary3, con);


                    string g = cmd3.ExecuteScalar().ToString();

                    if (g != null)
                    {


                        txtName.Text = g;

                        string quary2 = $"SELECT Состояние FROM Устройства WHERE id = {kod}";
                        cmd = new OleDbCommand(quary2, con);
                        string g2 = cmd.ExecuteScalar().ToString();


                        SostCombo2.Text = g2;



                    }


                }
                else
                {
                    txtName.Text = "";
                    SostCombo2.SelectedIndex = -1;
                }

            }
            catch (Exception ex)
            {

            }

            con.Close();
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            con = new OleDbConnection(@"Provider = Microsoft.ACE.Oledb.12.0; Data Source = ДопингБД.accdb"); // Создаем подключение к БД для этой функции
            cmd = new OleDbCommand(); // Создаем команду
            con.Open();

            try
            {
                int kod = Convert.ToInt32(txtKod.Text);

                string select = $"SELECT Тип FROM Устройства WHERE id = {kod}";
                DataTable dt = new DataTable();
                OleDbDataAdapter da = new OleDbDataAdapter(select, con);
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    OleDbCommand cmdS = new OleDbCommand(select, con);
                    string tip = cmdS.ExecuteScalar().ToString();

                    string quaryUp = $"UPDATE Комплект SET {tip} = 0 WHERE {tip} = {kod}";
                    OleDbCommand cmdU = new OleDbCommand(quaryUp, con);
                    cmdU.ExecuteNonQuery();

                    string quary = $"DELETE * FROM Устройства WHERE id = {kod}";
                    cmd = new OleDbCommand(quary, con);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Устройство удалено из базы");

                    da = new OleDbDataAdapter("select * from Устройства", con); // Представляет набор команд данных и подключение к базе данных, которые используются для заполнения DataSet и обновления источника данных.
                    ds = new DataSet();
                    da.Fill(ds, "Устройства");

                    SkladGrid.ItemsSource = ds.Tables["Устройства"].DefaultView; // Заполнение грида нужной базой
                    con.Close();
                }
                else
                {
                    MessageBox.Show("Устройства с таким id не найдено");
                    txtKod.Text = "";
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Введите код!");
                txtKod.Text = "";
            }
            con.Close();


        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            con = new OleDbConnection(@"Provider = Microsoft.ACE.Oledb.12.0; Data Source = ДопингБД.accdb"); // Создаем подключение к БД для этой функции
            cmd = new OleDbCommand(); // Создаем команду
            con.Open();

            try
            {
                string name = txtName.Text;

                if (name == "" && txtKod.Text != "")
                {
                    MessageBox.Show("Укажите код устройства,\nпо названию которого вы хотите удалить");
                    txtKod.Text = "";
                }
                else if (name == "" && txtKod.Text == "")
                {
                    MessageBox.Show("Введите код!");
                }
                else
                {

                    string select2 = $"SELECT DISTINCT Тип FROM Устройства WHERE Название = '{name}'";
                    OleDbCommand cmdS2 = new OleDbCommand(select2, con);
                    string tip = cmdS2.ExecuteScalar().ToString();

                    string quaryUp = $"UPDATE Комплект SET {tip} = 0 WHERE {tip} IN (SELECT id FROM Устройства WHERE Название = '{name}')";
                    OleDbCommand cmdU = new OleDbCommand(quaryUp, con);
                    cmdU.ExecuteNonQuery();

                    string quary = $"DELETE * FROM Устройства WHERE Название = '{name}'";
                    cmd = new OleDbCommand(quary, con);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Устройства удалены из базы");

                    da = new OleDbDataAdapter("select * from Устройства", con); // Представляет набор команд данных и подключение к базе данных, которые используются для заполнения DataSet и обновления источника данных.
                    ds = new DataSet();
                    da.Fill(ds, "Устройства");

                    SkladGrid.ItemsSource = ds.Tables["Устройства"].DefaultView; // Заполнение грида нужной базой
                    con.Close();
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Введите код!");
                txtKod.Text = "";
            }
            con.Close();
        }

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            con = new OleDbConnection(@"Provider = Microsoft.ACE.Oledb.12.0; Data Source = ДопингБД.accdb"); // Создаем подключение к БД для этой функции
            cmd = new OleDbCommand(); // Создаем команду
            con.Open();

            

            try
            {
                int kod = Convert.ToInt32(txtKod.Text);
                string sost = SostCombo2.Text;

                if (kod == 0)
                {
                    MessageBox.Show("Введите код!");
                }
                else if (kod != 0 && txtName.Text == "")
                {
                    MessageBox.Show("Такой записи нет!");
                    txtKod.Text = "";
                }
                else
                {

                    string select2 = $"SELECT Состояние FROM Устройства WHERE id = {kod}";
                    OleDbCommand cmdSel = new OleDbCommand(select2, con);
                    string tip2 = cmdSel.ExecuteScalar().ToString();

                    if (tip2 == sost)
                    {
                        MessageBox.Show($"Данное устройство уже в састоянии '{tip2}'");
                    }
                    else
                    {
                        string select = $"SELECT Тип FROM Устройства WHERE id = {kod}";
                        OleDbCommand cmdS = new OleDbCommand(select, con);
                        string tip = cmdS.ExecuteScalar().ToString();

                        string quaryUp = $"UPDATE Комплект SET {tip} = 0 WHERE {tip} = {kod}";
                        OleDbCommand cmdU = new OleDbCommand(quaryUp, con);
                        cmdU.ExecuteNonQuery();

                        string quaryUp2 = $"UPDATE Устройства SET Состояние = '{sost}' WHERE id = {kod}";
                        OleDbCommand cmdU2 = new OleDbCommand(quaryUp2, con);
                        cmdU2.ExecuteNonQuery();

                        MessageBox.Show("Состояние устройства изменено");

                        da = new OleDbDataAdapter("select * from Устройства", con); // Представляет набор команд данных и подключение к базе данных, которые используются для заполнения DataSet и обновления источника данных.
                        ds = new DataSet();
                        da.Fill(ds, "Устройства");

                        SkladGrid.ItemsSource = ds.Tables["Устройства"].DefaultView; // Заполнение грида нужной базой
                        con.Close();
                    }
                }
                 
            }
            catch (FormatException)
            {
                MessageBox.Show("Введите код!");
                txtKod.Text = "";
            }
            con.Close() ;
        }
    }
}
