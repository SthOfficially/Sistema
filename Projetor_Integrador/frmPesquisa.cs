using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Projetor_Integrador
{
    public partial class frmPesquisa : Form        
    {
        public int codigo { get; set; }

        //ComboBox
        List<string> _Campo = new List<string>();
        List<string> _Apelido = new List<string>();

        //Colunas Grid
        List<string> _NomeCampo = new List<string>();
        List<string> _NomeColuna = new List<string>();
        List<DataGridViewContentAlignment> _Alinhamento = new List<DataGridViewContentAlignment>();
        List<string> _Formato = new List<string>();
        List<int> _Largura = new List<int>();


        public string Tabela = "";
        public string SQL = "";
        private string sqlGrid = "";
        //public DbConnection Conexao = null;

        public string CodRetorno = "";
        public string SegundoCampoRetorno = "";
        public string TerceiroCampoRetorno = "";

        public bool Vazio = false;

        public string FiltroAdicional { get; set; }

        public frmPesquisa()
        {
            InitializeComponent();
        }

        private void frmPesquisa_Load(object sender, EventArgs e)
        {
            CarregaControles();            
        }

        public void AddCampos(string Campo, string Apelido)
        {
            _Campo.Add(Campo);
            _Apelido.Add(Apelido);
        }

        public void AddColunas(string NomeCampo, string NomeColuna, int Largura, DataGridViewContentAlignment Alinhamento, string Formato)
        {
            _NomeCampo.Add(NomeCampo);
            _Largura.Add(Largura);
            _NomeColuna.Add(NomeColuna);
            _Alinhamento.Add(Alinhamento);
            _Formato.Add(Formato);
        }

        private void CarregaControles()
        {
            //ComboBox
            for (int i = 0; i < _Campo.Count; i++)
            {
                cboCampos.Items.Add(_Apelido[i]);
            }
            cboCampos.SelectedIndex = 0;
            cboPesquisarEm.SelectedIndex = 1;


            sqlGrid = SQL;
            if (!string.IsNullOrEmpty(FiltroAdicional))
            {
                sqlGrid += " where " + FiltroAdicional + " ";
            }
            sqlGrid+= " order by " + _Campo[0];
            if (!Vazio)
            {
                CarregaDados();                
            }            

        }

        private void txtPesquisa_TextChanged(object sender, EventArgs e)
        {
            sqlGrid = "";
            string Tudo = "";

            if (cboPesquisarEm.SelectedIndex == 1)
            {
                Tudo = "%";
            }

            if (txtPesquisa.Text == string.Empty)
            {
                if (!string.IsNullOrEmpty(FiltroAdicional))
                {
                    sqlGrid += SQL + " where " + FiltroAdicional + " ";
                }
                sqlGrid = SQL + " order by " + _Campo[cboCampos.SelectedIndex];
            }
            else
            {
                sqlGrid = SQL + " where " + _Campo[cboCampos.SelectedIndex] + " LIKE '" + Tudo + txtPesquisa.Text + "%' ";
                if (!string.IsNullOrEmpty(FiltroAdicional))
                {
                    sqlGrid += " and " + FiltroAdicional + " ";
                }
                sqlGrid += " order by " + _Campo[cboCampos.SelectedIndex];
            }

            CarregaDados();
                     
        }

        private void cboCampos_SelectionChangeCommitted(object sender, EventArgs e)
        {
            txtPesquisa.Text = string.Empty;
        }

        private void txtPesquisa_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                spGrid.Focus();
                spGrid.Select();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                if (spGrid.Rows.Count > 0)
                {
                    CodRetorno = spGrid.CurrentRow.Cells[0].Value.ToString();
                    SegundoCampoRetorno = spGrid.CurrentRow.Cells[1].Value.ToString();
                    if (spGrid.ColumnCount > 2)
                        TerceiroCampoRetorno = spGrid.CurrentRow.Cells[2].Value.ToString();
                    this.Close();
                }
            }
        }

        private void spGrid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CodRetorno = spGrid.CurrentRow.Cells[1].Value.ToString();
                SegundoCampoRetorno = spGrid.CurrentRow.Cells[1].Value.ToString();
                if (spGrid.ColumnCount > 2)
                        TerceiroCampoRetorno = spGrid.CurrentRow.Cells[2].Value.ToString();
                this.Close();
            }
        }

        public void CarregaDados()
        {
            spGrid.DataSource = classes.db.RetornaDataTable(sqlGrid);

            ////Grid

            for (int i = 0; i < _NomeCampo.Count; i++)
            {
                FormataColuna(i, _NomeColuna[i], _Largura[i], _Alinhamento[i], _Formato[i]);
            }

        }        

        private void FormataColuna(int NumeroColuna, string Titulo, int Largura, DataGridViewContentAlignment Alinhamento, string Formato)
        {
            spGrid.Columns[NumeroColuna].HeaderText = Titulo;
            spGrid.Columns[NumeroColuna].DefaultCellStyle.Alignment = Alinhamento;

            //Largura das Colunas
            if (Largura != 0)
            {
                spGrid.Columns[NumeroColuna].Width = Largura;
            }
            else
            {
                spGrid.Columns[NumeroColuna].Visible = false;
            }

            //Formato das Colunas
            spGrid.Columns[NumeroColuna].DefaultCellStyle.Format = Formato;

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (spGrid.RowCount > 0)
            {
                CodRetorno = spGrid.CurrentRow.Cells[0].Value.ToString();
                SegundoCampoRetorno = spGrid.CurrentRow.Cells[1].Value.ToString();
                if (spGrid.ColumnCount > 2)
                    TerceiroCampoRetorno = spGrid.CurrentRow.Cells[2].Value.ToString();
                this.Close();
            }
            
            else MessageBox.Show("Selecione um registro!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);            

        }

        private void btnExportar_Click(object sender, EventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {

        }
    }

}


