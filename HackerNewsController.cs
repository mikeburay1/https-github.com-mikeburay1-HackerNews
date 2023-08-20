using Microsoft.AspNetCore.Http.Json;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text.Json;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HackerNewsController : ControllerBase
    {
        private const string StoryIdsUrl = "https://hacker-news.firebaseio.com/v0/beststories.json";
        private List<HackerNews> Stories = new List<HackerNews>();


        [HttpGet(Name = "GetHackerNews")]
        public async Task<IEnumerable<HackerNews>?> Get(int count)
        {
            int[] StoryIds;
            using (var client = new HttpClient())
            {
                var webRequest = new HttpRequestMessage(HttpMethod.Get, StoryIdsUrl);

                var response = await client.SendAsync(webRequest);
                if (response.IsSuccessStatusCode)
                {
                    var contents = await response.Content.ReadAsStringAsync();

                    StoryIds = JsonConvert.DeserializeObject<int[]>(contents);

                    // now get the stories for each id
                    foreach (var id in StoryIds)
                    {
                        webRequest = new HttpRequestMessage(HttpMethod.Get, $"https://hacker-news.firebaseio.com/v0/item/{id}.json");

                        response = await client.SendAsync(webRequest);
                        if (response.IsSuccessStatusCode)
                        {
                            contents = await response.Content.ReadAsStringAsync();

                            var Story = JsonConvert.DeserializeObject<HackerNews>(contents);

                            if (Story != null)
                            {
                                Stories.Add(Story);
                            }
                        }
                    }

                    int NumStories = Stories.Count();
                    if (count > NumStories)
                        count = NumStories;

                    // sort the story based on score in descending order
                    List<HackerNews> SortedList = Stories.OrderByDescending(o => o.Score).ToList();
                    SortedList.RemoveRange(count, SortedList.Count() - count);
                    return SortedList.ToArray().AsEnumerable();
                }
                return null;
            }
        }
    }
}