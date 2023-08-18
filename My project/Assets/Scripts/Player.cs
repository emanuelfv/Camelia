using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] GameObject particulas;
    [SerializeField] Rigidbody gravidade;
    [SerializeField] public float horizontal, velocidade, forcaDePulo, forcaDeQueda, tempoDeQueda;
    [SerializeField] public bool noChao, pulando, puloDuplo, queda;
    
    //Camera
    [SerializeField] Camera visualizarcamera;
    [SerializeField] Transform cameraObj;


#region Métodos padrões
	void Start()
    {
        //gravidade receberá o componente Rigidbody do nosso objeto
        gravidade = GetComponent<Rigidbody>();
    }

	void Update()
    {
        //Aqui estamos puxando as funções que criamos para o nosso jogador, isto organiza o código
        Movimento();
        Pulo();
        Queda();
        Camera();
    }
#endregion

#region Métodos criados
	void Movimento()
    {
        //Movimentos horizontais recebe velocidade quando clicamos nas teclas "Horizontais"
        horizontal = Input.GetAxisRaw("Horizontal") * velocidade;

        //A velocidade da gravidade receberá novos movimentos verticais e horizontais de acordo com o que escrevemos acima
        gravidade.velocity = new Vector3(horizontal, gravidade.velocity.y, gravidade.velocity.z);

        //Mudando o ângulo horizontal do Personagem
        // if (horizontal > 0)
        // {
        //     //Com base no ângulo do componente transform, o sprite receberá o ângulo 
        //     transform.eulerAngles = new Vector3(0, 0, 0);
        // }
        // else if (horizontal < 0)
        // {
        //     transform.eulerAngles = new Vector3(0, 180, 0);
        // }
    }
    void Pulo()
    {
        //Verificamos se o botão de pulo está ativado, se o jogador está no chão e se ele não está pulando
        if (Input.GetButtonDown("Jump") && noChao && !pulando)
        {
            gravidade.AddForce(new Vector3(0, forcaDePulo, 0), ForceMode.Impulse);
            pulando = true;
            puloDuplo = true;
        }

        else if (Input.GetButtonDown("Jump") && !noChao && pulando && puloDuplo)
        {
            gravidade.AddForce(new Vector3(0, forcaDePulo, 0), ForceMode.Impulse);
            puloDuplo = false;
        }
    }
    void Queda()
    {
        if (Input.GetAxisRaw("Vertical") < 0 && !noChao && pulando)
        {
            queda = true;
            gravidade.AddForce(new Vector3(0, -forcaDeQueda, 0), ForceMode.Impulse);
        }
    }

    void Camera()
    {
        // cameraObj.position = Vector3.MoveTowards(cameraObj.position, new Vector3(transform.position.x, transform.position.y + 3, transform.position.z), 2 * Time.deltaTime);
    }
#endregion

#region Colisões
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Chão")
        {
            noChao = true;
            queda = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Pouso
        if (collision.gameObject.tag == "Chão")
        {
            pulando = false;
            //Particulas de queda
            if (queda == true)
            {
                Instantiate(particulas, new Vector3(transform.position.x, transform.position.y - 1, transform.position.z), Quaternion.identity);
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Chão")
        {
            noChao = false;
        }

    }
#endregion

#region Evitando Limbo
    // void TemporizadorDeQueda()
    // {        
    //     //Contagem de tempo fora do chão
    //     tempoDeQueda += Time.deltaTime;
    //     if (collision.gameObject.tag == null && tempoDeQueda > 5f)
    // }
#endregion
}
