using System.Diagnostics;

namespace SpartaTextRPG
{
    internal class Program
    {
        public static Scene scene;
        static void Main(string[] args)
        {
            Title.instance.StartTitle();
            Item.Instance.ItemInit();
            Skill.instance.makeSkill();
            Console.Clear();
            while (true)
            {
                switch (scene)
                {
                    case Scene.mainScene:
                        GameManager.instance.MainGameScene();
                        break;
                    case Scene.playerState:
                        Console.Clear();
                        State.instance.Status();
                        break;
                    case Scene.inventory:
                        Inventory.Instance.ShowInvenPage();
                        break;
                    case Scene.shop:
                        Shop.Instance.ShowShopPage();
                        break;
                    case Scene.dungeon:
                        Dungeon.instance.DungeonEntrance();
                        break;
                    case Scene.rest:
                        GameManager.instance.Rest();
                        break;
                    case Scene.fishing:
                        Fishing.instance.StartFishing();
                        break;
                    case Scene.guild:
                        Guild.instance.ShowGuildPage();
                        break;
                }
            }
        }
    }
}
