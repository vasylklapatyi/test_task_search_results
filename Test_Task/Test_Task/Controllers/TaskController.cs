using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CsQuery;
using System.Net;
using System.Net.Http;
using Test_Task.Models;
using System.IO;

namespace Test_Task.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class TaskController : ControllerBase
	{
		[HttpPost]
		public ActionResult GetFromGoogle([FromBody] string searchingText)
		{
			using (var db = new DbEntities())
			{
				if (string.IsNullOrEmpty(searchingText))
					return BadRequest();
				searchingText.Replace(" ", "+");
				Uri uri = new Uri("http://google.com/search?q=" + searchingText);
				HttpClient client = new HttpClient();
				var request = new HttpRequestMessage(HttpMethod.Post, uri);
				request.Properties.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:85.0)");
				var response = client.SendAsync(request);
				response.Wait();
				var responseStream = response.Result.Content.ReadAsStreamAsync();
				responseStream.Wait();
				string html = ((new StreamReader(responseStream.Result)).ReadToEnd());
				CQ cq = CQ.Create(html);

				foreach (IDomObject obj in cq.Find(".yuRUbf"))
				{
					string url;
					if ((url = obj.GetAttribute("href")).Contains("url"))
					{
						SearchResult res = new SearchResult
						{
							Id = Guid.NewGuid(),
							SearchEngine = "Google",//hardcode,the searching method is individual for google,in advance,the request method on the fron t will be dynamicly chosen depend to the dropdown value
							TimeStamp = DateTime.Now,
							Url = url,
							Title = string.IsNullOrEmpty(((IDomObject)(obj.Cq().Find("span"))).InnerText) ? ((IDomObject)(obj.Cq().Find("span"))).InnerText : "",
						};
					}
				}

				db.SaveChanges();
			}
			return new JsonResult("Success");
		}

		[HttpGet]
		public ActionResult GetGridData()
		{
			try
			{
				using (DbEntities db = new DbEntities())
				{
					return new JsonResult(db.SearchResults.ToList());
				}

			}
			catch (Exception)
			{
				return new JsonResult("Unable to load data.Please,rtry again");
			}
		}


		private void SaveRecord(DbEntities db, SearchResult model)
		{
			db.SearchResults.Add(model);
		}

	}
}
