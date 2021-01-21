using LocalizationUsingAcceptLangauage.WebAPI.Data;
using LocalizationUsingAcceptLangauage.WebAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace LocalizationUsingAcceptLangauage.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class MoviesController : ControllerBase
    {
        private readonly IMoviesService _movieService;
        private readonly IStringLocalizer<MoviesController> _localizer;

        public MoviesController(IMoviesService movieService, IStringLocalizer<MoviesController> localizer)
        {
            _movieService = movieService;
            this._localizer = localizer;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var movie = (await _movieService.Get()).ToList();
            if (movie == null)
            {
                return NotFound();
            }
            return Ok(movie);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var movie = (await _movieService.Get(new[] { id })).FirstOrDefault();
            if (movie == null)
            {
                return NotFound();
            }
            //getArticleTitles("");
            movie.Category = _localizer[movie.Category];
            return Ok(movie);
        }

        [HttpPost("")]
        public async Task<IActionResult> Add(Movie movie)
        {
            await _movieService.Add(movie);
            return Ok();
        }
        public async Task<List<string>> getArticleTitles(string author)
        {
            int startPage = 1, total_Pages = int.MaxValue;
            List<String> titles = new List<String>();
            while (startPage <= total_Pages)
            {
                try
                {
                    var client = new HttpClient();
                    client.BaseAddress = new Uri("https://jsonmock.hackerrank.com/api/movies/search/?Title=" + author + "page=" + startPage);
                    client.DefaultRequestHeaders.Clear();

                    var requestMsg = new HttpRequestMessage(new HttpMethod("GET"), client.BaseAddress);
                    string acceptMsg = "application/x-protobuf";
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(acceptMsg));
                    var response = await client.SendAsync(requestMsg);
                    using (HttpContent content = response.Content)
                    {
                        var json = content.ReadAsStringAsync().Result;
                        var data = JsonConvert.DeserializeObject<List<Article>>(json);
                        foreach (var item in data)
                        {
                            titles.Add(item.title);
                        }
                        return titles;
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return null;
        }
        public class Article
        {
            public string title { get; set; }
            public string url { get; set; }
            public string author { get; set; }
            public long num_comments { get; set; }
            public long story_id { get; set; }
            public string story_title { get; set; }
            public string story_url { get; set; }
            public long parent_id { get; set; }
            public long created_at { get; set; }

        }
    }
}
