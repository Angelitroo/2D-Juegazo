using UnityEngine;


public class JugadorRespawn : MonoBehaviour
{
    [SerializeField] private AudioClip sonidoCheckpoint;
    private Transform checkpointActual;
    private Vida vidaJugador;
    private UIManager uiManager;

    private void Awake()
    {
        vidaJugador = GetComponent<Vida>();
        uiManager = FindObjectOfType<UIManager>();
    }

    public void ComprobarRespawn()
    {
        //Comprobar si hay checkpoint
        if(checkpointActual == null)
        {
            uiManager.GameOver();
            return;
        } 
        transform.position = checkpointActual.position;
        vidaJugador.Respawn();

        //mover camara
        Camera.main.GetComponent<CamaraController>().MoverNuevaHabitacion(checkpointActual.parent);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Checkpoint"))
        {
            checkpointActual = collision.transform;
            Sonidos.instancia.ReproducirSonido(sonidoCheckpoint);
            collision.GetComponent<Collider2D>().enabled = false;
            collision.GetComponent<Animator>().SetTrigger("aparecer");
        }
    }
}