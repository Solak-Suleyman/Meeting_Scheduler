using System.Data.Entity;
using WebApplication4.Models.Context;
using System.Transactions;

using System.Data.Entity.Validation;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;

namespace WebApplication4.UnitOfWork
{
    public class UnitOfWork<TContext> : IUnitOfWork<TContext>, IDisposable where TContext: MeetingSchedulerContext, new()
    {
        private bool _disposed;
        public TContext Context { get; }
        private string _errorMessage = string.Empty;
        //The following Object is going to hold the Transaction Object
        private DbContextTransaction _objTran;

        public UnitOfWork()
        {
            Context=new TContext();
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
                if (disposing)
                    Context.Dispose();
            _disposed = true;
        }
        
        public void Dispose()
        {
            Dispose(true);

        }

        public void CreateTransaction()
        {
            _objTran = (DbContextTransaction)Context.Database.BeginTransaction();
        }

        public void Commit()
        {
            _objTran.Commit();
        }

        public void Rollback()
        {
            _objTran.Rollback();
            _objTran.Dispose();
        }

        public void Save()
        {
            try
            {
                //Calling DbContext Class SaveChanges method 
                Context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        _errorMessage = _errorMessage + $"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage} {Environment.NewLine}";
                    }
                }
                throw new Exception(_errorMessage, dbEx);
            }
        }
    }
}
