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
        public IEnumerable<Batch> ViewActiveBatches(int programId)
        {
            return unitOfWork.BatchRepository.GetActiveBatches(programId);
        }
        public IEnumerable<Batch> ViewPassedBatches(int programId)
        {
            return unitOfWork.BatchRepository.GetPassedBatches(programId);
        }
        protected bool ValidateBatch(Batch batch)
        {

            return modelState.IsValid;
        }
        public void Dispose()
        {
            unitOfWork.Dispose();
        }
    }
}