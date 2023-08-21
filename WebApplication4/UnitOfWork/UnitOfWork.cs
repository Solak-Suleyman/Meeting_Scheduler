using WebApplication4.Models.Context;
using System.Transactions;
using Microsoft.EntityFrameworkCore.Internal;
using System.Data.Entity.Validation;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.ChangeTracking;
namespace WebApplication4.UnitOfWork
{
    public class UnitOfWork<TContext> : IUnitOfWork<TContext>, IDisposable where TContext: MeetingSchedulerContext, new()
    {
        private bool _disposed;
        public TContext Context { get; }
        private string _errorMessage = string.Empty;
        //The following Object is going to hold the Transaction Object
        private IDbContextTransaction _objTran;

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
            _objTran = Context.Database.BeginTransaction();
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
        //public override int SaveChanges()
        //{
        //    var now = DateTime.UtcNow;

        //    foreach (var changedEntity in ChangeTracker.Entries())
        //    {
        //        if (changedEntity.Entity is IEntityDate entity)
        //        {
        //            switch (changedEntity.State)
        //            {
        //                case EntityState.Added:
        //                    entity.CreatedDate = now;
        //                    entity.UpdatedDate = now;
        //                    break;

        //                case EntityState.Modified:
        //                    Entry(entity).Property(x => x.CreatedDate).IsModified = false;
        //                    entity.UpdatedDate = now;
        //                    break;
        //            }
        //        }
        //    }

        //    return Context.SaveChanges();
        //}
    }
}
