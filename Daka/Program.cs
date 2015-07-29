using System;

namespace Daka
{
    class Program
    {
        static void Main(string[] args)
        {
            //Note: If you get exception of IE security issue then you can look at http://stackoverflow.com/questions/14952348/not-able-to-launch-ie-browser-using-selenium2-webdriver-with-java
            SeleniumHelper.Execute();

            Console.Read();
        }


    }
}
