using IITAcademicAutomationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IITAcademicAutomationSystem.Areas.Two.Repo
{
    interface MarksRepoI
    {
        List<Marks> getMarks(int marksDistributionId);
        List<Marks> getMarks(int marksDistributionId,int subHeadId);
        Marks getMarksByDistribution(int distributionId,int subheadId);
        void saveGivenMarks(Marks marks);
        void saveEditedMarks(Marks[] marks);
        List<MarksSubHead> getSubHeadByDistributionId(int distributionId);
        bool checkIfMarksIsGivenOfAHead(int distributionId);
    }
}
