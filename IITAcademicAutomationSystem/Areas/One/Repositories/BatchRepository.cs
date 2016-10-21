using IITAcademicAutomationSystem.DAL;
using IITAcademicAutomationSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace IITAcademicAutomationSystem.Areas.One.Repositories
{
    public interface IBatchRepository
    {
        IEnumerable<Batch> GetActiveBatches(int programId);
        IEnumerable<Batch> GetPassedBatches(int programId);
        Batch GetBatchById(int programId, int batchId);
        void CreateBatch(Batch batch);
    }
    public class BatchRepository : IBatchRepository
    {
        private ApplicationDbContext context;
        public BatchRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public IEnumerable<Batch> GetActiveBatches(int programId)
        {
            return context.Batches.Where(b => b.ProgramId == programId && b.BatchStatus == "Active").OrderByDescending(a => a.BatchNo).ToList();
        }
        public IEnumerable<Batch> GetPassedBatches(int programId)
        {
            return context.Batches.Where(b => b.ProgramId == programId && b.BatchStatus == "Passed").OrderByDescending(a => a.BatchNo).ToList();
        }
        public Batch GetBatchById(int programId, int batchId)
        {
            return context.Batches.Find(programId, batchId);
        }
        public void CreateBatch(Batch batch)
        {
            context.Batches.Add(batch);
        }
    }
}