using SpartaTextRPG.DataClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SpartaTextRPG
{
    public class Guild
    {
        //싱글톤
        public static Guild instance = new Guild();

        //길드 페이지
        public void ShowGuildPage()
        {
            Console.Clear();
            Color.ChangeTextColor(Colors.YELLOW, "", "길드\n");
            Console.WriteLine("퀘스트를 확인할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("1. 새로운 퀘스트 받기");
            Console.WriteLine("2. 진행중인 퀘스트 확인");
            Console.WriteLine("3. 퀘스트 보상 받기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Color.ChangeTextColor(Colors.YELLOW, "", ">>");
            string input = Console.ReadLine();

            switch (input)
            {
                default:
                    break;

                //퀘스트 받기
                case "1":
                    ShowRequestPage();
                    break;

                //퀘스트 확인
                case "2":
                    break;

                //퀘스트 보상
                case "3":
                    break;
            }
        }

        //의뢰 페이지
        public void ShowRequestPage()
        {
            Console.Clear();
            Color.ChangeTextColor(Colors.YELLOW, "", "길드 - 퀘스트 받기\n");
            Console.WriteLine("새로운 퀘스트를 받을 수 있습니다.\n");

            //메인 퀘스트 목록 보여주기
            QuestList.instance.ShowMainQuest();
            
            Console.ReadLine();
        }
    }
}
