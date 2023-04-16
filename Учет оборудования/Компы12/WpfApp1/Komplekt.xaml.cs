using System;
using System.Collections.Generic;
using System.Data.Common;
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
using System.Data.SqlClient;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для Komplekt.xaml
    /// </summary>
    public partial class Komplekt : Window
    {
        private OleDbConnection myConnection;
        public static string connectString = @"Provider = Microsoft.ACE.Oledb.12.0; Data Source = ДопингБД.accdb";  // Подключаем БД

        OleDbConnection con;  // Задаем осн. команды для использования
        OleDbCommand cmd, cmd2, cmd3, cmd4, cmd5;
        OleDbDataReader dr;
        OleDbDataAdapter da;
        DataSet ds;

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            con = new OleDbConnection(@"Provider = Microsoft.ACE.Oledb.12.0; Data Source = ДопингБД.accdb"); // Создаем подключение к БД для этой функции
            cmd = new OleDbCommand(); // Создаем команду
            con.Open(); // Открываем соединение с базой

            string naz = txtName.Text;

            string coun = $"SELECT COUNT(*) FROM Комплект WHERE Название = '{naz}'";
            OleDbCommand cmdCoun = new OleDbCommand(coun, con);
            int col1 = Convert.ToInt32(cmdCoun.ExecuteScalar());

            if (col1 > 0)
            {
                MessageBox.Show("Комплект с таким названием уже существует!");
                return;
            }

            if (naz == "")
            {
                MessageBox.Show("Введите название комплекта!");
            }

            else
            {

            
            string mouse = comMouse.Text.ToString();
            string key = comKey.Text.ToString();
            string mon = comMon.Text.ToString();
            string block = comBlock.Text.ToString();
            string bes = comBes.Text.ToString();
            try
            {

                int col = Convert.ToInt32(txtCol.Text);

                string quary1 = $"SELECT COUNT(*) FROM Устройства WHERE Название = '{mouse}' AND Состояние = 'Работает' AND [Номер комплекта] = 0";
                cmd = new OleDbCommand(quary1, con);
                int mouseCol = Convert.ToInt32(cmd.ExecuteScalar().ToString());

                string quary2 = $"SELECT COUNT(*) FROM Устройства WHERE Название = '{key}' AND Состояние = 'Работает' AND [Номер комплекта] = 0";
                cmd2 = new OleDbCommand(quary2, con);
                int keyCol = Convert.ToInt32(cmd2.ExecuteScalar().ToString());

                string quary3 = $"SELECT COUNT(*) FROM Устройства WHERE Название = '{mon}' AND Состояние = 'Работает' AND [Номер комплекта] = 0";
                cmd3 = new OleDbCommand(quary3, con);
                int monCol = Convert.ToInt32(cmd3.ExecuteScalar().ToString());

                string quary4 = $"SELECT COUNT(*) FROM Устройства WHERE Название = '{block}' AND Состояние = 'Работает' AND [Номер комплекта] = 0";
                cmd4 = new OleDbCommand(quary4, con);
                int blockCol = Convert.ToInt32(cmd4.ExecuteScalar().ToString());

                string quary5 = $"SELECT COUNT(*) FROM Устройства WHERE Название = '{bes}' AND Состояние = 'Работает' AND [Номер комплекта] = 0";
                cmd5 = new OleDbCommand(quary5, con);
                int besCol = Convert.ToInt32(cmd5.ExecuteScalar().ToString());

                int[] array = new int[] { mouseCol, keyCol, monCol, blockCol, besCol };
                int minCol = array.Min();

                if (mouseCol == 0 || keyCol == 0 || monCol == 0 || blockCol == 0 || besCol == 0)
                {
                    MessageBox.Show("Нет свободных устройств для полного комплекта");
                }

                else if (mouseCol < col || keyCol < col || monCol < col || blockCol < col || besCol < col)
                {
                    for (int i = 0; i < minCol; i++)
                    {
                        string z1 = $"SELECT MIN(id) FROM Устройства WHERE Название = '{mouse}' AND Состояние = 'Работает' AND [Номер комплекта] = 0";
                        string z2 = $"SELECT MIN(id) FROM Устройства WHERE Название = '{key}' AND Состояние = 'Работает' AND [Номер комплекта] = 0";
                        string z3 = $"SELECT MIN(id) FROM Устройства WHERE Название = '{mon}' AND Состояние = 'Работает' AND [Номер комплекта] = 0";
                        string z4 = $"SELECT MIN(id) FROM Устройства WHERE Название = '{block}' AND Состояние = 'Работает' AND [Номер комплекта] = 0";
                        string z5 = $"SELECT MIN(id) FROM Устройства WHERE Название = '{bes}' AND Состояние = 'Работает' AND [Номер комплекта] = 0";

                        OleDbCommand cm1 = new OleDbCommand(z1, con);
                        OleDbCommand cm2 = new OleDbCommand(z2, con);
                        OleDbCommand cm3 = new OleDbCommand(z3, con);
                        OleDbCommand cm4 = new OleDbCommand(z4, con);
                        OleDbCommand cm5 = new OleDbCommand(z5, con);

                        int MOUSE = Convert.ToInt32(cm1.ExecuteScalar());
                        int KEY = Convert.ToInt32(cm2.ExecuteScalar());
                        int MON = Convert.ToInt32(cm3.ExecuteScalar());
                        int BLOCK = Convert.ToInt32(cm4.ExecuteScalar());
                        int BES = Convert.ToInt32(cm5.ExecuteScalar());

                        string quary = $"INSERT INTO Комплект (Название, Блок, Мышь, Клавиатура, Монитор, Бесперебойник, Класс) VALUES ('{naz}',{BLOCK},{MOUSE},{KEY},{MON},{BES}, '-')";
                        OleDbCommand cmINS = new OleDbCommand(quary, con);
                        cmINS.ExecuteNonQuery();

                        string SelUp = $"SELECT MAX(Код) FROM Комплект";
                        OleDbCommand selup = new OleDbCommand(SelUp, con);
                        int id = Convert.ToInt32(selup.ExecuteScalar());

                        string update = $"UPDATE Устройства SET [Номер комплекта] = {id} WHERE id IN({BLOCK},{MOUSE},{BES},{MON},{KEY})";
                        OleDbCommand UPD = new OleDbCommand(update, con);
                        UPD.ExecuteNonQuery();

                    }
                        Komplect();
                        ComboKomplect();
                    da = new OleDbDataAdapter("select * from Комплект", con); // Представляет набор команд данных и подключение к базе данных, которые используются для заполнения DataSet и обновления источника данных.
                    ds = new DataSet();
                    da.Fill(ds, "Комплект");

                    KomplektGrid.ItemsSource = ds.Tables["Комплект"].DefaultView; // Заполнение грида нужной базой


                    MessageBox.Show($"Было создано {minCol} комплектов из {col} желаемых!");

                    
                }

                else if (mouseCol >= col && keyCol >= col && monCol >= col && blockCol >= col && besCol >= col)
                {
                    for (int i = 0; i < col; i++)
                    {
                        string z1 = $"SELECT MIN(id) FROM Устройства WHERE Название = '{mouse}' AND Состояние = 'Работает' AND [Номер комплекта] = 0";
                        string z2 = $"SELECT MIN(id) FROM Устройства WHERE Название = '{key}' AND Состояние = 'Работает' AND [Номер комплекта] = 0";
                        string z3 = $"SELECT MIN(id) FROM Устройства WHERE Название = '{mon}' AND Состояние = 'Работает' AND [Номер комплекта] = 0";
                        string z4 = $"SELECT MIN(id) FROM Устройства WHERE Название = '{block}' AND Состояние = 'Работает' AND [Номер комплекта] = 0";
                        string z5 = $"SELECT MIN(id) FROM Устройства WHERE Название = '{bes}' AND Состояние = 'Работает' AND [Номер комплекта] = 0";

                        OleDbCommand cm1 = new OleDbCommand(z1, con);
                        OleDbCommand cm2 = new OleDbCommand(z2, con);
                        OleDbCommand cm3 = new OleDbCommand(z3, con);
                        OleDbCommand cm4 = new OleDbCommand(z4, con);
                        OleDbCommand cm5 = new OleDbCommand(z5, con);

                        int MOUSE = Convert.ToInt32(cm1.ExecuteScalar());
                        int KEY = Convert.ToInt32(cm2.ExecuteScalar());
                        int MON = Convert.ToInt32(cm3.ExecuteScalar());
                        int BLOCK = Convert.ToInt32(cm4.ExecuteScalar());
                        int BES = Convert.ToInt32(cm5.ExecuteScalar());

                        string quary = $"INSERT INTO Комплект (Название, Блок, Мышь, Клавиатура, Монитор, Бесперебойник, Класс) VALUES ('{naz}',{BLOCK},{MOUSE},{KEY},{MON},{BES}, '-')";
                        OleDbCommand cmINS = new OleDbCommand(quary, con);
                        cmINS.ExecuteNonQuery();

                        string SelUp = $"SELECT MAX(Код) FROM Комплект";
                        OleDbCommand selup = new OleDbCommand(SelUp, con);
                        int id = Convert.ToInt32(selup.ExecuteScalar());

                        string update = $"UPDATE Устройства SET [Номер комплекта] = {id} WHERE id IN({BLOCK},{MOUSE},{BES},{MON},{KEY})";
                        OleDbCommand UPD = new OleDbCommand(update, con);
                        UPD.ExecuteNonQuery();

                    }
                        Komplect();
                        ComboKomplect();
                    da = new OleDbDataAdapter("select * from Комплект", con); // Представляет набор команд данных и подключение к базе данных, которые используются для заполнения DataSet и обновления источника данных.
                    ds = new DataSet();
                    da.Fill(ds, "Комплект");

                    KomplektGrid.ItemsSource = ds.Tables["Комплект"].DefaultView; // Заполнение грида нужной базой


                    MessageBox.Show($"Было создано {col} комплектов!");

                    string quaryy = "Select MAX(Код) FROM Комплект";
                    OleDbCommand cmd = new OleDbCommand(quaryy, con);
                    int kod = (Convert.ToInt32(cmd.ExecuteScalar()));
                    string quary6 = $"SELECT Название FROM Комплект WHERE Код = {kod}";
                    OleDbCommand cmd6 = new OleDbCommand(quary6, con);
                    OleDbDataReader dr6 = cmd6.ExecuteReader();

                    

                    con.Close();
                }



            }
            catch (FormatException)
            {
                MessageBox.Show("Введите количество комплектов!");
            }
            }
        }

        public Komplekt()
        {
            InitializeComponent();
            con = new OleDbConnection(@"Provider = Microsoft.ACE.Oledb.12.0; Data Source = ДопингБД.accdb"); // Создаем подключение к БД для этой функции
            cmd = new OleDbCommand(); // Создаем команду
            con.Open();


            ComboName();
            ComboKomplect();
            comName.SelectedIndex = 0;
            comKomplekt.SelectedIndex = 0;
            txtCol.MaxLength = 5;
            comKomplect.SelectedIndex = 0;
            comMouse.SelectedIndex = 0;
            comMon.SelectedIndex = 0;
            comKey.SelectedIndex = 0;
            comBlock.SelectedIndex = 0;
            comBes.SelectedIndex = 0;

            con = new OleDbConnection(@"Provider = Microsoft.ACE.Oledb.12.0; Data Source = ДопингБД.accdb"); // Создаем подключение к БД для этой функции
            cmd = new OleDbCommand(); // Создаем команду
            con.Open(); // Открываем соединение с базой

            da = new OleDbDataAdapter("select * from Комплект", con); // Представляет набор команд данных и подключение к базе данных, которые используются для заполнения DataSet и обновления источника данных.
            ds = new DataSet();
            da.Fill(ds, "Комплект");

            KomplektGrid.ItemsSource = ds.Tables["Комплект"].DefaultView; // Заполнение грида нужной базой
            con.Close();
         
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            con = new OleDbConnection(@"Provider = Microsoft.ACE.Oledb.12.0; Data Source = ДопингБД.accdb"); // Создаем подключение к БД для этой функции
            cmd = new OleDbCommand(); // Создаем команду
            con.Open();

            string kom = comKomplect.Text;

            string upQuary = $"UPDATE Устройства SET [Номер комплекта] = 0 WHERE [Номер комплекта] IN (SELECT Код FROM Комплект WHERE Название = '{kom}')";
            string quaryUp = $"UPdate Кабинеты SET Комплект = '-', Колличество = 0 WHERE Комплект = '{kom}'";

            OleDbCommand cmd3 = new OleDbCommand(quaryUp, con);
            cmd3.ExecuteNonQuery();
            cmd2 = new OleDbCommand(upQuary, con);
            cmd2.ExecuteNonQuery();

            

            string Quary = $"DEleTE * FROM Комплект WHERE Название = '{kom}'";
            cmd = new OleDbCommand(Quary, con);
            cmd.ExecuteNonQuery();

            

            da = new OleDbDataAdapter("select * from Комплект", con); // Представляет набор команд данных и подключение к базе данных, которые используются для заполнения DataSet и обновления источника данных.
            ds = new DataSet();
            da.Fill(ds, "Комплект");

            KomplektGrid.ItemsSource = ds.Tables["Комплект"].DefaultView; // Заполнение грида нужной базой
            

            MessageBox.Show($"Комплекты с названием {kom} были удалены!");
            ComboKomplect();

            comKomplect.SelectedIndex = 0;

            con.Close();
        }

        private void txtCol_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true;
            }
        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true;
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            

            

            try
            {
                con = new OleDbConnection(@"Provider = Microsoft.ACE.Oledb.12.0; Data Source = ДопингБД.accdb"); // Создаем подключение к БД для этой функции
                cmd = new OleDbCommand(); // Создаем команду
                con.Open();

                int kom = Convert.ToInt32(txtKomplect.Text);

                string qqq = "SELECT MAX(Код) FROM Комплект";
                OleDbCommand cmdq = new OleDbCommand(qqq, con);
                int mach = Convert.ToInt32(cmdq.ExecuteScalar());

                if (kom > 0 && kom <= mach)
                {
                    string upQuary = $"UPDATE Устройства SET [Номер комплекта] = 0 WHERE [Номер комплекта]  = {kom}";

                    cmd2 = new OleDbCommand(upQuary, con);
                    cmd2.ExecuteNonQuery();

                    //--------------------------------------------------------------------------------------------------------------------
                    string select1 = $"SELECT Класс FROM Комплект WHERE Код = {kom}";
                    OleDbCommand cmdS1 = new OleDbCommand(select1, con);
                    string kod = cmdS1.ExecuteScalar().ToString();

                   /* string select2 = $"SELECT Код FROM Кабинеты WHERE Колличество = (SELECT MAX(Колличество) FROM Кабинеты WHERE Комплект = '{naz}') AND Комплект = '{naz}'";
                    OleDbCommand cmdS2 = new OleDbCommand(select2, con);
                    string kod = cmdS2.ExecuteScalar().ToString(); */

                    //string select = $"SELECT Класс FROM Комплект WHERE Код = {kom}";
                    string update = $"UPDATE Кабинеты SET Колличество = Колличество - 1 WHERE [Номер кабинета] = '{kod}' ";
                    OleDbCommand cmdU = new OleDbCommand(update, con);
                    cmdU.ExecuteNonQuery();

                    //--------------------------------------------------------------------------------------------------------------------



                    string Quary = $"DEleTE * FROM Комплект WHERE Код = {kom}";
                    cmd = new OleDbCommand(Quary, con);
                    cmd.ExecuteNonQuery();

                    da = new OleDbDataAdapter("select * from Комплект", con); // Представляет набор команд данных и подключение к базе данных, которые используются для заполнения DataSet и обновления источника данных.
                    ds = new DataSet();
                    da.Fill(ds, "Комплект");

                    KomplektGrid.ItemsSource = ds.Tables["Комплект"].DefaultView; // Заполнение грида нужной базой
                    

                    MessageBox.Show($"Комплекты с названием {kom} были удалены!");
                    Komplect();

                    comKomplect.SelectedIndex = 0;

                    con.Close();
                }
                else
                {
                    MessageBox.Show("Такого комплекта не существует!");
                }
                }
            catch (FormatException)
            {
                MessageBox.Show("введите код комплекта!");
            }
            }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            con = new OleDbConnection(@"Provider = Microsoft.ACE.Oledb.12.0; Data Source = ДопингБД.accdb"); // Создаем подключение к БД для этой функции
            cmd = new OleDbCommand(); // Создаем команду
            con.Open();

            
            string komplect = comKomplekt.Text;
            string nom = comName.Text;

            if (comKomplekt.Items.Count == 0)
            {
                MessageBox.Show("Нет свободных комплектов!");


                if (comName.Items.Count == 0)
                {
                    MessageBox.Show("Классы уже укомплектованы");
                }

            }
            else
            {

                try
                {
                    int kol = Convert.ToInt32(txtKol.Text);

                    if (kol == 0)
                    {
                        MessageBox.Show("Нелзя добавить 0 комплектов!");
                    }
                    else
                    {
                        string quary = $"SELECT COUNT(*) FROM Комплект WHERE Название = '{komplect}' AND Класс = '-'";
                        cmd = new OleDbCommand(quary, con);
                        int result = Convert.ToInt32(cmd.ExecuteScalar());

                        if (result < kol)
                        {
                            MessageBox.Show($"Вам не хватает комплектов для добавления!\nВсего есть {result} комплектов '{komplect}'");
                            txtKol.Text = "";
                        }
                        else
                        {

                            for (int i = 1; i <= kol; i++)
                            {
                                string quaryS = $"SELECT MIN(Код) FROM Комплект WHERE Название = '{komplect}' AND Класс = '-'";
                                OleDbCommand cmdS = new OleDbCommand(quaryS, con);
                                int MinKod = Convert.ToInt32(cmdS.ExecuteScalar());

                                string upKom = $"UPDATE Комплект SET Класс = '{nom}' WHERE Код = {MinKod}";
                                OleDbCommand cmdUp = new OleDbCommand(upKom, con);
                                cmdUp.ExecuteNonQuery();

                            }
                            
                            string upClass = $"UPDATE Кабинеты SET Комплект = '{komplect}', Колличество = {kol} WHERE [Номер кабинета] = '{nom}'";

                            OleDbCommand cmdUP2 = new OleDbCommand(upClass, con);

                            cmdUP2.ExecuteNonQuery();

                            MessageBox.Show("Комплекты добавлены в кабинет");

                            da = new OleDbDataAdapter("select * from Комплект", con); // Представляет набор команд данных и подключение к базе данных, которые используются для заполнения DataSet и обновления источника данных.
                            ds = new DataSet();
                            da.Fill(ds, "Комплект");

                            KomplektGrid.ItemsSource = ds.Tables["Комплект"].DefaultView; // Заполнение грида нужной базой

                            ComboName();
                            ComboKomplect();

                            con.Close();

                            
                        }
                    }

                }
                catch (FormatException)
                {
                    MessageBox.Show("Введите число комплектов!");
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {           
            this.Close();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtKol_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true;
            }
        }

        private void Button_Click_10(object sender, RoutedEventArgs e)
        {
            ОтчетКомплект otchet = new ОтчетКомплект();
            otchet.Show();
        }

        private void Window_Initialized(object sender, EventArgs e)
        {

            con = new OleDbConnection(@"Provider = Microsoft.ACE.Oledb.12.0; Data Source = ДопингБД.accdb"); // Создаем подключение к БД для этой функции
            cmd = new OleDbCommand(); // Создаем команду
            con.Open(); // Открываем соединение с базой

            string quary = "SELECT DISTINCT(Название) FROM Устройства WHERE Тип = 'Мышь'";
            cmd = new OleDbCommand(quary, con);
            OleDbDataReader dr = cmd.ExecuteReader(); //Считывает таблицу

            OleDbDataAdapter da = new OleDbDataAdapter(quary, con);

            DataTable dt = new DataTable(); //Создаем таблицу (виртуальную,  всне базы)
            da.Fill(dt); //Заполняет таблицу запросом из da

            while (dr.Read()) //Пробегается по всей таблице
            {

                comMouse.Items.Add(dr[0].ToString()); //Создает елементы из таблицы, название элементов из "Название"

            }
            dr.Close();

            //---------------------------------------------------------------------------------------------------------------------------------------------------

            string quary2 = "SELECT DISTINCT(Название) FROM Устройства WHERE Тип = 'Клавиатура'";
            cmd2 = new OleDbCommand(quary2, con);
            OleDbDataReader dr2 = cmd2.ExecuteReader(); //Считывает таблицу

            OleDbDataAdapter da2 = new OleDbDataAdapter(quary2, con);

            DataTable dt2 = new DataTable(); //Создаем таблицу (виртуальную,  всне базы)
            da2.Fill(dt2); //Заполняет таблицу запросом из da

            while (dr2.Read()) //Пробегается по всей таблице
            {

                comKey.Items.Add(dr2[0].ToString()); //Создает елементы из таблицы, название элементов из "Название"

            }
            dr2.Close();

            //---------------------------------------------------------------------------------------------------------------------------------------------------

            string quary3 = "SELECT DISTINCT(Название) FROM Устройства WHERE Тип = 'Монитор'";
            cmd3 = new OleDbCommand(quary3, con);
            OleDbDataReader dr3 = cmd3.ExecuteReader(); //Считывает таблицу

            OleDbDataAdapter da3 = new OleDbDataAdapter(quary3, con);

            DataTable dt3 = new DataTable(); //Создаем таблицу (виртуальную,  всне базы)
            da3.Fill(dt3); //Заполняет таблицу запросом из da

            while (dr3.Read()) //Пробегается по всей таблице
            {

                comMon.Items.Add(dr3[0].ToString()); //Создает елементы из таблицы, название элементов из "Название"

            }
            dr3.Close();

            //---------------------------------------------------------------------------------------------------------------------------------------------------

            string quary4 = "SELECT DISTINCT(Название) FROM Устройства WHERE Тип = 'Блок'";
            cmd4 = new OleDbCommand(quary4, con);
            OleDbDataReader dr4 = cmd4.ExecuteReader(); //Считывает таблицу

            OleDbDataAdapter da4 = new OleDbDataAdapter(quary4, con);

            DataTable dt4 = new DataTable(); //Создаем таблицу (виртуальную,  всне базы)
            da4.Fill(dt4); //Заполняет таблицу запросом из da

            while (dr4.Read()) //Пробегается по всей таблице
            {

                comBlock.Items.Add(dr4[0].ToString()); //Создает елементы из таблицы, название элементов из "Название"

            }
            dr4.Close();

            //---------------------------------------------------------------------------------------------------------------------------------------------------

            string quary5 = "SELECT DISTINCT(Название) FROM Устройства WHERE Тип = 'Бесперебойник'";
            cmd5 = new OleDbCommand(quary5, con);
            OleDbDataReader dr5 = cmd5.ExecuteReader(); //Считывает таблицу

            OleDbDataAdapter da5 = new OleDbDataAdapter(quary5, con);

            DataTable dt5 = new DataTable(); //Создаем таблицу (виртуальную,  всне базы)
            da5.Fill(dt5); //Заполняет таблицу запросом из da

            while (dr5.Read()) //Пробегается по всей таблице
            {

                comBes.Items.Add(dr5[0].ToString()); //Создает елементы из таблицы, название элементов из "Название"

            }
            dr.Close();

            //---------------------------------------------------------------------------------------------------------------------------------------------------

            string quary6 = "SELECT DISTINCT(Название) FROM Комплект";
            OleDbCommand cmd6 = new OleDbCommand(quary6, con);
            OleDbDataReader dr6 = cmd6.ExecuteReader();

            while (dr6.Read())
            {
                comKomplect.Items.Add(dr6[0].ToString());
            }
            dr.Close();

            con.Close();
        }
        public void Komplect()
        {
            con = new OleDbConnection(@"Provider = Microsoft.ACE.Oledb.12.0; Data Source = ДопингБД.accdb"); // Создаем подключение к БД для этой функции
            cmd = new OleDbCommand(); // Создаем команду
            con.Open();

            comKomplect.Items.Clear();
            string quary6 = "SELECT DISTINCT(Название) FROM Комплект";
            OleDbCommand cmd6 = new OleDbCommand(quary6, con);
            OleDbDataReader dr6 = cmd6.ExecuteReader();

            while (dr6.Read())
            {
                comKomplect.Items.Add(dr6[0].ToString());
            }
            dr6.Close();

            comKomplect.SelectedIndex = 0;
            
        }

        private void ComboName()
        {
            comName.Items.Clear();
            string quary6 = "SELECT DISTINCT([Номер кабинета]) FROM Кабинеты WHERE Колличество = 0";
            OleDbCommand cmd6 = new OleDbCommand(quary6, con);
            OleDbDataReader dr6 = cmd6.ExecuteReader();

            while (dr6.Read())
            {
                comName.Items.Add(dr6[0].ToString());
            }
            dr6.Close();

            comName.SelectedIndex = 0;
        }

        private void ComboKomplect()
        {
            comKomplekt.Items.Clear();
            string quary6 = "SELECT DISTINCT(Название) FROM Комплект WHERE Класс = '-'";
            OleDbCommand cmd6 = new OleDbCommand(quary6, con);
            OleDbDataReader dr6 = cmd6.ExecuteReader();

            while (dr6.Read())
            {
                comKomplekt.Items.Add(dr6[0].ToString());
            }
            dr6.Close();

            comKomplekt.SelectedIndex = 0;
        }
    }
}
