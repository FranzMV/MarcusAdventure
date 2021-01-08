using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDiamante : MonoBehaviour
{

    private const string TAG_JUGADOR = "Jugador";
    [SerializeField] private Transform particulasRecogerDiamante = null;
    [SerializeField] AudioClip sonidoDiamanteRecogido = null;

    // Start is called before the first frame update
    void Start()
    { }

    // Update is called once per frame
    void Update(){ }


    //Función para establecer las colisiones del Item diamante
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Si un diamante es recogido por el jugador
        if (other.tag.Equals(TAG_JUGADOR))
        {
            //Llamamos a GameController para anotar un diamante recogido
            FindObjectOfType<GameController>().SendMessage("AnotarItemDiamante");
            //Instanciamos el sonido de recoger un diamante en la posición de la cámara
            AudioSource.PlayClipAtPoint(sonidoDiamanteRecogido, Camera.main.transform.position);
            //Instanciamos la animación de partículas al recoger un diamante
            Transform diamanteRecogido = Instantiate(particulasRecogerDiamante,
                transform.position, Quaternion.identity);

            Destroy(gameObject); //Destruimos el diamante
            Destroy(diamanteRecogido.gameObject, 1.5f);//Destruimos la explosión de partículas del diamante
        }

    }
}
