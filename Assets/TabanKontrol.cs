using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabanKontrol : MonoBehaviour
{
    bool carpistiMi=false;
    [SerializeField] Rigidbody2D karakterrb;
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.CompareTag("AyiTepe")){
            Destroy(collision.gameObject.transform.parent.gameObject);
            karakterrb.AddForce(Vector2.up*10.0f,ForceMode2D.Impulse);
        }
    }
    void Update()
    {
        
    }
}
