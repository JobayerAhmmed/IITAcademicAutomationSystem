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
        IEnumerable<Student> GetCurrentStudentsOfBatch(int batchId);
        IEnumerable<Student> GetAdmittedStudentsOfBatch(int batchId);
        Batch GetBatchById(int batchId);
        Batch GetBatchByBatchNo(int programId, int batchNo);
        Batch GetPreviousBatch(int programId);
        void CreateBatch(Batch batch);
        void EditBatch(Batch batch);
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
            return context.Batches.Where(b => b.ProgramId == programId && b.BatchStatus == "Active").OrderBy(a => a.BatchNo).ToList();
        }
        public IEnumerable<Batch> GetPassedBatches(int programId)
        {
            return context.Batches.Where(b => b.ProgramId == programId && b.BatchStatus == "Passed").OrderByDescending(a => a.BatchNo).ToList();
        }
        
        // Get current students of a batch
        public IEnumerable<Student> GetCurrentStudentsOfBatch(int batchId)
        {
            return context.Students.Where(s => s.BatchIdCurrent == batchId).OrderBy(b => b.CurrentRoll).ToList();
        }

        // Get admitted students of a batch
        public IEnumerable<Student> GetAdmittedStudentsOfBatch(int batchId)
        {
            return context.Students.Where(s => s.BatchIdOriginal == batchId).OrderBy(b => b.CurrentRoll).ToList();
        }
        public Batch GetBatchById(int batchId)
        {
            return context.Batches.Find(batchId);
        }

        public Batch GetBatchByBatchNo(int programId, int batchNo)
        {
            return context.Batches.Where(b => b.ProgramId == programId && b.BatchNo == batchNo).FirstOrDefault();
        }

        public Batch GetPreviousBatch(int programId)
        {
            return context.Batches.Where(b => b.ProgramId == programId).OrderByDescending(a => a.BatchNo).FirstOrDefault();
        }
        public void CreateBatch(Batch batch)
        {
            context.Batches.Add(batch);
        }
        public void EditBatch(Batch batch)
        {
            context.Entry(batch).State = EntityState.Modified;
        }

    }
}