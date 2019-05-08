using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Limo.Models;

namespace Limo.DataBase.Repository
{
    public class RequestRepository : IRepository<Request>
    {
        private readonly DataBaseContext dataBaseContext;
        public RequestRepository(string dbpath)
        {
            dataBaseContext = new DataBaseContext(dbpath);
        }

        public async Task<IEnumerable<Request>> GetAllAsync()
        {
            try
            {
                var Requests = await dataBaseContext.Requests.ToListAsync();
                return Requests;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return null;
            }
        }

        public async Task<Request> GetByIdAsync(int id)
        {
            try
            {
                var Requests = await dataBaseContext.Requests.FindAsync(id);
                return Requests;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return null;
            }
        }

        public async Task<bool> HardDeleteAsync(int id)
        {
            try
            {
                var Request = dataBaseContext.Requests.FindAsync(id);
                var entity = dataBaseContext.Remove(Request);
                await dataBaseContext.SaveChangesAsync();
                if (entity.State == EntityState.Deleted)
                    return true;
                return false;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }
        }

        public async Task<Request> InsertAsync(Request entity)
        {
            entity.AddedDate = DateTime.Now;

            try
            {
                var Request = await dataBaseContext.Requests.AddAsync(entity);
                await dataBaseContext.SaveChangesAsync();
                if (Request.State == EntityState.Added)
                    return entity;
                return null;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return null;
            }
        }

        public async Task<bool> SoftDeleteAsync(Request entity)
        {
            entity.IsDeleted = true;
            entity.DeletedDate = DateTime.Now;
            try
            {
                var Request = dataBaseContext.Update(entity);
                await dataBaseContext.SaveChangesAsync();
                if (Request.State == EntityState.Modified)
                    return true;
                return false;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }
        }

        public async Task<Request> UpdateAsync(Request entity)
        {
            entity.ModifiedDate = DateTime.Now;
            try
            {
                var Request = dataBaseContext.Update(entity);
                await dataBaseContext.SaveChangesAsync();
                if (Request.State == EntityState.Modified)
                    return entity;
                return null;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return null;
            }
        }
    }
}
