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

        public static bool isClear = false;

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

        public void BattleStart(int stageLevel) // 2. 전투 시작
        {
            DungeonEnter.dungeonLevel = stageLevel + 1;
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
                    new Monster(3, "쿠크세이튼", 20, 20, 20, 1),
                    new Monster(4, "한효승 매니저", 30, 50, 50, 30)

                };
                // 몬스터에 연속 번호 부여
                for (int i = 0; i < monsters.Count; i++)
                {
                    monsters[i].Number = i + 1;
                }
                return monsters;
            }
            List<Monster> MonsterSpawn()
            {
                List<Monster> spawnedMonsters = new List<Monster>();
                Random rand = new Random();
                int Monster_count = rand.Next(1, 5);

                if (DungeonEnter.dungeonLevel == 5)
                {
                    Monster Manager = new Monster(999, "한효승 매니저", 3000, 3000, 50, 30);
                    spawnedMonsters.Add(Manager);
                }
                for (int i = 0; i < Monster_count; i++)
                {
                    int randMonsterIndex = rand.Next(0, 3);

                    switch (randMonsterIndex)
                    {
                        case 0:
                            Monster Vykas = new Monster(stageLevel + 1, "비아키스", 10 * ((float)Math.Pow(stageLevel + 1, 2)), 10 * ((float)Math.Pow(stageLevel + 1, 2)), 4 * stageLevel + 1, 2 * stageLevel + 1);
                            spawnedMonsters.Add(Vykas);
                            break;
                        case 1:
                            Monster Abrel = new Monster(stageLevel + 1, "아브렐슈드", 10 * ((float)Math.Pow(stageLevel + 1, 2)), 10 * ((float)Math.Pow(stageLevel + 1, 2)), 5 * stageLevel + 1, 1 * stageLevel + 1);
                            spawnedMonsters.Add(Abrel);
                            break;
                        case 2:
                            Monster Saton = new Monster(stageLevel + 1, "쿠크세이튼", 10 * ((float)Math.Pow(stageLevel + 1, 2)), 10 * ((float)Math.Pow(stageLevel + 1, 2)), 6 * stageLevel + 1, 0 * stageLevel + 1);
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
            while (0 < Player.player.hp && !isClear) // 전투 시작 플레이어 턴
            {
                BattleScene(DungeonEnter.dungeonLevel);
                switch (ChoiceInput(1, 3))
                {
                    case 1:
                        if (Attack())
                            return;
                        break;
                    case 2:
                        if (Skill.showSkill(spawnedMonsters))
                            return;
                        break;
                    case 3:
                        UsePotion(1);
                        break;
                }
            }
        }
        public bool Attack()
        {
            {
                Console.Clear();
                BattleScene(DungeonEnter.dungeonLevel);

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
                            Reward.Instance.ClearReward(DungeonEnter.dungeonLevel);
                            return true;
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
                return false;
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
                        Reward.Instance.FailReward(DungeonEnter.dungeonLevel);
                        return;
                    }
                    else
                    {
                        Console.Write($"Lv.{Player.player.level} {Player.player.name}\nHP {Player.player.maxHp} -> {Player.player.hp}\n\nEnter. 다음");
                        Console.ReadLine();
                        if (!isClear)
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
        public void UsePotion(int index)
        {
            Console.Clear();
            Console.WriteLine("================================================");
            Color.ChangeTextColor(Colors.YELLOW, "", $"Stage.{index} - {Dungeon.instance.dungeonList[index - 1].name}\n");
            Console.WriteLine("================================================");
            Console.WriteLine("");
            while (true)
            {
                int input;
                Console.WriteLine("\n1. 하급 포션 : HP를 30 회복해줍니다. \n2. 중급 포션 : HP를 60, MP를 30 회복해줍니다. \n3. 돌아가기");
                Console.Write(">>");
                switch (input = ChoiceInput(1, 3))
                {
                    case 1:
                    case 2:
                        UseHpPotion(input);
                        break;
                    case 3:
                        // 전투 중인 곳으로 돌아가기
                        return;
                }
            }
        }
        void UseHpPotion(int index)
        {
            Console.Clear();
            Console.WriteLine("================================================");
            Color.ChangeTextColor(Colors.YELLOW, "", $"Stage.{index} - {Dungeon.instance.dungeonList[index - 1].name}\n");
            Console.WriteLine("================================================");
            Console.WriteLine("");
            Console.WriteLine("[포션 목록]");
            if (Player.player.hp >= Player.player.maxHp)
            {
                Console.WriteLine("이미 최대 HP 입니다.");
            }
            else if (Item.Instance.consumItems[index - 1].count > 0)
            {
                Item.Instance.consumItems[index - 1].UsePotion();
                Console.Write("\nHP를");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(Item.Instance.consumItems[index - 1].recoveryHp); // 회복량 출력
                Console.ResetColor();
                Console.Write("회복 하였습니다.\n");
                if (Item.Instance.consumItems[index - 1].recoveryMp > 0)
                {
                    Console.Write("\nMP를");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.ResetColor();
                    Console.Write(Item.Instance.consumItems[index - 1].recoveryMp);
                    Console.Write("회복 하였습니다.");
                }
                Console.WriteLine("\n");
                Console.Write("플레이어의 현재 HP : ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(Player.player.hp);
                Console.ResetColor();
                Console.WriteLine();
                Console.Write("남은" + Item.Instance.consumItems[index - 1].name + " : ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(Item.Instance.consumItems[index - 1].count);
                Console.ResetColor();
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("보유하신 포션이 없습니다.");
            }
        }

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