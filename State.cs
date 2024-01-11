using SpartaTextRPG;
using SpartaTextRPG.DataClass;
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
            Color.ChangeTextColor(Colors.YELLOW, "", "���� ����", "\n");
            Console.WriteLine("ĳ������ ������ ǥ�õ˴ϴ�.\n");
            Color.ChangeTextColor(Colors.MAGENTA, "���谡 ", Player.player.name, "\n\n");
            Console.WriteLine($"Lv. {Player.player.level}");
            Console.WriteLine($"chad < {job} >\n");
            Console.WriteLine($"���ݷ� : {Player.player.baseAtk} " + (Player.player.addAtk != 0 ? $"(+{Player.player.addAtk})" : ""));
            Console.WriteLine($"���� : {Player.player.baseDef} " + (Player.player.addDef != 0 ? $"(+{Player.player.addDef})" : ""));
            Console.WriteLine($"ü�� : {Player.player.maxHp} / {Player.player.hp}");
            Console.WriteLine($"���� : {Player.player.maxMp} / {Player.player.mp}");
            Console.WriteLine($"Gold : {Player.player.gold} G\n");

           
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

            if (Player.player.level >= 1 && Player.player.job.ToString() == ((Job)0).ToString())
            {
                Console.WriteLine("1. 1�� �����ϱ�");
            }
            else if (Player.player.level >= 1 && (Player.player.job.ToString() == ((Job)1).ToString() || Player.player.job.ToString() == ((Job)2).ToString()))
            {
                Console.WriteLine("1. 2�� �����ϱ�");
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
                    else if (Player.player.job != ((Job)0))
                    {
                        Console.Clear();
                        SecondJob();
                    }
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


            Console.WriteLine("���Ͻô� ������ �������ּ���(���ڸ� �Է�)");
            Color.ChangeTextColor(Colors.RED, "", "0. ������", "\n\n");
            Console.WriteLine("���Ͻô� �ൿ�� �Է����ּ���.");
            Console.Write(">>");
            string read = Console.ReadLine();
            switch (read)
            {
                case "1":
                    Console.WriteLine("\n����� �����Ͻðڽ��ϱ�?(Y)");
                    Console.WriteLine("����� �뷱���� ���� �����̸� ü���� �����ϴ�\n");
                    Console.Write(">>");
                    string check = Console.ReadLine();
                    switch (check.ToLower())
                    {
                        case "y":
                            Player.player.job = (Job)1;
                            Player.player.baseAtk += 5;
                            Player.player.hp = Player.player.maxHp + 50;
                            Player.player.maxHp += 50;
                            Player.player.baseDef += 5;
                            Skill.instance.getSkill();
                            Console.Clear();
                            Color.ChangeTextColor(Colors.YELLOW, "", "�������� �����ϼ̽��ϴ�.", "\n");
                            Status();
                            break;
                        default:
                            Console.Clear();
                            Color.ChangeTextColor(Colors.RED, "", "�߸��� �Է��Դϴ�.", "\n");
                            FirstJob();
                            break;
                    }
                    break;
                case "2":
                    Console.WriteLine("\n�������� �����Ͻðڽ��ϱ�?(Y)");
                    Console.WriteLine("������ ü�°� ������ ������ ���� ���ݷ����� ���� ����Ŀ�Դϴ�.\n");
                    Console.Write(">>");
                    check = Console.ReadLine();

                    switch (check.ToLower())
                    {
                        case "y":
                            Player.player.job = (Job)2;
                            Player.player.baseAtk += 10;
                            Player.player.hp = Player.player.maxHp + 20;
                            Player.player.maxHp += 20;
                            Player.player.mp = Player.player.maxMp + 30;
                            Player.player.maxMp += 30;
                            Skill.instance.getSkill();
                            Console.Clear();
                            Color.ChangeTextColor(Colors.YELLOW, "", "�������� �����ϼ̽��ϴ�.", "\n");
                            Status();
                            break;
                        default:
                            Console.Clear();
                            Color.ChangeTextColor(Colors.RED, "", "�߸��� �Է��Դϴ�.", "\n");
                            FirstJob();
                            break;
                    }
                    break;
                case "0":
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
            Console.Clear();
            checkJob();
            Color.ChangeTextColor(Colors.YELLOW, "", "2�� �����ϱ�", "\n");
            Console.WriteLine("���� ������ �� �� ������ �����մϴ�.\n");
            Console.WriteLine($"Lv. {Player.player.level}  < {job} >\n");
            Board();


            Console.WriteLine("���Ͻô� ������ �������ּ���(���ڸ� �Է�)");
            Color.ChangeTextColor(Colors.RED, "", "0. ������", "\n\n");
            Console.WriteLine("���Ͻô� �ൿ�� �Է����ּ���.");
            Console.Write(">>");
            string read = Console.ReadLine();
            switch (read)
            {
                case "1":
                    Console.WriteLine($"\n{Sjob[0]}�� �����Ͻðڽ��ϱ�?(Y)");
                    if (Sjob[0] == "����Ŀ")
                        Console.WriteLine($"����Ŀ�� ü���� �Һ��Ͽ� �����ϰ� ���� ���߽� ü���� ����ϴ� Ŭ���� �Դϴ�.\n");
                    else
                        Console.WriteLine($"���۴� ü���� ���� �ְ��� ���ݷ��� ������ �ִ� ǻ����� Ŭ�����Դϴ�.\n");

                    Console.Write(">>");
                    string check = Console.ReadLine();
                    switch (check.ToLower())
                    {
                        case "y":
                            Console.WriteLine($"\n{Sjob[0]}�� �����ϼ̽��ϴ�.");
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

                            Thread.Sleep(1000);
                            Status();
                            break;
                        default:
                            Console.Clear();
                            Color.ChangeTextColor(Colors.RED, "", "�߸��� �Է��Դϴ�.", "\n");
                            SecondJob();
                            break;
                    }
                    break;
                case "2":
                    Console.WriteLine($"\n{Sjob[1]}�� �����Ͻðڽ��ϱ�?(Y)");
                    if (Sjob[1] == "���ε�")
                        Console.WriteLine($"���ε�� ���� ü���� ������ �ִ� ��Ŀ Ŭ���� �Դϴ�.\n");
                    else
                        Console.WriteLine($"������� �뷣���� ���� ĳ���� �Դϴ�.\n");

                    Console.Write(">>");
                    check = Console.ReadLine();
                    switch (check.ToLower())
                    {
                        case "y":
                            Console.WriteLine($"\n{Sjob[1]}�� �����ϼ̽��ϴ�.");
                            Player.player.job = (second[1]);
                            Skill.instance.getSkill();
                            if (Sjob[0] == "���ε�")
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
