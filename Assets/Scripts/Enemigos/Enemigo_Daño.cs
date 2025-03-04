using UnityEngine;

public class Enemigo_Daño : MonoBehaviour
{
    [SerializeField] private float daño;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Jugador"))
        {
            collision.GetComponent<Vida>().RecibirDaño(daño);
        }
    }
}
