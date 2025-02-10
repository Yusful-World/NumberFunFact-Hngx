using Microsoft.AspNetCore.Mvc;
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

        [HttpGet("{number}")]
        public async Task<IActionResult> GetNumberInfo(int number)
        {
            var result = new
            {
                number = number,
                is_prime = _numberFact.IsPrime(number),
                is_perfect = _numberFact.IsPerfect(number),
                properties = _numberFact.GetProperties(number),
                digit_sum = _numberFact.GetDigitSum(number),
                fun_fact = await _numberFact.GetFunFactAsync(number)
            };

            return Ok(result);
        }

    }
}
