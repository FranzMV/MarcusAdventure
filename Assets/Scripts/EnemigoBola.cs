using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoBola : MonoBehaviour
{
    [SerializeField] Transform[] wayPoints = null;
    [SerializeField] float velocidadEnemigoBola;
    [SerializeField] Transform prefabExplosionEnemigoBola = null;
    [SerializeField] AudioClip sonidoEnemigoEliminado = null;
    
    private Vector3 siguientePosicion;
    private int numeroSiguientePosicion;
    private float distanciaCambio;
    private const string TAG_DISPARO_JUGADOR = "DisparoJugador";

    //Ejercicio revisión del profesor: Que un enemigo muera tras recibir varios disparos
    private byte energia = 2;


    // Start is called before the first frame update
    void Start()
    {
        siguientePosicion = wayPoints[numeroSiguientePosicion].position;
        velocidadEnemigoBola = FindObjectOfType<GameStatus>().VelocidadEnemigos;
        distanciaCambio = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(
           transform.position,
           siguientePosicion,
           velocidadEnemigoBola * Time.deltaTime);

        if (Vector3.Distance(transform.position,
            siguientePosicion) < distanciaCambio)
        {
            numeroSiguientePosicion++;
            if (numeroSiguientePosicion >= wayPoints.Length)
                numeroSiguientePosicion = 0;

            siguientePosicion = wayPoints[numeroSiguientePosicion].position;
        }
    }

    //Función para comprobar la colisión del enemigo con el disparo del jugador
    //Esta función podría estar en el Objeto DisparoJugador, de manera que no se
    //se repita el mismo código en todos los enemigos
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Si el enemigo es alcanzado por el disparo del jugador
        if (collision.tag.Equals(TAG_DISPARO_JUGADOR))
        {
            energia--;//Ejercicio revisión del profesor. Éste enemigo debe morir al recibir dos disparos, no uno, como el resto
            Transform explosion = Instantiate(prefabExplosionEnemigoBola,
               transform.position,
               Quaternion.identity);

            //Instanciamos la explosión del enemigo correspondiente
            AudioSource.PlayClipAtPoint(sonidoEnemigoEliminado, Camera.main.transform.position);
            
          
            //Si el disparo del jugador ha acertado dos veces a éste enemigo, lo eliminamos
            if (energia <= 0)
            {
                //Llamamos a gameController para anotar los puntos correspondientes
                FindObjectOfType<GameController>().SendMessage("AnotarEnemigoEliminado");
                Destroy(gameObject);//Eliminamos al enemigo
                Destroy(collision.gameObject);//Destruimos el disparo
                Destroy(explosion.gameObject, 1.5f);//Destruimos la explosión
            }
            
            //Si hemos alcanzado al enemigo una sola vez
            Destroy(collision.gameObject);//Destruimos el disparo
            Destroy(explosion.gameObject, 1.5f);//Destruimos la explosión
        }
    }
}
