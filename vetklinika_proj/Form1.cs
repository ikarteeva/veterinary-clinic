using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using System.Globalization;



namespace vetklinika
{
    public partial class Form1 : MetroFramework.Forms.MetroForm
    {
        public Form1()
        {
            InitializeComponent();
            this.StyleManager = metroStyleManager1;
        }

        private void metroTextBox6_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {


        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void metroTile6_Click(object sender, EventArgs e)
        {
            using (StreamReader fs = new StreamReader("bazadan.json", encoding: Encoding.Default))
            {
                string ser = fs.ReadToEnd();
                dataGridView2.Rows.Clear();
                database[] newdata = JsonConvert.DeserializeObject<database[]>(ser);

                int r = 0;

                foreach (var p in newdata)
                {

                    foreach (var x in p.pers)
                    {
                        if (x.specialization == 0 & x.exist)
                        {
                            dataGridView2.Rows.Add();
                                    dataGridView2[0, r].Value = x.name;
                                    r++;
                            }
                        }

                    }
                }
            }



        private void metroTile9_Click(object sender, EventArgs e)
        {
            metroComboBox4.Items.Clear();
            metroComboBox5.Items.Clear();
            metroComboBox6.Items.Clear();
            metroComboBox10.Items.Clear();
            dataGridView1.Rows.Clear();

            metroComboBox9.Items.Add(10);
            metroComboBox9.Items.Add(20);
            metroComboBox9.Items.Add(30);

            List<application> appl = new List<application>();
            List<pets> petl = new List<pets>();
            List<persons> perl = new List<persons>();

            using (StreamReader fs = new StreamReader("bazadan.json", encoding: Encoding.Default))
            {

               string ser = fs.ReadToEnd();

                database[] newdata = JsonConvert.DeserializeObject<database[]>(ser);

                int r = 0;

                foreach (var p in newdata)
                {

                    foreach (var x in p.pers)
                    {
                        perl.Add(x);
                        Zayavka newForm = new Zayavka();
                        string spec = "1";


                        if (x.specialization == 0 & x.exist)
                        {
                            spec = "Клиент";
                            metroComboBox4.Items.Add(x.name);
                            metroComboBox10.Items.Add(x.name);
                        }

                        if (x.specialization == 0)
                        {
                            foreach (var y in p.app)
                            {
                                appl.Add(y);
                                if (y.name != null)
                                {
                                    dataGridView1.Rows.Add();
                                    dataGridView1[0, r].Value = y.name;
                                    dataGridView1[1, r].Value = x.name;
                                    dataGridView1[2, r].Value = y.namepet;
                                    dataGridView1[3, r].Value = y.namevet;
                                    dataGridView1[4, r].Value = y.date;
                                    dataGridView1[5, r].Value = y.time;
                                    dataGridView1[6, r].Value = y.count;
                                    dataGridView1[7, r].Value = y.cabinet;
                                    r++;
                                }
                            }
                        }

                        if (x.specialization > 0 & x.exist)
                        {
                            metroComboBox5.Items.Add(x.name);
                        }

                    }




                    foreach (var s in p.pet)
                    {
                        petl.Add(s);

                        if (s.name == "0" | s.name == null)
                        { }
                        else
                        {
                            metroComboBox6.Items.Add(s.name);
                        }

                    }

                }
            }



        }

        private void metroTile2_Click(object sender, EventArgs e)
        {
            Zayavka newForm = new Zayavka();
            newForm.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void metroComboBox9_SelectedIndexChanged(object sender, EventArgs e)
        {
            int timeprom = Convert.ToInt32(metroComboBox9.SelectedItem.ToString());
            using (StreamWriter st = new StreamWriter("settings.txt", false, System.Text.Encoding.Default))
            {
                st.WriteLine(timeprom);

            }

        }

        private void metroButton3_Click(object sender, EventArgs e)
        {
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
        }

        public int spec = 0;

        private void metroButton2_Click(object sender, EventArgs e)
        {
        }

        private void metroComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void metroButton4_Click(object sender, EventArgs e)
        {
            Delete();
            MessageBox.Show("Выполнено!");
            using (StreamReader fs = new StreamReader("bazadan.json", encoding: Encoding.Default))
            {
                string ser = fs.ReadToEnd();
                dataGridView7.Rows.Clear();
                database[] newdata = JsonConvert.DeserializeObject<database[]>(ser);

                int r = 0;

                foreach (var p in newdata)
                {

                    foreach (var x in p.pers)
                    {
                        if (x.specialization == 0 & x.exist)
                        {
                            dataGridView7.Rows.Add();
                            dataGridView7[0, r].Value = x.name;
                            r++;
                        }
                    }

                }
            }
        }

        private void metroButton5_Click(object sender, EventArgs e)
        {
            string ser;
            database[] newdata;

            using (StreamReader fs = new StreamReader("bazadan.json", encoding: Encoding.Default))
            {
                ser = fs.ReadToEnd();
                newdata = JsonConvert.DeserializeObject<database[]>(ser);
                int opa = newdata.Length;

                foreach (var p in newdata)
                {
                    foreach (var x in p.pers)
                    {
                        if (x.name == metroComboBox5.SelectedItem.ToString())
                        {
                            x.exist = false;
                        }
                    }
                }
            }

            string serialized = JsonConvert.SerializeObject(newdata);

            using (StreamWriter sw = new StreamWriter("bazadan.json", false, System.Text.Encoding.Default))
            {
                sw.WriteLine(serialized);
            }

            using (StreamReader fs = new StreamReader("bazadan.json", encoding: Encoding.Default))
            {
                string ser1 = fs.ReadToEnd();
                dataGridView6.Rows.Clear();
                database[] newdata1 = JsonConvert.DeserializeObject<database[]>(ser1);

                int r = 0;

                foreach (var p in newdata1)
                {

                    foreach (var x in p.pers)
                    {
                        if (x.specialization != 0 & x.exist)
                        {

                            dataGridView6.Rows.Add();
                            dataGridView6[0, r].Value = x.name;
                            r++;
                        }
                    }

                }
            }

            MessageBox.Show("Выполнено!");
        }


        private void Delete()
        {
            string ser;
            database[] newdata;

            using (StreamReader fs = new StreamReader("bazadan.json", encoding: Encoding.Default))
            {
                ser = fs.ReadToEnd();
                newdata = JsonConvert.DeserializeObject<database[]>(ser);
                int opa = newdata.Length;

                foreach (var p in newdata)
                {
                    foreach (var x in p.pers)
                    {
                        if (x.name == metroComboBox4.SelectedItem.ToString())
                        {
                            x.exist = false;
                        }
                    }
                }
            }

            string serialized = JsonConvert.SerializeObject(newdata);

            using (StreamWriter sw = new StreamWriter("bazadan.json", false, System.Text.Encoding.Default))
            {
                sw.WriteLine(serialized);
            }
        }

        private void metroButton6_Click(object sender, EventArgs e)
        {
            string ser;
            database[] newdata;

            using (StreamReader fs = new StreamReader("bazadan.json", encoding: Encoding.Default))
            {
                ser = fs.ReadToEnd();
                newdata = JsonConvert.DeserializeObject<database[]>(ser);
                int opa = newdata.Length;

                foreach (var p in newdata)
                {
                    foreach (var x in p.pet)
                    {
                        if (x.name == metroComboBox6.SelectedItem.ToString())
                        {
                            x.name = "0";
                        }
                    }
                }
            }

            string serialized = JsonConvert.SerializeObject(newdata);

            using (StreamWriter sw = new StreamWriter("bazadan.json", false, System.Text.Encoding.Default))
            {
                sw.WriteLine(serialized);
            }

            using (StreamReader fs = new StreamReader("bazadan.json", encoding: Encoding.Default))
            {
                string ser1 = fs.ReadToEnd();
                dataGridView5.Rows.Clear();
                database[] newdata1 = JsonConvert.DeserializeObject<database[]>(ser1);

                int r = 0;

                foreach (var p in newdata1)
                {

                    foreach (var x in p.pet)
                    {
                        if (x.name != "0")
                        {

                            dataGridView5.Rows.Add();
                            dataGridView5[0, r].Value = x.name;
                            r++;
                        }
                    }

                }
            }

            MessageBox.Show("Выполнено!");

        }

        private void metroTile4_Click(object sender, EventArgs e)
        {
            Deleter newForm = new Deleter();
            newForm.Show();
        }

        private void metroComboBox10_SelectedIndexChanged(object sender, EventArgs e)
        {
            int client = metroComboBox10.SelectedIndex;
            using (StreamWriter st = new StreamWriter("settings2.txt", false, System.Text.Encoding.Default))
            {
                st.WriteLine(client);

            }


            metroComboBox4.SelectedIndex = metroComboBox10.SelectedIndex;

        }

        private void metroComboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch(metroComboBox7.SelectedIndex)
            {
                case 0:
                    metroStyleManager1.Theme = MetroFramework.MetroThemeStyle.Dark;
                    break;
                case 1:
                    metroStyleManager1.Theme = MetroFramework.MetroThemeStyle.Light;
                    break;


            }
        }

        private void metroComboBox8_SelectedIndexChanged(object sender, EventArgs e)
        {
            metroStyleManager1.Style = (MetroFramework.MetroColorStyle)Convert.ToInt32(metroComboBox8.SelectedIndex);
        }

        private void metroTabPage5_Click(object sender, EventArgs e)
        {

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void metroTile10_Click(object sender, EventArgs e)
        {
            string ser;
            database[] newdata;
            int count = 0;

            chart1.Series.Clear();
            chart1.Series.Add("Количество заявок");

            using (StreamReader fs = new StreamReader("bazadan.json", encoding: Encoding.Default))
            {
                ser = fs.ReadToEnd();
                newdata = JsonConvert.DeserializeObject<database[]>(ser);

                string period1 = metroDateTime1.Value.ToLongDateString();
                string period2 = metroDateTime2.Value.ToLongDateString();
                DateTime periodfrom = DateTime.Parse(period1);
                DateTime periodto = DateTime.Parse(period2);

                foreach (var p in newdata)
                {
                    foreach (var y in p.pers)
                    {
                        if (y.specialization == 0)
                        {
                            foreach (var x in p.app)
                            {
                                string time = x.date;

                                
                                DateTime periodnow = DateTime.Parse(time);
                                if (periodnow > periodfrom && periodnow < periodto)
                                {
                                    count++;
                                }
                            }


                            this.chart1.Series["Количество заявок"].Points.AddXY(y.name, count);
                            count = 0;

                        }

                    }
                }
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void metroTile7_Click(object sender, EventArgs e)
        {
            using (StreamReader fs = new StreamReader("bazadan.json", encoding: Encoding.Default))
            {
                string ser = fs.ReadToEnd();
                dataGridView3.Rows.Clear();
                database[] newdata = JsonConvert.DeserializeObject<database[]>(ser);

                int r = 0;

                foreach (var p in newdata)
                {

                    foreach (var x in p.pers)
                    {
                        if (x.specialization != 0 & x.exist)
                        {

                            dataGridView3.Rows.Add();
                            dataGridView3[0, r].Value = x.name;
                            if (x.specialization == 1)
                            { dataGridView3[1, r].Value = "Терапевт"; }
                            else if (x.specialization == 2)
                            { dataGridView3[1, r].Value = "Врач ВК"; }
                            else if (x.specialization == 3)
                            { dataGridView3[1, r].Value = "Офтальмолог"; }
                            else if (x.specialization == 4)
                            { dataGridView3[1, r].Value = "Рентгенолог"; }
                            else if (x.specialization == 5)
                            { dataGridView3[1, r].Value = "Онколог"; }
                            else if (x.specialization == 6)
                            { dataGridView3[1, r].Value = "Невролог"; }
                            r++;
                        }
                    }

                }
            }

        }

        private void metroTile8_Click(object sender, EventArgs e)
        {
            using (StreamReader fs = new StreamReader("bazadan.json", encoding: Encoding.Default))
            {
                string ser = fs.ReadToEnd();
                dataGridView4.Rows.Clear();
                database[] newdata = JsonConvert.DeserializeObject<database[]>(ser);

                int r = 0;

                foreach (var p in newdata)
                {

                    foreach (var x in p.pet)
                    {
                        if (x.name != "0")
                        {

                            dataGridView4.Rows.Add();
                            dataGridView4[0, r].Value = x.name;
                            dataGridView4[1, r].Value = x.type;
                            dataGridView4[2, r].Value = x.weight;
                            dataGridView4[3, r].Value = x.age;
                            r++;
                        }
                    }

                }
            }
        }

        private void metroTile5_Click(object sender, EventArgs e)
        {

        }

        private void metroTile3_Click(object sender, EventArgs e)
        {

        }

        private void metroTile1_Click(object sender, EventArgs e)
        {

        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.Show();
        }

        private void dataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            /*int row = e.RowIndex;
            string name = Convert.ToString(dataGridView4.Rows[row].Cells[0]);
            int weight = Convert.ToInt32(dataGridView4.Rows[row].Cells[2]);
            pets petz = new pets();
            
            using (StreamReader fs = new StreamReader("bazadan.json", encoding: Encoding.Default))
            {
                string ser = fs.ReadToEnd();
                database[] newdata = JsonConvert.DeserializeObject<database[]>(ser);
                int r = 0;
                foreach (var p in newdata)
                {
                    foreach (var x in p.pet)
                    {
                        if (x.name == name & x.weight == weight)
                        {
                            petz = x;
                        }
                    }

                }
            }

            Form3 f3 = new Form3(petz); */




        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void metroTile1_Click_1(object sender, EventArgs e)
        {
            ClientReg Cr = new ClientReg();
            Cr.Show();
        }

        private void metroTile3_Click_1(object sender, EventArgs e)
        {
            PetReg Pr = new PetReg();
            Pr.Show();
        }

        private void metroTile5_Click_1(object sender, EventArgs e)
        {
            RegWork Rw = new RegWork();
            Rw.Show();
        }

        private void metroTile11_Click(object sender, EventArgs e)
        {
            Form3 f3 = new Form3();
            f3.Show();
        }

        private void metroComboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void metroTile12_Click(object sender, EventArgs e)
        {
            ChWork Cw = new ChWork();
            Cw.Show();
        }

        private void metroTile13_Click(object sender, EventArgs e)
        {
            dataGridView8.Rows.Clear();

            using (StreamReader fs = new StreamReader("bazadan.json", encoding: Encoding.Default))
            {
                string ser = fs.ReadToEnd();
                int r = 0;
                database[] newdata = JsonConvert.DeserializeObject<database[]>(ser);

                foreach (var p in newdata)
                {
                    foreach (var x in p.pers)
                    {
                        if (x.specialization == 0 & x.exist)
                        {
                            foreach (var s in p.pet)
                            {
                                if (s.name != "0")
                                {
                                    dataGridView8.Rows.Add();
                                    dataGridView8[0, r].Value = s.name;
                                    dataGridView8[1, r].Value = x.name;
                                    r++;
                                }

                            }
                        }
                    }

                }

            }
        }

        //metroComboBox10.SelectedIndex
    }
}

/*string[] elements = time.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                int day = Convert.ToInt32(elements[0]);
                                int year = Convert.ToInt32(elements[2]);
                                int months = 1;

                                if (elements[1] == "января")
                                {
                                    months = 1;
                                }
                                else if (elements[1] == "февраля")
                                {
                                    months = 2;
                                }
                                else if (elements[1] == "марта")
                                {
                                    months = 3;
                                }
                                else if (elements[1] == "апреля")
                                {
                                    months = 4;
                                }
                                else if (elements[1] == "мая")
                                {
                                    months = 5;
                                }
                                else if (elements[1] == "июня")
                                {
                                    months = 6;
                                }
                                else if (elements[1] == "июля")
                                {
                                    months = 7;
                                }
                                else if (elements[1] == "августа")
                                {
                                    months = 8;
                                }
                                else if (elements[1] == "сентября")
                                {
                                    months = 9;
                                }
                                else if (elements[1] == "октября")
                                {
                                    months = 10;
                                }
                                else if (elements[1] == "ноября")
                                {
                                    months = 11;
                                }
                                else if (elements[1] == "декабря")
                                {
                                    months = 12;
                                }

                                DateTime periodnow = new DateTime(year, months, day);*/


/*   database data = new database();
            data.pers = new persons[1];
            data.pers[0] = new persons()
            {
                name = "Inna",
                specialization = 0,
                exist = true
            };
            data.pet = new pets[1];
            data.pet[0] = new pets()
            {
                name = "Дым",
                type = "Кот",
                age = 10,
                weight = 4
            };
            data.app = new application[1];
            data.app[0] = new application()
            {
                name = "Прием терапевта",
                namepet = "Дым",
                namevet = "Альфред",
                date = "20 июня 2019 г.",
                time = "10:00",
                count = 1000,
                cabinet = 100
            };

            database data2 = new database();
            data2.pers = new persons[1];

            data2.pers[0] = new persons()
            {
                name = "Romul",
                specialization = 0,
                exist = true
            };

            data2.pet = new pets[1];
            data2.pet[0] = new pets()
            {
                name = "Wolf",
                type = "Волк",
                age = 3,
                weight = 32
            };
            data2.app = new application[1];
            data2.app[0] = new application()
            {
                name = "Прием врача высшей категории",
                namepet = "Wolf",
                namevet = "Ричи",
                date = "21 июня 2019 г.",
                time = "11:00",
                count = 3000,
                cabinet = 200
            };

            database data3 = new database();
            data3.pers = new persons[1];

            data3.pers[0] = new persons()
            {
                name = "Алекс",
                specialization = 0,
                exist = true
            };

            data3.pet = new pets[2];
            data3.pet[0] = new pets()
            {
                name = "Хьюго",
                type = "Черепаха",
                age = 2,
                weight = 4
            };
            data3.pet[1] = new pets()
            {
                name = "Сьюзан",
                type = "Кролик",
                age = 1,
                weight = 2
            };

            data3.app = new application[2];

            data3.app[0] = new application()
            {
                name = "Прием офтальмолога",
                namepet = "Сьюзан",
                namevet = "Чарли",
                date = "18 июня 2019 г.",
                time = "11:30",
                count = 1100,
                cabinet = 300

            };

            data3.app[1] = new application()
            {
                name = "Прием терапевта",
                namepet = "Хьюго",
                namevet = "Альфред",
                date = "20 июня 2019 г.",
                time = "12:00",
                count = 1000,
                cabinet = 100
            };


            database data4 = new database();
            data4.pers = new persons[1];

            data4.pers[0] = new persons()
            {
                name = "Чарли",
                specialization = 3,
                exist = true
            };

            data4.pet = new pets[1];
            data4.pet[0] = new pets()
            {
                name = "0",
                type = "0",
                age = 0,
                weight = 0
            };
            data4.app = new application[1];
            data4.app[0] = new application()
            {
                name = "Прием офтальмолога",
                namepet = "Сьюзан",
                namevet = "Чарли",
                date = "18 июня 2019 г.",
                time = "11:30",
                count = 1100,
                cabinet = 300

            };

            database data5 = new database();
            data5.pers = new persons[1];

            data5.pers[0] = new persons()
            {
                name = "Альфред",
                specialization = 1,
                exist = true
            };

            data5.pet = new pets[1];
            data5.pet[0] = new pets()
            {
                name = "0",
                type = "0",
                age = 0,
                weight = 0
            };
            data5.app = new application[2];
            data5.app[0] = new application()
            {
                name = "Прием терапевта",
                namepet = "Дым",
                namevet = "Альфред",
                date = "20 июня 2019 г.",
                time = "10:00",
                count = 1000,
                cabinet = 100
            };
            data5.app[1] = new application()
            {
                name = "Прием терапевта",
                namepet = "Хьюго",
                namevet = "Альфред",
                date = "20 июня 2019 г.",
                time = "12:00",
                count = 1000,
                cabinet = 100
            };

            database data6 = new database();
            data6.pers = new persons[1];

            data6.pers[0] = new persons()
            {
                name = "Лорел",
                specialization = 4,
                exist = true
            };

            data6.pet = new pets[1];
            data6.pet[0] = new pets()
            {
                name = "0",
                type = "0",
                age = 0,
                weight = 0
            };
            data6.app = new application[1];
            data6.app[0] = new application()
            {
                name = "0",
                namepet = "0",
                namevet = "0",
                date = "0",
                time = "0",
                count = 0,
                cabinet = 0
            };

            database data7 = new database();
            data7.pers = new persons[1];

            data7.pers[0] = new persons()
            {
                name = "Ричи",
                specialization = 2,
                exist = true
            };

            data7.pet = new pets[1];
            data7.pet[0] = new pets()
            {
                name = "0",
                type = "0",
                age = 0,
                weight = 0
            };
            data7.app = new application[1];
            data7.app[0] = new application()
            {
                name = "0",
                namepet = "0",
                namevet = "0",
                date = "0",
                time = "0",
                count = 0,
                cabinet = 0
            };

            database data8 = new database();
            data8.pers = new persons[1];

            data8.pers[0] = new persons()
            {
                name = "Рэйвен",
                specialization = 5,
                exist = true
            };

            data8.pet = new pets[1];
            data8.pet[0] = new pets()
            {
                name = "0",
                type = "0",
                age = 0,
                weight = 0
            };
            data8.app = new application[1];
            data8.app[0] = new application()
            {
                name = "0",
                namepet = "0",
                namevet = "0",
                date = "0",
                time = "0",
                count = 0,
                cabinet = 0
            };

            database data9 = new database();
            data9.pers = new persons[1];

            data9.pers[0] = new persons()
            {
                name = "Василиса",
                specialization = 6,
                exist = true
            };

            data9.pet = new pets[1];
            data9.pet[0] = new pets()
            {
                name = "0",
                type = "0",
                age = 0,
                weight = 0
            };
            data9.app = new application[1];
            data9.app[0] = new application()
            {
                name = "0",
                namepet = "0",
                namevet = "0",
                date = "0",
                time = "0",
                count = 0,
                cabinet = 0
            };

            database[] datadata = new database[] { data, data2, data3, data4, data5, data6, data7, data8, data9 };

            string serialized = JsonConvert.SerializeObject(datadata);
            richTextBox1.Text = serialized;

            using (StreamWriter sw = new StreamWriter("bazadan.json", false, System.Text.Encoding.Default))
            {
                sw.WriteLine(serialized);
            }


*/

/*string json = richTextBox1.Text;

richTextBox1.Text = " ";

database[] newdata = JsonConvert.DeserializeObject<database[]>(json);

string spec = "1";


foreach (var p in newdata)
{
    foreach (var s in newdata.)
    {

    }
    if (p.spec.id == 0)
    {
        spec = "Клиент";
    }


} */

/*string json = richTextBox1.Text;
database p = JsonConvert.DeserializeObject<database>(json);

string spec = "1";


if (p.spec.id == 0)
{
    spec = "Клиент";
}


richTextBox1.Text += "Имя: " + p.name + " Специализация: " + spec + " Имя питомца: " + p.pet.name + " Вид питомца: " + p.pet.type + " Возраст питомца: " + p.pet.age + " Вес питомца: " + p.pet.weight + " Тип услуги: " + p.app.name + " Время: " + p.app.time + " Стоимость: " + p.app.count;
*/
