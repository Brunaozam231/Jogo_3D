using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movimento")]
    public float velocidade = 5f;
    public float velocidadeRotacao = 10f;

    [Header("Pulo")]
    public float forcaPulo = 6f;
    public Transform groundCheck;
    public float raioGroundCheck = 0.25f;
    public LayerMask camadaChao;

    [Header("Referências")]
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
        // Movimento
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 frenteCamera = cameraTransform.forward;
        Vector3 direitaCamera = cameraTransform.right;

        frenteCamera.y = 0f;
        direitaCamera.y = 0f;

        frenteCamera.Normalize();
        direitaCamera.Normalize();

        direcaoMovimento =
            frenteCamera * vertical +
            direitaCamera * horizontal;

        direcaoMovimento.Normalize();

        // Rotação do personagem
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

        // Verifica se o personagem está encostando no chão
        estaNoChao = Physics.CheckSphere(
            groundCheck.position,
            raioGroundCheck,
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