using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewTest.DriverData.Interfaces
{
    public interface ISerializer
    {
        object Deserialize(string text, Type type);
        string Serialize(object obj);
    }
}
