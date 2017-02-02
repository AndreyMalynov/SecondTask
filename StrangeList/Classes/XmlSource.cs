using System;
using System.Collections.Generic;
using System.Web;
using StrangeList.Models;
using StrangeList.Interfaces;
using System.Xml.Serialization;
using System.IO;
namespace StrangeList.Classes
{
    public class XmlSource : IData
    {

        XmlSerializer formatter;
        public XmlSource(string name)
        {
            Name = name;
            formatter = new XmlSerializer(typeof(List<ElementOfList>));
            OpenOrCreate();
        }

        public string Name { get; private set; }
        public List<ElementOfList> ElementsOfList { get; set; }

        public void OpenOrCreate()
        {
            try
            {

                //выбросит исключение если файл еще не создан
                try
                {
                    using (FileStream fs = new FileStream( HttpContext.Current.Server.MapPath("~/App_Data/strangeList.xml"), FileMode.OpenOrCreate))
                    {
                        ElementsOfList = (List<ElementOfList>)formatter.Deserialize(fs);
                    }
                }

                //сработет в случае если файла еще не существует. создаст его
                catch
                {
                    ElementsOfList = new List<ElementOfList>();
                    using (FileStream fs = new FileStream(HttpContext.Current.Server.MapPath("~/App_Data/strangeList.xml"), FileMode.OpenOrCreate))
                    {
                        formatter.Serialize(fs, ElementsOfList);
                    }
                }
            }
            catch
            {
                new Exception("Неизвестная ошибка, мы уже работаем");
            }

        }

        public void SaveChanges()
        {
            try
            {
                using (FileStream fs = new FileStream(HttpContext.Current.Server.MapPath("~/App_Data/strangeList.xml"), FileMode.Create))
                {
                    formatter.Serialize(fs, ElementsOfList);
                }
            }
            catch
            {
                new Exception("Неизвестная ошибка, мы уже работаем");
            }

        }

    }
}