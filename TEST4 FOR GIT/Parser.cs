using System.Web;
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

        public Dictionary<string, List<Article>> GetNews()
        {
            var DictionarySite = new Dictionary<string, List<Article>>();
            var ArticlesList = new List<Article>();


            foreach (var chapterNode in GetChapterNodes())
            {

                var chapterNodeHeader = HttpUtility.HtmlDecode(chapterNode.SelectSingleNode(".//h2").InnerText);


                foreach (var articleNode in GetArticleNodes(chapterNode))
                {
                    ArticlesList.Add(GetArticle(articleNode));
                }

                DictionarySite.Add(chapterNodeHeader, ArticlesList);
            }

            return DictionarySite;
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

        private Article GetArticle(HtmlNode articleNode)
        {
            Article article = new Article();

            article.ArticleText = articleNode.InnerText;
            article.ArticleUrl = articleNode.Attributes["href"].Value;
            article.GetArticleTextWithUrl();

            return article;
        }
    }
}
