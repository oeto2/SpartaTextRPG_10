using System;
using System.Collections.Generic;
using System.Numerics;
using SpartaTextRPG;
using SpartaTextRPG.DataClass;

namespace SpartaTextRPG
{
    enum DungeonScene
    {
        DungeonEntrance,
        DungeonChoice

    }
    public class Dungeon
    {
        public static Dungeon instance = new Dungeon();
        public int DungeonLevel { get; set; }
        public int MonsterMinCount { get; set; }
        public int MonsterMaxCount { get; set; }
        public int MonsterCount { get; set; }

        DungeonScene scene = DungeonScene.DungeonEntrance;

        public void DungeonEntrance()
        {
            if (scene == DungeonScene.DungeonEntrance)
            {
                Console.WriteLine("==========================================================");
                Color.ChangeTextColor(Colors.YELLOW, "", "던  전  입  구\n");
                Console.WriteLine("아스키 아트 삽입 예정 (던전 입구 이미지)");
                Console.WriteLine("==========================================================");
                Color.ChangeTextColor(Colors.MAGENTA, "모험가 ", Player.player.name, "\n\n");
                Console.WriteLine($"Lv. {Player.player.level}");
                //Console.WriteLine($"chad < {Player.job} >\n");
                Console.WriteLine($"공격력 : {Player.player.baseAtk + Player.player.addAtk} " + (Player.player.addAtk != 0 ? $"(+{Player.player.addAtk})" : ""));
                Console.WriteLine($"방어력 : {Player.player.baseDef + Player.player.addDef} " + (Player.player.addDef != 0 ? $"(+{Player.player.addDef})" : ""));
                Console.WriteLine($"체력 : {Player.player.maxHp} / {Player.player.hp}");
                Console.WriteLine($"마력 : {Player.player.maxMp} / {Player.player.mp}");
                Console.WriteLine($"Gold : {Player.player.gold} G\n");
                Console.WriteLine("==========================================================");
                Console.WriteLine("1. 던전 입장");
                Color.ChangeTextColor(Colors.RED, "", "0", ". 나가기\n");
                Console.WriteLine("메뉴를 선택하세요:");
                Console.Write(">>");

                string userInput = Console.ReadLine();
                Console.Clear();
                if (userInput == "1")
                {
                    if (Player.player.hp >= 20)
                    {
                        scene = DungeonScene.DungeonChoice;
                    }
                    else if (Player.player.hp < 20)
                    {
                        // 던전 입장 조건을 충족하지 못한 경우
                        Color.ChangeTextColor(Colors.RED, "", "던전에 입장할 수 없습니다. 체력이 부족합니다.", "\n");
                        Console.ReadLine();
                        Console.Clear();
                    }
                    else
                    {
                        Console.Clear();
                        Color.ChangeTextColor(Colors.RED, "", "잘못된 입력입니다.", "\n");
                    }
                }
                else if (userInput == "0")
                {
                    Console.Clear();
                    Program.scene = Scene.mainScene;
                }
                else
                {
                    Console.Clear();
                    Color.ChangeTextColor(Colors.RED, "", "잘못된 입력입니다.", "\n");
                }
            }
            else
            {
                DungeonChoice();
            }
        }
        public void DungeonChoice()
        {
            int stage1RecommendedAttack = 10;
            int stage2RecommendedAttack = 20;
            int stage3RecommendedAttack = 30;
            int stage4RecommendedAttack = 40;
            int stage5RecommendedAttack = 50;

            Console.WriteLine("==========================================================");
            Color.ChangeTextColor(Colors.YELLOW, "", "던  전  입  구\n");
            Console.WriteLine("아스키 아트 삽입 예정 (던전 입구 이미지)");
            Console.WriteLine("==========================================================");
            Color.ChangeTextColor(Colors.MAGENTA, "모험가 ", Player.player.name, "\n\n");
            Console.WriteLine($"Lv. {Player.player.level}");
            //Console.WriteLine($"chad < {Player.job} >\n");
            Console.WriteLine($"공격력 : {Player.player.baseAtk + Player.player.addAtk} " + (Player.player.addAtk != 0 ? $"(+{Player.player.addAtk})" : ""));
            Console.WriteLine($"방어력 : {Player.player.baseAtk + Player.player.addAtk} " + (Player.player.addDef != 0 ? $"(+{Player.player.addDef})" : ""));
            Console.WriteLine($"체력 : {Player.player.maxHp} / {Player.player.hp}");
            Console.WriteLine($"마력 : {Player.player.maxMp} / {Player.player.mp}");
            Console.WriteLine($"Gold : {Player.player.gold} G\n");
            Console.WriteLine("==========================================================");
            Console.WriteLine("[  단   계  ]");
            Console.WriteLine("Stage.1 - 모험의 시작");
            if (Player.player.baseAtk + Player.player.addAtk >= stage1RecommendedAttack)
            {
                Color.ChangeTextColor(Colors.GREEN, "", $"   적정 공격력 : {stage1RecommendedAttack}\n");
            }
            else
            {
                Color.ChangeTextColor(Colors.RED, "", $"   적정 공격력 : {stage1RecommendedAttack}\n");
            }
            Console.WriteLine("Stage.2 - 용의 둥지");
            if (Player.player.baseAtk + Player.player.addAtk >= stage2RecommendedAttack)
            {
                Color.ChangeTextColor(Colors.GREEN, "", $"   적정 공격력 : {stage2RecommendedAttack}\n");
            }
            else
            {
                Color.ChangeTextColor(Colors.RED, "", $"   적정 공격력 : {stage2RecommendedAttack}\n");
            }
            Console.WriteLine("Stage.3 - 불의 둥지");
            if (Player.player.baseAtk + Player.player.addAtk >= stage3RecommendedAttack)
            {
                Color.ChangeTextColor(Colors.GREEN, "", $"   적정 공격력 : {stage3RecommendedAttack}\n");
            }
            else
            {
                Color.ChangeTextColor(Colors.RED, "", $"   적정 공격력 : {stage3RecommendedAttack}\n");
            }
            Console.WriteLine("Stage.4 - 문영오의 둥지");
            if (Player.player.baseAtk + Player.player.addAtk >= stage4RecommendedAttack)
            {
                Color.ChangeTextColor(Colors.GREEN, "", $"   적정 공격력 : {stage4RecommendedAttack}\n");
            }
            else
            {
                Color.ChangeTextColor(Colors.RED, "", $"   적정 공격력 : {stage4RecommendedAttack}\n");
            }
            Console.WriteLine("Stage.5 - 한효승의 안식처");
            if (Player.player.baseAtk + Player.player.addAtk >= stage5RecommendedAttack)
            {
                Color.ChangeTextColor(Colors.GREEN, "", $"   적정 공격력 : {stage5RecommendedAttack} \n");
            }
            else
            {
                Color.ChangeTextColor(Colors.RED, "", $"   적정 공격력 : {stage5RecommendedAttack} \n");
            }
            Color.ChangeTextColor(Colors.RED, "", "0", ". 나가기\n");
            Console.WriteLine("");
            Console.WriteLine("입장하고 싶은 던전을 선택해주세요. :");
            Console.Write(">>");

            string selectedStage = Console.ReadLine();
            switch (selectedStage)
            {
                case "1":
                    if (Player.player.baseAtk >= stage1RecommendedAttack)
                    {
                        Console.WriteLine("던전에 입장하겠습니다. 건투를 빕니다!");
                        Console.ReadLine();
                        Console.Clear();
                        Battle.instance.Stage1();
                    }
                    else
                    {
                        Console.WriteLine($"{Player.player.name} 님의 공격력이 해당 던전의 적정 공격력보다 낮습니다. 그래도 입장 하시겠습니까?");
                        Console.WriteLine("1. 예");
                        Console.WriteLine("0. 이전 화면으로 돌아가기");
                        Console.Write(">>");

                        string stage = Console.ReadLine();

                        if (stage == "1")
                        {
                            Console.WriteLine("던전에 입장하겠습니다. 건투를 빕니다!");
                            Battle.instance.Stage1();
                        }
                        else if (stage == "0")
                        {
                            Console.Clear();
                        }
                        else
                        {
                            Console.Clear();
                            Color.ChangeTextColor(Colors.RED, "", "잘못된 입력입니다.", "\n");
                        }
                    }
                    break;

                case "2":
                    if (Player.player.baseAtk + Player.player.addAtk >= stage2RecommendedAttack)
                    {
                        Console.WriteLine("던전에 입장하겠습니다. 건투를 빕니다!");
                        Console.ReadLine();
                        Console.Clear();
                        Battle.instance.Stage2();
                    }
                    else
                    {
                        Console.WriteLine($"{Player.player.name} 님의 공격력이 해당 던전의 적정 공격력보다 낮습니다. 그래도 입장 하시겠습니까?");
                        Console.WriteLine("1. 예");
                        Console.WriteLine("0. 이전 화면으로 돌아가기");
                        Console.Write(">>");

                        string stage = Console.ReadLine();

                        if (stage == "1")
                        {
                            Console.WriteLine("던전에 입장하겠습니다. 건투를 빕니다!");
                            Battle.instance.Stage2();
                        }
                        else if (stage == "0")
                        {
                            Console.Clear();
                        }
                        else
                        {
                            Console.Clear();
                            Color.ChangeTextColor(Colors.RED, "", "잘못된 입력입니다.", "\n");
                        }
                    }
                    break;

                case "3":
                    if (Player.player.baseAtk + Player.player.addAtk >= stage3RecommendedAttack)
                    {
                        Console.WriteLine("던전에 입장하겠습니다. 건투를 빕니다!");
                        Console.ReadLine();
                        Console.Clear();
                        Battle.instance.Stage3();
                    }
                    else
                    {
                        Console.WriteLine($"{Player.player.name} 님의 공격력이 해당 던전의 적정 공격력보다 낮습니다. 그래도 입장 하시겠습니까?");
                        Console.WriteLine("1. 예");
                        Console.WriteLine("0. 이전 화면으로 돌아가기");
                        Console.Write(">>");

                        string stage = Console.ReadLine();

                        if (stage == "1")
                        {
                            Console.WriteLine("던전에 입장하겠습니다. 건투를 빕니다!");
                            Battle.instance.Stage3();
                        }
                        else if (stage == "0")
                        {
                            Console.Clear();
                        }
                        else
                        {
                            Console.Clear();
                            Color.ChangeTextColor(Colors.RED, "", "잘못된 입력입니다.", "\n");
                        }
                    }
                    break;

                case "4":
                    if (Player.player.baseAtk + Player.player.addAtk >= stage4RecommendedAttack)
                    {
                        Console.WriteLine("던전에 입장하겠습니다. 건투를 빕니다!");
                        Console.ReadLine();
                        Console.Clear();
                        Battle.instance.Stage4();
                    }
                    else
                    {
                        Console.WriteLine($"{Player.player.name} 님의 공격력이 해당 던전의 적정 공격력보다 낮습니다. 그래도 입장 하시겠습니까?");
                        Console.WriteLine("1. 예");
                        Console.WriteLine("0. 이전 화면으로 돌아가기");
                        Console.Write(">>");

                        string stage = Console.ReadLine();

                        if (stage == "1")
                        {
                            Console.WriteLine("던전에 입장하겠습니다. 건투를 빕니다!");
                            Battle.instance.Stage4();
                        }
                        else if (stage == "0")
                        {
                            Console.Clear();
                        }
                        else
                        {
                            Console.Clear();
                            Color.ChangeTextColor(Colors.RED, "", "잘못된 입력입니다.", "\n");
                        }
                    }
                    break;

                case "5":
                    if (Player.player.baseAtk + Player.player.addAtk >= stage5RecommendedAttack)
                    {
                        Console.WriteLine("던전에 입장하겠습니다. 건투를 빕니다!");
                        Console.ReadLine();
                        Console.Clear();
                        Battle.instance.Stage5();
                    }
                    else
                    {
                        Console.WriteLine($"{Player.player.name} 님의 공격력이 해당 던전의 적정 공격력보다 낮습니다. 그래도 입장 하시겠습니까?");
                        Console.WriteLine("1. 예");
                        Console.WriteLine("0. 이전 화면으로 돌아가기");
                        Console.Write(">>");

                        string stage = Console.ReadLine();

                        if (stage == "1")
                        {
                            Console.WriteLine("던전에 입장하겠습니다. 건투를 빕니다!");
                            Battle.instance.Stage5();
                        }
                        else if (stage == "0")
                        {
                            Console.Clear();
                        }
                        else
                        {
                            Console.Clear();
                            Color.ChangeTextColor(Colors.RED, "", "잘못된 입력입니다.", "\n");
                        }
                    }
                    break;
                case "0":
                    Console.Clear();
                    scene = DungeonScene.DungeonEntrance;
                    break;
                default:
                    Console.Clear();
                    Color.ChangeTextColor(Colors.RED, "", "잘못된 입력입니다.", "\n");
                    break;
            }
        }
    }

    internal class Game
    {
        public struct CurrentStageReward
        {
            public bool isClear;
            public int stageNumber;
            public List<Item> items;
            public int exp;
            public int gold;
        }

        public static CurrentStageReward currentStageReward = new CurrentStageReward();
    }
}