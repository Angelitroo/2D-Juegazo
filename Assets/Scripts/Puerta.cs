using UnityEngine;

public class Puerta : MonoBehaviour
{
    [SerializeField] private Transform habitacionAnterior;
    [SerializeField] private Transform habitacionSiguiente;
    [SerializeField] private CamaraController camaraController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(collision.transform.position.x < transform.position.x)
            {
                camaraController.MoverNuevaHabitacion(habitacionSiguiente);
            }
            else
            {
                camaraController.MoverNuevaHabitacion(habitacionAnterior);
            }
        }
    }
}
