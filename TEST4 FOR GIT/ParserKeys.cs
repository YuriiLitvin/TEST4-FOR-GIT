using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEST4_FOR_GIT
{
    class ParserKeys
    {
        public List<string[]> GetList()
        {
            List<string[]> list = new List<string[]>();
            
            string[] ukrNetKey = new string[] {"http://www.ukr.net",
            "//article//section",
            ".//h2",
            ".//*[position()>1 and position()<last()]//a"};

            string[] ukrOnlineKey = new string[] {"http://www.ukr-online.com",
                "//td[1]/div[1]/div[@class ='lastblock']",
                ".//*[@class = 'lastheader']",
                ".//div[@class = 'custom-4']//a"};

            list.Add(ukrNetKey);
            list.Add(ukrOnlineKey);

            return list;
        }
    }
}
