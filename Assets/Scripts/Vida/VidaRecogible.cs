using UnityEngine;

public class VidaRecogible : MonoBehaviour
{
    [SerializeField] private float valorVida;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Jugador"))
        {
            collision.GetComponent<Vida>().RecibirVida(valorVida);
            gameObject.SetActive(false);
        }
    }
}
