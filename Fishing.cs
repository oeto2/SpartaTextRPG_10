using SpartaTextRPG.DataClass.Quest;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        private static string passKey = "";
        private static int delay = 100;

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

                Thread.Sleep(delay);

                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey();
                    if (passKey == keyInfo.Key.ToString())
                    {
                        delay = 0;
                        continue;
                    }
                    if (keyInfo.Key == ConsoleKey.Spacebar)
                    {
                        if (x == starPosition)
                        {
                            int fish = random.Next(0, 1000);
                            Console.WriteLine("성공했습니다.");
                            if (fish == 17)
                            {
                                QuestBool.CatchFish(FishType.Fire);
                                Color.ChangeTextColor(Colors.RED, "전설의", "불고기", "가 존재하였습니다.");
                                Item.Instance.fishList[3].count++;
                            }
                            else if(fish <=10)
                            {
                                QuestBool.CatchFish(FishType.Gold);
                                Color.ChangeTextColor(Colors.YELLOW, "아닛! 이것이", "골드물고기", "!?!?");
                                Item.Instance.fishList[2].count++;
                            }
                            else if(fish>= 800)
                            {
                                QuestBool.CatchFish(FishType.Sliver);
                                Color.ChangeTextColor(Colors.BLUE, "흠...", "실버물고기", "를 낚았군요.");
                                Item.Instance.fishList[1].count++;
                            }
                            else
                            {
                                QuestBool.CatchFish(FishType.Nomal);
                                Console.Write("그냥 물고기를 낚았습니다.");
                                Item.Instance.fishList[0].count++;
                            }
                            Thread.Sleep(300);
                        }
                        else
                        {
                            Console.WriteLine("실패했습니다.");
                        }
                        passKey = keyInfo.Key.ToString();
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
                else
                {
                    delay = 100;
                    passKey = " ";
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
            Thread.Sleep(500);
        }
    }
}
