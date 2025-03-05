using UnityEngine;

public class Enemigo_Proyectil : Enemigo_Daño//Daña al jugador
{
    [SerializeField] private float velocidad;
    [SerializeField] private float tiempoReseteo;
    private float duracion; 

    public void ActivarProyectil()
    {
        duracion = 0;
        gameObject.SetActive(true);
    }

    private void Update()
    {
        float velocidadMovimiento = velocidad * Time.deltaTime;
        transform.Translate(velocidadMovimiento, 0, 0);

        duracion += Time.deltaTime;
        if (duracion > tiempoReseteo) {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision); //ejecutra el metodo de la clase padre primero
        gameObject.SetActive(false);//cuando colisiona se desactiva/desaparece
    }
}
