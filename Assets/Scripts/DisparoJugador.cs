using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparoJugador : MonoBehaviour
{
    private float valor_max_positivo; //Valor máximo positivo en el eje x en el que se debe destruir el disparo
    private float valor_max_negativo; //Valor máximo negativo en el eje x en el que se debe destruir el disparo
    private const string TAG_PLATAFORMAS = "Plataformas"; //Para comprobar si el disparo choca con las plataformas

    // Start is called before the first frame update
    void Start()
    {
        //Los valores, tanto negativos como positivos, se establecen a partir de la posición de la cámara principal
        valor_max_positivo = Camera.main.transform.position.x +10.0f;
        valor_max_negativo = Camera.main.transform.position.x -10.0f;
    }

    // Update is called once per frame
    void Update()
    {
        //Comprobamos los valores en el eje X para eliminar el disparo
        //teniéndo como referencia la posición de la cámara
         if (transform.position.x > valor_max_positivo ||
            transform.position.x < valor_max_negativo)
                Destroy(gameObject);
    }

    //Función para comprobar que el disparo se destruye al tocar una plataforma
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals(TAG_PLATAFORMAS))
            Destroy(gameObject);
    }
}
