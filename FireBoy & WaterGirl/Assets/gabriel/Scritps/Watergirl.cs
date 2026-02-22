using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para reiniciar el nivel

public class Watergirl : MonoBehaviour
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
        // 1. MOVIMIENTO HORIZONTAL (A y D)
        float movimiento = 0;

        if (Input.GetKey(KeyCode.D))
        {
            movimiento = 1;
            spr.flipX = false;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            movimiento = -1;
            spr.flipX = true;
        }

        rb.velocity = new Vector2(movimiento * velocidad, rb.velocity.y);
        anim.SetFloat("corre", Mathf.Abs(movimiento));

        // 2. SALTO (Tecla W)
        if (Input.GetKeyDown(KeyCode.W) && estaEnSuelo)
        {
            rb.velocity = new Vector2(rb.velocity.x, fuerzaSalto);
            anim.SetBool("salta", true);
            estaEnSuelo = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Detección de suelo
        if (collision.gameObject.CompareTag("Suelo"))
        {
            estaEnSuelo = true;
            anim.SetBool("salta", false);
        }

        // SI TOCA LA LAVA (Muerte de Watergirl)
        if (collision.gameObject.CompareTag("Lava"))
        {
            ReiniciarNivel();
        }
    }

    void ReiniciarNivel()
    {
        // Recarga la escena actual
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}