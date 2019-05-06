using System;
using System.IO;

using Newtonsoft.Json;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using DBEntities;

namespace DataSinkProject
{
    public static class DataLoadingProcedures
    {
        private static string _testDataFilePath = @"/home/idan/Documents/DataSinkProject/DataSinkProject/TestData/testdata.txt";


        public static void WriteDataToFile(List<Datum> datum, string filePath)
        {
            string serializedDatum = Newtonsoft.Json.JsonConvert.SerializeObject(datum);
            
            using (System.IO.StreamWriter file = new StreamWriter(filePath, false))
            {
                file.WriteLine(serializedDatum);
            }
        }

        public static List<Datum> LoadDataFile(string filePath)
        {
            List<Datum> loadedData = new List<Datum>();

            JsonSerializer serializer = new JsonSerializer();
            
            using (StreamReader file = File.OpenText(filePath))
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
        
        
    }
}