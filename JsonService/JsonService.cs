using System.Collections.Generic;
using System.IO;
using System.Web.Script.Serialization;

/// <summary>
/// JsonService
/// </summary>
public class JsonService
{
    /// <summary>
    /// Convert json string to T
    /// </summary>
    /// <typeparam name="T">JSON Root</typeparam>
    /// <param name="json">JSON string</param>
    /// <returns>Finished JSON Root Instance</returns>
    public static T GetT<T>(string json)
    {
        JavaScriptSerializer jss = new JavaScriptSerializer();

        return jss.Deserialize<T>(json);
    }

    /// <summary>
    /// Convert object to json string
    /// </summary>
    /// <param name="obj">JSON object</param>
    /// <returns>Finished JSON string</returns>
    public static string FromObj(object obj)
    {
        JavaScriptSerializer jss = new JavaScriptSerializer();

        return jss.Serialize(obj);
    }
}