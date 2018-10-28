using K2BrianTimeClock.DAL.Repos.Base;
using System;
using System.Collections.Generic;
using System.Text;
using TimeClock.Models.Entities;

namespace K2BrianTimeClock.DAL.Repos
{
    class IClockInRepo : IRepo<ClockIn>
    {
        public int Count => throw new NotImplementedException();

        public bool HasChanges => throw new NotImplementedException();

        public int Add(ClockIn entity, bool persist = true)
        {
            throw new NotImplementedException();
        }

        public int AddRange(IEnumerable<ClockIn> entities, bool persist = true)
        {
            throw new NotImplementedException();
        }

        public int Delete(ClockIn entity, bool persist = true)
        {
            throw new NotImplementedException();
        }

        public int Delete(int id, byte[] timeStamp, bool persist = true)
        {
            throw new NotImplementedException();
        }

        public int DeleteRange(IEnumerable<ClockIn> entities, bool persist = true)
        {
            throw new NotImplementedException();
        }

        public ClockIn Find(int? id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ClockIn> GetAll()
        {
            throw new NotImplementedException();
        }

        public ClockIn GetFirst()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ClockIn> GetRange(int skip, int take)
        {
            throw new NotImplementedException();
        }

        public int SaveChanges()
        {
            throw new NotImplementedException();
        }

        public int Update(ClockIn entity, bool persist = true)
        {
            throw new NotImplementedException();
        }

        public int UpdateRange(IEnumerable<ClockIn> entities, bool persist = true)
        {
            throw new NotImplementedException();
        }
    }
}
