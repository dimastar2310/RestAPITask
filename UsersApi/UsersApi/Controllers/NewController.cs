//using ApiTuturials.Data;
//using ApiTuturials.Models;
//using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
//using Microsoft.EntityFrameworkCore;

namespace ApiTuturials.Controllers
{
    [Route("api/[controller]")] //api/User =>Acttually controller replaced with User
    [ApiController]
    public class UserController : ControllerBase //in hes example its HomeController
    {
      //  private DemoDbContext _dbContext;
     //   public UserController(DemoDbContext dbContext) //accesing our db
     //   {
     //       this._dbContext = dbContext;
      //  }



        // api/getusers =>gets all users
        //showing get method
       // [HttpGet("GetUsers")] //http verbs its our action
       // public async Task<IActionResult> Get()
      //  {

//
       //     return Ok(await _dbContext.tblUsers.ToListAsync());
   

      /// <summary>
      /// }
      /// </summary>
      /// <param name="id"></param>
      /// <returns></returns>

        // api/getusers/5 =>gets 5 user
        [HttpGet("GetUsers/{gender}")] //http verbs its our action
        public async Task<IActionResult> Get([FromRoute] string type) //its knows to defreantiate by parameter
        {
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri("https://api.randomuser.me");
                    var response = await client.GetAsync("https://api.randomuser.me");
                    response.EnsureSuccessStatusCode();

                    var stringResult = await response.Content.ReadAsStringAsync();
                    var rawData = JsonConvert.DeserializeObject<PersonAPIResponse>(stringResult);
                    //RootObject person = JsonConvert.DeserializeObject<RootObject>(stringResult);
                    //return Ok(new
                    //{
                    //   Gender = rawData.Results.Select(x => type)
                    //     });
                    //You should create a new return type class and map into that, what you want to retu
                    //rn.. assigning from person:
                    RootObject person = JsonConvert.DeserializeObject<RootObject>(stringResult);
                    return Ok(new PersonAPIResponse
                    {
                        title = person.results[0]["name"].title, //first result
                    });
                }

                catch (HttpRequestException httpRequestException)
                {
                    return BadRequest($"Error generating person: {httpRequestException.Message}");
                }
            }

        }



    }
}
