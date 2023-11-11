using static DisplayCraft.Display;

namespace DisplayCraft
{
    public class Program
    {
        static void Main(string[] args)
        {
 
                Display d1 = new Display(width:50, height: 15, posX: 5, posY: 5, style: BorderStyle.Curved, background:ConsoleColor.Blue);
                d1.Borders();

                Display d2 = new(width: 20, height: 5, posX: 7, posY: 7 );  
                d2.Borders(); 
             
        }

    }
}
