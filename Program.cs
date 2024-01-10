using System.Diagnostics;

namespace SpartaTextRPG
{
    internal class Program
    {
        public static Scene scene;
        static void Main(string[] args)
        {
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

                        break;

                    case Scene.inventory:
                        Console.Clear();

                        break;
                    case Scene.shop:
                        Console.Clear();

                        break;
                    case Scene.Dungeon:
                        Console.Clear();

                        break;
                    case Scene.rest:
                        Console.Clear();

                        break;
                }
            }
        }
    }
}
