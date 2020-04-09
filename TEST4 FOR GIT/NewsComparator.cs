using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEST4_FOR_GIT
{
    class NewsComparator
    {
        public Dictionary<string, List<Article>> DictionarySite;
        public Dictionary<string, List<Article>> DictionaryDB;

        public NewsComparator(Dictionary<string, List<Article>> siteNews, Dictionary<string, List<Article>> dbNews) 
        {
            DictionarySite = siteNews;
            DictionaryDB = dbNews;
        }
        
        public Dictionary<string,List<Article>> GetFresh()
        {
            var difference = new Dictionary<string, List<Article>>();
            
            foreach (KeyValuePair<string, List<Article>> chapterArticlesPair in DictionarySite)
            {
                //Console.WriteLine(chapterArticlesPair.Key);

                foreach (Article article in chapterArticlesPair.Value)
                {
                 //   var listResult = article.Except().ToList();    
                }
                //difference.Add(chapterArticlesPair, listResult);
            }


            return difference;

        }

    }
}
