using UnityEngine;

public class CamaraController : MonoBehaviour
{
   [SerializeField] private float velocidad;
   private float posicionX;
   private Vector3 v = Vector3.zero;

   private void Update()
   {
    transform.position = Vector3.SmoothDamp(transform.position, new Vector3(posicionX, transform.position.y, transform.position.z), ref v, velocidad);
   }

   public void MoverNuevaHabitacion(Transform _nuevahabitacion)
    {
     posicionX = _nuevahabitacion.position.x;
    }
}
