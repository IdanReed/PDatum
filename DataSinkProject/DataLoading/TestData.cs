using System.Collections.Generic;
using System;
using DBEntities;

namespace DataSinkProject.DataLoading
{
    public static class TestData
    {

        private static string _testDataDir = @"/home/idan/RiderProjects/DataSinkProject/DataSinkProject/TestData";
        
        private static Dictionary<Action, string> _dataFilnames = 
            new Dictionary<Action, string>()
            {
                {RandomNumber, "RandomNumbers.txt"},
                {IncNumber, "IncNumbers.txt"},
            };
        
        
        
        public static void Generate()
        {
            foreach (KeyValuePair<Action, string> pair in _dataFilnames)
            {
               pair.Key.Invoke();
            }   
        }

        public static void LoadAndPush(DBController controller)
        {
            foreach (KeyValuePair<Action, string> pair in _dataFilnames)
            {
                controller.PushData(DataLoadingProcedures.LoadDataFile(_testDataDir + pair.Value));
            }   
        }
        
        private static void IncNumber()
        {
            List<Datum> data = new List<Datum>();
            
            for (int i = 0; i < 50; i++)
            {
                Datum datum = new Datum();
                
                datum.DatumAttrib = new List<DatumAttrib>()
                {
                    new DatumAttrib()
                    {
                        Tag = "IncNum",
                        Value = i.ToString()
                    },
                    new DatumAttrib()
                    {
                        Tag = "DataSet",
                        Value = "IncNumber"
                    }
                };
                
                data.Add(datum);
            }
            
            DataLoadingProcedures.WriteDataToFile(data, _testDataDir + _dataFilnames[IncNumber]);
        }
        
        private static void RandomNumber()
        {
            List<Datum> data = new List<Datum>();

            Random rand = new Random();
            
            for (int i = 0; i < 50; i++)
            {
                Datum datum = new Datum();
                
                datum.DatumAttrib = new List<DatumAttrib>()
                {
                    new DatumAttrib()
                    {
                        Tag = "RandNum",
                        Value = rand.Next().ToString()
                    },
                    new DatumAttrib()
                    {
                        Tag = "DataSet",
                        Value = "RandomNumber"
                    }
                };
                
                data.Add(datum);
            }
            
            DataLoadingProcedures.WriteDataToFile(data, _testDataDir + _dataFilnames[RandomNumber]);
        }
        

        
    }
}