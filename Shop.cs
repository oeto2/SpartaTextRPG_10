using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaTextRPG
{
    internal class Shop
    {
        public enum Color
        {
            RED,
            BLUE,
            YELLOW,
            MAGENTA
        }
        //
        public void ShowShopPage()
        {
            ChangeTextColor(Color.YELLOW, "", "상점", "\n");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");

            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
        }

        // 문자열 색깔 넣기
        public void ChangeTextColor(Color color, string str1, string colorstr, string str2 = "")
        {
            Console.Write(str1);

            switch (color)
            {
                case Color.RED:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case Color.BLUE:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                case Color.YELLOW:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case Color.MAGENTA:
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    break;
            }

            Console.Write(colorstr);
            Console.ResetColor();
            Console.Write(str2);
        }
    }

}
