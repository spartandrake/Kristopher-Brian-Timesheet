using K2BrianTimeClock.DAL.Repos.Base;
using System;
using System.Collections.Generic;
using System.Text;
using TimeClock.Models.Entities;

namespace K2BrianTimeClock.DAL.Repos
{
    class IManagerRepo : IRepo<Manager>
    {
        public int Count => throw new NotImplementedException();

        public bool HasChanges => throw new NotImplementedException();

        public int Add(Manager entity, bool persist = true)
        {
            throw new NotImplementedException();
        }

        public int AddRange(IEnumerable<Manager> entities, bool persist = true)
        {
            throw new NotImplementedException();
        }

        public int Delete(Manager entity, bool persist = true)
        {
            throw new NotImplementedException();
        }

        public int Delete(int id, byte[] timeStamp, bool persist = true)
        {
            throw new NotImplementedException();
        }

        public int DeleteRange(IEnumerable<Manager> entities, bool persist = true)
        {
            throw new NotImplementedException();
        }

        public Manager Find(int? id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Manager> GetAll()
        {
            throw new NotImplementedException();
        }

        public Manager GetFirst()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Manager> GetRange(int skip, int take)
        {
            throw new NotImplementedException();
        }

        public int SaveChanges()
        {
            throw new NotImplementedException();
        }

        public int Update(Manager entity, bool persist = true)
        {
            throw new NotImplementedException();
        }

        public int UpdateRange(IEnumerable<Manager> entities, bool persist = true)
        {
            throw new NotImplementedException();
        }
    }
}
