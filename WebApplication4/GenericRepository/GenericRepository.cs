﻿using Microsoft.Build.Construction;

using WebApplication4.Models.Context;

using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using WebApplication4.UnitOfWork;
using WebApplication4.Models;
using Microsoft.EntityFrameworkCore;
using WebApplication4.Models.Entity;

namespace WebApplication4.GenericRepository
{
    public class GenericRepository<T> : UserMeeting ,IGenericRepository<T>, IDisposable where T : class
    {

        public DbSet<T> _entities;
        private string _errorMessage = string.Empty;
        private bool _isDisposed = false;

        public GenericRepository(IUnitOfWork<MeetingSchedulerContext> unitOfWork)
            : this(unitOfWork.Context)
        {
        }

        public GenericRepository(MeetingSchedulerContext context)
        {
            Context = context;
        }

        public MeetingSchedulerContext Context { get; set; }
        protected virtual DbSet<T> Entities
        {
            get { _entities = Context.Set<T>();
                return _entities;
            }
        }

        public void Delete(T obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }
            if (Context == null || _isDisposed)
            {
                Context = new MeetingSchedulerContext();
            }
            Entities.Remove(obj);
        }

        public void Dispose()
        {
            if (Context != null)
                Context.Dispose();
            _isDisposed = true;
        }
        public virtual IEnumerable<T> GetAll()
        {
            return Entities.ToList();
        }
        public virtual T GetById(object id)
        {
            return Entities.Find(id);
        }

        public void Insert(T obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }
            if (Context==null ||_isDisposed)
            {
                Context = new MeetingSchedulerContext();
            }
            Entities.Add(obj);
        }



        public void Update(T obj)
        {
            if(obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }
            if (Context==null ||_isDisposed)
            {
                Context=new MeetingSchedulerContext();
            }
            Entities.Attach(obj);
            Context.Entry(obj).State = EntityState.Modified;
        }
    }
}
