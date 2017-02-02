using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using StrangeList.Models;
using System.Data.Entity;
using StrangeList.Classes;
namespace StrangeList.Controllers
{

    public class ValuesController : ApiController
    {
        public ElementOfListContext db = new ElementOfListContext(new XmlSource("xmlSource"));

        public IEnumerable<ElementOfList> GetElementsOfList()
        {
            return db.ElementsOfList;
        }

        public ElementOfList GetElementsOfList(int id)
        {
            ElementOfList elementOfList = db.ElementsOfList.Find(x => x.Id == id);
            return elementOfList;
        }

        [HttpPost]
        public void CreateElementOfList([FromBody]ElementOfList elementOfList)
        {
            try
            {
                elementOfList.Id = db.ElementsOfList[db.ElementsOfList.Count - 1].Id + 1;
            }
            catch
            {
                elementOfList.Id = 0;
            }
            db.ElementsOfList.Add(elementOfList);
            db.SaveChanges();
        }

        [HttpPut]
        public void EditElementOfList(int id, [FromBody]ElementOfList elementOfList)
        {
            if (id == elementOfList.Id)
            {
                db.ElementsOfList.Find(x => x.Id == id).Name = elementOfList.Name;
                db.ElementsOfList.Find(x => x.Id == id).Score = elementOfList.Score;
                db.SaveChanges();
            }
        }

        public void DeleteElementOfList(int id)
        {
            ElementOfList elementOfList = db.ElementsOfList.Find(x => x.Id == id);
            if (elementOfList != null)
            {
                db.ElementsOfList.Remove(elementOfList);
                db.SaveChanges();
            }
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
