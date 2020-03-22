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

        public Dictionary<string,List<Article>> GetNews()
        {
            Dictionary<string,List<Article>> DictionarySite = new Dictionary<string,List<Article>>();
            


            foreach (HtmlNode chapterNode in GetChapterNodes())
            {
                List<Article> ArticlesList = new List<Article>();
                // TODO: change to set encoding while setting up HtmlWeb object
                string chapterNodeHeader = HttpUtility.HtmlDecode(chapterNode.SelectSingleNode(".//h2").InnerText);


                foreach (HtmlNode articleNode in GetArticleNodes(chapterNode))
                {
                    ArticlesList.Add(GetArticle(articleNode));
                }

                DictionarySite.Add(chapterNodeHeader, ArticlesList);
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
            // DONE: not many articles, still one article
            Article article = new Article();

            // TODO: change to set encoding while setting up HtmlWeb object
            article.ArticleText = articleNode.InnerText;
            //article.ArticleText = HttpUtility.HtmlDecode(articleNode.SelectSingleNode("a").InnerText);
            article.ArticleUrl = articleNode.Attributes["href"].Value;
            article.GetArticleTextWithUrl();

            return article;
        }
    }
}
