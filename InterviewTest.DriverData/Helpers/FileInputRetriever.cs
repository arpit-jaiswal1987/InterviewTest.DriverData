using InterviewTest.DriverData.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewTest.DriverData.Helpers
{
    public class FileInputRetriever : IInputRetriever
    {
        public string RetrieveText(string path)
        {
            if (File.Exists(path))
            {
                return File.ReadAllText(path);
            }
            throw new FileNotFoundException("Unable to find file at location: " + path);
        }
    }
}
