using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lesson3.DAL.Entities;
using Lesson3.DAL.Repository.DataBase;
using Lesson3.DAL.Repository.Interfaces;
using Lesson3.DAL.Repository.Context;
using Microsoft.EntityFrameworkCore;

namespace Lesson3.DAL.Repository
{
    public class PersonsRepository: IRepository<PersonEntity>
    {
        private readonly Context.Context _context;

        public PersonsRepository(Context.Context context)
        {
            _context = context;            
        }        

        public async Task<PersonEntity> Get(int id)
        {
            var result = await _context.Persons.FindAsync(id);

            if (result == null || result.IsDelete == true)
            {
                throw new ArgumentException("Такого человека не найдено");
            }

            return result;
        }

        public async Task Add(PersonEntity item)
        {
            await _context.Persons.AddAsync(item);
            await _context.SaveChangesAsync();
        }
        

        public async Task Update(PersonEntity item)
        {
           _context.Persons.Update(item);
            await _context.SaveChangesAsync();

        }

        public async Task Delete(int id)
        {
            var item =  _context.Persons.Find(id);
            item.IsDelete = true;
            await _context.SaveChangesAsync();           

        }

        public  Task<PersonEntity> GetByName(string name)
        {
            var result =  _context.Persons.Single(x => x.FirstName == name);
            return Task.FromResult(result);
        }

        public async Task<IEnumerable<PersonEntity>> Select(int skip, int take) =>        
            await _context.Persons.Skip(skip).Take(take).Select(x => x).ToListAsync();


        public async Task<IEnumerable<PersonEntity>> GetAll() =>        
             await _context.Persons.Where(x => x.IsDelete == false).ToListAsync();
        
    }

    
}