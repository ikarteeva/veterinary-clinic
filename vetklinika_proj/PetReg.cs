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
    public partial class PetReg : MetroFramework.Forms.MetroForm
    {
        public PetReg()
        {
            InitializeComponent();
        }

        private void PetReg_Load(object sender, EventArgs e)
        {
            metroComboBox1.Items.Clear();
            metroComboBox2.Items.Clear();

            metroComboBox2.Items.Add("Собака");
            metroComboBox2.Items.Add("Попугай");
            metroComboBox2.Items.Add("Кот");
            metroComboBox2.Items.Add("Черепаха");
            metroComboBox2.Items.Add("Волк");
            metroComboBox2.Items.Add("Кролик");

            List<persons> perl = new List<persons>();

            using (StreamReader fs = new StreamReader("bazadan.json", encoding: Encoding.Default))
            {
                string ser = fs.ReadToEnd();

                database[] newdata = JsonConvert.DeserializeObject<database[]>(ser);

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
                            metroComboBox1.Items.Add(x.name);
                        }

                    }

                }
            }

        }

        private void metroTextBox1_Click(object sender, EventArgs e)
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

                foreach (var p in newdata)
                {

                    foreach (var x in p.pers)
                    {

                        if (x.name == metroComboBox1.SelectedItem.ToString())
                        {
                            int opa = p.pet.Length;

                            if (opa == 0)
                            {
                                p.pet = new pets[1];
                                p.pet[opa] = new pets()
                                {
                                    name = metroTextBox1.Text,
                                    type = metroComboBox2.SelectedItem.ToString(),
                                    age = Convert.ToDouble(metroTextBox3.Text),
                                    weight = Convert.ToDouble(metroTextBox2.Text)
                                };
                            }
                            database newdata1 = new database();
                            newdata1.pet = new pets[opa + 1];

                            for (int i = 0; i < opa; i++)
                            {
                                newdata1.pet[i] = p.pet[i];

                            }

                            p.pet = new pets[opa + 1];

                            for (int i = 0; i < opa; i++)
                            {
                                p.pet[i] = newdata1.pet[i];

                            }


                            p.pet[opa] = new pets()
                            {
                                name = metroTextBox1.Text,
                                type = metroComboBox2.SelectedItem.ToString(),
                                age = Convert.ToDouble(metroTextBox3.Text),
                                weight = Convert.ToDouble(metroTextBox2.Text)
                            };

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
