using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEST4_FOR_GIT
{
    interface IParser
    {
        string Url { get; set; }
        string ChapterHeadNode { get; set; }
        string ChapterSelector { get; set; }
        string ArticleSelector { get; set; }
        
        Dictionary<string, List<Article>> GetNews();
    }
}
