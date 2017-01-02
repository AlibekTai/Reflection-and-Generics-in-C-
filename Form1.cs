using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection; //Для использования Метод Type.GetFields

namespace Generics_and_Reflection
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        class Parrent
        {
            public static string fieldInfo1;
            public static string fieldInfo2;
            public static string fieldInfo3;

            public string paramName { get; set; }
            public string paramValue { get; set; }
            public string paramCode { get; set; }
            public string paramType { get; set; }
            public string paramFormul { get; set; }


            public static IEnumerable<string> GenericMethodEnum<T>(IEnumerable<T> par1)
            {
                var fields = typeof(T).GetFields();
                var properties = typeof(T).GetProperties();

                foreach (var f in par1)
                {
                    yield return string.Join(",",
                                             fields.Select(x => (x.GetValue(f) ?? string.Empty).ToString())
                                                   .Concat(properties.Select(p => (p.GetValue(f, null) ?? string.Empty).ToString()))
                                                   .ToArray());                    

                }
            }

            //Метод с универсальными типом в качестве параметра
            public static string GenericMethod<T>(IEnumerable<T> par1)
            {
                string  resStr="";
                Type typeObj = par1.GetType();
                PropertyInfo[] PropertyInfo = typeObj.GetProperties(BindingFlags.Public | BindingFlags.Instance);

               // var farr = Props.Select(f => f.Name).ToArray();
                //foreach (var f in farr)
               // {
               //     MessageBox.Show(f.ToString());
               // }

                var fields = typeof(T).GetFields();
                //Переменная properties содержит все свойства для типа <T> (в данном случае класса List<Employers>) 
                //в нетипизированную переменную
                var properties = typeof(T).GetProperties();

                /*PropertyInfo pinfo = typeof(T).GetProperty("Name");
                 foreach (var f in par1)
                    {
                        MessageBox.Show(pinfo.GetValue(f).ToString());
                    }*/
                //Перечисление значений для всех полей свойств класса <T> (в данном случае класса List<Employers>)
                foreach (var p in properties)
                    {
                        //Создается экземпляр класса PropertyInfo, в экземпляре созданного класса
                        //хранится характеристики очередного свойства класса Employers        
                        PropertyInfo Prop = typeof(T).GetProperty(p.Name.ToString());
                        //PropertyInfo Prop = pGetProperty(p.Name.ToString());
                    
                        resStr = resStr + "\n" + "Ниже перечислены значения для свойства " + p.Name.ToString() + ":";
                        foreach (var f in par1)
                        {
                            //MessageBox.Show(Prop.GetValue(f).ToString());
                            //Prop.GetValue(f) - возвращает значение заданного свойства (Prop) для заданного объекта (f)
                            resStr = resStr + "\n" + "-" + Prop.GetValue(f).ToString();
                        }
                    }

   

   
                return resStr;
            }

        }

        public class Employers
        {
            public int Salary { get; set; }
            public string Name { get; set; }
            //Поле коллекции, тип которого относится к классу EmpSkill    
            //public EmpSkill[] Skills { get; set; }
            public string Skills { get; set; }
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            //------------Рефлексия--------------
            Parrent obj = new Parrent();
            Parrent.fieldInfo1 = "value1";
            Parrent.fieldInfo2 = "value2";
            Parrent.fieldInfo3 = "value3";
            Type type = obj.GetType();
            FieldInfo[] fieldInfo = type.GetFields(BindingFlags.Static | BindingFlags.Public);
            PropertyInfo[] PropertyInfo = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            //var farr = fieldInfo.Select(f => f.Name).ToArray();


            //Вывести список названий полей и их значений
            foreach (var f in fieldInfo)
            {
                //MessageBox.Show(f.Name.ToString());
                richTextBox1.Text = richTextBox1.Text + "\n" + f.Name.ToString() + "=" + f.GetValue(null);
            }

            richTextBox1.Text = richTextBox1.Text + "\n" + "-------------------";
            //Вывести список названий свойств
            foreach (var p in PropertyInfo)
            {
                //MessageBox.Show(p.Name.ToString());
                richTextBox1.Text = richTextBox1.Text + "\n" + p.Name.ToString();
            }

            richTextBox1.Text = richTextBox1.Text + "\n" + "-------------------";

        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<Employers> worker = new List<Employers>        
            {
            new Employers(){Name = "Ivan", Salary=10000,   Skills= "junior"},
            new Employers(){Name = "Dmitriy", Salary=10000,   Skills= "middle"},                          
            new Employers(){Name = "Volodya", Salary=25000, Skills= "senior"}
            };
            string result;
            //Parrent obj = new Parrent();


            richTextBox1.Text = richTextBox1.Text + "\n" + "-------------------";
            richTextBox1.Text = richTextBox1.Text + "\n" + "Использование параметров  универсального типа";
            richTextBox1.Text = richTextBox1.Text + "\n" + Parrent.GenericMethod(worker);

            //foreach (var line in Parrent.GenericMethod(worker))
            //{
                //textWriter.WriteLine(line);
            //    MessageBox.Show(line);
            //}
            //result = Parrent.GenericMethod(worker);
        }
    }
}
