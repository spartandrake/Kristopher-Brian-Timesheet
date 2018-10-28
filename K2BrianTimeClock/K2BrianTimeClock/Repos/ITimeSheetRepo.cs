using K2BrianTimeClock.DAL.Repos.Base;
using System;
using System.Collections.Generic;
using System.Text;
using TimeClock.Models.Entities;

namespace K2BrianTimeClock.DAL.Repos
{
    class ITimeSheetRepo : IRepo<TimeSheet>
    {
        public int Count => throw new NotImplementedException();

        public bool HasChanges => throw new NotImplementedException();

        public int Add(TimeSheet entity, bool persist = true)
        {
            throw new NotImplementedException();
        }

        public int AddRange(IEnumerable<TimeSheet> entities, bool persist = true)
        {
            throw new NotImplementedException();
        }

        public int Delete(TimeSheet entity, bool persist = true)
        {
            throw new NotImplementedException();
        }

        public int Delete(int id, byte[] timeStamp, bool persist = true)
        {
            throw new NotImplementedException();
        }

        public int DeleteRange(IEnumerable<TimeSheet> entities, bool persist = true)
        {
            throw new NotImplementedException();
        }

        public TimeSheet Find(int? id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TimeSheet> GetAll()
        {
            throw new NotImplementedException();
        }

        public TimeSheet GetFirst()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TimeSheet> GetRange(int skip, int take)
        {
            throw new NotImplementedException();
        }

        public int SaveChanges()
        {
            throw new NotImplementedException();
        }

        public int Update(TimeSheet entity, bool persist = true)
        {
            throw new NotImplementedException();
        }

        public int UpdateRange(IEnumerable<TimeSheet> entities, bool persist = true)
        {
            throw new NotImplementedException();
        }
    }
}
