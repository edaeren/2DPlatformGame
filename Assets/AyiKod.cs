using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AyiKod : MonoBehaviour
{
    [SerializeField] float mesafe=5.0f;
    Rigidbody2D _rb;
    Vector2 hiz=new Vector2(1.0f,0.0f);
    float xPosition;
    void Start()
    {
        _rb=GetComponent<Rigidbody2D>();
        xPosition=transform.position.x;
       
    }

    // Update is called once per frame
    void Update()
    {
         _rb.velocity=hiz;
         //ters yone gidecek
         if(Mathf.Abs(transform.position.x-xPosition)>=mesafe){
            hiz=-hiz;
            xPosition=transform.position.x;
            GetComponent<SpriteRenderer>().flipX=!GetComponent<SpriteRenderer>().flipX;
         }
    }
}
