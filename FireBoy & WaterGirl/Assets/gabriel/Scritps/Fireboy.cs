using UnityEngine;

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
        // 1. MOVIMIENTO HORIZONTAL (SOLO FLECHAS)
        float movimiento = 0;

        if (Input.GetKey(KeyCode.RightArrow)) // Flecha Derecha
        {
            movimiento = 1;
            spr.flipX = false;
        }
        else if (Input.GetKey(KeyCode.LeftArrow)) // Flecha Izquierda
        {
            movimiento = -1;
            spr.flipX = true;
        }

        rb.velocity = new Vector2(movimiento * velocidad, rb.velocity.y);

        // Actualiza el parámetro 'corre' del Animator
        anim.SetFloat("corre", Mathf.Abs(movimiento));


        // 2. SALTO (SOLO FLECHA ARRIBA)
        if (Input.GetKeyDown(KeyCode.UpArrow) && estaEnSuelo)
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