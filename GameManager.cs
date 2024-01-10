using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaTextRPG
{
    enum Scene
    {
        mainScene,
        playerState,
        inventory,
        shop,
        Dungeon,
        rest
    }
    public class GameManager
    {
        public static void MainGameScene()
        {
            Console.Title = "스파르타 던전";
            Console.WriteLine("Sparta Dungeon Game!");
            Console.ResetColor();
            Console.Write($" ");
            Console.ResetColor();
            Console.WriteLine("님, 스파르타 마을에 오신것을 환영합니다!\n");
            Console.WriteLine("이곳에서 던전으로 돌아가기 전 활동을 할 수 있습니다.\n");
            Console.WriteLine("0. 게임 종료");
            Console.WriteLine("1. 상태 보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine("3. 상점 입장");
            Console.WriteLine("4. 던전 입장");
            Console.WriteLine("5. 휴식하기");
            Console.WriteLine();
            int _input = CheckValidAction(0, 5);

            switch (_input)
            {
                case 0:
                    Environment.Exit(0);
                    break;
                case 1:
                    Console.Clear();
                    Program.scene = Scene.playerState;
                    break;
                case 2:
                    Console.Clear();
                    Program.scene = Scene.inventory;
                    break;
                case 3:
                    Console.Clear();
                    Program.scene = Scene.shop;
                    break;
                case 4:
                    Console.Clear();
                    Program.scene = Scene.Dungeon;
                    break;
                case 5:
                    Console.Clear();
                    Program.scene = Scene.rest;
                    break;
            }
        }
        static int CheckValidAction(int _min, int _max)
        {
            while (true)
            {
                Console.WriteLine(" ");
                Console.WriteLine(" 원하시는 행동을 입력해주세요.");
                Console.Write(">>");
                string _input = Console.ReadLine();

                bool _parseSuccess = int.TryParse(_input, out var _ret);
                if (_parseSuccess)
                {
                    if (_ret >= _min && _ret <= _max)
                        return _ret;
                }
                Console.WriteLine("잘못된 입력입니다.");
            }
        }
    }
}
