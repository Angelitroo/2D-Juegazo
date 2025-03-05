using UnityEngine;

public class EnemigoMelee : MonoBehaviour
{
    [Header("Parametros de ataque")]
    [SerializeField] private float tiempoAtaque;
    [SerializeField] private int daño;
    [SerializeField] private float alcance;

    [Header("Parametros de vision")]
    [SerializeField] private float distanciaVision;
    [SerializeField] private BoxCollider2D boxCollider;

    [SerializeField] private LayerMask capaJugador;
    private float temporizador = Mathf.Infinity;

    [Header("Sonido ataque")]
    [SerializeField] private AudioClip sonidoAtaque;

    private EnemigoPatrulla enemigoPatrulla;
    private Animator anim;
    private Vida vidaJugador;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        enemigoPatrulla = GetComponentInParent<EnemigoPatrulla>();
    }

    private void Update()
    {
        temporizador += Time.deltaTime;

        // Atacar al jugador solo cuando sea visible
        if (JugadorVisible())
        {
            if (temporizador >= tiempoAtaque && vidaJugador.vidaActual > 0)
            {
                temporizador = 0;
                anim.SetTrigger("ataqueMelee");
                Sonidos.instancia.ReproducirSonido(sonidoAtaque);
            }
        }

        if(enemigoPatrulla != null)
        {
            enemigoPatrulla.enabled = !JugadorVisible();
        }

    }

    private bool JugadorVisible()
    {
        RaycastHit2D hit = 
            Physics2D.BoxCast(boxCollider.bounds.center + transform.right * alcance * transform.localScale.x * distanciaVision,
            new Vector3(boxCollider.bounds.size.x * alcance, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, Vector2.left, 0, capaJugador);

        if(hit.collider != null)
        {
            vidaJugador = hit.collider.GetComponent<Vida>();
        }

        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * alcance * transform.localScale.x * distanciaVision,
            new Vector3(boxCollider.bounds.size.x * alcance, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    private void Atacar()
    {
        if(JugadorVisible())
        {
            //Dañar jugador
            vidaJugador.RecibirDaño(daño);
        }
    }

}