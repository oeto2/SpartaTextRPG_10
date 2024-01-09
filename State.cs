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
                job = "�ʺ���";
            }
            else if ((job == ((Job)1).ToString()))
            {
                job = "����";
            }
            else if ((job == ((Job)2).ToString()))
            {
                job = "����";
            }
            else if ((job == ((Job)3).ToString()))
            {
                job = "������";
            }
            else if ((job == ((Job)4).ToString()))
            {
                job = "�ü�";
            }
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("���� ����");
            Console.ResetColor();
            Console.WriteLine("ĳ������ ������ ǥ�õ˴ϴ�.\n");
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
            Console.Write(">>");
            string read = Console.ReadLine();
            switch (read)
            {
                case "1":
                    FirstJob();
                    break;
                case "0":
                    Console.WriteLine("Ȩ����...");
                    break;
                default:
                    Console.WriteLine("\n�߸��� �Է��Դϴ�.\n");
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
                job = "�ʺ���";
            }
            else if ((job == ((Job)1).ToString()))
            {
                job = "����";
            }
            else if ((job == ((Job)2).ToString()))
            {
                job = "����";
            }
            else if ((job == ((Job)3).ToString()))
            {
                job = "������";
            }
            else if ((job == ((Job)4).ToString()))
            {
                job = "�ü�";
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("���� �ϱ�");
            Console.ResetColor();

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
                            Player.player.job = (Job)1;
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
                    Console.WriteLine("�߸��� ���Դϴ�.");
                    FirstJob();
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
