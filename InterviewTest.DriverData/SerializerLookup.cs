using InterviewTest.DriverData.Helpers;
using InterviewTest.DriverData.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewTest.DriverData
{
    public class SerializerLookup
    {
        public static ISerializer GetSerializer()
        {
            return new JsonSerializer();
        }
    }
}
