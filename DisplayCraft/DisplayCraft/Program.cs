using static DisplayCraft.Display;

namespace DisplayCraft
{
    public class Program
    {
        static void Main(string[] args)
        {
 
                Display display1 = new(width:50, height: 15, posX: 5, posY: 5, style: BorderStyle.Curved, background:ConsoleColor.Blue);
                display1.Borders();
                int[] posicoes = {1, 4, 7};
                string[] linhas = {"linha1", "teste da linha4", "essa linha78"};
                display1.SetLine(posicoes, linhas, Align.Middle);
                display1.Print();

                // Display d2 = new(width: 20, height: 5, posX: 7, posY: 7 );  
                // d2.Borders(); 

                // Display d3 = new(width: 30, height: 10, posX: 10, posY: 10,  style: BorderStyle.Block, background:ConsoleColor.Red, foreground:ConsoleColor.Green);  
                // d3.Borders(); 

                // Display d4 = new(width: 26, height: 6, posX: 12, posY: 12,  style: BorderStyle.SingleLine, background:ConsoleColor.Yellow, foreground:ConsoleColor.DarkMagenta);  
                // d4.Borders(); 


                Console.SetCursorPosition(0, 32);
             
        }

    }
}
