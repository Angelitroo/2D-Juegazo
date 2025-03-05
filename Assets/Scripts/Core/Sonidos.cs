using UnityEngine;

public class Sonidos : MonoBehaviour
{
    public static Sonidos instancia{ get; private set; }
    private AudioSource source;
    

    private void Awake()
    {
        instancia = this;
        source = GetComponent<AudioSource>();

        //guarda la musica entre un nivel y otro
        if(instancia == null)
        {
            instancia = this;
            DontDestroyOnLoad(gameObject);
        }
        //Quita duplicados
        else if (instancia != null && instancia != this)
        {
            Destroy(gameObject);
        }
    }

    public void ReproducirSonido(AudioClip sonido)
    {
        source.PlayOneShot(sonido);
    }
   
}
