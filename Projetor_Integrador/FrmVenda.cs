using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projetor_Integrador
{
    public partial class FrmVenda : Form
    {
        private Button button;
        private List<Venda> vendas;
        private void limpaCampo()
        {
            txtQtd.Clear();
            txtNProduto.Clear();
            txtPromocao.Clear();
            txtMaterial.Clear();
            maskedtxtPreco.Clear();
        }

        private void localiza(int codigoVenda)
        {
            limpaCampo();
            string sql = @"select Vendas.codvenda, Vendas.codcliente, Vendas.codproduto, Vendas.codestoque, Vendas.qtd, Vendas.nomeproduto, Vendas.preco, Vendas.material, Vendas.promocao from Vendas where Vendas.codvenda, Vendas.codproduto, Vendas.codcliente, Vendas.codestoque = " + codigoVenda;
            DataTable dt = classes.db.RetornaDataTable(sql);
            if (dt.Rows.Count > 0 && dt.Rows[0][0].ToString() != "")
            {
                txtQtd.Text = dt.Rows[0]["qtd"].ToString();
                txtNProduto.Text = dt.Rows[0]["nomeproduto"].ToString();
                maskedtxtPreco.Text = dt.Rows[0]["preco"].ToString();
                txtMaterial.Text = dt.Rows[0]["material"].ToString();
                txtPromocao.Text = dt.Rows[0]["promocao"].ToString();
            }
        }

        public FrmVenda()
        {
            InitializeComponent();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (txtNProduto.Text == "")
            {
                MessageBox.Show("Valor inválido para o campo Nome Produto!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string sql = "";

            if (txtCodigoVenda.Text != "")
            {
                sql = @"update Vendas qtd set = '" + txtQtd.Text + "'" +
                    ", nomeproduto= '" + txtNProduto.Text + "'" +
                    ", preco= '" + maskedtxtPreco + "'" +
                    ", material= '" + txtMaterial + "'" +
                     ", promocao= '" + txtPromocao + "'" +
                "where (codvenda = '" + txtCodigoVenda.Text + ", codcliente = '" + txtCodigoCliente.Text + ", codproduto = '" + txtCodigoProduto.Text + ", codestoque = '" + txtCEstoque.Text + ",)";

                Projetor_Integrador.classes.db.ExecutaComando(sql, false);
            }

            else
            {
                sql = @"INSERT INTO Vendas (codvenda, codproduto, codcliente, codestoque, qtd, nomeproduto, preco, material, promocao)" +
                        "VALUES ('" + txtCodigoVenda.Text + "','" + txtCodigoCliente.Text + "','" + txtCodigoProduto.Text + "','" + txtCEstoque.Text + "','" + txtQtd.Text + "','" + txtNProduto.Text + "','" + maskedtxtPreco.Text + "','" + txtMaterial.Text + "','" + txtPromocao + "')";
                int cod = Projetor_Integrador.classes.db.ExecutaComando(sql, true);
                txtCodigoVenda.Text = cod.ToString();
            }
            MessageBox.Show("Informações salva com sucesso!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            limpaCampo();
            btnNovo.Enabled = false;
            btnExcluir.Enabled = true;
            btnSalvar.Enabled = true;
            btnImprimir.Enabled = false;
            btnPesquisar.Enabled = false;
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (txtCodigoVenda.Text != "" && MessageBox.Show("Desejar excluir este registro?", "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes)
            {
                string sql = @"delete from Vendas where (codvenda=" + txtCodigoVenda.Text + "codcliente=" + txtCodigoCliente.Text + "codproduto=" + txtCodigoProduto.Text + "codestoque=" + txtCEstoque.Text + ")";
                Projetor_Integrador.classes.db.ExecutaComando(sql, false);

                limpaCampo();
            }
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            frmPesquisa f = new frmPesquisa();
            f.Tabela = "Vendas";
            f.AddCampos("qtd", "QTD");
            f.AddCampos("nomeproduto", "Nome Produto");
            f.AddCampos("preco", "preço");
            f.AddCampos("material", "Material");
            f.AddCampos("promocao", "Promoção");

            f.AddColunas("codvenda", "Código Venda", 50, DataGridViewContentAlignment.MiddleLeft, "");
            f.AddColunas("codcliente", "Código Cliente", 110, DataGridViewContentAlignment.MiddleLeft, "");
            f.AddColunas("codproduto", "Código Produto", 200, DataGridViewContentAlignment.MiddleLeft, "");
            f.AddColunas("codestoque", "Código Estoque", 110, DataGridViewContentAlignment.MiddleLeft, "");
            f.AddColunas("qtd", "QTD", 110, DataGridViewContentAlignment.MiddleLeft, "");
            f.AddColunas("nomeproduto", "Nome Produto", 150, DataGridViewContentAlignment.MiddleLeft, "");
            f.AddColunas("preco", "Preço", 150, DataGridViewContentAlignment.MiddleLeft, "");
            f.AddColunas("material", "Material", 150, DataGridViewContentAlignment.MiddleLeft, "");
            f.AddColunas("promocao", "Promoção", 150, DataGridViewContentAlignment.MiddleLeft, "");




            f.SQL = "select codvenda, codproduto, codcliente, codestoque, qtd, nomeproduto, preco, material, promocao from" + f.Tabela;

            f.ShowDialog();

            if (f.CodRetorno != string.Empty)
            {
                localiza(Convert.ToInt32(f.CodRetorno));
            }
            btnExcluir.Enabled = true;
            txtCodigoProduto.Focus();
        }

        private void pCadastroBotoes_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            vendas = LoadvendasFromDatabase();

         
            PrintDocument printDocument = new PrintDocument();
            printDocument.PrintPage += PrintDocument_PrintPage;
            PrintDialog printDialog = new PrintDialog { Document = printDocument };

            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                printDocument.Print();
            }
        }

        private List<Venda> LoadvendasFromDatabase()
        {
            var listavendas = new List<Venda>();
            string connectionString = "Server=DESKTOP-8MFDP0Q; Database=BlueBee; Trusted_Connection=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Vendas";
                using (SqlCommand command = new SqlCommand(query, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var venda = new Venda
                        {
                            codvenda = reader.GetInt32(0), 
                            codproduto = reader.GetString(1), 
                            codcliente = reader.GetString(2), 
                            codestoque = reader.GetString(3), 
                            qtd = reader.GetString(4),
                            nomeproduto = reader.GetString(5),
                            preco = reader.GetString(6), 
                            material = reader.GetString(7),
                            promocao = reader.GetString(8),
                        };
                        listavendas.Add(venda);
                    }
                }
            }
            return listavendas;
        }

        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            float yPos = 100;
            float linhaAltura = 20;
            foreach (var venda in vendas)
            {
                e.Graphics.DrawString($"{venda.codvenda}\t{venda.codproduto}\t{venda.codcliente}\t{venda.codestoque}\t{venda.qtd}\t{venda.nomeproduto}\t{venda.preco}\t{venda.material}\t{venda.promocao}",
                    new Font("Arial", 10), Brushes.Black, 100, yPos);
                yPos += linhaAltura;
            }
        }

        private void txtboxDataNasci_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }
    }


    public class Venda
    {
        public int codvenda { get; set; }
        public string codproduto { get; set; }
        public string codcliente { get; set; }
        public string codestoque { get; set; }
        public string qtd { get; set; }
        public string nomeproduto { get; set; }
        public string preco { get; set; }
        public string material { get; set; }
        public string promocao { get; set; }
    }
}
