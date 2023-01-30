using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

public static class MyHelpers
{

    public static bool ValidateJSON(this string s)
    {
        try
        {
            JToken.Parse(s);
            JObject.Parse(s);
            return true;
        }
        catch (JsonReaderException ex)
        {
            //Debug.Log(ex);
            return false;
        }
    }
}
