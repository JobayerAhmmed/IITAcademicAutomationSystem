using IITAcademicAutomationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IITAcademicAutomationSystem.Areas.Two.Repo
{
    interface MarksSubHeadRepoI
    {
        void createSubHead(MarksSubHead marksHead);
        MarksSubHead getSubheadById(int subheadId);
        List<MarksSubHead> getSubHeads(int headId);
        

    }
}
