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

        //���� Ȯ��
        static void checkJob()
        {
            Job job = Player.player.job;

            if (job == ((Job)0))
            {
                instance.job = "�ʺ���";
            }
            else if ((job == ((Job)1)))
            {
                instance.job = "����";
            }
            else if ((job == ((Job)2)))
            {
                instance.job = "����";
            }
            else if ((job == ((Job)3)))
            {
                instance.job = "������";
            }
            else if ((job == ((Job)4)))
            {
                instance.job = "�ü�";
            }
            else if ((job == ((Job)5)))
            {
                instance.job = "����Ŀ";
            }
            else if ((job == ((Job)6)))
            {
                instance.job = "���ε�";
            }
            else if ((job == ((Job)7)))
            {
                instance.job = "����";
            }
            else if ((job == ((Job)8)))
            {
                instance.job = "�����";
            }
        }

        // ����â
        public void Status()
        {
            checkJob();
            Color.ChangeTextColor(Colors.YELLOW, "", "���� ����", "\n\n");
            Color.ChangeTextColor(Colors.MAGENTA, "���谡 ", Player.player.name, "\n\n");
            Console.WriteLine($"Lv. {Player.player.level}");
            Console.WriteLine($"chad < {job} >\n");
            Console.WriteLine($"���ݷ� : {Player.player.baseAtk} " + (Player.player.addAtk != 0 ? $"(+{Player.player.addAtk})" : ""));
            Console.WriteLine($"���� : {Player.player.baseDef} " + (Player.player.addDef != 0 ? $"(+{Player.player.addDef})" : ""));
            Console.WriteLine($"ü�� : {Player.player.maxHp} / {Player.player.hp}");
            Console.WriteLine($"���� : {Player.player.maxMp} / {Player.player.mp}");
            Console.WriteLine($"Gold : {Player.player.gold} G\n");
            Console.WriteLine($"����ġ : {Player.player.levelExp - Player.player.needExp} / {Player.player.levelExp} ({(Player.player.needExp == 0 ? "0" : Convert.ToInt32((Player.player.levelExp - Player.player.needExp) * 100.00 / Player.player.levelExp))}%)\n");

            Console.WriteLine($"���� ��ų {Skills.myskills.Count}��\n");
            foreach (var skill in Skills.myskills)
            {
                Color.ChangeTextColor(Colors.BLUE, "",$"{ skill.name} ", $"{skill.job} ���뽺ų\n");
                Console.Write($"������ ���ط� : {Math.Truncate(skill.damage * (Player.player.baseAtk + Player.player.addAtk))}   ");
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

            string wname = "������";
            string aname = "������";
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

            Console.WriteLine($"���� : {wname}");
            Console.WriteLine($"�� : {aname} \n");


            Console.WriteLine("9. �κ��丮");

            if (Player.player.level >= 1 && Player.player.job.ToString() == ((Job)0).ToString())
            {
                Color.ChangeTextColor(Colors.BLUE, "", "1. 1�� �����ϱ�", "\n");
            }
            else if (Player.player.level >= 3 && (Player.player.job.ToString() == ((Job)1).ToString() || Player.player.job.ToString() == ((Job)2).ToString()))
            {
                Color.ChangeTextColor(Colors.BLUE, "", "1. 2�� �����ϱ�", "\n");
            }

            Color.ChangeTextColor(Colors.RED, "", "0. ������", "\n\n");
            Console.WriteLine("���Ͻô� �ൿ�� �Է����ּ���.");
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
                        Color.ChangeTextColor(Colors.RED, "", "�߸��� �Է��Դϴ�.", "\n");
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
                    Color.ChangeTextColor(Colors.RED, "", "�߸��� �Է��Դϴ�.", "\n");
                    Status();
                    break;
            }
        }

        // 1�� ���� �ϱ�
        public void FirstJob()
        {
            checkJob();
            Color.ChangeTextColor(Colors.YELLOW, "", "�����ϱ�", "\n");
            Console.WriteLine("���� ������ �� �� ������ �����մϴ�.\n");
            Console.WriteLine($"Lv. {Player.player.level}  < {job} >\n");
            Board();


            Console.WriteLine("���Ͻô� ������ �������ּ���(���ڸ� �Է�)\n");
            Color.ChangeTextColor(Colors.RED, "", "0. ������", "\n\n");
            Console.WriteLine("���Ͻô� �ൿ�� �Է����ּ���.");
            Console.Write(">>");
            string read = Console.ReadLine();
            switch (read)
            {
                case "1":
                    Color.ChangeTextColor(Colors.BLUE, "\n", "����", "�� �����Ͻðڽ��ϱ�?(Y)\n\n");
                    Console.WriteLine("����� �뷱���� ���� �����̸� ü���� �����ϴ�\n");
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
                            Color.ChangeTextColor(Colors.BLUE, "", "����", "�� �����ϼ̽��ϴ�.\n");
                            Status();
                            break;
                        default:
                            Console.Clear();
                            Color.ChangeTextColor(Colors.RED, "", "����ϼ̽��ϴ�", "\n");
                            FirstJob();
                            break;
                    }
                    break;
                case "2":
                    Color.ChangeTextColor(Colors.BLUE, "\n", "����", "���� �����Ͻðڽ��ϱ�?(Y)\n\n");
                    Console.WriteLine("������ ü�°� ������ ������ ���� ���ݷ����� ���� ����Ŀ�Դϴ�.\n");
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
                            Color.ChangeTextColor(Colors.BLUE, "", "����", "���� �����ϼ̽��ϴ�.\n");
                            Status();
                            break;
                        default:
                            Console.Clear();
                            Color.ChangeTextColor(Colors.RED, "", "����ϼ̽��ϴ�", "\n");
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
                    Color.ChangeTextColor(Colors.RED, "", "�߸��� �Է��Դϴ�.", "\n");
                    FirstJob();
                    break;
            }

        }
        static Job[] second = { Job.Berserker, Job.Warlord };
        static string[] Sjob = { "����Ŀ", "���ε�" };
        static void Board()
        {
            if (Player.player.job.ToString() == ((Job)0).ToString())
            {
                Console.WriteLine("     1��    |     2��    |  ��ÿ���  |  ��ÿ���  ");
                Console.WriteLine("  --------  |  --------  |  --------  |  --------  \n");
                Console.WriteLine("    ����    |    ����    |   ������   |    �ü�    ");
                Console.WriteLine("  ________  |  ________  |  ________  |  ________  \n\n");
            }
            else if (Player.player.job.ToString() == ((Job)1).ToString())
            {
                second[0] = Job.Berserker;
                second[1] = Job.Warlord;
                Sjob[0] = "����Ŀ";
                Sjob[1] = "���ε�";

                Console.WriteLine("     1��    |     2��       ");
                Console.WriteLine("  --------  |  --------   \n");
                Console.WriteLine("   ����Ŀ   |   ���ε�      ");
                Console.WriteLine("  ________  |  ________ \n\n");

            }
            else if (Player.player.job.ToString() == ((Job)2).ToString())
            {
                second[0] = Job.Reaper;
                second[1] = Job.Demonic;
                Sjob[0] = "����";
                Sjob[1] = "�����";

                Console.WriteLine("     1��    |     2��       ");
                Console.WriteLine("  --------  |  --------   \n");
                Console.WriteLine("    ����    |   �����      ");
                Console.WriteLine("  ________  |  ________ \n\n");

            }
        }
        public void SecondJob()
        {
            checkJob();
            Color.ChangeTextColor(Colors.YELLOW, "", "2�� �����ϱ�", "\n");
            Console.WriteLine("���� ������ �� �� ������ �����մϴ�.\n");
            Console.WriteLine($"Lv. {Player.player.level}  < {job} >\n");
            Board();


            Console.WriteLine("���Ͻô� ������ �������ּ���(���ڸ� �Է�)\n");
            Color.ChangeTextColor(Colors.RED, "", "0. ������", "\n\n");
            Console.WriteLine("���Ͻô� �ൿ�� �Է����ּ���.");
            Console.Write(">>");
            string read = Console.ReadLine();
            switch (read)
            {
                case "1":
                    Color.ChangeTextColor(Colors.BLUE, "\n", $"{Sjob[0]}", "�� �����Ͻðڽ��ϱ�?(Y)\n\n");
                    if (Sjob[0] == "����Ŀ")
                        Console.WriteLine($"����Ŀ�� ü���� �Һ��Ͽ� �����ϰ� ���� ���߽� ü���� ����ϴ� Ŭ���� �Դϴ�.\n");
                    else
                        Console.WriteLine($"���۴� ü���� ���� �ְ��� ���ݷ��� ������ �ִ� ǻ����� Ŭ�����Դϴ�.\n");

                    Console.Write(">>");
                    string check = Console.ReadLine();
                    switch (check.ToLower())
                    {
                        case "y":
                            Player.player.job = (second[0]);
                            Skill.instance.getSkill();
                            if (Sjob[0] == "����Ŀ")
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
                            Color.ChangeTextColor(Colors.BLUE, "", $"\n{Sjob[0]}", "�� �����ϼ̽��ϴ�.\n");
                            Status();
                            break;
                        default:
                            Console.Clear();
                            Color.ChangeTextColor(Colors.RED, "", "����ϼ̽��ϴ�", "\n");
                            SecondJob();
                            break;
                    }
                    break;
                case "2":
                    Color.ChangeTextColor(Colors.BLUE, "\n", $"{Sjob[1]}", "�� �����Ͻðڽ��ϱ�?(Y)\n\n");
                    if (Sjob[1] == "���ε�")
                        Console.WriteLine($"���ε�� ���� ü���� ������ �ִ� ��Ŀ Ŭ���� �Դϴ�.\n");
                    else
                        Console.WriteLine($"������� �뷣���� ���� ĳ���� �Դϴ�.\n");

                    Console.Write(">>");
                    check = Console.ReadLine();
                    switch (check.ToLower())
                    {
                        case "y":
                            Player.player.job = (second[1]);
                            Skill.instance.getSkill();
                            if (Sjob[0] == "���ε�")
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
                            Color.ChangeTextColor(Colors.BLUE, "", $"\n{Sjob[1]}", "�� �����ϼ̽��ϴ�.\n");
                            Status();
                            break;
                        default:
                            Console.Clear();
                            Color.ChangeTextColor(Colors.RED, "", "����ϼ̽��ϴ�", "\n");
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
                    Color.ChangeTextColor(Colors.RED, "", "�߸��� �Է��Դϴ�.", "\n");
                    SecondJob();
                    break;
            }
        }
    }
}
