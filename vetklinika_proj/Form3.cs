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
    public partial class Form3 : MetroFramework.Forms.MetroForm
    {
        public Form3()
        {
            InitializeComponent();
        }
        private void Form3_Load(object sender, EventArgs e, pets pet)
        {
            metroComboBox1.Items.Clear();


        }

        private void button1_Click(object sender, EventArgs e, pets pet)
        {
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void metroComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (StreamReader fs = new StreamReader("bazadan.json", encoding: Encoding.Default))
            {
                string ser = fs.ReadToEnd();
                database[] newdata = JsonConvert.DeserializeObject<database[]>(ser);

                foreach (var p in newdata)
                {

                    foreach (var x in p.pet)
                    {
                        if (x.name == metroComboBox1.SelectedItem.ToString())
                        {

                            textBox2.Text = Convert.ToString(x.weight);
                            textBox3.Text = Convert.ToString(x.age);
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

                    foreach (var x in p.pet)
                    {

                        if (x.name == metroComboBox1.SelectedItem.ToString())
                        {
                            x.age = Convert.ToDouble(textBox3.Text);
                            x.weight = Convert.ToDouble(textBox2.Text);

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

        private void metroTile2_Click(object sender, EventArgs e)
        {

            using (StreamReader fs = new StreamReader("bazadan.json", encoding: Encoding.Default))
            {
                string ser = fs.ReadToEnd();
                database[] newdata = JsonConvert.DeserializeObject<database[]>(ser);

                foreach (var p in newdata)
                {

                    foreach (var x in p.pet)
                    {
                        if (x.name != "0")
                        {
                            metroComboBox1.Items.Add(x.name);
                        }
                    }

                }
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
