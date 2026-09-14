using UnityEngine;

public class ScriptVini : MonoBehaviour
{
    [Header("Movimeto")]

    public float velocidade = 5f;
    public float forcaPulo = 6f;

    public Transform cameraPrimeiraPessoa;
    public Transform cameraTerceiraPessoa;

    private Rigidbody rb;
    private bool noChao;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump") && noChao)
        {
            rb.AddForce(Vector3.up * forcaPulo, ForceMode.Impulse);
        }
    }

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Transform cameraAtual;

        if (cameraPrimeiraPessoa.gameObject.activeSelf)
        {
            cameraAtual = cameraPrimeiraPessoa;
        }
        else
        {
            cameraAtual = cameraTerceiraPessoa;
        }

        Vector3 frente = cameraAtual.forward;
        Vector3 direita = cameraAtual.right;

        frente.y = 0f;
        direita.y = 0f;

        frente.Normalize();
        direita.Normalize();

        Vector3 direcao =
            (frente * v + direita * h) * velocidade;

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