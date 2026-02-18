using UnityEngine;

public class Watergirl : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    private SpriteRenderer spr;

    public float fuerzaSalto = 7f;
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
        // 1. MOVIMIENTO HORIZONTAL (SOLO A y D)
        float movimiento = 0;

        if (Input.GetKey(KeyCode.D)) // Derecha
        {
            movimiento = 1;
            spr.flipX = false;
        }
        else if (Input.GetKey(KeyCode.A)) // Izquierda
        {
            movimiento = -1;
            spr.flipX = true;
        }

        rb.velocity = new Vector2(movimiento * velocidad, rb.velocity.y);

        // Actualiza el parámetro 'corre' del Animator
        anim.SetFloat("corre", Mathf.Abs(movimiento));


        // 2. SALTO (SOLO TECLA W)
        if (Input.GetKeyDown(KeyCode.W) && estaEnSuelo)
        {
            rb.velocity = new Vector2(rb.velocity.x, fuerzaSalto);
            anim.SetBool("salta", true);
            estaEnSuelo = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Suelo"))
        {
            estaEnSuelo = true;
            anim.SetBool("salta", false);
        }
    }
}