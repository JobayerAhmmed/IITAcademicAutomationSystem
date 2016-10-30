using IITAcademicAutomationSystem.Areas.Two.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IITAcademicAutomationSystem.Models;
using IITAcademicAutomationSystem.DAL;

namespace IITAcademicAutomationSystem.Areas.Two.RepoImpl
{
    public class MarksSubHeadRepoImpl : MarksSubHeadRepoI
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public void createSubHead(MarksSubHead marksSubHead)
        {
            try
            {
                db.MarksSubHeads.Add(marksSubHead);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public MarksSubHead getSubheadById(int subheadId)
        {
            try
            {
                var query = from MarksSubHead in db.MarksSubHeads where MarksSubHead.Id == subheadId select MarksSubHead;
                var subHeads = query.ToList();

                if (subHeads.Count != 0)
                    return subHeads.ElementAt(0);
                else
                    return null;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public List<MarksSubHead> getSubHeads(int headId)
        {
            try
            {
                var query = from MarksSubHead in db.MarksSubHeads where MarksSubHead.headId == headId select MarksSubHead ;
                var subHeads = query.ToList();

                return subHeads;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

       
    }
}