using K2BrianTimeClock.DAL.Repos.Base;
using System;
using System.Collections.Generic;
using System.Text;
using TimeClock.Models.Entities;

namespace K2BrianTimeClock.DAL.Repos
{
    class IDateAndTimeRepo : IRepo<DateAndTime>
    {
        public int Count => throw new NotImplementedException();

        public bool HasChanges => throw new NotImplementedException();

        public int Add(DateTime entity, bool persist = true)
        {
            throw new NotImplementedException();
        }

        public int Add(DateAndTime entity, bool persist = true)
        {
            throw new NotImplementedException();
        }

        public int AddRange(IEnumerable<DateTime> entities, bool persist = true)
        {
            throw new NotImplementedException();
        }

        public int AddRange(IEnumerable<DateAndTime> entities, bool persist = true)
        {
            throw new NotImplementedException();
        }

        public int Delete(DateTime entity, bool persist = true)
        {
            throw new NotImplementedException();
        }

        public int Delete(int id, byte[] timeStamp, bool persist = true)
        {
            throw new NotImplementedException();
        }

        public int Delete(DateAndTime entity, bool persist = true)
        {
            throw new NotImplementedException();
        }

        public int DeleteRange(IEnumerable<DateTime> entities, bool persist = true)
        {
            throw new NotImplementedException();
        }

        public int DeleteRange(IEnumerable<DateAndTime> entities, bool persist = true)
        {
            throw new NotImplementedException();
        }

        public override bool Equals(object obj)
        {
            var repo = obj as IDateAndTimeRepo;
            return repo != null &&
                   Count == repo.Count &&
                   HasChanges == repo.HasChanges;
        }

        public DateTime Find(int? id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DateTime> GetAll()
        {
            throw new NotImplementedException();
        }

        public DateTime GetFirst()
        {
            throw new NotImplementedException();
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Count, HasChanges);
        }

        public IEnumerable<DateTime> GetRange(int skip, int take)
        {
            throw new NotImplementedException();
        }

        public int SaveChanges()
        {
            throw new NotImplementedException();
        }

        public int Update(DateTime entity, bool persist = true)
        {
            throw new NotImplementedException();
        }

        public int Update(DateAndTime entity, bool persist = true)
        {
            throw new NotImplementedException();
        }

        public int UpdateRange(IEnumerable<DateTime> entities, bool persist = true)
        {
            throw new NotImplementedException();
        }

        public int UpdateRange(IEnumerable<DateAndTime> entities, bool persist = true)
        {
            throw new NotImplementedException();
        }

        DateAndTime IRepo<DateAndTime>.Find(int? id)
        {
            throw new NotImplementedException();
        }

        IEnumerable<DateAndTime> IRepo<DateAndTime>.GetAll()
        {
            throw new NotImplementedException();
        }

        DateAndTime IRepo<DateAndTime>.GetFirst()
        {
            throw new NotImplementedException();
        }

        IEnumerable<DateAndTime> IRepo<DateAndTime>.GetRange(int skip, int take)
        {
            throw new NotImplementedException();
        }
    }
}
