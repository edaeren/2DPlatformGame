using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
   Vector3 kameraEskiKonum;
   [SerializeField] float hareketOlcegi =0.2f;
    void Start()
    {
        kameraEskiKonum=Camera.main.transform.position;
    }

   
    void Update()
    {
        
    }
    //hep en son updatei getirir
    private void LateUpdate() {
        var kameraYeniKonum=Camera.main.transform.position;
        var hareketVectoru=kameraYeniKonum-kameraEskiKonum;
        transform.position+=hareketVectoru*hareketOlcegi;
        kameraEskiKonum=kameraYeniKonum;
    }
}
