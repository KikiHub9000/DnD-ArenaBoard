using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArenaBoard
{
    public partial class ColorPicker : Form
    {
        private string name = "";
        private Color color = Color.White;

        public ColorPicker(string name, Color color)
        {
            InitializeComponent();
            KeyPreview = true;
            this.name = name;
            this.color = color;
        }

        private List<Color> colors = new List<Color>();

        public Color SelectedColor { get; set; }

        public string Input => SplitText();

        private string SplitText()
        {
            if (txtName.Text.Length > 2)
            {
                string s1 = txtName.Text.Substring(0, 2);
                string s2 = txtName.Text.Substring(2, txtName.Text.Length - 2);
                return $"{s1}{Environment.NewLine}{s2}";
            }
            return txtName.Text;
        }

        private void InitColors()
        {            
            colors.Add(ColorTranslator.FromHtml("#dc143c"));
            colors.Add(ColorTranslator.FromHtml("#1e90ff"));
            colors.Add(ColorTranslator.FromHtml("#008000"));
            colors.Add(ColorTranslator.FromHtml("#00ff7f"));
            colors.Add(ColorTranslator.FromHtml("#8a2be2"));
            colors.Add(ColorTranslator.FromHtml("#dda0dd"));
            colors.Add(ColorTranslator.FromHtml("#ffa500"));            
            colors.Add(ColorTranslator.FromHtml("#8b4513"));

            SelectedColor = colors[0];

            //colors.Add(ColorTranslator.FromHtml("#a9a9a9"));
            //colors.Add(ColorTranslator.FromHtml("#808000"));
            //colors.Add(ColorTranslator.FromHtml("#483d8b"));
            //colors.Add(ColorTranslator.FromHtml("#008b8b"));
            //colors.Add(ColorTranslator.FromHtml("#000080"));
            //colors.Add(ColorTranslator.FromHtml("#9acd32"));
            //colors.Add(ColorTranslator.FromHtml("#8b008b"));
            //colors.Add(ColorTranslator.FromHtml("#ff4500"));
            //colors.Add(ColorTranslator.FromHtml("#ffff00"));
            //colors.Add(ColorTranslator.FromHtml("#00ff00"));
            //colors.Add(ColorTranslator.FromHtml("#00ffff"));
            //colors.Add(ColorTranslator.FromHtml("#00bfff"));
            //colors.Add(ColorTranslator.FromHtml("#0000ff"));
            //colors.Add(ColorTranslator.FromHtml("#f08080"));
            //colors.Add(ColorTranslator.FromHtml("#ff00ff"));
            //colors.Add(ColorTranslator.FromHtml("#eee8aa"));
            //colors.Add(ColorTranslator.FromHtml("#ff1493"));
        }

        private void ColorPicker_Load(object sender, EventArgs e)
        {
            InitColors();
            txtName.Text = name;

            foreach (Color c in colors)
            {
                Button btn = new Button();
                btn.Width = 40;
                btn.Height = 40;
                btn.Margin = new Padding(0, 0, 0, 0);
                btn.BackColor = c;

                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 1;

                pnlColors.Controls.Add(btn);

                btn.Click += Btn_Click;

                if (c == color)
                    btn.PerformClick();
            }

            KeyDown += ColorPicker_KeyDown;
        }

        private void ColorPicker_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                DialogResult = DialogResult.Cancel;
                Close();
            }
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            foreach(Button btn in pnlColors.Controls.OfType<Button>())
                btn.FlatAppearance.BorderSize = 1;

            (sender as Button).FlatAppearance.BorderSize = 3;

            SelectedColor = (sender as Button).BackColor;

            txtName.Select();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
