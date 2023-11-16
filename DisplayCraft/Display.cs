using System.Text;
using static System.Console;


namespace DisplayCraft
{
    /// <summary>
    /// Cria um objeto display personalizável 
    /// para mostrar informações na tela
    /// Windows: Recomendado utilizar "Console.BufferWidth, Console.BufferHeight, 
    ///                                Console.WindowWidth e Console.WindowHeight 
    ///                                para setar tamanho da janela antes de utilizar Display 
    /// </summary>
    public class Display
    {
        public int PosX { get; }
        public int PosY { get; }
        private int Width { get; }
        private int Height { get; }
        private string[] Lines { get; set; }
        internal ConsoleColor Background { get; private set; }
        internal ConsoleColor Foreground { get; private set; }
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
        /// <summary>
        /// Inicializa o display com os parametros escolhidos.
        /// Todos os parametros sao opcionais e possuem valor default
        /// (style: Uso: Display.BorderStyle.ESTILOESCOLHIDO)
        /// Windows: Recomendado utilizar "Console.BufferWidth, Console.BufferHeight, 
        ///                                Console.WindowWidth e Console.WindowHeight 
        ///                                para setar tamanho da janela antes de utilizar Display 
        /// </summary>
        /// <param name="height">Altura em linhas</param>
        /// <param name="width">Largura em caracteres</param>
        /// <param name="posX">Posicao do topo esquerdo em relacao a janela do console</param>
        /// <param name="posY">Posicao em relacao ao topo da janela do console</param>
        /// <param name="background">Cor do plano de fundo. (Uso: ConsoleColor.CORESCOLHIDA)</param>
        /// <param name="foreground">Cor da impressao na tela. (Uso: ConsoleColor.CORESCOLHIDA)</param>
        /// <param name="style">Estilo das bordas. (Uso: Display.BorderStyle.ESTILOESCOLHIDO)</param>
        public Display(int height = 30, int width = 100, int posX = 0, int posY = 0,
        ConsoleColor background = ConsoleColor.Black, ConsoleColor foreground = ConsoleColor.White,
        BorderStyle style = BorderStyle.Default)
        {
            OutputEncoding = Encoding.UTF8;
            PosX = posX;
            PosY = posY;
            Width = width + 2;
            Height = height + 2;
            Background = background;
            Foreground = foreground;
            Lines = new string[Height];
            SetStyle(style); // estilo da margem
        }
        /// <summary>
        /// Define o conteudo de uma linha especifica no display.        
        /// (align - Uso: Display.Align.ALINHAMENTOESCOLHIDO)
        /// </summary>
        /// <param name="line">Numero da linha a ser atualizada.</param>
        /// <param name="text">Texto a ser exibido.</param>
        /// <param name="align">Alinhamento do texto (Padrao: Align.Left).</param>
        /// <exception cref="Exception">Levantada se o numero da linha estiver fora do intervalo válido.</exception>
        public void SetLine(int line, string text, Align align = Align.Left)
        {
            if (line > 0 && line < Height - 1)
            {
                Lines[line] = BuildString(text, align);
            }
            else
            {
                throw new Exception($"Line {line} is not within the display");
            }
        }
        /// <summary>
        /// Define o conteudo de varias linhas, atraves de 2 vetores.
        /// (Uso: vetor lines: recebe o indice da linha / 
        /// vetor text: recebe o texto para a linha indicada / 
        /// --Ex.: lines[]{1, 3, 5} + text[]{"abc", "def", "ghi"} 
        /// para atualizar linhas 1, 3 e 5--
        /// align: Display.Align.ALINHAMENTOESCOLHIDO)
        /// </summary>
        /// <param name="lines">Numero da linha a ser atualizada.</param>
        /// <param name="text">Texto a ser exibido.</param>
        /// <param name="align">Alinhamento do texto (Padrao: Align.Left)</param>
        /// <exception cref="Exception"></exception>
        public void SetLine(int[] lines, string[] text, Align align = Align.Left)
        {
            if (lines.Length != text.Length)
            {
                throw new Exception($"lines[] and text[] must have the same number of members");
            }
            foreach (int line in lines)
            {
                int index = Array.IndexOf(lines, line);
                string str = text[index];

                if (line > Height - 2 || line < 1)
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
        /// <summary>
        /// Imprime todas as linhas internas do menu, excluindo as margens
        /// </summary>
        public void PrintAll()
        {
            int x = PosX + 1;
            int y = PosY + 1;

            SetColor(Background, Foreground);

            string[] insideLines = Lines.Skip(1).SkipLast(1).ToArray();

            foreach (string line in insideLines)
            {
                SetCursorPosition(x, y);
                Write(line ?? BuildString(""));
                y++;
            }

            DefaultColor();
        }
        /// <summary>
        /// Imprime todas as linhas internas do menu, excluindo as margens, 
        /// aceitando cores de fundo e do conteudo como parametros
        /// </summary>
        /// <param name="background">Cor do plano de fundo</param>
        /// <param name="foreground">Cor do conteudo impresso</param>
        public void PrintAll(ConsoleColor background, ConsoleColor foreground)
        {
            int x = PosX + 1;
            int y = PosY + 1;

            SetColor(background, foreground);

            string[] insideLines = Lines.Skip(1).SkipLast(1).ToArray();

            foreach (string line in insideLines)
            {
                SetCursorPosition(x, y);
                Write(line ?? BuildString(""));
                y++;
            }

            DefaultColor();
        }
        /// <summary>
        /// Imprime as margens e o plano de fundo.
        /// Deve ser chamado antes do PrintAll().
        /// </summary>
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
        /// <summary>
        /// Imprime as margens e o plano de fundo, 
        /// alterando o estilo de margem.
        /// Deve ser chamado antes do PrintAll().
        /// </summary>
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
        /// <summary>
        /// Altera o estilo da margem
        /// (Uso: Display.BorderStyle.ESCOLHA)
        /// </summary>
        /// <param name="style">Estilo de margem (Uso: Display.BorderStyle.ESCOLHA)</param>
        /// <exception cref="Exception"></exception>
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
                        throw new Exception("Invalid Selection of BorderStyle");
                    }
            }
        }
        private string BuildString(string text, Align align = Align.Left)
        {
            int leftPadSize = Width / 2;
            int rightPadSize = Width / 2;
            int halfTextLen = text.Length / 2;
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

            switch (align)
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
