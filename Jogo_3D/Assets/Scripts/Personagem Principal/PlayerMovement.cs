using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float velocidade = 5f;
    public float velocidadeRotacao = 10f;

    [Header("Pulo")]
    public float forcaPulo = 6f;
    public float distanciaChao = 0.2f;
    public LayerMask camadaChao;

    public Transform cameraTransform;

    private Rigidbody rb;
    private Vector3 direcaoMovimento;

    private bool estaNoChao;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 frenteCamera = cameraTransform.forward;
        Vector3 direitaCamera = cameraTransform.right;

        frenteCamera.y = 0;
        direitaCamera.y = 0;

        frenteCamera.Normalize();
        direitaCamera.Normalize();

        direcaoMovimento =
            frenteCamera * vertical +
            direitaCamera * horizontal;

        direcaoMovimento.Normalize();

        if (direcaoMovimento.magnitude > 0.1f)
        {
            Quaternion rotacaoDesejada =
                Quaternion.LookRotation(direcaoMovimento);

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                rotacaoDesejada,
                velocidadeRotacao * Time.deltaTime
            );
        }

        // Verifica se está no chão
        estaNoChao = Physics.Raycast(
            transform.position,
            Vector3.down,
            distanciaChao + 1f,
            camadaChao
        );

        // Pulo
        if (Input.GetKeyDown(KeyCode.Space) && estaNoChao)
        {
            rb.AddForce(
                Vector3.up * forcaPulo,
                ForceMode.Impulse
            );
        }
    }

    void FixedUpdate()
    {
        Vector3 movimento =
            direcaoMovimento * velocidade * Time.fixedDeltaTime;

        rb.MovePosition(rb.position + movimento);
    }
}