using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using Newtonsoft.Json;


namespace vetklinika
{
    [DataContract]
    class database
    {

        [DataMember]
        public persons[] pers { get; set; }

        [DataMember]
        public pets[] pet { get; set; }

        [DataMember]
        public application[] app { get; set; }


    }

    class persons
    {
        public string name { get; set; }
        public int specialization { get; set; }
        public bool exist { get; set; }

        public persons() { }

        public persons(string Name, int Specialization, bool Exist)
        {
            name = Name;
            specialization = Specialization;
            exist = Exist;
        }

    }

    public class pets
    {
        public string name { get; set; }
        public string type { get; set; }
        public double age { get; set; }
        public double weight { get; set; }
        public pets() { }

        public pets(string Name, string Type, int Age, int Weight)
        {
            name = Name;
            type = Type;
            age = Age;
            weight = Weight;
        }

    }

    class application
    {
        public string name { get; set; }
        public string namepet { get; set; }
        public string namevet { get; set; }
        public string time { get; set; }
        public int count { get; set; }
        public string date { get; set; }

        public int cabinet { get; set; }
        public application() { }

        public application(string Name, string NameP, string NameV, string Time, int Count, string Date, int Cabinet)
        {
            name = Name;
            namepet = NameP;
            namevet = NameV;
            date = Date;
            time = Time;
            count = Count;
            cabinet = Cabinet;
        }

    }

}
