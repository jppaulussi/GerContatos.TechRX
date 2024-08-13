using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Utils;

public static class LocationUtils
{
    public static string GetRegionByState(string state)
    {
        switch (state.ToUpper())
        {
            case "AC":
            case "AP":
            case "AM":
            case "PA":
            case "RO":
            case "RR":
            case "TO":
                return "Norte";
            case "AL":
            case "BA":
            case "CE":
            case "MA":
            case "PB":
            case "PE":
            case "PI":
            case "RN":
            case "SE":
                return "Nordeste";
            case "DF":
            case "GO":
            case "MT":
            case "MS":
                return "Centro-Oeste";
            case "ES":
            case "MG":
            case "RJ":
            case "SP":
                return "Sudeste";
            case "PR":
            case "RS":
            case "SC":
                return "Sul";
            default:
                return "Desconhecido";
        }
    }
}
