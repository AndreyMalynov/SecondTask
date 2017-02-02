using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;
using StrangeList.Interfaces;

namespace StrangeList.Models
{
    public class ElementOfListContext
    {

        public ElementOfListContext(IData dataSource)
        {
            DataSource = dataSource;
        }

        public static int LastId { get; set; }

        public IData DataSource { get; set; }

        public string Name { get { return DataSource.Name; } }

        public List<ElementOfList> ElementsOfList { get { return DataSource.ElementsOfList; } }

        public void OpenOrCreate()
        {
            DataSource.OpenOrCreate();
        }

        public void SaveChanges()
        {
            DataSource.SaveChanges();
        }

        internal void Dispose()
        {
            SaveChanges();
        }
    }
}