//calling an API that returns true or false
[AllowAnonymous]
        [HttpGet("ValidateNiaID", Name = "ValidateNiaID")]
        //[HttpPost("ValidateNiaID", Name = "ValidateNiaID")]
        public async Task<IActionResult> ValidateNiaID([Required] string pin)
        {
            var data = "";
            //niaUrl contains the api url and ValidateNiaID is the Method name of this call
            string apiUrl = niaUrl + "ValidateNiaID/" + pin;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                     data = await response.Content.ReadAsStringAsync();
                    //var result = Newtonsoft.Json.JsonConvert.DeserializeObject<System.Data.DataTable>(data);
                   
                }
            }

            return new JsonResult(data);
        }

//call an API that returns a collection
  [AllowAnonymous]
        [HttpGet("GetNiaIdDetails", Name = "GetNiaIdDetails")]
        //[HttpPost("ValidateNiaID", Name = "ValidateNiaID")]
        public async Task<IEnumerable<GhanaCardDTO>> GetNiaIdDetails([Required] string pin)
        {
            var data = new List<GhanaCardDTO>();
            string apiUrl = niaUrl + "GetNiaIdDetails/" + pin;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    IEnumerable<GhanaCardDTO> dataObj = Enumerable.Empty<GhanaCardDTO>();

                    var strResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                    data = (List<GhanaCardDTO>)JsonConvert.DeserializeObject<IEnumerable<GhanaCardDTO>>(strResult);

                    return data;
                }

                return data;
            }

        }
