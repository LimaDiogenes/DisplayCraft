using static System.Console;


namespace DisplayCraft
{
    /// <summary>
    /// Cria um objeto display para mostrar informações na tela
    /// Podendo ter margens ou não
    /// </summary>
    public class Display
    {
        private int PosX {get;}
        private int PosY {get;}
        private int Width {get;}    
        private int Height {get;}
        private string[] Lines {get; set;}
        private ConsoleColor Background {get; set;}
        private ConsoleColor Foreground {get; set;} 
        private char TopLeft { get; set; }
        private char TopRight { get; set; }
        private char BottomLeft { get; set; }
        private char BottomRight { get; set; }
        private char Sides { get; set; }
        private char Mid { get; set; }   
        public enum BorderStyle
        {
            Default,
            SingleLine,
            Curved,
            Bold,
            Block,            
        }    
        public enum Align
        {
            Left,
            Center,
            MiddleRight,
            MiddleLeft,
            Right,
        }
        public Display(int height = 30, int width = 100,  int posX = 0, int posY = 0,
        ConsoleColor background = ConsoleColor.Black, ConsoleColor foreground = ConsoleColor.White, 
        BorderStyle style = BorderStyle.Default)
        {
            PosX = posX;
            PosY = posY;
            Width = width + 2;
            Height = height + 2;
            Background = background;
            Foreground = foreground;
            Lines = new string[Height];
            SetStyle(style); // estilo da margem         
        }
        public void SetLine(int line, string text, Align align = Align.Left)
        {
            Lines[line] = BuildString(text, align);
        }
        public void SetLine(int[] lines, string[] text, Align align = Align.Left)
        {         
            if (lines.Length != text.Length)
            {
                throw new Exception($"lines[] and text[] must have the same number of members");
            }   
            foreach(int line in lines)            
            {
                int index = Array.IndexOf(lines, line);
                string str = text[index];
                
                if (line > Height-2  || line < 1)
                {
                    throw new Exception($"Line {line} is not within the display");
                }

                if (str.Length > Width)
                {
                    throw new Exception($"Text {text[index]} is not within the display");
                }              

                Lines[line] = BuildString(str, align);
            }
        }
        public void PrintAll()
        {
            int x = PosX + 1;
            int y = PosY + 1;

            SetColor(Background, Foreground);

            string[] insideLines = Lines.Skip(1).SkipLast(1).ToArray();

            foreach(string line in insideLines)           
            {
                SetCursorPosition(x, y);
                Write(line ?? BuildString(""));
                y++;
            }

            DefaultColor();
        }
        public void PrintAll(ConsoleColor background, ConsoleColor foreground)
        {
            int x = PosX + 1;
            int y = PosY + 1;

            SetColor(background, foreground);

            string[] insideLines = Lines.Skip(1).SkipLast(1).ToArray();

            foreach(string line in insideLines)           
            {
                SetCursorPosition(x, y);
                Write(line ?? BuildString(""));
                y++;
            }

            DefaultColor();
        }
        public void Borders()
        {
            int x = PosX;
            int y = PosY;            
                        
            // margem superior
            SetCursorPosition(x, y);
            SetColor(Background, Foreground);         
            Write(BuildCenteredBorders(starter: TopLeft, sep: Mid, ends: TopRight));
            DefaultColor();            
            y++;

            // linhas do meio
            for (int lineCount = 1; lineCount < Lines.Length - 1; lineCount++)
            {
                SetCursorPosition(x, y);
                SetColor(Background, Foreground);
                Write(BuildCenteredBorders(starter: Sides, ends: Sides));
                DefaultColor();
                y++;
            }

            // margem inferior
            SetCursorPosition(x, y);
            SetColor(Background, Foreground);
            Write(BuildCenteredBorders(starter: BottomLeft, sep: Mid, ends: BottomRight));
            DefaultColor();
        }        
        public void Borders(BorderStyle style)
        {
            int x = PosX;
            int y = PosY;        
            SetStyle(style);  
                        
            // margem superior
            SetCursorPosition(x, y);
            SetColor(Background, Foreground);         
            Write(BuildCenteredBorders(starter: TopLeft, sep: Mid, ends: TopRight));
            DefaultColor();            
            y++;

            // linhas do meio
            for (int lineCount = 1; lineCount < Lines.Length - 1; lineCount++)
            {
                SetCursorPosition(x, y);
                SetColor(Background, Foreground);
                Write(BuildCenteredBorders(starter: Sides, ends: Sides));
                DefaultColor();
                y++;
            }

            // margem inferior
            SetCursorPosition(x, y);
            SetColor(Background, Foreground);
            Write(BuildCenteredBorders(starter: BottomLeft, sep: Mid, ends: BottomRight));
            DefaultColor();
        } 
        public void SetStyle(BorderStyle style)
        {
            switch (style)
            {
                case BorderStyle.Default: 
                {               
                    SetDefaultStyle();
                    break;         
                }
                case BorderStyle.SingleLine: 
                {
                    SetSingleLineStyle();
                    break;         
                }
                case BorderStyle.Curved:
                {
                    SetCurvedStyle();
                    break;
                }
                case BorderStyle.Bold:
                {
                    SetBoldStyle();
                    break;
                }
                case BorderStyle.Block:
                {
                    SetBlockStyle();
                    break;
                }
                default:
                {
                    throw new NotImplementedException();
                }  
            }
        }
        private string BuildString(string text, Align align = Align.Left)
        {            
            int leftPadSize = Width/2;
            int rightPadSize = Width/2;
            int halfTextLen = text.Length/2;
            int oddText = 0;
            int availableSpace = Width;
            
            if (Width % 2 != 0)
            {
                rightPadSize++;
            }

            if (text.Length % 2 != 0)
            {
                rightPadSize--;
                oddText = 1;
            }

            switch(align)
            {
            case Align.Right:
            {   
                if (availableSpace < text.Length)
                {
                    throw new Exception($"Not enough space to print {text} Maximum number of chars: {availableSpace}");
                }    
                return text.PadLeft(Width);                     
            }
            case Align.Center:
            {   
                if (availableSpace < text.Length)
                {
                    throw new Exception($"Not enough space to print {text} Maximum number of chars: {availableSpace}");
                }             
                return "".PadLeft(leftPadSize - halfTextLen) + text + "".PadRight(rightPadSize - halfTextLen);                                     
            }
            case Align.MiddleLeft:
            {                
                availableSpace -= rightPadSize;
                if ((availableSpace - 1 < text.Length && oddText == 1) || 
                    availableSpace < text.Length && oddText == 0) // verificando se existe espaço suficiente para imprimir
                {
                    throw new Exception($"Not enough space to print {text} Maximum number of chars: {availableSpace}");
                }
                return text.PadLeft(leftPadSize) + "".PadRight(rightPadSize + oddText);                                     
            }
            case Align.MiddleRight:
            {                
                availableSpace -= leftPadSize;
                if ((availableSpace < text.Length && oddText == 1) || 
                    availableSpace < text.Length && oddText == 0) // verificando se existe espaço suficiente para imprimir
                {
                    throw new Exception($"Not enough space to print {text} Maximum number of chars: {availableSpace}");
                }    
       
                return "".PadLeft(leftPadSize) + text.PadRight(rightPadSize + oddText);                                   
            }
            case Align.Left:
            {
                if (availableSpace < text.Length)
                {
                    throw new Exception($"Not enough space to print {text} Maximum number of chars: {availableSpace}");
                } 
                return text.PadRight(Width);
            }            
            default:
            {
                throw new Exception("Invalid alignment option. Usage: Align.Center / Align.Right, etc.");
            }            
            }
        }
        private string BuildCenteredBorders(string text = "", char starter = default, char ends = default, char sep = default)
        {         
            starter = (starter == default) ? Sides : starter;
            ends = (ends == default) ? Sides : ends;
            sep = (sep == default) ? ' ' : sep;           
            
            string str = text.PadLeft(Width, sep).PadRight(Width, sep);
            return starter + str + ends;            
        }
        private void SetDefaultStyle()
        {
            TopLeft = '╔';
            TopRight = '╗';
            BottomLeft = '╚';
            BottomRight = '╝';
            Sides = '║';
            Mid = '═';
        }
        private void SetSingleLineStyle()
        {
            TopLeft = '┌';
            TopRight = '┐';
            BottomLeft = '└';
            BottomRight = '┘';
            Sides = '│';
            Mid = '─';
        }
        private void SetCurvedStyle()
        {
            TopLeft = '╭';
            TopRight = '╮';
            BottomLeft = '╰';
            BottomRight = '╯';
            Sides = '│';
            Mid = '─';
        }
        private void SetBoldStyle()
        {
            TopLeft = '┏';
            TopRight = '┓';
            BottomLeft = '┗';
            BottomRight = '┛';
            Sides = '┃';
            Mid = '━';
        }
        private void SetBlockStyle()
        {
            TopLeft = '█';
            TopRight = '█';
            BottomLeft = '█';
            BottomRight = '█';
            Sides = '█';
            Mid = '█';
        }
        private void SetColor(ConsoleColor background, ConsoleColor foreground)
        {
            BackgroundColor = (BackgroundColor == background) ? Background : background;
            ForegroundColor = (ForegroundColor == foreground) ? Foreground : foreground;
        }
        private static void DefaultColor()
        {
            BackgroundColor = ConsoleColor.Black;
            ForegroundColor = ConsoleColor.White;
        }
    }
}
