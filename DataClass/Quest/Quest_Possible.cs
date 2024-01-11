using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaTextRPG.DataClass.Quest
{
    //시작 가능한 퀘스트
    public partial class QuestList
    {
        //시작 가능한 퀘스트 목록 보여주기
        public void ShowStQuestList()
        {
            int questNum = 1;

            int row, col;

            //시작 가능한 퀘스트가 없을 경우
            if (stQuestList.Count == 0)
            {
                Color.ChangeTextColor(Colors.RED, "", "***시작 가능한 퀘스트가 없습니다***\n");
            }

            //시작 가능한 퀘스트가 있을 경우
            else
            {
                //메인 퀘스트
                Color.ChangeTextColor(Colors.RED, "", "[메인 퀘스트]\n");
                foreach (Quest _quest in stQuestList)
                {
                    if (_quest.isPossible && _quest.type == 'M')
                    {
                        (row, col) = Console.GetCursorPosition();
                        Color.ChangeTextColor(Colors.YELLOW, "", $"{questNum++}.");
                        //Console.Write($"{qeustNum++}.");
                        Console.SetCursorPosition(3, col);

                        //Color.ChangeTextColor(Colors.YELLOW, "", $"{_quest.name} ");
                        Console.Write($"{_quest.name} ");
                        Console.SetCursorPosition(18, col);
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
                        Console.WriteLine("\n");
                    }
                }

                Color.ChangeTextColor(Colors.MAGENTA, "", "[서브 퀘스트]\n");
                //서브 퀘스트
                foreach (Quest _quest in stQuestList)
                {
                    if (_quest.isPossible && _quest.type == 'S')
                    {
                        (row, col) = Console.GetCursorPosition();
                        Color.ChangeTextColor(Colors.YELLOW, "", $"{questNum++}.");
                        //Console.Write($"{qeustNum++}.");
                        Console.SetCursorPosition(3, col);

                        //Color.ChangeTextColor(Colors.YELLOW, "", $"{_quest.name} ");
                        Console.Write($"{_quest.name} ");
                        Console.SetCursorPosition(18, col);
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
