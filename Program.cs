using CsvHelper;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace CsvHelperWithDynamicObject
{
    class Program
    {
        static void Main(string[] args)
        {
            generateDynamicHeadersForField2AndField4("data2.4.csv");            
            generateDynamicHeadersForField1AndField3("data1.3.csv");
        }

        private static void generateDynamicHeadersForField2AndField4(string fileName)
        {
            // from cdm 
            var allColumns = new List<string>(){"Field1","Field2","Field3","Field4"};
            //from sql/ json mapping manifest
            var data  = new List<Dictionary<string, string>>();
            var row1 = new Dictionary<string, string>();
            // We suppose to have the same fields for all rows (sql query)
            row1.Add("Field2", "Value 2.1");
            row1.Add("Field4", "Value 4.1");
            var row2 = new Dictionary<string, string>();
            row2.Add("Field2", "Value 2.2");
            row2.Add("Field4", "Value 4.2");
            data.Add(row1);
            data.Add(row2);
            using (var stream = new StreamWriter(fileName))
            using (var csv = new CsvWriter(stream, CultureInfo.InvariantCulture))
            {
                var records = new List<dynamic>();                
                foreach (var row in data)
                {
                    var record = new ExpandoObject() as IDictionary<string, object>;
                    // to keep the header column order, it's important to go through allColumns
                    foreach (var column in allColumns)
                    {
                        string value = string.Empty;
                        if (row.ContainsKey(column))
                        {
                            row.TryGetValue(column, out value);
                            record.Add(column, value);
                        }
                        else
                        {
                            record.Add(column, value);
                        }
                        
                    }
                    records.Add(record);
                }                
                csv.WriteRecords(records);
            }
        }
        private static void generateDynamicHeadersForField1AndField3(string fileName)
        {
            // from cdm 
            var allColumns = new List<string>() { "Field1", "Field2", "Field3", "Field4" };
            //from sql/ json mapping manifest
            var data = new List<Dictionary<string, string>>();
            var row1 = new Dictionary<string, string>();
            // We suppose to have the same fields for all rows (sql query)
            row1.Add("Field1", "Value 1.1");
            row1.Add("Field3", "Value 3.1");
            var row2 = new Dictionary<string, string>();
            row2.Add("Field1", "Value 1.2");
            row2.Add("Field3", "Value 3.2");
            data.Add(row1);
            data.Add(row2);
            using (var stream = new StreamWriter(fileName))
            using (var csv = new CsvWriter(stream, CultureInfo.InvariantCulture))
            {
                var records = new List<dynamic>();
                foreach (var row in data)
                {
                    var record = new ExpandoObject() as IDictionary<string, object>;
                    // to keep the header column order, it's important to go through allColumns
                    foreach (var column in allColumns)
                    {
                        string value = string.Empty;
                        if (row.ContainsKey(column))
                        {
                            row.TryGetValue(column, out value);
                            record.Add(column, value);
                        }
                        else
                        {
                            record.Add(column, value);
                        }

                    }
                    records.Add(record);
                }
                csv.WriteRecords(records);
            }
        }

    }
}
