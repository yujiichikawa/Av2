using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    public float velocidade;
    public float alturaPulo;
    public Rigidbody2D oRigidbody2D;
    public SpriteRenderer oSpriteRenderer;
    public AudioSource somPulo;
    public bool isChao;
    public Transform VerificarChao;
    public float raioVerificacao;
    public LayerMask layerChao;
    private GameController gcPlayer;


    // Start is called before the first frame update
    void Start()
    {
        gcPlayer = GameController.gc;
        gcPlayer.coins = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        Move();
        Pular();
    }

    public void Move()
    {
        float inputMove = Input.GetAxisRaw("Horizontal");
        oRigidbody2D.velocity = new Vector2(inputMove * velocidade, oRigidbody2D.velocity.y);

        if (inputMove > 0)
        {
            oSpriteRenderer.flipX = false;
        }

        if (inputMove < 0)
        {
            oSpriteRenderer.flipX = true;
        }
    }

    public void Pular()
    {
        isChao = Physics2D.OverlapCircle(VerificarChao.position, raioVerificacao, layerChao);

        if (Input.GetKeyDown(KeyCode.Space) && isChao == true)
        {
            oRigidbody2D.velocity = Vector2.up * alturaPulo;
            somPulo.Play();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Coins")
        {
            Destroy(collision.gameObject);
            gcPlayer.coins++;
            gcPlayer.coinsText.text = gcPlayer.coins.ToString();
        }

    }


}
