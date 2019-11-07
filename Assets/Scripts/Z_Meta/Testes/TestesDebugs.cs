using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestesDebugs : MonoBehaviour {

    /*Esse script possui uma série de métodos que facilitam a visualização de algumas coisas no jogo. Basta colocá-lo em algum objeto e marcar as bools do que você quer visualizar*/

    public bool screenRay;
	
	// Update is called once per frame
	void Update ()
    {
        if (screenRay) { ScreenRay(); }
	}

    void ScreenRay()
    {
        //Se tiver um clique com o botão esquerdo
        if (Input.GetMouseButton(0))
        {
            //Pega a posição do mouse relativa ao ponto mais próximo da câmera
            Vector3 mousePosFar = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.farClipPlane);
            //Pega a posição do mouse relativa ao ponto mais distante da câmera
            Vector3 mousePosNear = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane);

            //Transforma aqueles dois pontos relativos à câmera em pontos relativos ao mundo
            Vector3 mousePosF = Camera.main.ScreenToWorldPoint(mousePosFar);
            Vector3 mousePosN = Camera.main.ScreenToWorldPoint(mousePosNear);

            //Traça uma linha entre o ponto mais próximo e o mais distante da câmera
            Debug.DrawRay(mousePosN, mousePosF - mousePosN, Color.green);
        }
    }
}
