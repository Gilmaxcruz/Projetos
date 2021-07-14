using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Correios;

namespace ConsultaApi
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void btnConsulta_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCep.Text))
            {
                MessageBox.Show("O campo de CEP esta Vazio", "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                try
                {
                    CorreiosApi correiosApi = new CorreiosApi();
                    var retorno = correiosApi.consultaCEP(txtCep.Text);

                    if (retorno is null)
                    {
                        MessageBox.Show("CEP não encontrado", "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    txtEstado.Text = retorno.uf;
                    txtCidade.Text = retorno.cidade;
                    txtBairro.Text = retorno.bairro;
                    txtEndereco.Text = retorno.end;
                    txtComplemento1.Text = retorno.complemento;
                    txtComplemento2.Text = retorno.complemento2;
                    txtPostagem.Text = Convert.ToString(retorno.unidadesPostagem);

                }
                catch (Exception Erro)
                {
                    MessageBox.Show("Erro ao Consultar o Cep:" + Erro.Message, "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Sair da aplicação?", "Saindo...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                Application.Exit();
        }
    }
}
