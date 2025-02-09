using Newtonsoft.Json.Linq;

namespace NumberFunFact_Hngx.Data
{
    public class NumberFact
    {
        private readonly HttpClient _httpClient;

        public NumberFact(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public bool IsPrime(int num)
        {
            if (num < 2) return false;
            for (int i = 2; i <= Math.Sqrt(num); i++)
                if (num % i == 0) return false;
            return true;
        }

        public bool IsPerfect(int num)
        {
            if (num < 2) return false;

            int sum = 1;

            for (int i = 2; i <= Math.Sqrt(num); i++)
            {
                if (num % i == 0)
                {
                    sum += i;
                    if (i != num / i) 
                        sum += num / i;
                }
            }

            return sum == num;
        }

        public string[] GetProperties(int num)
        {
            bool isArmstrong = IsArmstrong(num);
            bool isOdd = num % 2 != 0;

            return isArmstrong ? new[] { "armstrong", isOdd ? "odd" : "even" } : new[] { isOdd ? "odd" : "even" };
        }

        public bool IsArmstrong(int num)
        {
            string stringNumber = num.ToString();
            int sum = stringNumber.Sum(digit => (int)Math.Pow(char.GetNumericValue(digit), stringNumber.Length));
            return sum == num;
        }

        public int GetDigitSum(int num)
        {
            return num.ToString().Sum(digit => digit - '0');
        }

        public async Task<string> GetFunFactAsync(int num)
        {
            try
            {
                string url = $"http://numbersapi.com/{num}/math?json";
                var response = await _httpClient.GetStringAsync(url);
                return JObject.Parse(response)["text"]?.ToString() ?? "Fun fact not available";
            }
            catch (HttpRequestException)
            {
                return "unable to handle funfact request";
            }
        }
    }
}
