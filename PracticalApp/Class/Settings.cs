using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracticalApp.Class
{
    public class Settings : ISettings
    {
        public string ConnectionStrings { get; set; }
    }

    public interface ISettings
    {
        string ConnectionStrings { get; set; }
    }
}
