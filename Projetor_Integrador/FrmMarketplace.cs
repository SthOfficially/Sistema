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
using static Projetor_Integrador.FrmMarketplace;

namespace Projetor_Integrador
{
    public partial class FrmMarketplace : Form
    {
        private Button button;
        private List<Marketplace> MarketPlace; 

        private void limpaCampo()
        {
            txtNome.Clear();
        }
        private void localiza(int codigoMarketPlace)
        {
            limpaCampo();
            string sql = @"select MarketPlace.CodMarketPlace, MarketPlace.nome, MarketPlace.codproduto from MarketPlace where MarketPlace .CodMarketPlace, MarketPlace.codproduto = " + codigoMarketPlace;
            DataTable dt = classes.db.RetornaDataTable(sql);
            if (dt.Rows.Count > 0 && dt.Rows[0][0].ToString() != "")
            {
                txtNome.Text = dt.Rows[0]["nome"].ToString();
            }
        }
        public FrmMarketplace()
        {
            InitializeComponent();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            {
                if (txtNome.Text == "")
                {
                    MessageBox.Show("Valor inválido para o campo Nome!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                string sql = "";

                if (txtCodMarketPlace.Text != "")
                {
                    sql = @"update MarketPlace set nome = '" + txtNome.Text + "'" +
                    "where (CodMarketPlace = '" + txtCodMarketPlace.Text + ", codproduto = '" + txtCodProduto.Text + ",)";

                    Projetor_Integrador.classes.db.ExecutaComando(sql, false);
                }

                else
                {
                    sql = @"insert into MarketPlace (nome)" +
                            "values ('" + txtNome.Text + "')";
                    int cod = Projetor_Integrador.classes.db.ExecutaComando(sql, true);
                    txtCodMarketPlace.Text = cod.ToString();
                    txtCodProduto.Text = cod.ToString();
                }
                MessageBox.Show("Informações salva com sucesso!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
            private void btnPesquisar_Click(object sender, EventArgs e)
        {
            frmPesquisa f = new frmPesquisa();
            f.Tabela = "MarketPlace";
            f.AddCampos("nome", "Nome");

            f.AddColunas("CodMarketPlace", "Código MarketPlace", 50, DataGridViewContentAlignment.MiddleLeft, "");
            f.AddColunas("codproduto", "Código Produto", 50, DataGridViewContentAlignment.MiddleLeft, "");
            f.AddColunas("nome", "Nome", 200, DataGridViewContentAlignment.MiddleLeft, "");

            f.SQL = "select CodMarketPlace, codproduto, nome from " + f.Tabela;
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

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            MarketPlace = LoadMarketPlaceFromDatabase();

            PrintDocument printDocument = new PrintDocument();
            printDocument.PrintPage += PrintDocument_PrintPage;
            PrintDialog printDialog = new PrintDialog { Document = printDocument };

            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                printDocument.Print();
            }
        }

        private List<Marketplace> LoadMarketPlaceFromDatabase()
        {
            var listaMarketplace = new List<Marketplace>();
            string connectionString = "Server=DESKTOP-8MFDP0Q; Database=BlueBee; Trusted_Connection=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM MarketPlace";
                using (SqlCommand command = new SqlCommand(query, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var Marketplace = new Marketplace
                        {
                            CodMarketPlace = reader.GetInt32(0),
                            Nome = reader.GetString(1), 
                            codproduto = reader.GetString(2), 
                        };
                        listaMarketplace.Add(Marketplace);
                    }
                }
            }
            return listaMarketplace;
        }

        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            float yPos = 100;
            float linhaAltura = 20;
            foreach (var MarketPlace in MarketPlace)
            {
                e.Graphics.DrawString($"{MarketPlace.CodMarketPlace}\t{MarketPlace.Nome}\t{MarketPlace.codproduto}",
                    new Font("Arial", 10), Brushes.Black, 100, yPos);
                yPos += linhaAltura;
            }
        }
        public class Marketplace
        {
            public int CodMarketPlace { get; set; }
            public string Nome { get; set; }
            public string codproduto { get; set; }
        }

        private void FrmMarketplace_Load(object sender, EventArgs e)
        {

        }
    }
}
