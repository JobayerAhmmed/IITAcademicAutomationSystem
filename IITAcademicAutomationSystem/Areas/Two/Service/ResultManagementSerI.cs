using IITAcademicAutomationSystem.Areas.Two.RequestDto;
using IITAcademicAutomationSystem.Areas.Two.ResponseDto;
using IITAcademicAutomationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IITAcademicAutomationSystem.Areas.Two.Service
{
    interface ResultManagementSerI
    {
        void createHead(AddHeadRequestDto addHeadRequestDto);
        GetHeadsResDto getAllHeads();
        void createSubHead(AddSubHeadRequestDto addSubHeadRequestDto);
        GetSubHeadsResDto getSubHeads(int headId);
        void distributeMarks(DistributeMarksFinalReqDto distributeMarksFinalReqDto);
        GetDistributedMarksResDto getDistributedMarks(int programId, int semesterId,int batchId, int courseId);
        GetDistributedMarksPartialResDto getDistributedMarksPartially(int programId, int semesterId,int batchId, int courseId);
        bool checkIfMarksIsDistributedForACourse(int programId, int semesterId,int batchId, int courseId);
        void editDistributedMarks(EditedDistributeMarksReqDto editedDistributeMarksReqDto);
        void saveGivenMarks(GiveMarksReqDto giveMarksReqDto);
        GetGivenMarksResto getGivenMarks_t(int programId,int semesterId,int batchId,int courseId);
        GetGivenMarksResto getGivenMarks_s(int courseId);
        bool checkIfMarksIsGiven(int programId, int semesterId, int batchId, int courseId, int headId, int subheadId);

        GetGivenMarksToEditResDto getGivenMarks(int programId, int semesterId, int batchId, int courseId,int headId,int subheadId);
        void saveEditedMarks(SaveEditedMarksResDto saveEditedMarksResDto);

        AllHeadsMarksStatusOfACourseGivingInfoResDto getHeadsMarksGivingInfoOfACourse(int programId, int semesterId, int batchId, int courseId);
        void submitFinally(int programId, int semesterId, int batchId, int courseId);

        AllFinalSubmissionResDto getFinalSubmissionInfoOfAllCourses(int programId, int semesterId, int batchId);
        bool checkIfAllCourseAreFinallySubmitted(int programId, int semesterId, int batchId);

        AllStudentsResultResDto getResult(int programId, int semesterId, int batchId);
        IndividualStudentPromotionResDto[] getPassFailInfoOfStudents(int programId, int semesterId, int batchId);
    }
}
