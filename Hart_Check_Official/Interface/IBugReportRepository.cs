using Hart_Check_Official.Models;

namespace Hart_Check_Official.Interface
{
    public interface IBugReportRepository
    {
        ICollection<BugReport> GetBugReports();

        BugReport GetBugReport(int bugID);

        BugReport GetBugReport(string description);
        bool BugExists(int bugID);

        bool CreateBugReport(BugReport bugReport);
        bool DeleteBugReport(BugReport bugReport);

        bool Save();
    }
}
