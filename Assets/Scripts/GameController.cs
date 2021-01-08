using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Text marcadorVidas = null;
    [SerializeField] private UnityEngine.UI.Text marcadorPuntos = null;
    [SerializeField] private UnityEngine.UI.Text marcadorNivel = null;
    [SerializeField] private UnityEngine.UI.Text textoGameOver = null;
    [SerializeField] private UnityEngine.UI.Text textoFin = null;
    
    private int puntos;
    private int vidas;
    private int nivelActual;
    private float velocidadEnemigos;

    // Start is called before the first frame update
    void Start()
    {
        //Asignamos los valores de las variables locales con las del gameStatus
        vidas = FindObjectOfType<GameStatus>().Vidas;
        puntos = FindObjectOfType<GameStatus>().Puntos;
        nivelActual = FindObjectOfType<GameStatus>().NivelActual;
        velocidadEnemigos = FindObjectOfType<GameStatus>().VelocidadEnemigos;
        //Por el momento, los textos no los mostramos en pantalla
        textoGameOver.enabled = false;
        textoFin.enabled = false;
    }

    // Update is called once per frame
    void Update() 
    {
        //Para que el marcador que mostramos en el juego se mantenga actualizado
        marcadorVidas.text = FindObjectOfType<GameStatus>().GetVidas();
        marcadorPuntos.text = FindObjectOfType<GameStatus>().GetPuntos();
        marcadorNivel.text = FindObjectOfType<GameStatus>().GetNivel();
    }

    //Función encargada de establecer la pérdida de una vida del jugador
    public void PerderVida()
    {
        vidas--;
        FindObjectOfType<GameStatus>().Vidas = vidas;//Actualizamos en el gameStatus
        FindObjectOfType<Jugador>().SendMessage("Recolocar");//Recolocamos al Jugador
      
        if (vidas <= 0) TerminarPartida();  //Si el número de vidas es cero, terminamos
    }
    
    //Función auxiliar para ir hacia la Corrutina y establecer la forma de terminar la partida
    private void TerminarPartida() => StartCoroutine(VolverAlMenuPrincipal());

    //Función auxiliar para volver al menú principal
    private void MostrarMenu() => SceneManager.LoadScene("Menu");


    //Corrutina para volver al menú principal, dependiendo si hemos terminado los niveles o nos hemos
    //quedado sin vidas
    private IEnumerator VolverAlMenuPrincipal()
    {
        //Si las vidas del personaje son 0, mostramos Gave Over y volvemos al menú
        if (FindObjectOfType<GameStatus>().Vidas <= 0)
            textoGameOver.enabled = true;
        
        //Si no, significa que ha terminado el nivel 2
        else textoFin.enabled = true;

        Time.timeScale = 1;
        yield return new WaitForSecondsRealtime(3);
        MostrarMenu();
    }


    //Función encargada de establecer el avance de nivel
    private void AvanzarNivel()
    {
        nivelActual++;
        //Actualizamos el nivel actual en el gameStatus
        FindObjectOfType<GameStatus>().NivelActual = nivelActual;

        //Si el nivel actual es mayor que el nivel más alto (2), significa que hemos completado el juego,
        //Mostramos el texto de FIN y volvemos al menú principal
        if (nivelActual > FindObjectOfType<GameStatus>().NivelMasAlto)
          StartCoroutine(VolverAlMenuPrincipal());
          
        //Si no, avanzamos de nivel
        else
        {
            SceneManager.LoadScene("Nivel" + nivelActual);//Avanzamos de nivel
            FindObjectOfType<GameStatus>().NivelActual = nivelActual;//Actualizamos el nivel en el gameStatus
            FindObjectOfType<GameStatus>().VelocidadEnemigos += 2.5f;//Aumentamos la velocidad de los enemigos
        }
    }

    //Funciones para establecer los diferentes puntos obtenidos al recoger cada tipo de Item o eliminar a un enemigo
    public void AnotarItemDiamante()
    {
        puntos += 10;//Aumentamos los puntos correspondientes
        FindObjectOfType<GameStatus>().Puntos = puntos;//Actualizamos en el GameStatus
    }

    public void AnotarItemGema()
    {
        puntos += 20;//Aumentamos los puntos correspondientes
        FindObjectOfType<GameStatus>().Puntos = puntos;//Actualizamos en el GameStatus
    }

    public void AnotarEnemigoEliminado()
    {
        puntos += 40;//Aumentamos los puntos correspondientes
        FindObjectOfType<GameStatus>().Puntos = puntos;//Actualizamos en el GameStatus
    }
}
