using System;
using System.Collections.Generic;
using System.Numerics;
using SpartaTextRPG;
using SpartaTextRPG.DataClass;

namespace SpartaTextRPG
{
    public class Dungeon
    {
        private static Dungeon _instance;
        public static Dungeon instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Dungeon();
                }
                return _instance;
            }
        }
        public int DungeonLevel { get; set; }
        public int MonsterCount;

        public Dungeon()
        {
            DungeonLevel = 1;
        }

        public List<Monster> MonsterSpawn()
        {
            Random random = new Random();
            MonsterCount = 4; // 항상 4마리의 몬스터 생성

            List<Monster> Monsters = new List<Monster>();
            Monster[] availableMonsters = Game.Monsters();

            for (int i = 0; i < MonsterCount; i++)
            {
                Monster randomMonster = availableMonsters[random.Next(0, availableMonsters.Length)];
                Monster monster = new Monster(randomMonster.Level, randomMonster.Name, randomMonster.Health, randomMonster.Atk, randomMonster.Def);
                Monsters.Add(monster);
            }
            return Monsters;
        }

        public struct CurrentDungeonReward
        {
            public bool isClear;
            public int stageNumber;
            public int exp;
            public int gold;
        }

        public void ClearReward()
        {
            // 리워드 설정
            Game.currentStageReward.isClear = true;
            Game.currentStageReward.stageNumber = DungeonLevel;
            Game.currentStageReward.exp = DungeonLevel * 20;
            Game.currentStageReward.gold = 100; // 골드획득 

            // 레벨 1 증가
            DungeonLevel++;
        }

        public void FailReward()
        {
            // 실패 시 전리품 미지급
            Game.currentStageReward.isClear = false;
            Game.currentStageReward.stageNumber = DungeonLevel;
        }

        public void DungeonEntrance()
        {
            Console.Clear();
            Console.WriteLine("==========================================================");
            Color.ChangeTextColor(Colors.YELLOW, "", "던  전  입  구\n");
            Console.WriteLine("아스키 아트 삽입 예정 (던전 입구 이미지)");
            Console.WriteLine("==========================================================");
            Color.ChangeTextColor(Colors.MAGENTA, "모험가 ", Player.player.name, "\n\n");
            Console.WriteLine($"Lv. {Player.player.level}");
            //Console.WriteLine($"chad < {Player.job} >\n");
            Console.WriteLine($"공격력 : {Player.player.baseAtk} " + (Player.player.addAtk != 0 ? $"(+{Player.player.addAtk})" : ""));
            Console.WriteLine($"방어력 : {Player.player.baseDef} " + (Player.player.addDef != 0 ? $"(+{Player.player.addDef})" : ""));
            Console.WriteLine($"체력 : {Player.player.maxHp} / {Player.player.hp}");
            Console.WriteLine($"마력 : {Player.player.maxMp} / {Player.player.mp}");
            Console.WriteLine($"Gold : {Player.player.gold} G\n");
            Console.WriteLine("==========================================================");
            Console.WriteLine("1. 던전 입장");
            Console.WriteLine("2. 이전 메뉴로 돌아가기");
            Console.WriteLine("메뉴를 선택하세요:");
            Console.Write(">>");

            string userInput = Console.ReadLine();

            if (userInput == "1")
            {
                if (Player.player.hp <= 20)
                {
                    int stage1RecommendedAttack = 10;
                    int stage2RecommendedAttack = 20;
                    int stage3RecommendedAttack = 30;
                    int stage4RecommendedAttack = 40;
                    int stage5RecommendedAttack = 50;

                    Console.Clear();
                    Console.WriteLine("==========================================================");
                    Color.ChangeTextColor(Colors.YELLOW, "", "던  전  입  구\n");
                    Console.WriteLine("아스키 아트 삽입 예정 (던전 입구 이미지)");
                    Console.WriteLine("==========================================================");
                    Color.ChangeTextColor(Colors.MAGENTA, "모험가 ", Player.player.name, "\n\n");
                    Console.WriteLine($"Lv. {Player.player.level}");
                    //Console.WriteLine($"chad < {Player.job} >\n");
                    Console.WriteLine($"공격력 : {Player.player.baseAtk} " + (Player.player.addAtk != 0 ? $"(+{Player.player.addAtk})" : ""));
                    Console.WriteLine($"방어력 : {Player.player.baseDef} " + (Player.player.addDef != 0 ? $"(+{Player.player.addDef})" : ""));
                    Console.WriteLine($"체력 : {Player.player.maxHp} / {Player.player.hp}");
                    Console.WriteLine($"마력 : {Player.player.maxMp} / {Player.player.mp}");
                    Console.WriteLine($"Gold : {Player.player.gold} G\n");
                    Console.WriteLine("==========================================================");
                    Console.WriteLine("[  단   계  ]");
                    Console.WriteLine("Stage.1 - 모험의 시작");
                    if (Player.player.baseAtk >= stage1RecommendedAttack)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    Console.Write($"적정 공격력 : {stage1RecommendedAttack}");
                    Console.WriteLine("Stage.2 - 용의 둥지");
                    if (Player.player.baseAtk >= stage2RecommendedAttack)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    Console.Write($"적정 공격력 : {stage2RecommendedAttack}");
                    Console.WriteLine(stage2RecommendedAttack);
                    Console.WriteLine("Stage.3 - 불의 둥지");
                    Console.WriteLine("Stage.4 - 물의 둥지");
                    Console.WriteLine("Stage.5 - 어둠의 둥지");
                    Console.WriteLine("");
                    Console.WriteLine("입장하고 싶은 던전을 선택해주세요. :");
                    Console.Write(">>");

                    string selectedStage = Console.ReadLine();

                    switch (selectedStage)
                    {
                        case "1":
                            Console.WriteLine("던전에 입장하겠습니다. 건투를 빕니다!");
                            Console.ReadLine();
                            break;

                        case "2":
                            Console.WriteLine("던전에 입장하겠습니다. 건투를 빕니다!");
                            Console.ReadLine();
                            break;

                        case "3":
                            Console.WriteLine("던전에 입장하겠습니다. 건투를 빕니다!");
                            Console.ReadLine();
                            break;

                        case "4":
                            Console.WriteLine("던전에 입장하겠습니다. 건투를 빕니다!");
                            Console.ReadLine();
                            break;

                        case "5":
                            Console.WriteLine("던전에 입장하겠습니다. 건투를 빕니다!");
                            Console.ReadLine();
                            break;
                    }
                }
                else if (Player.player.hp < 20)
                {
                    // 던전 입장 조건을 충족하지 못한 경우
                    Console.WriteLine("던전에 입장할 수 없습니다. 체력이 부족합니다.");
                }
                else
                {
                    Console.WriteLine("유효하지 않는 값입니다. 다시 선택해주세요.");
                    Console.Write(">>");
                }
            }
            else if (userInput == "2")
            {
                Console.Clear();
                Program.scene = Scene.mainScene;
                GameManager.instance.MainGameScene();
            }
            else
            {
                Console.WriteLine("유효하지 않는 값입니다. 다시 선택해주세요.");
                Console.Write(">>");
            }
        }
    }

    internal class Game
    {
        public static Monster[] Monsters()
        {
            return new Monster[]
            {
                new Monster(1, "몬스터1", 100, 10, 5),
                new Monster(1, "몬스터2", 100, 10, 5),
                new Monster(1, "몬스터3", 100, 10, 5)
            };
        }
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