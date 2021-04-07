using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Kontrol : MonoBehaviour
{
    public Sprite [] KusSrpite;
    SpriteRenderer spriteRenderer;
    bool ileriGeriKontrol = true;
    int kusSayac = 0;
    float kusAnimasyonZaman = 0;
    Rigidbody2D fizik;
    int puan = 0;
    bool oyunBitti = true;

    public Text puanText;

    OyunKontrol oyunKontrol;

    AudioSource []sesler;


    
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        fizik = GetComponent<Rigidbody2D>();
        oyunKontrol = GameObject.FindGameObjectWithTag("OyunKontrol").GetComponent<OyunKontrol>();
        sesler = GetComponents<AudioSource>();
    }

    
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && oyunBitti)
        {
            fizik.velocity = new Vector2(0, 0);
            fizik.AddForce(new Vector2(0,200));
            sesler[0].Play();
           
        }
        if (fizik.velocity.y > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 45);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, -45);
        }
        Animasyon();

        
    }
    void Animasyon()
    {
        kusAnimasyonZaman += Time.deltaTime;
        if (kusAnimasyonZaman > 0.2f)
        {
            kusAnimasyonZaman = 0;
            if (ileriGeriKontrol)
            {
                spriteRenderer.sprite = KusSrpite[kusSayac];
                kusSayac++;
                if (kusSayac == KusSrpite.Length)
                {
                    kusSayac--;
                    ileriGeriKontrol = false;
                }
            }
            else
            {
                kusSayac--;
                spriteRenderer.sprite = KusSrpite[kusSayac];
                if (kusSayac == 0)
                {
                    kusSayac++;
                    ileriGeriKontrol = true;
                }
            }

        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "puan")
        {
            puan++;
            puanText.text = "Puan = " + puan;
            sesler[1].Play();
        }
        if (collision.gameObject.tag == "engel")
        {
            oyunBitti = false;
            oyunKontrol.oyunBitti();
            sesler[2].Play();
            GetComponent<CircleCollider2D>().enabled = false;
        }
    }
}
