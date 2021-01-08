using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BotonInicio : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        //Valores para establece al inicio de la partida
        FindObjectOfType<GameStatus>().Vidas = 3;
        FindObjectOfType<GameStatus>().NivelActual = 1;
        FindObjectOfType<GameStatus>().Puntos = 0;
    }

    // Update is called once per frame
    void Update()
    { }

    //Función para lanzar el primer nivel del juego
    public void LanzarJuego() => SceneManager.LoadScene("Nivel1");
}
