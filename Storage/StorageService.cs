using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PISLabs.Models;

namespace PISLabs.Storage
{
    public class StorageService
    {
        private readonly IStorage<TicketsData> _storage;

        public StorageService(IStorage<TicketsData> storage)
        {
            _storage = storage;
        }

        public string GetStorageType()
        {
            return _storage.StorageType;
        }

        public int GetNumberOfItems()
        {
            return _storage.All.Count;
        }
    }
}
