using IITAcademicAutomationSystem.Areas.Two.RequestDto;
using IITAcademicAutomationSystem.Areas.Two.ResponseDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IITAcademicAutomationSystem.Areas.Two.Service
{
    interface NoticeManagementSerI
    {
        void uploadNotice(UploadNoticeReqDto uploadNoticeReqDto);
        NoticeAllResDto getNotices_teacherProgramOfficer(int programId,int semesterId);
        NoticeAllResDto getNotices_student();
        void editNotice(EditNoticeReqDto editNoticeReqDto);
        void deleteNotice(int noticeId);
    }
}