using UnityEngine;
using UnityEngine.SceneManagement;
public class ControlPuerta : MonoBehaviour
{
    private Animator anim;
    private int personajesEnLaPuerta = 0;

    [SerializeField] private float velocidadAnim = 0.5f;
    [SerializeField] private float tiempoEspera = 1.5f;

    void Start()
    {
        anim = GetComponent<Animator>();
        anim.speed = velocidadAnim;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Detectamos si entro
        if (other.name.Contains("Fire") || other.name.Contains("Water"))
        {
            personajesEnLaPuerta++;
            anim.SetBool("estaCerca", true);

            // Si ambos están dentro 
            if (personajesEnLaPuerta >= 2)
            {
                Debug.Log("¡Ambos llegaron! Pasando de nivel...");
                Invoke("SiguienteNivel", tiempoEspera);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.name.Contains("Fire") || other.name.Contains("Water"))
        {
            personajesEnLaPuerta--;

            // Si no queda nadie, se cierra la puerta
            if (personajesEnLaPuerta <= 0)
            {
                personajesEnLaPuerta = 0;
                anim.SetBool("estaCerca", false);
            }
        }
    }

    void SiguienteNivel()
    {
        // Carga la siguiente escena en la lista de Build Settings
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}