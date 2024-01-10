using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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

        // 장비 장착 / 해제
        void EquipOnOff(int index)
        {
            // 범위 초과 시 리턴
            if (ownEquipCount.Count - 1 < index)
            {
                return;
            }

            switch (Item.Instance.equipItems[index].type)
            {
                case ItemType.WEAPON:
                    if (Item.Instance.equipItems[index].id == Player.player.weapon)
                    {
                        Player.player.weapon = 0;
                    }
                    else
                    {
                        Player.player.weapon = Item.Instance.equipItems[index].id;
                    }
                    break;

                case ItemType.ARMOR:
                    if (Item.Instance.equipItems[index].id == Player.player.armor)
                    {
                        Player.player.armor = 0;
                    }
                    else
                    {
                        Player.player.armor = Item.Instance.equipItems[index].id;
                    }
                    break;

                default:
                    Console.WriteLine("장비 장착 / 해제 오류");
                    break;
            }
        }

        // 보유 장비 확인
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
        }

        // 인벤토리 장비 리스트 출력
        void ShowInvenEquipList()
        {
            Console.WriteLine("[아이템 목록]");
            // 장비 목록
            for (int i = 0; i < Item.Instance.equipItems.Count; i++)
            {
                if (Item.Instance.equipItems[i].isOwn)
                {
                    EquipItem equip = Item.Instance.equipItems[i];

                    if (scene == InvenScene.NORMAL)
                    {
                        Console.Write("- ");
                    }
                    else if (scene != InvenScene.NORMAL)
                    {
                        Color.ChangeTextColor(Colors.MAGENTA, "", Convert.ToString(i + 1), " ");
                    }

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

                    if (scene == InvenScene.NORMAL)
                    {
                        Console.Write("- ");
                    }
                    else if (scene == InvenScene.MANAGE)
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
                    Color.ChangeTextColor(Colors.MAGENTA, "| ", Convert.ToString(consum.count), "개 소지\n");
                }
            }
        }
        public void ShowInvenPage()
        {
            // 기본 페이지
            if (scene == InvenScene.NORMAL)
            {
                Color.ChangeTextColor(Colors.YELLOW, "", "인벤토리", "\n");
                Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");

                ShowInvenEquipList();
                ShowInvenConsumList();

                Console.WriteLine();
                Color.ChangeTextColor(Colors.MAGENTA, "", "1", ". 장착 관리\n");
                Color.ChangeTextColor(Colors.MAGENTA, "", "2", ". 장비 강화\n");
                Color.ChangeTextColor(Colors.MAGENTA, "", "0", ". 나가기\n\n");

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
                CheckOwnEquip();
                Color.ChangeTextColor(Colors.YELLOW, "", "인벤토리 - 장착 관리", "\n");
                Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");

                ShowInvenEquipList();

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
                CheckOwnEquip();
                Color.ChangeTextColor(Colors.YELLOW, "", "인벤토리 - 장비 강화", "\n");
                Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");

                ShowInvenEquipList();

                Console.WriteLine("미구현입니다~");
                Color.ChangeTextColor(Colors.MAGENTA, "", "0", ". 나가기\n\n");

                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.Write(">> ");

                string answer = Console.ReadLine();
                Console.Clear();

                switch (answer)
                {
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
