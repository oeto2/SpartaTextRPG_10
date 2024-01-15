using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata;
using System.Threading;
using SpartaTextRPG.DataClass;
using static System.Net.Mime.MediaTypeNames;

namespace SpartaTextRPG
{
    internal class Battle
    {
        public static Battle instance = new Battle();
        List<Monster> Monsters = new List<Monster>();
        List<Monster> spawnedMonsters = new List<Monster>();
        List<Monster> monsters = new List<Monster>();
        int[] monsterHp;

        public void BattleStart() // 2. 전투 시작
        {
            monsters = Monsters();
            spawnedMonsters = MonsterSpawn();
            monsterHp = new int[spawnedMonsters.Count];
            while (0 < Player.player.hp) // 전투 시작 플레이어 턴
            {
                BattleScene();
                switch (ChoiceInput(1, 3))
                {
                    case 1:
                        Attack();
                        break;
                    case 2:
                        // Skill();
                        break;
                    case 3:
                        // HpRecovery();
                        break;
                }
                if (CheckMonsters() == 0) break;
            }
            if (Player.player.hp > 0)
                ClearReward();
            else
                FailReward();
            int ChoiceInput(int fst, int last) // 선택지 입력 메서드
            {
                Console.WriteLine();
                string input = Console.ReadLine();
                int choice;
                while (!(int.TryParse(input, out choice)) || choice < fst || choice > last)
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    Console.Write("                    \r");
                    input = Console.ReadLine();
                }
                return choice;
            }

            static List<Monster> Monsters() // 몬스터 정보
            {
                Random rand = new Random();
                List<Monster> monsters = new List<Monster>
                {
                    new Monster(1, "비아키스", 10, 10, 10, 5),
                    new Monster(2, "아브렐슈드", 15, 15, 15, 8),
                    new Monster(3, "쿠크세이튼", 20, 20, 20, 10)
                };
                // 몬스터에 연속 번호 부여
                for (int i = 0; i < monsters.Count; i++)
                {
                    monsters[i].Number = i + 1;
                }
                return monsters;
            }
            static List<Monster> MonsterSpawn()
            {
                List<Monster> spawnedMonsters = new List<Monster>();
                Random rand = new Random();
                int Monster_count = rand.Next(1, 5);

                for (int i = 0; i < Monster_count; i++)
                {
                    int randMonsterIndex = rand.Next(0, 4);
                    switch(randMonsterIndex)
                    {
                        case 0:
                            Monster Vykas = new Monster(1, "비아키스", 10, 10, 10, 5);
                            spawnedMonsters.Add(Vykas);
                            break;
                        case 1:
                            Monster Abrel = new Monster(2, "아브렐슈드", 15, 15, 15, 8);
                            spawnedMonsters.Add(Abrel);
                            break;
                        case 2:
                            Monster Saton = new Monster(3, "쿠크세이튼", 20, 20, 20, 10);
                            spawnedMonsters.Add(Saton);
                            break;
                    }
                }
                return spawnedMonsters;
            }

            string GetPlayerInfo(Player player) // 플레이어 정보 불러오기
            {
                return $"Lv.{player.level}  {player.name}  {player.job}\t " +
                       $"HP: \u001b[91m{player.hp} / {player.maxHp}\u001b[0m\n" +
                       $"\t\t\t MP: \u001b[94m{player.mp} / {player.maxMp}\u001b[0m";
            }
            void MonsterNumber() // 몬스터 번호 출력 메서드
            {
                for (int i = 0; i < monsters.Count; i++)
                {
                    if (0 < spawnedMonsters[i].Health)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine($"{i + 1}");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine($"{i + 1}");
                        Console.ResetColor();
                    }
                }
            }
            void DisplayBattleStatus()
            {
                Console.WriteLine("[몬스터 정보]");
                for (int i = 0; i < spawnedMonsters.Count; i++)
                {
                    if (spawnedMonsters[i].Health > 0)
                    {
                        Console.Write($"{spawnedMonsters[i].Number}  Lv.{spawnedMonsters[i].Level}\t{spawnedMonsters[i].Name} \t HP : ");
                        Color.ChangeTextColor(Colors.RED, "", $"{spawnedMonsters[i].Health} / {spawnedMonsters[i].MaxHealth}");
                        Console.WriteLine("");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.WriteLine($"{spawnedMonsters[i].Number}  Lv. {spawnedMonsters[i].Level} {spawnedMonsters[i].Name} Dead");
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
            }

            void Attack() // 플레이어 공격
            {
                Random rand = new Random();

                Console.Clear();
                BattleScene();
                MonsterNumber();

                Console.WriteLine("대상을 선택해주세요.");
                Console.Write(">>");

                int atk = ChoiceInput(1, monsters.Count);

                if (atk != 0 && atk <= monsters.Count) // 0일 경우에 대한 예외 처리 및 몬스터의 수 이내의 값인지 확인
                {
                    if (0 < monsterHp[atk - 1])
                    {
                        int Critical = rand.Next(1, 101);
                        int bh = monsterHp[atk - 1];

                        if (Critical <= 15)
                        {
                            monsterHp[atk - 1] -= rand.Next((int)Math.Ceiling(Player.player.baseAtk + Player.player.addAtk * 1.44f), (int)Math.Ceiling(Player.player.baseAtk + Player.player.addAtk * 1.76f) + 1); // 플레이어 공격 데미지
                            BattleScene();
                            Console.WriteLine($"\n\n{Player.player.name} 의 공격!\n");
                            Console.WriteLine($"Lv.{monsters[atk - 1].Level} {monsters[atk - 1].Name} 을(를) 맞췄습니다. [데미지 : {bh - monsterHp[atk - 1]}] - 치명타!!");
                        }
                        else if (Critical >= 90)
                        {
                            BattleScene();
                            Console.WriteLine($"\n\n{Player.player.name} 의 공격!\n");
                            Console.WriteLine($"Lv.{monsters[atk - 1].Level} {monsters[atk - 1].Name} 을(를) 공격했지만 아무일도 일어나지 않았습니다.");
                        }
                        else
                        {
                            monsterHp[atk - 1] -= rand.Next((int)Math.Ceiling(Player.player.baseAtk + Player.player.addAtk * 0.9f), (int)Math.Ceiling(Player.player.baseAtk + Player.player.addAtk * 1.1f) + 1); // 플레이어 공격 데미지
                            BattleScene();
                            Console.WriteLine($"\n\n{Player.player.name} 의 공격!\n");
                            Console.WriteLine($"Lv.{monsters[atk - 1].Level} {monsters[atk - 1].Name} 을(를) 맞췄습니다. [데미지 : {bh - monsterHp[atk - 1]}]");
                        }
                        if (monsterHp[atk - 1] <= 0)
                        {
                            Console.WriteLine($"\nLv.{monsters[atk - 1].Level} {monsters[atk - 1].Name}\nHP {bh} -> Dead");
                        }

                        Console.WriteLine("\n\nEnter. 다음");
                        Console.ReadLine();

                        if (CheckMonsters() != 0)
                        {
                            MonsterTurn(); // 공격 종료 후, 몬스터가 남아있으면 몬스터 턴
                        }
                    }
                    else
                    {
                        Console.WriteLine("Dead 상태의 몬스터는 공격할 수 없습니다.\n\nEnter. 다음");
                        Console.ReadLine();
                    }
                }
                else
                {
                    Console.WriteLine("잘못된 선택입니다.\n\nEnter. 다음");
                    Console.ReadLine();
                }
            }

            /*
            void Skill()
            {
            }
            */

            void MonsterTurn() // 몬스터 턴, 몬스터 행동
            {
                for (int i = 0; i < monsters.Count; i++)
                {
                    float damage = monsters[i].Atk - Player.player.baseDef - Player.player.addDef;    // 몬스터 데미지
                    if (damage < 0) damage = 0;
                    if (0 < monsters[i].Health)
                    {

                        Player.player.maxHp -= damage;
                        BattleScene();
                        Console.WriteLine("\n");
                        Console.WriteLine($"Lv.{monsters[i].Level} {monsters[i].Name} 의 공격!\n{Player.player.name} 을(를) 맞췄습니다. [데미지 : {Player.player.maxHp - Player.player.hp}]\n");
                        if (Player.player.hp <= 0)
                        {
                            Console.WriteLine($"Lv.{Player.player.level} {Player.player.name}\nHP {Player.player.maxHp} -> Dead\n\nEnter. 다음");
                            Console.ReadLine();
                            break;
                        }
                        else
                        {
                            Console.Write($"Lv.{Player.player.level} {Player.player.name}\nHP {Player.player.maxHp} -> {Player.player.hp}\n\nEnter. 다음");
                            Console.ReadLine();
                        }
                    }
                }
            }

            int CheckMonsters()
            {
                int[] monsterHp = new int[monsters.Count];
                int live = 0;
                for (int i = 0; i < monsters.Count; i++)
                {
                    if (0 < monsterHp[i]) live++;
                }
                return live;
            }

            void ClearReward() // 클리어 보상
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("   전투 결과t\n\n");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("   Victory\n\n");
                Console.ResetColor();
                // 리워드 설정
                Game.currentStageReward.isClear = true;
                Game.currentStageReward.gold = 100; // 골드획득
            }
            void FailReward() // 실패
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("   전투 결과\n\n");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("   You Lose\n\n");
                Console.ResetColor();
                // 실패 시 전리품 미지급
                Game.currentStageReward.isClear = false;
            }

            /*
            void HpRecovery()
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("[회복]\n");
                Console.ResetColor();
                Console.Write("체력, 마나 포션을 사용하면 Hp/Mp를 ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("30");
                Console.ResetColor();
                Console.Write(" 회복 할 수 있습니다.\n");
                Console.Write("\n(남은 체력 포션 : ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(Player.player.HpPotion);
                Console.ResetColor();
                Console.Write(" )\n");
                Console.Write("\n(남은 마나 포션 : ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(Player.player.MpPotion);
                Console.ResetColor();
                Console.Write(" )\n");

                while (true)
                {
                    Console.WriteLine("\n1. 체력 회복 \n2. 마나 회복 \n3. 돌아가기");
                    switch (ChoiceInput(1, 3))
                    {
                        case 1:
                            UseHpPotion();
                            break;
                        case 2:
                            UseMpPotion();
                            break;
                        case 3:
                            // 전투 중인 곳으로 돌아가기
                            return;
                    }
                }
            }
            void UseHpPotion()
            {
                if (player.HpPotion > 0)
                {
                    if (player.Hp < player.MaxHp)
                    {
                        int currentHp = player.Hp; // 현재 체력 저장, 플레이어hp
                        player.Hp = Math.Min(player.Hp + 30, player.MaxHp);
                        int recoveryHp = player.Hp - currentHp; // 회복량 계산 
                        player.HpPotion--; // 1개 소모
                        Console.Write("\n체력을 ");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(recoveryHp); // 회복량 출력
                        Console.ResetColor();
                        Console.Write("회복 하였습니다.\n");
                        Console.WriteLine("\n");
                        Console.Write("플레이어의 현재 체력 : ");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(player.Hp);
                        Console.ResetColor();
                        Console.WriteLine();
                        Console.Write("남은 체력 포션 : ");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write(player.HpPotion);
                        Console.ResetColor();
                        Console.WriteLine();
                    }
                    else if (player.Hp >= player.MaxHp)
                    {
                        Console.Write("이미 ");
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("최대 체력");
                        Console.ResetColor();
                        Console.Write("입니다.");
                        Console.WriteLine();
                    }

                    Console.Write("입니다.");
                    Console.WriteLine();
                }
                else
                {
                    Console.Write("체력 포션이 ");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("부족");
                    Console.ResetColor();
                    Console.Write("합니다!");
                    Console.WriteLine();
                }
            }
            void UseMpPotion()
            {
                if (player.MpPotion > 0)
                {
                    if (player.Mp < player.MaxMp)
                    {
                        int currentMp = player.Mp; // 현재 마나 저장, 플레이어hp
                        player.Mp = Math.Min(player.Mp + 30, player.MaxMp);
                        int recoveryMp = player.Mp - currentMp; // 회복량 계산 
                        player.MpPotion--; // 1개 소모
                        Console.Write("\n체력을 ");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(recoveryMp); // 회복량 출력
                        Console.ResetColor();
                        Console.Write("회복 하였습니다.\n");
                        Console.WriteLine("\n");
                        Console.Write("플레이어의 현재 마나 : ");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(player.Mp);
                        Console.ResetColor();
                        Console.WriteLine();
                        Console.Write("남은 마나 포션 : ");
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write(player.MpPotion);
                        Console.ResetColor();
                        Console.WriteLine();
                    }
                    else if (player.Mp >= player.MaxMp)
                    {
                        Console.Write("이미 ");
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write("최대 마나");
                        Console.ResetColor();
                        Console.Write("입니다.");
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.Write("마나 포션이 ");
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("부족");
                        Console.ResetColor();
                        Console.Write("합니다!");
                        Console.WriteLine();
                    }
                }
            }
            */

            void BattleScene() // 현재 전투 필드 출력 메서드
            {
                Console.Clear();
                int index;
                Console.WriteLine("================================================");
                // Color.ChangeTextColor(Colors.YELLOW, "", $"Stage.{index} - {Dungeon.instance.dungeonList[index - 1].name}\n");
                Console.WriteLine("================================================");
                Console.WriteLine("");
                DisplayBattleStatus();
            }
        }
    }
}