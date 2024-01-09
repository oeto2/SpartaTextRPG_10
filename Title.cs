using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SpartaTextRPG
{
    internal class Title
    {
        public static Title instance = new Title();

        //타이틀 화면 시작
        public void StartTitle()
        {
            //메인 타이틀 화면
            ShowMainTitle();

            

        }

        //유저 이름 짓기
        private string MakePlayerName()
        {
            //이름을 지었는지
            bool isName = false;

            //캐릭터 이름
            string name;
            Console.Clear();
            Console.WriteLine("스파르타 RPG에 오신 여러분 환영합니다.");
            Console.WriteLine("");
            Console.WriteLine("모험을 시작하기 앞서 캐릭터의 이름을 입력 해주세요.");
            Console.Write(">>");
            name = Console.ReadLine();

            Console.Clear();
            Console.WriteLine("캐릭터생성 - 이름");
            Console.WriteLine();
            Console.WriteLine("캐릭터의 이름을 {0}로 하시겠습니까?", name);
            Console.WriteLine();
            Console.WriteLine("1.예");
            Console.WriteLine("2.아니오");
            Console.WriteLine();
            Console.Write(">>");
            string input = Console.ReadLine();
        }


        //타이틀 보여주기
        private void ShowMainTitle()
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
            Console.WriteLine("                 PRESS ANYKEY TO START                ");
            Console.CursorVisible = false;
            Console.ReadKey();
            Console.CursorVisible = true;
        }
    }
}
