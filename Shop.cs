﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaTextRPG
{
    enum ShopScene
    {
        NORMAL,
        BUY,
        SELL,

    }
    internal class Shop
    {
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

        ShopScene scene = ShopScene.NORMAL;

        int row, col;

        // 아이템 리스트 출력 함수
        void ShopItemList()
        {
            Console.WriteLine("[아이템 목록]");
            for (int i = 0; i < Item.Instance.equipItems.Count; i++)
            {
                EquipItem equip = Item.Instance.equipItems[i];

                if (scene == ShopScene.NORMAL)
                {
                    Console.Write("- ");
                }
                else if (scene == ShopScene.BUY)
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
                    Color.ChangeTextColor(Colors.MAGENTA, "", Convert.ToString(equip.cost), " G\n");
                }
            }

            for (int i = 0; i < Item.Instance.consumItems.Count; i++)
            {
                ConsumItem consum = Item.Instance.consumItems[i];

                if (scene == ShopScene.NORMAL)
                {
                    Console.Write("- ");
                }
                else if (scene == ShopScene.BUY)
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
                Color.ChangeTextColor(Colors.MAGENTA, "", Convert.ToString(consum.cost), " G\n");
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
                else if (Player.player.gold < Item.Instance.equipItems[index].cost)
                {
                    Color.ChangeTextColor(Colors.RED, "", "Gold 가 부족합니다.", "\n");
                }
                else
                {
                    Player.player.gold -= Item.Instance.equipItems[index].cost;
                    Item.Instance.equipItems[index].isOwn = true;
                    Color.ChangeTextColor(Colors.BLUE, "", "구매를 완료했습니다.", "\n");
                }
            }
            // 소모품 구매
            else
            {
                int consumIndex = index - Item.Instance.equipItems.Count;
                if (Player.player.gold < Item.Instance.consumItems[consumIndex].cost)
                {
                    Color.ChangeTextColor(Colors.RED, "", "Gold 가 부족합니다.", "\n");
                }
                else
                {
                    Player.player.gold -= Item.Instance.consumItems[consumIndex].cost;
                    Item.Instance.consumItems[consumIndex].count++;
                    Color.ChangeTextColor(Colors.BLUE, "", "구매를 완료했습니다.", "\n");
                }

            }
        }
        // 판매 리스트 출력
        void ShowSellList()
        {
            Inventory.Instance.CheckOwnEquip();

            Console.WriteLine("[아이템 목록]");
            // 장비 목록
            for (int i = 0; i < Inventory.Instance.ownEquipCount.Count; i++)
            {
                int index = Inventory.Instance.ownEquipCount[i];

                Color.ChangeTextColor(Colors.MAGENTA, "", Convert.ToString(i + 1), " ");

                EquipItem equip = Item.Instance.equipItems[index];

                if (Player.player.weapon == equip.id)
                {
                    Color.ChangeTextColor(Colors.MAGENTA, "[", "E", "]");
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
                Console.WriteLine(equip.info);

                Console.SetCursorPosition(80, col);
                Console.Write("| ");
                
                Color.ChangeTextColor(Colors.MAGENTA, "", Convert.ToString(equip.cost), " G\n");
                
            }
            // 소비 목록
            for (int i = 0; i < Item.Instance.consumItems.Count; i++)
            {
                if (Item.Instance.consumItems[i].count > 0)
                {
                    ConsumItem consum = Item.Instance.consumItems[i];

                    Color.ChangeTextColor(Colors.MAGENTA, "", Convert.ToString(i + 1 + Inventory.Instance.ownEquipCount.Count), " ");

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
                    Color.ChangeTextColor(Colors.MAGENTA, "| ", Convert.ToString(consum.count), "개 소지\n");

                    Console.SetCursorPosition(94, col);
                    Console.Write("| ");
                    Color.ChangeTextColor(Colors.MAGENTA, "", Convert.ToString(consum.cost), " G\n");
                }
            }
        }

        // 판매 기능
        void SellItems(int index)
        {
            // 장비 판매
            if (Inventory.Instance.ownEquipCount.Count - 1 >= index)
            {
                int equip = Inventory.Instance.ownEquipCount[index];

                switch (Item.Instance.equipItems[equip].type)
                {
                    case ItemType.WEAPON:
                        if (Item.Instance.equipItems[equip].id == Player.player.weapon)
                        {
                            Player.player.weapon = 0;
                        }
                        Item.Instance.equipItems[equip].isOwn = false;
                        Player.player.gold += Item.Instance.equipItems[equip].cost * 85 / 100;
                        Color.ChangeTextColor(Colors.BLUE, "", Item.Instance.equipItems[equip].name + "을(를) 판매하였습니다.\n");
                        break;

                    case ItemType.ARMOR:
                        if (Item.Instance.equipItems[equip].id == Player.player.armor)
                        {
                            Player.player.armor = 0;
                        }
                        Item.Instance.equipItems[equip].isOwn = false;
                        Player.player.gold += Item.Instance.equipItems[equip].cost * 85 / 100;
                        Color.ChangeTextColor(Colors.BLUE, "", Item.Instance.equipItems[equip].name + "을(를) 판매하였습니다.\n");
                        break;

                    default:
                        Console.WriteLine("아이템 판매 기능 오류");
                        break;
                }
            }
            // 소모품 판매
            else if (Inventory.Instance.ownEquipCount.Count + Inventory.Instance.ownConsumCount.Count - 1 >= index)
            {

            }
            // 범위 초과 시 리턴
            else
            {
                return;
            }
        }
        // 상점 출력
        public void ShowShopPage()
        {
            if (scene == ShopScene.NORMAL)
            {
                Color.ChangeTextColor(Colors.YELLOW, "", "상점", "\n");
                Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");

                Console.WriteLine("보유 골드");
                Color.ChangeTextColor(Colors.MAGENTA, "", Convert.ToString(Player.player.gold), " G\n\n");

                ShopItemList();

                Console.WriteLine();
                Color.ChangeTextColor(Colors.MAGENTA, "", "1", ". 아이템 구매\n");
                Color.ChangeTextColor(Colors.MAGENTA, "", "2", ". 아이템 판매\n");
                Color.ChangeTextColor(Colors.MAGENTA, "", "0", ". 나가기\n\n");

                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.Write(">> ");

                string answer = Console.ReadLine();
                Console.Clear();

                switch (answer)
                {
                    case "1":
                        scene = ShopScene.BUY;
                        break;
                    case "2":
                        scene = ShopScene.SELL;
                        break;
                    case "0":
                        Program.scene = Scene.mainScene;
                        break;
                    default:
                        Color.ChangeTextColor(Colors.RED, "", "잘못된 입력입니다.", "\n");
                        break;
                }
            }
            // 구매 페이지
            else if (scene == ShopScene.BUY)
            {
                Color.ChangeTextColor(Colors.YELLOW, "", "상점 - 아이템 구매", "\n");
                Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");

                Console.WriteLine("보유 골드");
                Color.ChangeTextColor(Colors.MAGENTA, "", Convert.ToString(Player.player.gold), " G\n\n");

                ShopItemList();

                Console.WriteLine();
                Color.ChangeTextColor(Colors.MAGENTA, "", "0", ". 나가기\n\n");

                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.Write(">> ");

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
                    case "8":
                        // 구매
                        BuyItems(Convert.ToInt16(answer) - 1);
                        break;
                    case "0":
                        scene = ShopScene.NORMAL;
                        break;
                    default:
                        Color.ChangeTextColor(Colors.RED, "", "잘못된 입력입니다.", "\n");
                        break;
                }
            }
            // 판매 페이지
            else
            {
                Color.ChangeTextColor(Colors.YELLOW, "", "상점 - 아이템 판매", "\n");
                Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");

                Console.WriteLine("보유 골드");
                Color.ChangeTextColor(Colors.MAGENTA, "", Convert.ToString(Player.player.gold), " G\n\n");

                ShowSellList();

                Console.WriteLine();
                Color.ChangeTextColor(Colors.MAGENTA, "", "0", ". 나가기\n\n");

                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.Write(">> ");

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
                    case "8":
                    case "9":
                        // 판매
                        SellItems(Convert.ToInt16(answer) - 1);
                        break;
                    case "0":
                        scene = ShopScene.NORMAL;
                        break;
                    default:
                        Color.ChangeTextColor(Colors.RED, "", "잘못된 입력입니다.", "\n");
                        break;
                }
            }

        }


    }

}
