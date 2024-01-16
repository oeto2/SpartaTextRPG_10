using System;
using System.Collections.Generic;
using System.Numerics;
using System.Reflection;
using SpartaTextRPG;
using SpartaTextRPG.DataClass;
using SpartaTextRPG.DataClass.Quest;

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

        public List<DungeonEnter> dungeonList = new List<DungeonEnter>()
        {
            new DungeonEnter(10, "모험의 시작"),
            new DungeonEnter(20, "용의 둥지"),
            new DungeonEnter(30, "불의 둥지"),
            new DungeonEnter(40, "의 둥지"),
            new DungeonEnter(50, "스파르타 매니저들의 안식처")
        };

        public void DungeonEntrance()
        {
            if (scene == DungeonScene.DungeonEntrance)
            {
                Console.Clear();
                Console.WriteLine("==========================================================");
                Color.ChangeTextColor(Colors.YELLOW, "", "던  전  입  구\n");
                Console.WriteLine("==========================================================");
                Color.ChangeTextColor(Colors.MAGENTA, "모험가 ", Player.player.name, "\n\n");
                Console.WriteLine($"Lv. {Player.player.level}");
                Console.WriteLine($"chad < {Player.player.job} >\n");
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
            Console.WriteLine("==========================================================");
            Color.ChangeTextColor(Colors.YELLOW, "", "던  전  입  구\n");
            Console.WriteLine("==========================================================");
            Color.ChangeTextColor(Colors.MAGENTA, "모험가 ", Player.player.name, "\n\n");
            Console.WriteLine($"Lv. {Player.player.level}");
            Console.WriteLine($"chad < {Player.player.job} >\n");
            Console.WriteLine($"공격력 : {Player.player.baseAtk + Player.player.addAtk} " + (Player.player.addAtk != 0 ? $"(+{Player.player.addAtk})" : ""));
            Console.WriteLine($"방어력 : {Player.player.baseAtk + Player.player.addAtk} " + (Player.player.addDef != 0 ? $"(+{Player.player.addDef})" : ""));
            Console.WriteLine($"체력 : {Player.player.maxHp} / {Player.player.hp}");
            Console.WriteLine($"마력 : {Player.player.maxMp} / {Player.player.mp}");
            Console.WriteLine($"Gold : {Player.player.gold} G\n");
            Console.WriteLine("==========================================================");
            Console.WriteLine("[  단   계  ]");

            for (int i = 0; i < dungeonList.Count; i++)
            {
                dungeonList[i].DungeonAnnounce(i);
            }

            Color.ChangeTextColor(Colors.RED, "", "0", ". 나가기\n");
            Console.WriteLine("");
            Console.WriteLine("입장하고 싶은 던전을 선택해주세요. ");
            Console.Write(">>");
            string selectedStage = Console.ReadLine();
            switch (selectedStage)
            {
                case "1":
                    if (Player.player.baseAtk >= DungeonEnter.Instance.dunAtk)
                    {
                        scene = DungeonScene.DungeonEntrance;
                        Console.WriteLine("던전에 입장하겠습니다. 건투를 빕니다!");
                        Console.ReadLine();
                        Battle.instance.BattleStart();
                        Console.ReadLine();
                        QuestBool.enterDungeon = true;
                    }
                    else
                    {
                        Color.ChangeTextColor(Colors.MAGENTA, "", $"{Player.player.name}");
                        Color.ChangeTextColor(Colors.RED, "", " 님의 공격력이 해당 던전의 적정 공격력보다 낮습니다. 그래도 입장 하시겠습니까?", "\n");
                        Console.WriteLine("1. 예");
                        Console.WriteLine("0. 이전 화면으로 돌아가기");
                        Console.Write(">>");

                        string stage = Console.ReadLine();

                        if (stage == "1")
                        {
                            scene = DungeonScene.DungeonEntrance;
                            Console.WriteLine("던전에 입장하겠습니다. 건투를 빕니다!");
                            Console.ReadLine();
                            Console.Clear();
                            Battle.instance.BattleStart();
                            QuestBool.enterDungeon = true;
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
                    if (Player.player.baseAtk + Player.player.addAtk >= DungeonEnter.Instance.dunAtk)
                    {
                        scene = DungeonScene.DungeonEntrance;
                        Console.WriteLine("던전에 입장하겠습니다. 건투를 빕니다!");
                        Console.ReadLine();
                        Console.Clear();
                        Battle.instance.BattleStart();
                        QuestBool.enterDungeon = true;
                    }
                    else
                    {
                        Color.ChangeTextColor(Colors.MAGENTA, "", $"{Player.player.name}");
                        Color.ChangeTextColor(Colors.RED, "", " 님의 공격력이 해당 던전의 적정 공격력보다 낮습니다. 그래도 입장 하시겠습니까?", "\n");
                        Console.WriteLine("1. 예");
                        Console.WriteLine("0. 이전 화면으로 돌아가기");
                        Console.Write(">>");

                        string stage = Console.ReadLine();

                        if (stage == "1")
                        {
                            scene = DungeonScene.DungeonEntrance;
                            Console.WriteLine("던전에 입장하겠습니다. 건투를 빕니다!");
                            Battle.instance.BattleStart();
                            QuestBool.enterDungeon = true;
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
                    if (Player.player.baseAtk + Player.player.addAtk >= DungeonEnter.Instance.dunAtk)
                    {
                        scene = DungeonScene.DungeonEntrance;
                        Console.WriteLine("던전에 입장하겠습니다. 건투를 빕니다!");
                        Console.ReadLine();
                        Console.Clear();
                        Battle.instance.BattleStart();
                        QuestBool.enterDungeon = true;
                    }
                    else
                    {
                        Color.ChangeTextColor(Colors.MAGENTA, "", $"{Player.player.name}");
                        Color.ChangeTextColor(Colors.RED, "", " 님의 공격력이 해당 던전의 적정 공격력보다 낮습니다. 그래도 입장 하시겠습니까?", "\n");
                        Console.WriteLine("1. 예");
                        Console.WriteLine("0. 이전 화면으로 돌아가기");
                        Console.Write(">>");

                        string stage = Console.ReadLine();

                        if (stage == "1")
                        {
                            scene = DungeonScene.DungeonEntrance;
                            Console.WriteLine("던전에 입장하겠습니다. 건투를 빕니다!");
                            Battle.instance.BattleStart();
                            QuestBool.enterDungeon = true;
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
                    if (Player.player.baseAtk + Player.player.addAtk >= DungeonEnter.Instance.dunAtk)
                    {
                        scene = DungeonScene.DungeonEntrance;
                        Console.WriteLine("던전에 입장하겠습니다. 건투를 빕니다!");
                        Console.ReadLine();
                        Console.Clear();
                        Battle.instance.BattleStart();
                        QuestBool.enterDungeon = true;
                    }
                    else
                    {
                        Color.ChangeTextColor(Colors.MAGENTA, "", $"{Player.player.name}");
                        Color.ChangeTextColor(Colors.RED, "", " 님의 공격력이 해당 던전의 적정 공격력보다 낮습니다. 그래도 입장 하시겠습니까?", "\n");
                        Console.WriteLine("1. 예");
                        Console.WriteLine("0. 이전 화면으로 돌아가기");
                        Console.Write(">>");

                        string stage = Console.ReadLine();

                        if (stage == "1")
                        {
                            scene = DungeonScene.DungeonEntrance;
                            Console.WriteLine("던전에 입장하겠습니다. 건투를 빕니다!");
                            Battle.instance.BattleStart();
                            QuestBool.enterDungeon = true;
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
                    if (Player.player.baseAtk + Player.player.addAtk >= DungeonEnter.Instance.dunAtk)
                    {
                        scene = DungeonScene.DungeonEntrance;
                        Console.WriteLine("던전에 입장하겠습니다. 건투를 빕니다!");
                        Console.ReadLine();
                        Console.Clear();
                        Battle.instance.BattleStart();
                        QuestBool.enterDungeon = true;
                    }
                    else
                    {
                        Color.ChangeTextColor(Colors.MAGENTA, "", $"{Player.player.name}");
                        Color.ChangeTextColor(Colors.RED,"" , " 님의 공격력이 해당 던전의 적정 공격력보다 낮습니다. 그래도 입장 하시겠습니까?", "\n");
                        Console.WriteLine("1. 예");
                        Console.WriteLine("0. 이전 화면으로 돌아가기");
                        Console.Write(">>");

                        string stage = Console.ReadLine();

                        if (stage == "1")
                        {
                            scene = DungeonScene.DungeonEntrance;
                            Console.WriteLine("던전에 입장하겠습니다. 건투를 빕니다!");
                            Battle.instance.BattleStart();
                            QuestBool.enterDungeon = true;
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