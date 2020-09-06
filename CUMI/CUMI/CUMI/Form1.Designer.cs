namespace CUMI
{
    partial class Form1
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_confirmar = new System.Windows.Forms.Button();
            this.btn_ajuda = new System.Windows.Forms.Button();
            this.campo_txt = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btn_confirmar
            // 
            this.btn_confirmar.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btn_confirmar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_confirmar.Location = new System.Drawing.Point(460, 41);
            this.btn_confirmar.Name = "btn_confirmar";
            this.btn_confirmar.Size = new System.Drawing.Size(68, 25);
            this.btn_confirmar.TabIndex = 0;
            this.btn_confirmar.Text = "Confirmar !";
            this.btn_confirmar.UseVisualStyleBackColor = false;
            this.btn_confirmar.Click += new System.EventHandler(this.btn_confirmar_Click);
            // 
            // btn_ajuda
            // 
            this.btn_ajuda.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btn_ajuda.Cursor = System.Windows.Forms.Cursors.Help;
            this.btn_ajuda.Location = new System.Drawing.Point(526, 12);
            this.btn_ajuda.Name = "btn_ajuda";
            this.btn_ajuda.Size = new System.Drawing.Size(58, 23);
            this.btn_ajuda.TabIndex = 1;
            this.btn_ajuda.Text = "Ajuda ?";
            this.btn_ajuda.UseVisualStyleBackColor = false;
            this.btn_ajuda.Click += new System.EventHandler(this.btn_ajuda_Click);
            // 
            // campo_txt
            // 
            this.campo_txt.Location = new System.Drawing.Point(12, 46);
            this.campo_txt.Name = "campo_txt";
            this.campo_txt.Size = new System.Drawing.Size(442, 20);
            this.campo_txt.TabIndex = 2;
            this.campo_txt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(596, 78);
            this.Controls.Add(this.campo_txt);
            this.Controls.Add(this.btn_ajuda);
            this.Controls.Add(this.btn_confirmar);
            this.Name = "Form1";
            this.Text = "CUMI";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_confirmar;
        private System.Windows.Forms.Button btn_ajuda;
        private System.Windows.Forms.TextBox campo_txt;
    }
}

