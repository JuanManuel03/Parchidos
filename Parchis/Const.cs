﻿using System;
using System.Drawing;

public class Const
{

    // Id del equipo
    public const int MORADO = 0;
    public const int AMARILLO = 1;
    public const int AZUL = 2;
    public const int ROJO = 3;

    // Coordenadas de las casas
    public static int[] casillaCasa = { 56, 5, 22, 39 };
    public static int[] preCasillaGana = { 51, 68, 17, 34 };

    public static Point[,] espaciosEsCasa = {
        { new Point(78, 550), new Point(138, 550), new Point(78, 623), new Point(138, 623) },
        { new Point(515, 550), new Point(575, 550), new Point(515, 623), new Point(575, 623) },
        { new Point(515, 78), new Point(575, 78), new Point(515, 151), new Point(575, 151) },
        { new Point(78, 78), new Point(138, 78), new Point(78, 151), new Point(138, 151) }
    };

    // Circulo resaltador que indica el turno del jugador
    public static Point[] Resaltador = { new Point(10, 485), new Point(447, 485), new Point(447, 13), new Point(10, 13) };

    // Ultimas ocho casillas
    public static Point[,] casillaEtapFinal = { {
        new Point(44, 352),
        new Point(76, 352),
        new Point(107, 352),
        new Point(139, 352),
        new Point(171, 352),
        new Point(202, 352),
        new Point(231, 352),
        new Point(285, 352)
    }, {
        new Point(325, 657),
        new Point(325, 624),
        new Point(325, 589),
        new Point(325, 553),
        new Point(325, 520),
        new Point(325, 487),
        new Point(325, 454),
        new Point(325, 400)
    }, {
        new Point(607, 352),
        new Point(575, 352),
        new Point(543, 352),
        new Point(512, 352),
        new Point(480, 352),
        new Point(448, 352),
        new Point(416, 352),
        new Point(370, 352)
    }, {
        new Point(325, 47),
        new Point(325, 82),
        new Point(325, 115),
        new Point(325, 150),
        new Point(325, 185),
        new Point(325, 218),
        new Point(325, 250),
        new Point(325, 300)
    } };

    //Asignar casillas fichas = WinColores
    public static Point[] winMorado = {
        new Point(44, 352),
        new Point(76, 352),
        new Point(107, 352),
        new Point(139, 352),
        new Point(171, 352),
        new Point(202, 352),
        new Point(231, 352),
        new Point(285, 352)
    };

    public static Point[] winAmarillo = {
        new Point(325, 657),
        new Point(325, 624),
        new Point(325, 589),
        new Point(325, 553),
        new Point(325, 520),
        new Point(325, 487),
        new Point(325, 454),
        new Point(325, 400)
    };

    public static Point[] winAzul = {
        new Point(607, 352),
        new Point(575, 352),
        new Point(543, 352),
        new Point(512, 352),
        new Point(480, 352),
        new Point(448, 352),
        new Point(416, 352),
        new Point(370, 352)
    };

    public static Point[] winRojo = {
        new Point(325, 47),
        new Point(325, 82),
        new Point(325, 115),
        new Point(325, 150),
        new Point(325, 185),
        new Point(325, 218),
        new Point(325, 250),
        new Point(325, 300)
    };

    // Casillas normales o no especiales del juego
    public static Point[] casillas = {
        
        new Point(398, 690),
        new Point(398, 657),
        new Point(398, 624),
        new Point(398, 589),
        new Point(398, 553),
        new Point(398, 520),
        new Point(398, 487),
        new Point(398, 454),
        new Point(418, 430),
        new Point(448, 430),
        new Point(480, 430),
        new Point(512, 430),
        new Point(543, 430),
        new Point(575, 430),
        new Point(607, 430),
        new Point(638, 430),
        new Point(638, 352),
        new Point(638, 270),
        new Point(607, 270),
        new Point(575, 270),
        new Point(543, 270),
        new Point(512, 270),
        new Point(480, 270),
        new Point(448, 270),
        new Point(421, 270),
        new Point(398, 250),
        new Point(398, 218),
        new Point(398, 185),
        new Point(398, 150),
        new Point(398, 115),
        new Point(398, 82),
        new Point(398, 47),
        new Point(398, 13),
        new Point(325, 13),
        new Point(251, 13),
        new Point(251, 47),
        new Point(251, 82),
        new Point(251, 115),
        new Point(251, 150),
        new Point(251, 185),
        new Point(251, 218),
        new Point(251, 250),
        new Point(233, 275),
        new Point(202, 275),
        new Point(171, 275),
        new Point(139, 275),
        new Point(107, 275),
        new Point(76, 275),
        new Point(44, 275),
        new Point(13, 275),
        new Point(13, 352),
        new Point(13, 431),
        new Point(44, 431),
        new Point(76, 431),
        new Point(107, 431),
        new Point(139, 431),
        new Point(171, 431),
        new Point(202, 431),
        new Point(231, 431),
        new Point(254, 452),
        new Point(254, 487),
        new Point(254, 520),
        new Point(254, 553),
        new Point(254, 589),
        new Point(254, 624),
        new Point(254, 657),
        new Point(254, 690),
        new Point(325, 690)
    };

}
