using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WeShareProject.Models
{
    public class Users
    {

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "UserName")]
        public string UserName { get; set; }

        [JsonProperty(PropertyName = "Password")]
        public string Password { get; set; }

    }
}
