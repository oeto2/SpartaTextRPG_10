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
        public static void useSkill(int skillnum, int monsternum, Monster[] monsters)
        {
            float damage = Skills.myskills[skillnum].damage * (Player.player.baseAtk + Player.player.addAtk);
            Math.Truncate(damage);
            Player.player.hp += Skills.myskills[skillnum].hp;
            Player.player.mp += Skills.myskills[skillnum].mp;

            Monster targetMonster = monsters[monsternum];
            Battle.OnDamage(targetMonster, damage);
        }
    }
}
