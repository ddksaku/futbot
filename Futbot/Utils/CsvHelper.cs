using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Futbot.Utils
{
    public class CSVHelper : List<string[]>
    {
        protected string csvFileContent = string.Empty;
        protected string separator = ",";

        public CSVHelper(string csvFilePath, string separator = ",")
        {
            var streamReader = new StreamReader(csvFilePath);            
            csvFileContent = streamReader.ReadToEnd();                                        
            this.separator = separator;

            foreach (string line in Regex.Split(csvFileContent, System.Environment.NewLine).ToList().Where(s => !string.IsNullOrEmpty(s)))
            {
                string[] values = Regex.Split(line, separator);

                for (int i = 0; i < values.Length; i++)
                {
                    //Trim values
                    values[i] = values[i].Trim('\"');
                }

                this.Add(values);
            }
        }
    }
}
