using System;

namespace Member.Service.Model
{
    public class JWTModel
    {
        public string Issuer {get; set;}
        public string Audience { get; set; }
        public string SecretKey { get; set; }
        public double ExpirationInMinutes { get; set; }
    }
}

