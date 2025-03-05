using System.Collections;
using UnityEngine;

public class Trampa_Fuego : MonoBehaviour
{
    [SerializeField] private float daño;

    [Header("Timers Trampa de Fuego")]
    [SerializeField] private float tiempoEncendido;
    [SerializeField] private float tiempoActivo;
    private Animator anim;
    private SpriteRenderer spriteRenderer;
    private bool encendido; //Cuando la trampa se enciende/apaga
    private bool activo;// cuando esta activa y puede dañar al jugador

    [Header("Sonidos")]
    [SerializeField] private AudioClip sonidoFuego;

    private Vida vidaJugador;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if(vidaJugador != null && activo)
        {
            vidaJugador.RecibirDaño(daño);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Jugador"))
        {
            vidaJugador = collision.GetComponent<Vida>();

            if(!encendido)
            {
                StartCoroutine(ActivarTrampaFuego());
            }
            if(activo)
            {
                collision.GetComponent<Vida>().RecibirDaño(daño);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Jugador"))
        {
            vidaJugador = null;
        }
    }



    private IEnumerator ActivarTrampaFuego()
    {
        //notifica que va a dañar y se vuelve rojo
        encendido = true;
        spriteRenderer.color = Color.red;

        //Espera delay, activa trampa, resetea color
        yield return new WaitForSeconds(tiempoEncendido);
        Sonidos.instancia.ReproducirSonido(sonidoFuego);
        spriteRenderer.color = Color.white; 
        activo = true;
        anim.SetBool("activado", true);

        //Espera tiempo activo, desactiva trampa, resetea booleanos        
        yield return new WaitForSeconds(tiempoActivo);
        activo = false;
        encendido = false;
        anim.SetBool("activado", false);
    }
}
