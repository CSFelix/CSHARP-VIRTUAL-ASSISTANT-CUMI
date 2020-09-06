using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using AssistenteVirtual; // importação da classe "Cumi"

namespace CUMI
{
    
    public partial class Form1 : Form
    {
        // Variáveis Globais
        string comando; // comando digitado pelo usuário
        Cumi objeto = new Cumi();

        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Ao clicar no botão "Ajuda ?", formulário contendo todos os comandos é exibido numa nova janela
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_ajuda_Click(object sender, EventArgs e)
        {
            Form ajuda = new Ajuda();
            ajuda.Show();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_confirmar_Click(object sender, EventArgs e)
        {
            if (!(campo_txt.Text.Equals("")))
            {
                comando = campo_txt.Text;
                campo_txt.Text = null;

                objeto.Processo(comando);

                comando = null;
            }
        }
    }
}
