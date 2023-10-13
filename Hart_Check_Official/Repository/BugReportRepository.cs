using Hart_Check_Official.Data;
using Hart_Check_Official.Interface;
using Hart_Check_Official.Models;
using System;

namespace Hart_Check_Official.Repository
{
    public class BugReportRepository : IBugReportRepository
    {
        private readonly datacontext _context;
        public BugReportRepository(datacontext context)
        {
            _context = context;
        }

        public BugReport CreateBugReport(BugReport bugReport)
        {
            
            _context.Add(bugReport);
            _context.SaveChanges();
            return (bugReport);
        }

        public bool DeleteBugReport(BugReport bugReport)
        {
            _context.Remove(bugReport);
            return Save();
        }

        public BugReport GetBugReport(int bugID)
        {
            return _context.BugReport.Where(e => e.bugID == bugID).FirstOrDefault();
        }

        public BugReport GetBugReport(string description)
        {
            return _context.BugReport.Where(e => e.description == description).FirstOrDefault();
        }

        public ICollection<BugReport> GetBugReports()
        {
            return _context.BugReport.OrderBy(e => e.bugID).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool BugExists(int bugID)
        {
            return _context.BugReport.Any(e => e.bugID == bugID);
        }

        public bool UpdateBugReport(BugReport bugReport)
        {
            _context.Update(bugReport);
            return Save();
        }
    }
}
