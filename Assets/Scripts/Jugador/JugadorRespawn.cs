using System.Collections.Generic;
using UnityEngine;

public class JugadorRespawn : MonoBehaviour
{
    [SerializeField] private AudioClip sonidoCheckpoint;
    private Transform checkpointActual;
    private Vida vidaJugador;
    private UIManager uiManager;
    private HashSet<Transform> checkpointsUsados = new HashSet<Transform>();

    private void Awake()
    {
        vidaJugador = GetComponent<Vida>();
        uiManager = FindObjectOfType<UIManager>();
    }

    public void ComprobarRespawn()
    {
        //Comprobar si hay checkpoint
        if (checkpointActual == null || checkpointsUsados.Contains(checkpointActual))
        {
            uiManager.GameOver();
            return;
        }

        transform.position = checkpointActual.position;
        vidaJugador.Respawn();

        //mover camara
        Camera.main.GetComponent<CamaraController>().MoverNuevaHabitacion(checkpointActual.parent);

        // Marcar el checkpoint como usado
        checkpointsUsados.Add(checkpointActual);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Checkpoint"))
        {
            checkpointActual = collision.transform;
            Sonidos.instancia.ReproducirSonido(sonidoCheckpoint);
            collision.GetComponent<Collider2D>().enabled = false;
            collision.GetComponent<Animator>().SetTrigger("aparecer");
        }
    }
}