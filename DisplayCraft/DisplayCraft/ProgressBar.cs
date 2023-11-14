using static System.Console;

namespace DisplayCraft
{
    internal class ProgressBar : IMenu
    {
        public Display Screen { get; set; }
        public int PosX { get; set; }
        public int PosY { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        private double Percentage { get; set; }
        private char BarCharacter { get; set; }
        private bool ShowNumber { get; set; }
        private ConsoleColor NumberColor { get; set; }

        public ProgressBar(Display display, int percentage, int width = 10, int height = 1, char barCharacter = '█', bool showNumber = false, ConsoleColor numberColor = ConsoleColor.Black)
        {
            Screen = display;
            Percentage = percentage;
            PosX = Screen.PosX;
            PosY = Screen.PosY;
            Width = width;
            Height = height;
            BarCharacter = barCharacter;
            ShowNumber = showNumber;
            NumberColor = NumberColor;
        }

        public ProgressBar(Display display, double percentage, int posX, int posY, int width = 10, int height = 1, 
                           char barCharacter = '█', bool showNumber = false, ConsoleColor numberColor = ConsoleColor.Black)
        {
            Screen = display;
            Percentage = percentage;
            PosX = posX;
            PosY = posY;
            Width = width;
            Height = height;
            BarCharacter = barCharacter;
            ShowNumber = showNumber;
            NumberColor = numberColor;
        }

        public void PrintBar(ConsoleColor background = ConsoleColor.DarkRed, ConsoleColor foreground = ConsoleColor.DarkGreen)
        {
            int x = PosX;
            int y = PosY;            
            
            if (Percentage < 0 || Percentage > 100)
            {
                throw new ArgumentOutOfRangeException(nameof(Percentage), "Percentage must be between 0 and 100.");
            }
            int progress = (int)(Percentage / 100 * Width);

            string[] bar = new string[Width];
            string[] bg = new string[Width];

            for (int n = 0; n < progress; n++)
            {
                bar[n] = $"{BarCharacter}";
            }
            for (int o = 0; o < Width; o++)
            {
                bg[o] = " ";
            }
            string numberColor = GetColorCode(NumberColor);
            string bgColor = GetColorCode(background, false);
            string fgColor = GetColorCode(foreground);
            string originalBgColor = GetColorCode(Screen.Background, false);
            string originalFgColor = GetColorCode(Screen.Foreground);
            string finalBg = "";
            string finalBar = "";

            foreach (string s in bar)
            {
                finalBar += s;
            }
            foreach (string t in bg)
            {
                finalBg += t;
            }

            for (int h = 1; h <= Height; h++)
            {
                SetCursorPosition(x, y);
                Write(bgColor + finalBg + originalBgColor);

                SetCursorPosition(x, y);
                Write(bgColor + fgColor + finalBar + originalFgColor + originalBgColor);
                y++;
            }

            if (ShowNumber)
            {
                SetCursorPosition((x + Width / 2) - 2 , (y - 1) - Height / 2);
                Write($"{numberColor}{bgColor}{(int)Percentage}%{originalBgColor}{originalFgColor}");
            }
        }
        private static string GetColorCode(ConsoleColor color, bool isForeground = true)
        {
            string colorCode = isForeground ? "\u001b[38;5;" : "\u001b[48;5;";

            return color switch
            {
                ConsoleColor.Black => $"{colorCode}0m",
                ConsoleColor.DarkBlue => $"{colorCode}4m",
                ConsoleColor.DarkGreen => $"{colorCode}2m",
                ConsoleColor.DarkCyan => $"{colorCode}6m",
                ConsoleColor.DarkRed => $"{colorCode}1m",
                ConsoleColor.DarkMagenta => $"{colorCode}5m",
                ConsoleColor.DarkYellow => $"{colorCode}3m",
                ConsoleColor.Gray => $"{colorCode}7m",
                ConsoleColor.DarkGray => $"{colorCode}8m",
                ConsoleColor.Blue => $"{colorCode}12m",
                ConsoleColor.Green => $"{colorCode}10m",
                ConsoleColor.Cyan => $"{colorCode}14m",
                ConsoleColor.Red => $"{colorCode}9m",
                ConsoleColor.Magenta => $"{colorCode}13m",
                ConsoleColor.Yellow => $"{colorCode}11m",
                ConsoleColor.White => $"{colorCode}15m",
                _ => "",
            };
        }
    }


}
