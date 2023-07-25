using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

/// <summary>
/// DataService
/// </summary>
public class DataService
{
    // private statics like singlton
    private static DataService singlton;

    // stuff stored in the singlton
    private Dictionary<string, Dictionary<string, string>> configData;
    private string filePath;

    /// <summary>
    /// Get the singlton instance of the DataService.
    /// </summary>
    /// <returns>DataService Singlton</returns>
    public static DataService Get()
    {
        if (singlton == null)
        {
            // create the singlton instance & read config from file
            singlton = new DataService();
            singlton.filePath = "config.cfg"; /* change this to write to a different file */
            singlton.Read();
        }

        return singlton;
    }

    /// <summary>
    /// Initialize a new instance of (Singlton object) DataService
    /// </summary>
    public DataService()
    {
        configData = new Dictionary<string, Dictionary<string, string>>();
    }

    /// <summary>
    /// Get a value stored in key from datastore using a custom header
    /// </summary>
    /// <param name="key">Key to read from</param>
    /// <returns>Value read from the key in the custom header</returns>
    /// <exception cref="KeyNotFoundException">Failure to find header or key</exception>
    public string this[string headerName, string key]
    {
        get
        {
            if (configData.ContainsKey(headerName) && configData[headerName].ContainsKey(key))
            {
                return configData[headerName][key];
            }
            else
            {
                throw new KeyNotFoundException($"Header '{headerName}' or key '{key}' not found.");
            }
        }
        set
        {
            if (!configData.ContainsKey(headerName))
            {
                configData.Add(headerName, new Dictionary<string, string>());
            }

            configData[headerName][key] = value;

            Save();
        }
    }

    /// <summary>
    /// Get a value stored in key from datastore using the "default" header
    /// </summary>
    /// <param name="key">Key to read from</param>
    /// <returns>Value read from the key in the "default" header</returns>
    /// <exception cref="KeyNotFoundException">Failure to find header or key</exception>
    public string this[string key]
    {
        get => this["default", key];
        set => this["default", key] = value;
    }

    private void Save()
    {
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            foreach (var header in configData)
            {
                writer.WriteLine($"[{header.Key}]");

                foreach (var entry in header.Value)
                {
                    writer.WriteLine($"{entry.Key} = {entry.Value}");
                }

                writer.WriteLine();
            }
        }
    }

    private void Read()
    {
        if (!File.Exists(filePath))
        {
            Save();
            return;
        }

        string currentHeader = null;

        foreach (string line in File.ReadAllLines(filePath))
        {
            if (line.Trim().StartsWith("[") && line.Trim().EndsWith("]"))
            {
                currentHeader = line.Trim().Substring(1, line.Trim().Length - 2);
                configData[currentHeader] = new Dictionary<string, string>();
            }
            else if (!string.IsNullOrWhiteSpace(line) && line.Contains("=") && currentHeader != null)
            {
                int separatorIndex = line.IndexOf('=');
                string key = line.Substring(0, separatorIndex).Trim();
                string value = line.Substring(separatorIndex + 1).Trim();

                configData[currentHeader][key] = value;
            }
        }
    }
}