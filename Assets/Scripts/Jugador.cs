using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugador : MonoBehaviour
{
    [SerializeField] private float velocidad;
    [SerializeField] private float velocidadSalto;

    //Variable para establecer haciá donde mira el jugador en función de su dirección
    [SerializeField] private SpriteRenderer spriteRenderer; 

    [SerializeField] private float salto;
    [SerializeField] private float velocidadDisparo;

    [SerializeField] Transform prefabDisparo = null;
    [SerializeField] AudioClip sonidoDisparo = null;
    [SerializeField] AudioClip sonidoMorirJugador = null;

    private const string TAG_RIO_LAVA = "RioLava";
    private const string TAG_PLATAFORMA_PINCHOS = "Pinchos";
    private const string TAG_ENEMIGO_BOLA = "EnemigoBola";
    private const string TAG_ENEMIGO_PINCHOS = "EnemigoPinchos";
    private const string TAG_ENEMIGO_VOLADOR = "EnemigoVolador";

    private new Rigidbody2D rigidbody2D;
    private Vector3 posicionInicial;
    private float alturaPersonaje;
    private Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        posicionInicial = transform.position;
        velocidad = 3.5f;
        velocidadSalto = 20.0f;
        velocidadDisparo = 10.0f;
        rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        alturaPersonaje = GetComponent<Collider2D>().bounds.size.y;
        animator = gameObject.GetComponent<Animator>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //Comprobamos los movimientos del jugador para establecer hacia donde mira, 
        //la dirección del disparo y la animación correspondiente
        if (Input.GetKey("right"))
        {
            //Movimiento hacia la derecha
            rigidbody2D.velocity = new Vector2(velocidad, rigidbody2D.velocity.y);//Velocidad en el eje X positiva
            spriteRenderer.flipX = false;//Para establecer hacia dónde mira el jugador
            animator.SetBool("Correr", true); //Activamos la animación de correr
            animator.SetBool("Saltando", false); //Desactivamos la animación de saltar
            velocidadDisparo = 10; //Velocidad de disparo positiva
        }
        else if (Input.GetKey("left"))
        {
            //Movimiento hacia la izquierda
            rigidbody2D.velocity = new Vector2(-velocidad, rigidbody2D.velocity.y);//Velocidad en el ejex X negativa
            spriteRenderer.flipX = true;//Cambiamos hacia dónde mira el jugador, en éste caso, hacia la izquierda
            animator.SetBool("Correr", true);//Activamos la animación de correr 
            animator.SetBool("Saltando", false);//Desactivamos el salto
            velocidadDisparo = -10;//Velocidad de disparo negativa
        }
        else
        {
            //Jugador estático
            rigidbody2D.velocity = new Vector2(0, rigidbody2D.velocity.y);//Jugador estático
            animator.SetBool("Correr", false);//Desactivamos la animación de correr
            animator.SetBool("Saltando", false); //Desactivamos la animación de saltar
        }

        //Salto del Jugador
        salto = Input.GetAxis("Jump");
        if (salto > 0)
        {
            RaycastHit2D impacto = Physics2D.Raycast(transform.position, new Vector2(0, -1));

            if (impacto.collider != null)
            {
                float distanciaAlSuelo = impacto.distance;
                bool tocandoElSuelo = distanciaAlSuelo < alturaPersonaje;
                animator.SetBool("Correr", false);
                animator.SetBool("Saltando", true);

                if (tocandoElSuelo)
                {
                    Vector2 fuerzaSalto = new Vector3(0, velocidadSalto, 0);
                    GetComponent<Rigidbody2D>().AddForce(fuerzaSalto);
                    animator.SetBool("Saltando", false);
                }
            }
        }

        //Disparo del Jugador
        if (Input.GetButtonDown("Fire1"))
        {
            //Instanciamos el disparo del Jugador
            Transform disparo = Instantiate(
                prefabDisparo,
                transform.position,
                Quaternion.identity);

            //Establecemos su velocidad
            disparo.gameObject.GetComponent<Rigidbody2D>().velocity =
                new Vector3(velocidadDisparo, 0,0);

            //Reproducimos el sonido del disparo en la posición donde se encuentra la cámara
            AudioSource.PlayClipAtPoint(sonidoDisparo, Camera.main.transform.position);
        }
    }

    //Función para comprobar las colisiones del Jugador, perder una vida y lanzar el sonido correspondiente
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Equals(TAG_RIO_LAVA)||
            collision.tag.Equals(TAG_PLATAFORMA_PINCHOS)||
            collision.tag.Equals(TAG_ENEMIGO_BOLA)||
            collision.tag.Equals(TAG_ENEMIGO_PINCHOS)||
            collision.tag.Equals(TAG_ENEMIGO_VOLADOR)){
           
            FindObjectOfType<GameController>().SendMessage("PerderVida");
            AudioSource.PlayClipAtPoint(sonidoMorirJugador, Camera.main.transform.position);
        }
    }

    //Función para recolocar al Jugador en su posición inicial cuando ha perdido una vida
    public void Recolocar() => transform.position = posicionInicial;

}
