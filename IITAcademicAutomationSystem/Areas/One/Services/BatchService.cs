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
        bool EditBatch(Batch batch);
        IEnumerable<Batch> ViewActiveBatches(int programId);
        IEnumerable<Batch> ViewPassedBatches(int programId);
        IEnumerable<Student> ViewCurrentStudentsOfBatch(int batchId);
        IEnumerable<Student> ViewAdmittedStudentsOfBatch(int batchId);
        Batch ViewBatch(int id);
        Batch GetPreviousBatch(int programId);
        IEnumerable<Batch> GetNextBatches(int batchId);
        bool BatchExist(int batchNo, int programId);
        ApplicationUser GetBatchCoordinator(int batchId);
        BatchCoordinator GetLastBatchCoordinator(int batchId);
        IEnumerable<BatchCoordinator> GetBatchCoordinatorOfBatch(int batchId);
        bool AssignCoordinator(BatchCoordinator coordinator);
        bool EditBatchCoordinator(BatchCoordinator coordinator);
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

        // Edit batch
        public bool EditBatch(Batch batch)
        {
            try
            {
                unitOfWork.BatchRepository.EditBatch(batch);
                unitOfWork.Save();
                return true;
            }
            catch (DataException)
            {
                modelState.AddModelError("", "Unable to save, try again.");
                return false;
            }
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

        // Get next batches of a batch
        public IEnumerable<Batch> GetNextBatches(int batchId)
        {
            return unitOfWork.BatchRepository.GetNextBatches(batchId);
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

        public ApplicationUser GetBatchCoordinator(int batchId)
        {
            BatchCoordinator coordinator = unitOfWork.BatchCoordinatorRepository.GetBatchCoordinator(batchId);
            ApplicationUser user = null;
            if (coordinator != null)
            {
                user = unitOfWork.UserRepository.GetUserById(coordinator.TeacherId);
            }
            return user;
        }
        public BatchCoordinator GetLastBatchCoordinator(int batchId)
        {
            return unitOfWork.BatchCoordinatorRepository.GetLastBatchCoordinator(batchId);
        }

        public IEnumerable<BatchCoordinator> GetBatchCoordinatorOfBatch(int batchId)
        {
            return unitOfWork.BatchCoordinatorRepository.GetBatchCoordinatorOfBatch(batchId);
        }
        public bool AssignCoordinator(BatchCoordinator coordinator)
        {
            try
            {
                unitOfWork.BatchCoordinatorRepository.Create(coordinator);
                unitOfWork.Save();
                return true;
            }
            catch (Exception)
            {
                modelState.AddModelError("", "Unable to save, try again.");
                return false;
            }
        }
        public bool EditBatchCoordinator(BatchCoordinator coordinator)
        {
            try
            {
                unitOfWork.BatchCoordinatorRepository.Edit(coordinator);
                unitOfWork.Save();
                return true;
            }
            catch (Exception)
            {
                modelState.AddModelError("", "Unable to save, try again.");
                return false;
            }
        }
        public void Dispose()
        {
            unitOfWork.Dispose();
        }
    }
}