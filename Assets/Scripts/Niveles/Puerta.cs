using UnityEngine;

public class Puerta : MonoBehaviour
{
    [SerializeField] private Transform habitacionAnterior;
    [SerializeField] private Transform habitacionSiguiente;
    [SerializeField] private CamaraController camaraController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Jugador"))
        {
            if(collision.transform.position.x < transform.position.x)
            {
                camaraController.MoverNuevaHabitacion(habitacionSiguiente);
                habitacionSiguiente.GetComponent<Nivel>().ResetearNivel(true);                
                habitacionAnterior.GetComponent<Nivel>().ResetearNivel(false);
            }
            else
            {
                camaraController.MoverNuevaHabitacion(habitacionAnterior);
                habitacionAnterior.GetComponent<Nivel>().ResetearNivel(true);
                habitacionSiguiente.GetComponent<Nivel>().ResetearNivel(false);  
            }
        }
    }
}
