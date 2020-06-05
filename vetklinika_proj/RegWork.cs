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
    public partial class RegWork : MetroFramework.Forms.MetroForm
    {
        public RegWork()
        {
            InitializeComponent();
        }

        int spec = 0;


        private void RegWork_Load(object sender, EventArgs e)
        {
            metroComboBox1.Items.Clear();
            metroComboBox1.Items.Add("Терапевт");
            metroComboBox1.Items.Add("Врач высшей категории");
            metroComboBox1.Items.Add("Офтальмолог");
            metroComboBox1.Items.Add("Рентгенолог");
            metroComboBox1.Items.Add("Онколог");
            metroComboBox1.Items.Add("Невролог");
        }

        private void metroComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedState = metroComboBox1.SelectedItem.ToString();
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

        private void metroTile1_Click(object sender, EventArgs e)
        {
            string ser;
            database[] newdata;

            using (StreamReader fs = new StreamReader("bazadan.json", encoding: Encoding.Default))
            {
                ser = fs.ReadToEnd();
                newdata = JsonConvert.DeserializeObject<database[]>(ser);

                int opa = newdata.Length;

                database[] newdata1;
                newdata1 = new database[opa + 1];

                for (int i = 0; i < opa; i++)
                {
                    newdata1[i] = newdata[i];
                }

                newdata = new database[opa + 1];

                for (int i = 0; i < opa; i++)
                {
                    newdata[i] = newdata1[i];

                }

                newdata[opa] = new database();
                newdata[opa].pers = new persons[1];
                newdata[opa].pers[0] = new persons()
                {
                    name = metroTextBox1.Text,
                    specialization = spec,
                    exist = true
                };
                newdata[opa].pet = new pets[0];
                newdata[opa].app = new application[0];

                MessageBox.Show("Выполнено!");

            }

            string serialized = JsonConvert.SerializeObject(newdata);

            using (StreamWriter sw = new StreamWriter("bazadan.json", false, System.Text.Encoding.Default))
            {
                sw.WriteLine(serialized);
            }

        }

        private void metroComboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            string selectedState = metroComboBox1.SelectedItem.ToString();
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

        private void metroTextBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
