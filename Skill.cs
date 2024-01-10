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
        public void getSkill()
        {
            Skills.allskills.Add(Skills.smash);
            Skills.allskills.Add(Skills.luncky7);
            Skills.allskills.Add(Skills.frenzy);
            Skills.allskills.Add(Skills.bash);
            Skills.allskills.Add(Skills.requiem);
            Skills.allskills.Add(Skills.howling);

            foreach (var skill in Skills.allskills)
            {
                if (Player.player.job.ToString() == skill.job.ToString())
                {
                    Skills.myskills.Add(skill);
                    Skills.myskills = Skills.myskills.Distinct().ToList();
                }
            }
        }
    }
}
