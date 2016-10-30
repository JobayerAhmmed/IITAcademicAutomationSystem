using IITAcademicAutomationSystem.Areas.Two.Repo;
using IITAcademicAutomationSystem.Areas.Two.RepoImpl;
using IITAcademicAutomationSystem.Areas.Two.RequestDto;
using IITAcademicAutomationSystem.Areas.Two.Service;
using IITAcademicAutomationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IITAcademicAutomationSystem.Areas.Two.ResponseDto;

namespace IITAcademicAutomationSystem.Areas.Two.ServiceImpl
{
    public class NoticeManagementSerImpl : NoticeManagementSerI
    {
        AcademicFileRepoI academicFileRepository = new AcademicFileRepoImpl();
        UtilitySerI utilityService = new UtilitySerImpl();

        

        public void uploadNotice(UploadNoticeReqDto uploadNoticeReqDto)
        {
            AcademicFile file = new AcademicFile();
            try
            {
                file.type = "notice";
                file.title = uploadNoticeReqDto.title;
                file.description = uploadNoticeReqDto.description;
                file.path = uploadNoticeReqDto.path;

                DateTime thisDay = DateTime.Today;
                file.date = thisDay.ToString("d");
                file.uploaderId = utilityService.getIdOfLoggedInProgramOfficer();
                file.isDeleted = false;

                if(uploadNoticeReqDto.programId== -1)
                {
                    if (uploadNoticeReqDto.semesterId == -1)
                    {
                        file.isForAll = true;

                        file.programId = -1;
                        file.semesterId = uploadNoticeReqDto.semesterId;
                        file.batchId = -1;
                    }
                    else if(uploadNoticeReqDto.semesterId != -1)
                    {
                        file.isForAll = true;

                        file.programId = -1;
                        file.semesterId = 0;
                        file.batchId = -1;
                    }
                    
                }

                else
                {
                    file.isForAll = false;

                    file.programId = uploadNoticeReqDto.programId;
                    file.semesterId = uploadNoticeReqDto.semesterId;
                    file.batchId = uploadNoticeReqDto.batchId;
                }               

                academicFileRepository.saveAcademicFile(file);
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public NoticeAllResDto getNotices_teacherProgramOfficer(int programId, int semesterId)
        {
            NoticeAllResDto responseToResturn = new NoticeAllResDto();
            try
            {              

                var batch = utilityService.getBatch(programId,semesterId);
                int batchId = batch.id;

                string type = "notice";

                List<AcademicFile> noticeList = academicFileRepository.getAcademicFiles(programId,semesterId,batchId,type);

                List<NoticeIndividualResDto> noticesToReturn = new List<NoticeIndividualResDto>();

                responseToResturn.programId = programId;
                responseToResturn.semesterId = semesterId;
                responseToResturn.batchId = batchId;
                for (int i=0;i< noticeList.Count; i++)
                {
                    NoticeIndividualResDto tempNotice = new NoticeIndividualResDto();
                    tempNotice.id = noticeList.ElementAt(i).Id;
                    tempNotice.title = noticeList.ElementAt(i).title;
                    tempNotice.description = noticeList.ElementAt(i).description;
                    tempNotice.path = noticeList.ElementAt(i).path;
                    tempNotice.date = noticeList.ElementAt(i).date;
                    noticesToReturn.Add(tempNotice);
                }
                responseToResturn.notices = noticesToReturn.ToArray();

                return responseToResturn;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public NoticeAllResDto getNotices_student()
        {
            NoticeAllResDto responseToResturn = new NoticeAllResDto();
            try
            {
                var studentInfo = utilityService.getProgramSemesterBatchOfLoggedInStudent();
                int programId = studentInfo.program.id;
                int semesterId = studentInfo.semester.id;
                int batchId = studentInfo.batch.id;

                string type = "notice";

                List<AcademicFile> noticeList = academicFileRepository.getAcademicFiles(programId, semesterId, batchId, type);

                List<NoticeIndividualResDto> noticesToReturn = new List<NoticeIndividualResDto>();

                responseToResturn.programId = programId;
                responseToResturn.semesterId = semesterId;
                responseToResturn.batchId = batchId;
                for (int i = 0; i < noticeList.Count; i++)
                {
                    NoticeIndividualResDto tempNotice = new NoticeIndividualResDto();
                    tempNotice.id = noticeList.ElementAt(i).Id;
                    tempNotice.title = noticeList.ElementAt(i).title;
                    tempNotice.description = noticeList.ElementAt(i).description;
                    tempNotice.path = noticeList.ElementAt(i).path;
                    tempNotice.date = noticeList.ElementAt(i).date;
                    noticesToReturn.Add(tempNotice);
                }
                responseToResturn.notices = noticesToReturn.ToArray();

                return responseToResturn;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public void editNotice(EditNoticeReqDto editNoticeReqDto)
        {
            try
            {
                AcademicFile file = academicFileRepository.getAcademicFile(editNoticeReqDto.id);
                if (file != null)
                {
                    file.title = editNoticeReqDto.title;
                    file.description = editNoticeReqDto.description;
                    academicFileRepository.updateAcademicFile(file);
                }
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public void deleteNotice(int noticeId)
        {
            try
            {
                AcademicFile file = academicFileRepository.getAcademicFile(noticeId);
                if (file != null)
                {
                    file.isDeleted = true;
                    academicFileRepository.updateAcademicFile(file);
                }
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}