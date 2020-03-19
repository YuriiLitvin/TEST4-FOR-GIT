using System;
using System.Collections.Generic;
using HtmlAgilityPack;

namespace TEST4_FOR_GIT
{
    class Parser
    {

        public string Url { get; set; }
        public string ChapterSelector { get; set; }
        public string ArticleSelector { get; set; }


        public Parser(string newsUrl, string chapterSelector, string articleSelector)
        {
            Url = newsUrl;
            ChapterSelector = chapterSelector;
            ArticleSelector = articleSelector;
        }

        public List<string> GetNewsUrls()
        {
            var listSite = new List<string>();

            foreach (var chapterNode in GetChapterNodes())
            {
                foreach (var articleNode in GetArticleNodes(chapterNode))
                {
                    listSite.Add(GetArticleUrl(articleNode));
                }

            }
            return listSite;
        }
        private HtmlNodeCollection GetChapterNodes()
        {
            var web = new HtmlWeb();
            var doc = web.Load(Url);
            return doc.DocumentNode.SelectNodes(ChapterSelector);
        }
       
        private HtmlNodeCollection GetArticleNodes(HtmlNode chapterNode)
        {
            return chapterNode.SelectNodes(ArticleSelector);
        }
        
        private string GetArticleUrl(HtmlNode articleNode) 
        {
            string value = articleNode.Attributes["href"].Value;
            return value;
        }
    
    }
}
