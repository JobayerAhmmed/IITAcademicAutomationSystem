using IITAcademicAutomationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IITAcademicAutomationSystem.Areas.Two.Repo
{
    interface AcademicFileRepoI
    {
        AcademicFile getAcademicFile(int academicFileId);
        AcademicFile getAcademicCalendar(int programId, int semesterId, int batchId,string type);
        AcademicFile getRoutine(int programId, int semesterId, int batchId, string type);
        void saveAcademicFile(AcademicFile file);
        List<AcademicFile> getAcademicFiles(int programId, int semesterId, int batchId, string type);
        void updateAcademicFile(AcademicFile file);
        
    }
}
