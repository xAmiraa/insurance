﻿using System.Collections.Generic;

namespace InsuranceClaims.Data.ThirdPartyInfo
{
    public class CountryInfoApi
    {
        public string Name { get; set; }
        public string Alpha3Code { get; set; }
        public string Capital { get; set; }
        public string Subregion { get; set; }
        public string Flag { get; set; }
        public string NativeName { get; set; }
        public List<CountryInfoApi_Currency> Currencies { get; set; }
        public List<string> CallingCodes { get; set; }
    }

    public class CountryInfoApi_Currency
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
    }

}
