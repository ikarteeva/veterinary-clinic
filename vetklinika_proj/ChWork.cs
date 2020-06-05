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
    public partial class ChWork : MetroFramework.Forms.MetroForm
    {
        public ChWork()
        {
            InitializeComponent();
        }

        private void ChWork_Load(object sender, EventArgs e)
        {
            metroComboBox1.Items.Clear();
            metroComboBox2.Items.Clear();
            metroComboBox2.Items.Add("Терапевт");
            metroComboBox2.Items.Add("Врач высшей категории");
            metroComboBox2.Items.Add("Офтальмолог");
            metroComboBox2.Items.Add("Рентгенолог");
            metroComboBox2.Items.Add("Онколог");
            metroComboBox2.Items.Add("Невролог");
        }

        private void metroTile2_Click(object sender, EventArgs e)
        {
            using (StreamReader fs = new StreamReader("bazadan.json", encoding: Encoding.Default))
            {
                string ser = fs.ReadToEnd();
                database[] newdata = JsonConvert.DeserializeObject<database[]>(ser);

                foreach (var p in newdata)
                {

                    foreach (var x in p.pers)
                    {
                        if (x.specialization != 0 & x.exist)
                        {
                            metroComboBox1.Items.Add(x.name);
                        }
                    }

                }
            }
        }

        int spec = 0;
        private void metroComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedState = metroComboBox2.SelectedItem.ToString();
            if (selectedState == "Терапевт")
            {
                spec = 1;
            }
            else if (selectedState == "Врач высшей категории")
            {
                spec = 2;
            }
            else if (selectedState == "Офтальмолог")
            {
                spec = 3;
            }
            else if (selectedState == "Рентгенолог")
            {
                spec = 4;
            }
            else if (selectedState == "Онколог")
            {
                spec = 5;
            }
            else if (selectedState == "Невролог")
            {
                spec = 6;
            }
        }

        private void metroComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (StreamReader fs = new StreamReader("bazadan.json", encoding: Encoding.Default))
            {
                string ser = fs.ReadToEnd();
                database[] newdata = JsonConvert.DeserializeObject<database[]>(ser);

                foreach (var p in newdata)
                {

                    foreach (var x in p.pers)
                    {
                        if (x.name == metroComboBox1.SelectedItem.ToString())
                        { 
                            metroComboBox2.SelectedIndex = x.specialization - 1;
                        }
                    }

                }
            }
        }


        private void metroTile1_Click(object sender, EventArgs e)
        {
            string ser;
            database[] newdata;

            using (StreamReader fs = new StreamReader("bazadan.json", encoding: Encoding.Default))
            {
                ser = fs.ReadToEnd();
                newdata = JsonConvert.DeserializeObject<database[]>(ser);

                foreach (var p in newdata)
                {

                    foreach (var x in p.pers)
                    {

                        if (x.name == metroComboBox1.SelectedItem.ToString())
                        {
                            x.specialization = spec;

                        }

                    }
                }

            }

            string serialized = JsonConvert.SerializeObject(newdata);

            using (StreamWriter sw = new StreamWriter("bazadan.json", false, System.Text.Encoding.Default))
            {
                sw.WriteLine(serialized);
            }

            MessageBox.Show("Выполнено!");
        }
    }
}
