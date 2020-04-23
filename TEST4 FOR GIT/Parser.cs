using System.Web;
using System.Collections.Generic;
using HtmlAgilityPack;

namespace TEST4_FOR_GIT
{
    class Parser : IParser
    {
        public string Url { get; set; }
        public string ChapterHeadNode { get; set; }
        public string ChapterSelector { get; set; }
        public string ArticleSelector { get; set; }

        //Parser(string newsUrl, string chapterHeadNode, string chapterSelector, string articleSelector)

        public Parser(string [] array)
        {
            Url = array[0];
            ChapterSelector = array[1];
            ArticleSelector = array[2];
            ChapterHeadNode = array[3];
        }

        public Dictionary<string,List<Article>> GetNews()
        {
            Dictionary<string,List<Article>> DictionarySite = new Dictionary<string,List<Article>>();
            
            foreach (HtmlNode chapterNode in GetChapterNodes())
            {
                List<Article> ArticleList = new List<Article>();

                string chapterHeader = chapterNode.SelectSingleNode(ChapterHeadNode).InnerText;
                
                foreach (HtmlNode articleNode in GetArticleNodes(chapterNode))
                {
                    ArticleList.Add(GetArticle(articleNode));
                }

                DictionarySite.Add(chapterHeader, ArticleList);
            }

            return DictionarySite;
        }
        private HtmlNodeCollection GetChapterNodes()
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(Url);
            return doc.DocumentNode.SelectNodes(ChapterSelector);
        }

        private HtmlNodeCollection GetArticleNodes(HtmlNode chapterNode)
        {
            return chapterNode.SelectNodes(ArticleSelector);
        }

        private Article GetArticle(HtmlNode articleNode)
        {
            Article article = new Article
            {
                ArticleText = articleNode.InnerText,
                ArticleUrl = articleNode.Attributes["href"].Value
            };
            article.GetArticleTextWithUrl();

            return article;
        }
    }
}
