using IITAcademicAutomationSystem.DAL;
using IITAcademicAutomationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IITAcademicAutomationSystem.Areas.One.Repositories
{
    public interface IBatchCoordinatorRepository
    {
        void Create(BatchCoordinator coordinator);
        void Edit(BatchCoordinator coordinator);
        BatchCoordinator GetBatchCoordinator(int batchId);
        BatchCoordinator GetLastBatchCoordinator(int batchId);
        IEnumerable<BatchCoordinator> GetBatchCoordinatorOfBatch(int batchId);
    }
    public class BatchCoordinatorRepository : IBatchCoordinatorRepository
    {
        private ApplicationDbContext context;
        public BatchCoordinatorRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void Create(BatchCoordinator coordinator)
        {
            context.BatchCoordinators.Add(coordinator);
        }
        public void Edit(BatchCoordinator coordinator)
        {
            context.Entry(coordinator).State = System.Data.Entity.EntityState.Modified;
        }
        public BatchCoordinator GetBatchCoordinator(int batchId)
        {
            return context.BatchCoordinators.Where(c => c.BatchId == batchId
                && c.StartDate == c.EndDate).FirstOrDefault();
        }
        public BatchCoordinator GetLastBatchCoordinator(int batchId)
        {
            return context.BatchCoordinators.Where(c =>
                c.BatchId == batchId && c.StartDate == c.EndDate).FirstOrDefault();
        }
        public IEnumerable<BatchCoordinator> GetBatchCoordinatorOfBatch(int batchId)
        {
            return context.BatchCoordinators.Where(c => c.BatchId == batchId).OrderByDescending(s => s.StartDate).ToList();
        }
    }
}