using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Limo.Models;

namespace Limo.DataBase.Repository
{
    public class CreditcardRepository : IRepository<CreditCard>
    {
        private readonly DataBaseContext dataBaseContext;
        public CreditcardRepository(string dbpath)
        {
            dataBaseContext = new DataBaseContext(dbpath);
        }

        public async Task<IEnumerable<CreditCard>> GetAllAsync()
        {
            try
            {
                var CreditCards = await dataBaseContext.CreditCards.ToListAsync();
                return CreditCards;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return null;
            }
        }

        public async Task<CreditCard> GetByIdAsync(int id)
        {
            try
            {
                var CreditCards = await dataBaseContext.CreditCards.FindAsync(id);
                return CreditCards;
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
                var CreditCard = dataBaseContext.CreditCards.FindAsync(id);
                var entity = dataBaseContext.Remove(CreditCard);
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

        public async Task<CreditCard> InsertAsync(CreditCard entity)
        {
            entity.AddedDate = DateTime.Now;
            try
            {
                var CreditCard = await dataBaseContext.CreditCards.AddAsync(entity);
                await dataBaseContext.SaveChangesAsync();
                if (CreditCard.State == EntityState.Added)
                    return entity;
                return null;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return null;
            }
        }

        public async Task<bool> SoftDeleteAsync(CreditCard entity)
        {
            entity.IsDeleted = true;
            entity.DeletedDate = DateTime.Now;
            try
            {
                var CreditCard = dataBaseContext.Update(entity);
                await dataBaseContext.SaveChangesAsync();
                if (CreditCard.State == EntityState.Modified)
                    return true;
                return false;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }
        }

        public async Task<CreditCard> UpdateAsync(CreditCard entity)
        {
            entity.ModifiedDate = DateTime.Now;
            try
            {
                var CreditCard = dataBaseContext.Update(entity);
                await dataBaseContext.SaveChangesAsync();
                if (CreditCard.State == EntityState.Modified)
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
