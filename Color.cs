using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaTextRPG
{
    public enum Colors
    {
        RED,
        BLUE,
        YELLOW,
        MAGENTA,
        GREEN
    }

    internal class Color
    {
        // 문자열 색깔 넣기
        public static void ChangeTextColor(Colors color, string str1, string colorstr, string str2 = "")
        {
            Console.Write(str1);

            switch (color)
            {
                case Colors.RED:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case Colors.BLUE:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                case Colors.YELLOW:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case Colors.MAGENTA:
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    break;
                case Colors.GREEN:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
            }

            Console.Write(colorstr);
            Console.ResetColor();
            Console.Write(str2);
        }
    }
}
