using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Limo.Models;

namespace Limo.DataBase.Repository
{
    public class UserRepository : IRepository<User>
    {
        private readonly DataBaseContext dataBaseContext;
        public UserRepository(string dbpath)
        {
            dataBaseContext = new DataBaseContext(dbpath);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            try
            {
                var Users = await dataBaseContext.Users.ToListAsync();
                return Users;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return null;
            }
        }

        public async Task<User> GetByIdAsync(int id)
        {
            try
            {
                var Users = await dataBaseContext.Users.FindAsync(id);
                return Users;
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
                var User = dataBaseContext.Users.FindAsync(id);
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

        public async Task<User> InsertAsync(User entity)
        {
            entity.AddedDate = DateTime.Now;
            try
            {
                var User = await dataBaseContext.Users.AddAsync(entity);
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

        public async Task<bool> SoftDeleteAsync(User entity)
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

        public async Task<User> UpdateAsync(User entity)
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
