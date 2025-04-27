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
    public partial class frmFornecedor : Form

    {
        private Button button;
        private List<fornecedor> fornecedors; 
        private void limpaCampo()
        {
            txtNome.Clear();
            txtboxCnpj.Clear();
            txtEndereco.Clear();
            txtBairro.Clear();
            txtCidade.Clear();
            cboxEstado.SelectedIndex = -1;
            txtboxCep.Clear();
            txtContato.Clear();
            txtTelefone.Clear();
            txtEmail.Clear();
            txtboxDataCadastro.Clear();
            txtLink.Clear();
            txtRedeSocial.Clear();
        }

        private void localiza(int codigoFornecedor)
        {
            limpaCampo();
            string sql = @"select Fornecedor.codfornecedor, Fornecedor.nome, Fornecedor.cnpj, Fornecedor.endereco, Fornecedor.bairro, Fornecedor.cidade, Fornecedor.estado, Fornecedor.cep, Fornecedor.pessoa, Fornecedor.telefone, Fornecedor.email, Fornecedor.datacadastro, Fornecedor.link, Fornecedor.redesocial from Fornecedor where Fornecedor.codfornecedor = " + codigoFornecedor;
            DataTable dt = classes.db.RetornaDataTable(sql);

            if (dt.Rows.Count > 0 && dt.Rows[0][0].ToString() != "")
            {
                txtCodigo.Text = dt.Rows[0]["codfornecedor"].ToString();
                txtNome.Text = dt.Rows[0]["nome"].ToString();
                txtboxCnpj.Text = dt.Rows[0]["cnpj"].ToString();
                txtEndereco.Text = dt.Rows[0]["endereco"].ToString();
                txtBairro.Text = dt.Rows[0]["bairro"].ToString();
                txtCidade.Text = dt.Rows[0]["cidade"].ToString();
                cboxEstado.Text = dt.Rows[0]["estado"].ToString();
                txtboxCep.Text = dt.Rows[0]["cep"].ToString();
                txtContato.Text = dt.Rows[0]["pessoa"].ToString();
                txtTelefone.Text = dt.Rows[0]["telefone"].ToString();
                txtEmail.Text = dt.Rows[0]["email"].ToString();
                txtboxDataCadastro.Text = dt.Rows[0]["datacadastro"].ToString();
                txtLink.Text = dt.Rows[0]["link"].ToString();
                txtRedeSocial.Text = dt.Rows[0]["redesocial"].ToString();
            }
        }

        public frmFornecedor()
        {
            InitializeComponent();
        }

        private void label17_Click(object sender, EventArgs e)
        {

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
                sql = @"update Fornecedor set nome= '" + txtNome.Text + "'" +
                   ", cnpj= '" + txtboxCnpj.Text + "'" +
                    ", endereco= '" + txtEndereco.Text + "'" +
                    ", bairro= '" + txtBairro + "'" +
                    ", cidade= '" + txtCidade.Text + "'" +
                    ", estado= '" + cboxEstado.Text + "'" +
                    ", cep= '" + txtboxCep.Text + "'" +
                    ", pessoa= '" + txtContato.Text + "'" +
                    ", telefone= '" + txtTelefone.Text + "'" +
                    ", email= '" + txtEmail.Text + "'" +
                    ", datacadastro= '" + txtboxDataCadastro.Text + "'" +
                    ", link= '" + txtLink.Text + "'" +
                    ", redesocial= '" + txtRedeSocial.Text + "'" +
                "where (codfornecedor = '" + txtCodigo.Text + "')";

                Projetor_Integrador.classes.db.ExecutaComando(sql, false);
            }

            else
            {
                sql = @"insert into Fornecedor (nome, cnpj, endereco, bairro, cidade, estado, cep, pessoa, telefone, email, datacadastro, link, redesocial)" +
                        "values ('" + txtNome.Text + "','" + txtboxCnpj.Text + "','" + txtEndereco.Text + "','" + txtBairro.Text + "','" + txtCidade.Text + "','" + cboxEstado.Text + "','" + txtboxCep.Text + "','" + txtContato.Text + "','" + txtTelefone.Text + "','" + txtEmail.Text + "','" + txtboxDataCadastro.Text + "','" + txtLink.Text + "','" + txtRedeSocial.Text + "')";
                int cod = Projetor_Integrador.classes.db.ExecutaComando(sql, true);
                txtCodigo.Text = cod.ToString();
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

        private void btnIncluir_Click(object sender, EventArgs e)
        {

        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            frmPesquisa f = new frmPesquisa();
            f.Tabela = "Fornecedor";
            f.AddCampos("nome", "Nome");
            f.AddCampos("cnpj", "CNPJ");
            f.AddCampos("endereco", "Endereço");
            f.AddCampos("cep", "CEP");
            f.AddCampos("bairro", "Bairro");
            f.AddCampos("cidade", "Cidade");
            f.AddCampos("estado", "Estado");
            f.AddCampos("pessoa", "Contato");
            f.AddCampos("telefone", "Telefone");
            f.AddCampos("email", "Email");
            f.AddCampos("datacadastro", "Data Cadastro");
            f.AddCampos("link", "Link");
            f.AddCampos("redesocial", "Rede Social");

            f.AddColunas("codfornecedor", "Código", 50, DataGridViewContentAlignment.MiddleLeft, "");
            f.AddColunas("nome", "Nome", 200, DataGridViewContentAlignment.MiddleLeft, "");
            f.AddColunas("cnpj", "CNPJ", 200, DataGridViewContentAlignment.MiddleLeft, "");
            f.AddColunas("endereco", "Endereço", 110, DataGridViewContentAlignment.MiddleLeft, "");
            f.AddColunas("bairro", "Bairro", 150, DataGridViewContentAlignment.MiddleLeft, "");
            f.AddColunas("cidade", "Cidade", 90, DataGridViewContentAlignment.MiddleLeft, "");
            f.AddColunas("estado", "Estado", 100, DataGridViewContentAlignment.MiddleLeft, "");
            f.AddColunas("cep", "CEP", 100, DataGridViewContentAlignment.MiddleLeft, "");
            f.AddColunas("pessoa", "Contato", 100, DataGridViewContentAlignment.MiddleLeft, "");
            f.AddColunas("telefone", "Telefone", 100, DataGridViewContentAlignment.MiddleLeft, "");
            f.AddColunas("email", "Email", 60, DataGridViewContentAlignment.MiddleLeft, "");
            f.AddColunas("datacadastro", "Data Cadastro", 60, DataGridViewContentAlignment.MiddleLeft, "");
            f.AddColunas("link", "Link", 60, DataGridViewContentAlignment.MiddleLeft, "");
            f.AddColunas("redesocial", "Rede Social", 60, DataGridViewContentAlignment.MiddleLeft, "");

            f.SQL = "select codfornecedor, nome, cnpj, endereco, bairro, cidade, estado, cep, telefone, email, datacadastro, link, redesocial from " + f.Tabela;

            f.ShowDialog();

            if (f.CodRetorno != string.Empty)
            {
                localiza(Convert.ToInt32(f.CodRetorno));
            }
            btnExcluir.Enabled = true;
            txtNome.Focus();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (txtCodigo.Text != "" && MessageBox.Show("Desejar excluir este registro?", "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes)
            {
                string sql = @"delete from Fornecedor where (codfornecedor=" + txtCodigo.Text + ")";
                Projetor_Integrador.classes.db.ExecutaComando(sql, false);

                limpaCampo();
            }
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {

        }

        private void frmFornecedor_Load(object sender, EventArgs e)
        {

        }

        private void txtboxCnpj_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            fornecedors = LoadfornecedorFromDatabase();

            PrintDocument printDocument = new PrintDocument();
            printDocument.PrintPage += PrintDocument_PrintPage;
            PrintDialog printDialog = new PrintDialog { Document = printDocument };

            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                printDocument.Print();
            }
        }

        private List<fornecedor> LoadfornecedorFromDatabase()
        {
            var listafornecedor = new List<fornecedor>();
            string connectionString = "Server=DESKTOP-8MFDP0Q; Database=BlueBee; Trusted_Connection=True;"; 

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Fornecedor";
                using (SqlCommand command = new SqlCommand(query, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var fornecedor = new fornecedor
                        {
                            codfornecedor = reader.GetInt32(0), 
                           Nome = reader.GetString(1),
                            cnpj = reader.GetString(2),
                            Endereco = reader.GetString(2), 
                            Bairro = reader.GetString(3), 
                            Cidade = reader.GetString(4), 
                            Estado = reader.GetString(5), 
                            Cep = reader.GetString(6), 
                            Telefone = reader.GetString(7), 
                            Email = reader.GetString(8),
                            datacadastro = reader.GetString(9),
                            link = reader.GetString(10), 
                            RedeSocial = reader.GetString(12) 
                        };
                        listafornecedor.Add(fornecedor);
                    }
                }
            }
            return listafornecedor;
        }

        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            float yPos = 100;
            float linhaAltura = 20;
            foreach (var fornecedor in fornecedors)
            {
                e.Graphics.DrawString($"t{fornecedor.codfornecedor}\t{fornecedor.Nome}\t{fornecedor.cnpj}\t{fornecedor.Endereco}\t{fornecedor.Bairro}\t{fornecedor.Cidade}\t{fornecedor.Estado}\t{fornecedor.Cep}\t{fornecedor.Telefone}\t{fornecedor.Email}\t{fornecedor.datacadastro}\t{fornecedor.link}\t{fornecedor.RedeSocial}",
                    new Font("Arial", 10), Brushes.Black, 100, yPos);
                yPos += linhaAltura;
            }
        }

    }

    public class fornecedor
    {
        public int codfornecedor { get; set; }
        public string Nome { get; set; }
        public string cnpj { get; set; }
        public string Endereco { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Cep { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string datacadastro { get; set; }
        public string link { get; set; }
        public string RedeSocial { get; set; }
    }
}

