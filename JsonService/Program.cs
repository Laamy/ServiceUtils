using System;

namespace JsonServiceUtil
{
    class TestRoot
    {
        public TestRoot() {} // required for json deserialization

        public TestRoot(string ello)
        {
            this.ello = ello;
        }

        public string ello { get; set; }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            TestRoot testRoot = new TestRoot("test1");

            Console.WriteLine(JsonService.FromObj(testRoot)); // {"ello":"test1"}

            TestRoot testRoot2 = JsonService.GetT<TestRoot>("{\"ello\":\"test2\"}");

            Console.WriteLine(testRoot2.ello); // test2

            Console.ReadKey();
        }
    }
}
