using IITAcademicAutomationSystem.Areas.Two.Repo;
using IITAcademicAutomationSystem.Areas.Two.RepoImpl;
using IITAcademicAutomationSystem.Areas.Two.ResponseDto;
using IITAcademicAutomationSystem.Areas.Two.Service;
using IITAcademicAutomationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static IITAcademicAutomationSystem.Areas.Two.RequestDto.AcademicCalendarReqDto;

namespace IITAcademicAutomationSystem.Areas.Two.ServiceImpl
{
    public class AcademicCalendarManagementSerImpl: AcademicCalendarManagementSerI
    {
        AcademicFileRepoI academicFileRepository = new AcademicFileRepoImpl();
        UtilitySerI utilityService = new UtilitySerImpl();

        public void uploadAcademicCalendar(UploadAcademicCalendarReqDto uploadAcademicCalendarReqDto,string uploaderId)
        {
            AcademicFile file = new AcademicFile();
            try
            {
                file.type = "academicCalendar";
                file.title = uploadAcademicCalendarReqDto.title;
                file.description = uploadAcademicCalendarReqDto.description;
                file.path = uploadAcademicCalendarReqDto.path;

                DateTime thisDay = DateTime.Today;
                file.date = thisDay.ToString("d");
                file.uploaderId = uploaderId;
                file.isDeleted = false;

                if (uploadAcademicCalendarReqDto.programId == -1)
                {
                    file.isForAll = true;

                    file.programId = -1;
                    file.semesterId = -1;
                    file.batchId = -1;
                }

                else
                {
                    file.isForAll = false;

                    file.programId = uploadAcademicCalendarReqDto.programId;
                    file.semesterId = uploadAcademicCalendarReqDto.semesterId;
                    file.batchId = uploadAcademicCalendarReqDto.batchId;
                }



                academicFileRepository.saveAcademicFile(file);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public AcademicCalendarAllResDto getAcademicCalendars_teacherProgramOfficer(int programId, int semesterId)
        {
            AcademicCalendarAllResDto responseToResturn = new AcademicCalendarAllResDto();
            try
            {

                var batch = utilityService.getBatch(programId, semesterId);
                int batchId = batch.id;

                string type = "academicCalendar";



                responseToResturn.programId = programId;
                responseToResturn.semesterId = semesterId;
                responseToResturn.batchId = batchId;

                AcademicFile academicCalendar = academicFileRepository.getAcademicCalendar(programId, semesterId, batchId, type);

                if (academicCalendar != null)
                {
                    AcademicCalendarIndividualResDto tempAcademicCalendar = new AcademicCalendarIndividualResDto();
                    tempAcademicCalendar.id = academicCalendar.Id;
                    tempAcademicCalendar.title = academicCalendar.title;
                    tempAcademicCalendar.description = academicCalendar.description;
                    tempAcademicCalendar.path = academicCalendar.path;
                    tempAcademicCalendar.date = academicCalendar.date;

                    responseToResturn.academicCalendar = tempAcademicCalendar;
                }
                

                return responseToResturn;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public AcademicCalendarAllResDto getAcademicCalendars_student(int studentId)
        {
            AcademicCalendarAllResDto responseToResturn = new AcademicCalendarAllResDto();
            try
            {
                var studentInfo = utilityService.getProgramSemesterBatchOfLoggedInStudent(studentId);
                int programId = studentInfo.program.id;
                int semesterId = studentInfo.semester.id;
                int batchId = studentInfo.batch.id;

                string type = "academicCalendar";



                responseToResturn.programId = programId;
                responseToResturn.semesterId = semesterId;
                responseToResturn.batchId = batchId;

                AcademicFile academicCalendar = academicFileRepository.getAcademicCalendar(programId, semesterId, batchId, type);

                if (academicCalendar!=null){
                    AcademicCalendarIndividualResDto tempAcademicCalendar = new AcademicCalendarIndividualResDto();
                    tempAcademicCalendar.id = academicCalendar.Id;
                    tempAcademicCalendar.title = academicCalendar.title;
                    tempAcademicCalendar.description = academicCalendar.description;
                    tempAcademicCalendar.path = academicCalendar.path;
                    tempAcademicCalendar.date = academicCalendar.date;

                    responseToResturn.academicCalendar = tempAcademicCalendar;
                }
                

                return responseToResturn;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void editAcademicCalendar(EditAcademicCalendarReqDto editAcademicCalendarReqDto)
        {
            try
            {
                AcademicFile file = academicFileRepository.getAcademicFile(editAcademicCalendarReqDto.id);
                if (file != null)
                {
                    file.title = editAcademicCalendarReqDto.title;
                    file.description = editAcademicCalendarReqDto.description;
                    academicFileRepository.updateAcademicFile(file);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void deleteAcademicCalendar(int academicCalendarId)
        {
            try
            {
                AcademicFile file = academicFileRepository.getAcademicFile(academicCalendarId);
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

        public bool checkIfAcademicCalendarUploaded(int programId, int semesterId)
        {
            try
            {
                string type = "academicCalendar";
                var batch = utilityService.getBatch(programId, semesterId);
                AcademicFile academicCalendar = academicFileRepository.getAcademicCalendar(programId, semesterId, batch.id, type);
                if (academicCalendar != null)
                    return true;
                else
                {
                    return false;
                }
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}