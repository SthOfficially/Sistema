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
    public partial class frmProdutos : Form
    {
        private Button button;
        private List<Produto> produtos;
        private void limpaCampo()
        {
            txtNome.Clear();
            txtTamanho.Clear();
            txtboxValidade.Clear();
            txtboxPrecoCusto.Clear();
            txtboxPrecoVenda.Clear();
            txtTempoProducao.Clear();
            txtPeso.Clear();
            txtDimensoes.Clear();
            txtMaterial.Clear();
            txtAcabamento.Clear();
            txtGarantia.Clear();
        }

        private void localiza(int codigoProdutos)
        {
            limpaCampo();
            string sql = @"select Produtos.codproduto, Produtos.codfornecedor, Produtos.nome, Produtos.tamanho, Produtos.validade, Produtos.precocusto, Produtos.precovenda, Produtos.tempoproducao, Produtos.peso, Produtos.dimensoes, Produtos.material, Produtos.acabamento, Produtos.garantia, from Produtos where Produtos.codproduto, Produtos.codfornecedor = " + codigoProdutos;
            DataTable dt = classes.db.RetornaDataTable(sql);

            if (dt.Rows.Count > 0 && dt.Rows[0][0].ToString() != "")
            {
                txtCodigo.Text = dt.Rows[0]["codproduto"].ToString();
                textBox1.Text = dt.Rows[0]["codfornecedor"].ToString();
                txtNome.Text = dt.Rows[0]["nome"].ToString();
                txtTamanho.Text = dt.Rows[0]["tamanho"].ToString();
                txtboxValidade.Text = dt.Rows[0]["validade"].ToString();
                txtboxPrecoCusto.Text = dt.Rows[0]["precocusto"].ToString();
                txtboxPrecoVenda.Text = dt.Rows[0]["precovenda"].ToString();
                txtTempoProducao.Text = dt.Rows[0]["tempoproducao"].ToString();
                txtPeso.Text = dt.Rows[0]["peso"].ToString();
                txtDimensoes.Text = dt.Rows[0]["dimensoes"].ToString();
                txtMaterial.Text = dt.Rows[0]["material"].ToString();
                txtAcabamento.Text = dt.Rows[0]["acabamento"].ToString();
                txtGarantia.Text = dt.Rows[0]["garantia"].ToString();
            }
        }

        public frmProdutos()
        {
            InitializeComponent();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void frmProdutos_Load(object sender, EventArgs e)
        {

        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (txtCodigo.Text != "" && MessageBox.Show("Desejar excluir este registro?", "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes)
            {
                string sql = @"delete from Produtos where (codproduto=" + txtCodigo.Text + ")";
                Projetor_Integrador.classes.db.ExecutaComando(sql, false);

                limpaCampo();
            }
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

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (txtNome.Text == "")
            {
                MessageBox.Show("Valor inválido para o campo Nome!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string sql = "";

            if (txtCodigo.Text != "")
            {
                sql = @"update Produtos set nome= '" + txtNome.Text + "'" +
                    ", tamanho= '" + txtTamanho.Text + "'" +
                    ", validade= '" + txtboxValidade.Text + "'" +
                    ", precocusto= '" + txtboxPrecoCusto.Text + "'" +
                    ", precovenda= '" + txtboxPrecoVenda.Text + "'" +
                    ", tempoproducao= '" + txtTempoProducao.Text + "'" +
                    ", peso= '" + txtPeso.Text + "'" +
              ", dimensoes= '" + txtDimensoes.Text + "'" +
              ", material= '" + txtMaterial.Text + "'" +
              ", acabamento= '" + txtAcabamento.Text + "'" +
              ", garantia= '" + txtGarantia.Text + "'" +
              "where (codproduto = '" + txtCodigo.Text + "'" +
                "where (codfornecedor = '" + txtCodigo.Text + "')";

                Projetor_Integrador.classes.db.ExecutaComando(sql, false);
            }

            else
            {
                sql = @"insert into Produtos (nome, tamanho, validade, precocusto, precovenda, tempoproducao, peso, dimensoes, material, acabamento, garantia)" +
                        "values ('" + txtNome.Text + "','" + txtTamanho.Text + "','" + txtboxValidade.Text + "','" + txtboxPrecoCusto.Text + "','" + txtboxPrecoVenda.Text + "','" + txtTempoProducao.Text + "','" + txtPeso.Text + "','" + txtDimensoes.Text + "','" + txtMaterial.Text + "','" + txtAcabamento.Text + "','" + txtGarantia.Text + "')";
                int cod = Projetor_Integrador.classes.db.ExecutaComando(sql, true);
                txtCodigo.Text = cod.ToString();
            }
            MessageBox.Show("Informações salva com sucesso!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnPesquisar_Click_1(object sender, EventArgs e)
        {
            frmPesquisa f = new frmPesquisa();
            f.Tabela = "Produtos";
            f.AddCampos("nome", "Nome");
            f.AddCampos("tamanho", "Tamanho");
            f.AddCampos("validade", "Validade");
            f.AddCampos("precocusto", "Preço Custo");
            f.AddCampos("precovenda", "Preço Venda");
            f.AddCampos("tempoproducao", "Tempo Produção");
            f.AddCampos("peso", "Peso");
            f.AddCampos("dimensoes", "Dimensões");
            f.AddCampos("material", "Material");
            f.AddCampos("acabamento", "Acabamento");
            f.AddCampos("garantia", "Garantia");

            f.AddColunas("codproduto", "Código", 50, DataGridViewContentAlignment.MiddleLeft, "");
            f.AddColunas("codfornecedor", "Código", 50, DataGridViewContentAlignment.MiddleLeft, "");
            f.AddColunas("nome", "Nome", 200, DataGridViewContentAlignment.MiddleLeft, "");
            f.AddColunas("tamanho", "Tamanho", 110, DataGridViewContentAlignment.MiddleLeft, "");
            f.AddColunas("validade", "Validade", 150, DataGridViewContentAlignment.MiddleLeft, "");
            f.AddColunas("precocusto", "Preço Custo", 90, DataGridViewContentAlignment.MiddleLeft, "");
            f.AddColunas("precovenda", "preço Venda", 100, DataGridViewContentAlignment.MiddleLeft, "");
            f.AddColunas("tempoproducao", "Tempo Produção", 100, DataGridViewContentAlignment.MiddleLeft, "");
            f.AddColunas("peso", "Peso", 60, DataGridViewContentAlignment.MiddleLeft, "");
            f.AddColunas("dimensoes", "Dimensões", 60, DataGridViewContentAlignment.MiddleLeft, "");
            f.AddColunas("material", "Material", 60, DataGridViewContentAlignment.MiddleLeft, "");
            f.AddColunas("acabamento", "Acabamento", 60, DataGridViewContentAlignment.MiddleLeft, "");
            f.AddColunas("garantia", "Garantia", 60, DataGridViewContentAlignment.MiddleLeft, "");

            f.SQL = "select codproduto, codfornecedor, nome, tamanho, validade, precocusto, precovenda, tempoproducao, peso, dimensoes, material, acabamento, garantia from " + f.Tabela;

            f.ShowDialog();

            if (f.CodRetorno != string.Empty)
            {
                localiza(Convert.ToInt32(f.CodRetorno));
            }
            btnExcluir.Enabled = true;
            txtNome.Focus();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            produtos = LoadprodutosFromDatabase();


            PrintDocument printDocument = new PrintDocument();
            printDocument.PrintPage += PrintDocument_PrintPage;
            PrintDialog printDialog = new PrintDialog { Document = printDocument };

            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                printDocument.Print();
            }
        }

        private List<Produto> LoadprodutosFromDatabase()
        {
            var listaProduto = new List<Produto>();
            string connectionString = "Server=DESKTOP-8MFDP0Q; Database=BlueBee; Trusted_Connection=True;"; // Altere para a sua string de conexão

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Produtos";
                using (SqlCommand command = new SqlCommand(query, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var Produto = new Produto
                        {
                            codproduto = reader.GetInt32(0), 
                            codfornecedor = reader.GetString(1), 
                            nome = reader.GetString(2),
                            tamanho = reader.GetString(3),
                            validade = reader.GetString(4),
                            precocusto = reader.GetString(5),
                            precovenda = reader.GetString(6),
                            tempoproducao = reader.GetString(7), 
                            peso = reader.GetString(8), 
                            dimensoes = reader.GetString(9), 
                            material = reader.GetString(10), 
                            acabamento = reader.GetString(11), 
                            garantia = reader.GetString(12) 
                        };

                        listaProduto.Add(Produto);
                    }
                }
            }
            return listaProduto;
        }

        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            float yPos = 100;
            float linhaAltura = 20;
            foreach (var produto in produtos)
            {
                e.Graphics.DrawString($"\t{produto.codproduto}\t{produto.codfornecedor}\t{produto.nome}\t{produto.tamanho}\t{produto.validade}\t{produto.precocusto}\t{produto.precovenda}\t{produto.tempoproducao}\t{produto.peso}\t{produto.dimensoes}\t{produto.material}\t{produto.acabamento}\t{produto.garantia}",
                    new Font("Arial", 10), Brushes.Black, 100, yPos);
                yPos += linhaAltura;
            }
        }

    }

    public class Produto
    {
        public int codproduto { get; set; }
        public string codfornecedor { get; set; }
        public string nome { get; set; }
        public string tamanho { get; set; }
        public string validade { get; set; }
        public string precocusto { get; set; }
        public string precovenda { get; set; }
        public string tempoproducao { get; set; }
        public string peso { get; set; }
        public string dimensoes { get; set; }
        public string material { get; set; }
        public string acabamento { get; set; }
        public string garantia { get; set; }
    }
}

