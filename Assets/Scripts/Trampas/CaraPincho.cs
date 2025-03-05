using UnityEngine;

public class CaraPincho : Enemigo_Daño //Daña al jugador
{
    [Header("Atributos del CaraPincho")]
    [SerializeField] private float velocidad;
    [SerializeField] private float alcance;//hasta donde ven estos
    [SerializeField] private float tiempoBusqueda;
    [SerializeField] private LayerMask capaJugador;

    [Header("Sonidos")]
    [SerializeField] private AudioClip sonidoImpacto;


    private Vector3 destino;
    private Vector3[] direcciones  = new Vector3[4];

    private float temporizador;
    private bool atacando;
    
    private void OnEnable()
    {
        Parar();
    }

    private void Update()
    {
        //mover al destino
        if(atacando)
        {
        transform.Translate(destino.normalized * Time.deltaTime * velocidad);
        }
        else
        {
            temporizador += Time.deltaTime;
            if(temporizador >= tiempoBusqueda)
            {
                BuscarJugador();
            }
        }
    }

    private void BuscarJugador()
    {
        CalcularDirecciones();

        //Comprobar si el jugador esta en el alcance de vision en alguna de las 4 direcciones
        for (int i = 0; i < direcciones.Length; i++)
        {
            Debug.DrawRay(transform.position, direcciones[i], Color.red);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direcciones[i], alcance, capaJugador);

            if(hit.collider != null && !atacando)
            {
                atacando = true;
                destino = direcciones[i];
                temporizador = 0;
            }
        }
    }

    private void CalcularDirecciones()
    {
        direcciones[0] = transform.right * alcance;//derecha
        direcciones[1] = -transform.right * alcance;//izquierda
        direcciones[2] = transform.up * alcance;//arriba
        direcciones[3] = -transform.up * alcance;//abajo
    }

    private void Parar()
    {
        destino = transform.position;
        atacando = false;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Sonidos.instancia.ReproducirSonido(sonidoImpacto);
        base.OnTriggerEnter2D(collision);
        //Parar cuando se choca con algo/contigo
        Parar();

    }
}
