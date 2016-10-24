using IITAcademicAutomationSystem.DAL;
using IITAcademicAutomationSystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IITAcademicAutomationSystem.Areas.One.Services
{
    public interface IBatchService
    {
        bool CreateBatch(Batch batch);
        IEnumerable<Batch> ViewActiveBatches(int programId);
        IEnumerable<Batch> ViewPassedBatches(int programId);
        IEnumerable<Student> ViewCurrentStudentsOfBatch(int batchId);
        IEnumerable<Student> ViewAdmittedStudentsOfBatch(int batchId);
        Batch ViewBatch(int id);
        Batch GetPreviousBatch(int programId);
        bool BatchExist(int batchNo, int programId);
        void Dispose();
    }
    public class BatchService : IBatchService
    {
        private ModelStateDictionary modelState;
        public UnitOfWork unitOfWork;
        public BatchService(ModelStateDictionary modelState, UnitOfWork unitOfWork)
        {
            this.modelState = modelState;
            this.unitOfWork = unitOfWork;
        }

        // Create batch
        public bool CreateBatch(Batch batch)
        {
            if (!ValidateBatch(batch))
                return false;

            try
            {
                unitOfWork.BatchRepository.CreateBatch(batch);
                unitOfWork.Save();
            }
            catch (DataException)
            {
                modelState.AddModelError("", "Unable to save, try again.");
                return false;
            }
            return true;
        }

        // View batch
        public Batch ViewBatch(int id)
        {
            return unitOfWork.BatchRepository.GetBatchById(id);
        }

        // View active batches
        public IEnumerable<Batch> ViewActiveBatches(int programId)
        {
            return unitOfWork.BatchRepository.GetActiveBatches(programId);
        }

        // View passed batches
        public IEnumerable<Batch> ViewPassedBatches(int programId)
        {
            return unitOfWork.BatchRepository.GetPassedBatches(programId);
        }

        // View current students
        public IEnumerable<Student> ViewCurrentStudentsOfBatch(int batchId)
        {
            return unitOfWork.BatchRepository.GetCurrentStudentsOfBatch(batchId);
        }

        // View admitted students
        public IEnumerable<Student> ViewAdmittedStudentsOfBatch(int batchId)
        {
            return unitOfWork.BatchRepository.GetAdmittedStudentsOfBatch(batchId);
        }

        // Get previous batch
        public Batch GetPreviousBatch(int programId)
        {
            return unitOfWork.BatchRepository.GetPreviousBatch(programId);
        }

        // Validate batch
        protected bool ValidateBatch(Batch batch)
        {

            return modelState.IsValid;
        }

        // Batch Exist
        public bool BatchExist(int batchNo, int programId)
        {
            Batch batch = unitOfWork.BatchRepository.GetBatchByBatchNo(programId, batchNo);
            if (batch == null)
                return false;
            return true;
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }
    }
}