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
    public partial class FrmEstoque : Form
    {
        private Button button;
        private List<Estoque> estoques; 

        private void limpaCampo()
        {
            txtQtd.Clear();
        }

        private void localiza(int codigoEstoque)
        {
            limpaCampo();
            string sql = @"select Estoque.codestoque, Estoque.codproduto, Estoque.codvenda, Estoque.qtd from Estoque where Estoque .codestoque, Estoque.codproduto, Estoque.codvenda = " + codigoEstoque;
            DataTable dt = classes.db.RetornaDataTable(sql);
            if (dt.Rows.Count > 0 && dt.Rows[0][0].ToString() != "")
            {
                txtQtd.Text = dt.Rows[0]["qtd"].ToString();
            }
        }

        public FrmEstoque()
        {
            InitializeComponent();
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            limpaCampo();
            btnNovo.Enabled = false;
            btnSalvar.Enabled = true;
            btnImprimir.Enabled = false;
            btnPesquisar.Enabled = false;
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            frmPesquisa f = new frmPesquisa();
            f.Tabela = "Estoque";
            f.AddCampos("qtd", "QTD");

            f.AddColunas("codestoque", "Código Estoque", 50, DataGridViewContentAlignment.MiddleLeft, "");
            f.AddColunas("codproduto", "Código Produto", 50, DataGridViewContentAlignment.MiddleLeft, "");
            f.AddColunas("codvenda", "Código Venda", 50, DataGridViewContentAlignment.MiddleLeft, "");
            f.AddColunas("qtd", "QTD", 50, DataGridViewContentAlignment.MiddleLeft, "");

            f.SQL = "select codestoque, codproduto, codvenda, qtd from " + f.Tabela;
            f.ShowDialog();
            if (f.CodRetorno != string.Empty)
            {
                localiza(Convert.ToInt32(f.CodRetorno));
            }

        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (txtQtd.Text == "")
            {
                MessageBox.Show("Valor inválido para o campo Nome!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string sql = "";

            if (txtCodEstoque.Text != "")
            {
                sql = @"update Estoque set qtd = '" + txtQtd.Text + "'" +
                "where (codestoque = '" + txtCodEstoque.Text + ", codproduto = '" +txtCodProduto.Text + ", codvenda = '" + txtCodVenda.Text + ",)";

                Projetor_Integrador.classes.db.ExecutaComando(sql, false);
            }

            else
            {
                sql = @"insert into Estoque (qtd)" +
                        "values ('" + txtQtd.Text + "')";
                int cod = Projetor_Integrador.classes.db.ExecutaComando(sql, true);
                txtCodEstoque.Text = cod.ToString();
                txtCodProduto.Text = cod.ToString();
                txtCodVenda.Text = cod.ToString();
            }
            MessageBox.Show("Informações salva com sucesso!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void FrmEstoque_Load(object sender, EventArgs e)
        {

        }

        private void btnImprimir_Click_1(object sender, EventArgs e)
        {
            estoques = LoadEstoqueFromDatabase(); 

            PrintDocument printDocument = new PrintDocument();
            printDocument.PrintPage += PrintDocument_PrintPage;
            PrintDialog printDialog = new PrintDialog { Document = printDocument };

            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                printDocument.Print();
            }
        }

        private List<Estoque> LoadEstoqueFromDatabase() 
        {
            var listaEstoque = new List<Estoque>();
            string connectionString = "Server=DESKTOP-8MFDP0Q; Database=BlueBee; Trusted_Connection=True"; 

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Estoque"; 
                using (SqlCommand command = new SqlCommand(query, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var estoques = new Estoque 
                        {
                            codEstoque = reader.GetInt32(0), 
                            codProduto = reader.GetString(1), 
                            Qtd = reader.GetString(1) 
                        };
                        listaEstoque.Add(estoques);
                    }
                }
            }
            return listaEstoque;
        }

        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            float yPos = 100;
            float linhaAltura = 20;
            foreach (var Estoque in estoques) 
            {
                e.Graphics.DrawString($"{Estoque.codEstoque}\t{Estoque.codProduto}\t{Estoque.Qtd}" , 
                    new Font("Arial", 10), Brushes.Black, 100, yPos);
                yPos += linhaAltura;
            }
        }

        public class Estoque
        {
            public int codEstoque { get; set; }
            public string codProduto { get; set; }
            public string Qtd { get; set; }
        }
    }
}
    
    
