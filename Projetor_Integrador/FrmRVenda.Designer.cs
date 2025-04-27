namespace Projetor_Integrador
{
    partial class FrmRVenda
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRVenda));
            this.txtCCliente = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCProduto = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCEstoque = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cboxFPagamento = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.maskedtxtTotalVenda = new System.Windows.Forms.MaskedTextBox();
            this.maskedtxtValorReceita = new System.Windows.Forms.MaskedTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.btnPesquisar = new System.Windows.Forms.Button();
            this.btnImprimir = new System.Windows.Forms.Button();
            this.btnNovo = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtCRelatorio = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtCCliente
            // 
            this.txtCCliente.Location = new System.Drawing.Point(270, 24);
            this.txtCCliente.Name = "txtCCliente";
            this.txtCCliente.ReadOnly = true;
            this.txtCCliente.Size = new System.Drawing.Size(100, 20);
            this.txtCCliente.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(270, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Codigo Cliente";
            // 
            // txtCProduto
            // 
            this.txtCProduto.Location = new System.Drawing.Point(380, 24);
            this.txtCProduto.Name = "txtCProduto";
            this.txtCProduto.ReadOnly = true;
            this.txtCProduto.Size = new System.Drawing.Size(100, 20);
            this.txtCProduto.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(377, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Codigo Produto";
            // 
            // txtCEstoque
            // 
            this.txtCEstoque.Location = new System.Drawing.Point(500, 24);
            this.txtCEstoque.Name = "txtCEstoque";
            this.txtCEstoque.ReadOnly = true;
            this.txtCEstoque.Size = new System.Drawing.Size(100, 20);
            this.txtCEstoque.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(497, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Codigo Estoque";
            // 
            // cboxFPagamento
            // 
            this.cboxFPagamento.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboxFPagamento.FormattingEnabled = true;
            this.cboxFPagamento.Items.AddRange(new object[] {
            "Dinheiro",
            "Cartão Crédito",
            "Cartão Débito",
            "Parcelado",
            "Pix"});
            this.cboxFPagamento.Location = new System.Drawing.Point(150, 69);
            this.cboxFPagamento.Name = "cboxFPagamento";
            this.cboxFPagamento.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cboxFPagamento.Size = new System.Drawing.Size(110, 21);
            this.cboxFPagamento.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(150, 50);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(110, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Forma De Pagamento";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(270, 54);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Total Venda";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(392, 51);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Valor Receita";
            // 
            // maskedtxtTotalVenda
            // 
            this.maskedtxtTotalVenda.Location = new System.Drawing.Point(270, 70);
            this.maskedtxtTotalVenda.Mask = "$00,00";
            this.maskedtxtTotalVenda.Name = "maskedtxtTotalVenda";
            this.maskedtxtTotalVenda.Size = new System.Drawing.Size(100, 20);
            this.maskedtxtTotalVenda.TabIndex = 12;
            // 
            // maskedtxtValorReceita
            // 
            this.maskedtxtValorReceita.Location = new System.Drawing.Point(389, 70);
            this.maskedtxtValorReceita.Mask = "$00,00";
            this.maskedtxtValorReceita.Name = "maskedtxtValorReceita";
            this.maskedtxtValorReceita.Size = new System.Drawing.Size(100, 20);
            this.maskedtxtValorReceita.TabIndex = 13;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.btnSalvar);
            this.panel1.Controls.Add(this.btnPesquisar);
            this.panel1.Controls.Add(this.btnImprimir);
            this.panel1.Controls.Add(this.btnNovo);
            this.panel1.Location = new System.Drawing.Point(22, 216);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(572, 44);
            this.panel1.TabIndex = 55;
            // 
            // btnSalvar
            // 
            this.btnSalvar.Image = global::Projetor_Integrador.Properties.Resources.save24;
            this.btnSalvar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSalvar.Location = new System.Drawing.Point(393, 5);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(77, 36);
            this.btnSalvar.TabIndex = 31;
            this.btnSalvar.Text = "Salvar";
            this.btnSalvar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSalvar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSalvar.UseVisualStyleBackColor = true;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // btnPesquisar
            // 
            this.btnPesquisar.Image = global::Projetor_Integrador.Properties.Resources.pesqui_24;
            this.btnPesquisar.Location = new System.Drawing.Point(476, 5);
            this.btnPesquisar.Name = "btnPesquisar";
            this.btnPesquisar.Size = new System.Drawing.Size(89, 35);
            this.btnPesquisar.TabIndex = 30;
            this.btnPesquisar.Text = "Pesquisar";
            this.btnPesquisar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnPesquisar.UseVisualStyleBackColor = true;
            this.btnPesquisar.Click += new System.EventHandler(this.btnPesquisar_Click);
            // 
            // btnImprimir
            // 
            this.btnImprimir.Image = global::Projetor_Integrador.Properties.Resources.Print_24;
            this.btnImprimir.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnImprimir.Location = new System.Drawing.Point(97, 5);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(94, 33);
            this.btnImprimir.TabIndex = 5;
            this.btnImprimir.Text = "Imprimir";
            this.btnImprimir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnImprimir.UseVisualStyleBackColor = true;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // btnNovo
            // 
            this.btnNovo.Image = global::Projetor_Integrador.Properties.Resources.novo24;
            this.btnNovo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnNovo.Location = new System.Drawing.Point(3, 5);
            this.btnNovo.Name = "btnNovo";
            this.btnNovo.Size = new System.Drawing.Size(88, 34);
            this.btnNovo.TabIndex = 0;
            this.btnNovo.Text = "Novo";
            this.btnNovo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnNovo.UseVisualStyleBackColor = true;
            this.btnNovo.Click += new System.EventHandler(this.btnNovo_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Projetor_Integrador.Properties.Resources.baixados;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(116, 153);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 14;
            this.pictureBox1.TabStop = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(150, 5);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(85, 13);
            this.label7.TabIndex = 56;
            this.label7.Text = "Codigo Relatório";
            // 
            // txtCRelatorio
            // 
            this.txtCRelatorio.Location = new System.Drawing.Point(153, 24);
            this.txtCRelatorio.Name = "txtCRelatorio";
            this.txtCRelatorio.ReadOnly = true;
            this.txtCRelatorio.Size = new System.Drawing.Size(100, 20);
            this.txtCRelatorio.TabIndex = 57;
            // 
            // FrmRVenda
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(621, 272);
            this.Controls.Add(this.txtCRelatorio);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.maskedtxtValorReceita);
            this.Controls.Add(this.maskedtxtTotalVenda);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cboxFPagamento);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtCEstoque);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtCProduto);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtCCliente);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmRVenda";
            this.Text = "Relatório Venda";
            this.Load += new System.EventHandler(this.FrmRVenda_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtCCliente;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCProduto;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCEstoque;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboxFPagamento;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.MaskedTextBox maskedtxtTotalVenda;
        private System.Windows.Forms.MaskedTextBox maskedtxtValorReceita;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnImprimir;
        private System.Windows.Forms.Button btnNovo;
        private System.Windows.Forms.Button btnPesquisar;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtCRelatorio;
    }
}