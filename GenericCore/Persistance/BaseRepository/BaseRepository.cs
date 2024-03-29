﻿using GenericCore.Persistance.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace GenericCore.Persistance.BaseRepository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly AppDbContext context;

        public BaseRepository(AppDbContext context)
        {
            this.context = context;
        }

        public T Add(T item)
        {
            context.Add(item);
            context.SaveChanges();
            return item;
        }

        public IEnumerable<T> GetAll()
        {
            return context.Set<T>().AsNoTracking().AsEnumerable();
        }

        public void Remove(int id)
        {
            T item = GetById(id);
            context.Set<T>().Remove(item);
            context.SaveChanges();
        }

        public T GetById(int Id)
        {
            return context.Set<T>().Find(Id);
        }

        public T Update(T item)
        {
            context.Set<T>().Update(item);
            context.SaveChangesAsync();
            return item;
        }
    }
}
