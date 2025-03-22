using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Parchis
{
    class Ficha
    {

        public PictureBox img;
        public int pos;
        public int team;

        public Ficha(PictureBox img, int team)
        {
            this.img = img;
            this.pos = -2; // No empezado (-1 = EnCasa, -3 = Cielo o victoria)
            this.team = team;

        }

        public bool enCasa()
        {
            return (this.pos >= 100);
        }

    }

}
