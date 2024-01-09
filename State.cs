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
        public static void Status()
        {
            Console.Clear();
            string job = Player.player.job.ToString();

            if (job == ((Job)0).ToString())
            {
                job = "초보자";
            }
            else if ((job == ((Job)1).ToString()))
            {
                job = "전사";
            }
            else if ((job == ((Job)2).ToString()))
            {
                job = "도적";
            }
            else if ((job == ((Job)3).ToString()))
            {
                job = "마법사";
            }
            else if ((job == ((Job)4).ToString()))
            {
                job = "궁수";
            }
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("상태 보기");
            Console.ResetColor();
            Console.WriteLine("캐릭터의 정보가 표시됩니다.\n");
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
            Console.Write(">>");
            string read = Console.ReadLine();
            switch (read)
            {
                case "1":
                    FirstJob();
                    break;
                case "0":
                    Console.WriteLine("홈으로...");
                    break;
                default:
                    Console.WriteLine("\n잘못된 입력입니다.\n");
                    Thread.Sleep(500);
                    Status();
                    break;
            }
        }

        public static void FirstJob()
        {

            Console.Clear();
            string job = Player.player.job.ToString();

            if (job == ((Job)0).ToString())
            {
                job = "초보자";
            }
            else if ((job == ((Job)1).ToString()))
            {
                job = "전사";
            }
            else if ((job == ((Job)2).ToString()))
            {
                job = "도적";
            }
            else if ((job == ((Job)3).ToString()))
            {
                job = "마법사";
            }
            else if ((job == ((Job)4).ToString()))
            {
                job = "궁수";
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("전직 하기");
            Console.ResetColor();

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
                            Player.player.job = (Job)1;
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
                    Console.WriteLine("잘못된 값입니다.");
                    FirstJob();
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
