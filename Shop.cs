using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaTextRPG
{

    internal class Shop
    {
        enum Scene
        {
            NORMAL,
            BUY,
            SELL,

        }
        public static Shop _instance;

        public static Shop Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Shop();
                }
                return _instance;
            }
        }

        Scene scene = Scene.NORMAL;

        int row, col;

        // 아이템 리스트 출력 함수
        void ShopItemList()
        {
            Console.WriteLine("[아이템 목록]");
            for (int i = 0; i < Item.Instance.equipItems.Count; i++)
            {
                EquipItem equip = Item.Instance.equipItems[i];

                if (scene == Scene.NORMAL)
                {
                    Console.Write("- ");
                }
                else if (scene == Scene.BUY)
                {
                    Color.ChangeTextColor(Colors.MAGENTA, "", Convert.ToString(i + 1), " ");
                }
                Console.Write(equip.name);

                (row, col) = Console.GetCursorPosition();
                Console.SetCursorPosition(20, col);

                Console.Write("| ");

                if (equip.atk != 0)
                {
                    Console.SetCursorPosition(22, col);
                    Color.ChangeTextColor(Colors.MAGENTA, "공격력 +", Convert.ToString(equip.atk), " ");
                }
                if (equip.def != 0)
                {
                    Console.SetCursorPosition(36, col);
                    Color.ChangeTextColor(Colors.MAGENTA, "방어력 +", Convert.ToString(equip.def), " ");
                }

                Console.SetCursorPosition(50, col);
                Console.Write("| ");
                Console.Write(equip.info);

                Console.SetCursorPosition(80, col);
                Console.Write("| ");
                if (equip.isOwn)
                {
                    Console.WriteLine("구매완료");
                }
                else
                {
                    Color.ChangeTextColor(Colors.MAGENTA, "", Convert.ToString(equip.cost), "\n");
                }
            }

            for (int i = 0; i < Item.Instance.consumItems.Count; i++)
            {
                ConsumItem consum = Item.Instance.consumItems[i];

                if (scene == Scene.NORMAL)
                {
                    Console.Write("- ");
                }
                else if (scene == Scene.BUY)
                {
                    Color.ChangeTextColor(Colors.MAGENTA, "", Convert.ToString(i + 1 + Item.Instance.equipItems.Count), " ");
                }
                Console.Write(consum.name);

                (row, col) = Console.GetCursorPosition();
                Console.SetCursorPosition(20, col);

                Console.Write("| ");

                if (consum.recoveryHp != 0)
                {
                    Console.SetCursorPosition(22, col);
                    Color.ChangeTextColor(Colors.MAGENTA, "HP회복량 +", Convert.ToString(consum.recoveryHp), " ");
                }
                if (consum.recoveryMp != 0)
                {
                    Console.SetCursorPosition(36, col);
                    Color.ChangeTextColor(Colors.MAGENTA, "MP회복량 +", Convert.ToString(consum.recoveryMp), " ");
                }

                Console.SetCursorPosition(50, col);
                Console.Write("| ");
                Console.Write(consum.info);

                Console.SetCursorPosition(80, col);
                Console.Write("| ");
                Color.ChangeTextColor(Colors.MAGENTA, "", Convert.ToString(consum.cost), "\n");
            }
        }

        // 구매 기능
        void BuyItems(int index)
        {
            // 장비 구매
            if (index < Item.Instance.equipItems.Count)
            {
                if (Item.Instance.equipItems[index].isOwn)
                {
                    Color.ChangeTextColor(Colors.BLUE, "", "이미 구매한 아이템입니다.", "\n");
                }
                else
                {

                }
            }
            // 소모품 구매
            else
            {

            }
        }

        // 상점 출력
        public void ShowShopPage()
        {
            if (scene == Scene.NORMAL)
            {
                Color.ChangeTextColor(Colors.YELLOW, "", "상점", "\n");
                Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");

                Console.WriteLine("보유 골드");
                Color.ChangeTextColor(Colors.MAGENTA, "", Convert.ToString(10000), " G\n\n");

                ShopItemList();

                Console.WriteLine("");
                Console.WriteLine("1. 아이템 구매");
                Console.WriteLine("2. 아이템 판매");
                Console.WriteLine("0. 나가기\n");

                string answer = Console.ReadLine();
                Console.Clear();

                switch (answer)
                {
                    case "1":
                        scene = Scene.BUY;
                        break;
                    case "2":
                        scene = Scene.SELL;
                        break;
                    case "0":
                        Program.scene = SpartaTextRPG.Scene.mainScene;
                        break;
                    default:
                        Color.ChangeTextColor(Colors.RED, "", "잘못된 입력입니다.", "\n");
                        break;
                }
            }
            else if (scene == Scene.BUY)
            {
                Color.ChangeTextColor(Colors.YELLOW, "", "상점 - 아이템 구매", "\n");
                Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");

                Console.WriteLine("보유 골드");
                Color.ChangeTextColor(Colors.MAGENTA, "", Convert.ToString(10000), " G\n\n");

                ShopItemList();

                Console.WriteLine("");
                Console.WriteLine("0. 나가기\n");

                string answer = Console.ReadLine();
                Console.Clear();

                switch (answer)
                {
                    case "1":
                    case "2":
                    case "3":
                    case "4":
                    case "5":
                    case "6":
                    case "7":
                        // 구매
                        break;
                    case "0":
                        scene = Scene.NORMAL;
                        break;
                    default:
                        Color.ChangeTextColor(Colors.RED, "", "잘못된 입력입니다.", "\n");
                        break;
                }
            }
            else
            {
                // 판매 페이지
            }

        }

        
    }

}
