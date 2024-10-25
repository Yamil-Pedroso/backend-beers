using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SomeController : ControllerBase
    {
        [HttpGet("sync")]  // With sync programmation we can't make two tasks at same time--First one task a after the other task and so on...
        public IActionResult GetSync()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            Stopwatch.StartNew();

            Thread.Sleep(1000);
            Console.WriteLine("Connection to DB finished");

            Thread.Sleep(1000);
            Console.WriteLine("Send an email finished");

            Console.WriteLine("All is done!");

            stopwatch.Stop();
            return Ok(stopwatch.Elapsed);
        }

        [HttpGet("async")]
        public async Task<IActionResult> GetAsync() // Task is a class that represents an asynchrone task --- concurrent and asynchrone programmation
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            Stopwatch.StartNew();

            var task1 = new Task<string>(() => // This part is completing in a subprocess and Task can return a value
            { 
                Thread.Sleep(1000);
                Console.WriteLine("Connection to DB finished");
                return "Hi World!";
            });

            var task2 = new Task<int>(() => // This part is completing in a subprocess and Task can return a value
            {
                Thread.Sleep(1000);
                Console.WriteLine("Send an email finished");
                return 10;
            });


            task1.Start(); // Completing in second plane
            task2.Start();

            Console.WriteLine("Doing other thing"); // This completing in main process

            var result1 = await task1;
            var result2 = await task2;

            Console.WriteLine("All is done");

            stopwatch.Stop();

            return Ok(result1 + " " + result2 + " " + stopwatch.Elapsed);
        }
    }
}
