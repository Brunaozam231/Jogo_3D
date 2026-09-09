using UnityEngine;

public class ScriptVini : MonoBehaviour
{
    [Header("Movimeto")]

    public float velocidade = 5f;
    public float forcaPulo = 6f;

    private Rigidbody rb;
    private bool noChao;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        if(Input.GetButtonDown("Jump") && noChao)
        {
            rb.AddForce(Vector3.up * forcaPulo, ForceMode.Impulse);
        }
    }

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 direcao = new Vector3(h, 0f, v) * velocidade;
        direcao.y = rb.linearVelocity.y;

        rb.linearVelocity = direcao;
    }

    void OnCollisionStay(Collision colisao)
    {
        if (colisao.gameObject.CompareTag("Chao"))
        {
            noChao = true;
        }
    }

    void OnCollisionExit(Collision colisao)
    {
        noChao = false;
    }
}