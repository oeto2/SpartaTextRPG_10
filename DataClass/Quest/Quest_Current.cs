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
            //퀘스트 번호
            int questNum = 1;

            int row, col;

            //진행중인 퀘스트가 없을 경우
            if (questList.Find(x => x.isPossible && x.isOngoing && !x.isComplete) == null)
                Color.ChangeTextColor(Colors.RED, "", "***진행중인 퀘스트가 없습니다***\n");

            //진행중인 퀘스트가 있을 경우
            else
            {
                Color.ChangeTextColor(Colors.YELLOW, "", "[진행중인 퀘스트]\n");

                foreach (Quest _quest in questList)
                {
                    if (_quest.isOngoing)
                    {
                        (row, col) = Console.GetCursorPosition();
                        Color.ChangeTextColor(Colors.YELLOW, "", $"{questNum++}.");
                        //Console.Write($"{qeustNum++}.");
                        Console.SetCursorPosition(3, col);

                        //Color.ChangeTextColor(Colors.YELLOW, "", $"{_quest.name} ");
                        Console.Write($"{_quest.name} ");
                        Console.SetCursorPosition(20, col);
                        Console.Write('|');

                        //Color.ChangeTextColor(Colors.YELLOW, "", $" {_quest.info}");
                        Console.Write($" {_quest.info}");
                        Console.SetCursorPosition(60, col);
                        Console.Write('|');


                        //Color.ChangeTextColor(Colors.YELLOW, "", $" {_quest.gold}".PadLeft(6));
                        Console.Write($" {_quest.gold}".PadLeft(6));
                        Console.SetCursorPosition(68, col);
                        //Color.ChangeTextColor(Colors.YELLOW, "", "G");
                        Console.Write("G");
                        Console.SetCursorPosition(70, col);
                        Console.Write('|');

                        //Color.ChangeTextColor(Colors.YELLOW, "", $" {_quest.exp}".PadLeft(5));
                        Console.Write($" {_quest.exp}".PadLeft(5));
                        Console.SetCursorPosition(77, col);
                        //Color.ChangeTextColor(Colors.YELLOW, "", "EXP ");
                        Console.Write("EXP ");
                        Console.WriteLine("");
                    }
                }
            }
        }
    }
}
