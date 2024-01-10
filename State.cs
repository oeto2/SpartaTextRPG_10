using SpartaTextRPG;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaTextRPG
{
    public class State
    {
        public static State instance = new State();

        string job;

        //직업 확인
        static void checkJob()
        {
            string job = Player.player.job.ToString();

            if (job == ((Job)0).ToString())
            {
                instance.job = "초보자";
            }
            else if ((job == ((Job)1).ToString()))
            {
                instance.job = "전사";
            }
            else if ((job == ((Job)2).ToString()))
            {
                instance.job = "도적";
            }
            else if ((job == ((Job)3).ToString()))
            {
                instance.job = "마법사";
            }
            else if ((job == ((Job)4).ToString()))
            {
                instance.job = "궁수";
            }
        }

        // 상태창
        public void Status()
        {
            Console.Clear();
            checkJob();
            Color.ChangeTextColor(Colors.YELLOW, "", "상태 보기", "\n");
            Console.WriteLine("캐릭터의 정보가 표시됩니다.\n");
            Console.WriteLine($"{Player.player.name}\n");
            Console.WriteLine($"Lv. {Player.player.level}");
            Console.WriteLine($"chad < {job} >\n");
            Console.WriteLine($"공격력 : {Player.player.baseAtk} " + (Player.player.addAtk != 0 ? $"(+{Player.player.addAtk})" : ""));
            Console.WriteLine($"공격력 : {Player.player.baseDef} " + (Player.player.addDef != 0 ? $"(+{Player.player.addDef})" : ""));
            Console.WriteLine($"체력 : {Player.player.maxHp} / {Player.player.hp}");
            Console.WriteLine($"마력 : {Player.player.maxMp} / {Player.player.mp}");
            Console.WriteLine($"Gold : {Player.player.gold} G\n");
            
            if(Player.player.level >= 1 && Player.player.job.ToString() == ((Job)0).ToString())
            {
                Console.WriteLine("1. 전직하기");
            }

            Console.WriteLine("0. 나가기\n");
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            bool res = false;
            while (!res)
            {
                Console.Write(">>");
                string read = Console.ReadLine();
                switch (read)
                {
                    case "1":
                        res = true;
                        FirstJob();
                        break;
                    case "0":
                        res = true;
                        Console.Clear();
                        Program.scene = Scene.mainScene;
                        GameManager.MainGameScene();
                        break;
                    default:
                        Console.WriteLine("\n잘못된 값입니다.");
                        break;
                }
            }
        }

        // 1차 전직 하기
        public void FirstJob()
        {

            Console.Clear();
            checkJob();
            Color.ChangeTextColor(Colors.YELLOW, "", "전직하기", "\n");
            Console.WriteLine("일정 레벨이 될 시 전직이 가능합니다.\n");
            Console.WriteLine($"Lv. {Player.player.level}  < {job} >\n");
            Board();


            Console.WriteLine("원하시는 직업을 선택해주세요(숫자만 입력)");
            Console.Write(">>");
            string read = Console.ReadLine();

            switch (read)
            {
                case "1":
                    Console.WriteLine("\n전사로 전직하시겠습니까?(Y/N)");
                    Console.WriteLine("전사는 \n");
                    Console.Write(">>");
                    string check = Console.ReadLine();
                    switch (check.ToLower())
                    {
                        case "y":
                            Console.WriteLine("\n전사로 전직하셨습니다.");
                            Player.player.job = (Job)1;
                            Player.player.baseAtk = 5;
                            Player.player.maxHp = 100;
                            Player.player.hp = 100;
                            Player.player.baseDef = 5;

                            Thread.Sleep(1000);
                            Status();
                            break;
                        default:
                            Console.WriteLine("취소하셨습니다");
                            Thread.Sleep(500);
                            FirstJob();
                            break;
                    }
                    break;
                case "2":
                    Console.WriteLine("\n도적으로 전직하시겠습니까?(Y/N)");
                    Console.WriteLine("도적은 \n");
                    Console.Write(">>");
                    check = Console.ReadLine();

                    switch (check.ToLower())
                    {
                        case "y":
                            Console.WriteLine("\n도적으로 전직하셨습니다.");
                            Player.player.job = (Job)2;
                            Player.player.baseAtk = 10;
                            Player.player.maxHp = 70;
                            Player.player.hp = 70;
                            Player.player.maxMp = 50;
                            Player.player.mp = 50;

                            Thread.Sleep(1000);
                            Status();
                            break;
                        default:
                            Console.WriteLine("취소하셨습니다");
                            Thread.Sleep(500);
                            FirstJob();
                            break;
                    }
                    break;
                case "0":
                    Status();
                    break;
                default:
                    Console.WriteLine("\n잘못된 값입니다.");
                    break;
            }

            static void Board()
            {
                Console.WriteLine("     1번    |     2번    |  출시예정  |  출시예정  ");
                Console.WriteLine("  --------  |  --------  |  --------  |  --------  ");
                Console.WriteLine("    전사    |    도적    |   마법사   |    궁수    ");
                Console.WriteLine("  ________  |  ________  |  ________  |  ________  \n\n");
            }
        }
    }
}
