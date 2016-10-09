using System;
using System.Collections.Generic;
using Nancy;
using DbConnection;
 
namespace Quotes 
{
    public class QuotesModule : NancyModule  
    {
        public QuotesModule()
        {
// ____________get name and quote__________________
            Get("/", args =>
            {
                return View["Index.sshtml"];   
            }); 

// ____________display quotes__________________

            Post("/process", args => 
            {
                Console.WriteLine("Creating A New Quote");
                string name = Request.Form["name"];
                string quote = Request.Form["quote"];
                string query = $"INSERT INTO quotes (name, quote, created_at) VALUES('{name}', '{quote}', NOW())";
                DbConnector.ExecuteQuery(query);
                Console.WriteLine("The quote has been added to the db!");
                return Response.AsRedirect("/quotes");  
            });  

            Get("/quotes", args =>    
            {
                @ViewBag.quotes = "";
                List<Dictionary<string, object>> results = DbConnector.ExecuteQuery("SELECT * FROM quotes");
                Console.WriteLine("2 q");
                results.Reverse(); //this will display data in reverse order or descending
                foreach(Dictionary<string,object> item in results)
                {
                    @ViewBag.quotes += "<p>" + "<h4>" + item["name"] + "</h4>" + "said:" + " ' "+ item["quote"] + " '" +  "@" + item["created_at"] + "</p>" + "<hr>";
                }
                return View["Quotes.sshtml", results]; 
            }); 
        }
    }
}