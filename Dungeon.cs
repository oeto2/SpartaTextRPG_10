using System;
using System.Collections.Generic;
using SpartaTextRPG;

namespace SpartaTextRPG
{
    public class Dungeon
    {
        private static Dungeon _instance;
        public static Dungeon Instance
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
            bool inDungeon = false;

            while (true)
            {
                Console.Clear();
                Color.ChangeTextColor(Colors.YELLOW, "", "던 전 입 구\n");
                Console.WriteLine("아스키 아트 삽입 예정 (던전 입구 이미지)");
                Console.WriteLine("==========================================================");
                Console.WriteLine("1. 던전 입장");
                Console.WriteLine("2. 이전 메뉴로 돌아가기");
                Console.WriteLine("메뉴를 선택하세요:");
                Console.Write(">>");

                string userInput = Console.ReadLine();

                if (userInput == "1")
                {
                    Player player = new Player(1, "PlayerName", 100.0f, 100.0f, Job.Warrior, 50.0f, 30.0f, 20.0f, 10.0f);

                    if (player.hp <= 20)
                    {
                        Console.WriteLine("던전에 입장하였습니다. 건투를 빕니다!");
                        inDungeon = true;
                        // 던전으로 이동하는 로직 추가
                    }
                    else
                    {
                        // 던전 입장 조건을 충족하지 못한 경우
                        Console.WriteLine("던전에 입장할 수 없습니다. 체력이 부족합니다.");
                    }
                }
                else if (userInput == "2")
                {
                    Console.WriteLine("던전에서 빠져나왔습니다.");
                    inDungeon = false;
                    Program.scene = Scene.mainScene;
                    // 이전 메뉴로 돌아가는 로직 추가
                }
                else
                {
                    Console.WriteLine("유효하지 않는 값입니다. 다시 선택해주세요.");
                    Console.Write(">>");
                }
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
