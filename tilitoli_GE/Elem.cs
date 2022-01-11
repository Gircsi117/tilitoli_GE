using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tilitoli_GE
{
    class Elem
    {
        private Button maga;
        private int sor;
        private int oszlop;
        private bool status;

        public Button Maga { get => maga; set => maga = value; }
        public int Sor { get => sor; set => sor = value; }
        public int Oszlop { get => oszlop; set => oszlop = value; }
        public bool Status { get => status; set => status = value; }

        public Elem(Button pic, int s, int o)
        {
            this.maga = pic;
            this.sor = s;
            this.oszlop = o;
            this.status = true;
        }
    }
}
