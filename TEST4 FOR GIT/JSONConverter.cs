using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace TEST4_FOR_GIT
{
    class JSONConverter
    {
        public string SerializeAndPush(Dictionary<string,List<Article>> DictionarySite) 
        {
            string json = JsonConvert.SerializeObject(DictionarySite, Formatting.Indented);
            return json;
        }        
        
        //public Dictionary<string, List<Article>> Deserialize(string value)
        //{
        //    Dictionary<string, string> htmlAttributes = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
        //    return htmlAttributes;
        //}

    }
}
