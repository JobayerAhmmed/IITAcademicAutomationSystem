using IITAcademicAutomationSystem.Areas.Two.Repo;
using IITAcademicAutomationSystem.Areas.Two.RepoImpl;
using IITAcademicAutomationSystem.Areas.Two.RequestDto;
using IITAcademicAutomationSystem.Areas.Two.ResponseDto;
using IITAcademicAutomationSystem.Areas.Two.Service;
using IITAcademicAutomationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IITAcademicAutomationSystem.Areas.Two.ServiceImpl
{
    public class RoutineManagementSerImpl : RoutineManagementSerI
    {
        AcademicFileRepoI academicFileRepository = new AcademicFileRepoImpl();
        UtilitySerI utilityService = new UtilitySerImpl();

        public void uploadRoutine(UploadRoutineReqDto uploadRoutineReqDto)
        {
            AcademicFile file = new AcademicFile();
            try
            {
                file.type = "routine";
                file.title = uploadRoutineReqDto.title;
                file.description = uploadRoutineReqDto.description;
                file.path = uploadRoutineReqDto.path;

                DateTime thisDay = DateTime.Today;
                file.date = thisDay.ToString("d");
                file.uploaderId = utilityService.getIdOfLoggedInProgramOfficer();
                file.isDeleted = false;

                if (uploadRoutineReqDto.programId == -1)
                {
                    file.isForAll = true;

                    file.programId = -1;
                    file.semesterId = -1;
                    file.batchId = -1;
                }

                else
                {
                    file.isForAll = false;

                    file.programId = uploadRoutineReqDto.programId;
                    file.semesterId = uploadRoutineReqDto.semesterId;
                    file.batchId = uploadRoutineReqDto.batchId;
                }



                academicFileRepository.saveAcademicFile(file);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public RoutineAllResDto getRoutines_teacherProgramOfficer(int programId, int semesterId)
        {
            RoutineAllResDto responseToResturn = new RoutineAllResDto();
            try
            {

                var batch = utilityService.getBatch(programId, semesterId);
                int batchId = batch.id;

                string type = "routine";



                responseToResturn.programId = programId;
                responseToResturn.semesterId = semesterId;
                responseToResturn.batchId = batchId;

                AcademicFile routine = academicFileRepository.getRoutine(programId, semesterId, batchId, type);

                if (routine != null)
                {
                    RoutineIndividualResDto tempRoutine = new RoutineIndividualResDto();
                    tempRoutine.id = routine.Id;
                    tempRoutine.title = routine.title;
                    tempRoutine.description = routine.description;
                    tempRoutine.path = routine.path;
                    tempRoutine.date = routine.date;

                    responseToResturn.routine = tempRoutine;
                }


                return responseToResturn;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public RoutineAllResDto getRoutines_student()
        {
            RoutineAllResDto responseToResturn = new RoutineAllResDto();
            try
            {
                var studentInfo = utilityService.getProgramSemesterBatchOfLoggedInStudent();
                int programId = studentInfo.program.id;
                int semesterId = studentInfo.semester.id;
                int batchId = studentInfo.batch.id;

                string type = "routine";



                responseToResturn.programId = programId;
                responseToResturn.semesterId = semesterId;
                responseToResturn.batchId = batchId;

                AcademicFile routine = academicFileRepository.getRoutine(programId, semesterId, batchId, type);

                if (routine != null)
                {
                    RoutineIndividualResDto tempRoutine = new RoutineIndividualResDto();
                    tempRoutine.id = routine.Id;
                    tempRoutine.title = routine.title;
                    tempRoutine.description = routine.description;
                    tempRoutine.path = routine.path;
                    tempRoutine.date = routine.date;

                    responseToResturn.routine = tempRoutine;
                }


                return responseToResturn;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void editRoutine(EditRoutineReqDto editRoutineReqDto)
        {
            try
            {
                AcademicFile file = academicFileRepository.getAcademicFile(editRoutineReqDto.id);
                if (file != null)
                {
                    file.title = editRoutineReqDto.title;
                    file.description = editRoutineReqDto.description;
                    academicFileRepository.updateAcademicFile(file);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void deleteRoutine(int routineId)
        {
            try
            {
                AcademicFile file = academicFileRepository.getAcademicFile(routineId);
                if (file != null)
                {
                    file.isDeleted = true;
                    academicFileRepository.updateAcademicFile(file);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool checkIfRoutineUploaded(int programId, int semesterId)
        {
            try
            {
                string type = "routine";
                var batch = utilityService.getBatch(programId, semesterId);
                AcademicFile routine = academicFileRepository.getRoutine(programId, semesterId, batch.id, type);
                if (routine != null)
                    return true;
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}