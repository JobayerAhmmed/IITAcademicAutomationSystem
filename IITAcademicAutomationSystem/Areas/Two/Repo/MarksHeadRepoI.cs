using IITAcademicAutomationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IITAcademicAutomationSystem.Areas.Two.Repo
{
    interface MarksHeadRepoI
    {
        void createHead(MarksHead marksHead);
        List<MarksHead> getAllHeads();
        MarksHead getHead(int headId);
        
    }
}
