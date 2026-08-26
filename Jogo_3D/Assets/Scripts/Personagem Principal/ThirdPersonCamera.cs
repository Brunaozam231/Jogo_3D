using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [Header("Referências")]
    public Transform jogador;

    [Header("Câmera")]
    public float distancia = 5f;
    public float altura = 2f;
    public float sensibilidade = 3f;

    [Header("Limite Vertical")]
    public float anguloMinimo = -20f;
    public float anguloMaximo = 60f;

    private float rotacaoX = 15f;
    private float rotacaoY = 0f;

    void Start()
    {
        // Trava o mouse no centro da tela
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void LateUpdate()
    {
        // Movimento do mouse
        float mouseX = Input.GetAxis("Mouse X") * sensibilidade;
        float mouseY = Input.GetAxis("Mouse Y") * sensibilidade;

        rotacaoY += mouseX;
        rotacaoX -= mouseY;

        // Impede a câmera de dar uma volta vertical completa
        rotacaoX = Mathf.Clamp(
            rotacaoX,
            anguloMinimo,
            anguloMaximo
        );

        // Cria a rotação da câmera
        Quaternion rotacao = Quaternion.Euler(
            rotacaoX,
            rotacaoY,
            0f
        );

        // Ponto que a câmera vai olhar
        Vector3 alvo = jogador.position + Vector3.up * altura;

        // Coloca a câmera atrás do jogador
        Vector3 posicaoCamera =
            alvo + rotacao * new Vector3(0f, 0f, -distancia);

        transform.position = posicaoCamera;

        // Faz a câmera olhar para o jogador
        transform.LookAt(alvo);
    }
}