using System;

namespace DataServiceUtil
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DataService data = DataService.Get();

            data["Key1"] = "Value1";
            data["header1", "Key1"] = "Value1";
            data["header2", "Key1"] = "Value1";

            Console.WriteLine(data["Key1"]);
            Console.WriteLine(data["header1", "Key1"]);
            Console.WriteLine(data["header2", "Key1"]);

            Console.ReadKey();
        }
    }
}