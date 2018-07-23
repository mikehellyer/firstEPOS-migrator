using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;

namespace csvfile
{
    public class CsvRow : List<string>
    {
        public string Linetext { get; set; }
    }

    public class CsvFileWriter : StreamWriter
    {
        public CsvFileWriter(Stream stream)
            : base(stream)
        {
        }

        public CsvFileWriter(string filename)
            : base(filename)
        {
        }

        public void WriteRow(CsvRow row)
        {
            StringBuilder builder = new StringBuilder();
            foreach (string value in row)
            {
                //Add seperator if not first value
                if (builder.Length > 0)
                    builder.Append(',');

                if (value.IndexOfAny(new Char[] { '"', ',' }) != -1)
                {
                    // if the value is a comma or quote
                    // double up any double quotes etc
                    builder.AppendFormat("\"{0}\"", value.Replace("\"", "\"\""));
                }
                else builder.Append(value);
            }
            row.Linetext = builder.ToString();
            WriteLine(row.Linetext);

        }



    }

}
