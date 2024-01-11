using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaTextRPG
{
    internal class Fishing
    {
        public static Fishing instance = new Fishing();
        private enum Direction
        {
            Right = 0,
            Left = 1
        }

        private static int[] stars = new int[10];

        public void StartFishing()
        {
            Direction dir = Direction.Right;
            int x = 0;

            Random random = new Random();
            int starPosition = random.Next(10);
            Array.Fill(stars, -1); 
            stars[starPosition] = starPosition;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("                                      ,!..        \r\n                                      -;:~        \r\n                                   ,:~~~=~-       \r\n                                  :,,,...-:.      \r\n                                  !*~~,-,,-,      \r\n                                  @:,.-,.*-       \r\n                                  !    ..@:       \r\n                                 ~~,,*. !=-       \r\n                                 ,~. ,,,.~        \r\n                                 :!:=---~=        \r\n                                 :!:,,~-,#:       \r\n                                ,!*.,~,!;!;.      \r\n          =.                   ,~:*~,;,.!;=.!     \r\n         ,.~.                 .,.:!,,,--;;-.-:    \r\n         *  ,~               ,,..,:~,~*;;!,..-,   \r\n        .    -=             .;....;!;=!;;.,...!.  \r\n        ;     :,            -.....;:;:;;:,,,...*  \r\n       ,.      -~.          ;.-...!!;;;:,,,.~..,  \r\n                ~=         !..,.,.*!!;;-,,,,,.,,- \r\n       :         ,=       ~,..*,,,$=;;;,,,,,,,,-- \r\n      ,           ;-.     -,.,!,,,@;!:.,,,,:::-,-~\r\n      .             ~    .~..,:,,.*;~,,,,,.$!-,,-,\r\n     -               :.  ..,,,!.-!;;-,-,==:,,,,,-,\r\n                  .~:~:! !,,,,,,=$!!,:*:,,,,,,,,;-\r\n                .*$*:,,!*,,,,,,-*#;$~$;-,,,,,,,,~~\r\n    .          #;-,,~,,!#=,,,,;;-.-   ;-,,,,,,,~!*\r\n   .         .;,-,~,,,-~==;,,-!*-,* ,*-*:,,,,,,*~;\r\n   .         :=,-,,,,,=!*=-,-*;~~:,-~,,;!.,,,,~-,-\r\n  .         $!,,,,,,,,~-;;*!~:-,:#!!~,-!!=,,,!-,,,\r\n  .        *=--,,,,,,,: .-!!:~:~=;!*;=!-**!!!,,~,,\r\n          .!,,,,!~,,,,~   .-!;#$;*.-=*;;:,,,,,,,,:\r\n .          ~:=#~*-,,,~~    -;=$!,=. . ;!*,,,,,,,~\r\n            :#=,,;~--,,--,.=***=**    . ;:,,,,-~*-\r\n.            !$,,-~--~,,-:;**!==*=:   ,.,!:,-~;!~,\r\n             .*$-,,,,,~,,-!**;!**!-:,    =*= ;-,,,");
                Console.SetCursorPosition(x*3, 35);
                Console.Write('↓');
                Console.WriteLine();

                for (int i = 0; i < 10; i++)
                {
                    if (stars[i] == i)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(" * ");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.Write(" x ");
                    }
                }
                Console.WriteLine();
                Console.WriteLine("0. 나가기");

                System.Threading.Thread.Sleep(100);


                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey();
                    if (keyInfo.Key == ConsoleKey.Spacebar)
                    {
                        if (x == starPosition)
                        {
                            int getGold = random.Next(0, 20);
                            Player.player.gold += getGold;
                            Console.WriteLine("성공했습니다.");
                            Console.WriteLine(getGold + "원을 획득했습니다.");
                        }
                        else
                        {
                            Console.WriteLine("실패했습니다.");
                        }

                        // Program exit
                        break;
                    }
                    else if(keyInfo.Key == ConsoleKey.NumPad0 ||
                        keyInfo.Key == ConsoleKey.D0)
                    {
                        Program.scene = Scene.mainScene;
                        Console.Clear();
                        break;
                    }
                }
                if (dir == Direction.Right)
                {
                    x++;
                    if (x == 9)
                    {
                        dir = Direction.Left;
                    }
                }
                else
                {
                    x--;
                    if (x == 0)
                    {
                        dir = Direction.Right;
                    }
                }
            }
            System.Threading.Thread.Sleep(500);
        }
    }
}
