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
            Console.Clear();
            checkJob();
            Color.ChangeTextColor(Colors.YELLOW, "", "���� ����", "\n");
            Console.WriteLine("ĳ������ ������ ǥ�õ˴ϴ�.\n");
            Color.ChangeTextColor(Colors.BLUE, "", Player.player.name, " ȯ���մϴ�!\n");
            Console.WriteLine($"Lv. {Player.player.level}");
            Console.WriteLine($"chad < {job} >\n");
            Console.WriteLine($"���ݷ� : {Player.player.baseAtk} " + (Player.player.addAtk != 0 ? $"(+{Player.player.addAtk})" : ""));
            Console.WriteLine($"���� : {Player.player.baseDef} " + (Player.player.addDef != 0 ? $"(+{Player.player.addDef})" : ""));
            Console.WriteLine($"ü�� : {Player.player.maxHp} / {Player.player.hp}");
            Console.WriteLine($"���� : {Player.player.maxMp} / {Player.player.mp}");
            Console.WriteLine($"Gold : {Player.player.gold} G\n");

            if (Player.player.level >= 1 && Player.player.job.ToString() == ((Job)0).ToString())
            {
                Console.WriteLine("1. 1�� �����ϱ�");
            }
            else if (Player.player.level >= 1 && Player.player.job.ToString() != ((Job)0).ToString())
            {
                Console.WriteLine("1. 2�� �����ϱ�");
            }

            Console.WriteLine("0. ������\n");
            Console.WriteLine("���Ͻô� �ൿ�� �Է����ּ���.");
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
                        Console.WriteLine("\n�߸��� ���Դϴ�.");
                        break;
                }
            }
        }

        // 1�� ���� �ϱ�
        public void FirstJob()
        {
            Console.Clear();
            checkJob();
            Color.ChangeTextColor(Colors.YELLOW, "", "�����ϱ�", "\n");
            Console.WriteLine("���� ������ �� �� ������ �����մϴ�.\n");
            Console.WriteLine($"Lv. {Player.player.level}  < {job} >\n");
            Board();


            Console.WriteLine("���Ͻô� ������ �������ּ���(���ڸ� �Է�)");
            Console.WriteLine("0. ������\n");
            Console.WriteLine("���Ͻô� �ൿ�� �Է����ּ���.");
            bool res = false;
            while (!res)
            {
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
                                Console.WriteLine("\n����� �����ϼ̽��ϴ�.");
                                Player.player.job = (Job)1;
                                Player.player.baseAtk += 5;
                                Player.player.hp = Player.player.maxHp + 50;
                                Player.player.maxHp += 50;
                                Player.player.baseDef += 5;

                                Thread.Sleep(1000);
                                Status();
                                break;
                            default:
                                Console.WriteLine("����ϼ̽��ϴ�");
                                Thread.Sleep(500);
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
                                Console.WriteLine("\n�������� �����ϼ̽��ϴ�.");
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
                                Console.WriteLine("����ϼ̽��ϴ�");
                                Thread.Sleep(500);
                                FirstJob();
                                break;
                        }
                        break;
                    case "0":
                        Status();
                        break;
                    default:
                        Console.WriteLine("\n�߸��� ���Դϴ�.");
                        break;
                }
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
            Console.WriteLine("0. ������\n");
            Console.WriteLine("���Ͻô� �ൿ�� �Է����ּ���.");
            bool res = false;
            while (!res)
            {
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
                                Player.player.job = (second[0]);

                                Thread.Sleep(1000);
                                Status();
                                break;
                            default:
                                Console.WriteLine("����ϼ̽��ϴ�");
                                Thread.Sleep(500);
                                SecondJob();
                                break;
                        }
                        break;
                    case "2":
                        Console.WriteLine($"\n{Sjob[1]}�� �����Ͻðڽ��ϱ�?(Y)");
                        if (Sjob[0] == "���ε�")
                            Console.WriteLine($"���ε�� ���� ü���� ������ �ִ� ��Ŀ Ŭ���� �Դϴ�.\n");
                        else
                            Console.WriteLine($"������� �뷣���� ���� ĳ���� �Դϴ�.\n");

                        Console.Write(">>");
                        check = Console.ReadLine();
                        switch (check.ToLower())
                        {
                            case "y":
                                Console.WriteLine($"\n{Sjob[1]}�� �����ϼ̽��ϴ�.");
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
                                Player.player.job = (second[1]);
                                Status();
                                break;
                            default:
                                Console.WriteLine("����ϼ̽��ϴ�");
                                Thread.Sleep(500);
                                SecondJob();
                                break;
                        }
                        break;
                    case "0":
                        Status();
                        break;
                    default:
                        Console.WriteLine("\n�߸��� ���Դϴ�.");
                        break;
                }
            }
        }
    }
}
