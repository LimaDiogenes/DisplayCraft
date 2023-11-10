namespace DisplayCraft
{
    internal class Display
    {

        protected int Width {get; set;}    
        protected int Height {get; set;}
        private string[] Line {get; set;}
        private ConsoleColor Background {get; set;}
        private ConsoleColor Foreground {get; set;}        

        internal Display(int height = 30, int width = 100, ConsoleColor background = ConsoleColor.Black, ConsoleColor foreground = ConsoleColor.White)
        {
            Width = width;
            Height = height;
            Background = background;
            Foreground = foreground;
            Line = new string[Height];

        }

        internal void Borders()
        {
            Console.Clear();
            ImprimirDisplayVoid(52, 15, ConsoleColor.White);
        }

        /// <summary>
        /// Imprime um Display, por padrão em branco. Usa as propriedades Lines para imprimir as informações na tela.
        /// Para ser usado com os demais metodos de Displays, como TelaInicial, DisplayCadastrar, Etc.
        /// </summary>
        /// <param name="posicaoX">"Ajusta a posição L-O em que o cursor ficará na tela"</param>
        /// <param name="posicaoY">"Ajusta a posição N-S em que o cursor ficará na tela"</param>
        /// <param name="cor">"Seta a cor das informações. Ex. Uso: ConsoleColor.White"</param>
        internal void ImprimirDisplayVoid(int posicaoX, int posicaoY, ConsoleColor cor = ConsoleColor.White) // substitui Lines pela informacao necessaria. Usar dentro da classe
        {
            Console.SetCursorPosition(0, 0); // seta a posição do cursor para começar a impressão
            Console.ForegroundColor = cor; // ajusta a cor
            TextoCentralizado("", starter: '╔', sep: '═', ends: '╗');
            TextoCentralizado(SubstituirPalavras(Line[1], starter: ' ', ends: ' ', width: 94));
            TextoCentralizado(SubstituirPalavras(Line[2], starter: ' ', ends: ' ', width: 94));
            TextoCentralizado(SubstituirPalavras(Line[3], starter: ' ', ends: ' ', width: 94));
            TextoCentralizado(SubstituirPalavras(Line[4], starter: ' ', ends: ' ', width: 94));
            TextoCentralizado(SubstituirPalavras(Line[5], starter: ' ', ends: ' ', width: 94));
            TextoCentralizado(SubstituirPalavras(Line[6], starter: ' ', ends: ' ', width: 94));
            TextoCentralizado(SubstituirPalavras(Line[7], starter: ' ', ends: ' ', width: 94));
            TextoCentralizado(SubstituirPalavras(Line[8], starter: ' ', ends: ' ', width: 94));
            TextoCentralizado(SubstituirPalavras(Line[9], starter: ' ', ends: ' ', width: 94));
            TextoCentralizado(SubstituirPalavras(Line[10], starter: ' ', ends: ' ', width: 94));
            TextoCentralizado(SubstituirPalavras(Line[11], starter: ' ', ends: ' ', width: 94));
            TextoCentralizado(SubstituirPalavras(Line[12], starter: ' ', ends: ' ', width: 94));
            TextoCentralizado(SubstituirPalavras(Line[13], starter: ' ', ends: ' ', width: 94));
            TextoCentralizado(SubstituirPalavras(Line[14], starter: ' ', ends: ' ', width: 94));
            TextoCentralizado(SubstituirPalavras(Line[15], starter: ' ', ends: ' ', width: 94));
            TextoCentralizado(SubstituirPalavras(Line[16], starter: ' ', ends: ' ', width: 94));
            TextoCentralizado(SubstituirPalavras(Line[17], starter: ' ', ends: ' ', width: 94));
            TextoCentralizado(SubstituirPalavras(Line[18], starter: ' ', ends: ' ', width: 94));
            TextoCentralizado(SubstituirPalavras(Line[19], starter: ' ', ends: ' ', width: 94));
            TextoCentralizado(SubstituirPalavras(Line[20], starter: ' ', ends: ' ', width: 94));
            TextoCentralizado(SubstituirPalavras(Line[21], starter: ' ', ends: ' ', width: 94));
            TextoCentralizado(SubstituirPalavras(Line[22], starter: ' ', ends: ' ', width: 94));
            TextoCentralizado(SubstituirPalavras(Line[23], starter: ' ', ends: ' ', width: 94));
            TextoCentralizado(SubstituirPalavras(Line[24], starter: ' ', ends: ' ', width: 94));
            TextoCentralizado(SubstituirPalavras(Line[25], starter: ' ', ends: ' ', width: 94));
            TextoCentralizado("", starter: '╚', sep: '═', ends: '╝');

            Console.SetCursorPosition(posicaoX, posicaoY); // posiciona do cursor para input

        }

        /// <summary>
        /// Imprime um Display, por padrão em branco. Retorna um 'char'. Usa as propriedades Lines para imprimir as informações na tela.
        /// </summary>
        /// <param name="posicaoX">"Ajusta a posição L-O em que o cursor ficará na tela"</param>
        /// <param name="posicaoY">"Ajusta a posição N-S em que o cursor ficará na tela"</param>
        /// <param name="cor">"Seta a cor das informações. Ex. Uso: ConsoleColor.White"</param>
        /// <returns></returns>
        internal char DisplayRetornaChar(int posicaoX, int posicaoY, ConsoleColor cor = ConsoleColor.White) // substituir Lines pela informacao necessaria. Usar dentro da classe
        {
            Console.SetCursorPosition(0, 0); // seta a posição do cursor para começar a impressão

            Console.ForegroundColor = cor; // ajusta a cor
            TextoCentralizado("", starter: '╔', sep: '═', ends: '╗');
            TextoCentralizado(SubstituirPalavras(Line[1], starter: ' ', ends: ' ', width: 94));
            TextoCentralizado(SubstituirPalavras(Line[2], starter: ' ', ends: ' ', width: 94));
            TextoCentralizado(SubstituirPalavras(Line[3], starter: ' ', ends: ' ', width: 94));
            TextoCentralizado(SubstituirPalavras(Line[4], starter: ' ', ends: ' ', width: 94));
            TextoCentralizado(SubstituirPalavras(Line[5], starter: ' ', ends: ' ', width: 94));
            TextoCentralizado(SubstituirPalavras(Line[6], starter: ' ', ends: ' ', width: 94));
            TextoCentralizado(SubstituirPalavras(Line[7], starter: ' ', ends: ' ', width: 94));
            TextoCentralizado(SubstituirPalavras(Line[8], starter: ' ', ends: ' ', width: 94));
            TextoCentralizado(SubstituirPalavras(Line[9], starter: ' ', ends: ' ', width: 94));
            TextoCentralizado(SubstituirPalavras(Line[10], starter: ' ', ends: ' ', width: 94));
            TextoCentralizado(SubstituirPalavras(Line[11], starter: ' ', ends: ' ', width: 94));
            TextoCentralizado(SubstituirPalavras(Line[12], starter: ' ', ends: ' ', width: 94));
            TextoCentralizado(SubstituirPalavras(Line[13], starter: ' ', ends: ' ', width: 94));
            TextoCentralizado(SubstituirPalavras(Line[14], starter: ' ', ends: ' ', width: 94));
            TextoCentralizado(SubstituirPalavras(Line[15], starter: ' ', ends: ' ', width: 94));
            TextoCentralizado(SubstituirPalavras(Line[16], starter: ' ', ends: ' ', width: 94));
            TextoCentralizado(SubstituirPalavras(Line[17], starter: ' ', ends: ' ', width: 94));
            TextoCentralizado(SubstituirPalavras(Line[18], starter: ' ', ends: ' ', width: 94));
            TextoCentralizado(SubstituirPalavras(Line[19], starter: ' ', ends: ' ', width: 94));
            TextoCentralizado(SubstituirPalavras(Line[20], starter: ' ', ends: ' ', width: 94));
            TextoCentralizado(SubstituirPalavras(Line[21], starter: ' ', ends: ' ', width: 94));
            TextoCentralizado(SubstituirPalavras(Line[22], starter: ' ', ends: ' ', width: 94));
            TextoCentralizado(SubstituirPalavras(Line[23], starter: ' ', ends: ' ', width: 94));
            TextoCentralizado(SubstituirPalavras(Line[24], starter: ' ', ends: ' ', width: 94));
            TextoCentralizado(SubstituirPalavras(Line[25], starter: ' ', ends: ' ', width: 94));
            TextoCentralizado("", starter: '╚', sep: '═', ends: '╝');


            Console.SetCursorPosition(posicaoX, posicaoY); //posicionamento do cursor para input
            char OpcaoUsuario = char.ToUpper(Console.ReadKey().KeyChar); //variavel com a selecao do usuario. ReadKey aqui serve para receber a primeira tecla do input do usuario,
                                                                         //e KeyChar passa o valor recebido em formato ´char´para a variavel OpcaoUsuario

            return OpcaoUsuario;
        }

        /// <summary>       
        /// cria um texto centralizado, usado para montar a tela no console.        /// 
        /// Todos os parâmetros são opcionais, e com valores padrão criam Lines com margem nas laterais
        /// Caracteres para usar como starter / ends / sep:{ ╔ ╗ ╚ ╝ ═ } Com estes é possível montar Lines do topo e do final
        /// Altura é controlada pelo número de Lines utilizado
        /// </summary>
        /// <param name="text">texto a ser exibido centralizado</param>
        /// <param name="starter">primeiro caractere da Line (margem)</param>
        /// <param name="ends">ultimo caractere da Line (margem)</param>
        /// <param name="sep">caracter para usar como separador, que aparecerá antes e depois do texto até o tamanho da tela</param>
        /// <param name="width">largura</param>                
        internal static void TextoCentralizado(string text = "", char starter = '║', char ends = '║', char sep = ' ', int width = 100)
        {

            int padWidth = (width - text.Length) / 2; // encontra a metade da largura, excluindo o tamanho do texto criado
            string paddedText = text.PadLeft(text.Length + padWidth, sep).PadRight(width, sep); // PadLeft e PadRight criam caracteres de preenchimento "sep" em ambos os lados do texto, deixando centralizado na tela  
            Console.WriteLine("  " + starter + paddedText + ends + "  "); // cria o texto centralizado na tela usando starter e ends como margem

        }
        /// <summary>
        ///SubstituirPalavras é uma versão alterada de TextoCentralizado, imprimindo o que deve ser escrito
        ///para poder utilizar como argumento, dentro de TextoCentralizado(), assim podendo imprimir texto dentro das margens, sem apagá-las
        /// </summary>
        /// <param name="text"></param>
        /// <param name="starter"></param>
        /// <param name="ends"></param>
        /// <param name="sep"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        internal static string SubstituirPalavras(string text = "", char starter = '║', char ends = '║', char sep = ' ', int width = 100) // cria um texto centralizado, aceitando como argumentos "end = primeiro e ultimo caractere da Line", "sep = caracter para usar como separador, que aparecerá antes e depois do texto até o tamanho da tela", "width = largura" todos os parâmetros são opcionais
        {
            int padWidth = (width - text.Length) / 2; // encontra a metada da largura, excluindo o tamanho do texto criado
            string palavra = text.PadLeft(text.Length + padWidth, sep).PadRight(width, sep); // PadLeft e PadRight criam caracteres de preenchimento "sep" em ambos os lados do texto, deixando centralizado na tela  
            string replacer = ("  " + starter + palavra + ends + "  "); // cria o texto centralizado na tela usando os "ends" como margem
            return replacer;
        }

    }
}
