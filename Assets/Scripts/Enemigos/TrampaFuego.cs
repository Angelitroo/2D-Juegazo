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

    void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Jugador"))
        {
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
    private IEnumerator ActivarTrampaFuego()
    {
        //notifica que va a dañar y se vuelve rojo
        encendido = true;
        spriteRenderer.color = Color.red;

        //Espera delay, activa trampa, resetea color
        yield return new WaitForSeconds(tiempoEncendido);
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
