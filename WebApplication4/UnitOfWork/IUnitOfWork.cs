using WebApplication4.Models.Context;

namespace WebApplication4.UnitOfWork
{
    public interface IUnitOfWork<out TContext> where TContext : MeetingSchedulerContext, new()
    {
        TContext Context { get; }
        void CreateTransaction();
        void Commit();
        void Rollback();
        void Save();
    }
}
