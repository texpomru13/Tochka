using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TestTochka
{
    public class Statistic
    {
        [JsonExtensionData]
        public Dictionary<char, string> DictionaryStat = new Dictionary<char, string>();     
    }
}
