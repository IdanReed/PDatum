using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using Microsoft.EntityFrameworkCore;

namespace DataSinkProject
{
    class  Program 
    {
        static void Main(string[] args)
        {
            DBController.RebuildDB();
            TestingProcedures.GenerateAndWriteTestData();
            
            DBController controller = new DBController();
        
            foreach (Datum datum in TestingProcedures.LoadTestFile())
            {
                controller.PushDatum(datum);
            }
        }t
    }
}