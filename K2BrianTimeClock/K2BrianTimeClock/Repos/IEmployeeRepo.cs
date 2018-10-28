using K2BrianTimeClock.DAL.Repos.Base;
using System;
using System.Collections.Generic;
using System.Text;
using TimeClock.Models.Entities;

namespace K2BrianTimeClock.DAL.Repos
{
    class IEmployeeRepo : IRepo<Employee>
    {
        public int Count => throw new NotImplementedException();

        public bool HasChanges => throw new NotImplementedException();

        public int Add(Employee entity, bool persist = true)
        {
            throw new NotImplementedException();
        }

        public int AddRange(IEnumerable<Employee> entities, bool persist = true)
        {
            throw new NotImplementedException();
        }

        public int Delete(Employee entity, bool persist = true)
        {
            throw new NotImplementedException();
        }

        public int Delete(int id, byte[] timeStamp, bool persist = true)
        {
            throw new NotImplementedException();
        }

        public int DeleteRange(IEnumerable<Employee> entities, bool persist = true)
        {
            throw new NotImplementedException();
        }

        public Employee Find(int? id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Employee> GetAll()
        {
            throw new NotImplementedException();
        }

        public Employee GetFirst()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Employee> GetRange(int skip, int take)
        {
            throw new NotImplementedException();
        }

        public int SaveChanges()
        {
            throw new NotImplementedException();
        }

        public int Update(Employee entity, bool persist = true)
        {
            throw new NotImplementedException();
        }

        public int UpdateRange(IEnumerable<Employee> entities, bool persist = true)
        {
            throw new NotImplementedException();
        }
    }
}
