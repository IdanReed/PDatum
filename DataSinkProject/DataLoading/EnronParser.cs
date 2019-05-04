using System;
using System.Collections.Generic;
using System.Linq;

namespace DataSinkProject.DataLoading
{
    public static class EnronParser
    {
        private const string _enronCsvPath =
            @"/home/idan/Documents/DataSinkProject/DataSinkProject/TestData/emails.csv";
        
        private const string _enronParsedPath = 
            @"/home/idan/Documents/DataSinkProject/DataSinkProject/TestData/emailsParsed.txt";

        private const string _selectedSender = @"phillip.allen@enron.com";     
       
        private struct Email
        {
            public string Date;
            public string From;
            public string To;
        }

        public static void LoadAndPushParsed(DBController controller)
        {
            controller.PushData(DataLoadingProcedures.LoadDataFile(_enronParsedPath));
        }
        public static void ParseCSV()
        {
            
            List<Email> emails = new List<Email>();
            
            int counter = 0;
            string line;
            
            System.IO.StreamReader file =   
                new System.IO.StreamReader(_enronCsvPath);

            Email email = new Email();
            
            while((line = file.ReadLine()) != null && emails.Count < 100)
            {
                string [] splitLine = line.Split(' ');

                switch (splitLine[0])
                {
                    case "Date:":
                        email = new Email();
                        email.Date = string.Concat(splitLine.Skip(1));
                        break;
                    
                    case "From:":
                        email.From = string.Concat(splitLine.Skip(1));
                        break;
                    
                    case "To:":
                        email.To = string.Concat(splitLine.Skip(1));
                        emails.Add(email);
                        //if(email.From == _selectedSender) emails.Add(email);
                        break;
                }
                counter++;  
            }
            
            DataLoadingProcedures.WriteDataToFile(ConvertToDatum(emails), _enronParsedPath);

            file.Close();  
        }

        private static List<Datum> ConvertToDatum(List<Email> emails)
        {
            List<Datum> data = new List<Datum>();
            
            foreach(Email email in emails)
            {
                Datum datum = new Datum();

                datum.DatumAttrib = new List<DatumAttrib>()
                {
                    new DatumAttrib()
                    {
                        Tag = "Date",
                        Value = email.Date
                    },
                    new DatumAttrib()
                    {
                        Tag = "From",
                        Value = email.From   
                    },
                    new DatumAttrib()
                    {
                        Tag = "To",
                        Value = email.To   
                    },
                    new DatumAttrib()
                    {
                        Tag = "DataSet",
                        Value = "EnronEmails"
                    }
                };
                
                data.Add(datum);
            }

            return data;
        }
    }
}