using System;
using System.Drawing;

namespace Parchis
{
    class GUI
    {
        public const string nomJuego = "Parchidos";

        public static string[] colores = { morado, amarillo, azul, rojo };

        public const string morado = "ISOBU";
        public const string azul = "MATATABI";
        public const string amarillo = "SHUKAKU";
        public const string rojo = "KURAMA";

        public const string botonInicio = "Iniciar Juego";
        public const string stop = "Detener Juego";
        public const string saltarturno = "Saltar turno";
        public const string turnoDe = "Turno de: ";
        public const string iniciarconFicha = "Ficha Inicial?";
        public const string botonDado = "Lanzar Dados";

        public const string LoginiciarJuego = "Juego Iniciado.";
        public const string LogdetenerJuego = "Juego Detenido.";
        public const string cancelOperation = "Cancelado.";

        public const string mensajeInicio = "¿Estas listo para iniciar el juego?";
        public const string mensajeDetener = "¿Estas seguro de detener el Juego?";

        //Cadenas de texto del juego
        public const string bridgesPresent = "No se puede mover.";
        public const string mueve20 = "Corre 20 casillas.";
        public const string lanzarDados = "Lanzando Dados...";
        public const string resultadoDado = "los dados arrojaron {0} y {1}";
        public const string dadosSumanSeis = "La suma es 6, Ficha sale de Casa";
        public const string noMovimiento = "No puedes mover ninguna Ficha.";
        public const string fichaCielo = "La ficha llego al Cielo!";
        public const string fichaComida = "La Ficha {0} mato a la Ficha {1}";
        public const string elGanador = "El Ganador es: ";
        public const string fichaFuera = "Has sacado la Ficha ";
        public const string roj = "Rojo";
        public const string azu = "Azul";
        public const string ama = "Amarillo";
        public const string mor = "Morado";

        // Sistema de dados
        public const string seleccionarDado = "Dado {0} seleccionado";
        public const string valorDado = "Para el valor {0}, selecciona una Ficha.";
        public const string paresConsecutivos = "Has sacado 3 Pares seguidos!";
        public const string quienEmpieza = "Se ha decidido que {0} empieza.";

        // Dubug
        public const string debugCoinsCount = "Hay {0} Fichas en esta casilla.";
    }
}
