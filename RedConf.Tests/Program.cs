using System;
using System.IO;
using RedConf.Core.Statements;

namespace RedConf.Tests {
    public class Program {

        public class Config {

            public string StringTest { get; set; }

            public int IntTest { get; set; }

            public bool BooleanTest { get; set; }

        }

        public static void Main(string[] args) {
            new Program().Run(args);
        }

        public void Run(string[] args) {
            var fileStream = new FileStream("config.cnf", FileMode.Open, FileAccess.Read, FileShare.Read);
            var streamReader = new StreamReader(fileStream);
            var config = RedConf.GetTokens(streamReader.ReadToEnd());
            var count = 0;
            foreach (var statement in config.Statements) {
                Console.WriteLine($"{count}: [{statement.GetType().Name}] {statement}");
                count++;
            }
        }

    }
}
