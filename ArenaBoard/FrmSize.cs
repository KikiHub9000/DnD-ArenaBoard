using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArenaBoard
{
    public partial class FrmSize : Form
    {
        public FrmSize()
        {
            InitializeComponent();
        }

        public int FieldSize { get; set; } = 20;

        private void FrmSize_Load(object sender, EventArgs e)
        {
            txtSize.SelectedIndex = 2;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            switch (txtSize.SelectedIndex)
            {
                case 0:
                    FieldSize = 10;
                    break;
                case 1:
                    FieldSize = 15;
                    break;
                case 2:
                    FieldSize = 20;
                    break;
                case 3:
                    FieldSize = 25;
                    break;
                default:
                    FieldSize = 20;
                    break;
            }
            Close();
        }
    }
}
