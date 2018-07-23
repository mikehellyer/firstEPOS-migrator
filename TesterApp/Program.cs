using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using csvfile;

namespace TesterApp
{
    class Program
    {
        static void Main(string[] args)
        {
            using (CsvFileWriter writer = new CsvFileWriter("WriteTest.csv"))
            {
                for (int i = 0; i < 100; i++)
                {
                    CsvRow row = new CsvRow();
                    for (int J = 0; J < 5; J++)
                        row.Add(String.Format("Column{0}", J));
                    writer.WriteRow(row);
                }
            
            }

        }
    }
}
