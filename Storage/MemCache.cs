using PISLabs.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PISLabs.Storage
{
    public class MemCache : IStorage<TicketsData>
    {
        private object _sync = new object();
        private List<TicketsData> _memCache = new List<TicketsData>();
        public TicketsData this[Guid id]
        {
            get
            {
                lock (_sync)
                {
                    if (!Has(id))
                    {
                        throw new IncorrectTicketsDataException($"No TicketsData with id {id}");
                    }

                    return _memCache.Single(x => x.Id == id);
                }
            }
            set
            {

                if (id == Guid.Empty)
                {
                    throw new IncorrectTicketsDataException("Cannot request TicketsData with an empty id");
                }

                lock (_sync)

                {

                    if (Has(id))

                    {

                        RemoveAt(id);

                    }


                    value.Id = id;

                    _memCache.Add(value);

                }

            }

        }


        public System.Collections.Generic.List<TicketsData> All => _memCache.Select(x => x).ToList();


        public void Add(TicketsData value)

        {

            if (value.Id != Guid.Empty)
            {
                throw new IncorrectTicketsDataException($"Cannot add value with predefined id {value.Id}");
            }

            value.Id = Guid.NewGuid();

            this[value.Id] = value;

        }


        public bool Has(Guid id)

        {

            return _memCache.Any(x => x.Id == id);

        }


        public void RemoveAt(Guid id)

        {

            lock (_sync)

            {

                _memCache.RemoveAll(x => x.Id == id);

            }

        }

    }
}
