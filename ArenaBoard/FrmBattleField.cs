using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ArenaBoard
{
    public partial class FrmBattleField : Form
    {
        private int size = 20;

        private Button currentButton;
        private Color currentColor;
        private string currentText;
        private bool move = false;

        private Button prevPosition = null;
        private Button newPosition = null;

        private Button delButton = null;
        private string delText = "";
        private Color delColor = Color.White;

        public FrmBattleField()
        {
            InitializeComponent();

            //TopMost = true;
            KeyPreview = true;
            StartPosition = FormStartPosition.CenterScreen;
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            using (var sizePicker = new FrmSize())
            {
                sizePicker.StartPosition = FormStartPosition.CenterScreen;
                sizePicker.TopMost = true;
                sizePicker.ShowDialog();
                size = sizePicker.FieldSize;
            }

            int fontSize = 10;
            switch (size)
            {
                case 25:
                    fontSize = 6;
                    break;
                case 20:
                    fontSize = 10;
                    break;
                case 15:
                    fontSize = 15;
                    break;
                case 10:
                    fontSize = 25;
                    break;
            }

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Button btn = new Button
                    {
                        Width = 1000 / size - 2,
                        Height = 1000 / size - 2,
                        Margin = new Padding(1, 1, 1, 1),
                        BackColor = Color.White,
                        FlatStyle = FlatStyle.Flat,
                        Font = new Font(Font.Name, fontSize, FontStyle.Bold),
                        Tag = new Point(i, j)
                    };

                    btn.FlatAppearance.BorderSize = 1;
                    btn.MouseUp += Btn_MouseUp;
                    btn.Click += Btn_Click;

                    pnlArena.Controls.Add(btn);
                }
            }
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            if (move)
            {
                if (sender as Button == currentButton)
                {
                    currentButton.FlatAppearance.BorderSize = 1;
                    move = false;
                    currentButton = null;
                    return;
                }

                if ((sender as Button).BackColor != Color.White)
                    return;

                (sender as Button).BackColor = currentColor;
                (sender as Button).Text = currentText;
                (sender as Button).FlatAppearance.BorderSize = 1;

                currentButton.BackColor = Color.White;
                currentButton.Text = string.Empty;
                currentButton.FlatAppearance.BorderSize = 1;
                currentColor = Color.White;
                delButton = null;
                prevPosition = currentButton;
                newPosition = (sender as Button);
                currentButton = null;
                move = false;

                return;
            }

            if ((sender as Button).BackColor == Color.White)
                return;

            currentColor = (sender as Button).BackColor;
            currentText = (sender as Button).Text;
            currentButton = (sender as Button);
            currentButton.FlatAppearance.BorderSize = 3;
            move = true;
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (e.KeyCode == Keys.Delete)
            {
                if (currentButton != null)
                {
                    currentButton.FlatAppearance.BorderSize = 1;
                    currentButton.BackColor = Color.White;
                    currentButton.Text = string.Empty;
                    move = false;

                    delButton = currentButton;
                    delText = currentText;
                    delColor = currentColor;

                    currentButton = null;
                    return;
                }
            }

            if (e.KeyCode == Keys.Escape)
            {
                if (delButton != null)
                {
                    delButton.BackColor = delColor;
                    delButton.Text = delText;
                    delButton = null;
                    newPosition = null;
                    prevPosition = null;
                }
                else if (newPosition != null && prevPosition != null)
                {
                    if (currentButton != null)
                        currentButton.PerformClick();

                    newPosition.PerformClick();
                    prevPosition.PerformClick();
                    newPosition = null;
                    prevPosition = null;
                }                
            }
        }

        private void Btn_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                string name = (sender as Button).Text;
                ColorPicker cd = new ColorPicker(Regex.Replace(name, "\\s+", ""), (sender as Button).BackColor);

                cd.TopMost = true;
                cd.StartPosition = FormStartPosition.Manual;

                int offsetY = 0;
                if (Cursor.Position.Y > Screen.FromControl(sender as Button).Bounds.Height - 96)
                    offsetY = 96;

                int offsetX = 0;
                if (Cursor.Position.X > Screen.FromControl(sender as Button).Bounds.Height - 512)
                    offsetX = 512;

                cd.Location = new Point(Cursor.Position.X - offsetX, Cursor.Position.Y - offsetY);
                if (cd.ShowDialog() == DialogResult.OK)
                {
                    Color cc = cd.SelectedColor;
                    string tt = cd.Input;
                    (sender as Button).BackColor = cc;
                    (sender as Button).Text = tt;
                    (sender as Button).FlatAppearance.BorderSize = 1;
                }
                cd.Dispose();
            }            
        }
    }
}
