using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projetor_Integrador
{
    public partial class Principal : Form
    {
        public Principal()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            slabData.Text = DateTime.Now.ToLongDateString();
            slabHoras.Text = DateTime.Now.ToLongTimeString();
        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            slabData.Text = DateTime.Now.ToString();
            slabHoras.Text = DateTime.Now.ToString();
        }

        private void Principal_Load(object sender, EventArgs e)
        {
            Projetor_Integrador.classes.db.AbreBanco();
            Login login = new Login();
            login.ShowDialog();
        }

        private void siteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "https://www.youtube.com/@_Sth.Officially_",
                UseShellExecute = true
            });
        }

        private void instagramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "https://www.instagram.com/geek_bluebee/",
                UseShellExecute = true
            });
        }

        private void facebookToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "https://www.facebook.com/Claudinharmos?mibextid=ZbWKwL",
                UseShellExecute = true
            });
        }

        private void btnSite_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "https://www.youtube.com/@_Sth.Officially_",
                UseShellExecute = true
            });
        }

        private void btnInstagram_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "https://www.instagram.com/geek_bluebee/",
                UseShellExecute = true
            });
        }

        private void btnFacebook_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "https://www.facebook.com/Claudinharmos?mibextid=ZbWKwL",
                UseShellExecute = true
            });
        }

        private void clientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmClientes cliente = new frmClientes();
            cliente.ShowDialog();
        }

        private void fornecedorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmFornecedor fornecedor = new frmFornecedor();
            fornecedor.ShowDialog();
        }

        private void produtosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmProdutos produtos = new frmProdutos();
            produtos.ShowDialog();
        }

        private void usuáriosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUsuarios usuarios = new frmUsuarios();
            usuarios.ShowDialog();
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void vendaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FrmVenda venda = new FrmVenda();
            venda.ShowDialog();
        }

        private void estoqueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmEstoque estoque = new FrmEstoque();
            estoque.ShowDialog();
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            frmClientes cliente = new frmClientes();
            cliente.ShowDialog();
        }

        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            frmUsuarios usuarios = new frmUsuarios();
            usuarios.ShowDialog();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            frmFornecedor fornecedor = new frmFornecedor();
            fornecedor.ShowDialog();
        }

        private void StripBtnProdutos_Click(object sender, EventArgs e)
        {
            frmProdutos produtos = new frmProdutos();
            produtos.ShowDialog();
        }

        private void StripBtnEstoque_Click(object sender, EventArgs e)
        {
            FrmEstoque estoque = new FrmEstoque();
            estoque.ShowDialog();
        }

        private void vendaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmRVenda rVenda = new FrmRVenda();
            rVenda.ShowDialog();
        }

        private void marketPlaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmMarketplace marketplace = new FrmMarketplace();
            marketplace.ShowDialog();
        }
    }
}
