using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eshop.ClassLibrary.Models.Repository
{
    public interface IRepository<T> where T:class
    {
        IEnumerable<T> GetAll();
        T GetById(object Id);
        void Insert(T obj);
        void Delete(object Id);
        void Update(T obj);
        void Save();

    }
}