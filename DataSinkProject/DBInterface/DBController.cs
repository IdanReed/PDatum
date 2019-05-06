using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System;
using DBEntities;

namespace DataSinkProject
{
    public class DBController
    {
        private DataSinkDbContext _context;
        
        private const string _buildScriptPath = 
            @"/home/idan/RiderProjects/DataSinkProject/DataSinkProject/SqlFiles/build.sh";

        private const string _storedProcScriptPath =
            @"/home/idan/RiderProjects/DataSinkProject/DataSinkProject/RegisteredSPs/build.sh";
        
        public DBController() 
        {
            _context = new DataSinkDbContext();
        }

        private void AddServerTags(Datum datum)
        {
            datum.DatumAttrib.Add(
                new DatumAttrib()
                {
                    Tag = "ServerTime",
                    Value = DateTime.Now.ToString()
                });
        }
        
        public void PushData(List<Datum> data)
        {
            foreach (Datum datum in data)
            {
                AddServerTags(datum);
                PushDatum(datum);
            }
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

        public static void RebuildStoredProcedures()
        {
            MiscUtils.Bash(_storedProcScriptPath);
        }

        public DbDataReader ExecuteProcedure(string sql)
        {
            _context.Database.OpenConnection();
            
            DbCommand command = _context.Database.GetDbConnection().CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = sql;

            return command.ExecuteReader();;
        }

        public static void DebugPrintReader(DbDataReader reader)
        {
            while (reader.Read())
            {
                Console.WriteLine("-------------------------------");
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    Console.WriteLine(reader.GetValue(i));
                }
            }
            reader.Close();
        }

    }
}