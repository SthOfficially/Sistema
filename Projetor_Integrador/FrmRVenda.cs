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
    public partial class FrmRVenda : Form
    {
        private Button button;
        private List<Rvenda> rvendas; 
        private void limpaCampo()
        {
            cboxFPagamento.SelectedIndex = -1;
            maskedtxtTotalVenda.Clear();
            maskedtxtValorReceita.Clear();
        }
        private void localiza(int codigoRvenda)
        {
            limpaCampo();
            string sql = @"select Rvenda.codrvenda, Rvenda.codcliente, Rvenda.codproduto, Rvenda.codestoque, Rvenda.formapagamento, Rvenda.totalvenda, Rvenda.totalreceita from Rvenda where Rvenda .codrvenda, Rvenda.codcliente, Rvenda.codproduto, Rvenda.codestoque = " + codigoRvenda;
            DataTable dt = classes.db.RetornaDataTable(sql);
            if (dt.Rows.Count > 0 && dt.Rows[0][0].ToString() != "")
            {
                cboxFPagamento.Text = dt.Rows[0]["formapagamento"].ToString();
                maskedtxtTotalVenda.Text = dt.Rows[0]["totalvenda"].ToString();
                maskedtxtValorReceita.Text = dt.Rows[0]["totalreceita"].ToString();
            }
        }

        public FrmRVenda()
        {
            InitializeComponent();
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            frmPesquisa f = new frmPesquisa();
            f.Tabela = "Rvenda";
            f.AddCampos("formapagamento", "Forma Pagamento");
            f.AddCampos("totalvenda", "Total Venda");
            f.AddCampos("totalreceita", "Total Receita");

            f.AddColunas("cdrvenda", "Código Venda", 50, DataGridViewContentAlignment.MiddleLeft, "");
            f.AddColunas("codcliente", "Código Cliente", 50, DataGridViewContentAlignment.MiddleLeft, "");
            f.AddColunas("codproduto", "Código Produto", 50, DataGridViewContentAlignment.MiddleLeft, "");
            f.AddColunas("codestoque", "Código Estoque", 50, DataGridViewContentAlignment.MiddleLeft, "");
            f.AddColunas("formapagamento", "Forma Pagamento", 50, DataGridViewContentAlignment.MiddleLeft, "");
            f.AddColunas("totalvenda", "Total venda", 50, DataGridViewContentAlignment.MiddleLeft, "");
            f.AddColunas("totalreceita", "Total receita", 50, DataGridViewContentAlignment.MiddleLeft, "");

            f.SQL = "select codrvenda, codcliente, codproduto, codestoque, formapagamento, totalvenda, totalreceita from " + f.Tabela;
            f.ShowDialog();
            if (f.CodRetorno != string.Empty)
            {
                localiza(Convert.ToInt32(f.CodRetorno));
            }

        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            limpaCampo();
            btnNovo.Enabled = false;
            btnSalvar.Enabled = true;
            btnImprimir.Enabled = false;
            btnPesquisar.Enabled = false;

        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (maskedtxtTotalVenda.Text == "")
            {
                MessageBox.Show("Valor inválido para o campo Nome!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string sql = "";

            if (txtCRelatorio.Text != "")
            {
                sql = @"update Rvenda set formapagamento = '" + cboxFPagamento.Text + "'" +
                    ", totalvenda= '" + maskedtxtTotalVenda.Text + "'" +
                    ", totalreceita= '" + maskedtxtValorReceita + "'" +
                "where (codrvenda = '" + txtCRelatorio.Text + ", codcliente = '" + txtCCliente.Text + ", codproduto = '" + txtCProduto.Text + ", codestoque = '" + txtCEstoque.Text + ",)";

                Projetor_Integrador.classes.db.ExecutaComando(sql, false);
            }

            else
            {
                sql = @"insert into Rvenda (formapagamento, totalvenda, totalreceita)" +
                        "values ('" + cboxFPagamento.Text + "','" + maskedtxtTotalVenda.Text + "','" + maskedtxtValorReceita + "')";
                int cod = Projetor_Integrador.classes.db.ExecutaComando(sql, true);
                txtCRelatorio.Text = cod.ToString();
                txtCCliente.Text = cod.ToString();
                txtCProduto.Text = cod.ToString();
                txtCEstoque.Text = cod.ToString();
            }
            MessageBox.Show("Informações salva com sucesso!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void FrmRVenda_Load(object sender, EventArgs e)
        {

        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            
            rvendas = LoadRvendaFromDatabase();

            
            PrintDocument printDocument = new PrintDocument();
            printDocument.PrintPage += PrintDocument_PrintPage;
            PrintDialog printDialog = new PrintDialog { Document = printDocument };

            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                printDocument.Print();
            }
        }

        private List<Rvenda> LoadRvendaFromDatabase()
        {
            var listaRvenda = new List<Rvenda>();
            string connectionString = "Server=DESKTOP-8MFDP0Q; Database=BlueBee; Trusted_Connection=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Rvenda";
                using (SqlCommand command = new SqlCommand(query, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var rvenda = new Rvenda
                        {
                            codrvenda = reader.GetInt32(0), 
                            codcliente = reader.GetString(1), 
                            codproduto = reader.GetString(2), 
                            codestoque = reader.GetString(3), 
                            formapagamento = reader.GetString(4), 
                            totalvenda = reader.GetString(5), 
                            totalreceita = reader.GetString(6), 
                        };
                        listaRvenda.Add(rvenda);
                    }
                }
            }
            return listaRvenda;
        }

        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            float yPos = 100;
            float linhaAltura = 20;
            foreach (var rvenda in rvendas)
            {
                e.Graphics.DrawString($"{rvenda.codrvenda}\t{rvenda.codcliente}\t{rvenda.codproduto}\t{rvenda.codestoque}\t{rvenda.formapagamento}\t{rvenda.totalvenda}\t{rvenda.totalreceita}",
                    new Font("Arial", 10), Brushes.Black, 100, yPos);
                yPos += linhaAltura;
            }

        }
        public class Rvenda
        {
            public int codrvenda { get; set; }
            public string codcliente { get; set; }
            public string codproduto { get; set; }
            public string codestoque { get; set; }
            public string formapagamento { get; set; }
            public string totalvenda { get; set; }
            public string totalreceita { get; set; }
        }

    }


}
