using K2BrianTimeClock.DAL.Repos.Base;
using System;
using System.Collections.Generic;
using System.Text;
using TimeClock.Models.Entities;

namespace K2BrianTimeClock.DAL.Repos
{
    class IHRManagerRepo : IRepo<HRManager>
    {
        public int Count => throw new NotImplementedException();

        public bool HasChanges => throw new NotImplementedException();

        public int Add(HRManager entity, bool persist = true)
        {
            throw new NotImplementedException();
        }

        public int AddRange(IEnumerable<HRManager> entities, bool persist = true)
        {
            throw new NotImplementedException();
        }

        public int Delete(HRManager entity, bool persist = true)
        {
            throw new NotImplementedException();
        }

        public int Delete(int id, byte[] timeStamp, bool persist = true)
        {
            throw new NotImplementedException();
        }

        public int DeleteRange(IEnumerable<HRManager> entities, bool persist = true)
        {
            throw new NotImplementedException();
        }

        public HRManager Find(int? id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<HRManager> GetAll()
        {
            throw new NotImplementedException();
        }

        public HRManager GetFirst()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<HRManager> GetRange(int skip, int take)
        {
            throw new NotImplementedException();
        }

        public int SaveChanges()
        {
            throw new NotImplementedException();
        }

        public int Update(HRManager entity, bool persist = true)
        {
            throw new NotImplementedException();
        }

        public int UpdateRange(IEnumerable<HRManager> entities, bool persist = true)
        {
            throw new NotImplementedException();
        }
    }
}
