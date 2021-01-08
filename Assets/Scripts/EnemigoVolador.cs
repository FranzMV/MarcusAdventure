using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoVolador : MonoBehaviour
{
    [SerializeField] Transform[] wayPoints = null;
    [SerializeField] float velocidadEnemigoPinchos;
    [SerializeField] Transform prefabExplosionEnemigoGis = null;
    [SerializeField] AudioClip sonidoEnemigoEliminado = null;

    private Vector3 siguientePosicion;
    private int numeroSiguientePosicion;
    private float distanciaCambio;
    private const string TAG_DISPARO_JUGADOR = "DisparoJugador";

    // Start is called before the first frame update
    void Start()
    {
        siguientePosicion = wayPoints[numeroSiguientePosicion].position;
        velocidadEnemigoPinchos = FindObjectOfType<GameStatus>().VelocidadEnemigos;
        distanciaCambio = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(
           transform.position,
           siguientePosicion,
           velocidadEnemigoPinchos * Time.deltaTime);

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
            //Instanciamos la explosión del enemigo correspondiente (Gris)
            Transform explosion = Instantiate(prefabExplosionEnemigoGis, 
                transform.position, 
                Quaternion.identity);

            //Reproducimos en sonido de eliminar enemigo en la posición de la cámara
            AudioSource.PlayClipAtPoint(sonidoEnemigoEliminado, Camera.main.transform.position);
            //Llamamos a gameController para anotar los puntos correspondientes
            FindObjectOfType<GameController>().SendMessage("AnotarEnemigoEliminado");

            Destroy(gameObject);//Destruimos el objeto enemigo
            Destroy(collision.gameObject);//Destruimos el disparo
            Destroy(explosion.gameObject, 1.5f);//Destruimos la explosión
        }
    }
}
