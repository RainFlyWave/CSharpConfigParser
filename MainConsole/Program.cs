using System;
using Parser;

namespace MainConsole
{
    class Program
    {

        static void Main(string[] args)
        {
            string filename = @"../test_config.ini";
            ParserConfig parser = new ParserConfig(filename);
            
            string restServerAddress = parser.GetValue("rest_config", "server_address");
            Console.WriteLine($"Server address: {restServerAddress}");

        }


    }
}