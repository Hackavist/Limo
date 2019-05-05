using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Limo.Models;

namespace Limo.DataBase.Repository
{
    public class DriverRepository : IRepository<Driver>
    {
        private readonly DataBaseContext dataBaseContext;
        public DriverRepository(string dbpath)
        {
            dataBaseContext = new DataBaseContext(dbpath);
        }

        public async Task<IEnumerable<Driver>> GetAllAsync()
        {
            try
            {
                var Drivers = await dataBaseContext.Drivers.ToListAsync();
                return Drivers;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return null;
            }
        }

        public async Task<Driver> GetByIdAsync(int id)
        {
            try
            {
                var Drivers = await dataBaseContext.Drivers.FindAsync(id);
                return Drivers;
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
                var Driver = dataBaseContext.Drivers.FindAsync(id);
                var entity = dataBaseContext.Remove(Driver);
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

        public async Task<Driver> InsertAsync(Driver entity)
        {
            entity.AddedDate = DateTime.Now;
            try
            {
                var Driver = await dataBaseContext.Drivers.AddAsync(entity);
                await dataBaseContext.SaveChangesAsync();
                if (Driver.State == EntityState.Added)
                    return entity;
                return null;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return null;
            }
        }

        public async Task<bool> SoftDeleteAsync(Driver entity)
        {
            entity.IsDeleted = true;
            entity.DeletedDate = DateTime.Now;
            try
            {
                var Driver = dataBaseContext.Update(entity);
                await dataBaseContext.SaveChangesAsync();
                if (Driver.State == EntityState.Modified)
                    return true;
                return false;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }
        }

        public async Task<Driver> UpdateAsync(Driver entity)
        {
            entity.ModifiedDate = DateTime.Now;
            try
            {
                var Driver = dataBaseContext.Update(entity);
                await dataBaseContext.SaveChangesAsync();
                if (Driver.State == EntityState.Modified)
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
