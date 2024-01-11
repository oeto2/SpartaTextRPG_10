using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SpartaTextRPG
{
    enum InvenScene
    {
        NORMAL,
        MANAGE,
        ENFORCE
    }
    internal class Inventory
    {
        public static Inventory _instance;

        public static Inventory Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Inventory();
                }
                return _instance;
            }
        }

        InvenScene scene = InvenScene.NORMAL;

        int row, col;

        public List<int> ownEquipCount = new List<int>();
        public List<int> ownConsumCount = new List<int>();
        public List<int> ownFishCount = new List<int>();

        // 장비 장착 / 해제
        void EquipOnOff(int index)
        {
            // 범위 초과 시 리턴
            if (ownEquipCount.Count - 1 < index)
            {
                return;
            }

            int equip = ownEquipCount[index];

            switch (Item.Instance.equipItems[equip].type)
            {
                case ItemType.WEAPON:
                    // 해제
                    if (Item.Instance.equipItems[equip].id == Player.player.weapon)
                    {
                        Item.Instance.equipItems[equip].SubItemStat();
                        Player.player.weapon = 0;
                    }
                    // 장착
                    else
                    {
                        Player.player.weapon = Item.Instance.equipItems[equip].id;
                        Item.Instance.equipItems[equip].AddItemStat();
                    }
                    break;

                case ItemType.ARMOR:
                    // 해제
                    if (Item.Instance.equipItems[equip].id == Player.player.armor)
                    {
                        Item.Instance.equipItems[equip].SubItemStat();
                        Player.player.armor = 0;
                    }
                    // 장착
                    else
                    {
                        Player.player.armor = Item.Instance.equipItems[equip].id;
                        Item.Instance.equipItems[equip].AddItemStat();
                    }
                    break;

                default:
                    Console.WriteLine("장비 장착 / 해제 오류");
                    break;
            }
        }

        // 장비 강화
        void EnforceEquip(int index)
        {
            if (ownEquipCount.Count - 1 < index)
            {
                Color.ChangeTextColor(Colors.RED, "", "잘못된 입력입니다.", "\n");
                return;
            }

            int equip = ownEquipCount[index];
            // 강화비용(장비 기본가격의 절반)이 모자라다면
            if (Player.player.gold < Item.Instance.enforceInit[equip].cost / 2)
            {
                Color.ChangeTextColor(Colors.RED, "", "Gold 가 부족합니다.", "\n");
                return;
            }
            // 최대 강화수치라면
            else if (Item.Instance.equipItems[equip].enforce == 5)
            {
                Color.ChangeTextColor(Colors.RED, "", "더 이상 강화가 불가능합니다.", "\n");
                return;
            }
            // 강화 성공 
            Item.Instance.equipItems[equip].enforce++;
            Player.player.gold -= Item.Instance.enforceInit[equip].cost / 2;
            // 장비 능력치, 가격 변화 (추후 변동)
            Item.Instance.equipItems[equip].atk += 3;
            Item.Instance.equipItems[equip].cost += Item.Instance.enforceInit[equip].cost / 2;
        }

        // 보유 아이템 확인
        public void CheckOwnEquip()
        {
            ownEquipCount.Clear();
            for (int i = 0; i < Item.Instance.equipItems.Count; i++)
            {
                if (Item.Instance.equipItems[i].isOwn)
                {
                    ownEquipCount.Add(i);
                }
            }
            ownConsumCount.Clear();
            for (int i = 0; i < Item.Instance.consumItems.Count; i++)
            {
                if (Item.Instance.consumItems[i].count > 0)
                {
                    ownConsumCount.Add(i);
                }
            }
            ownFishCount.Clear();
            for (int i = 0; i < Item.Instance.fishList.Count; i++)
            {
                if (Item.Instance.fishList[i].count > 0)
                {
                    ownFishCount.Add(i);
                }
            }
        }

        // 인벤토리 장비 리스트 출력
        void ShowInvenEquipList()
        {
            Console.WriteLine("[아이템 목록]");

            for (int i = 0; i < ownEquipCount.Count; i++)
            {
                int index = ownEquipCount[i];

                if (scene == InvenScene.NORMAL)
                {
                    Console.Write("- ");
                }
                else if (scene != InvenScene.NORMAL)
                {
                    Color.ChangeTextColor(Colors.MAGENTA, "", Convert.ToString(i + 1), " ");
                }

                EquipItem equip = Item.Instance.equipItems[index];

                if (Player.player.weapon == equip.id || Player.player.armor == equip.id)
                {
                    Color.ChangeTextColor(Colors.MAGENTA, "[", "E", "]");
                }
                Console.Write(equip.name);

                if (equip.enforce > 0)
                {
                    Color.ChangeTextColor(Colors.MAGENTA, " +", Convert.ToString(equip.enforce));
                }

                (row, col) = Console.GetCursorPosition();
                Console.SetCursorPosition(24, col);

                Console.Write("| ");

                if (equip.atk != 0)
                {
                    Console.SetCursorPosition(26, col);
                    Color.ChangeTextColor(Colors.MAGENTA, "공격력 +", Convert.ToString(equip.atk), " ");
                }
                if (equip.def != 0)
                {
                    Console.SetCursorPosition(40, col);
                    Color.ChangeTextColor(Colors.MAGENTA, "방어력 +", Convert.ToString(equip.def), " ");
                }

                Console.SetCursorPosition(54, col);
                Console.Write("| ");
                Console.WriteLine(equip.info);

                if (scene == InvenScene.ENFORCE)
                {
                    Console.SetCursorPosition(94, col);
                    Console.Write("| ");
                    Color.ChangeTextColor(Colors.MAGENTA, "", Convert.ToString(Item.Instance.enforceInit[index].cost / 2), " G\n");
                }
            }

        }
        // 인벤토리 소비 리스트 출력
        void ShowInvenConsumList()
        {
            // 소비 목록
            for (int i = 0; i < Item.Instance.consumItems.Count; i++)
            {
                if (Item.Instance.consumItems[i].count > 0)
                {
                    ConsumItem consum = Item.Instance.consumItems[i];

                    Console.Write("- ");

                    Console.Write(consum.name);

                    (row, col) = Console.GetCursorPosition();
                    Console.SetCursorPosition(24, col);

                    Console.Write("| ");

                    if (consum.recoveryHp != 0)
                    {
                        Console.SetCursorPosition(26, col);
                        Color.ChangeTextColor(Colors.MAGENTA, "HP회복량 +", Convert.ToString(consum.recoveryHp), " ");
                    }
                    if (consum.recoveryMp != 0)
                    {
                        Console.SetCursorPosition(40, col);
                        Color.ChangeTextColor(Colors.MAGENTA, "MP회복량 +", Convert.ToString(consum.recoveryMp), " ");
                    }

                    Console.SetCursorPosition(54, col);
                    Console.Write("| ");
                    Console.Write(consum.info);

                    Console.SetCursorPosition(84, col);
                    Color.ChangeTextColor(Colors.MAGENTA, "| ", Convert.ToString(consum.count), "개 소지\n");
                }
            }
        }
        // 인벤토리 물고기 출력
        void ShowInvenFish()
        {
            for (int i = 0; i < Item.Instance.fishList.Count; i++)
            {
                if (Item.Instance.fishList[i].count > 0)
                {
                    Fish fish = Item.Instance.fishList[i];

                    Console.Write("- ");

                    Console.Write(fish.name);

                    (row, col) = Console.GetCursorPosition();
                    Console.SetCursorPosition(24, col);

                    Console.Write($"| {fish.info}");

                    Console.SetCursorPosition(84, col);
                    Color.ChangeTextColor(Colors.MAGENTA, "| ", Convert.ToString(fish.count), "마리 소지\n");
                }
            }
        }

        public void ShowInvenPage()
        {
            CheckOwnEquip();
            // 기본 페이지
            if (scene == InvenScene.NORMAL)
            {
                Color.ChangeTextColor(Colors.YELLOW, "", "인벤토리", "\n");
                Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");

                ShowInvenEquipList();
                ShowInvenConsumList();
                ShowInvenFish();

                Console.WriteLine();
                Color.ChangeTextColor(Colors.MAGENTA, "", "1", ". 장착 관리\n");
                Color.ChangeTextColor(Colors.MAGENTA, "", "2", ". 장비 강화\n");
                Color.ChangeTextColor(Colors.RED, "", "0", ". 나가기\n\n");

                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.Write(">> ");

                string answer = Console.ReadLine();
                Console.Clear();

                switch (answer)
                {
                    case "1":
                        scene = InvenScene.MANAGE;
                        break;
                    case "2":
                        scene = InvenScene.ENFORCE;
                        break;
                    case "0":
                        Program.scene = Scene.mainScene;
                        break;
                    default:
                        Color.ChangeTextColor(Colors.RED, "", "잘못된 입력입니다.", "\n");
                        break;
                }
            }
            // 장착 관리 페이지
            else if (scene == InvenScene.MANAGE)
            {
                Color.ChangeTextColor(Colors.YELLOW, "", "인벤토리 - 장착 관리", "\n");
                Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");

                ShowInvenEquipList();

                Console.WriteLine();
                Color.ChangeTextColor(Colors.RED, "", "0", ". 나가기\n\n");

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
                        EquipOnOff(Convert.ToInt16(answer) - 1);
                        break;
                    case "0":
                        scene = InvenScene.NORMAL;
                        break;
                    default:
                        Color.ChangeTextColor(Colors.RED, "", "잘못된 입력입니다.", "\n");
                        break;
                }
            }
            // 장비 강화 페이지
            else
            {
                Color.ChangeTextColor(Colors.YELLOW, "", "인벤토리 - 장비 강화", "\n");
                Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");

                Console.WriteLine("보유 골드");
                Color.ChangeTextColor(Colors.MAGENTA, "", Convert.ToString(Player.player.gold), " G\n\n");

                ShowInvenEquipList();

                Console.WriteLine();
                Color.ChangeTextColor(Colors.RED, "", "0", ". 나가기\n\n");

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
                        EnforceEquip(Convert.ToInt16(answer) - 1);
                        break;
                    case "0":
                        scene = InvenScene.NORMAL;
                        break;
                    default:
                        Color.ChangeTextColor(Colors.RED, "", "잘못된 입력입니다.", "\n");
                        break;
                }
            }

        }
    }
}
