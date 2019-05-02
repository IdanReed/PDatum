using Microsoft.EntityFrameworkCore;

namespace DataSinkProject
{
    public class DBController
    {
        private DataSinkDbContext _context;
        
        private const string _buildScriptPath = @"/home/idan/RiderProjects/DataSinkProject/DataSinkProject/SqlFiles/build.sh";
        
        
        public DBController()
        {
            _context = new DataSinkDbContext();
        }
        
        public void PushDatum(Datum datum)
        {
            _context.Datum.Add(datum);
            _context.SaveChanges();
        }

        public static void RebuildDB()
        {
            MiscUtils.Bash(_buildScriptPath);
        }

    }
}