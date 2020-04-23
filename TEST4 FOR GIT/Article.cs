using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEST4_FOR_GIT
{
    class Article
    {
        public string ArticleText { get; set; }
        public string ArticleUrl { get; set; }

        public string GetArticleTextWithUrl()
        {
            return string.Format("{0}\n{1}", ArticleText, ArticleUrl);
        }
    }
}
