using System;
using SpartaTextRPG;
using SpartaTextRPG.DataClass;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Emit;
using System.Xml.Linq;
using System.Numerics;
using System.Threading;
using System.Runtime.ConstrainedExecution;

namespace SpartaTextRPG
{
    internal partial class Battle
    {
        public static Battle instance = new Battle();
        public int DungeonLevel { get; set; }
        public static Monster[] Monsters()
        {
            return new Monster[]
            {   //int number, int level, string name, float currenthp, float basehp, int atk, int def
                new Monster(1, 1, "슬라임", 100, 100, 10, 5),
                new Monster(2, 1, "주황버섯", 100, 100, 10, 5),
                new Monster(3, 1, "리본돼지", 100, 100, 10, 5)
            };
        }

        public string GetMonsterInfo(Monster monster) // 몬스터 정보
        {
            return $"\n{monster.Number}.  레벨: {monster.Level} 몬스터 이름: {monster.Name}      체력: \u001b[31m{monster.Health} / {monster.MaxHealth}\u001b[0m";
        }

        public string GetPlayerInfo(Player player) // 플레이어 정보
        {
            return $"\nLv.{player.level}  {player.name}      체력: \u001b[31m{player.hp} / {player.maxHp}\u001b[0m";
        }
        public class MonsterSpawner // 몬스터 스폰
        {
            private Random rand = new Random();
            private int respawn;

            private int MonsterMinCount { get; set; }
            private int MonsterMaxCount { get; set; }

            public MonsterSpawner(int minCount, int maxCount)
            {
                MonsterMinCount = minCount;
                MonsterMaxCount = maxCount;
                respawn = rand.Next(1, 4);
            }
        }

        public void UpdatePlayerInfo(Player player)
        {
            Console.WriteLine(GetPlayerInfo(player));
        }
        public void UpdateMonsterInfo(Monster monster)
        {
            Console.WriteLine(GetMonsterInfo(monster));
        }

        internal static void OnDamage(Monster monsters, float damage) // 몬스터가 데미지 받았을때
        {
            monsters.Health -= damage;
            Console.WriteLine($"{monsters.Name}은(는) {damage}의 데미지를 입었습니다!");
        }

        public void GetDamage(Player player, float damage) // 플레이어가 데미지 받았을때
        {
            Player.player.hp -= damage;
            Console.WriteLine($"{Player.player.name}은(는) {damage}의 데미지를 입었습니다!");
        }

        public void Criticaln(int originDamage, bool useSkill)
        {
            bool isCritical = false;
            bool isDodged = false;
            int calculatedDamage = originDamage;

            // 치명타 계산
            Random random = new Random();
            double chance = random.NextDouble();
            if (chance <= 0.15) // 15% 의 확률로 치명타 발생
            {
                isCritical = true;
                calculatedDamage = (int)(originDamage * 1.6f); // 160% 데미지
            }

            // 회피 계산
            chance = random.NextDouble();
            if (chance <= 0.1 && !useSkill) // 스킬공격이 아닐 시, 10% 의 확률로 회피
            {
                isDodged = true;
                calculatedDamage = 0; // 0의 데미지
            }
        }

        public bool CheckIsDead(List<Monster> monsters) // 처치 여부
        {
            if (Player.player.hp <= 0)
            {
                Console.WriteLine("플레이어가 전투에서 패배했습니다...");
                return true;
            }

            bool allMonstersDead = monsters.All(m => m.Health <= 0);
            if (allMonstersDead)
            {
                Console.WriteLine("모든 몬스터를 처치하여 전투에서 승리했습니다!");
                return true;
            }

            return false;
        }

        public void ClearReward() // 클리어 보상
        {
            // 리워드 설정
            Game.currentStageReward.isClear = true;
            Game.currentStageReward.stageNumber = DungeonLevel;
            Game.currentStageReward.exp = DungeonLevel * 20;
            Game.currentStageReward.gold = 100; // 골드획득
        }

        public void FailReward() // 실패
        {
            // 실패 시 전리품 미지급
            Game.currentStageReward.isClear = false;
            Game.currentStageReward.stageNumber = DungeonLevel;
        }

        public void Stage1()
        {
            Console.Clear();
            Console.WriteLine("==========================================================");
            Color.ChangeTextColor(Colors.YELLOW, "", "Stage 1. 모험의 시작\n");
            Console.WriteLine("==========================================================");
            Console.WriteLine("\n[몬스터 정보]");

            Monster[] monsters = Monsters();

            foreach (var monster in monsters)
            {
                Console.WriteLine(GetMonsterInfo(monster));
            }

            Console.WriteLine("\n[플레이어 정보]");
            Console.WriteLine(GetPlayerInfo(Player.player));
            int currentMonsterIndex = 0;
            int monsterNumber;

            while (Player.player.hp > 0 && monsters.Any(m => m.Health > 0))
            {
                // 플레이어의 차례
                Console.WriteLine("\n[플레이어의 차례]");
                Console.WriteLine("1. 일반 공격");
                Console.WriteLine("2. 스킬 공격");
                Console.WriteLine("3. 아이템 사용");
                Console.Write(">>");

                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "1":
                        // 일반 공격
                        Console.WriteLine("플레이어가 일반 공격을 시전합니다.");
                        Console.Write("공격할 몬스터 번호를 입력하세요: ");
                        monsterNumber = int.Parse(Console.ReadLine());

                        Monster targetMonster = monsters[monsterNumber - 1];
                        float damageToMonster = Player.player.baseAtk + Player.player.addAtk;
                        OnDamage(targetMonster, damageToMonster);

                        // 몬스터가 죽었는지 확인
                        if (targetMonster.Health <= 0)
                        {
                            Console.WriteLine($"{targetMonster.Name}을(를) 처치했습니다!");
                        }
                        else
                        {
                            // 몬스터의 공격
                            Console.WriteLine($"{targetMonster.Name}이(가) 플레이어를 공격합니다.");
                            float damageToPlayer = targetMonster.Atk;
                            GetDamage(Player.player, damageToPlayer);
                        }
                        break;

                    case "2":
                        // 스킬 공격
                        Console.WriteLine("플레이어가 스킬 공격을 시전합니다.");
                        Console.Write("사용할 스킬 번호를 입력하세요: ");
                        int skillNumber = int.Parse(Console.ReadLine());
                        Console.Write("공격할 몬스터 번호를 입력하세요: ");
                        monsterNumber = int.Parse(Console.ReadLine());
                        Skill.useSkill(skillNumber, monsterNumber, monsters);
                        break;

                    case "3":
                        // 아이템 사용
                        Console.WriteLine("플레이어가 아이템을 사용합니다.");
                        break;

                    default:
                        Console.WriteLine("올바른 선택지를 입력하세요.");
                        break;
                }

                // 몬스터의 차례
                foreach (var monster in monsters)
                {
                    if (monster.Health > 0)
                    {
                        Console.WriteLine($"\n[{monster.Name}의 차례]");
                        float damageToPlayer = monster.Atk;
                        GetDamage(Player.player, damageToPlayer);
                    }
                }
                Console.ReadKey();
                Console.Clear();
                Console.Out.Flush();
                Console.WriteLine("\n[몬스터 정보]");
                foreach (var monster in monsters)
                {
                    UpdateMonsterInfo(monster);
                }
                Console.WriteLine("\n[플레이어 정보]");
                UpdatePlayerInfo(Player.player);
            }
        }

        public void Stage2()
        {
            Console.Clear();
            Console.WriteLine("==========================================================");
            Color.ChangeTextColor(Colors.YELLOW, "", "Stage 2. 용의 둥지\n");
            Console.WriteLine("==========================================================");
            Console.WriteLine("\n[몬스터 정보]");

        }

        public void Stage3()
        {
            Console.Clear();
            Console.WriteLine("==========================================================");
            Color.ChangeTextColor(Colors.YELLOW, "", "Stage 3. 몰라 \n");
            Console.WriteLine("==========================================================");
            Console.WriteLine("\n[몬스터 정보]");

        }

        public void Stage4()
        {
            Console.Clear();
            Console.WriteLine("==========================================================");
            Color.ChangeTextColor(Colors.YELLOW, "", "Stage 4. 몰라222\n");
            Console.WriteLine("==========================================================");
            Console.WriteLine("\n[몬스터 정보]");

        }

        public void Stage5()
        {
            Console.Clear();
            Console.WriteLine("==========================================================");
            Color.ChangeTextColor(Colors.YELLOW, "", "Stage 5. 문영오와 한효승의 안식처\n");
            Console.WriteLine("==========================================================");
            Console.WriteLine("\n[몬스터 정보]");
        }
    }
}