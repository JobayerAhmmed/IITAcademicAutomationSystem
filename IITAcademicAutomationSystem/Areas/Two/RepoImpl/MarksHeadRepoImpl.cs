using IITAcademicAutomationSystem.Areas.Two.Repo;
using IITAcademicAutomationSystem.DAL;
using IITAcademicAutomationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IITAcademicAutomationSystem.Areas.Two.RepoImpl
{
    public class MarksHeadRepoImpl : MarksHeadRepoI
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public void createHead(MarksHead marksHead)
        {
            try
            {
                db.MarksHeads.Add(marksHead);
                db.SaveChanges();
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public List<MarksHead>  getAllHeads()
        {
            try
            {
                var query= from MarksHead in db.MarksHeads select MarksHead;
                var heads = query.ToList();

                return heads;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public MarksHead getHead(int headId)
        {
            try
            {
                var query = from MarksHead in db.MarksHeads where MarksHead.Id == headId select MarksHead;
                var heads = query.ToList();
                return heads.ElementAt(0);
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}