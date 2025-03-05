using UnityEngine;

public class VidaRecogible : MonoBehaviour
{
    [SerializeField] private float valorVida;
    [Header("Sonidos")]
    [SerializeField] private AudioClip sonidoRecogida;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Jugador"))
        {
            Sonidos.instancia.ReproducirSonido(sonidoRecogida);
            collision.GetComponent<Vida>().RecibirVida(valorVida);
            gameObject.SetActive(false);
        }
    }
}
