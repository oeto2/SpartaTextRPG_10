using System;
using System.Collections.Generic;
using SpartaTextRPG.DataClass;

namespace SpartaTextRPG
{
    internal class Battle
    {
        public static Battle instance = new Battle();
        public static List<Monster> Monsters()
        {
            List<Monster> monsters = new List<Monster>
            {
                new Monster(1, "슬라임", 10, 10, 10, 5),
                new Monster(1, "주황버섯", 10, 10, 10, 5),
                new Monster(1, "리본돼지", 10, 10, 10, 5)
            };

            // 몬스터에 연속 번호 부여
            for (int i = 0; i < monsters.Count; i++)
            {
                monsters[i].Number = i + 1;
            }

            return monsters;
        }

        public static List<Monster> MonsterSpawn()
        {
            Random rand = new Random();
            List<Monster> allMonsters = Monsters();
            int numSpawnedMonsters = rand.Next(1, 5);
            List<Monster> spawnedMonsters = new List<Monster>();

            for (int i = 0; i < numSpawnedMonsters; i++)
            {
                int randMonsterIndex = rand.Next(0, allMonsters.Count);
                Monster monster = allMonsters[randMonsterIndex];
                monster.Number = i + 1; // 연속 번호 할당
                spawnedMonsters.Add(monster);
            }
            return spawnedMonsters;
        }

        public string GetPlayerInfo(Player player)
        {
            return $"Lv.{player.level}  {player.name}  {player.job}\t " +
                   $"HP: \u001b[91m{player.hp} / {player.maxHp}\u001b[0m\n" +
                   $"\t\t\t MP: \u001b[94m{player.mp} / {player.maxMp}\u001b[0m";
        }
        void Attack() // 기본공격
        {
            
        }

        void Skill() // 스킬공격
        {
            
        }

        void GetDamage(Player player, List<Monster> monsters)
        {
            for (int i = 0; i < monsters.Count; i++)
            {
                float befHp = player.hp;
                float damage = monsters[i].Atk - player.baseDef + player.addDef;

                if (damage < 0)
                {
                    if (0 < monsters[i].Health)
                    {
                        player.hp -= damage;
                        BattleScene();
                        Console.WriteLine($"{monsters[i].Number} Lv.{monsters[i].Level} {monsters[i].Name} 의 공격!\n{player.name} 을/를 맞췄습니다! [데미지 : {player.maxHp - player.hp}]");

                        if (player.hp <= 0)
                        {
                            Console.WriteLine($"{monsters[i].Number} Lv.{player.level} {player.name}\n HP{player.maxHp} -> Dead\n\n Enter.다음");
                            Console.ReadLine();
                            break;
                        }
                        else
                        {
                            Console.Write($"{monsters[i].Number} Lv.{player.level} {player.name}\nHP {player.maxHp} -> {player.hp}\n\n Enter. 다음");
                            Console.ReadLine();
                        }
                    }
                }
            }
        }

        public void BattleScene()
        {
            Console.Clear();
            Console.WriteLine("================================================");
            Color.ChangeTextColor(Colors.YELLOW, "", $"Stage.{DungeonEnter.Instance.index + 1} - {DungeonEnter.Instance.name}\n");
            Console.WriteLine("================================================");
            Console.WriteLine("");
            Console.WriteLine("[몬스터 정보]");

            List<Monster> spawnedMonsters = MonsterSpawn();

            for (int i = 0; i < spawnedMonsters.Count; i++)
            {
                if (spawnedMonsters[i].Health > 0)
                {
                    Console.Write($"Lv.{spawnedMonsters[i].Level} \t {spawnedMonsters[i].Name} \t HP 체력: ");
                    Color.ChangeTextColor(Colors.RED, "", spawnedMonsters[i].Health + " / " + spawnedMonsters[i].MaxHealth);
                    Console.WriteLine("");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine($"Lv. {spawnedMonsters[i].Level} {spawnedMonsters[i].Name} Dead");
                    Console.ResetColor();
                }
            }

            Console.WriteLine("");
            Console.WriteLine("[내 정보]");
            Console.WriteLine(GetPlayerInfo(Player.player));
            Console.WriteLine("\n================================================");
            Console.WriteLine("[플레이어의 차례]");
            Console.WriteLine("1. 일반 공격");
            Console.WriteLine("2. 스킬 공격");
            Console.WriteLine("3. 아이템 사용");
            Console.WriteLine("행동을 선택해주세요.");
            Console.Write(">>");
            string userInput = Console.ReadLine();

            switch (userInput)
            {
                case "1":
                    Attack();
                    break;
                case "2":
                    Skill();
                    break;
            }
        }
    }
}
