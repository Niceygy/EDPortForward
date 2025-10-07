using EDPortTest.Web;

namespace EDPortTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Loading EDPortTest server...");
            HTTP.Serve("+", 8090);
        }
    }
}