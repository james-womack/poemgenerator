using System;
using System.Diagnostics;
using System.IO;
using PoetryEngine;

namespace LinuxGen {
    class Program {
        static void Main(string[] args) {
            Generator.Load();
            switch(Environment.OSVersion.Platform) {
                case PlatformID.Unix:
                    UnixGen();
                    break;
                default:
                    WinGen();
                    break;
            }
        }
        public static void UnixGen() {
            string poem;
            do {
                poem = Generator.Gen();
                var cmd = "say \"" + poem + "\"";
                var startInfo = new ProcessStartInfo("/bin/bash", "-c \"" + cmd + "\"");
                var proc = new Process() { StartInfo = startInfo, };
                proc.Start();
            } while(false);
        }

        public static void WinGen() {
            string poem;
            Process proc;
            do {
                
                poem = Generator.Gen();
                System.IO.File.WriteAllText(@"C:\Users\Public\poem\temp.txt", poem);
                var cmd = "ptts -u " + @"C:\Users\Public\poem\temp.txt";
                var startInfo = new ProcessStartInfo("cmd.exe", "/c" + cmd);
                proc = new Process() { StartInfo = startInfo, };
                proc.Start();
                while (!proc.HasExited)
                    System.Threading.Thread.Sleep(5000);
            } while(true);
        }
    }
}
