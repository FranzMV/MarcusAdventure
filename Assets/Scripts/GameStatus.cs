using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatus : MonoBehaviour
{
    private int puntos = 0;//Puntos conseguidos por el jugador
    private int vidas = 3;//Número total de vidas del jugador (En la revisión del profesor, pide poner el número a 5)
    private int nivelActual = 1;//Para saber en el nivel que estamos
    private int nivelMasAlto = 2;//Nivel más alto del juego
    private float velocidadEnemigos = 1.5f;//Velocidad inicial de los enemigos

    //Getters y setters para acceder a los valores y poder leerlos y mostrarlos desde la clase GameController
    public int Puntos { get => puntos; set => puntos = value; }
    public int Vidas { get => vidas; set => vidas = value; }
    public int NivelActual { get => nivelActual; set => nivelActual = value; }
    public int NivelMasAlto { get => nivelMasAlto; set => nivelMasAlto = value; }
    public float VelocidadEnemigos { get => velocidadEnemigos; set => velocidadEnemigos = value; }

    //Función para manejar el número de Objetos GameStatus que hay creados en cada momento en el juego
    private void Awake()
    {
        //Comprobamos cuántos objetos hay de GameStatus
        int gameStatusCount = FindObjectsOfType<GameStatus>().Length;

        //Si hay más de uno, lo destruimos
        if (gameStatusCount > 1) Destroy(gameObject);

        //Si no, dejamos el actual
        else DontDestroyOnLoad(gameObject);
    }

    //Funcioenes para obtener el contador de vidas, el marcador de puntos y el nivel actual y poder mostrarlos como texto 
    //en un marcador del juego
    public string GetVidas() => "Vidas: " + Vidas;
    public string GetPuntos() => "Puntos: " + Puntos;
    public string GetNivel() => "Nivel: " + NivelActual;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update(){ }
}
