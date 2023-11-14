using DisplayCraft;
using static DisplayCraft.Display;

namespace ExampleNamespace
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.BufferWidth = Console.WindowWidth = 150; // apenas windows
            Console.BufferHeight = Console.WindowHeight = 40;

            Console.Clear();
            Display display1 = new(width: 80, height: 28, posX: 2, posY: 3, style: BorderStyle.Curved,
                                    background: ConsoleColor.Blue, foreground: ConsoleColor.Black);
            display1.Borders();
            string linha15 = "esse é o teste da linha 15";
            int[] posicoes = { 1, 4, 7, 15 };
            string[] linhas = { "linha1", "teste da linha4", "essa é a linha7", linha15 };
            display1.SetLine(posicoes, linhas, Align.Right);
            display1.SetLine(new int[]{2,8}, new string[]{"dos", "ocho"}, Align.Right);          
            display1.PrintAll();

            ProgressBar bar1 = new(display1, 50.0, 35, 6, 30, 3, showNumber: true, numberColor: ConsoleColor.White);
            bar1.PrintBar();

            ProgressBar bar2 = new(display1, percentage: 10.0, posX: 20, posY: 20, width: 10, 
                                   height: 1, barCharacter: '■', showNumber: true);
            bar2.PrintBar(ConsoleColor.DarkGreen, ConsoleColor.White);

            //Display d2 = new(width: 10, height: 4, posX: 38, posY: 4,
            //                background: ConsoleColor.DarkCyan, foreground: ConsoleColor.Gray);
            //d2.SetLine(3, "janela2");
            //d2.SetLine(4, "foo bar");
            //d2.PrintAll();

            //Display d3 = new(width: 30, height: 13, posX: 3, posY: 4,
            //                background: ConsoleColor.Gray, foreground: ConsoleColor.DarkGray);
            //d3.Borders(BorderStyle.Block);
            //string linha5 = "0123456789abcdef";
            //int[] d4posicoes = { 2, 3, 9, 10, 5 };
            //string[] d4linhas = { "2", "três", "IX", "(10/10)*10", linha5 };
            //d3.SetLine(d4posicoes, d4linhas, Align.MiddleRight);
            //d3.SetLine(11, "[]", Align.Center);
            //d3.SetLine(12, "that's all, folks!");
            //d3.PrintAll();

            //Display d4 = new(width: 37, height: 6, posX: 38, posY: 10);
            //d4.SetStyle(BorderStyle.Default);
            //d4.Borders();
            //d4.SetLine(1, "Alin. esquerda", Align.Left);
            //d4.SetLine(2, "Alin. direita", Align.Right);
            //d4.SetLine(3, "Alin. centro", Align.Center);
            //d4.SetLine(4, "Alin. meio esquerda", Align.MiddleLeft);
            //d4.SetLine(5, "Alin. meio direita", Align.MiddleRight);
            //d4.SetLine(6, "..................Adeus................", Align.Center);
            //d4.PrintAll();

            Console.SetCursorPosition(0, 32);
            Console.ReadLine();
        }

    }
}
