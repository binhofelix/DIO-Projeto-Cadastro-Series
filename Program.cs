using System;
using System.IO;
using System.Collections.Generic;

namespace DIO.Series
{
    class Program
    {
        static SerieRepositorio repositorio = new SerieRepositorio();        
        static FileHelper arqHelper = new FileHelper();
        static string caminho = AppDomain.CurrentDomain.BaseDirectory;
        static string nomeArqJson = "series.json";
      

        static void Main(string[] args)
        {

            nomeArqJson = Path.Combine(caminho, nomeArqJson);
            //Se JSon existir:
            if (File.Exists(nomeArqJson)) {
                var lista = arqHelper.LerJson(nomeArqJson);                
                foreach (var serie in lista)
                {
                    //Inserir:
                    Serie novaSerie = new Serie(id: repositorio.ProximoId(),
                                    genero: (Genero)serie.Genero,
                                    titulo: serie.Titulo,
                                    ano: serie.Ano,
                                    desc: serie.Descricao);

                    repositorio.Insere(novaSerie);                    
                }
            }

            //Tela:
            string opcaoUsuario = ObterOpcaoUsuario();

            while (opcaoUsuario.ToUpper() != "X")
            {
                switch(opcaoUsuario)
                {
                    case "1":
                        Console.Clear();
                        ListarSeries();
                        break;
                    case "2":
                        Console.Clear();                        
                        InserirSerie();
                        break;
                    case "3":                        
                        AtualizarSerie();
                        break;
                    case "4":                        
                        ExcluirSerie();
                        break;
                    case "5":                
                        VisualizarSerie();
                        break;
                    case "C":                        
                        Console.Clear();
                        break;
                    case "S":
                        SalvarListaSeries();
                        Console.WriteLine("Arquivo JSON foi salvo!");
                        break;                        
                    default:
                        //throw new ArgumentOutOfRangeException();
                        Console.Clear(); 
                        Console.WriteLine("Opção inválida!");                                                                                                                                  
                        break;
                }
                opcaoUsuario = ObterOpcaoUsuario();
            }
        }

        private static bool Confirmacao(string msg)
        {
            Console.WriteLine();
            Console.WriteLine(msg + "[Digite S para Sim]"); //Tem certeza
            string opcaoUsuario = Console.ReadLine().ToUpper();
            if (opcaoUsuario == "S") {
                return true;
            } else {
                return false;
            }
        }

        private static int GetConsoleInt()
        {
            bool rodando = true;
            string linha;
            int valor = 0;
            while (rodando) {
                linha = Console.ReadLine();
                if (int.TryParse(linha, out valor)) {
                    rodando = false;
                } else {
                    Console.WriteLine("[ERRO] Valor inválido! Digite um número válido:");
                }
            }
            return valor;
        }
        private static void SalvarListaSeries()
        {
            var lista = repositorio.Lista(); //SerieRepositorio.cs
            if (lista.Count == 0)
            {
                Console.WriteLine("Nenhuma série cadastrada.");
                return;
            }
            arqHelper.GravarJson(nomeArqJson, lista);               
        }

        private static void ListarSeries()
        {
            Console.WriteLine();
            Console.WriteLine("LISTA DE SÉRIES:");
            var lista = repositorio.Lista(); //SerieRepositorio.cs
            if (lista.Count == 0)
            {
                Console.WriteLine("Nenhuma série cadastrada.");
                return;
            }
            var msg = "";
            foreach(var serie in lista)
            {
                if (serie.retornaExcluido() == false)
                {
                    msg = "";
                    msg += "#ID "+serie.retornaId()+": "+serie.retornaTitulo() + " ("+serie.Ano+") ["+serie.Genero+"]";
                    Console.WriteLine(msg);
                }                
            }
        }

        private static void AtualizarSerie()
        {
            Console.WriteLine("Digite o id da série: ");
            int indiceSerie = int.Parse(Console.ReadLine());

            foreach (int i in Enum.GetValues(typeof(Genero)))
            {
                Console.WriteLine("{0}-{1}", i, Enum.GetName(typeof(Genero),i));
            }
            Console.WriteLine("Digite o gênero entre as opções acima: ");            
            int entradaGenero = GetConsoleInt();

            Console.WriteLine("Digite o título da série: ");
            string entradaTitulo = Console.ReadLine();

            Console.WriteLine("Digite o Ano de inicio da série: ");            
            int entradaAno = GetConsoleInt();

            Console.WriteLine("Digite a descrição da série: ");
            string entradaDesc = Console.ReadLine();

            //Cadastrar:
            Serie atualizaSerie = new Serie(id: indiceSerie,
                                    genero: (Genero)entradaGenero,
                                    titulo: entradaTitulo,
                                    ano: entradaAno,
                                    desc: entradaDesc);

            repositorio.Atualiza(indiceSerie, atualizaSerie);            
        }

        private static void InserirSerie()
        {
            Console.WriteLine("INSERIR NOVA SÉRIE:");
            //
            foreach (int i in Enum.GetValues(typeof(Genero)))
            {
                Console.WriteLine("{0}-{1}", i, Enum.GetName(typeof(Genero),i));
            }
            Console.WriteLine();
            Console.WriteLine("Digite o gênero entre as opções acima: ");            
            int entradaGenero = GetConsoleInt();

            Console.WriteLine("Digite o título da série: ");
            string entradaTitulo = Console.ReadLine();

            Console.WriteLine("Digite o Ano de inicio da série: ");            
            int entradaAno = GetConsoleInt();

            Console.WriteLine("Digite a descrição da série: ");
            string entradaDesc = Console.ReadLine();

            //Cadastrar:
            Serie novaSerie = new Serie(id: repositorio.ProximoId(),
                                    genero: (Genero)entradaGenero,
                                    titulo: entradaTitulo,
                                    ano: entradaAno,
                                    desc: entradaDesc);

            repositorio.Insere(novaSerie);
        }

        private static void ExcluirSerie()
        {
            Console.WriteLine("Digite o id da série a ser excluída: ");
            int indiceSerie = int.Parse(Console.ReadLine());

            if (Confirmacao("Tem certeza que quer excluir? ")) {
                repositorio.Excluir(indiceSerie);
            }            
        }

        private static void VisualizarSerie()
        {
            Console.WriteLine("Digite o id da série para visualizar: ");            
            int indiceSerie = GetConsoleInt();
            Console.WriteLine();

            var serie = repositorio.RetornaPorId(indiceSerie);

            Console.WriteLine(serie); //ToString
        }


        private static string ObterOpcaoUsuario()
        {
            Console.WriteLine();
            Console.WriteLine("BINHO SERIES");
            Console.WriteLine();
            Console.WriteLine("Informe a opção desejada:");

            Console.WriteLine("1 - Listar Séries");
            Console.WriteLine("2 - Inserir Nova Série");
            Console.WriteLine("3 - Atualizar Série");
            Console.WriteLine("4 - Excluir série");
            Console.WriteLine("5 - Visualizar série");
            Console.WriteLine("C - Limpar tela");
            Console.WriteLine("S - Salvar lista");
            Console.WriteLine("X - Sair");
            Console.WriteLine();
            string opcaoUsuario = Console.ReadLine().ToUpper();
            Console.WriteLine();
            return opcaoUsuario;
        }
    }
}
