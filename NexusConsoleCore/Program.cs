using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NexusConsoleCore
{
    class Program
    {
            private static async Task<float[]> DownloadWebAsync(int arrayLength)
            {
                var values = await WaitArrayData(arrayLength);

                await Task.Delay(2000);

                return values;
            }

            private static async Task<float[]> WaitArrayData(int arrayLength)
            {
                var task1 = Task.Factory.StartNew(() =>
                {
                    float[] values = Enumerable.Range(0, arrayLength).Select(i => (float)i / 10).ToArray();
                    return values;
                });

                return await task1;
            }

            public static int Main_orig(float[] values)
            {
                int valuesLength = values.Length;

                return valuesLength;
            }
            private static void VerifyBreakpoint()
            {
                int iGlobal = 0;
                for (int i = 0; i < 50; i++)
                {
                    iGlobal++; //set bp #1
                    i++;
                }
            }
            private static int VerifyEE()
            {
                List<string> fruits = new List<string> { "apple", "passionfruit", "banana", "mango", "orange", "blueberry", "grape", "strawberry" };
                IEnumerable<string> query = fruits.Where(fruit => fruit.Length < 6);
                foreach (string fruit in query)
                {
                    Console.WriteLine(fruit);
                }

                object[] pList = new object[] { 1, "one", 2, "two", 3, "three" };
                var query1 = pList.OfType<string>();
                dynamic expObj = new ExpandoObject();
                expObj.FirstName = "Daffy";
                expObj.LastName = "Duck";

                Dictionary<string, string> mygroup = new Dictionary<string, string>() { { "Hannah","Zhang"},{ "Alex","Yao"},{ "Alisa","Zhang"}
                ,{ "Nelson","Yan"},{ "Richard","Zeng"},{ "Clarie","Kang"},{ "Qian","Wang"},{ "Serena","Wang"},{ "Maggie","Zhang"},{ "Cherry","Wu"}
                ,{ "Lynn","Zhang"},{ "Grace","Dong"} };

                return 0; //set bp #2
            }

            private static void VerifyException()
            {
                while (true)
                {
                    Thread.Sleep(100);

                    try
                    {
                        throw new InvalidOperationException();
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }

            private static int VerifyRecursion(int m)
            {
                if (m <= 1)
                    return 1;
                return m * VerifyRecursion(m - 1);
            }
            static async Task Main(string[] args)
            {
                int a = 1;
                VerifyBreakpoint();
                VerifyEE();
                VerifyRecursion(10);

                try
                {
                    string s1 = String.Format("{1}", "ab12");
                }
                catch (Exception ex)
                {
                    string s = ex.ToString();
                }

                if (a == 10)
                {
                    VerifyException();
                }
                var values = await DownloadWebAsync(100);
                Main_orig(values);
            }
    }
}
