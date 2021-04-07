using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OyunKontrol : MonoBehaviour
{
    public GameObject gokyuzu1;
    public GameObject gokyuzu2;
    public float arkaPlanHiz = -1.5f;

    Rigidbody2D fizikbir;
    Rigidbody2D fizikiki;

    float uzunluk = 0;

    public GameObject engel;
    public int kacAdetEngel = 5;
    GameObject[] engeller;

    float degisimZaman = 0;
    int sayac = 0;
    bool oyunBittiMi = true;
    void Start()
    {
        fizikbir = gokyuzu1.GetComponent<Rigidbody2D>();
        fizikiki = gokyuzu2.GetComponent<Rigidbody2D>();

        fizikbir.velocity = new Vector2(arkaPlanHiz, 0);
        fizikiki.velocity = new Vector2(arkaPlanHiz, 0);

        uzunluk = gokyuzu1.GetComponent<BoxCollider2D>().size.x;

        engeller = new GameObject[kacAdetEngel];
        for (int i = 0; i < engeller.Length; i++)
        {
            engeller[i] = Instantiate(engel, new Vector2(-20, -20), Quaternion.identity);
            Rigidbody2D fizikEngel = engeller[i].AddComponent<Rigidbody2D>();
            fizikEngel.gravityScale = 0;
            fizikEngel.velocity = new Vector2(arkaPlanHiz, 0);

        }
    }


    void Update()
    {
        if (oyunBittiMi)
        {

            if (gokyuzu1.transform.position.x <= -uzunluk)
            {
                gokyuzu1.transform.position += new Vector3(uzunluk * 2, 0);
            }
            if (gokyuzu2.transform.position.x <= -uzunluk)
            {
                gokyuzu2.transform.position += new Vector3(uzunluk * 2, 0);
            }
            //----------------------
            degisimZaman += Time.deltaTime;
            if (degisimZaman > 2f)
            {
                degisimZaman = 0;
                float Yeksenim = Random.Range(-0.50f, 1.10f);
                engeller[sayac].transform.position = new Vector3(18, Yeksenim);
                sayac++;
                if (sayac >= engeller.Length)
                {
                    sayac = 0;
                }
            }
        }
        else
        {

        }
    }

    public void oyunBitti()
    {
        for (int i = 0; i < engeller.Length; i++)
        {
            engeller[i].GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            fizikbir.velocity = Vector2.zero;
            fizikiki.velocity = Vector2.zero;
        }
        oyunBittiMi = false;
    }
}
