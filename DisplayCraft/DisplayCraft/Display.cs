using static System.Console;

namespace DisplayCraft
{
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
            Middle,
            MiddleLeft,
            Right,
        }
        public Display(int height = 30, int width = 100,  int posX = 0, int posY = 0,
        ConsoleColor background = ConsoleColor.Black, ConsoleColor foreground = ConsoleColor.White, 
        BorderStyle style = BorderStyle.Default)
        {
            PosX = posX;
            PosY = posY;
            Width = width + posX+ 2;
            Height = height + posY + 2;
            Background = background;
            Foreground = foreground;
            Lines = new string[Height + 2]; // + 2 para usar linha 0 e última linha como margens
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
                
                if (index > Height  || index < 0)
                {
                    throw new Exception($"Line {line} is not within the display");
                }

                if (text[index].Length > Width - 2)
                {
                    throw new Exception($"Text {text[index]} is not within the display");
                }              

                Lines[line] = BuildString(text[index], align);
            }
        }
        public void Print()
        {
            int x = PosX + 1;
            int y = PosY + 1;
            
            foreach(string line in Lines)
            {
                SetCursorPosition(x, y);
                Write(line);
                y++;
            }
        }
        private string BuildString(string text, Align align)
        {            
            int padHalfWidth = Width/2;
            
            if (Width % 2 != 0 && text.Length % 2 == 0)
            {
                padHalfWidth++;
            }

            switch(align)
            {
            case Align.Right:
            {      
                return text.PadLeft(Width - text.Length, ' ');                     
            }
            case Align.Center:
            {                
                return "".PadLeft((Width/2) - (text.Length/2)) + text + "".PadRight(padHalfWidth - text.Length/2);                                     
            }
            case Align.MiddleLeft:
            {                
                return text.PadLeft(padHalfWidth) + "".PadRight(padHalfWidth);                                     
            }
            case Align.Middle:
            {                              
                return text.PadLeft(Width/2) + "".PadRight(padHalfWidth);                                   
            }
            case Align.Left:
            {
                return text.PadRight(Width, ' ');
            }
            default:
            {
                throw new NotImplementedException();
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
            BackgroundColor = (BackgroundColor == background) ? background : Background;
            ForegroundColor = (ForegroundColor == foreground) ? foreground : Foreground;
        }
        private static void DefaultColor()
        {
            BackgroundColor = ConsoleColor.Black;
            ForegroundColor = ConsoleColor.White;
        }

    }
}
