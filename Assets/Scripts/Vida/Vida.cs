using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Vida : MonoBehaviour
{
    [Header("Vida")]
    [SerializeField] private float vidaInicial;
    public float vidaActual {get; private set;}
    private Animator anim;
    private bool muerto;

    [Header("iFrame")]
    [SerializeField] private float iframeDuracion;
    [SerializeField] private int numeroFlashes;
    private SpriteRenderer spriteRenderer;

    [Header("Componentes")]
    [SerializeField] private Behaviour[] componentes;
    private bool invulnerable;

    [Header("Sonido muerte")]
    [SerializeField] AudioClip sonidoMuerte;

    [Header("Sonido daño")]
    [SerializeField] AudioClip sonidoDaño;

    private void Awake()
    {
        vidaActual = vidaInicial;   
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    } 

    public void RecibirDaño(float daño)
    {
        if(invulnerable) return;
        vidaActual = Mathf.Clamp(vidaActual - daño, 0, vidaInicial);
        if (vidaActual >0)
        {
            anim.SetTrigger("daño");
            StartCoroutine(Invulnerabilidad());
            //iframe
            Sonidos.instancia.ReproducirSonido(sonidoDaño);
        } 
        else
        {
            if (!muerto)
                {
                    anim.SetTrigger("muerto");
                // Desactivar componentes
                foreach (Behaviour componente in componentes)
                {
                    componente.enabled = false;
                }

                muerto = true;
                Sonidos.instancia.ReproducirSonido(sonidoMuerte);

                // Llamar al método Respawneo del script JugadorRespawn
                GetComponent<JugadorRespawn>().ComprobarRespawn();
                }
            
        }
    }

    public void RecibirVida(float valor){
        vidaActual = Mathf.Clamp(vidaActual + valor, 0, vidaInicial);
    }

    private IEnumerator Invulnerabilidad()
    {
        invulnerable = true;
        Physics2D.IgnoreLayerCollision(8, 9, true);
        //duracion invulnerabilidad
        for(int i = 0; i < numeroFlashes; i++)
        {
            spriteRenderer.color = new Color(1,0,0,0.5f);
            yield return new WaitForSeconds(iframeDuracion / (numeroFlashes * 2));
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(iframeDuracion / (numeroFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(8, 9, false);
        invulnerable = false;
    }

    private void Desactivar()
    {
        gameObject.SetActive(false);
    }

    public void Respawn()
    {
        muerto = false;
        RecibirVida(vidaInicial);
        anim.ResetTrigger("muerto");
        anim.Play("Idle");
        StartCoroutine(Invulnerabilidad());

        // Reactivar componentes
        foreach (Behaviour componente in componentes)
        {
            componente.enabled = true;
        }
    }
}