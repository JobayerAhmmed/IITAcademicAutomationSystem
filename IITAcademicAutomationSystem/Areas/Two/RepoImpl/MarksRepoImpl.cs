using IITAcademicAutomationSystem.Areas.Two.Repo;
using IITAcademicAutomationSystem.DAL;
using IITAcademicAutomationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IITAcademicAutomationSystem.Areas.Two.RepoImpl
{
    public class MarksRepoImpl:MarksRepoI
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public List<Marks> getMarks(int marksDistributionId)
        {
            try
            {
                var query = from Marks in db.Marks where Marks.marksDistributionId == marksDistributionId select Marks;
                var marks = query.ToList();


                return marks;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Marks> getMarks(int marksDistributionId, int subHeadId)
        {
            try
            {
                var query = from Marks in db.Marks where Marks.marksDistributionId == marksDistributionId && Marks.subheadId == subHeadId select Marks;
                var marks = query.ToList();

                
                    return marks;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public Marks getMarksByDistribution(int distributionId, int subheadId)
        {
            try
            {
                var query = (from Marks in db.Marks where Marks.marksDistributionId == distributionId &&  Marks.subheadId == subheadId select Marks).FirstOrDefault();

                return query;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public void saveEditedMarks(Marks[] marks)
        {
            try
            {
                for(int i=0;i< marks.Length; i++)
                {                   

                    var originalMarks = db.Marks.Find(marks[i].Id);


                    if (originalMarks != null)
                    {
                        originalMarks.obtainedMarks = marks[i].obtainedMarks;
                        db.Entry(originalMarks).CurrentValues.SetValues(originalMarks);
                        db.SaveChanges();
                    }
                }
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public void saveGivenMarks(Marks[] marks)
        {
            try
            {
                for (int i = 0; i < marks.Length; i++)
                {
                    db.Marks.Add(marks[i]);
                    db.SaveChanges();

                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void saveGivenMarks(Marks marks)
        {
            try
            {
                db.Marks.Add(marks);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<MarksSubHead> getSubHeadByDistributionId(int distributionId)
        {
            try
            {
               var  queryOne= from Marks in db.Marks where Marks.marksDistributionId == distributionId select Marks;
                List<Marks> marksList = queryOne.ToList();

                List<int> uniqueSubHeadIdList = new List<int>();

                for(int i=0;i< marksList.Count; i++)
                {
                    if (uniqueSubHeadIdList.Contains(marksList.ElementAt(i).subheadId))
                        continue;
                    else
                        uniqueSubHeadIdList.Add(marksList.ElementAt(i).subheadId);
                }

                List<MarksSubHead> uniqueSubHeadList = new List<MarksSubHead>();

                MarksSubHeadRepoI subHeadRepo = new MarksSubHeadRepoImpl();
                for(int i=0;i< uniqueSubHeadIdList.Count; i++)
                {
                    var subHead = subHeadRepo.getSubheadById(uniqueSubHeadIdList.ElementAt(i));
                    uniqueSubHeadList.Add(subHead);
                }

                return uniqueSubHeadList;

            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public bool checkIfMarksIsGivenOfAHead(int distributionId)
        {
            try
            {
                var query = db.Marks.FirstOrDefault(s => s.marksDistributionId == distributionId);
                if (query != null)
                {
                    return true;
                }
                else
                    return false;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public List<Marks> getMarksOfAHeadOfAllSubHeadOfAStudent(int marksDistributionId, int studentId)
        {
            try
            {
                var query = (from Marks in db.Marks where Marks.marksDistributionId == marksDistributionId && Marks.studentId == studentId select Marks);

                return query.ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}