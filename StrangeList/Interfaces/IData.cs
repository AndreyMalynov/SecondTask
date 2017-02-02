using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StrangeList.Models;

namespace StrangeList.Interfaces
{
    public interface IData
    {
        string Name { get; }
        List<ElementOfList> ElementsOfList { get; set; }
        void OpenOrCreate();
        void SaveChanges();
    }
}
