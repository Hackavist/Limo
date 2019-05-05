using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Limo.Models;

namespace Limo.DataBase.Repository
{
    class CarsRepository : IRepository<Car>
    {
        private readonly DataBaseContext dataBaseContext;
        public CarsRepository(string dbpath)
        {
            dataBaseContext = new DataBaseContext(dbpath);
        }
        public async Task<IEnumerable<Car>> GetAllAsync()
        {
            try
            {
                var Cars = await dataBaseContext.Cars.ToListAsync();
                return Cars;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return null;
            }
        }

        public async Task<Car> GetByIdAsync(int id)
        {
            try
            {
                var Cars = await dataBaseContext.Cars.FindAsync(id);
                return Cars;
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
                var User = dataBaseContext.Cars.FindAsync(id);
                var entity = dataBaseContext.Remove(User);
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

        public async Task<Car> InsertAsync(Car entity)
        {
            entity.AddedDate = DateTime.Now;
            try
            {
                var User = await dataBaseContext.Cars.AddAsync(entity);
                await dataBaseContext.SaveChangesAsync();
                if (User.State == EntityState.Added)
                    return entity;
                return null;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return null;
            }
        }

        public async Task<bool> SoftDeleteAsync(Car entity)
        {
            entity.IsDeleted = true;
            entity.DeletedDate = DateTime.Now;
            try
            {
                var User = dataBaseContext.Update(entity);
                await dataBaseContext.SaveChangesAsync();
                if (User.State == EntityState.Modified)
                    return true;
                return false;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }
        }

        public async Task<Car> UpdateAsync(Car entity)
        {
            entity.ModifiedDate = DateTime.Now;
            try
            {
                var User = dataBaseContext.Update(entity);
                await dataBaseContext.SaveChangesAsync();
                if (User.State == EntityState.Modified)
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
