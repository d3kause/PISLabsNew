using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PISLabs.Storage
{
    public interface IStorage<T> where T : class
    {
        List<T> All { get; }
        T this[Guid id] { get; set; }
        void Add(T value);
        void RemoveAt(Guid id);
        bool Has(Guid id);
        string StorageType { get; }
    }
}
