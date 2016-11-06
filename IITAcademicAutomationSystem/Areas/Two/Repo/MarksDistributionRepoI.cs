using IITAcademicAutomationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IITAcademicAutomationSystem.Areas.Two.Repo
{
    interface MarksDistributionRepoI
    {
        MarksDistribution getDistributedMarks(int marksDistributionId);
        void distributeMarks(MarksDistribution distributeMarksFinalReqDto);
        List<MarksDistribution> getDistributedMarks(int programId, int semesterId, int batchId, int courseId);
        int GetMarksDistributionId(int programId, int semesterId, int batchId, int courseId,int headId);
        MarksDistribution GetMarksDistribution(int programId, int semesterId, int batchId, int courseId, int headId);

          /*List<MarksDistribution> getDistributedMarksByProgramSemesterCourse(int programId, int semesterId, int courseId);*/
        void editDistributedMarks(MarksDistribution marksDistribution);
        void submitFinally(int programId, int semesterId, int batchId, int courseId);
        bool checkIfFinallySubmitted(int programId, int semesterId, int batchId, int courseId);

    }
}
