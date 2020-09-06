using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic; // uso de Dictionaries
using System.Diagnostics; // permite à máquina acessar softwares alternativos presentes nela
using System.Windows.Forms;

namespace AssistenteVirtual
{
    public class Cumi
    {
        /***********************************************************
							  CLASSES PRIVADAS
		***********************************************************/
        class Erros
        {
            /*********************************
					    Atributos
		    **********************************/
            List<string> titulos_erros = new List<string>(); // listagem de títulos de erros
            Dictionary<string, string> mensagens_erros = new Dictionary<string, string>(); // listagem de descrições de erros

            string titulo_ocorrido; // título do erro ocorrido
            string erro_ocorrido; // descrição do erro ocorrido

            /*********************************
					   Propriedades
		    **********************************/              
            // TITULOS_ERROS
            string getTitulos_erros(int index)
            {
                return (this.titulos_erros[index]);
            }

            void setTitulos_erros()
            {
                this.titulos_erros.Add("Arquivo / Software Desconhecido");
                this.titulos_erros.Add("Serviço Inexistente");
                this.titulos_erros.Add("Arquivo Não Encontrado");
            }

            // MENSAGENS_ERROS
            string getMensagens_erros(string titulo)
            {
                return (this.mensagens_erros[titulo]);
            }

            void setMensagens_erros()
            {
                this.mensagens_erros.Add(this.getTitulos_erros(0), "Arquivo ou software desconhecido pela CUMI.\nVerifique se o nome foi digitado corretamente.");
                this.mensagens_erros.Add(this.getTitulos_erros(1), "Serviço inexistente à CUMI.\nVerifique se o nome foi digitado corretamente.");
                this.mensagens_erros.Add(this.getTitulos_erros(2), "Arquivo não encontrado!\nVerifique se o mesmo encontrasse salvo na pasta de documentos da CUMI ou se o nome foi digitado corretamente.");
            }

            // TITULO_OCORRIDO
            string getTitulo_ocorrido()
            {
                return (this.titulo_ocorrido);
            }

            void setTitulo_ocorrido(string titulo)
            {
                this.titulo_ocorrido = titulo;
            }

            // ERRO_OCORRIDO
            string getErro_ocorrido()
            {
                return (this.erro_ocorrido);
            }

            void setErro_ocorrido(string descricao_erro)
            {
                this.erro_ocorrido = descricao_erro;
            }

            /*********************************
					    Métodos
		    **********************************/
            public void Display_Erro(int index_titulo)
            {
                /** Atribuições **/
                this.setTitulo_ocorrido(this.getTitulos_erros(index_titulo));
                this.setErro_ocorrido(this.getMensagens_erros(this.getTitulo_ocorrido()));

                /** Exibição da mensagem **/
                MessageBox.Show(this.getErro_ocorrido(), this.getTitulo_ocorrido());
            }

            /*********************************
					    Construtor
		    **********************************/
            public Erros()
            {
                /** Memória de Erros **/
                this.setTitulos_erros();
                this.setMensagens_erros();
            }

            /*********************************
					    Destruidor
		    **********************************/
            ~Erros()
            { }
        }


        /***********************************************************
                               OBJETOS
       ***********************************************************/
        private Erros erros; // objeto que trata dos erros ocorridos no programa
                                           // exibindo caixas de diálogos

        /***********************************************************
								ATRIBUTOS
		***********************************************************/

        /** Processos **/
        private string query; // armazena comando digitado pelo usuário
        private string suporte; // "query" com Replace e Remove durante a verificação de serviço requisitado
        private string resultado; // decisão escolhida pela Cumi para processamento

        /** Chaves **/
        private string[] servicos_especiais = new string[2]; // serviços disponibilizados pela Cumi, exceto abrir páginas web e inicializar softwares
        private string[] chaves_sites = new string[11]; // armazena chaves identificadoras dos web sites que podem ser abertos
        private string[] chaves_softwares = new string[6]; // armazena chaves identificadoras dos softwares que podem ser inicializados
        private string[] chaves_arquivos = new string[5]; // armazena chaves identificadoras dos arquivos que podem ser abertos pela Cumi

        /** Dictionaries **/
        private Dictionary<string, string> sites_softwares = new Dictionary<string, string>(); // par "chave_site ou chave_software // diretório"
        private Dictionary<string, string[]> sites_pesquisa = new Dictionary<string, string[]>(); // par "chave_site // fragmentos_de_url" para pesquisa web
        private Dictionary<string, string> arquivos_tipos = new Dictionary<string, string>(); // par "tipo_arquivo // diretório_da_pasta"
        private Dictionary<string, bool> flags_generica = new Dictionary<string, bool>(); // armazena genericamente se um serviço especial está ativado ou não
        private Dictionary<string, bool> flags_pesquisa = new Dictionary<string, bool>(); // sinalizadores sobre qual pesquisa está ativada e qual não está. Apenas uma pode estar ativada por vez
        private Dictionary<string, bool> flags_arquivo = new Dictionary<string, bool>(); // sinalizadores sobre qual arquivo está ativado e qual não está

        /** Auxiliares **/
        private string pesquisa_ativada; // armazena nome do site em que a pesquisa está ativada.
                                         // essa variável é declarada a fim de ser necessário realizar uma varredura em "flags_pesquisa" para encontrar o nome do web site ativado
        private string arquivo_ativado; // armazena tipo de arquivo ativado
        private const string IDENTIFICADOR_FLAG_GERAL = "geral"; // armazena chave 'mestra' de "flags_generica"

        /***********************************************************
								PROPRIEDADES
		***********************************************************/

        /** Processos **/

        // QUERY
        private string getQuery()
        {
            return (this.query);
        }

        private void setQuery(string query)
        {
            this.query = query;
        }

        // SUPORTE
        private string getSuporte()
        {
            return (this.suporte);
        }

        private void setSuporte(string query)
        {
            this.suporte = query.Replace(" ", ""); // remoção de qualquer espaço contido no parâmetro
        }

        // RESULTADO
        private string getResultado()
        {
            return (this.resultado);
        }

        private void setResultado(string valor)
        {
            this.resultado = valor;
        }

        /** CHAVES **/

        // SERVICOS_ESPECIAIS
        private string getServicos_especiais(int index)
        {
            return (this.servicos_especiais[index]);
        }

        private void setServicos_especiais()
        {
            this.servicos_especiais[0] = "arquivo"; // Iniciar algum arquivo
            this.servicos_especiais[1] = "pesquisa"; // Pesquisa Web
        }

        // CHAVES_SITES
        private string getChaves_sites(int index)
        {
            return (this.chaves_sites[index]);
        }

        private void setChaves_sites()
        {
            chaves_sites[0] = "google"; // Google
            chaves_sites[1] = "imagem"; // Google Image
            chaves_sites[2] = "yahoo"; // Yahoo
            chaves_sites[3] = "youtube"; // Youtube
            chaves_sites[4] = "facebook"; // Facebook
            chaves_sites[5] = "linkedin"; // LinkedIn
            chaves_sites[6] = "geekie"; // Geekie Games
            chaves_sites[7] = "khan"; // Khan Academy
            chaves_sites[8] = "sololearn"; // SoloLearn
            chaves_sites[9] = "jbc"; // Mangás JBC
            chaves_sites[10] = "outlook"; // Outlook
        }

        // CHAVES_SOFTWARES
        private string getChaves_softwares(int index)
        {
            return (this.chaves_softwares[index]);
        }

        private void setChaves_softwares()
        {
            this.chaves_softwares[0] = "calc"; // Calculadora
            this.chaves_softwares[1] = "word"; // Word
            this.chaves_softwares[2] = "excel"; // Excel
            this.chaves_softwares[3] = "cmd"; // Prompt de Comando
            this.chaves_softwares[4] = "gerenciador"; // Gerenciador de Tarefas
            this.chaves_softwares[5] = "docs"; // Pasta de Documentos da CUMI
        }

        // CHAVES_ARQUIVOS
        private string getChaves_arquivos(int index)
        {
            return (this.chaves_arquivos[index]);
        }

        private void setChaves_arquivos()
        {
            this.chaves_arquivos[0] = "codigo"; // Códigos de Programação (py, c#, java...)
            this.chaves_arquivos[1] = "documento"; // Documentos (txt, word, excel...)
            this.chaves_arquivos[2] = "imagem"; // Imagens
            this.chaves_arquivos[3] = "som"; // Sons
            this.chaves_arquivos[4] = "video"; // Vídeos
        }

        /** Dictionaries **/

        // SITES_SOFTWARES
        private string getSites_softwares(string chave)
        {
            return (this.sites_softwares[chave]);
        }

        private void setSites_softwares()
        {
            // URL'S
            sites_softwares.Add(this.getChaves_sites(0), "https://www.google.com.br"); // Google
            sites_softwares.Add(this.getChaves_sites(1), "https://images.google.com"); // Google Image
            sites_softwares.Add(this.getChaves_sites(2), "https://br.yahoo.com"); // Yahoo
            sites_softwares.Add(this.getChaves_sites(3), "https://www.youtube.com/?gl=BR&hl=pt"); // Youtube
            sites_softwares.Add(this.getChaves_sites(4), "https://www.facebook.com"); // Facebook
            sites_softwares.Add(this.getChaves_sites(5), "https://br.linkedin.com"); // LinkedIn
            sites_softwares.Add(this.getChaves_sites(6), "https://geekiegames.geekie.com.br"); // Geekie Games
            sites_softwares.Add(this.getChaves_sites(7), "https://pt.khanacademy.org"); // Khan Academy
            sites_softwares.Add(this.getChaves_sites(8), "https://www.sololearn.com"); // SoloLearn
            sites_softwares.Add(this.getChaves_sites(9), "https://mangasjbc.com.br"); // Mangás JBC
            sites_softwares.Add(this.getChaves_sites(10), "https://login.live.com/login.srf?wa=wsignin1.0&rpsnv=13&ct=1551790325&rver=7.0.6737.0&wp=MBI_SSL&wreply=https%3a%2f%2foutlook.live.com%2fowa%2f%3fRpsCsrfState%3d248c45db-f185-1e50-e380-90077d637dcf&id=292841&aadredir=1&whr=hotmail.com&CBCXT=out&lw=1&fl=dob%2cflname%2cwld&cobrandid=90015"); // Outlook

            // SOFTWARES
            sites_softwares.Add(this.getChaves_softwares(0), "calc.exe"); // Calculadora
            sites_softwares.Add(this.getChaves_softwares(1), "winword"); // Word
            sites_softwares.Add(this.getChaves_softwares(2), "excel"); // Excel
            sites_softwares.Add(this.getChaves_softwares(3), "cmd"); // Prompt de Comando
            sites_softwares.Add(this.getChaves_softwares(4), "taskmgr"); // Gerenciador de Tarefas
            sites_softwares.Add(this.getChaves_softwares(5), "Arquivos"); // Pasta de Documentos da CUMI
        }

        // SITES_PESQUISA
        private string[] getSites_pesquisa()
        {
            return (this.sites_pesquisa[this.getPesquisa_ativada()]);
        }

        private void setSites_pesquisa()
        {
            this.sites_pesquisa.Add(getChaves_sites(0), new string[] { "https://www.google.com.br/search?client=opera&hs=45r&ei=b3N-XOLkKefG5OUP7PyjwAI&q=", "&oq=", "&gs_l=psy-ab.3..0i10j0i22i10i30.350.4000..4143...0.0..0.103.2251.24j2......0....1..gws-wiz.....0..0i71j0i22i30j0j0i67j0i131.291jwxZMmYg" }); // Google
            this.sites_pesquisa.Add(getChaves_sites(1), new string[] { "https://www.google.com/search?tbm=isch&source=hp&biw=1560&bih=747&ei=OnN-XJPfB7a75OUPrd2joAs&q=", "&oq=", "&gs_l=img.3...885.2340..2452...0.0..0.85.1106.14......0....1..gws-wiz-img.....0..0.k8gWBhGQl8Y" }); // Google Image
            this.sites_pesquisa.Add(getChaves_sites(2), new string[] { "https://br.search.yahoo.com/search?p=", "&fr=yfp-t&fp=1&toggle=1&cop=mss&ei=UTF-8" }); // Yahoo
            this.sites_pesquisa.Add(getChaves_sites(3), new string[] { "https://www.youtube.com/results?search_query=" }); // Youtube
        }

        // ARQUIVOS_TIPOS
        private string getArquivos_tipos()
        {
            return (this.arquivos_tipos[this.getArquivo_ativado()]);
        }

        private void setArquivos_tipos()
        {
            this.arquivos_tipos.Add(this.getChaves_arquivos(0), "Arquivos\\Codigos\\"); // Códigos de Programação (py, c#, java...)
            this.arquivos_tipos.Add(this.getChaves_arquivos(1), "Arquivos\\Documentos\\"); // Documentos (txt, word, excel...)
            this.arquivos_tipos.Add(this.getChaves_arquivos(2), "Arquivos\\Imagens\\"); // Imagens
            this.arquivos_tipos.Add(this.getChaves_arquivos(3), "Arquivos\\Sons\\"); // Sons
            this.arquivos_tipos.Add(this.getChaves_arquivos(4), "Arquivos\\Videos\\"); // Vídeos
        }

        // FLAGS_GENERICA
        private bool getFlags_generica(string chave)
        {
            return (this.flags_generica[chave]);
        }

        private void setFlags_generica(string chave)
        {
            this.flags_generica[IDENTIFICADOR_FLAG_GERAL] = (this.getFlags_generica(IDENTIFICADOR_FLAG_GERAL)) ? false : true;
            this.flags_generica[chave] = this.getFlags_generica(IDENTIFICADOR_FLAG_GERAL);
        }

        private void gerarFlags_generica()
        {
            this.flags_generica.Add(IDENTIFICADOR_FLAG_GERAL, false); // qualquer serviço especial
            this.flags_generica.Add(this.getServicos_especiais(0), false); // arquivos
            this.flags_generica.Add(this.getServicos_especiais(1), false); // pesquisas
        }

        // FLAGS_PESQUISA
        private bool getFlags_pesquisa(string chave)
        {
            return (this.flags_pesquisa[chave]);
        }

        private void setFlags_pesquisa(string chave)
        {
            this.flags_pesquisa[chave] = (this.getFlags_pesquisa(chave)) ? false : true;

            // Caso a pesquisa esteja ativada, "pesquisa_ativada" recebe chave
            if (this.getFlags_pesquisa(chave))
            {
                this.setPesquisa_ativada(chave);
            }

            // Caso contrário, atributo é 'limpado'
            else
            {
                this.setPesquisa_ativada("");
            }
        }

        private void gerarFlags_pesquisa()
        {
            this.flags_pesquisa.Add(getChaves_sites(0), false); // Google
            this.flags_pesquisa.Add(getChaves_sites(1), false); // Google Image
            this.flags_pesquisa.Add(getChaves_sites(2), false); // Yahoo
            this.flags_pesquisa.Add(getChaves_sites(3), false); // Youtube
        }

        // FLAGS_ARQUIVO
        private bool getFlags_arquivo(string chave)
        {
            return (this.flags_arquivo[chave]);
        }

        private void setFlags_arquivo(string chave)
        {
            this.flags_arquivo[chave] = (this.getFlags_arquivo(chave)) ? false : true;

            // Caso um arquivo esteja ativado, "arquivo_ativado" recebe chave
            if (this.getFlags_arquivo(chave))
            {
                this.setArquivo_ativado(chave);
            }

            // Caso contrário, atributo é 'limpado'
            else
            {
                this.setArquivo_ativado("");
            }
        }

        private void gerarFlags_arquivo()
        {
            this.flags_arquivo.Add(this.getChaves_arquivos(0), false); // Códigos de Programação
            this.flags_arquivo.Add(this.getChaves_arquivos(1), false); // Documentos
            this.flags_arquivo.Add(this.getChaves_arquivos(2), false); // Imagens
            this.flags_arquivo.Add(this.getChaves_arquivos(3), false); // Sons
            this.flags_arquivo.Add(this.getChaves_arquivos(4), false); // Vídeos
        }

        /** Auxiliares **/

        // PESQUISA_ATIVADA
        private string getPesquisa_ativada()
        {
            return (this.pesquisa_ativada);
        }

        private void setPesquisa_ativada(string chave)
        {
            this.pesquisa_ativada = chave;
        }

        // ARQUIVO_ATIVADO
        private string getArquivo_ativado()
        {
            return (this.arquivo_ativado);
        }

        private void setArquivo_ativado(string chave)
        {
            this.arquivo_ativado = chave;
        }

        /***********************************************************
								 MÉTODOS
		***********************************************************/
        // PROCESSO
        /// <summary>
        /// Processamento sobre qual decisão tomar.
        /// </summary>
        /// <param name="comando">Input do usuário (string)</param>
        public void Processo(string comando)
        {
            /** Atribuições **/
            this.setQuery(comando);
            this.setSuporte(this.getQuery()); // os espaços já são substituídos por "" dentro da propriedade

            /** Processamento **/
            
            // Serviço Especial Desativado
            if (!(this.getFlags_generica(IDENTIFICADOR_FLAG_GERAL)))
            {
                this.setQuery(this.getQuery().ToLower()); // a fim de evitar erros, "query" tem seu dado transformado em lowercase
                this.setSuporte(this.getSuporte().ToLower()); // o mesmo vale para "suporte"

                // Nenhuma requisição de ativação (serviços especiais)
                if (!(this.Checkar_Requisicao(this.getSuporte())))
                {
                    this.Iniciar_Software(this.getQuery());
                }

                // Requisição de ativação (serviços especiais)
                else
                {
                    this.Iniciar_Servico(this.getSuporte());                    
                }
            }

            // Serviço Especial Ativado
            else
            {
                // Arquivo
                if (this.getFlags_generica(this.getServicos_especiais(0)))
                {
                    this.Abrir_Arquivo();
                }

                // Pesquisa
                else
                {
                    this.Realizar_Pesquisa();
                }
                
            }

            /** Flush?! **/
            this.setQuery("");
            this.setSuporte("");
        }

        // CHECKAR_REQUISICAO
        /// <summary>
        /// Verificação se algum serviço especial está sendo requisitado para ativação.
        /// </summary>
        /// <param name="comando">Requisição do usuário (string)</param>
        /// <returns></returns>
        private bool Checkar_Requisicao(string comando)
        {
            /** Atribuições **/
            bool status = false;

            /** Processamento **/
            for (int index = 0; index < this.servicos_especiais.Length; index++)
            {
                // Caso algum serviço tenha sido mesmo requisitado, o loop é quebrado e o método retorna "true"
                if (comando.StartsWith(this.getServicos_especiais(index)))
                {
                    status = true;
                    break;
                }
            }

            /** Retorno **/
            return (status);
        }

        // INICIAR_SOFTWARE
        /// <summary>
        /// Inicialização de softwares instalados na máquina e websites
        /// </summary>
        /// <param name="comando">Nome do software ou website a ser inicializado (string)</param>
        private void Iniciar_Software(string comando)
        {
            try
            {
                this.setResultado(this.getSites_softwares(comando));
                Process.Start(this.getResultado());
                this.setResultado(""); // flush
            }
            catch
            {
                this.erros = new Erros();
                this.erros.Display_Erro(0); // Site / Software não reconhecido pela CUMI
            }
        }

        // INICIAR_SERVICO
        /// <summary>
        /// Ativação de serviços espeicias
        /// </summary>
        /// <param name="comando">Serviço especial a ser ativado (string)</param>
        private void Iniciar_Servico(string comando)
        {
            /** Atribuições **/
            string ativador_servico = ""; // armazena qual serviço está sendo requisitado. Valor inicial: Nenhum Serviço

            /** Processamento **/

            // VARREDURA
            // Varredura em "servicos_especiais" para verificar se algum serviço especial está sendo requisitado
            for (int index = 0; index < this.servicos_especiais.Length; index++)
            {
                // Caso algum serviço está sendo requisitado, o loop é quebrado
                if (comando.StartsWith(this.getServicos_especiais(index)))
                {
                    ativador_servico = this.getServicos_especiais(index);
                    break;
                }
            }

            // ATIVAÇÃO
            // Serviço Requisitado
            if (!(ativador_servico.Equals("")))
            {
                try
                {
                    comando = comando.Remove(0, ativador_servico.Length); // remoção da identificação do serviço a ser ativadO                                               

                    // Arquivo
                    if (ativador_servico.Equals(this.getServicos_especiais(0)))
                    {
                        this.setFlags_arquivo(comando);
                    }

                    // Pesquisa
                    else
                    {
                        this.setFlags_pesquisa(comando);
                    }

                    this.setFlags_generica(ativador_servico); // ativação genérico do serviço
                                                              // deixei esta linha aqui no final, a fim de não gerar problemas no "catch"
                                                              // pois quando o bloco "catch" era ativado, "flags_generica" continuava ativada
                }

                catch
                {

                    this.erros = new Erros();
                    this.erros.Display_Erro(1); // Serviço não existente
                }         
            }

            // Serviço Não Requisitado / Reconhecido
            else
            {
                this.erros = new Erros();
                this.erros.Display_Erro(1); // Serviço não existente
            }
        }

        // ABRIR_ARQUIVO
        /// <summary>
        /// Inicialziação de arquivos encontrados na pasta de documentos de CUMI
        /// </summary>
        private void Abrir_Arquivo()
        {
            /** Atribuições **/
            this.setResultado(this.getArquivos_tipos() + this.getQuery());

            try
            {
                /** Desativação das flags **/
                this.setFlags_generica(this.getServicos_especiais(0));
                this.setFlags_arquivo(this.getArquivo_ativado());

                /** Abertura do arquivo **/
                Process.Start(this.getResultado());
            }

            catch
            {
                this.erros = new Erros();
                this.erros.Display_Erro(2); // Arquivo não encontrado
            }

            finally
            {
                /** Flush de "resultado" **/
                this.setResultado("");
            }
        }

        // REALIZAR_PESQUISA
        /// <summary>
        /// Realização de pesqusia web
        /// </summary>
        private void Realizar_Pesquisa()
        {
            /** Atribuições **/
            this.setQuery(this.getQuery().Replace(" ", "+")); // como nas URL's espaços são representados como "+", substitui-se os mesmos me "query"
            this.setResultado(this.Unir_Links());

            /** Desativação das flags **/
            this.setFlags_generica(this.getServicos_especiais(1));
            this.setFlags_pesquisa(this.getPesquisa_ativada());

            /** Realização da pesquisa **/
            Process.Start(this.getResultado());

            /** Flush de "resultado" **/
            this.setResultado("");
        }

        // UNIR_LINKS
        /// <summary>
        /// União de links armazenados no atributo "sites_pesquisa" anexando "query" (aquilo que o usuário deseja pesquisa)
        /// </summary>
        /// <returns>Link unido (string)</returns>
        private string Unir_Links()
        {
            /** Atribuições **/
            string[] link_quebrado = this.getSites_pesquisa(); // armazena link quebrado a ser acessado
                                    // é necessário incluir o conteúdo da pesquisa contido em "query" entre cada fragmento do link
            string link_unido = ""; // armazena link a ser acessado

            /** Processamento **/

            // Varredura no link para uni-lo em "link_unido"
            for (int index = 0; index < link_quebrado.Length; index++)
            {
                link_unido += link_quebrado[index];

                // Anexação do conteúdo da pesquisa armazenado em "query" no "link_unido"
                // A anexação não ocorre quando o último fragmento é concatenado em "link_unido", exceto se a pesqusia ocorrer pelo "Youtube" (this.getChaves_sites(3))
                if ((index != link_quebrado.Length - 1) || (this.getPesquisa_ativada().Equals(this.getChaves_sites(3))))
                {
                    link_unido += this.getQuery();
                }
            }

            /** Retorno **/
            return (link_unido);
        }

        /***********************************************************
								CONSTRUTOR
		***********************************************************/
        public Cumi()
        {
            /** Atribuição de memória à Cumi **/

            // SERVICOS ESPECIAIS
            this.setServicos_especiais();

            // CHAVES
            this.setChaves_sites();
            this.setChaves_softwares();
            this.setChaves_arquivos();

            // SITES, SOFTWARES E ARQUIVOS
            this.setSites_softwares();
            this.setSites_pesquisa();
            this.setArquivos_tipos();

            // FLAGS
            this.gerarFlags_generica();
            this.gerarFlags_pesquisa();
            this.gerarFlags_arquivo();
        }

        /***********************************************************
								DESTRUIDOR
		***********************************************************/
        ~Cumi()
        { }
    }
}