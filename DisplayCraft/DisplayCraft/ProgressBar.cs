using System;
using static System.Console;
using ANSIConsoleWindows;
using static System.ConsoleColor;
using System.Drawing;



namespace DisplayCraft
{
    internal class ProgressBar
    {
        private Display Screen { get; set; }
        private int PosX { get; set; }
        public int PosY { get; set; }
        private int Width { get; set; }
        private int Height { get; set; }
        private int Percentage { get; set; }
        private bool ShowNumber { get; set; } 
        private bool ShowNumberBackground { get; set;}
        private char BarCharacter { get; set; }        
        private ConsoleColor BarColor {get; set;}  
        private ConsoleColor BarBackground {get; set;}           
        private ConsoleColor NumberColor {get; set;}
        private ConsoleColor NumberBackground {get; set;}
        private string[] Strings {get; set;} // recebe membros do PrintBar()
        
        /// <summary>
        /// Cria uma barra de progresso. 
        /// Usa as propriedades PosX e PosY do objeto Display como referência para posicionar a barra.
        /// </summary>
        /// <param name="display"></param>
        /// <param name="percentage"></param>
        public ProgressBar(Display display, int percentage)
        {
            Screen = display;
            Percentage = percentage;
            PosX = Screen.PosX + 1;
            PosY = Screen.PosY + 1;
            Width = 10;
            Height = 1;
            BarCharacter = '█';
            ShowNumber = false;
            ShowNumberBackground = true;
            BarColor = DarkGreen;
            BarBackground = Black;
            Strings = new string[Width];
        }

        public ProgressBar(Display display, int percentage, int posX, int posY, int width = 10, int height = 1, 
                           char barCharacter = '█', bool showNumber = true, bool showNumberBackground = true,
                           ConsoleColor barBackground = DarkRed, ConsoleColor barColor = DarkGreen, 
                           ConsoleColor numberColor = White, ConsoleColor numberBackground = Black)
        {
            Screen = display;
            Percentage = percentage;
            PosX = Screen.PosX + posX;
            PosY = Screen.PosY + posY;
            Width = width;
            Height = height;
            BarCharacter = barCharacter;
            ShowNumber = showNumber;
            BarColor = barColor;
            BarBackground = barBackground;
            NumberColor = numberColor;
            NumberBackground = numberBackground;
            ShowNumberBackground = showNumberBackground;
            Strings = new string[Width];
        }
       /// <summary>
       /// Imprime a barra. Uso: apenas após formatar o estilo. Qualquer formatação abaixo do PrintBar não será aplicada.
       /// </summary>
       /// <exception cref="ArgumentOutOfRangeException"></exception>
        
        public void SetNumberStyle(ConsoleColor background, ConsoleColor color, 
                                   bool showBackground,  bool showNumber = true)
        {
            ShowNumber = showNumber;
            ShowNumberBackground = showBackground;
            NumberBackground = background;
            NumberColor = color;
        }
        
        public void SetBarColors(ConsoleColor background, ConsoleColor bar)
        {
            BarBackground = background;
            BarColor = bar;
        }
        /// <summary>
        /// Ajusta posição com base no objeto Display.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void SetPosition(int x, int y)
        {                     
            PosX = x + Screen.PosX;
            PosY = y + Screen.PosY;
        }
        public void SetSize(int width, int height = 1)
        {
            if (width < 1 || height < 1)
            {
                throw new IndexOutOfRangeException("Sizes must be bigger than 0");
            }
            Width = width;
            Height = height;
        }
        public void SetPercentage(int newPerc)
        {
            if (newPerc < 0 || newPerc > 100)
            {
                throw new IndexOutOfRangeException("Percentage must be between 0 and 100");
            }
            Percentage = newPerc;
        }
        public void PrintBar()
        {
            if (Percentage < 0 || Percentage > 100)
            {
                throw new ArgumentOutOfRangeException(nameof(Percentage), "Percentage must be between 0 and 100.");
            }

            int x = PosX;
            int y = PosY;                    
            int progressCells = (int)(Percentage / 100.0 * Width);
            ANSIString[] barCells = new ANSIString[Width];
            ANSIString[] bgBar = new ANSIString[Width];
            ANSIString[] perc = new ANSIString[Width];

            for (int n = 0; n < progressCells; n++)
            {                
                barCells[n] = $"{BarCharacter}".Color(BarColor).Background(BarBackground);
            }
            for (int o = 0; o < Width; o++)
            {
                bgBar[o] = "█".Color(BarBackground);
            }

            for (int p = 1; p <= Height; p++)
            {
                foreach (var q in bgBar)
                {
                    SetCursorPosition(x, y);
                    Write(q);
                    x++;
                }
                x -= bgBar.Length;
                foreach (var r in barCells)
                {
                    SetCursorPosition(x, y);
                    Write($"{r}");                    
                    x++;
                }
                x -= barCells.Length;
                y++;
            }
            y -= Height;                          

            if (ShowNumber)
            {               
                string percStr = Percentage.ToString() + "%";
                int startIndex = bgBar.Length / 2 - 2;                

                foreach(var s in percStr)
                {
                    ANSIString t = new($"{s}");
                    perc[startIndex] = t;
                    startIndex++;
                }                
                
                if(ShowNumberBackground)
                {
                    foreach(var t in perc)
                    {
                        SetCursorPosition(x, y + Height / 2);
                        Write($"{t}".Background(NumberBackground).Color(NumberColor));                        
                        x++;
                    }
                    x -= perc.Length;
                }

                else
                {
                    foreach(var u in perc)
                    {
                        SetCursorPosition(x, y + Height / 2);
                        int index = Array.IndexOf(perc, u);
                        if (barCells[index] != null)
                        {
                            var bgColor = barCells[index].GetForegroundColor();
                            string numberStr = $"{u}".Background(bgColor).Color(NumberColor).ToString();                            
                            Write(numberStr);
                        }
                        else 
                        {
                            var bgColor = bgBar[index].GetForegroundColor();
                            string numberStr = $"{u}".Background(bgColor).Color(NumberColor).ToString();                            
                            Write(numberStr);
                        }                      

                        x++;
                    }
                    x -= perc.Length;                    
                }
            }
        }        
        /// <summary>  
        /// Em aguardo
        /// Não funcional - verificações de tamanho na classe display estão considerando os caracteres escapado (cores) no tamanho da string
        /// retorna apenas a primeira linha para impressão, independente da altura da barra.
        /// Uso: Dentro de outros métodos de impressão
        /// </summary>
        /// <returns></returns>
        // public override string ToString()
        // {   
        //     string final = "";
        //     foreach (var a in Strings)
        //     {
        //         final += a;
        //     }
        //     return final;
        // }
    }
}
