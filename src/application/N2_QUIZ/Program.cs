using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace N2_QUIZ
{
    class Program
    {
        /* Programa desenvolvido por:
         * 
         * Guilherme Feruglio Nishiyama        | RA: 081210018
         * Lucas Araújo dos Santos             | RA: 081210009
         */

        #region Enunciado
        /* Sistema QUIZ
         * 
         * Atenção: Este arquivo deve ser preenchido e deve ser enviado junto com o trabalho
         * 
         * 
         * Faça um sistema que leia de um arquivo texto um conjunto de perguntas e alternativas.  Observe que a entrada de dados será via arquivo texto. O usuário não irá digitar as perguntas/alternativas!!!
         * 
         * O sistema deverá entrar então em um modo de jogo, onde o usuário irá escolher o tema e a quantidade de perguntas que serão apresentadas.
         * 
         * O sistema deverá selecionar aleatoriamente N perguntas e exibi-las ao usuário (respeitando o tema escolhido!!!).
         * 
         * Ao final, exiba em vídeo todas as perguntas, a resposta correta e a resposta dada pelo usuário. Exiba também quantas perguntas o usuário acertou e quantas ele errou.
         * 
         * O sistema deverá suportar até 100 perguntas e até 10 temas diferentes. Todas as perguntas deverão possuir 4 alternativas. 
         * 
         * Os valores no arquivo devem ser separados pelo caractere pipe |
         * 
         * O nome do arquivo de entrada é fixo : QUIZ.TXT e deverá ser enviado juntamente com o trabalho um arquivo contendo uma amostra de dados para que seja possível efetuar a correção do trabalho.
         * 
         * Deixe o arquivo na mesma pasta que o seu executável.
         * 
         * 
         * Exemplo do arquivo texto que seu programa deverá ler:
         * 
         * Pergunta | tema | alternativa correta | alternativa 1 | alternativa 2 | alternativa 3 | alternativa 4 
         * 
         * 
         * ex: 
         * 
         * Quem ganhou a copa do mundo de 2002?| futebol | Brasil | italia |peru | argentina | brasil
         * Quanto é 5+5?| matemática | 10 | 55 | 0 | 5+5 | 10
         * Quanto é 3+3?| matemática | 6 | 23 | 0 | 2+3| 6
         * Qual o maior inimigo do batman? | desenho | coringa | bozo | fura tripa|superman|coringa
         * 
         * Será analisado:
         * 
         * Codificação, identação, utilização de métodos e outras estruturas da linguagem, organização do código, comentários nos métodos, além das funcionalidades definidas neste documento.
         * 
         * Complexidade ciclomática máxima: 8 pontos.
         * 
         * Trabalhos iguais ou similares serão anulados e a nota será zero.
         */
        #endregion

        #region Programa Principal
        static void Main(string[] args)
        {
            CarregaArquivo();
            int QuantidadePerguntas, qtdTema = 0;
            string temaEscolhido;
            string temasDisponiveis = "";
            qtdTema = DescobreQuantidadeDeTemas(qtdTema);

            string[] TemasDoQuiz = new string[qtdTema];

            for (int i = 0; i < qtdTema; i++)
            {
                TemasDoQuiz[i] = vetorAux[i];
            }

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("- - - - - - - - - QUIZ - - - - - - - - - -");
            Console.ResetColor();

            for (int i = 0; i < TemasDoQuiz.Length; i++)
            {
                temasDisponiveis = temasDisponiveis + $"\n{i+1} - {TemasDoQuiz[i].ToUpper()}" ;
            }

            temaEscolhido = VerificaTema(TemasDoQuiz, VerificaInteiro($"\nEscolha o tema das perguntas (1 até {TemasDoQuiz.Length}):\n{temasDisponiveis}\n\nTema: ", TemasDoQuiz.Length));

            Pergunta[] PerguntasDoTemaEscolhido = PreenchePerguntasNoTema(temaEscolhido);

            QuantidadePerguntas = VerificaInteiro($"\nDigite a quantidade de perguntas a serem exibidas (1 até {PerguntasDoTemaEscolhido.Length}):\n\nQuantidade: ", PerguntasDoTemaEscolhido.Length);
            Quiz(QuantidadePerguntas, PerguntasDoTemaEscolhido);

            Console.ReadKey();
        }
        #endregion

        #region Métodos

        #region Struct e vetor Pergunta
        struct Pergunta
        {
            public string questao;
            public string tema;
            public string respostaCorreta;
            public string respostaEscolhida;
            public string alt1, alt2, alt3, alt4;
            public bool acertou;
        }

        static Pergunta[] Perguntas = new Pergunta[100];
        static string[] vetorAux = new string[Perguntas.Length];
        #endregion

        #region Carrega Arquivo
        /// <summary>
        /// Este método carrega o arquivo texto, e verifica se possui uma quantidade de linhas inferior ou igual a 100, além de armazenar os dados da linha no Struct.
        /// </summary>
        static void CarregaArquivo()
        {
            int contLinha = 0;

            if (File.Exists("QUIZ.txt"))
            {
                string[] linhas = File.ReadAllLines("QUIZ.txt");

                foreach (string linha in linhas)
                {
                    string[] dadosLinha = linha.Split('|');

                    if (contLinha == 100)
                    {
                        Console.WriteLine("O limite de linhas do arquivo texto é 100 linhas!!\n\nPor favor, corrija e tente novamente...");
                        Console.ReadKey();
                        Environment.Exit(1);
                    }
                    else
                    {
                        ArmazenaDadosNoStruct(dadosLinha, contLinha);

                        contLinha++;
                    }
                }
            }
            else
            {
                Console.WriteLine("Arquivo texto com as perguntas não encontrado!!");
            }
        }
        #endregion

        #region Preenche Perguntas no Tema
        /// <summary>
        /// Este método preenche em um vetor todas as perguntas correspondentes ao tema escolhido pelo usuário.
        /// </summary>
        /// <param name="temaEscolhido">Tema escolhido pelo usuário.</param>
        /// <returns>Retorna um vetor contendo todas as perguntas do tema escolhido pelo usuário.</returns>
        static Pergunta[] PreenchePerguntasNoTema(string temaEscolhido)
        {
            Pergunta[] vetAux = new Pergunta[Perguntas.Length];

            int qtd = 0;

            foreach (Pergunta p in Perguntas)
            {
                if (p.tema == temaEscolhido)
                {
                    vetAux[qtd] = p;

                    qtd++;
                }
            }

            Pergunta[] PerguntasDoTemaEscolhido = new Pergunta[qtd];

            for (int i = 0; i < qtd; i++)
            {
                PerguntasDoTemaEscolhido[i] = vetAux[i];
            }

            return PerguntasDoTemaEscolhido;
        }
        #endregion

        #region QUIZ
        /// <summary>
        /// Este método é responsável por mostrar ao usuário as perguntas durante o programa, e mostrar no final o status do jogo, a quantidade de perguntas certas e erradas que o usuário obteve.
        /// </summary>
        /// <param name="QuantidadePerguntas">Quantidade de perguntas a serem exibidas durante o programa.</param>
        /// <param name="PerguntasDoTemaEscolhido">Vetor contendo todas as perguntas do tema escolhido pelo usuário.</param>
        static void Quiz(int QuantidadePerguntas, Pergunta[] PerguntasDoTemaEscolhido)
        {
            string resposta, alternativa;
            int contPergunta = 1, contCorretas = 0, contErradas = 0;
            int[] verificador = new int[QuantidadePerguntas];

            verificador = GeraSequenciaDePerguntas(QuantidadePerguntas);

            foreach (int pos in verificador)
            {
                int posLinha = 0;
                
                foreach (Pergunta linha in PerguntasDoTemaEscolhido)
                {
                    if (posLinha == pos-1)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.WriteLine($"{contPergunta}° Pergunta:\n");
                        Console.ResetColor();
                        Console.WriteLine(linha.questao + "\n");
                        Console.WriteLine("A - " + linha.alt1);
                        Console.WriteLine("B - " + linha.alt2);
                        Console.WriteLine("C - " + linha.alt3);
                        Console.WriteLine("D - " + linha.alt4 + "\n");

                        resposta = RespostaEscolhida();
                        alternativa = VerificaAlternativa(resposta, linha.alt1, linha.alt2, linha.alt3, linha.alt4);

                        PerguntasDoTemaEscolhido[posLinha].respostaEscolhida = alternativa;

                        if (ValidarResposta(linha.respostaCorreta, alternativa) == true)
                        {
                            PerguntasDoTemaEscolhido[posLinha].acertou = true;
                            contCorretas++;
                        }
                        else
                        {
                            PerguntasDoTemaEscolhido[posLinha].acertou = false;
                            contErradas++;
                        }

                        contPergunta++;
                    }

                    posLinha++;
                }
            }

            ExibirPerguntasResolvidas(PerguntasDoTemaEscolhido, verificador);

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("- - - - - - RESULTADOS - - - - - -");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nACERTOS: {contCorretas}");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"ERROS: {contErradas}");
        }
        #endregion

        #region Descobre Quantidade de Temas
        /// <summary>
        /// Este método descobre a quantidade de temas disponíveis, comparando os temas existentes em todas as perguntas armazenadas no Struct.
        /// </summary>
        /// <param name="qtdTema">Variável para armazenar a quantidade de temas disponíveis no programa.</param>
        /// <returns>Retorna a quantidade de temas disponíveis no programa.</returns>
        static int DescobreQuantidadeDeTemas(int qtdTema)
        {
            foreach (Pergunta p in Perguntas)
            {
                bool existe = false;

                for (int n = 0; n < qtdTema; n++)
                {
                    if (vetorAux[n] == p.tema)
                    {
                        existe = true;
                        break;
                    }
                }

                if (!existe)
                {
                    vetorAux[qtdTema] = p.tema;
                    qtdTema++;
                }
            }

            if (qtdTema > 10)
            {
                Console.WriteLine("O limite de temas do programa é 10 temas!!\n\nPor favor, corrija e tente novamente...");
                Console.ReadKey();
                Environment.Exit(1);
            }

            if (qtdTema < 10)
                qtdTema = qtdTema - 1;

            return qtdTema;
        }
        #endregion

        #region Armazena Dados no Struct
        /// <summary>
        /// Este método armazena os dados da linha do arquivo texto no Struct.
        /// </summary>
        /// <param name="dadosLinha">Vetor string contendo os dados da linha analisada em questão do arquivo texto.</param>
        /// <param name="contLinha">Posição da linha analisada do arquivo texto.</param>
        static void ArmazenaDadosNoStruct(string[] dadosLinha, int contLinha)
        {
            Perguntas[contLinha].questao = dadosLinha[0];
            Perguntas[contLinha].tema = dadosLinha[1];
            Perguntas[contLinha].respostaCorreta = dadosLinha[2];
            Perguntas[contLinha].alt1 = dadosLinha[3];
            Perguntas[contLinha].alt2 = dadosLinha[4];
            Perguntas[contLinha].alt3 = dadosLinha[5];
            Perguntas[contLinha].alt4 = dadosLinha[6];
        }
        #endregion

        #region Exibir Perguntas Resolvidas
        /// <summary>
        /// Este método exibe no final do programa, as perguntas respondidas pelo usuário, resposta correta e a resposta do usuário.
        /// </summary>
        /// <param name="PerguntasDoTemaEscolhido">Vetor contendo todas as perguntas do tema escolhido pelo usuário.</param>
        /// <param name="verificador">Vetor contendo a sequência das perguntas exibidas durante o programa.</param>
        static void ExibirPerguntasResolvidas(Pergunta[] PerguntasDoTemaEscolhido, int[] verificador)
        {
            int contPosicao = 1;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("- - - - -  STATUS DO JOGO  - - - - -");
            Console.WriteLine("- - - - - - - - - - - - - - - - - -");
            Console.ResetColor();
            Console.WriteLine($"\nTEMA: {PerguntasDoTemaEscolhido[0].tema.ToUpper()}");

            foreach (int posExibida in verificador)
            {
                Console.WriteLine($"\n{contPosicao}° pergunta:");
                Console.WriteLine(PerguntasDoTemaEscolhido[posExibida - 1].questao);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nResposta Correta: " + PerguntasDoTemaEscolhido[posExibida - 1].respostaCorreta);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("A sua resposta: " + PerguntasDoTemaEscolhido[posExibida - 1].respostaEscolhida + "\n");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("- - - - - - - - - - - - - - - - - -");
                Console.ResetColor();
                contPosicao++;
            }
        }
        #endregion

        #region Gera Número Aleatório
        /// <summary>
        /// Este método gera um número aleatório.
        /// </summary>
        /// <param name="QtdPerguntas">Quantidade de perguntas.</param>
        /// <returns>Retorna um número gerado aleatoriamente.</returns>
        static int GeraNumeroAleatorio(int QtdPerguntas)
        {
            Random gerador = new Random();

            return gerador.Next(1, QtdPerguntas+1);
        }
        #endregion

        #region Gera Sequência de Perguntas
        /// <summary>
        /// Este método gera aleatoriamente a sequência das perguntas a serem exibidas para o usuário.
        /// </summary>
        /// <param name="QtdPerguntas">Quantidade de perguntas a serem exibidas.</param>
        /// <returns>Retorna um vetor inteiro, contendo a ordem da sequência das perguntas que serão exibidas.</returns>
        static int[] GeraSequenciaDePerguntas(int QtdPerguntas)
        {
            int numeroGerado = 0;
            int[] verificador = new int[QtdPerguntas];

            for (int n = 0; n < QtdPerguntas; n++)
            {
                bool existe;
                do
                {
                    existe = false;
                    numeroGerado = GeraNumeroAleatorio(QtdPerguntas);

                    if (VerificaRepetido(verificador, numeroGerado) == true)
                    {
                        existe = true;
                    }
                    else
                    {
                        verificador[n] = numeroGerado;
                        break;
                    }
                }
                while (existe);
            }

            return verificador;
        }
        #endregion

        #region Verifica Repetido
        /// <summary>
        /// Este método verifica se o número gerado aleatoriamente, é igual aos gerados anteriormente, visando mostrar se está sendo repetido.
        /// </summary>
        /// <param name="verificador">Vetor analisado contendo todos os números gerados anteriormente.</param>
        /// <param name="numeroGerado">Número gerado aleatoriamente.</param>
        /// <returns>Retorna o valor true se o número for repetido, e false caso não for repetido.</returns>
        static bool VerificaRepetido(int[] verificador, int numeroGerado)
        {
            foreach (int num in verificador)
            {
                if (num == numeroGerado)
                {
                    return true;
                }
            }

            return false;
        }
        #endregion

        #region Resposta Escolhida
        /// <summary>
        /// Este método verifica se a resposta digitada pelo usuário é A, B, C ou D, e se o usuário tem certeza da escolha da resposta escolhida.
        /// </summary>
        /// <returns>Retorna a resposta digitada pelo usuário.</returns>
        static string RespostaEscolhida()
        {
            string resposta = "";
            string continua = "N";

            do
            {
                resposta = PreencheResposta(resposta, continua);
                continua = ConfirmaResposta(continua);

            } while (continua[0] == 'N' || continua[0] != 'S' || continua.Length == 0);

            return resposta;
        }
        #endregion

        #region Preenche Resposta
        /// <summary>
        /// Este método verifica se a resposta digitada pelo usuário é A, B, C ou D, e caso não for, solicita novamente para digitar a resposta certa.
        /// </summary>
        /// <param name="resposta">String para armazenar a resposta digitada pelo usuário.</param>
        /// <param name="continua">String responsável para verificar se a confirmação do usuário referente a resposta escolhida é sim ou não.</param>
        /// <returns>Retorna a resposta digitada pelo usuário.</returns>
        static string PreencheResposta(string resposta, string continua)
        {  
            if (continua[0] != 'N' || continua.Length == 0)
                return resposta;
            
            do
            {
                resposta = VerificaResposta(resposta);

            } while (resposta != "A" && resposta != "B" && resposta != "C" &&
            resposta != "D" || resposta.Length == 0);

            return resposta;
        }
        #endregion

        #region Verifica Resposta
        /// <summary>
        /// Este método verifica se a resposta digitada pelo usuário é A, B, C ou D e exibe uma mensagem de erro, caso necessário.
        /// </summary>
        /// <param name="resposta">String para armazenar a resposta digitada pelo usuário.</param>
        /// <returns>Retorna a resposta digitada pelo usuário.</returns>
        static string VerificaResposta(string resposta)
        {
            Console.Write("Resposta: ");
            resposta = Console.ReadLine().ToUpper();

            if (resposta != "A" && resposta != "B" && resposta != "C" &&
                resposta != "D" || resposta.Length == 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("\nDigite apenas A, B, C ou D");
                Console.ResetColor();
            }

            return resposta;
        }
        #endregion

        #region Confirma Resposta
        /// <summary>
        /// Este método tem como objetivo verificar se o usuário tem certeza da escolha da resposta escolhida.
        /// </summary>
        /// <param name="continua">String que armazenará a resposta digitada pelo usuário.</param>
        /// <returns>Retorna a resposta digitada pelo usuário.</returns>
        static string ConfirmaResposta(string continua)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nVocê confirma a sua resposta? (S/N)");
            Console.ResetColor();
            continua = Console.ReadLine().ToUpper();

            if (continua.Length == 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("\nDigite sim ou não!!");
                Console.ResetColor();
                continua = " ";
            }
            else if (continua[0] == 'N')
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nDigite outra opção: ");
                Console.ResetColor();
            }

            else if (continua[0] != 'S' || continua[0] != 'N' || continua.Length == 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("\nDigite sim ou não!!");
                Console.ResetColor();
            }

            return continua;
        }
        #endregion

        #region Verifica Alternativa
        /// <summary>
        /// Este método verifica qual foi a alternativa escolhida pelo usuário (A, B, C ou D) e retorna o valor correspondente a ela.
        /// </summary>
        /// <param name="resposta">Resposta escolhida pelo usuário.</param>
        /// <param name="a">Valor da string correspondente a alternativa A.</param>
        /// <param name="b">Valor da string correspondente a alternativa B.</param>
        /// <param name="c">Valor da string correspondente a alternativa C.</param>
        /// <param name="d">Valor da string correspondente a alternativa D.</param>
        /// <returns>Retorna o valor da string correspondente a alternativa escolhida pelo usuário.</returns>
        static string VerificaAlternativa(string resposta, string a, string b, string c, string d)
        {
            switch (resposta)
            {
                case "A":
                    return a;
                case "B":
                    return b;
                case "C":
                    return c;
                case "D":
                    return d;
                default:
                    return "";
            }
        }
        #endregion

        #region Validar Resposta
        /// <summary>
        /// Este método verifica se a resposta do usuário está correta ou errada.
        /// </summary>
        /// <param name="AlternativaCorreta">Alternativa correta da pergunta em questão.</param>
        /// <param name="resposta">Resposta escolhida pelo usuário.</param>
        /// <returns>Retorna true se a resposta estiver correta e false se a resposta estiver errada.</returns>
        static bool ValidarResposta(string AlternativaCorreta, string resposta)
        {
            if(resposta == AlternativaCorreta)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region Verifica Tema
        /// <summary>
        /// Este método verifica o tema escolhido pelo usuário (entre 1 até a quantidade de temas do programa) e retorna uma string com o valor correspondente ao tema.
        /// </summary>
        /// <param name="TemasDoQuiz">Vetor string contendo todos os temas disponíveis no programa.</param>
        /// <param name="temaEscolhido">Tema escolhido pelo usuário (entre 1 até a quantidade de temas do programa).</param>
        /// <returns>Retorna uma string com o valor correspondente ao tema escolhido pelo usuário.</returns>
        static string VerificaTema(string[] TemasDoQuiz, int temaEscolhido)
        {
            for (int i = 1; i <= 10; i++)
            {
                if(temaEscolhido == i)
                {
                    return TemasDoQuiz[i - 1];
                }
            }

            return "";
        }
        #endregion

        #region Verifica Inteiro
        /// <summary>
        /// Este método verifica se o número digitado pelo usuário é um número inteiro entre 1 até a quantidade em questão.
        /// </summary>
        /// <param name="mensagem">Mensagem que será exibida ao usuário.</param>
        /// <param name="qtd">Quantidade especificada para o limite da condição.</param>
        /// <returns>Retorna um valor inteiro entre 1 até a quantidade em questão.</returns>
        static int VerificaInteiro(string mensagem, int qtd)
        {
            int valor = 0;

            do
            {
                try
                {
                    Console.Write(mensagem);
                    valor = int.Parse(Console.ReadLine());

                    if (valor <= 0 || valor > qtd)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine($"Digite apenas entre 1 até {qtd}!!");
                        Console.ResetColor();
                    }
                }
                catch
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine($"Digite apenas entre 1 até {qtd}!!");
                    Console.ResetColor();
                }

            } while (valor <= 0 || valor > qtd);

            return valor;
        }
        #endregion

        #endregion
    }
}