using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using NumberFunFact_Hngx.Data;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;


namespace NumberFunFact_Hngx.Controllers
{
    
    [Route("")]
    [ApiController]
    public class NumberController : ControllerBase
    {
        private readonly NumberFact _numberFact;

        public NumberController(NumberFact numberFact)
        {
            _numberFact = numberFact;
        }

        //[HttpGet]
        //public IActionResult GetWelcomeMessage()
        //{
        //    return Ok("Hi dear! Input a number to get its fun facts.");
        //}

        [HttpGet("{number?}")]
        public async Task<IActionResult> GetNumberInfo(int? number)
        {
            if (!number.HasValue)
            {
                return Ok("Hi there! Input a number to get its fun facts.");
            }

            var numberValue = number.Value;

            var result = new
            {
                number = number,
                is_prime = _numberFact.IsPrime(numberValue),
                is_perfect = _numberFact.IsPerfect(numberValue),
                properties = _numberFact.GetProperties(numberValue),
                digit_sum = _numberFact.GetDigitSum(numberValue),
                fun_fact = await _numberFact.GetFunFactAsync(numberValue)
            };

            return Ok(result);
        }

    }
}
