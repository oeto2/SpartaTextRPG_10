using SpartaTextRPG.DataClass.Quest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaTextRPG
{
    public class CurrentStageReward
    {
        public bool isClear = false;
        public int stageNumber;
        public int exp;
        public int gold;

        public CurrentStageReward(int _stageNumber, int _exp, int _gold)
        {
            stageNumber = _stageNumber;
            exp = _exp;
            gold = _gold;
        }
    }
    internal class Reward
    {
        public static Reward _instance;

        public static Reward Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Reward();
                }
                return _instance;
            }
        }

        List<CurrentStageReward> currentStageRewards = new List<CurrentStageReward>()
        {
            new CurrentStageReward(1, 100, 100),
            new CurrentStageReward(2, 200, 200),
            new CurrentStageReward(3, 300, 300),
            new CurrentStageReward(4, 400, 400),
            new CurrentStageReward(5, 500, 500),
        };


        public void ClearReward(int index)
        {
            Battle.isClear = true;   
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("================================================");
            Console.WriteLine("    전투 결과");
            Console.WriteLine("================================================");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("   ★☆Victory☆★\n\n");
            Console.ResetColor();
            Color.ChangeTextColor(Colors.YELLOW, "던전 ", Dungeon.instance.dungeonList[index - 1].name, " 공략을 성공하였습니다!\n\n");
            Color.ChangeTextColor(Colors.MAGENTA, "", Convert.ToString(Player.player.gold), " G  ->  ");
            Player.player.gold += currentStageRewards[index - 1].gold; // 골드획득
            Color.ChangeTextColor(Colors.MAGENTA, "", Convert.ToString(Player.player.gold), " G\n");
            // 리워드 설정
            Console.ReadLine(); 
            Console.Clear();
            currentStageRewards[index - 1].isClear = true;
            Player.player.GetExp(currentStageRewards[index - 1].exp);
            Program.scene = Scene.mainScene;
            QuestBool.dungeonClear = true;
        }

        public void FailReward(int index)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("================================================");
            Console.WriteLine("    전투 결과");
            Console.WriteLine("================================================");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("     You Lose\n\n");
            Console.ResetColor();
            Color.ChangeTextColor(Colors.RED, "던전 ", Dungeon.instance.dungeonList[index - 1].name, " 공략을 실패하였습니다.\n\n");
            Color.ChangeTextColor(Colors.RED, "", "        .---.\n");
            Color.ChangeTextColor(Colors.RED, "", "        |   |  \n");
            Color.ChangeTextColor(Colors.RED, "", "     ___|   |___\n");
            Color.ChangeTextColor(Colors.RED, "", "    [           ]  \n");
            Color.ChangeTextColor(Colors.RED, "", "    `---.   .---'\n");
            Color.ChangeTextColor(Colors.RED, "", "        |   | \n");
            Color.ChangeTextColor(Colors.RED, "", "        |   | \n");
            Color.ChangeTextColor(Colors.RED, "", "        |   |   \n");
            Color.ChangeTextColor(Colors.RED, "", "     _.-|   |-,_\n");
            Color.ChangeTextColor(Colors.RED, "", " .-\"`   `\"`'`   `\"-.\n");

            // 실패 시 전리품 미지급
            Console.ReadLine();
            Console.Clear();
            Program.scene = Scene.mainScene;
        }
    }
}
