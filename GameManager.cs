using SpartaTextRPG.DataClass.Quest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace SpartaTextRPG
{
    enum Scene
    {
        mainScene,
        playerState,
        inventory,
        shop,
        dungeon,
        rest,
        fishing,
        guild
    }
    public class GameManager
    {
        public static GameManager instance = new GameManager();
        public void MainGameScene()
        {
            Console.Title = "스파르타 던전";
            Console.WriteLine("Sparta Dungeon Game!");
            Color.ChangeTextColor(Colors.MAGENTA, "", $"{Player.player.name} ");
            Console.WriteLine("님, 스파르타 마을에 오신것을 환영합니다!\n");
            Console.WriteLine("이곳에서 던전으로 돌아가기 전 활동을 할 수 있습니다.\n");
            Console.WriteLine("1. 상태 보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine("3. 상점 입장");
            Console.WriteLine("4. 던전 입장");
            Console.WriteLine("5. 휴식 하기");
            Console.WriteLine("6. 낚시 하기");
            Console.WriteLine("7. 길드 입장");
            Color.ChangeTextColor(Colors.RED, "", "9", ". 게임종료\n\n");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">> ");
            string answer = Console.ReadLine();
            Console.Clear();
            switch (answer)
            {
                case "1":
                    Program.scene = Scene.playerState;
                    break;
                case "2":
                    Program.scene = Scene.inventory;
                    break;
                case "3":
                    Program.scene = Scene.shop;
                    break;
                case "4":
                    Program.scene = Scene.dungeon;
                    break;
                case "5":
                    Program.scene = Scene.rest;
                    break;
                case "6":
                    Program.scene = Scene.fishing;
                    break;
                case "7":
                    Program.scene = Scene.guild;
                    break;
                case "9":
                    Environment.Exit(0);
                    break;
                default:
                    Color.ChangeTextColor(Colors.RED, "", "잘못된 입력입니다.", "\n");
                    break;
            }
        }
        public void Rest()
        {   
            QuestBool.isRest = true;
            Player.player.hp = Player.player.maxHp;
            Console.WriteLine("휴 식");
            Color.ChangeTextColor(Colors.MAGENTA, "", $"{Player.player.name} ");
            Console.WriteLine("님, 휴식을 할수있는 공간에 오셨습니다.\n");
            Console.WriteLine("체력이 회복됩니다.\n");
            Console.WriteLine("1. 상태 보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine("3. 상점 입장");
            Color.ChangeTextColor(Colors.RED, "", "0", ". 나가기\n\n");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">> ");
            string answer = Console.ReadLine();
            Console.Clear();
            switch (answer)
            {
                case "0":
                    Program.scene = Scene.mainScene;
                    break;
                case "1":
                    Program.scene = Scene.playerState;
                    break;
                case "2":
                    Program.scene = Scene.inventory;
                    break;
                case "3":
                    Program.scene = Scene.shop;
                    break;
                default:
                    Color.ChangeTextColor(Colors.RED, "", "잘못된 입력입니다.", "\n");
                    break;
            }
        }
    }
}
