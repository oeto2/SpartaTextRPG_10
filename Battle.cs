using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
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
        Random rand = new Random();
        int[] monsterHp;
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

        public void BattleStart() // 2. 전투 시작
        {
            monsters = Monsters();
            spawnedMonsters = MonsterSpawn();
            monsterHp = new int[spawnedMonsters.Count];
            PlayerTurn();

            static List<Monster> Monsters() // 몬스터 정보
            {
                List<Monster> monsters = new List<Monster>
                {
                    new Monster(1, "비아키스", 10, 10, 10, 1),
                    new Monster(2, "아브렐슈드", 15, 15, 15, 1),
                    new Monster(3, "쿠크세이튼", 20, 20, 20, 1)
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
                    int randMonsterIndex = rand.Next(0, 5);
                    if (i == 0) randMonsterIndex = rand.Next(0, 2);
                    switch (randMonsterIndex)
                    {
                        case 0:
                            Monster Vykas = new Monster(1, "비아키스", 10, 10, 10, 0);
                            spawnedMonsters.Add(Vykas);
                            break;
                        case 1:
                            Monster Abrel = new Monster(2, "아브렐슈드", 15, 15, 15, 0);
                            spawnedMonsters.Add(Abrel);
                            break;
                        case 2:
                            Monster Saton = new Monster(3, "쿠크세이튼", 20, 20, 20, 0);
                            spawnedMonsters.Add(Saton);
                            break;
                    }
                }
                return spawnedMonsters;
            }
        }

        public string GetPlayerInfo(Player player) // 플레이어 정보 불러오기
        {
            return $"Lv.{player.level}  {player.name}  {player.job}\t " +
                   $"HP: \u001b[91m{player.hp} / {player.maxHp}\u001b[0m\n" +
                   $"\t\t\t MP: \u001b[94m{player.mp} / {player.maxMp}\u001b[0m";
        }

        public void DisplayBattleStatus()
        {
            Console.WriteLine("[몬스터 정보]");
            for (int i = 0; i < spawnedMonsters.Count; i++)
            {
                Console.Write($"{i + 1}  Lv.{spawnedMonsters[i].Level}\t{spawnedMonsters[i].Name} \t HP : ");
                if (spawnedMonsters[i].Health > 0)
                {
                    Color.ChangeTextColor(Colors.RED, "", $"{spawnedMonsters[i].Health} / {spawnedMonsters[i].MaxHealth}");
                    Console.WriteLine("");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine($"Dead");
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
        public void PlayerTurn()
        {
            while (0 < Player.player.hp) // 전투 시작 플레이어 턴
            {
                BattleScene(1);
                switch (ChoiceInput(1, 3))
                {
                    case 1:
                        Attack();
                        break;
                    case 2:
                        if (Skills.myskills.Count > 0)
                        {
                            Skill.showSkill(spawnedMonsters);
                        }
                        else
                        {
                            Color.ChangeTextColor(Colors.RED, "", "보유 스킬이 없습니다.", "\n");
                        }
                        break;
                    case 3:
                        // HpRecovery();
                        break;
                }
            }
            if (Player.player.hp > 0)
            {
                Console.ReadLine();
                Reward.Instance.ClearReward(1);
            }
            else
            {
                Reward.Instance.FailReward(1);

            }
        }
        public void Attack()
        {
            {
                Console.Clear();
                BattleScene(1);

                int selectedMonsterNumber;

                while (true)
                {
                    Console.WriteLine("대상을 선택해주세요.");
                    Console.Write(">>");

                    if (int.TryParse(Console.ReadLine(), out selectedMonsterNumber) && selectedMonsterNumber > 0 && selectedMonsterNumber <= spawnedMonsters.Count)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("잘못된 몬스터 번호입니다. 다시 입력해주세요.");
                    }
                }

                Monster targetMonster = spawnedMonsters[selectedMonsterNumber - 1];
                float damageToMonster = Math.Max(0, Player.player.baseAtk + Player.player.addAtk - targetMonster.Def);
                bool iscritical = false;
                bool isdodge = false;

                damageToMonster = CriticalAttack(damageToMonster, ref iscritical);
                damageToMonster = DodgeAttack(damageToMonster, false, ref isdodge);

                // 몬스터에게 데미지 적용
                if (damageToMonster > 0)
                {
                    targetMonster.Health -= damageToMonster;
                    Console.WriteLine($"{Player.player.name}이(가) {targetMonster.Name}에게 일반 공격을 가했습니다! [데미지: {damageToMonster}]");
                    Console.ReadLine();
                    Console.WriteLine("");
                    if (targetMonster.Health <= 0)
                    {
                        Console.WriteLine($"{targetMonster.Name}이(가) 사망했습니다!");

                        // 몬스터가 다 죽었을 때
                        bool allMonstersDead = spawnedMonsters.All(monster => monster.Health <= 0);
                        Console.ReadLine();

                        if (allMonstersDead == true)
                        {
                            Reward.Instance.ClearReward(1);
                        }

                    }
                }
                else if (targetMonster.Health == 0 && isdodge == false)
                {
                    targetMonster.Health = 0;
                    Console.WriteLine("이미 죽은 몬스터에게는 데미지를 입힐 수 없습니다!");
                }
                // 몬스터 턴
                MonsterTurn();
            }
        }

        public int CriticalAttack(float damage, ref bool isCritical) // 크리티컬
        {
            int critical = new Random().Next(1, 101);
            if (critical <= 15)
            { // 크리티컬일때
                isCritical = true;
                double newCharacterSkill = damage * 1.6;
                damage = (int)Math.Round(newCharacterSkill);
            }
            else
            {
                isCritical = false;
            }

            return (int)damage;
        }

        public float DodgeAttack(float damage, bool isUsingSkill, ref bool isDodge) // 회피
        {
            int dodge = new Random().Next(1, 101);
            if (dodge > 11 && !isUsingSkill)
            {
                // 대미지 들어감 
                isDodge = false;
            }
            else
            {
                // 회피
                isDodge = true;
                Console.WriteLine("회피 하였습니다.");
                return 0; // 회피했으면 데미지 0 반환
            }
            return damage; // 회피하지 않았으면 원래의 대미지 반환
        }


        /*
        void Skill()
        {
        }
        */

        // 몬스터 턴
        public void MonsterTurn()
        {
            for (int i = 0; i < spawnedMonsters.Count; i++)
            {
                if (spawnedMonsters[i].Health > 0)
                {
                    float damage = spawnedMonsters[i].Atk - Player.player.baseDef + Player.player.addDef;
                    if (damage < 0) damage = 0;

                    // 몬스터 데미지
                    Player.player.hp -= damage;

                    // 전투 상황 출력
                    Console.WriteLine($"Lv.{spawnedMonsters[i].Level} {spawnedMonsters[i].Name} 의 공격!\n{Player.player.name} 을(를) 맞췄습니다. [데미지 : {damage}]");

                    if (Player.player.hp <= 0)
                    {
                        Console.WriteLine($"Lv.{Player.player.level} {Player.player.name}\nHP {Player.player.maxHp} -> Dead\n\nEnter. 다음");
                        Console.ReadLine();
                        Console.Clear();
                        Reward.Instance.FailReward(1);
                        return;
                    }
                    else
                    {
                        Console.Write($"Lv.{Player.player.level} {Player.player.name}\nHP {Player.player.maxHp} -> {Player.player.hp}\n\nEnter. 다음");
                        Console.ReadLine();
                        PlayerTurn();
                    }
                }
            }
        }

        public int CheckMonsters()
        {
            int live = 0;
            for (int i = 0; i < monsterHp.Length; i++)
            {
                if (0 < monsterHp[i]) live++;
            }
            return live;
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

        public void BattleScene(int index) // 현재 전투 필드 출력 메서드
        {
            Console.Clear();
            Console.WriteLine("================================================");
            Color.ChangeTextColor(Colors.YELLOW, "", $"Stage.{index} - {Dungeon.instance.dungeonList[index - 1].name}\n");
            Console.WriteLine("================================================");
            Console.WriteLine("");
            DisplayBattleStatus();
        }
    }
}