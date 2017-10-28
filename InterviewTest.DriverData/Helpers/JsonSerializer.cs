using InterviewTest.DriverData.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewTest.DriverData.Helpers
{
    public class JsonSerializer:ISerializer
    {
        public object Deserialize(string text, Type type)
        {
            try
            {
                return JsonConvert.DeserializeObject(text, type);
            }
            catch
            { throw new FormatException("Text is not in valid JSON format, or Type does not matches"); }
        }
        public string Serialize(object obj)
        {
            try
            {
                return JsonConvert.SerializeObject(obj);
            }
            catch
            { throw new FormatException("Object could not be serialized"); }
        }
    }
}
