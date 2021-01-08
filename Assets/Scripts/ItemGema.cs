using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGema : MonoBehaviour
{
    private const string TAG_JUGADOR = "Jugador";
    [SerializeField] private Transform particulasRecogerGema = null;
    [SerializeField] AudioClip sonidoGemaRecogida = null;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update(){ }


    //Función para establecer las colisiones del Item Gema
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Si una gema es recogida por el jugador
        if (other.tag.Equals(TAG_JUGADOR))
        {
            //Llamamos a GameController para anotar una gema recogida
            FindObjectOfType<GameController>().SendMessage("AnotarItemGema");
            //Instanciamos el sonido de recoger una gema en la posición de la cámara
            AudioSource.PlayClipAtPoint(sonidoGemaRecogida, Camera.main.transform.position);
            //Instanciamos la animación de partículas al recoger una gema
            Transform gemaRecogida = Instantiate(particulasRecogerGema,
                transform.position, Quaternion.identity);
           
            Destroy(gameObject); //Destruimos la gema
            Destroy(gemaRecogida.gameObject, 1.5f);//Destruimos la explosión de partículas de la gema
        }

    }
}
