using System;
using static System.Console;
using ANSIConsole;

namespace DisplayCraft
{
    internal class ProgressBar
    {
        internal ProgressBar(Display screen, int posY, int height, bool showNumber) 
        {
            this.Screen = screen;
    this.PosY = posY;
    this.Height = height;
    this.ShowNumber = showNumber;
   
        }
                public Display Screen { get; set; }
        public int PosX { get; set; }
        public int PosY { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        private int Percentage { get; set; }
        private bool ShowNumber { get; set; } 
        private char BarCharacter { get; set; }        
        private ConsoleColor BarColor {get; set;}  
        private ConsoleColor BarBackground {get; set;}   
        
        private ConsoleColor NumberColor;
        private ConsoleColor NumberBackground;

        public ProgressBar(Display display, int percentage)
        {
            Screen = display;
            Percentage = percentage;
            PosX = Screen.PosX;
            PosY = Screen.PosY;
            Width = 10;
            Height = 1;
            BarCharacter = '█';
            ShowNumber = true;            
        }

        public ProgressBar(Display display, int percentage, int posX, int posY, int width = 10, int height = 1, 
                           char barCharacter = '█', bool showNumber = true, ConsoleColor barBackground = ConsoleColor.DarkRed, 
                           ConsoleColor barColor = ConsoleColor.DarkGreen, ConsoleColor numberColor = ConsoleColor.Black, 
                           ConsoleColor numberBackground = ConsoleColor.Black)
        {
            Screen = display;
            Percentage = percentage;
            PosX = posX;
            PosY = posY;
            Width = width;
            Height = height;
            BarCharacter = barCharacter;
            ShowNumber = showNumber;
            BarColor = barColor;
            BarBackground = barBackground;
            NumberColor = numberColor;
            NumberBackground = numberBackground;

        }

        public void PrintBar()
        {          
            
            if (Percentage < 0 || Percentage > 100)
            {
                throw new ArgumentOutOfRangeException(nameof(Percentage), "Percentage must be between 0 and 100.");
            }

            int x = PosX;
            int y = PosY;  
            
            string percentageStr = Percentage.ToString();

            string[] bar = new string[Width];
            string[] bg = new string[Width];
            string[] num = new string[Width];

            for (int n = 0; n < percentageStr.Length; n++)
            {
                bar[n] = $"{BarCharacter}";
            }
            for (int o = 0; o < Width; o++)
            {
                bg[o] = " ";
            }           


            string finalBg = "";
            string finalBar = "";

            foreach (string s in bar)
            {
                finalBar += s.Color(BarColor).Background(BarBackground);
            }
            foreach (string t in bg)
            {
                finalBg += t.Color(BackgroundColor);
            }

            for (int h = 1; h <= Height; h++)
            {
                SetCursorPosition(x, y);
                Write(finalBg);

                SetCursorPosition(x, y);
                Write(finalBar);
                y++;
            }

            if (ShowNumber)
            {      
                int controlX = (int)((x + Width / 2) - 2);               
                     
                foreach(char v in percentageStr)
                {   
                    SetCursorPosition(controlX, (y - 1) - Height / 2);                  
                    Write($@"{v}%");
                    controlX++;
                }
            }
        }
    }
}
