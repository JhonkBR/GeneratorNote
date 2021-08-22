using GeneratorNote.Main;
using System;
using System.Drawing;
using System.Security;
using System.Windows.Forms;
namespace GeneratorNote
{
    public partial class Home : Form
    {
        private readonly Utils _utils;

        public Home()
        {
            InitializeComponent();
            _utils = new Utils();
        }
        private int borderWidth = 5; //Exemplo apenas para teste
        private new Padding Padding = new Padding(50); //Exemplo apenas para teste (Pode ser especificado direto nas propriedades do Form)

        private WinApi.HitTest HitTestNCA(IntPtr lparam)
        {
            Point vPoint = new Point((Int16)lparam, (Int16)((int)lparam >> 16));
            int vPadding = Math.Max(Padding.Right, Padding.Bottom);

            if (RectangleToScreen(new Rectangle(ClientRectangle.Width - vPadding, ClientRectangle.Height - vPadding, vPadding, vPadding)).Contains(vPoint))
                return WinApi.HitTest.HTBOTTOMRIGHT;

            if (RectangleToScreen(new Rectangle(borderWidth, borderWidth, ClientRectangle.Width - 2 * borderWidth, 50)).Contains(vPoint))
                return WinApi.HitTest.HTCAPTION;

            return WinApi.HitTest.HTCLIENT;
        }

        protected override void WndProc(ref Message m)
        {
            if (DesignMode)
            {
                base.WndProc(ref m);
                return;
            }

            switch (m.Msg)
            {
                case (int)WinApi.Messages.WM_NCHITTEST:
                    WinApi.HitTest ht = HitTestNCA(m.LParam);
                    if (ht != WinApi.HitTest.HTCLIENT)
                    {
                        m.Result = (IntPtr)ht;
                        return;
                    }
                    break;
            }

            base.WndProc(ref m);
        }

        [SuppressUnmanagedCodeSecurity]
        internal static class WinApi
        {
            public enum Messages : uint
            {
                WM_NCHITTEST = 0x84,
            }

            public enum HitTest
            {
                HTCLIENT = 1,
                HTBOTTOMRIGHT = 17,
                HTCAPTION = 2
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (nfe.Checked)
            {

                if (!string.IsNullOrEmpty(txtpathBase.Text) && !string.IsNullOrEmpty(txtDestiny.Text) && !string.IsNullOrEmpty(txtQuantity.Text))
                {
                    //TODO DEMO
                    if (Convert.ToInt32(txtQuantity.Text) > 10)
                    {
                        MessageBox.Show($"Verão Incompleta, somente 10 arquivos por vez!", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.None);
                    }
                    else
                    {
                        var result = _utils.GenerateNfe(txtpathBase.Text, txtDestiny.Text, Convert.ToInt32(txtQuantity.Text));
                        if (result.Result)
                        {
                            //TODO DEMO
                            MessageBox.Show($"{txtQuantity.Text} arquivos gerados com êxito!", "SUCESSO", MessageBoxButtons.OK, MessageBoxIcon.None);
                        }
                    }

                }
                else
                    MessageBox.Show("Por favor, verifique os campos obrigatórios e tente novamente!");
            }
            else if (ct.Checked)
            {

            }
            else if (mdf.Checked)
            {

            }
            else if (nfs.Checked)
            {

            }
            else
            {
                MessageBox.Show("Favor selecione o tipo de documento!");
            }



        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            var openDlg = new OpenFileDialog
            {
                InitialDirectory = @"c:\\",
                Filter = @"xml files (*.xml)|*.xml"
            };

            if (openDlg.ShowDialog() == DialogResult.OK)
                txtpathBase.Text = openDlg.FileName;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtDestiny.Clear();
            txtpathBase.Clear();
            txtQuantity.Clear();
        }

        private void btnFolderDestiny_Click(object sender, EventArgs e)
        {
            var openDlg = new FolderBrowserDialog();

            if (openDlg.ShowDialog() == DialogResult.OK)
                txtDestiny.Text = openDlg.SelectedPath;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void Home_Load(object sender, EventArgs e)
        {

        }

        private void txtQuantity_Click(object sender, EventArgs e)
        {
            txtQuantity.Clear();
        }
    }
}

