using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessPiece : MonoBehaviour
{
    public Vector3 target;
    float speed;
    GameController scriptGmCtrl;

    private void Start()
    {
        scriptGmCtrl = FindObjectOfType<GameController>();
        target = transform.position;
        speed = 10.0f;
    }
   
    void Update()
    {
        if (target != this.transform.position)
        {
            Vector3 mouv = Vector3.MoveTowards(this.transform.position, target, Time.deltaTime * speed);
            this.transform.position = mouv;
            scriptGmCtrl.mouvement = true;
        }
    }
}
