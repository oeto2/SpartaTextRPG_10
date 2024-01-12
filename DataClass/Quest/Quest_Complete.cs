using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaTextRPG.DataClass.Quest
{
    //완료한 퀘스트
    public partial class QuestList
    {
        //퀘스트 완료 조건
        public void CheckCompQuest()
        {
            CheckStQuest();

            // 1. 1차 전직
            if (questList[0].isOngoing && (int)Player.player.job != 0)
                questList[0].isComplete = true;

            // 2. 2차 전직
            if (questList[1].isOngoing && 4 < (int)Player.player.job)
                questList[1].isComplete = true;

            // 3. 장착 해보기
            if (questList[2].isOngoing && (Player.player.weapon != 0 || Player.player.armor != 0))
                questList[2].isComplete = true;

            // 4. 던전 입장하기
            if (questList[3].isOngoing && QuestBool.enterDungeon)
                questList[3].isComplete = true;

            // 4. 휴식하기
            if (questList[4].isOngoing && QuestBool.isRest)
                questList[4].isComplete = true;

            // 5.Stage.2 던전 클리어
            if (questList[5].isOngoing && QuestBool.dungeonClear[2])
                questList[5].isComplete = true;

            // 6.포션아이템 사용
            if (questList[6].isOngoing && QuestBool.usePotion)
                questList[6].isComplete = true;
        }

        //완료한 퀘스트 목록 보여주기
        public void ShowCompQuestList()
        {
            compQuestNum = 0;

            //퀘스트 번호
            int questNum = 1;

            int row, col;

            //완료한 퀘스트가 있는지 확인
            CheckCompQuest();

            //완료할 수 있는 퀘스트가 없을 경우
            if (questList.Find(x => x.isOngoing && x.isComplete && !x.isClear) == null)
                Color.ChangeTextColor(Colors.RED, "", "[완료할 수 있는 퀘스트가 없습니다]\n");

            //완료할 수 있는 퀘스트가 있을 경우
            else
            {
                Color.ChangeTextColor(Colors.YELLOW, "", "[완료 가능한 퀘스트]\n");

                foreach (Quest _quest in questList)
                {
                    if (_quest.isComplete && !_quest.isClear)
                    {
                        ++compQuestNum;

                        (row, col) = Console.GetCursorPosition();
                        Color.ChangeTextColor(Colors.YELLOW, "", $"{questNum++}.");
                        //Console.Write($"{qeustNum++}.");
                        Console.SetCursorPosition(3, col);

                        Color.ChangeTextColor(Colors.YELLOW, "", $"{_quest.name} ");
                        //Console.Write($"{_quest.name} ");
                        Console.SetCursorPosition(18, col);
                        Console.Write('|');

                        Color.ChangeTextColor(Colors.YELLOW, "", $" {_quest.info}");
                        //Console.Write($" {_quest.info}");
                        Console.SetCursorPosition(60, col);
                        Console.Write('|');


                        Color.ChangeTextColor(Colors.YELLOW, "", $" {_quest.gold}".PadLeft(6));
                        //Console.Write($" {_quest.gold}".PadLeft(6));
                        Console.SetCursorPosition(68, col);
                        Color.ChangeTextColor(Colors.YELLOW, "", "G");
                        //Console.Write("G");
                        Console.SetCursorPosition(70, col);
                        Console.Write('|');

                        Color.ChangeTextColor(Colors.YELLOW, "", $" {_quest.exp}".PadLeft(5));
                        //Console.Write($" {_quest.exp}".PadLeft(5));
                        Console.SetCursorPosition(77, col);
                        Color.ChangeTextColor(Colors.YELLOW, "", "EXP   ");
                        if (_quest.type == QuestType.Main)
                            Color.ChangeTextColor(Colors.RED, "", "[메인]");
                        else if (_quest.type == QuestType.Sub)
                            Color.ChangeTextColor(Colors.MAGENTA, "", "[서브]");
                        //Console.Write("EXP ");
                        Console.WriteLine("");
                    }
                }
            }
        }
    }
}
