using UnityEngine;
using UnityEngine.SceneManagement;
public class ControlPuerta : MonoBehaviour
{
    private Animator anim;
    private int personajesEnLaPuerta = 0; // Contador de personajes

    [SerializeField] private float velocidadAnim = 0.5f;
    [SerializeField] private float tiempoEspera = 1.5f; // Tiempo antes de cambiar de nivel

    void Start()
    {
        anim = GetComponent<Animator>();
        anim.speed = velocidadAnim;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Detectamos si entró Fireboy o Watergirl
        if (other.name.Contains("Fire") || other.name.Contains("Water"))
        {
            personajesEnLaPuerta++; // Sumamos 1 al contador
            anim.SetBool("estaCerca", true); // Se abre si hay al menos uno

            // Si ambos están dentro (2 personajes)
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
            personajesEnLaPuerta--; // Restamos 1 al salir

            // Si no queda nadie, se cierra la puerta
            if (personajesEnLaPuerta <= 0)
            {
                personajesEnLaPuerta = 0; // Seguridad para no tener números negativos
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