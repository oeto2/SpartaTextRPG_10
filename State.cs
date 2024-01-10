using SpartaTextRPG;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading;
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
            Job job = Player.player.job;

            if (job == ((Job)0))
            {
                instance.job = "초보자";
            }
            else if ((job == ((Job)1)))
            {
                instance.job = "전사";
            }
            else if ((job == ((Job)2)))
            {
                instance.job = "도적";
            }
            else if ((job == ((Job)3)))
            {
                instance.job = "마법사";
            }
            else if ((job == ((Job)4)))
            {
                instance.job = "궁수";
            }
            else if ((job == ((Job)5)))
            {
                instance.job = "버서커";
            }
            else if ((job == ((Job)6)))
            {
                instance.job = "워로드";
            }
            else if ((job == ((Job)7)))
            {
                instance.job = "리퍼";
            }
            else if ((job == ((Job)8)))
            {
                instance.job = "데모닉";
            }
        }

        // 상태창
        public void Status()
        {
            Console.Clear();
            checkJob();
            Color.ChangeTextColor(Colors.YELLOW, "", "상태 보기", "\n");
            Console.WriteLine("캐릭터의 정보가 표시됩니다.\n");
            Color.ChangeTextColor(Colors.BLUE, "", Player.player.name, " 환영합니다!\n");
            Console.WriteLine($"Lv. {Player.player.level}");
            Console.WriteLine($"chad < {job} >\n");
            Console.WriteLine($"공격력 : {Player.player.baseAtk} " + (Player.player.addAtk != 0 ? $"(+{Player.player.addAtk})" : ""));
            Console.WriteLine($"방어력 : {Player.player.baseDef} " + (Player.player.addDef != 0 ? $"(+{Player.player.addDef})" : ""));
            Console.WriteLine($"체력 : {Player.player.maxHp} / {Player.player.hp}");
            Console.WriteLine($"마력 : {Player.player.maxMp} / {Player.player.mp}");
            Console.WriteLine($"Gold : {Player.player.gold} G\n");

            if (Player.player.level >= 1 && Player.player.job.ToString() == ((Job)0).ToString())
            {
                Console.WriteLine("1. 1차 전직하기");
            }
            else if (Player.player.level >= 1 && Player.player.job.ToString() != ((Job)0).ToString())
            {
                Console.WriteLine("1. 2차 전직하기");
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
                        if (Player.player.job == ((Job)0))
                            FirstJob();
                        else if (Player.player.job != ((Job)0))
                            SecondJob();
                        break;
                    case "0":
                        res = true;
                        Console.Clear();
                        Program.scene = Scene.mainScene;
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
                        Console.WriteLine("\n전사로 전직하시겠습니까?(Y)");
                        Console.WriteLine("전사는 밸런스가 좋은 직업이며 체력이 높습니다\n");
                        Console.Write(">>");
                        string check = Console.ReadLine();
                        switch (check.ToLower())
                        {
                            case "y":
                                Console.WriteLine("\n전사로 전직하셨습니다.");
                                Player.player.job = (Job)1;
                                Player.player.baseAtk += 5;
                                Player.player.hp = Player.player.maxHp + 50;
                                Player.player.maxHp += 50;
                                Player.player.baseDef += 5;

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
                        Console.WriteLine("\n도적으로 전직하시겠습니까?(Y)");
                        Console.WriteLine("도적은 체력과 방어력은 낮으나 높은 공격력으로 메인 어택커입니다.\n");
                        Console.Write(">>");
                        check = Console.ReadLine();

                        switch (check.ToLower())
                        {
                            case "y":
                                Console.WriteLine("\n도적으로 전직하셨습니다.");
                                Player.player.job = (Job)2;
                                Player.player.baseAtk += 10;
                                Player.player.hp = Player.player.maxHp + 20;
                                Player.player.maxHp += 20;
                                Player.player.mp = Player.player.maxMp + 30;
                                Player.player.maxMp += 30;

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
            }
        }
        static Job[] second = { Job.Berserker, Job.Warlord };
        static string[] Sjob = { "버서커", "워로드" };
        static void Board()
        {
            if (Player.player.job.ToString() == ((Job)0).ToString())
            {
                Console.WriteLine("     1번    |     2번    |  출시예정  |  출시예정  ");
                Console.WriteLine("  --------  |  --------  |  --------  |  --------  \n");
                Console.WriteLine("    전사    |    도적    |   마법사   |    궁수    ");
                Console.WriteLine("  ________  |  ________  |  ________  |  ________  \n\n");
            }
            else if (Player.player.job.ToString() == ((Job)1).ToString())
            {
                second[0] = Job.Berserker;
                second[1] = Job.Warlord;
                Sjob[0] = "버서커";
                Sjob[1] = "워로드";

                Console.WriteLine("     1번    |     2번       ");
                Console.WriteLine("  --------  |  --------   \n");
                Console.WriteLine("   버서커   |   워로드      ");
                Console.WriteLine("  ________  |  ________ \n\n");

            }
            else if (Player.player.job.ToString() == ((Job)2).ToString())
            {
                second[0] = Job.Reaper;
                second[1] = Job.Demonic;
                Sjob[0] = "리퍼";
                Sjob[1] = "데모닉";

                Console.WriteLine("     1번    |     2번       ");
                Console.WriteLine("  --------  |  --------   \n");
                Console.WriteLine("    리퍼    |   데모닉      ");
                Console.WriteLine("  ________  |  ________ \n\n");

            }
        }
        public void SecondJob()
        {
            Console.Clear();
            checkJob();
            Color.ChangeTextColor(Colors.YELLOW, "", "2차 전직하기", "\n");
            Console.WriteLine("일정 레벨이 될 시 전직이 가능합니다.\n");
            Console.WriteLine($"Lv. {Player.player.level}  < {job} >\n");
            Board();


            Console.WriteLine("원하시는 직업을 선택해주세요(숫자만 입력)");
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
                        Console.WriteLine($"\n{Sjob[0]}로 전직하시겠습니까?(Y)");
                        if (Sjob[0] == "버서커")
                            Console.WriteLine($"버서커는 체력을 소비하여 공격하고 공격 적중시 체력을 흡수하는 클래스 입니다.\n");
                        else
                            Console.WriteLine($"리퍼는 체방이 낮고 최고의 공격력을 가지고 있는 퓨어딜러 클래스입니다.\n");

                        Console.Write(">>");
                        string check = Console.ReadLine();
                        switch (check.ToLower())
                        {
                            case "y":
                                Console.WriteLine($"\n{Sjob[0]}로 전직하셨습니다.");
                                if (Sjob[0] == "버서커")
                                {
                                    Player.player.baseAtk += 10;
                                    Player.player.hp = Player.player.maxHp + 50;
                                    Player.player.maxHp += 50;
                                    Player.player.baseDef += 5;
                                }
                                else
                                {
                                    Player.player.baseAtk += 20;
                                    Player.player.hp = Player.player.maxHp + 10;
                                    Player.player.maxHp += 10;

                                }
                                Player.player.job = (second[0]);

                                Thread.Sleep(1000);
                                Status();
                                break;
                            default:
                                Console.WriteLine("취소하셨습니다");
                                Thread.Sleep(500);
                                SecondJob();
                                break;
                        }
                        break;
                    case "2":
                        Console.WriteLine($"\n{Sjob[1]}로 전직하시겠습니까?(Y)");
                        if (Sjob[0] == "워로드")
                            Console.WriteLine($"워로드는 높은 체방을 가지고 있는 탱커 클래스 입니다.\n");
                        else
                            Console.WriteLine($"데모닉은 밸랜스형 변신 캐릭터 입니다.\n");

                        Console.Write(">>");
                        check = Console.ReadLine();
                        switch (check.ToLower())
                        {
                            case "y":
                                Console.WriteLine($"\n{Sjob[1]}로 전직하셨습니다.");
                                if (Sjob[0] == "워로드")
                                {
                                    Player.player.hp = Player.player.maxHp + 100;
                                    Player.player.maxHp += 100;
                                    Player.player.baseDef += 10;
                                }
                                else
                                {
                                    Player.player.baseAtk += 10;
                                    Player.player.hp = Player.player.maxHp + 50;
                                    Player.player.maxHp += 50;
                                    Player.player.baseDef += 5;

                                }
                                Player.player.job = (second[1]);
                                Status();
                                break;
                            default:
                                Console.WriteLine("취소하셨습니다");
                                Thread.Sleep(500);
                                SecondJob();
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
            }
        }
    }
}
