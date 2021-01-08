using UnityEngine;

public class Trampolin : MonoBehaviour
{
    private Animator animator;
    private float fuerzaSalto;
    private const string TAG_JUGADOR = "Jugador";

    // Start is called before the first frame update
    void Start() 
    {
        animator = gameObject.GetComponent<Animator>();
        fuerzaSalto = 10.5f;
    }

    // Update is called once per frame
    void Update(){ }

    //Función para comprobar cuando el Jugador toca el trampolin e impulsarlo hacia arriba
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals(TAG_JUGADOR))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().velocity =
                (Vector2.up * fuerzaSalto);
            animator.Play("SaltoTrampolin");
        }
    }
}
