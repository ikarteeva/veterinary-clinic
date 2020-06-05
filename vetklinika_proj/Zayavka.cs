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


namespace vetklinika
{
    public partial class Zayavka : MetroFramework.Forms.MetroForm
    {
        int counts;
        public Zayavka()
        {
            InitializeComponent();
        }

        private void metroDateTime1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void metroTile1_Click(object sender, EventArgs e)
        {
            string ser;
            database[] newdata;

            using (StreamReader fs = new StreamReader("bazadan.json", encoding: Encoding.Default))
            {
                ser = fs.ReadToEnd();
                newdata = JsonConvert.DeserializeObject<database[]>(ser);
                int r = 0;
                string zapis = null;

                foreach (var p in newdata)
                {
                    foreach (var y in p.app)
                    {
                        if (metroDateTime2.Value.ToLongDateString() == y.date & metroComboBox5.SelectedItem.ToString() == y.time & metroComboBox4.SelectedItem.ToString() == y.namevet)
                        {
                            zapis = "Время занято!";
                        }
                        else if (metroDateTime2.Value.ToLongDateString() == y.date & metroComboBox5.SelectedItem.ToString() == y.time & Convert.ToInt32(metroComboBox6.SelectedItem.ToString()) == y.cabinet)
                            {
                                zapis = "Кабинет занят!";
                            }


                    }

                    foreach (var x in p.pers)
                    {
                        if (zapis == null)
                        {
                            if (x.name == metroComboBox1.SelectedItem.ToString())
                            {

                                int opa = p.app.Length;
                                database newdata1 = new database();
                                newdata1.app = new application[opa + 1];

                                for (int i = 0; i < opa; i++)
                                {
                                    newdata1.app[i] = p.app[i];

                                }

                                p.app = new application[opa + 1];

                                for (int i = 0; i < opa; i++)
                                {
                                    p.app[i] = newdata1.app[i];

                                }


                                p.app[opa] = new application()
                                {
                                    name = metroComboBox3.SelectedItem.ToString(),
                                    namepet = metroComboBox2.SelectedItem.ToString(),
                                    namevet = metroComboBox4.SelectedItem.ToString(),
                                    date = metroDateTime2.Value.ToLongDateString(),
                                    time = metroComboBox5.SelectedItem.ToString(),
                                    count = Convert.ToInt32(metroTextBox1.Text),
                                    cabinet = Convert.ToInt32(metroComboBox6.SelectedItem.ToString())
                                };

                            }

                        }

                        else { MessageBox.Show(zapis); break; }

                    }

                    if (zapis != null)
                    {
                        break;
                    }
                }

                if (zapis == null)
                {
                    MessageBox.Show("Выполнено!");
                }


            }

            string serialized = JsonConvert.SerializeObject(newdata);

            using (StreamWriter sw = new StreamWriter("bazadan.json", false, System.Text.Encoding.Default))
            {
                sw.WriteLine(serialized);
            }

        }

        private void Zayavka_Load(object sender, EventArgs e)
        {
            int flag;
            using (StreamReader rs = new StreamReader("settings.txt", encoding: Encoding.Default))
            {
                string set = rs.ReadToEnd();
                flag = Convert.ToInt32(set);
            }


            if (flag == 10)
            {
                metroComboBox5.Items.Add("10:00");
                metroComboBox5.Items.Add("10:10");
                metroComboBox5.Items.Add("10:20");
                metroComboBox5.Items.Add("10:30");
                metroComboBox5.Items.Add("10:40");
                metroComboBox5.Items.Add("10:50");
                metroComboBox5.Items.Add("11:00");
                metroComboBox5.Items.Add("11:10");
                metroComboBox5.Items.Add("11:20");
                metroComboBox5.Items.Add("11:30");
                metroComboBox5.Items.Add("11:40");
                metroComboBox5.Items.Add("11:50");
                metroComboBox5.Items.Add("12:00");
                metroComboBox5.Items.Add("12:10");
                metroComboBox5.Items.Add("12:20");
                metroComboBox5.Items.Add("12:30");
                metroComboBox5.Items.Add("12:40");
                metroComboBox5.Items.Add("12:50");

            }

            if (flag == 20)
            {
                metroComboBox5.Items.Add("10:00");
                metroComboBox5.Items.Add("10:20");
                metroComboBox5.Items.Add("10:40");
                metroComboBox5.Items.Add("11:00");
                metroComboBox5.Items.Add("11:20");
                metroComboBox5.Items.Add("11:40");
                metroComboBox5.Items.Add("12:00");
                metroComboBox5.Items.Add("12:20");
                metroComboBox5.Items.Add("12:40");

            }

            if (flag == 30)
            {
                metroComboBox5.Items.Add("10:00");
                metroComboBox5.Items.Add("10:30");
                metroComboBox5.Items.Add("11:00");
                metroComboBox5.Items.Add("11:30");
                metroComboBox5.Items.Add("12:00");
                metroComboBox5.Items.Add("12:30");

            }



            using (StreamReader fs = new StreamReader("bazadan.json", encoding: Encoding.Default))
            {
                string ser = fs.ReadToEnd();

                database[] newdata = JsonConvert.DeserializeObject<database[]>(ser);

                metroComboBox3.Items.Add("Прием терапевта");
                metroComboBox3.Items.Add("Прием врача высшей категории");
                metroComboBox3.Items.Add("Прием офтальмолога");
                metroComboBox3.Items.Add("Прием рентгенолога");
                metroComboBox3.Items.Add("Прием онколога");
                metroComboBox3.Items.Add("Прием невролога");


                foreach (var p in newdata)
                {
                    foreach (var x in p.pers)
                    {
                        string spec = "1";

                        if (x.specialization == 0)
                        {
                            spec = "Client";
                            metroComboBox1.Items.Add(x.name);

                        }
                    }
                }

            }
        }
        private void metroComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {



            string selectedState = metroComboBox3.SelectedItem.ToString();
            if (selectedState == "Прием терапевта")
            {
                metroTextBox1.Text = "1000";
                counts = 1000;
                metroComboBox4.Items.Clear();
                metroComboBox6.Items.Clear();
                metroComboBox6.Items.Add(100);
                metroComboBox6.Items.Add(101);

                using (StreamReader fs = new StreamReader("bazadan.json", encoding: Encoding.Default))
                {
                    string ser = fs.ReadToEnd();

                    database[] newdata = JsonConvert.DeserializeObject<database[]>(ser);

                    foreach (var p in newdata)
                    {
                        foreach (var x in p.pers)
                        {

                            if (x.specialization == 1)
                            {
                                metroComboBox4.Items.Add(x.name);
                            }

                        }

                    }
                }

            }

            else if (selectedState == "Прием врача высшей категории")
            {
                metroTextBox1.Text = "3000";
                counts = 3000;
                metroComboBox4.Items.Clear();
                metroComboBox6.Items.Clear();
                metroComboBox6.Items.Add(200);
                metroComboBox6.Items.Add(201);

                using (StreamReader fs = new StreamReader("bazadan.json", encoding: Encoding.Default))
                {
                    string ser = fs.ReadToEnd();

                    database[] newdata = JsonConvert.DeserializeObject<database[]>(ser);

                    foreach (var p in newdata)
                    {
                        foreach (var x in p.pers)
                        {

                            if (x.specialization == 2)
                            {
                                metroComboBox4.Items.Add(x.name);
                            }

                        }

                    }
                }

            }

            else if (selectedState == "Прием офтальмолога")
            {
                metroTextBox1.Text = "1100";
                counts = 1100;
                metroComboBox4.Items.Clear();
                metroComboBox6.Items.Clear();
                metroComboBox6.Items.Add(300);
                metroComboBox6.Items.Add(301);

                using (StreamReader fs = new StreamReader("bazadan.json", encoding: Encoding.Default))
                {
                    string ser = fs.ReadToEnd();

                    database[] newdata = JsonConvert.DeserializeObject<database[]>(ser);

                    foreach (var p in newdata)
                    {
                        foreach (var x in p.pers)
                        {

                            if (x.specialization == 3)
                            {
                                metroComboBox4.Items.Add(x.name);
                            }

                        }

                    }
                }

            }

            else if (selectedState == "Прием рентгенолога")
            {
                metroTextBox1.Text = "1200";
                counts = 1200;
                metroComboBox4.Items.Clear();
                metroComboBox6.Items.Clear();
                metroComboBox6.Items.Add(400);
                metroComboBox6.Items.Add(401);

                using (StreamReader fs = new StreamReader("bazadan.json", encoding: Encoding.Default))
                {
                    string ser = fs.ReadToEnd();

                    database[] newdata = JsonConvert.DeserializeObject<database[]>(ser);

                    foreach (var p in newdata)
                    {
                        foreach (var x in p.pers)
                        {

                            if (x.specialization == 4)
                            {
                                metroComboBox4.Items.Add(x.name);
                            }

                        }

                    }
                }

            }

            else if (selectedState == "Прием онколога")
            {
                metroTextBox1.Text = "1700";
                counts = 1700;
                metroComboBox4.Items.Clear();
                metroComboBox6.Items.Clear();
                metroComboBox6.Items.Add(500);
                metroComboBox6.Items.Add(501);

                using (StreamReader fs = new StreamReader("bazadan.json", encoding: Encoding.Default))
                {
                    string ser = fs.ReadToEnd();

                    database[] newdata = JsonConvert.DeserializeObject<database[]>(ser);

                    foreach (var p in newdata)
                    {
                        foreach (var x in p.pers)
                        {

                            if (x.specialization == 5)
                            {
                                metroComboBox4.Items.Add(x.name);
                            }

                        }

                    }
                }

            }

            else if (selectedState == "Прием невролога")
            {
                metroTextBox1.Text = "1100";
                counts = 1100;
                metroComboBox4.Items.Clear();
                metroComboBox6.Items.Clear();
                metroComboBox6.Items.Add(600);
                metroComboBox6.Items.Add(601);

                using (StreamReader fs = new StreamReader("bazadan.json", encoding: Encoding.Default))
                {
                    string ser = fs.ReadToEnd();

                    database[] newdata = JsonConvert.DeserializeObject<database[]>(ser);

                    foreach (var p in newdata)
                    {
                        foreach (var x in p.pers)
                        {

                            if (x.specialization == 6)
                            {
                                metroComboBox4.Items.Add(x.name);
                            }

                        }

                    }
                }

            }
        }



        private void metroLabel5_Click(object sender, EventArgs e)
        {

        }

        private void metroDateTime2_ValueChanged(object sender, EventArgs e)
        {
        }

        private void metroComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            metroComboBox2.Items.Clear();
            using (StreamReader fs = new StreamReader("bazadan.json", encoding: Encoding.Default))
            {
                string ser = fs.ReadToEnd();

                database[] newdata = JsonConvert.DeserializeObject<database[]>(ser);

                foreach (var p in newdata)
                {
                    foreach (var x in p.pers)
                    {

                        if (x.specialization == 0)
                        {
                            foreach (var s in p.pet)
                            {
                                if (metroComboBox1.SelectedItem.ToString() == x.name & s.name != "0")
                                {
                                    metroComboBox2.Items.Add(s.name);
                                }

                            }
                        }
                    }

                }

            }
        }

        private void metroTextBox1_Click(object sender, EventArgs e)
        {

        }

        private void metroTextBox2_Click(object sender, EventArgs e)
        {

        }

        private void metroComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
