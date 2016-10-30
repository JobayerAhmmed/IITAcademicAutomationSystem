using IITAcademicAutomationSystem.Areas.Two.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IITAcademicAutomationSystem.Models;
using IITAcademicAutomationSystem.DAL;

namespace IITAcademicAutomationSystem.Areas.Two.RepoImpl
{
    public class AcademicFileRepoImpl : AcademicFileRepoI
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public AcademicFile getAcademicCalendar(int programId, int semesterId, int batchId,string type)
        {
            try
            {
                var academicCalendar = (from AcademicFile in db.AcademicFiles where AcademicFile.programId == programId && AcademicFile.semesterId == semesterId && AcademicFile.batchId == batchId && AcademicFile.isDeleted == false && AcademicFile.type == type orderby AcademicFile.Id descending select AcademicFile).FirstOrDefault();
                return academicCalendar;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public AcademicFile getAcademicFile(int academicFileId)
        {
            try
            {
                var query = from AcademicFile in db.AcademicFiles where AcademicFile.Id == academicFileId select AcademicFile;
                var files = query.ToList();

                if (files.Count != 0)
                    return files.ElementAt(0);
                else
                    return null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<AcademicFile> getAcademicFiles(int programId, int semesterId, int batchId, string type)
        {
            try
            {
                var query = from AcademicFile in db.AcademicFiles where ((AcademicFile.programId==-1)||(AcademicFile.programId == programId && AcademicFile.semesterId==-1) ||((AcademicFile.programId == programId && AcademicFile.semesterId == semesterId && AcademicFile.batchId == batchId))) && AcademicFile.isDeleted == false && AcademicFile.type == type orderby AcademicFile.Id descending select AcademicFile;
                var files = query.ToList();

                return files;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public AcademicFile getRoutine(int programId, int semesterId, int batchId,string type)
        {
            try
            {
                var academicCalendar = (from AcademicFile in db.AcademicFiles where AcademicFile.programId == programId && AcademicFile.semesterId == semesterId && AcademicFile.batchId == batchId && AcademicFile.isDeleted == false && AcademicFile.type == type orderby AcademicFile.Id descending select AcademicFile).FirstOrDefault();
                return academicCalendar;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void saveAcademicFile(AcademicFile file)
        {
            try
            {
                db.AcademicFiles.Add(file);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void updateAcademicFile(AcademicFile file)
        {
            try
            {
                db.Entry(file).CurrentValues.SetValues(file);
                db.SaveChanges();
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}