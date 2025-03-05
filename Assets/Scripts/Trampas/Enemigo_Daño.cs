using UnityEngine;

public class Enemigo_Daño : MonoBehaviour
{
    [SerializeField] protected float daño;

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Jugador"))
        {
            collision.GetComponent<Vida>().RecibirDaño(daño);
        }
    }
}
