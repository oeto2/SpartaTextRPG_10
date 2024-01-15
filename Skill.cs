﻿using SpartaTextRPG.DataClass;
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
            Skills.allskills.Add(Skills.smash);
            Skills.allskills.Add(Skills.luncky7);
            Skills.allskills.Add(Skills.frenzy);
            Skills.allskills.Add(Skills.bash);
            Skills.allskills.Add(Skills.requiem);
            Skills.allskills.Add(Skills.howling);
            Skills.allskills = Skills.allskills.Distinct().ToList();
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
        public static void useSkill(int skillnum,  List<Monster> monsters)
        {
            int monsternum;
            bool res = false;
            while (!res)
            {
                Console.WriteLine("공격할 몬스터를 선택해주세요.");
                Console.Write(">>");
                string userInput = Console.ReadLine();
                int num;

                res = int.TryParse(userInput, out monsternum);

                if (!res)
                {
                    Color.ChangeTextColor(Colors.RED, "", "잘못된 입력입니다.", "\n");
                }
                else if (monsters[monsternum - 1] != null)
                {
                    res = true;
                    float damage = Skills.myskills[skillnum].damage * (Player.player.baseAtk + Player.player.addAtk);
                    Math.Truncate(damage);
                    Player.player.hp += Skills.myskills[skillnum].hp;
                    Player.player.mp += Skills.myskills[skillnum].mp;

                    monsters[monsternum - 1].Health -= damage;

                }
                else if (monsternum == 0)
                {
                    res = true;
                }
            }
            // Battle.OnDamage(targetMonster, damage);

            Battle.instance.BattleScene();
        }

        public static void showSkill(List<Monster> monsters)
        {
            int i = 1;
            foreach (var skill in Skills.myskills)
            {
                Color.ChangeTextColor(Colors.BLUE, "\n", $"{i}. {skill.name} ","");
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
                }else if(Skills.myskills[num - 1] != null)
                {
                    useSkill(num - 1, monsters);
                } else if(num  ==  0)
                {
                    Battle.instance.BattleScene();
                }
            }
        }
    }
}
