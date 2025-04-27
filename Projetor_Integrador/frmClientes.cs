using Projetor_Integrador.classes;
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
    public partial class frmClientes : Form
    {
        private Button button;
        private List<cliente> clientes; // Supondo que você tenha uma classe Cliente


        private void limpaCampo()  //Limpar as informações 
        {
            txtNome.Clear();
            txtEndereco.Clear();
            txtBairro.Clear();
            txtCidade.Clear();
            cboxEstado.SelectedIndex = -1;
            txtTelefone.Clear();
            txtEmail.Clear();
            txtboxDataNasci.Clear();
            txtboxRg.Clear();
            txtboxCpf.Clear();
            txtRedeSocial.Clear();
        }

        private void localiza(int codigoCliente) //Localiza as informações dentro do SQl
        {
            limpaCampo();
            string sql = @"select Clientes.codcliente, Clientes.nome, Clientes.endereco, Clientes.bairro, Clientes.cidade, Clientes.estado, Clientes.cep, Clientes.telefone, Clientes.email, Clientes.rg, Clientes.cpf, Clientes.redesocial from Clientes where Clientes.codcliente = " + codigoCliente;
            DataTable dt = classes.db.RetornaDataTable(sql);

            if (dt.Rows.Count > 0 && dt.Rows[0][0].ToString() != "")
            {
                txtCodigo.Text = dt.Rows[0]["codcliente"].ToString();
                txtNome.Text = dt.Rows[0]["nome"].ToString();
                txtEndereco.Text = dt.Rows[0]["endereco"].ToString();
                txtBairro.Text = dt.Rows[0]["bairro"].ToString();
                txtCidade.Text = dt.Rows[0]["cidade"].ToString();
                cboxEstado.Text = dt.Rows[0]["estado"].ToString();
                TxtboxCep.Text = dt.Rows[0]["cep"].ToString();
                txtTelefone.Text = dt.Rows[0]["telefone"].ToString();
                txtEmail.Text = dt.Rows[0]["email"].ToString(); ;
                txtboxDataNasci.Text = dt.Rows[0]["datanasc"].ToString();
                txtboxRg.Text = dt.Rows[0]["rg"].ToString();
                txtboxCpf.Text = dt.Rows[0]["cpf"].ToString();
                txtRedeSocial.Text = dt.Rows[0]["redesocial"].ToString();
            }
        }
        public frmClientes()
        {
            InitializeComponent();
        }

        private void btnNovo_Click(object sender, EventArgs e) //Recomeça com novas informações como mesmo código
        {
            limpaCampo();
            btnNovo.Enabled = false;
            btnExcluir.Enabled = true;
            btnSalvar.Enabled = true;
            btnImprimir.Enabled = false;
            btnPesquisar.Enabled = false;

        }

        private void frmClientes_Load(object sender, EventArgs e)
        {

        }

        private void btnSalvar_Click(object sender, EventArgs e) //Salvaram todas as informações no SQL
        {
            if (txtNome.Text == "")
            {
                MessageBox.Show("Valor inválido para o campo Nome!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string sql = "";

            if (txtCodigo.Text != "")//As informações são retiradas do SQL
            {
                sql = @"UPDATE Cliente SET 
                nome = '" + txtNome.Text + "'," +
                            " endereco = '" + txtEndereco.Text + "'," +
                            " bairro = '" + txtBairro.Text + "'," + 
                            " cidade = '" + txtCidade.Text + "'," +
                            " estado = '" + cboxEstado.Text + "'," +
                            " cep = '" + TxtboxCep.Text + "'," +
                            " telefone = '" + txtTelefone.Text + "'," +
                            " email = '" + txtEmail.Text + "'," +
                            " datanasc = '" + txtboxDataNasci.Text + "'," +
                            " rg = '" + txtboxRg.Text + "'," +
                            " cpf = '" + txtboxCpf.Text + "'" + 
                        " WHERE (codcliente = '" + txtCodigo.Text + "')"; 

                Projetor_Integrador.classes.db.ExecutaComando(sql, false);
            }
            else
            {
                sql = @"INSERT INTO Clientes (nome, endereco, bairro, cidade, estado, cep, telefone, email, datanasc, rg, cpf, redesocial) " +
                        "VALUES ('" + txtNome.Text + "','" + txtEndereco.Text + "','" + txtBairro.Text + "','" + txtCidade.Text + "','" + cboxEstado.Text + "','" + TxtboxCep.Text + "','" + txtTelefone.Text + "','" + txtEmail.Text + "','" + txtboxDataNasci.Text + "','" + txtboxRg.Text + "','" + txtboxCpf.Text + "','" + txtRedeSocial.Text + "')";

                int cod = Projetor_Integrador.classes.db.ExecutaComando(sql, true);
                txtCodigo.Text = cod.ToString();
            }

            MessageBox.Show("Informações salvas com sucesso!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

            private void btnExcluir_Click(object sender, EventArgs e)//Excluir todas as informações
        {
            if (txtCodigo.Text != "" && MessageBox.Show("Desejar excluir este registro?", "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes)
            {
                string sql = @"delete from Clientes where (codcliente=" + txtCodigo.Text + ")";
                Projetor_Integrador.classes.db.ExecutaComando(sql, false);

                limpaCampo();
            }
        }

        private void btnPesquisar_Click(object sender, EventArgs e) //Botão localizar os dados que foram inserido.
        {
            frmPesquisa f = new frmPesquisa();
            f.Tabela = "Clientes";
            f.AddCampos("nome", "Nome");
            f.AddCampos("endereco", "Endereço");
            f.AddCampos("bairro", "Bairro");
            f.AddCampos("cidade", "Cidade");
            f.AddCampos("estado", "Estado");
            f.AddCampos("cep", "CEP");
            f.AddCampos("telefone", "Telefone");
            f.AddCampos("email", "Email");
            f.AddCampos("datanasc", "Data Nascimento");
            f.AddCampos("rg", "RG");
            f.AddCampos("cpf", "CPF");
            f.AddCampos("redesocial", "Rede Social");

            f.AddColunas("codcliente", "Código", 50, DataGridViewContentAlignment.MiddleLeft, "");
            f.AddColunas("nome", "Nome", 200, DataGridViewContentAlignment.MiddleLeft, "");
            f.AddColunas("endereco", "Endereço", 110, DataGridViewContentAlignment.MiddleLeft, "");
            f.AddColunas("bairro", "Bairro", 150, DataGridViewContentAlignment.MiddleLeft, "");
            f.AddColunas("cidade", "Cidade", 90, DataGridViewContentAlignment.MiddleLeft, "");
            f.AddColunas("estado", "Estado", 100, DataGridViewContentAlignment.MiddleLeft, "");
            f.AddColunas("cep", "CEP", 100, DataGridViewContentAlignment.MiddleLeft, "");
            f.AddColunas("telefone", "Telefone", 100, DataGridViewContentAlignment.MiddleLeft, "");
            f.AddColunas("email", "Email", 60, DataGridViewContentAlignment.MiddleLeft, "");
            f.AddColunas("datanasc", "Data Nascimento", 60, DataGridViewContentAlignment.MiddleLeft, "");
            f.AddColunas("rg", "RG", 60, DataGridViewContentAlignment.MiddleLeft, "");
            f.AddColunas("cpf", "CPF", 60, DataGridViewContentAlignment.MiddleLeft, "");
            f.AddColunas("redesocial", "Rede Social", 60, DataGridViewContentAlignment.MiddleLeft, "");

            f.SQL = "select codcliente, nome, endereco, bairro, cidade, estado, cep, telefone, email, datanasc, rg, cpf, redesocial from " + f.Tabela;

            f.ShowDialog();

            if (f.CodRetorno != string.Empty)
            {
                localiza(Convert.ToInt32(f.CodRetorno));
            }
            btnExcluir.Enabled = true;
            txtNome.Focus();
        }

        private void btnIncluir_Click(object sender, EventArgs e)
        {

        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {

        }

        private void btnImprimir_Click(object sender, EventArgs e) //Botão imprimir as informações
        {
            // Aqui você pode adicionar a lógica para carregar e imprimir os clientes
            // Carrega os dados do banco de dados
            clientes = LoadClientesFromDatabase();

            // Inicia a impressão
            PrintDocument printDocument = new PrintDocument();
            printDocument.PrintPage += PrintDocument_PrintPage;
            PrintDialog printDialog = new PrintDialog { Document = printDocument };

            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                printDocument.Print();
            }
        }

        private List<cliente> LoadClientesFromDatabase() //Conecta o banco de dados com SQL
        {
            var listaClientes = new List<cliente>();
            string connectionString = "Server=DESKTOP-8MFDP0Q; Database=BlueBee; Trusted_Connection=True;"; // Altere para a sua string de conexão

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Clientes";
                using (SqlCommand command = new SqlCommand(query, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var cliente = new cliente
                        {
                            CodCliente = reader.GetInt32(0), // codcliente
                            Nome = reader.GetString(1), // nome
                            Endereco = reader.GetString(2), // endereco
                            Bairro = reader.GetString(3), // bairro
                            Cidade = reader.GetString(4), // cidade
                            Estado = reader.GetString(5), // estado
                            Cep = reader.GetString(6), // cep
                            Telefone = reader.GetString(7), // telefone
                            Email = reader.GetString(8), // email
                            DataNasc = reader.GetString(9), // datanasc
                            Rg = reader.GetString(10), // rg
                            Cpf = reader.GetString(11), // cpf
                            RedeSocial = reader.GetString(12) // redesocial
                        };
                        listaClientes.Add(cliente);
                    }
                }
            }
            return listaClientes;//Lista das informações
        }

        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            float yPos = 100;
            float linhaAltura = 20;
            foreach (var cliente in clientes)
            {
                e.Graphics.DrawString($"t{cliente.CodCliente}\t{cliente.Nome}\t{cliente.Endereco}\t{cliente.Bairro}\t{cliente.Cidade}\t{cliente.Estado}\t{cliente.Cep}\t{cliente.Telefone}\t{cliente.Email}\t{cliente.DataNasc}\t{cliente.Rg}\t{cliente.Cpf}\t{cliente.RedeSocial}",
                    new Font("Arial", 10), Brushes.Black, 100, yPos);
                yPos += linhaAltura;
            }
        }

        private void txtboxDataNasci_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }


    // A classe Cliente deve estar definida em algum lugar do seu projeto
    public class cliente
    {
        public int CodCliente { get; set; }
        public string Nome { get; set; }
        public string Endereco { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Cep { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string DataNasc { get; set; }
        public string Rg { get; set; }
        public string Cpf { get; set; }
        public string RedeSocial { get; set; }
    }


}

