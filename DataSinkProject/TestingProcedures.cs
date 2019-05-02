using System;
using System.IO;

using Newtonsoft.Json;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace DataSinkProject
{
    public static class TestingProcedures
    {
        public static string TestDataFilePath = @"/home/idan/RiderProjects/DataSinkProject/DataSinkProject/TestData/";
        public static string TestDataFileName = @"testdata.txt";

        public static string FullFile = TestDataFilePath + TestDataFileName;

        public static void WriteDataToFile(List<Datum> datum)
        {
            string serializedDatum = Newtonsoft.Json.JsonConvert.SerializeObject(datum);
            
            using (System.IO.StreamWriter file = new StreamWriter(FullFile, false))
            {
                file.WriteLine(serializedDatum);
            }
        }

        public static List<Datum> LoadTestFile()
        {
            List<Datum> loadedData = new List<Datum>();

            JsonSerializer serializer = new JsonSerializer();
            
            using (StreamReader file = File.OpenText(FullFile))
            using (JsonTextReader reader = new JsonTextReader(file))
            {
                while (reader.Read())
                {
                    if (reader.TokenType == JsonToken.StartObject)
                    {
                        Datum datum = serializer.Deserialize<Datum>(new JTokenReader(JObject.Load(reader)));
                        loadedData.Add(datum);
                    }
                }
            }

            return loadedData;
        }
        
        public static void GenerateAndWriteTestData()
        {
            List<Datum> generatedData = new List<Datum>();
            
            for (int i = 0; i < 10; i++)
            {
                Datum datum = new Datum();
                datum.DatumAttrib = new List<DatumAttrib>();
                
                datum.DatumAttrib.Add(
                    new DatumAttrib()
                    {
                        Tag = "Time",
                        Value = DateTime.Now.TimeOfDay.ToString()
                    });
                
                for (int j = 0; j < 5; j++)
                {
                    datum.DatumAttrib.Add(
                        new DatumAttrib()
                        {
                            Tag = "PosIntToFour",
                            Value = j.ToString()
                        });
                }
                
                generatedData.Add(datum);
            }
            
            WriteDataToFile(generatedData);
        }
    }
}