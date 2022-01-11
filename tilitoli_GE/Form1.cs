using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;


namespace tilitoli_GE
{
    public partial class Form1 : Form
    {
        public static int meret = 4;
        private int lassitas = 20;

        private Elem[,] palya = new Elem[meret, meret];

        private int[] ures;
        private Random rand = new Random();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            general(0, 0, 0);

            settings();
        }

        private void settings()
        {
            ures = new int[] { meret - 1, meret - 1,/* index <---> location */ palya[meret - 1, meret - 1].Maga.Location.X, palya[meret - 1, meret - 1].Maga.Location.Y };
            panel1.Controls.RemoveAt(meret * meret - 1);
            palya[meret - 1, meret - 1] = null;
        }

        private void general(int sor, int oszlop, int sorszam)
        {
            int szeles = panel1.Width / meret;

            Button btn = new Button();
            panel1.Controls.Add(btn);
            btn.Size = new Size(szeles, szeles);
            btn.Name = sorszam.ToString();
            btn.Tag = $"{sor};{oszlop}";
            btn.Location = new Point(oszlop * szeles, sor * szeles);
            btn.BackColor = Color.White;
            btn.Font = new Font("Arial", btn.Height / 3);
            btn.Text = sorszam.ToString();

            palya[sor, oszlop] = new Elem(btn, sor, oszlop);

            btn.Click += kattintas;

            oszlop += 1;
            if (oszlop == meret)
            {
                sor += 1;
                oszlop = 0;
            }
            if (sor != meret)
            {
                general(sor, oszlop, sorszam + 1);
            }

        }
       

        private void kattintas(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            int sor = Convert.ToInt32(btn.Tag.ToString().Split(';')[0]);
            int oszlop = Convert.ToInt32(btn.Tag.ToString().Split(';')[1]);

            mozgat(sor, oszlop);
        }

        private bool bennevane(int sor, int oszlop)
        {
            if (sor < meret && oszlop < meret && sor >= 0 && oszlop >= 0)
            {
                return true;
            }
            return false;
        }

        private bool mozoghate(int sor, int oszlop)
        {
            if (bennevane(sor, oszlop) && palya[sor, oszlop] == null)
            {
                return true;
            }
            return false;
        }

        private void felcserel(Elem teli)
        {
            int[] seged = new int[] { teli.Sor, teli.Oszlop, teli.Maga.Location.X, teli.Maga.Location.Y};

            teli.Sor = ures[0];
            teli.Oszlop = ures[1];

            teli.Maga.Location = new Point(ures[2], ures[3]);
            teli.Maga.Tag = $"{teli.Sor};{teli.Oszlop}";

            palya[teli.Sor, teli.Oszlop] = teli;
            palya[seged[0], seged[1]] = null;

            ures = new int[] { seged[0], seged[1], seged[2], seged[3] };

        }

        private void kever(int i)
        {
            int sor = rand.Next(0, meret);
            int oszlop = rand.Next(0, meret);

            if (palya[sor, oszlop] != null)
            {
                mozgat(sor, oszlop, false);
            }

            if (i < 1000)
            {
                Thread.Sleep((checkBox1.Checked) ? (lassitas) : (0));
                kever(i + 1);
            }
        }

        private void mozgat(int sor, int oszlop, bool tesztele = true)
        {
            if (mozoghate(sor+1, oszlop) || mozoghate(sor, oszlop + 1) || mozoghate(sor-1, oszlop) || mozoghate(sor, oszlop - 1))
            {
                felcserel(palya[sor, oszlop]);
            }

            if (tesztele)
            {
                gyozelem_vizsgal(0, 0, 0, true);
            }
        }

        private void gyozelem_vizsgal(int sor, int oszlop, int sorszam, bool mehet)
        {
            if (palya[sor, oszlop] != null && Convert.ToInt32(palya[sor, oszlop].Maga.Name) != sorszam)
            {
                mehet = false;
            }

            oszlop += 1;
            if (oszlop == meret)
            {
                sor += 1;
                oszlop = 0;
            }

            if (sorszam < meret * meret - 1 )
            {
                gyozelem_vizsgal(sor, oszlop, sorszam + 1, mehet);
            }
            else if (mehet)
            {
                MessageBox.Show("Sikeresen kiraktad a számokat!", "Winner", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            lassitas = Convert.ToInt32(numericUpDown1.Value);
            kever(0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            palya = new Elem[meret, meret];
            panel1.Controls.Clear();
            general(0, 0, 0);
            settings();
        }
    }
}
