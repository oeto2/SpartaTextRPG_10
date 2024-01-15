using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaTextRPG.DataClass.Quest
{
    //진행중인 퀘스트
    public partial class QuestList
    {
        //진행중인 퀘스트 목록 보여주기
        public void ShowCurQuestList()
        {
            //시작 가능한 퀘스트, 완료된 퀘스트 체크
            QuestList.instance.CheckStQuest();
            QuestList.instance.CheckCompQuest();

            curQuestNum = 0;

            //퀘스트 번호
            int questNum = 1;

            int row, col;

            //진행중인 퀘스트가 없을 경우
            if (questList.Find(x => x.isPossible && x.isOngoing && !x.isClear) == null)
                Color.ChangeTextColor(Colors.RED, "", "[진행중인 퀘스트가 없습니다]\n");

            //진행중인 퀘스트가 있을 경우
            else
            {
                Color.ChangeTextColor(Colors.RED, "", "[메인 퀘스트]\n");

                foreach (Quest _quest in questList)
                {
                    if (_quest.isOngoing && !_quest.isClear && _quest.type == QuestType.Main)
                    {
                        curQuestNum++;

                        //퀘스트 완료시 (글자색 변경)
                        if(_quest.isComplete)
                        {
                            (row, col) = Console.GetCursorPosition();
                            Color.ChangeTextColor(Colors.YELLOW, "", $"{questNum++}.");
                            //Console.Write($"{qeustNum++}.");
                            Console.SetCursorPosition(3, col);

                            Color.ChangeTextColor(Colors.YELLOW, "", $"{_quest.name} ");
                            //Console.Write($"{_quest.name} ");
                            Console.SetCursorPosition(22, col);
                            Console.Write('|');

                            Color.ChangeTextColor(Colors.YELLOW, "", $" {_quest.info}");
                            //Console.Write($" {_quest.info}");
                            Console.SetCursorPosition(66, col);
                            Console.Write('|');


                            Color.ChangeTextColor(Colors.YELLOW, "", $" {_quest.gold}".PadLeft(6));
                            //Console.Write($" {_quest.gold}".PadLeft(6));
                            Console.SetCursorPosition(74, col);
                            Color.ChangeTextColor(Colors.YELLOW, "", "G");
                            //Console.Write("G");
                            Console.SetCursorPosition(76, col);
                            Console.Write('|');

                            Color.ChangeTextColor(Colors.YELLOW, "", $" {_quest.exp}".PadLeft(6));
                            //Console.Write($" {_quest.exp}".PadLeft(5));
                            Console.SetCursorPosition(84, col);
                            Color.ChangeTextColor(Colors.YELLOW, "", "EXP   [완료]");
                            //Console.Write("EXP ");
                            Console.WriteLine("");
                        }

                        //기본
                        else
                        {
                            (row, col) = Console.GetCursorPosition();
                            Color.ChangeTextColor(Colors.YELLOW, "", $"{questNum++}.");
                            //Console.Write($"{qeustNum++}.");
                            Console.SetCursorPosition(3, col);

                            //Color.ChangeTextColor(Colors.YELLOW, "", $"{_quest.name} ");
                            Console.Write($"{_quest.name} ");
                            Console.SetCursorPosition(22, col);
                            Console.Write('|');

                            //Color.ChangeTextColor(Colors.YELLOW, "", $" {_quest.info}");
                            Console.Write($" {_quest.info}");
                            Console.SetCursorPosition(66, col);
                            Console.Write('|');


                            //Color.ChangeTextColor(Colors.YELLOW, "", $" {_quest.gold}".PadLeft(6));
                            Console.Write($" {_quest.gold}".PadLeft(6));
                            Console.SetCursorPosition(74, col);
                            //Color.ChangeTextColor(Colors.YELLOW, "", "G");
                            Console.Write("G");
                            Console.SetCursorPosition(76, col);
                            Console.Write('|');

                            //Color.ChangeTextColor(Colors.YELLOW, "", $" {_quest.exp}".PadLeft(5));
                            Console.Write($" {_quest.exp}".PadLeft(6));
                            Console.SetCursorPosition(84, col);
                            //Color.ChangeTextColor(Colors.YELLOW, "", "EXP ");
                            Console.Write("EXP   [미완료]");
                            //Console.Write("EXP");
                            Console.WriteLine("");
                        }
                    }
                }

                
                Color.ChangeTextColor(Colors.MAGENTA, "", "\n서브 퀘스트]\n");

                foreach (Quest _quest in questList)
                {
                    if (_quest.isOngoing && !_quest.isClear && _quest.type == QuestType.Sub)
                    {
                        curQuestNum++;

                        //퀘스트 완료시 (글자색 변경)
                        if (_quest.isComplete)
                        {
                            (row, col) = Console.GetCursorPosition();
                            Color.ChangeTextColor(Colors.YELLOW, "", $"{questNum++}.");
                            //Console.Write($"{qeustNum++}.");
                            Console.SetCursorPosition(3, col);

                            Color.ChangeTextColor(Colors.YELLOW, "", $"{_quest.name} ");
                            //Console.Write($"{_quest.name} ");
                            Console.SetCursorPosition(22, col);
                            Console.Write('|');

                            Color.ChangeTextColor(Colors.YELLOW, "", $" {_quest.info}");
                            //Console.Write($" {_quest.info}");
                            Console.SetCursorPosition(66, col);
                            Console.Write('|');


                            Color.ChangeTextColor(Colors.YELLOW, "", $" {_quest.gold}".PadLeft(6));
                            //Console.Write($" {_quest.gold}".PadLeft(6));
                            Console.SetCursorPosition(74, col);
                            Color.ChangeTextColor(Colors.YELLOW, "", "G");
                            //Console.Write("G");
                            Console.SetCursorPosition(76, col);
                            Console.Write('|');

                            Color.ChangeTextColor(Colors.YELLOW, "", $" {_quest.exp}".PadLeft(6));
                            //Console.Write($" {_quest.exp}".PadLeft(5));
                            Console.SetCursorPosition(84, col);
                            Color.ChangeTextColor(Colors.YELLOW, "", "EXP   [완료]");
                            //Console.Write("EXP ");
                            Console.WriteLine("");
                        }

                        //기본
                        else
                        {
                            (row, col) = Console.GetCursorPosition();
                            Color.ChangeTextColor(Colors.YELLOW, "", $"{questNum++}.");
                            //Console.Write($"{qeustNum++}.");
                            Console.SetCursorPosition(3, col);

                            //Color.ChangeTextColor(Colors.YELLOW, "", $"{_quest.name} ");
                            Console.Write($"{_quest.name} ");
                            Console.SetCursorPosition(22, col);
                            Console.Write('|');

                            //Color.ChangeTextColor(Colors.YELLOW, "", $" {_quest.info}");
                            Console.Write($" {_quest.info}");
                            Console.SetCursorPosition(66, col);
                            Console.Write('|');


                            //Color.ChangeTextColor(Colors.YELLOW, "", $" {_quest.gold}".PadLeft(6));
                            Console.Write($" {_quest.gold}".PadLeft(6));
                            Console.SetCursorPosition(74, col);
                            //Color.ChangeTextColor(Colors.YELLOW, "", "G");
                            Console.Write("G");
                            Console.SetCursorPosition(76, col);
                            Console.Write('|');

                            //Color.ChangeTextColor(Colors.YELLOW, "", $" {_quest.exp}".PadLeft(5));
                            Console.Write($" {_quest.exp}".PadLeft(6));
                            Console.SetCursorPosition(84, col);
                            //Color.ChangeTextColor(Colors.YELLOW, "", "EXP ");
                            Console.Write("EXP   [미완료]");
                            //Console.Write("EXP");
                            Console.WriteLine("");
                        }
                    }
                }
            }
        }
    }
}
