using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using DataSinkProject.DataLoading;
using Microsoft.EntityFrameworkCore;

namespace DataSinkProject
{
    class  Program 
    {
        static void Main(string[] args)
        {
            DBController controller = new DBController();
            
            DBController.RebuildDB();
            DBController.RebuildStoredProcedures();
            
            
            EnronParser.ParseCSV();
            EnronParser.LoadAndPushParsed(controller);

            TestData.Generate();
            TestData.LoadAndPush(controller);
            
            DBController.DebugPrintReader(controller.ExecuteProcedure("PROC_ALL_DATASET"));
        }
    }
}