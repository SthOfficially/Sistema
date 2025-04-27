using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace Projetor_Integrador
{
    public partial class Login : Form
    {


        private string GerarSenhaTemporaria()
        {
            // Gerar uma senha aleatória com 6 caracteres, por exemplo
            Random random = new Random();
            int senhaTemporaria = random.Next(100000, 999999); // Número entre 100000 e 999999
            return senhaTemporaria.ToString();
        }

        private Boolean Validacao(string v_username, string v_senha)
        {
            string usuario_banco = "";
            string senha_banco = "";

            string sql = @"select IdUsuario, usuario, senha from tblUsuario
                    where usuario = '" + v_username + "' and senha = '" + v_senha + "'";
            DataTable dt = Projetor_Integrador.classes.db.RetornaDataTable(sql);

            if (dt.Rows.Count > 0 && dt.Rows[0][0].ToString() != "")
            {
                usuario_banco = dt.Rows[0]["usuario"].ToString();
                senha_banco = dt.Rows[0]["senha"].ToString();
            }

            if (v_username == usuario_banco && v_senha == senha_banco && v_username != string.Empty)
            {
                return true;
            }
            else
            {
                MessageBox.Show("Usuário/Senha Inválidos!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsuario.Focus();
            }
            return false;
        }

        public Login()
        {
            InitializeComponent();
        }
        private void btnEntrar_Click(object sender, EventArgs e)
        {
            if (!(txtUsuario.Text.Equals(string.Empty) || (txtSenha.Text.Equals(string.Empty))))
            {
                string c_senha = txtSenha.Text.Replace("'", "");
                string c_usuario = txtUsuario.Text.Replace("'", "");

                if (!Validacao(c_usuario, c_senha))
                {
                    return;
                }
            }
            else
            {
                MessageBox.Show("Há campos em branco!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnESenha_Click(object sender, EventArgs e)
        {
            // Exibir uma mensagem de confirmação
            DialogResult resultado = MessageBox.Show("Deseja redefinir sua senha?", "Redefinir Senha", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                // Gerar uma senha temporária (por exemplo, uma sequência aleatória de números)
                string novaSenha = GerarSenhaTemporaria();
                // Salvar a nova senha no banco de dados para o usuário
                // Aqui você precisará adicionar o código para atualizar a senha do usuário no banco de dados
                AtualizarSenhaNoBanco(txtUsuario.Text, novaSenha);

                // Exibir a nova senha para o usuário
                MessageBox.Show($"Sua nova senha temporária é: {novaSenha}", "Nova Senha", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void AtualizarSenhaNoBanco(string usuario, string novaSenha)
        {
            // Código para atualizar a senha do usuário no banco de dados
            // Exemplo usando SQL Server e C#
            string connectionString = "sua_string_de_conexão"; // Substitua pela sua string de conexão
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE Usuarios SET Senha = @novaSenha WHERE Usuario = @usuario";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@novaSenha", novaSenha);
                command.Parameters.AddWithValue("@usuario", usuario);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
