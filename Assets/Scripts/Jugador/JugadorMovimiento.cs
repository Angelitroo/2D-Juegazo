using Unity.VisualScripting;
using UnityEngine;

public class JugadorMovimiento : MonoBehaviour
{
    [SerializeField] private float velocidad;
    [SerializeField] private float potenciaSalto;
    [SerializeField] private LayerMask capaSuelo; 
    [SerializeField] private LayerMask capaPared; 

    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private float saltoParedCooldown;
    private float horizontalInput;

    [Header("Sonido salto")]
    [SerializeField] private AudioClip sonidoSalto;

    //Para pillar los componentes
    private void Awake()
    { 
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        //Girar jugador izq-der
        if (horizontalInput > 0.01f)
            transform.localScale = Vector3.one;
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);

        //Animaciones
        anim.SetBool("correr", horizontalInput != 0);
        anim.SetBool("suelo", isSuelo());

        //Salto Pared
        if (saltoParedCooldown > 0.2f)
        {
            body.linearVelocity = new Vector2(horizontalInput * velocidad, body.linearVelocity.y);

            if (isPared() && !isSuelo())
            {
                body.gravityScale = 0;
                body.linearVelocity = Vector2.zero;
            }
            else
                body.gravityScale = 7;

            if (Input.GetKey(KeyCode.Space))
                Saltar();
                if(Input.GetKeyDown(KeyCode.Space) && isSuelo())
                    Sonidos.instancia.ReproducirSonido(sonidoSalto);
        }
        else
            saltoParedCooldown += Time.deltaTime;
    }

    private void Saltar()
    {
        if (isSuelo())
        {
            body.linearVelocity = new Vector2(body.linearVelocity.x, potenciaSalto);
            anim.SetTrigger("saltar");
        }
        else if (isPared() && !isSuelo())
        {
            if (horizontalInput == 0)
            {
                body.linearVelocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, 0);
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else
                body.linearVelocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 6);

            saltoParedCooldown = 0;
        }
    }
 

    private bool isSuelo()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, capaSuelo);
        return raycastHit.collider != null;
    }
    private bool isPared()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, capaPared);
        return raycastHit.collider != null;
    }
    public bool isAtaque()
    {
        return horizontalInput == 0 && isSuelo() && !isPared();
    }

}