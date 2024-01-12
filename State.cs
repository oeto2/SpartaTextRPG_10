using SpartaTextRPG;
using SpartaTextRPG.DataClass;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Numerics;
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
            checkJob();
            Color.ChangeTextColor(Colors.YELLOW, "", "상태 보기", "\n\n");
            Color.ChangeTextColor(Colors.MAGENTA, "모험가 ", Player.player.name, "\n\n");
            Console.WriteLine($"Lv. {Player.player.level}");
            Console.WriteLine($"chad < {job} >\n");
            Console.WriteLine($"공격력 : {Player.player.baseAtk} " + (Player.player.addAtk != 0 ? $"(+{Player.player.addAtk})" : ""));
            Console.WriteLine($"방어력 : {Player.player.baseDef} " + (Player.player.addDef != 0 ? $"(+{Player.player.addDef})" : ""));
            Console.WriteLine($"체력 : {Player.player.maxHp} / {Player.player.hp}");
            Console.WriteLine($"마력 : {Player.player.maxMp} / {Player.player.mp}");
            Console.WriteLine($"Gold : {Player.player.gold} G\n");
            Console.WriteLine($"경험치 : {Player.player.levelExp - Player.player.needExp} / {Player.player.levelExp} ({(Player.player.needExp == 0 ? "0" : Convert.ToInt32((Player.player.levelExp - Player.player.needExp) * 100.00 / Player.player.levelExp))}%)\n");

            Console.WriteLine($"보유 스킬 {Skills.myskills.Count}개\n");
            foreach (var skill in Skills.myskills)
            {
                Color.ChangeTextColor(Colors.BLUE, "",$"{ skill.name} ", $"{skill.job} 전용스킬\n");
                Console.Write($"데미지 피해량 : {Math.Truncate(skill.damage * (Player.player.baseAtk + Player.player.addAtk))}   ");
                if(skill.hp != 0)
                {
                    Console.WriteLine($"HP : {skill.hp}");
                }
                if (skill.mp != 0)
                {
                    Console.WriteLine($"MP : {skill.mp}");
                }
                Console.WriteLine($"{skill.text}\n");

            }


            int weapon = Player.player.weapon;
            int armor = Player.player.armor;

            string wname = "미착용";
            string aname = "미착용";
            foreach (var item in Item._instance.equipItems)
            {
                if (weapon == item.id)
                {
                    wname = item.name;
                }
                if (armor == item.id)
                {
                    aname = item.name;
                }
            }

            Console.WriteLine($"무기 : {wname}");
            Console.WriteLine($"방어구 : {aname} \n");


            Console.WriteLine("9. 인벤토리");

            if (Player.player.level >= 1 && Player.player.job.ToString() == ((Job)0).ToString())
            {
                Color.ChangeTextColor(Colors.BLUE, "", "1. 1차 전직하기", "\n");
            }
            else if (Player.player.level >= 3 && (Player.player.job.ToString() == ((Job)1).ToString() || Player.player.job.ToString() == ((Job)2).ToString()))
            {
                Color.ChangeTextColor(Colors.BLUE, "", "1. 2차 전직하기", "\n");
            }

            Color.ChangeTextColor(Colors.RED, "", "0. 나가기", "\n\n");
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">>");

            string read = Console.ReadLine();
            switch (read)
            {
                case "1":
                    if (Player.player.job == ((Job)0))
                    {
                        Console.Clear();
                        FirstJob();
                    }
                    else if (Player.player.job.ToString() == ((Job)1).ToString() || Player.player.job.ToString() == ((Job)2).ToString())
                    {
                        Console.Clear();
                        SecondJob();
                    } else
                    {
                        Console.Clear();
                        Color.ChangeTextColor(Colors.RED, "", "잘못된 입력입니다.", "\n");
                        Status();
                    }
                    break;
                case "9":
                    Console.Clear();
                    Program.scene = Scene.inventory;
                    break;
                case "0":
                    Console.Clear();
                    Program.scene = Scene.mainScene;
                    break;
                default:
                    Console.Clear();
                    Color.ChangeTextColor(Colors.RED, "", "잘못된 입력입니다.", "\n");
                    Status();
                    break;
            }
        }

        // 1차 전직 하기
        public void FirstJob()
        {
            checkJob();
            Color.ChangeTextColor(Colors.YELLOW, "", "전직하기", "\n");
            Console.WriteLine("일정 레벨이 될 시 전직이 가능합니다.\n");
            Console.WriteLine($"Lv. {Player.player.level}  < {job} >\n");
            Board();


            Console.WriteLine("원하시는 직업을 선택해주세요(숫자만 입력)\n");
            Color.ChangeTextColor(Colors.RED, "", "0. 나가기", "\n\n");
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">>");
            string read = Console.ReadLine();
            switch (read)
            {
                case "1":
                    Color.ChangeTextColor(Colors.BLUE, "\n", "전사", "로 전직하시겠습니까?(Y)\n\n");
                    Console.WriteLine("전사는 밸런스가 좋은 직업이며 체력이 높습니다\n");
                    Console.Write(">>");
                    string check = Console.ReadLine();
                    switch (check.ToLower())
                    {
                        case "y":
                            Player.player.job = (Job)1;
                            Player.player.baseAtk += 5;
                            Player.player.hp = Player.player.maxHp + 20;
                            Player.player.maxHp += 20;
                            Player.player.baseDef += 5;
                            Skill.instance.getSkill();
                            Console.Clear();
                            Color.ChangeTextColor(Colors.BLUE, "", "전사", "로 전직하셨습니다.\n");
                            Status();
                            break;
                        default:
                            Console.Clear();
                            Color.ChangeTextColor(Colors.RED, "", "취소하셨습니다", "\n");
                            FirstJob();
                            break;
                    }
                    break;
                case "2":
                    Color.ChangeTextColor(Colors.BLUE, "\n", "도적", "으로 전직하시겠습니까?(Y)\n\n");
                    Console.WriteLine("도적은 체력과 방어력은 낮으나 높은 공격력으로 메인 어택커입니다.\n");
                    Console.Write(">>");
                    check = Console.ReadLine();

                    switch (check.ToLower())
                    {
                        case "y":
                            Player.player.job = (Job)2;
                            Player.player.baseAtk += 10;
                            Player.player.hp = Player.player.maxHp + 10;
                            Player.player.maxHp += 10;
                            Player.player.mp = Player.player.maxMp + 20;
                            Player.player.maxMp += 20;
                            Skill.instance.getSkill();
                            Console.Clear();
                            Color.ChangeTextColor(Colors.BLUE, "", "도적", "으로 전직하셨습니다.\n");
                            Status();
                            break;
                        default:
                            Console.Clear();
                            Color.ChangeTextColor(Colors.RED, "", "취소하셨습니다", "\n");
                            FirstJob();
                            break;
                    }
                    break;
                case "0":
                    Console.Clear();
                    Status();
                    break;
                default:
                    Console.Clear();
                    Color.ChangeTextColor(Colors.RED, "", "잘못된 입력입니다.", "\n");
                    FirstJob();
                    break;
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
            checkJob();
            Color.ChangeTextColor(Colors.YELLOW, "", "2차 전직하기", "\n");
            Console.WriteLine("일정 레벨이 될 시 전직이 가능합니다.\n");
            Console.WriteLine($"Lv. {Player.player.level}  < {job} >\n");
            Board();


            Console.WriteLine("원하시는 직업을 선택해주세요(숫자만 입력)\n");
            Color.ChangeTextColor(Colors.RED, "", "0. 나가기", "\n\n");
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">>");
            string read = Console.ReadLine();
            switch (read)
            {
                case "1":
                    Color.ChangeTextColor(Colors.BLUE, "\n", $"{Sjob[0]}", "로 전직하시겠습니까?(Y)\n\n");
                    if (Sjob[0] == "버서커")
                        Console.WriteLine($"버서커는 체력을 소비하여 공격하고 공격 적중시 체력을 흡수하는 클래스 입니다.\n");
                    else
                        Console.WriteLine($"리퍼는 체방이 낮고 최고의 공격력을 가지고 있는 퓨어딜러 클래스입니다.\n");

                    Console.Write(">>");
                    string check = Console.ReadLine();
                    switch (check.ToLower())
                    {
                        case "y":
                            Player.player.job = (second[0]);
                            Skill.instance.getSkill();
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

                            Console.Clear();
                            Color.ChangeTextColor(Colors.BLUE, "", $"\n{Sjob[0]}", "로 전직하셨습니다.\n");
                            Status();
                            break;
                        default:
                            Console.Clear();
                            Color.ChangeTextColor(Colors.RED, "", "취소하셨습니다", "\n");
                            SecondJob();
                            break;
                    }
                    break;
                case "2":
                    Color.ChangeTextColor(Colors.BLUE, "\n", $"{Sjob[1]}", "로 전직하시겠습니까?(Y)\n\n");
                    if (Sjob[1] == "워로드")
                        Console.WriteLine($"워로드는 높은 체방을 가지고 있는 탱커 클래스 입니다.\n");
                    else
                        Console.WriteLine($"데모닉은 밸랜스형 변신 캐릭터 입니다.\n");

                    Console.Write(">>");
                    check = Console.ReadLine();
                    switch (check.ToLower())
                    {
                        case "y":
                            Player.player.job = (second[1]);
                            Skill.instance.getSkill();
                            if (Sjob[0] == "워로드")
                            {
                                Player.player.hp = Player.player.maxHp + 100;
                                Player.player.maxHp += 100;
                                Player.player.baseAtk += 2;
                                Player.player.baseDef += 10;
                            }
                            else
                            {
                                Player.player.baseAtk += 10;
                                Player.player.hp = Player.player.maxHp + 50;
                                Player.player.maxHp += 50;
                                Player.player.baseDef += 5;

                            }
                            Console.Clear();
                            Color.ChangeTextColor(Colors.BLUE, "", $"\n{Sjob[1]}", "로 전직하셨습니다.\n");
                            Status();
                            break;
                        default:
                            Console.Clear();
                            Color.ChangeTextColor(Colors.RED, "", "취소하셨습니다", "\n");
                            SecondJob();
                            break;
                    }
                    break;
                case "0":
                    Console.Clear();
                    Status();
                    break;
                default:
                    Console.Clear();
                    Color.ChangeTextColor(Colors.RED, "", "잘못된 입력입니다.", "\n");
                    SecondJob();
                    break;
            }
        }
    }
}
