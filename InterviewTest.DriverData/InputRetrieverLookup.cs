using InterviewTest.DriverData.Helpers;
using InterviewTest.DriverData.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewTest.DriverData
{
    class InputRetrieverLookup
    {
        public static IInputRetriever GetInputRetriever()
        {
            return new FileInputRetriever();
        }
    }
}
