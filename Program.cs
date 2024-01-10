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
                        GameManager.MainGameScene();
                        break;
                    case Scene.playerState:
                        Console.Clear();
                        State.Status();
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

                        break;
                    case Scene.rest:
                        Console.Clear();
                        GameManager.Rest();
                        break;
                }
            }
        }
    }
}
