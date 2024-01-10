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

        //���� Ȯ��
        static void checkJob()
        {
            string job = Player.player.job.ToString();

            if (job == ((Job)0).ToString())
            {
                instance.job = "�ʺ���";
            }
            else if ((job == ((Job)1).ToString()))
            {
                instance.job = "����";
            }
            else if ((job == ((Job)2).ToString()))
            {
                instance.job = "����";
            }
            else if ((job == ((Job)3).ToString()))
            {
                instance.job = "������";
            }
            else if ((job == ((Job)4).ToString()))
            {
                instance.job = "�ü�";
            }
        }

        // ����â
        public void Status()
        {
            Console.Clear();
            checkJob();
            Color.ChangeTextColor(Colors.YELLOW, "", "���� ����", "\n");
            Console.WriteLine("ĳ������ ������ ǥ�õ˴ϴ�.\n");
            Console.WriteLine($"{Player.player.name}\n");
            Console.WriteLine($"Lv. {Player.player.level}");
            Console.WriteLine($"chad < {job} >\n");
            Console.WriteLine($"���ݷ� : {Player.player.baseAtk} " + (Player.player.addAtk != 0 ? $"(+{Player.player.addAtk})" : ""));
            Console.WriteLine($"���ݷ� : {Player.player.baseDef} " + (Player.player.addDef != 0 ? $"(+{Player.player.addDef})" : ""));
            Console.WriteLine($"ü�� : {Player.player.maxHp} / {Player.player.hp}");
            Console.WriteLine($"���� : {Player.player.maxMp} / {Player.player.mp}");
            Console.WriteLine($"Gold : {Player.player.gold} G\n");
            
            if(Player.player.level >= 1 && Player.player.job.ToString() == ((Job)0).ToString())
            {
                Console.WriteLine("1. �����ϱ�");
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
                        FirstJob();
                        break;
                    case "0":
                        res = true;
                        Console.Clear();
                        Program.scene = Scene.mainScene;
                        GameManager.MainGameScene();
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
            Console.Write(">>");
            string read = Console.ReadLine();

            switch (read)
            {
                case "1":
                    Console.WriteLine("\n����� �����Ͻðڽ��ϱ�?(Y/N)");
                    Console.WriteLine("����� \n");
                    Console.Write(">>");
                    string check = Console.ReadLine();
                    switch (check.ToLower())
                    {
                        case "y":
                            Console.WriteLine("\n����� �����ϼ̽��ϴ�.");
                            Player.player.job = (Job)1;
                            Player.player.baseAtk = 5;
                            Player.player.maxHp = 100;
                            Player.player.hp = 100;
                            Player.player.baseDef = 5;

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
                    Console.WriteLine("\n�������� �����Ͻðڽ��ϱ�?(Y/N)");
                    Console.WriteLine("������ \n");
                    Console.Write(">>");
                    check = Console.ReadLine();

                    switch (check.ToLower())
                    {
                        case "y":
                            Console.WriteLine("\n�������� �����ϼ̽��ϴ�.");
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

            static void Board()
            {
                Console.WriteLine("     1��    |     2��    |  ��ÿ���  |  ��ÿ���  ");
                Console.WriteLine("  --------  |  --------  |  --------  |  --------  ");
                Console.WriteLine("    ����    |    ����    |   ������   |    �ü�    ");
                Console.WriteLine("  ________  |  ________  |  ________  |  ________  \n\n");
            }
        }
    }
}
