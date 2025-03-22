using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Parchis
{
    class Tablero
    {

        public RichTextBox consola; //Cuadro que muestra los movimientos de los jugadores o historial
        public Ficha [] fichas; //Declaramos matriz fichas
        public int turno = -1; //El juego no ha empezado
        public bool [] jugadoresActivos = { true, true, true, true };//Selecciona la cantidad de jugadores
        public bool fichaInicio = false;
        public int dadosActivos = -1; //No se activan hasta no darle lanzar
        public int valorSeleccionado = -1;//Seleccionar el valor del dado que movera la ficha
        public PictureBox dadoSeleccionado = null;
        public Ficha ultimaFichaSeleccionada = null;
        public int[] dado;//Almacenamos valor
        public PictureBox tablero, turnoCirculo, dado0, dado1;
        public Timer gameCheck;//Controla el tiempo determinado para mover ficha
        public Random vRandom;//Valor random de los dados
        public Ficha previewFicha;
        public int paresEnRonda = 0;//Contador de pares
        public bool esDoble = false;
        public bool movimientoEspecial = false;

        public Tablero(Ficha [] f, PictureBox t, Timer time, PictureBox circle, PictureBox dado0, PictureBox dado1, Ficha p, RichTextBox c) {
            this.consola = c;//El historial de juego = RichTextBox
            this.fichas = f;//Fichas
            this.tablero = t;//Tablero
            this.gameCheck = time;//Timer
            this.turnoCirculo = circle;//Ficha en jugada
            this.dado0 = dado0;//Asignacion dado 1
            this.dado1 = dado1;//Asignaciond dado 2
            this.previewFicha = p;//Fantasma a donde llegaria la ficha segun el valor
            this.dado = new int[2];//Guarda el valor de ambos dados
            this.vRandom = new Random();//Asignacion de la variable Random para los valores de los dados

            this.tablero.Controls.Add(this.turnoCirculo);//Asignacion de la ficha en juego en el tablero
            this.turnoCirculo.Visible = false;

            this.tablero.Controls.Add(p.img);

            this.tablero.Controls.Add(dado0);
            this.tablero.Controls.Add(dado1);

            dado0.Location = new Point(296, 345);
            dado1.Location = new Point(341, 345);

            for (int i = 0; i < fichas.Length; i++)//Este for se encarga de mostrar las fichas y los dados en la ubicacion correspondiente al iniciar el juego
            {//Frente al tablero
                
                this.tablero.Controls.Add(fichas[i].img);
                enviarCasa(this.fichas[i]);
                fichas[i].img.Visible = true;
                fichas[i].img.BringToFront();
            }

            dado0.BringToFront();
            dado1.BringToFront();

        }

        public void detenerJuego()
        {//Si un jugador ya no tiene fichas en juego, se deshabilita el mismo

            for (int i=0; i<4; i++)
            {
                establecerEquipoFichas(i, false);
            }

            this.turnoCirculo.Visible = false;
            this.gameCheck.Enabled = false;
            this.turno = -1;
            this.dado0.Enabled = false;
            this.dado1.Enabled = false;

        }

        public void iniciarJuego() {

            // Todas las fichas se posicionan en su casa
            for (int i = 0; i < this.fichas.Length; i++)
            {
                if (this.fichas[i].pos != -1)
                    enviarCasa(this.fichas[i]);
                this.fichas[i].img.Enabled = false;
            }

            // Inicia un turno random 
            do { turno = vRandom.Next(0, 4); }
            while (!jugadoresActivos[turno]);

            //habilita
            this.turnoCirculo.Visible = true;
            this.turnoCirculo.Location = Const.Resaltador[turno];
            this.dadosActivos = -1;
            this.gameCheck.Enabled = true;

            if (fichaInicio)
            { // Crea una nueva ficha para cada jugador activo
                for (int i=0; i<4; i++)
                {
                    if (jugadoresActivos[i])
                    {
                        nuevaFicha(i);
                    }
                }
            }

        }

        public void nextTurn()
        {//No cede el turno hasta que el resultado sea ! a par

            if (esDoble) paresEnRonda++;
            else
            {
                do { turno = (turno + 1) % 4; }
                while (!jugadoresActivos[turno]);

                paresEnRonda = 0;
            }

            this.dado0.Enabled = false;
            this.dado1.Enabled = false;
            this.valorSeleccionado = -1;

            this.turnoCirculo.Location = Const.Resaltador[turno];
            this.dadosActivos = -1;

        }

        public int hayGanador()
        {

            for (int i=0; i<4; i++)
            {
                bool ganador = true;
                Ficha[] fichas = obtFichasEquipo(i);

                for (int j=0; j<fichas.Length && ganador; j++)
                {//Siempre que sea menor a la cantidad de fichas en juego
                    if (fichas[i].pos != -3)
                    {
                        ganador = false;
                    }
                }

                if (ganador) return i;
            }

            return -1;
        }

        public void lanzarDados()
        {

            log(GUI.lanzarDados);

            dado[0] = vRandom.Next(1, 7);//Da un valor del uno al seis
            dado[1] = vRandom.Next(1, 7);

            log(String.Format(GUI.resultadoDado, dado[0], dado[1]));//Registra Resultado

            esDoble = (dado[0] == dado[1]);

            //Verificar van tres pares
            if (esDoble && paresEnRonda == 2)
            {
                log(GUI.paresConsecutivos);
                enviarCasa(ultimaFichaSeleccionada); //Se envia a la carcel la ultima ficha movida
                paresEnRonda = 0;
                esDoble = false;
                dadosActivos = 0;
            } else dadosActivos = 2;

            establecerValorDado(dado[0], dado[1]);//Muestra valores de los dados en la interfaz
            establecerEquipoFichas(turno, true);//habilitas las fichas del equipo

            if (dado[0] == 6 && nuevaFicha(turno))
            {
                dadosActivos--;
                this.dado0.Enabled = false;
            }
            else if (dado[1] == 6 && nuevaFicha(turno))
            {
                dadosActivos--;
                this.dado1.Enabled = false;
            }
            else if (dado[0] + dado[1] == 6 && nuevaFicha(turno)) {
                dadosActivos -= 2;//resetea dados
                this.dado0.Enabled = false;
                this.dado1.Enabled = false;
                log(GUI.dadosSumanSeis);
            }

        }

        public bool puedeMoverFicha(Ficha f, int cant, Ficha preFicha)
        {

            if (f.enCasa())
            {
                return puedeMoverCasa(f, ((f.pos / 100) + cant), preFicha);
            }

            
            int prev = f.pos;

            int newPos = (f.pos + cant - 1) % Const.casillas.Length;
            previewFicha.pos = newPos + 1;
            previewFicha.img.Location = Const.casillas[newPos];

            //Entrando a casa
            if ((f.team == Const.ROJO && prev <= 34 && previewFicha.pos > 34) ||
                (f.team == Const.AZUL && prev <= 17 && previewFicha.pos > 17) ||
                (f.team == Const.AMARILLO && prev > 40 && prev <= 68 && (prev + cant) > 68) ||
                (f.team == Const.MORADO && prev <= 51 && previewFicha.pos > 51))
            {
                int cantCasa = (prev + cant - Const.preCasillaGana[f.team]);

                //Verificar puentes para los caminos fuera de casa
                if (esPuente(f, Const.preCasillaGana[f.team]))
                {
                    return false;//No se mueve - puente
                }

                return puedeMoverCasa(f, cantCasa, previewFicha);
            }

            //Si hay puentes (y no en casa), devolver null(false)
            if (esPuente(f, newPos + 1))
            {
                return false;
            }

            return true;

        }

        public bool puedeMoverCasa(Ficha f, int pos, Ficha preFicha)
        {

            if (esPuenteCasa(f, pos))
            {
                return false; //Si hay puentes no puede mover
            }

            if (pos == 8)
            {
                previewFicha.pos = -3;//la ficha ha llegado 
            }
            else if (pos > 8)
            {
                return false;//No puede mover
            }
            else
            {
                previewFicha.pos = pos * 100; //Si la posicion es >= 100 se entiende que esta en casa
            }

            previewFicha.img.Location = Const.casillaEtapFinal[f.team, pos - 1];

            return true;

        }

        public bool puedeMover(int team)
        {

            Ficha auxFicha = new Ficha(new PictureBox(), -1);

            //Necesario en caso de tener una ficha en casa
            if (dadosActivos == -1)
                return true;

            Ficha[] coins = obtFichasEquipo(team);

            for (int i=0; i<coins.Length; i++)
            {
                // Verifica - ficha -posición válida - puede moverse- dados activos o el valor seleccionado
                if (coins[i].pos >= 1 && (
                    (this.dado0.Enabled && puedeMoverFicha(coins[i], dado[0], auxFicha)) || 
                    (this.dado1.Enabled && puedeMoverFicha(coins[i], dado[1], auxFicha)) ||
                    (valorSeleccionado > 0 && puedeMoverFicha(coins[i], valorSeleccionado, auxFicha)) ))
                {
                    return true;//Al menos una ficha puede moverse
                }

            }

            log(GUI.noMovimiento);
            return false;//Nohay

        }

        public void establecerEquipoFichas(int team, bool enabled)
        {

            Ficha[] equiFichas = obtFichasEquipo(team);

            for (int i=0; i<4; i++)
            {
                if (equiFichas[i].pos > -1)
                    equiFichas[i].img.Enabled = enabled;
            }

        }

        private Ficha [] obtFichasEquipo(int team)
        {

            Ficha[] equiFichas = new Ficha[4];

            for (int i=0, j=0; i<this.fichas.Length; i++)
            {
                if (this.fichas[i].team == team)
                {
                    equiFichas[j++] = this.fichas[i];
                }
            }

            return equiFichas;

        }

        private void establecerValorDado(int val0, int val1)
        {
            // Verifica si los valores del dado son válidos (entre 1 y 6)
            if (val0 <= 0 || val0 > 6 || val1 <= 0 || val1 > 6)
            {
                log("Error");
                return;
            }

            this.dado0.Enabled = true;
            this.dado1.Enabled = true;

            Bitmap[] dados = {
                Properties.Resources.dice1,
                               Properties.Resources.dice2,
                               Properties.Resources.dice3,
                               Properties.Resources.dice4,
                               Properties.Resources.dice5,
                               Properties.Resources.dice6
                             };

            this.dado0.Image = dados[val0-1];// Establece las imágenes de los dados de acuerdo a los valores obtenidos en las img
            this.dado1.Image = dados[val1-1];

        }

        public bool mover(Ficha f, int cant)
        {
            movimientoEspecial = false;

            Ficha aux = testPosition(f, cant);
            if (aux == null)
            {
                return false; // No es posible mover la ficha
            } else
            {

                log("Moviendo Ficha del equipo " + GUI.colores[f.team] + " con " + cant + " casillas.");

                //Matar una ficha
                if (aux.pos < 100)
                {
                    List<Ficha> l = fichasEn(aux.pos, f.team);//Obtiene las fichas que se encuentran en la posición donde se movió la ficha.
                    if (l.Count() == 1 && 
                        l[0].team != f.team && 
                        !esSeguro(aux.pos))
                    {
                        log(String.Format(GUI.fichaComida, GUI.colores[f.team], GUI.colores[l[0].team]));
                        enviarCasa(l[0]);
                        valorSeleccionado = 20;
                        movimientoEspecial = true;
                        log(GUI.mueve20);
                    }
                }

                ultimaFichaSeleccionada = f;
                int prevPos = f.pos;
                f.pos = aux.pos;
                f.img.Location = aux.img.Location;
                fixPos(prevPos, f.team);
                fixPos(f.pos, f.team);

                //Mover 10 posiciones cuando una ficha llegue a cielo
                if (f.pos == -3) {
                    valorSeleccionado = 10;
                    movimientoEspecial = true;
                    log(GUI.fichaCielo);
                }

                return true;//La ficha se ha podido mover
            }

        }

        public bool preview(Ficha f, int amount)
        {
            return (testPosition(f, amount) != null);
        }

        public Ficha testPosition(Ficha f, int cant)
        {

            if (f.enCasa()) {
                return moverACasa(f, ((f.pos / 100) + cant));
            }

            int prev = f.pos;

            int newPos = (f.pos + cant - 1) % Const.casillas.Length;
            previewFicha.pos = newPos + 1;
            previewFicha.img.Location = Const.casillas[newPos];

            //Entrando a casa
            if ((f.team == Const.ROJO    && prev <= 34   && previewFicha.pos > 34) ||
                (f.team == Const.AZUL   && prev <= 17   && previewFicha.pos > 17) ||
                (f.team == Const.AMARILLO && prev > 40    && prev <= 68 && (prev + cant) > 68) ||
                (f.team == Const.MORADO  && prev <= 51   && previewFicha.pos > 51))
            {
                int cantCasas = (prev + cant - Const.preCasillaGana[f.team]);

                //Verificar puentes para los caminos fuera de casa
                if (esPuente(f, Const.preCasillaGana[f.team]))
                {
                    return null;
                }

                return moverACasa(f, cantCasas);
            }

            //Si hay puentes (y no en casa), devolver null(false)
            if (esPuente(f, newPos + 1))
            {
                return null;
            }

            return this.previewFicha;

        }

        public Ficha moverACasa(Ficha f, int pos)
        {

            if (esPuenteCasa(f, pos))
            {
                return null; //Si hay puentes no puede mover
            }

            if (pos == 8)
            {
                previewFicha.pos = -3;//la ficha ha llegado
            } else if (pos > 8)
            {
                return null;//No puede mover
            } else
            {
                previewFicha.pos = pos * 100;  //Si la posicion es >= 100 se entiende que esta en casa
            }

            previewFicha.img.Location = Const.casillaEtapFinal[f.team, pos - 1];
            
            return this.previewFicha;

        }

        public bool nuevaFicha(int team)
        {

            for (int i=0; i<this.fichas.Length; i++)
            {
                int datos = Const.casillaCasa[team];

                if (this.fichas[i].team == team && 
                    this.fichas[i].pos == -1)
                {

                    bool matar = false;

                    // Verificar si hay al menos dos fichas en la misma casilla
                    List<Ficha> l = fichasEn(datos, -1);
                    if (l.Count() >= 2)
                    {

                        for (int j = 0; j < l.Count() && !matar; j++)
                        {
                            if (l[j].team != team)
                            {
                                enviarCasa(l[j]);
                                fixPos(datos, -1);
                                dadosActivos++;
                                valorSeleccionado = 20;
                                matar = true;
                                log(GUI.mueve20);
                            }
                        }

                    }
                    else matar = true;

                    if (!matar) return false;
                    // Establecer la nueva posición y ubicación visual de la ficha
                    this.fichas[i].pos = datos;
                    this.fichas[i].img.Location = Const.casillas[datos -1];
                    this.fichas[i].img.Enabled = true;
                    fixPos(datos, -1);
                    log(GUI.fichaFuera + GUI.colores[this.fichas[i].team]);
                    return true;
                }

            }

            return false;

        }

        protected void enviarCasa(Ficha f)
        {
            // Verificar si no es el turno adecuado o si la ficha ya está en casa
            if (turno != -1 && (f.pos >= 100 || f.pos <= 0))
                return;

            // Obtener un puesto libre en la casa del equipo correspondiente
            int puestoLibre = puestoLibreCasa(f.team);

            // Actualizar la ubicación visual de la ficha a la posición libre en la casa
            f.img.Location = Const.espaciosEsCasa[f.team, puestoLibre];

            // Establecer la posición de la ficha como -1, indicando que está en casa
            f.pos = -1;

        }

        private int puestoLibreCasa(int team)
        {

            bool[] puestos = new bool[4];

            // Verificar si hay puestos en la casa del equipo
            for (int i=0; i<this.fichas.Length; i++)
            {
                if (this.fichas[i].team == team)
                {// Si hay una ficha en casa se mmarca como false
                    if (this.fichas[i].pos == -1) {
                        puestos[i % 4] = false;
                    } else {
                        puestos[i % 4] = true;
                    }
                } 
            }
            //Busca si hay un puesto libre en la casa
            for (int i=0; i<puestos.Length; i++) {
                if (puestos[i])
                {
                    return i;
                }
            }

            return -1;

        }

        public void fixPos(int pos, int team)
        {

            List<Ficha> coins = fichasEn(pos, team);
            if (coins.Count == 1)//Si solo hay una ficha en la lista
            {
                if (coins[0].enCasa())
                {

                    int enPos = (coins[0].pos / 100) - 1;//Calcula posicion
                    coins[0].img.Location = Const.casillaEtapFinal[coins[0].team, enPos];//Actualiza la posicion con la posicion en la matriz CasiilaEtapFinal

                } else
                {
                    coins[0].img.Location = Const.casillas[(coins[0].pos - 1)];
                }
                
            }
            else if (coins.Count == 2)
            {
                Ficha prim = coins[0];
                Ficha segu = coins[1];

                int posX = prim.img.Location.X;
                int posY = prim.img.Location.Y;

                //EnCasa
                if (prim.enCasa() && segu.enCasa())
                {
                    // Si la ficha primaria pertenece al equipo MORADO o AZUL
                    if (prim.team == Const.MORADO || prim.team == Const.AZUL)
                    {
                        prim.img.Location = new Point(posX, posY + 20);
                        segu.img.Location = new Point(posX, posY - 20);
                    }
                    // Si la ficha primaria pertenece al equipo MORADO o AZUL
                    if (prim.team == Const.ROJO || prim.team == Const.AMARILLO)
                    {
                        prim.img.Location = new Point(posX + 20, posY);
                        segu.img.Location = new Point(posX - 20, posY);
                    }
                }
                //posicion normal horizontal
                else if (   (prim.pos >= 52 && prim.pos <= 59)    || 
                            (prim.pos >= 9 && prim.pos <= 17)     || 
                            (prim.pos >= 18 && prim.pos <= 25)    || 
                            (prim.pos >= 43 && prim.pos <= 51)    )
                {
                    prim.img.Location = new Point(posX, posY + 20);
                    segu.img.Location = new Point(posX, posY - 20);
                }
                //posicion normal vertical
                else
                {
                    prim.img.Location = new Point(posX + 20, posY);
                    segu.img.Location = new Point(posX - 20, posY);
                }

            }
            else return;

        }

        public bool esSeguro(int pos)//Verifica posiciones seguras
        {
            int[] seguras = { 5, 12, 17, 22, 29, 34, 39, 46, 51, 56, 63, 68 };

            return Array.Find(seguras, element => element.Equals(pos)) != 0;
        }

        public List<Ficha> fichasEn(int pos, int team) {

            List<Ficha> list = new List<Ficha>();// Si la posición es menor o igual a cero, retorna una lista vacía
            if (pos <= 0) return list;

            for (int i = 0; i < this.fichas.Length; i++)
            {

                bool condition;
                if (pos >= 100)
                {
                    // Si la posición es mayor o igual a 100, verifica si la ficha está en esa posición y pertenece al equipo especificado
                    condition = this.fichas[i].pos == pos && this.fichas[i].team == team;
                }
                // Si la posición es menor a 100, verifica si la ficha está en esa posición sin considerar el equipo
                else condition = this.fichas[i].pos == pos;

                // Si la condición se cumple, agrega la ficha a la lista
                if (condition)
                {
                    list.Add(this.fichas[i]);
                }

            }

            return list;

        }

        public bool esPuente(Ficha f, int dest) {//Verifica si hay dos fichas en una misma casilla

            if (f.pos == dest) { return false; }

            //Mapa --> posicion, fichas amount
            Dictionary<int, int> map = new Dictionary<int, int>();

            for (int i = 0; i < this.fichas.Length; i++) {

                if (map.ContainsKey(this.fichas[i].pos))
                {
                    map[this.fichas[i].pos]++;
                }
                else map.Add(this.fichas[i].pos, 1);

            }

            int origen = ((f.pos + 1) == 69) ? 1 : (f.pos + 1);
            bool cont = true;
           
            for (int i=origen; cont; i++)
            {

                i = (i == 69) ? 1 : i;

                if (map.ContainsKey(i) && map[i] > 1)
                {
                    log("Hay puente en su pocision " + i);
                    return true;
                }

                if (i == dest)
                {
                    cont = false;
                }
            }
            // Si el algoritmo anterior no se ejecuta significa que no habian puentes en la partida
            return false;
        }

        public bool esPuenteCasa(Ficha f, int dest)//Verifica si hay dos fichas del mismo equipo en una casilla
        {

            List<Ficha> coins = new List<Ficha>();
                        
            for (int i = 0; i < this.fichas.Length; i++)
            {
                if (this.fichas[i].team == f.team && this.fichas[i].enCasa())
                {
                    coins.Add(this.fichas[i]);
                }
            }

            int posActual = (f.pos / 100);
            int posFinal = (7 > dest) ? dest : 7;

            for (int i = posActual+1; i != posFinal+1; i++)
            {
                int cant = 0;
                for (int j = 0; j<coins.Count; j++)
                {
                    int pos = (coins[j].pos / 100);
                    if (pos == i)
                    {
                        cant++;
                    }
                }
                if (cant > 1) {
                    log("Hay Puente en su posicion " + i);
                    return true;
                }
            }

            return false;
        }

        public void log(string s)//Mensajes que se muestran en consola
        {
            Console.WriteLine("MSG: " + s);//Imprime el mensaje
            this.consola.AppendText(s + "\n");//Hace un salto de linea
            this.consola.SelectionStart = this.consola.Text.Length;
            this.consola.ScrollToCaret(); // Desplazamiento en el log
        }
    }
}
