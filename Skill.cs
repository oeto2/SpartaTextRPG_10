using SpartaTextRPG.DataClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaTextRPG
{
    public class Skill
    {
        public static Skill instance = new Skill();
        public void makeSkill()
        {
            Skills.allskills.Add(Skills.hit);
            Skills.allskills.Add(Skills.smash);
            Skills.allskills.Add(Skills.luncky7);
            Skills.allskills.Add(Skills.frenzy);
            Skills.allskills.Add(Skills.bash);
            Skills.allskills.Add(Skills.requiem);
            Skills.allskills.Add(Skills.howling);
            Skills.allskills = Skills.allskills.Distinct().ToList();

            Skill.instance.getSkill();
        }
        //스킬 얻기
        public void getSkill()
        {

            foreach (var skill in Skills.allskills)
            {
                if (Player.player.job.ToString() == skill.job.ToString())
                {
                    Skills.myskills.Add(skill);
                }
            }
            Skills.myskills = Skills.myskills.Distinct().ToList();
        }

        //스킬 사용(원하는 스킬 인덱스 , 공격 몬스터 인덱스)
        //나중에 스플뎀 가능하면 넣을 예정

        public bool check = false;
        public static void useSkill(int skillnum, List<Monster> monsters)
        {
            int monsternum;

            bool res = false;
            while (!res)
            {
                Console.WriteLine("대상을 선택해주세요.");
                Console.Write(">>");
                string userInput = Console.ReadLine();
                int num;

                res = int.TryParse(userInput, out monsternum);
                if (res && monsters.Count >= monsternum && monsternum > 0) 
                { 
                    Monster targetMonster = monsters[monsternum - 1];
                    float damage = Skills.myskills[skillnum].damage * (Player.player.baseAtk + Player.player.addAtk - targetMonster.Def);
                    Math.Truncate(damage);
                    float damageToMonster = damage;
                    bool iscritical = false;
                    bool isdodge = false;

                    damageToMonster = Battle.instance.CriticalAttack(damageToMonster, ref iscritical);
                    damageToMonster = Battle.instance.DodgeAttack(damageToMonster, false, ref isdodge);

                    if (targetMonster.Health <= 0 && isdodge == false)
                    {
                        targetMonster.Health = 0;
                        Console.WriteLine("이미 죽은 몬스터에게는 데미지를 입힐 수 없습니다!\n");
                        res = false;
                    }
                    else if (Player.player.mp < -Skills.myskills[skillnum].mp)
                    {
                        Console.WriteLine("플레이어의 MP가 부족합니다.\n");
                        showSkill(monsters);
                    }
                    else if (Player.player.hp <= -Skills.myskills[skillnum].hp)
                    {
                        Console.WriteLine("플레이어의 HP가 부족합니다.\n");
                        showSkill(monsters);
                    }
                    else if (damageToMonster > 0)
                    {
                        targetMonster.Health -= damageToMonster;
                        Player.player.hp += Skills.myskills[skillnum].hp;
                        Player.player.mp += Skills.myskills[skillnum].mp;
                        Console.WriteLine($"{Player.player.name}이(가) {targetMonster.Name}에게 {Skills.myskills[skillnum].name}을 가했습니다! [데미지: {damageToMonster}]");
                        Console.ReadLine();
                        Console.WriteLine("");
                        if (targetMonster.Health <= 0)
                        {
                            Console.WriteLine($"{targetMonster.Name}이(가) 사망했습니다!");

                            // 몬스터가 다 죽었을 때
                            bool allMonstersDead = monsters.All(monster => monster.Health <= 0);
                            Console.ReadLine();

                            if (allMonstersDead == true)
                            {
                                Reward.Instance.ClearReward(1);
                                Skill.instance.check = true;
                            }

                        }
                    }
                    else if (isdodge)
                    {
                        Player.player.mp += Skills.myskills[skillnum].mp;
                    }
                    else
                    {
                        Color.ChangeTextColor(Colors.RED, "", "잘못된 입력입니다.", "\n");
                    }

                } else
                {
                    Console.WriteLine("잘못된 몬스터 번호입니다. 다시 입력해주세요.");
                    res = false;
                }

            }

            Battle.instance.MonsterTurn();
            Skill.instance.check = false;
        }

        public static bool showSkill(List<Monster> monsters)
        {
            int i = 1;
            foreach (var skill in Skills.myskills)
            {
                Color.ChangeTextColor(Colors.BLUE, "\n", $"{i}. {skill.name} ", "");
                if (skill.hp != 0)
                {
                    Console.Write($" - HP {-skill.hp} 사용\n");
                }
                if (skill.mp != 0)
                {
                    Console.Write($" - MP {-skill.mp} 사용\n");
                }
                i++;
            }
            Color.ChangeTextColor(Colors.RED, "\n", "0. 취소하기", "\n\n");

            bool res = false;
            while (!res)
            {
                Console.WriteLine("행동을 선택해주세요.");
                Console.Write(">>");
                string userInput = Console.ReadLine();
                int num;

                res = int.TryParse(userInput, out num);

                if (!res)
                {
                    Color.ChangeTextColor(Colors.RED, "", "잘못된 입력입니다.", "\n");
                }
                else if (num == 0)
                {

                    Battle.instance.PlayerTurn();
                    return false;
                }
                else if (Skills.myskills.Count >= num)
                {
                    useSkill(num - 1, monsters);
                    return Skill.instance.check;
                } else
                {
                    res = false;
                    Color.ChangeTextColor(Colors.RED, "", "잘못된 입력입니다.", "\n");
                }
            }
            return false;
        }
    }
}
