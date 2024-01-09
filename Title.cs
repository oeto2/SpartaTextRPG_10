using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaTextRPG
{
    internal class Title
    {
        public static Title instance = new Title();


        public void ShowTitle()
        {
            Console.Clear();
            Console.WriteLine("  ____                   _          ____  ____   ____ ");
            Console.WriteLine(" / ___| _ __   __ _ _ __| |_ __ _  |  _ \\|  _ \\ / ___|");
            Console.WriteLine(" \\___ \\| '_ \\ / _` | '__| __/ _` | | |_) | |_) | |  _ ");
            Console.WriteLine("  ___) | |_) | (_| | |  | || (_| | |  _ <|  __/| |_| |");
            Console.WriteLine(" |____/| .__/ \\__,_|_|   \\__\\__,_| |_| \\_\\_|    \\____|");
            Console.WriteLine("       |_|                                            ");
            Console.WriteLine("============================================================");
            Console.WriteLine("                 PRESS ANIKEY TO START                      ");
            Console.WriteLine("============================================================");
            Console.CursorVisible = false;
            Console.ReadKey();
            Console.CursorVisible = true;
        }
    }
}
