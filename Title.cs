using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SpartaTextRPG
{

    internal class Title
    {
        public static Title instance = new Title();

        //잘못된 입력 감지
        private bool isWrong = false;

        //플레이어 정보
        string playerName;

        //타이틀 화면 시작
        public void StartTitle()
        {
            //메인 타이틀 화면
            ShowMainTitle();

            //이름 짓기
            switch (NamePlayer())
            {
                //아니요
                case 2:
                    NamePlayer();
                    break;
            }

            Console.Clear();
            Console.WriteLine("{0}님 환영합니다!", playerName);
        }

        //유저 이름 짓기
        private int NamePlayer()
        {
            //이름을 지었는지
            bool isName = false;

            //캐릭터 이름
            playerName = "";

            while (playerName.Length < 1 || playerName.Contains(" "))
            {
                Console.Clear();
                Console.WriteLine("스파르타 RPG에 오신 여러분 환영합니다.");
                Console.WriteLine("");
                Console.WriteLine("모험을 시작하기 앞서 캐릭터의 이름을 입력 해주세요.");
                if (CheckEmpty(playerName))
                    Console.WriteLine("******캐릭터 이름에는 공백이 포함될 수 없습니다.******");
                Console.Write(">>");
                playerName = Console.ReadLine();
            }

            while (!isName)
            {
                isName = false;

                Console.Clear();
                Console.WriteLine("캐릭터생성 - 이름");
                Console.WriteLine();
                Console.WriteLine("캐릭터의 이름을 {0}로 하시겠습니까?", playerName);
                Console.WriteLine();
                Console.WriteLine("1.예");
                Console.WriteLine("2.아니오");
                Console.WriteLine();
                if (isWrong)
                    Console.WriteLine("******잘못된 입력입니다.******");
                Console.Write(">>");

                string input = Console.ReadLine();

                switch (input)
                {
                    //예
                    case "1":
                        isWrong = false;
                        isName = true;
                        return 1;

                    //아니오
                    case "2":
                        isWrong = false;
                        return 2;

                    default:
                        isWrong = true;
                        break;
                }
            }
            return 1;
        }

        //입력에 공백이 있으면 true
        public static bool CheckEmpty(string _input)
        {
            if (_input.Contains(" "))
                return true;
            else
                return false;
        }

        //입력 체크
        public static int CheckIntInput(int _min, int _max)
        {
            //입력 값, Nul이면 99반환
            int input = 99;

            //입력값이 int인지
            bool isInt;

            do
            {
                isInt = int.TryParse(Console.ReadLine(), out input);

                //null값 감지
                if (input == 0)
                    return input;
            } while (!isInt || (input < _min && input > _max));

            //이상이 없으면 입력 값 반환
            return input;
        }

        //현재 줄만 지우기
        public static void clearCurrentLine()
        {
            string s = "\r";
            s += new string(' ', Console.CursorLeft);
            s += "\r";
            Console.Write(s);
        }

        //타이틀 보여주기
        private void ShowMainTitle()
        {
            string input = "";

            while(input != "1")
            {
                Console.Clear();
                Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@**^^\"\"~~~\"^@@^*@*@@**@@@@@@@@@@@@\r\n@@@@@@@@@@@@@*^^'\"~   , - ' '; ,@@b. '  -e@@@@@@@@@@@@\r\n@@@@@@@@*^\"~      . '     . ' ,@@@@(  e@*@@@@@@@@@@@@@\r\n@@@@@^~         .       .   ' @@@@@@, ~^@@@@@@@@@@@@@@\r\n@@@~ ,e**@@*e,  ,e**e, .    ' '@@@@@@e,  \"*@@@@@'^@@@@\r\n@',e@@@@@@@@@@ e@@@@@@       ' '*@@@@@@    @@@'   0@@@\r\n@@@@@@@@@@@@@@@@@@@@@',e,     ;  ~^*^'    ;^~   ' 0@@@\r\n@@@@@@@@@@@@@@@^\"\"^@@e@@@   .'           ,'   .'  @@@@\r\n@@@@@@@@@@@@@@'    '@@@@@ '         ,  ,e'  .    ;@@@@\r\n@@@@@@@@@@@@@' ,&&,  ^@*'     ,  .  i^\"@e, ,e@e  @@@@@\r\n@@@@@@@@@@@@' ,@@@@,          ;  ,& !,,@@@e@@@@ e@@@@@\r\n@@@@@,~*@@*' ,@@@@@@e,   ',   e^~^@,   ~'@@@@@@,@@@@@@\r\n@@@@@@, ~\" ,e@@@@@@@@@*e*@*  ,@e  @@\"\"@e,,@@@@@@@@@@@@\r\n@@@@@@@@ee@@@@@@@@@@@@@@@\" ,e@' ,e@' e@@@@@@@@@@@@@@@@\r\n@@@@@@@@@@@@@@@@@@@@@@@@\" ,@\" ,e@@e,,@@@@@@@@@@@@@@@@@\r\n@@@@@@@@@@@@@@@@@@@@@@@~ ,@@@,,0@@@@@@@@@@@@@@@@@@@@@@\r\n@@@@@@@@@@@@@@@@@@@@@@@@,,@@@@@@@@@@@@@@@@@@@@@@@@@@@@\r\n\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"");
                Console.WriteLine("  ____                   _          ____  ____   ____ ");
                Console.WriteLine(" / ___| _ __   __ _ _ __| |_ __ _  |  _ \\|  _ \\ / ___|");
                Console.WriteLine(" \\___ \\| '_ \\ / _` | '__| __/ _` | | |_) | |_) | |  _ ");
                Console.WriteLine("  ___) | |_) | (_| | |  | || (_| | |  _ <|  __/| |_| |");
                Console.WriteLine(" |____/| .__/ \\__,_|_|   \\__\\__,_| |_| \\_\\_|    \\____|");
                Console.WriteLine("       |_|                                            ");
                Console.WriteLine("======================================================");
                Console.WriteLine("                      모험의 시작                      ");
                Console.WriteLine("======================================================");
                Console.WriteLine("                     1. 새로하기                      ");
                Console.WriteLine("                     2. 이어하기                      ");
                Console.Write(">>");
                input = Console.ReadLine();
            }
        }
    }
}

