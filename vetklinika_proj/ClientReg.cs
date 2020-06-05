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
    public partial class ClientReg : MetroFramework.Forms.MetroForm
    {
        public ClientReg()
        {
            InitializeComponent();
        }

        private void ClientReg_Load(object sender, EventArgs e)
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
                    specialization = 0,
                    exist = true
                };
                newdata[opa].pet = new pets[0];
                newdata[opa].app = new application[0];


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
