using System.Diagnostics;

namespace SpartaTextRPG
{
    internal class Program
    {
        public static Scene scene;
        static void Main(string[] args)
        {
            Title.instance.StartTitle();
            while (true)
            {
                switch (scene)
                {
                    case Scene.mainScene:
                        Console.Clear();
                        GameManager.instance.MainGameScene();
                        break;
                    case Scene.playerState:
                        Console.Clear();
                        State.instance.Status();
                        break;

                    case Scene.inventory:
                        Console.Clear();

                        break;
                    case Scene.shop:
                        Console.Clear();
                        Shop.Instance.ShowShopPage();
                        break;
                    case Scene.dungeon:
                        Console.Clear();
                        Dungeon.Instance.DungeonEntrance();
                        break;
                    case Scene.rest:
                        Console.Clear();
                        GameManager.instance.Rest();
                        break;
                }
            }
        }
    }
}
