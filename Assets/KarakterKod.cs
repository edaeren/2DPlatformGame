using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class KarakterKod : MonoBehaviour
{
    
    Rigidbody2D _rb;
    Animator _animator;
    SpriteRenderer _spriteRenderer;
    bool _zemindeMi=false;
    BoxCollider2D _boxCollider2D;
    [Header("Karakter Bilgileri")]
    [SerializeField] LayerMask _layerMask;

    [Header("Animasyon Bilgileri")]
    [SerializeField] GameObject _esyaYendiSablon;
    void Start()
    {
        _rb=GetComponent<Rigidbody2D>();
        _animator=GetComponent<Animator>();
        _spriteRenderer=GetComponent<SpriteRenderer>();
        _boxCollider2D=GetComponent<BoxCollider2D>();
    }

    void HareketKontrol(){
        float x= Input.GetAxis("Horizontal");
        //yercekimi ivmesine atanan degeri surekli sifirladigimiz icin yavas oluyor. bu yuzden yeni velocity tanimladik
        //yani ysinin ayni kalmsini sagladik.
        var velocity=_rb.velocity;
        velocity.x=x*15;
        _rb.velocity=velocity;

        bool KosuyorMu=false;
        if(x!=0){ //x 0dan farkli ise kosuyordur
           KosuyorMu=true;
        }
        if(x<0){ //geri geri gidiyor ise 
            _spriteRenderer.flipX=true;
        }else{
            _spriteRenderer.flipX=false;
        }
         _animator.SetBool("KosuyorMu",KosuyorMu);

         if(Zemindemi()==true){
            if(Input.GetKeyDown(KeyCode.Space)){
                _rb.AddForce(Vector2.up*10,ForceMode2D.Impulse);
                _animator.SetBool("SicriyorMu",true);
                _animator.SetBool("DusuyorMu",false);
                _animator.SetBool("ZemindeMi",false);
                _zemindeMi=false;
            }
            else{ //ziplamiyor
                 _animator.SetBool("SicriyorMu",false);
                _animator.SetBool("DusuyorMu",false);
                _animator.SetBool("ZemindeMi",true);
            }   
         }
        else{ //zeminde degil
            if(DusuyorMu()){
            _animator.SetBool("SicriyorMu",false);
            _animator.SetBool("DusuyorMu",true);
            _animator.SetBool("ZemindeMi",false);
           
         }
        }
         
    }
    bool DusuyorMu(){
        if(_rb.velocity.y<0)
            return true;
        else
            return false;
    }

    //snde 50 defa yapilir
    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.CompareTag("Zemin")){
            _zemindeMi=true;
            _animator.SetBool("ZemindeMi",true);
            _animator.SetBool("DusuyorMu",false);
        }
    }


/*
    private void OnDrawGizmos() {
        if(_zemindeMi){
            var collider=GetComponent<BoxCollider2D>();
            Gizmos.DrawCube(collider.bounds.center,collider.bounds.size);
       }
    }*/
    bool Zemindemi(){
        float tasma=0.1f;
        var carpismalar= Physics2D.BoxCast(
            _boxCollider2D.bounds.center,
            _boxCollider2D.bounds.size,
            0.0f,
            Vector2.down,
            _boxCollider2D.bounds.extents.y+tasma,_layerMask );
        bool zemindemi=false;
        if(carpismalar.collider!=null){


            _zemindeMi=true;
            zemindemi=true;
        }
        return zemindemi;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.CompareTag("Kiraz")){
            Destroy(collision.gameObject);
            var yendiEfekti=Instantiate(_esyaYendiSablon);
            yendiEfekti.transform.position=collision.gameObject.transform.position;
        }
    }

    bool ZemindeMiRayCast(){
        float tasma=0.1f;
        var carpismalar= Physics2D.Raycast(_boxCollider2D.bounds.center,
                                            Vector2.down,
                                            _boxCollider2D.bounds.extents.y+tasma,_layerMask
                                            );
        bool zemindemi=false;
         //birden fazla collider var demek                                   
        if(carpismalar.collider!=null){
            Debug.DrawRay(_boxCollider2D.bounds.center, //baslangic
                        new Vector3(0.0f,-_boxCollider2D.bounds.extents.y,0.0f+tasma) //bitis
                        );
            zemindemi=true;
           // _zemindeMi=true;
        }
        return zemindemi;

    }
    
    void Update()
    {
        HareketKontrol();
    }
}
