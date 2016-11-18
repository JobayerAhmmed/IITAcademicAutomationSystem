using IITAcademicAutomationSystem.Areas.Two.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IITAcademicAutomationSystem.Models;
using IITAcademicAutomationSystem.DAL;
using System.Data.Entity;

namespace IITAcademicAutomationSystem.Areas.Two.RepoImpl
{
    public class MarksDistributionRepoImpl : MarksDistributionRepoI
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public void distributeMarks(MarksDistribution marksDistribution)
        {
            try
            {
                db.MarksDistributions.Add(marksDistribution);
                db.SaveChanges();
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public List<MarksDistribution> getDistributedMarks(int programId, int semesterId,int batchId, int courseId)
        {
            try
            {
                var query = from MarksDistribution in db.MarksDistributions where MarksDistribution.programId == programId && MarksDistribution.semesterId == semesterId && MarksDistribution.batchId == batchId && MarksDistribution.courseId == courseId select MarksDistribution;
                var marksDistribution = query.ToList();

                return marksDistribution;
            }
            catch(Exception e)
            {
                throw e;
            }
        }


        public void editDistributedMarks(MarksDistribution marksDistribution)
        {
            try
            {                                 
                var original = db.MarksDistributions.Find(marksDistribution.Id);

                if (original != null)
                {
                    db.Entry(original).CurrentValues.SetValues(marksDistribution);
                    db.SaveChanges();
                }


            }
            catch(Exception e){
                throw e;
            }
        }

        public MarksDistribution getDistributedMarks(int marksDistributionId)
        {
            try
            {
                MarksDistribution marksDistribution = db.MarksDistributions.Find(marksDistributionId);
                return marksDistribution;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public int GetMarksDistributionId(int programId, int semesterId, int batchId, int courseId, int headId)
        {
            try
            {
                var query = (from MarksDistribution in db.MarksDistributions where MarksDistribution.programId == programId && MarksDistribution.semesterId == semesterId && MarksDistribution.batchId == batchId && MarksDistribution.courseId == courseId && MarksDistribution.headId == headId select MarksDistribution).FirstOrDefault();

                if (query != null)
                    return query.Id;
                else
                    return -1;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public MarksDistribution GetMarksDistribution(int programId, int semesterId, int batchId, int courseId, int headId)
        {
            try
            {
                var query = (from MarksDistribution in db.MarksDistributions where MarksDistribution.programId == programId && MarksDistribution.semesterId == semesterId && MarksDistribution.batchId == batchId && MarksDistribution.courseId == courseId && MarksDistribution.headId == headId select MarksDistribution).FirstOrDefault();
                return query;
                
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void submitFinally(int programId, int semesterId, int batchId, int courseId)
        {
            try
            {
                var query = from MarksDistribution in db.MarksDistributions where MarksDistribution.programId == programId && MarksDistribution.semesterId == semesterId && MarksDistribution.batchId == batchId && MarksDistribution.courseId == courseId select MarksDistribution;
                var marksDistribution = query.ToList();


                for (int i = 0; i < marksDistribution.Count; i++)
                {
                    var original = db.MarksDistributions.Find(marksDistribution.ElementAt(i).Id);

                    if (original != null)
                    {
                        original.isFinallySubmitted = true;
                        db.Entry(original).CurrentValues.SetValues(original);
                        db.SaveChanges();
                    }
                }

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool checkIfFinallySubmitted(int programId, int semesterId, int batchId, int courseId)
        {
            try
            {
                var query = (from MarksDistribution in db.MarksDistributions where MarksDistribution.programId == programId && MarksDistribution.semesterId == semesterId && MarksDistribution.batchId == batchId && MarksDistribution.courseId == courseId select MarksDistribution).FirstOrDefault();
                if (query != null)
                {
                    if (query.isFinallySubmitted == true)
                        return true;
                    else
                        return false;
                }
                else
                    return false;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        

        


    }
}