using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FilmesAPI.Models.ValueObject
{
    public class RG
    {

        public RG(string rg)
        {
            rg = LimpaRG(rg);
            rg = ValidarRg(rg);
        }


        public string LimpaRG(string rg)
        {
            rg = rg.Trim();
            rg = Regex.Replace(rg, "[^a-zA-Z0-9]", "");

            return rg;
        }
        public string ValidarRg(string rg)
        {
            string novorg = rg.PadLeft(9, '0');
            return novorg;
        }


    }
}
