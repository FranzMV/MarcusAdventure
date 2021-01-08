using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuertaSiguenteNivel : MonoBehaviour
{
    private const string TAG_JUGADOR = "Jugador";
    [SerializeField] AudioClip sonidoNivelCompletado = null;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update(){  }

    //Función que comprueba cuando el Jugador a llegado a la puerta para pasar al siguiente nivel o terminar la partida
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals(TAG_JUGADOR))
        {
            AudioSource.PlayClipAtPoint(sonidoNivelCompletado, Camera.main.transform.position);

            //Si llegamos a la salida del mapa y estamos en el nivel más alto (2) terminamos
            if (FindObjectOfType<GameStatus>().NivelActual == FindObjectOfType<GameStatus>().NivelMasAlto)
                 FindObjectOfType<GameController>().SendMessage("VolverAlMenuPrincipal");

            //Si no, avanzamos de nivel
            else FindObjectOfType<GameController>().SendMessage("AvanzarNivel");
        }
    }
}
