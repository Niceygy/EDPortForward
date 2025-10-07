using EDPortTest.Web;

namespace EDPortTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Test");
            HTTP.Serve("localhost", 8090);
        }
    }
}