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
    public partial class Deleter : MetroFramework.Forms.MetroForm
    {
        public Deleter()
        {
            InitializeComponent();
        }

        private void Deleter_Load(object sender, EventArgs e)
        {
            int flag;
            using (StreamReader rs = new StreamReader("settings.txt", encoding: Encoding.Default))
            {
                string set = rs.ReadToEnd();
                flag = Convert.ToInt32(set);
            }


            if (flag == 10)
            {
                metroComboBox2.Items.Add("10:00");
                metroComboBox2.Items.Add("10:10");
                metroComboBox2.Items.Add("10:20");
                metroComboBox2.Items.Add("10:30");
                metroComboBox2.Items.Add("10:40");
                metroComboBox2.Items.Add("10:50");
                metroComboBox2.Items.Add("11:00");
                metroComboBox2.Items.Add("11:10");
                metroComboBox2.Items.Add("11:20");
                metroComboBox2.Items.Add("11:30");
                metroComboBox2.Items.Add("11:40");
                metroComboBox2.Items.Add("11:50");
                metroComboBox2.Items.Add("12:00");
                metroComboBox2.Items.Add("12:10");
                metroComboBox2.Items.Add("12:20");
                metroComboBox2.Items.Add("12:30");
                metroComboBox2.Items.Add("12:40");
                metroComboBox2.Items.Add("12:50");

            }

            if (flag == 20)
            {
                metroComboBox2.Items.Add("10:00");
                metroComboBox2.Items.Add("10:20");
                metroComboBox2.Items.Add("10:40");
                metroComboBox2.Items.Add("11:00");
                metroComboBox2.Items.Add("11:20");
                metroComboBox2.Items.Add("11:40");
                metroComboBox2.Items.Add("12:00");
                metroComboBox2.Items.Add("12:20");
                metroComboBox2.Items.Add("12:40");

            }

            if (flag == 30)
            {
                metroComboBox2.Items.Add("10:00");
                metroComboBox2.Items.Add("10:30");
                metroComboBox2.Items.Add("11:00");
                metroComboBox2.Items.Add("11:30");
                metroComboBox2.Items.Add("12:00");
                metroComboBox2.Items.Add("12:30");

            }

            using (StreamReader fs = new StreamReader("bazadan.json", encoding: Encoding.Default))
            {
                string ser = fs.ReadToEnd();

                database[] newdata = JsonConvert.DeserializeObject<database[]>(ser);

                foreach (var p in newdata)
                {
                    foreach (var x in p.pet)
                    {
                        if (x.name != "0")
                        { metroComboBox1.Items.Add(x.name); }
                    }
                }

            }
        }

        private void metroTile1_Click(object sender, EventArgs e)
        {
            string namep = metroComboBox1.SelectedItem.ToString();
            string time = metroComboBox2.SelectedItem.ToString();
            string date = metroDateTime1.Value.ToLongDateString();

            string ser;
            database[] newdata;

            using (StreamReader fs = new StreamReader("bazadan.json", encoding: Encoding.Default))
            {
                ser = fs.ReadToEnd();
                newdata = JsonConvert.DeserializeObject<database[]>(ser);
                string zapis = null;

                foreach (var p in newdata)
                {
                    foreach (var y in p.app)
                    {
                        if (namep == y.namepet && time == y.time && date == y.date)
                        {
                            y.name = null;
                            zapis = "Выполнено!";
                        }


                    }

                }

                if (zapis == null)
                {
                    MessageBox.Show("Такой записи нет!");
                }
                else
                {
                    MessageBox.Show(zapis);
                }


            }

            string serialized = JsonConvert.SerializeObject(newdata);

            using (StreamWriter sw = new StreamWriter("bazadan.json", false, System.Text.Encoding.Default))
            {
                sw.WriteLine(serialized);
            }


        }
    }
}
