using UnityEngine;
using UnityEngine.SceneManagement; // Importante para reiniciar escenas

public class Fireboy : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    private SpriteRenderer spr;

    public float fuerzaSalto = 8f;
    public float velocidad = 5f;
    public bool estaEnSuelo;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // 1. MOVIMIENTO HORIZONTAL
        float movimiento = 0;
        if (Input.GetKey(KeyCode.RightArrow))
        {
            movimiento = 1;
            spr.flipX = false;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            movimiento = -1;
            spr.flipX = true;
        }

        rb.velocity = new Vector2(movimiento * velocidad, rb.velocity.y);
        anim.SetFloat("corre", Mathf.Abs(movimiento));

        // 2. SALTO
        if (Input.GetKeyDown(KeyCode.UpArrow) && estaEnSuelo)
        {
            rb.velocity = new Vector2(rb.velocity.x, fuerzaSalto);
            anim.SetBool("salta", true);
            estaEnSuelo = false;
        }
    }

    // Se ejecuta al chocar con objetos sólidos
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Suelo"))
        {
            estaEnSuelo = true;
            anim.SetBool("salta", false);
        }

        // Si el agua NO es trigger
        if (collision.gameObject.CompareTag("Agua"))
        {
            ReiniciarNivel();
        }
    }

    // Se ejecuta si el agua tiene "Is Trigger" activado (Recomendado para efectos de muerte)
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Agua"))
        {
            ReiniciarNivel();
        }
    }

    void ReiniciarNivel()
    {
        // Obtiene el nombre de la escena actual y la vuelve a cargar
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}