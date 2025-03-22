using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Parchis
{
    public partial class Main : Form
    {

        Tablero t;

        public Main()
        {

            InitializeComponent();
            //Asignacion de fichas (img) a las fichas del const
            Ficha[] fichas = {
                new Ficha(redCoin1,     Const.ROJO),
                new Ficha(redCoin2,     Const.ROJO),
                new Ficha(redCoin3,     Const.ROJO),
                new Ficha(redCoin4,     Const.ROJO),
                new Ficha(blueCoin1,    Const.AZUL),
                new Ficha(blueCoin2,    Const.AZUL),
                new Ficha(blueCoin3,    Const.AZUL),
                new Ficha(blueCoin4,    Const.AZUL),
                new Ficha(greenCoin1,   Const.MORADO),
                new Ficha(greenCoin2,   Const.MORADO),
                new Ficha(greenCoin3,   Const.MORADO),
                new Ficha(greenCoin4,   Const.MORADO),
                new Ficha(yellowCoin1,  Const.AMARILLO),
                new Ficha(yellowCoin2,  Const.AMARILLO),
                new Ficha(yellowCoin3,  Const.AMARILLO),
                new Ficha(yellowCoin4,  Const.AMARILLO)
            };
            //Accion de los botones
            StartMatch.Text = GUI.botonInicio;
            initialCoin.Text = GUI.iniciarconFicha;
            skipTurn.Text = GUI.saltarturno;
            turnOf.Text = GUI.turnoDe;
            DiceButton.Text = GUI.botonDado;

            //Nombres para las fichas
            redLabel1.Text = GUI.rojo;
            blueLabel1.Text = GUI.azul;
            yellowLabel1.Text = GUI.amarillo;
            greenLabel1.Text = GUI.morado;

            //Seleccionar cantidad de jugadores con su respectivo color
            enableRed.Text = GUI.roj;
            enableBlue.Text = GUI.azu;
            enableYellow.Text = GUI.ama;
            enableGreen.Text = GUI.mor;

            t = new Tablero(fichas, Tablero, gameCheck, currentTurn, diceGraph0, diceGraph1, new Ficha(auxFicha, -1), consoleLogs);

            Tablero.Controls.Add(blur0);
            Tablero.Controls.Add(blur1);

            blur0.Location = new Point(296, 305);
            blur1.Location = new Point(341, 305);

            blur0.Visible = false;
            blur1.Visible = false;

        }

        private void StartMatch_Click(object sender, EventArgs e)
        {

            DialogResult dialogResult = MessageBox.Show((t.turno == -1) ? GUI.mensajeInicio : GUI.mensajeDetener, GUI.nomJuego, MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                if (t.turno == -1)
                {
                    t.iniciarJuego();
                    t.log(GUI.mensajeInicio);
                    t.log(String.Format(GUI.quienEmpieza, GUI.colores[t.turno]));
                    StartMatch.Text = GUI.stop;

                    DiceButton.Enabled = true;

                    skipTurn.Enabled = true;
                    initialCoin.Enabled = false;

                    enableRed.Enabled = false;
                    enableBlue.Enabled = false;
                    enableGreen.Enabled = false;
                    enableYellow.Enabled = false;
                } else
                {
                    t.detenerJuego();
                    t.log(GUI.mensajeDetener);
                    StartMatch.Text = GUI.botonInicio;

                    DiceButton.Enabled = false;

                    blur0.Visible = false;
                    blur1.Visible = false;

                    skipTurn.Enabled = false;
                    initialCoin.Enabled = true;

                    enableRed.Enabled = true;
                    enableBlue.Enabled = true;
                    enableGreen.Enabled = true;
                    enableYellow.Enabled = true;
                }
                
            }
            else if (dialogResult == DialogResult.No)
            {
                t.log(GUI.cancelOperation);
            }

        }

        private void InitialCoin_CheckedChanged(object sender, EventArgs e)
        {
            t.fichaInicio = initialCoin.Checked;
        }

        private void PasaTurno_Tick(object sender, EventArgs e)
        {
            int win = t.hayGanador();
            if (win != -1)
            {
                t.detenerJuego();
                t.log(GUI.elGanador + GUI.colores[win] + "!");
                StartMatch.Text = GUI.botonInicio;

                DiceButton.Enabled = false;

                blur0.Visible = false;
                blur1.Visible = false;

                skipTurn.Enabled = false;
                initialCoin.Enabled = true;

                enableRed.Enabled = true;
                enableBlue.Enabled = true;
                enableGreen.Enabled = true;
                enableYellow.Enabled = true;
            }

            if (t.turno != -1)
            {
                turnLabel.Text = GUI.colores[t.turno];
            }

            if ((t.dadosActivos == 0 && t.valorSeleccionado <= 0) || !t.puedeMover(t.turno))
            {
                blur0.Visible = false;
                blur1.Visible = false;
                t.establecerEquipoFichas(t.turno, false);
                t.nextTurn();
                DiceButton.Enabled = true;
            }
        }

        private void EnableRed_CheckedChanged(object sender, EventArgs e)
        {
            t.jugadoresActivos[3] = enableRed.Checked;
        }

        private void EnableGreen_CheckedChanged(object sender, EventArgs e)
        {
            t.jugadoresActivos[0] = enableGreen.Checked;
        }

        private void EnableBlue_CheckedChanged(object sender, EventArgs e)
        {
            t.jugadoresActivos[2] = enableBlue.Checked;
        }

        private void EnableYellow_CheckedChanged(object sender, EventArgs e)
        {
            t.jugadoresActivos[1] = enableYellow.Checked;
        }

        private void SkipTurn_Click(object sender, EventArgs e)
        {
            t.dadosActivos = 0;
            t.valorSeleccionado = -1;
        }

        private void DiceButton_Click(object sender, EventArgs e)
        {
            t.lanzarDados();
            DiceButton.Enabled = false;
        }

        private void DiceGraph0_Click(object sender, EventArgs e)
        {
            t.movimientoEspecial = false;
            blur0.Visible = true;
            blur1.Visible = false;
            t.valorSeleccionado = t.dado[0];
            t.dadoSeleccionado = diceGraph0;
            t.log(String.Format(GUI.seleccionarDado, 0) + String.Format(GUI.valorDado, t.valorSeleccionado));
        }

        private void DiceGraph1_Click(object sender, EventArgs e)
        {
            t.movimientoEspecial = false;
            blur0.Visible = false;
            blur1.Visible = true;
            t.valorSeleccionado = t.dado[1];
            t.dadoSeleccionado = diceGraph1;
            t.log(String.Format(GUI.seleccionarDado, 1) + String.Format(GUI.valorDado, t.valorSeleccionado));
        }

        private int getCoin(object sender)
        {
            if (t.valorSeleccionado < 1 || !((PictureBox)sender).Enabled) { return -1; }

            string name = ((PictureBox)sender).Name;
            int id = Int32.Parse(name.Substring(name.Length - 1));
            string team = name.Substring(0, name.IndexOf("Coin"));

            int baseID = -2;

            
            switch (team)
            {
                case "blue":
                    baseID = 3;
                    break;
                case "red":
                    baseID = -1;
                    break;
                case "green":
                    baseID = 7;
                    break;
                case "yellow":
                    baseID = 11;
                    break;
            }

            return baseID + id;
        }

        private void CoinClick(object sender, EventArgs e)
        {

            int coin = getCoin(sender);
            if (coin == -1) return;

            if (t.fichas[coin].pos <= 0 || t.fichas[coin].team != t.turno)
                return;

            if (t.mover(t.fichas[coin], t.valorSeleccionado))
            {
                if (t.dadoSeleccionado != null) 
                {
                    t.dadoSeleccionado.Enabled = false;
                    t.dadoSeleccionado = null;
                }
                if (!t.movimientoEspecial)
                {
                    t.valorSeleccionado = -1;
                    t.dadosActivos--;
                }
            }

        }

        private void PreviewCoin(object sender, EventArgs e)
        {

            int coin = getCoin(sender);
            if (coin == -1) return;

            if (t.fichas[coin].pos <= 0 || t.fichas[coin].team != t.turno)
                return;

            if (t.preview(t.fichas[coin], t.valorSeleccionado))
            {
                auxFicha.Visible = true;
            }

        }

        private void HidePreviewCoin(object sender, EventArgs e)
        {
            auxFicha.Visible = false;
        }

        private void Main_Load(object sender, EventArgs e)
        {

        }

        private void auxFicha_Click(object sender, EventArgs e)
        {

        }

        private void blueLabel1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {


        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox13_Click(object sender, EventArgs e)
        {

        }

        private void turnLabel_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox5_Click_1(object sender, EventArgs e)
        {

        }

        private void Tablero_Click(object sender, EventArgs e)
        {

        }
    }

}
