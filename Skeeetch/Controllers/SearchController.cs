﻿using Skeeetch.Data;
using Skeeetch.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System.Runtime.Caching;

namespace Skeeetch.Controllers
{
    public class SearchController : Controller
    {
        private readonly MemoryCache _cache = MemoryCache.Default;
        private readonly CacheItemPolicy _policy = new CacheItemPolicy { SlidingExpiration = TimeSpan.FromHours(1) };

        public ActionResult Business()
        {
            //var id = yelpId;
            

            ViewBag.Title = "Business Info";
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer **API KEY GOES HERE**");
            var result = client.GetAsync($"https://api.yelp.com/v3/businesses/qa70o0JbMVMQJf4fvWiZaw").Result;
            var business = result.Content.ReadAsAsync<Business>().Result;

            _cache.Set("id", business, _policy);

            return View(business);

        }

        public async Task<ActionResult> Keyword()
        {
            var client = new HttpClient();
            var queryString = HttpUtility.ParseQueryString(string.Empty);

            // Request headers
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "***API key***");

            var uri = "https://eastus.api.cognitive.microsoft.com/text/analytics/v2.0/keyPhrases?" + queryString;

            HttpResponseMessage response;

            // Request body
            byte[] byteData = Encoding.UTF8.GetBytes("{'documents': [{'language': 'en','id': '1','text': 'Hello world. This is some input text that I love. Testing a little more.  Interested in how many keywords there are.'},{'language': 'fr','id': '2','text': 'Bonjour tout le monde'}]");

            using (var content = new ByteArrayContent(byteData))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                response = await client.PostAsync(uri, content);
                
            }
            var keywords = await response.Content.ReadAsAsync<DocumentRoot>();
            //var info = keywords.Documents.FirstOrDefault<Document>();
            //var info2 = keywords.Documents.ElementAt(1);


            _cache.Set("keywordcache", keywords, _policy);

            return View(keywords);
            
        }

        public ActionResult Results(string id)
        {
            var keywords = _cache.Get("keywordcache") as DocumentRoot;
            //var business = _cache.Get("id") as Business;
            return View(keywords);
        }


            // GET: Yelp
            public ActionResult Index()
        {
            return View();
        }

        // GET: Yelp/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Yelp/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Yelp/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Yelp/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Yelp/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Yelp/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Yelp/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
