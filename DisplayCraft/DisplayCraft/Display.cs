using static System.Console;

namespace DisplayCraft
{
    internal class Display
    {
        private int PosX {get; set;}
        private int PosY {get; set;}
        private int Width {get; set;}    
        private int Height {get; set;}
        private string[] Lines {get; set;}
        private ConsoleColor Background {get; set;}
        private ConsoleColor Foreground {get; set;} 
        private char TopLeft { get; set; }
        private char TopRight { get; set; }
        private char BottomLeft { get; set; }
        private char BottomRight { get; set; }
        private char Sides { get; set; }
        private char Mid { get; set; }       

        internal Display(int height = 30, int width = 100,  int posX = 0, int posY = 0,
        ConsoleColor background = ConsoleColor.Black, ConsoleColor foreground = ConsoleColor.White, 
        BorderStyle style = BorderStyle.Default)
        {
            Width = width + 2;
            Height = height + 2;
            PosX = posX;
            PosY = posY;
            Background = background;
            Foreground = foreground;
            Lines = new string[Height + 2]; // + 2 para usar linha 0 e última linha como margens
            SetStyle(style); // estilo da margem     
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                #pragma warning disable CA1416 // desativa warning que codigo abaixo é apenas para windows

                WindowWidth = Width+PosX+4;  // p/ console windows - tamanho do console
                WindowHeight = Height+PosY+2; 

                BufferWidth = WindowWidth+2;  // p/ console windows - tamanho do buffer
                BufferHeight = WindowHeight+2;
                
                #pragma warning restore CA1416    
            }    
        }

        internal void Borders()
        {
            int x = PosX;
            int y = PosY;            
            
            // margem superior
            SetCursorPosition(x, y);
            SetColor(Background, Foreground);         
            Write(Lines[0] = BuildCenteredString(starter: TopLeft, sep: Mid, ends: TopRight));
            DefaultColor();            
            y++;

            // linhas do meio
            for (int lineCount = 1; lineCount < Lines.Length - 1; lineCount++)
            {
                SetCursorPosition(x, y);
                SetColor(Background, Foreground);
                Write(Lines[lineCount] = BuildCenteredString($"Linha {lineCount}", starter: Sides, ends:Sides));
                DefaultColor();
                y++;
            }

            // margem inferior
            SetCursorPosition(x, y);
            SetColor(Background, Foreground);
            Write(Lines[Lines.Length - 1] = BuildCenteredString(starter: BottomLeft, sep: Mid, ends: BottomRight));
            DefaultColor();
        }

        internal enum BorderStyle
        {
            Default,
            SingleLine,
            Curved,
            Bold,
            Block,            
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

        internal void SetStyle(BorderStyle style)
        {
            switch (style)
            {
                case BorderStyle.Default:                
                    SetDefaultStyle();
                    break;                
                case BorderStyle.SingleLine:                
                    SetSingleLineStyle();
                    break;                       
                case BorderStyle.Curved:
                    SetCurvedStyle();
                    break;
                case BorderStyle.Bold:
                    SetBoldStyle();
                    break;
                case BorderStyle.Block:
                    SetBlockStyle();
                    break;
            }
        }

        private string BuildCenteredString(string text = "", char starter = default, char ends = default, char sep = default)
        {         
            starter = (starter == default) ? Sides : starter;
            ends = (ends == default) ? Sides : ends;
            sep = (sep == default) ? ' ' : sep;           

            int padWidth = (Width - text.Length) / 2;
            string str = text.PadLeft(text.Length + padWidth, sep).PadRight(Width, sep);
            string replacer = starter + str + ends;
            return replacer;
        }

        private void SetColor(ConsoleColor background, ConsoleColor foreground)
        {
            BackgroundColor = (BackgroundColor == background) ? background : Background;
            ForegroundColor = (ForegroundColor == foreground) ? foreground : Foreground;
        }

        private void DefaultColor()
        {
            BackgroundColor = ConsoleColor.Black;
            ForegroundColor = ConsoleColor.White;
        }

    }
}
