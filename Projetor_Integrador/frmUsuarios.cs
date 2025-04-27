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
    public partial class frmUsuarios : Form
    {
        private Button button;
        private List<Usuario> usuarios; 
        private void limpaCampo()
        {
            txtNome.Clear();
            txtTelefone.Clear();
            txtEmail.Clear();
            txtboxDataNasc.Clear();
            txtboxRg.Clear();
            txtboxCpf.Clear();
            txtUsuario.Clear();
            txtSenha.Clear();
        }

        private void localiza(int codigoUsuario)
        {
            limpaCampo();
            string sql = @"select Usuario.Idusuario, Usuario.nome, Usuario.telefone, Usuario.email, Usuario.datanasc, Usuario.rg, Usuario.cpf, from Usuario where Usuario .Idusuario = " + codigoUsuario;
            DataTable dt = classes.db.RetornaDataTable(sql);

            if (dt.Rows.Count > 0 && dt.Rows[0][0].ToString() != "")
            {
                txtCodigo.Text = dt.Rows[0]["Idusuario"].ToString();
                txtNome.Text = dt.Rows[0]["nome"].ToString();
                txtTelefone.Text = dt.Rows[0]["telefone"].ToString();
                txtEmail.Text = dt.Rows[0]["email"].ToString();
                txtboxDataNasc.Text = dt.Rows[0]["datanasc"].ToString();
                txtboxRg.Text = dt.Rows[0]["rg"].ToString();
                txtboxCpf.Text = dt.Rows[0]["cpf"].ToString();
                txtUsuario.Text = dt.Rows[0]["usuario"].ToString();
                txtSenha.Text = dt.Rows[0]["senha"].ToString();
            }
        }
        public frmUsuarios()
        {
            InitializeComponent();
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
                sql = @"update Usuario set nome= '" + txtNome.Text + "'" +
                   ", telefone= '" + txtTelefone.Text + "'" +
                   "' email= '" + txtEmail.Text + "'" +
                   "' datanasc= '" + txtboxDataNasc.Text + "'" +
                    ", rg= '" + txtboxRg + "'" +
                    ", cpf= '" + txtboxCpf.Text + "'" +
                    ", usuario= '" + txtUsuario.Text + "'" +
                    "' senha= '" + txtSenha.Text + "'" +
                "where (codusuarios = '" + txtCodigo.Text + "')";

                Projetor_Integrador.classes.db.ExecutaComando(sql, false);
            }

            else
            {
                sql = @"insert into Usuario (nome, telefone, email, datanasc, rg, cpf, usuario, senha)" +
                        "values ('" + txtNome.Text + "','" + txtTelefone.Text + "','" + txtEmail.Text + "','" + txtboxDataNasc.Text + "','" + txtboxRg.Text + "','" + txtboxCpf.Text + "','" + txtUsuario.Text + "','" + txtSenha.Text + "')";
                int cod = Projetor_Integrador.classes.db.ExecutaComando(sql, true);
                txtCodigo.Text = cod.ToString();
            }
            MessageBox.Show("Informações salva com sucesso!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (txtCodigo.Text != "" && MessageBox.Show("Desejar excluir este registro?", "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes)
            {
                string sql = @"delete from Usuario where (Idusuario=" + txtCodigo.Text + ")";
                Projetor_Integrador.classes.db.ExecutaComando(sql, false);

                limpaCampo();
            }
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            frmPesquisa f = new frmPesquisa();
            f.Tabela = "Usuario";
            f.AddCampos("nome", "Nome");
            f.AddCampos("telefone", "Telefone");
            f.AddCampos("email", "Email");
            f.AddCampos("datanasc", "Data Nascimento");
            f.AddCampos("rg", "RG");
            f.AddCampos("cpf", "CPF");
            f.AddCampos("usuario", "Usuario");
            f.AddCampos("senha", "Senha");

            f.AddColunas("Idusuario", "Código", 50, DataGridViewContentAlignment.MiddleLeft, "");
            f.AddColunas("nome", "Nome", 200, DataGridViewContentAlignment.MiddleLeft, "");
            f.AddColunas("telefone", "Telefone", 110, DataGridViewContentAlignment.MiddleLeft, "");
            f.AddColunas("email", "Email", 150, DataGridViewContentAlignment.MiddleLeft, "");
            f.AddColunas("datanasci", "Data Nascimento", 90, DataGridViewContentAlignment.MiddleLeft, "");
            f.AddColunas("rg", "RG", 100, DataGridViewContentAlignment.MiddleLeft, "");
            f.AddColunas("cpf", "CPF", 100, DataGridViewContentAlignment.MiddleLeft, "");
            f.AddColunas("usuario", "Usuario", 60, DataGridViewContentAlignment.MiddleLeft, "");
            f.AddColunas("senha", "Senha", 60, DataGridViewContentAlignment.MiddleLeft, "");

            f.SQL = "select Idusuario, nome, telefone, email, datanasc,rg, cpf, usuario, senha from " + f.Tabela;

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
            usuarios = LoadusuariosFromDatabase();

            
            PrintDocument printDocument = new PrintDocument();
            printDocument.PrintPage += PrintDocument_PrintPage;
            PrintDialog printDialog = new PrintDialog { Document = printDocument };

            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                printDocument.Print();
            }
        }

        private List<Usuario> LoadusuariosFromDatabase()
        {
            var listaUsuario = new List<Usuario>();
            string connectionString = "Server=DESKTOP-8MFDP0Q; Database=BlueBee; Trusted_Connection=True;"; // Altere para a sua string de conexão

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Usuario";
                using (SqlCommand command = new SqlCommand(query, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var Usuarios = new Usuario
                        {
                            Idusuario = reader.GetInt32(0),
                            Nome = reader.GetString(1), 
                            Telefone = reader.GetString(7), 
                            Email = reader.GetString(8), 
                            DataNasc = reader.GetString(9), 
                            Rg = reader.GetString(10), 
                            Cpf = reader.GetString(11), 
                            usuario = reader.GetString(12),
                            senha = reader.GetString(12)
                        };

                        listaUsuario.Add(Usuarios);
                    }
                }
            }
            return listaUsuario;
        }

        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            float yPos = 100;
            float linhaAltura = 20;
            foreach (var usuario in usuarios)
            {
                e.Graphics.DrawString($"\t{usuario.Idusuario}\t{usuario.Nome}\t{usuario.Telefone}\t{usuario.Email}\t{usuario.DataNasc}\t{usuario.Rg}\t{usuario.Cpf}\t{usuario.usuario}\t{usuario.senha}",
                    new Font("Arial", 10), Brushes.Black, 100, yPos);
                yPos += linhaAltura;
            }
        }

        private void txtboxDataNasci_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

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
    }
    public class Usuario
    {
        public int Idusuario { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string DataNasc { get; set; }
        public string Rg { get; set; }
        public string Cpf { get; set; }
        public string usuario { get; set; }
        public string senha { get; set; }
    }
}
    
    

