using System;
using System.Collections.Generic;
using HtmlAgilityPack;

namespace TEST4_FOR_GIT
{
    class Parser
    {

        public string Url { get; set; } 
        public string NodeCollection { get; set; }
        public string NodeSelect { get; set; }

        // Constructor
        public Parser(string url, string nodeCollection, string nodeSelect)
        {
            Url = url;
            NodeCollection = nodeCollection;
            NodeSelect = nodeSelect;
        }

        public List<string> SiteParsing() 
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(Url);
            
            List<string> listSite = new List<string>();

            // determines amount of chapters to parse
            int nodesQuantity = doc.DocumentNode.SelectNodes(NodeCollection).Count;
            for (int i = 1; i <= nodesQuantity; i++)
            {
                //determines amount of atricles in one chapter
                HtmlNodeCollection htmlNodes = doc.DocumentNode.SelectNodes(
                    $"{NodeCollection}[{i}]{NodeSelect}");
                

                foreach (HtmlNode article in htmlNodes)
                {
                    string articleValue = article.Attributes["href"].Value;
                    listSite.Add(articleValue);
                }
                //FillInTable(listSite, i, tableName);
                //listSite.Clear(); -- to clear listSite after comparison with listDB
            }

            return listSite;
        }

    }
}
