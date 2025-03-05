using UnityEngine;

public class Enemigo_Lados : MonoBehaviour
{
    [SerializeField] private float distanciaMovimiento;
    [SerializeField] private float velocidad;
    [SerializeField] private float daño;
    private bool izquierda;
    private float izquierdaFinal;
    private float derechaFinal;

    void Awake()
    {
        izquierdaFinal = transform.position.x - distanciaMovimiento;
        derechaFinal = transform.position.x + distanciaMovimiento;
    }

    void Update()
    {
        if(izquierda)
        {
            if(transform.position.x > izquierdaFinal)
            {
                transform.position = new Vector2(transform.position.x - velocidad * Time.deltaTime, transform.position.y);
            }
            else
            {
                izquierda = false;
            }
        }
        else
        {
            if(transform.position.x < derechaFinal)
            {
                 transform.position = new Vector2(transform.position.x + velocidad * Time.deltaTime, transform.position.y);
            }
            else
            {
                izquierda = true;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Jugador"))
        {
            collision.GetComponent<Vida>().RecibirDaño(daño);
        }
    }
}
