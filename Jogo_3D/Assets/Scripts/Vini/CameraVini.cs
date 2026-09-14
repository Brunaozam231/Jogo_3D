using UnityEngine;

public class CameraVini : MonoBehaviour
{
    [Header("Referências")]
    public Transform player;
    public Camera cameraPrimeiraPessoa;
    public Camera cameraTerceiraPessoa;

    [Header("Mouse")]
    public float sensibilidade = 2f;

    [Header("Terceira Pessoa")]
    public float distancia = 5f;
    public float altura = 2f;

    private float rotacaoX;
    private float rotacaoY;

    private bool primeiraPessoa = false;

    void Start()
    {
        // Trava o mouse no centro da tela
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Começa em terceira pessoa
        cameraPrimeiraPessoa.gameObject.SetActive(false);
        cameraTerceiraPessoa.gameObject.SetActive(true);

        rotacaoY = player.eulerAngles.y;
    }

    void Update()
    {
        // Pega o movimento do mouse
        float mouseX = Input.GetAxis("Mouse X") * sensibilidade;
        float mouseY = Input.GetAxis("Mouse Y") * sensibilidade;

        // Guarda a rotação do mouse
        rotacaoY += mouseX;
        rotacaoX -= mouseY;

        // Limita o quanto podemos olhar para cima e para baixo
        rotacaoX = Mathf.Clamp(rotacaoX, -80f, 80f);

        // Troca a câmera quando apertar C
        if (Input.GetKeyDown(KeyCode.C))
        {
            primeiraPessoa = !primeiraPessoa;

            cameraPrimeiraPessoa.gameObject.SetActive(primeiraPessoa);
            cameraTerceiraPessoa.gameObject.SetActive(!primeiraPessoa);
        }

        // Controla a câmera que estiver ativa
        if (primeiraPessoa)
        {
            PrimeiraPessoa();
        }
        else
        {
            TerceiraPessoa();
        }
    }

    void PrimeiraPessoa()
    {
        // Gira o Player para esquerda e direita
        player.rotation = Quaternion.Euler(0f, rotacaoY, 0f);

        // Gira a câmera para cima e para baixo
        cameraPrimeiraPessoa.transform.localRotation =
            Quaternion.Euler(rotacaoX, 0f, 0f);
    }

    void TerceiraPessoa()
    {
        // Rotação baseada no mouse
        Quaternion rotacao =
            Quaternion.Euler(rotacaoX, rotacaoY, 0f);

        // Calcula a posição da câmera atrás do Player
        Vector3 posicao =
            player.position
            + Vector3.up * altura
            - rotacao * Vector3.forward * distancia;

        // Coloca a câmera nessa posição
        cameraTerceiraPessoa.transform.position = posicao;

        // Faz a câmera olhar para o Player
        cameraTerceiraPessoa.transform.LookAt(
            player.position + Vector3.up * altura
        );
    }
}