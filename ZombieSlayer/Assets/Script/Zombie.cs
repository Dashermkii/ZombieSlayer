using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Zombie : MonoBehaviour
{

    public GameObject effectDead;
    public GameObject effect;
    public Rigidbody2D rb;
    public Rigidbody2D player;
    public float speed = 5f;
    public Text scoreCounter;
    public float score =5;
   



    public int health = 100;
    public int armor = 3;


    Vector2 movement;


    // Start is called before the first frame update
    void Start()
    {
        GameObject[] firstList = GameObject.FindObjectsOfType<GameObject>();
        List<Object> finalList = new List<Object>();
        string nameToLoogFor = "SoldierRifle";
        for(var i = 0; i < firstList.Length; i++)
        {
            if (firstList[i].gameObject.name == nameToLoogFor)
            {
                player = firstList[i].GetComponent<Rigidbody2D>();
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            GameObject bloodsplatter = Instantiate(effect, transform.position, Quaternion.identity);
            Destroy(bloodsplatter, 20f);
            if (collision.gameObject.GetComponent<Bullet>().damage - armor > 1)
            {
                health -= (collision.gameObject.GetComponent<Bullet>().damage - armor);
            }
            else
            {
                health -= 1;
            }
            
            if (health <= 0)
            {
                GameObject bloodsplatterDead = Instantiate(effectDead, transform.position, Quaternion.identity);
                SendMessageUpwards("incScore");
                Destroy(bloodsplatterDead, 20f);
                Destroy(gameObject);

            }
            
        }
        
    }

    private void FixedUpdate()
    {
        rb.AddForce(rb.transform.up * speed * Time.fixedDeltaTime);


        Vector2 lookAt = player.position - rb.position;
        float angle = Mathf.Atan2(lookAt.y, lookAt.x) * Mathf.Rad2Deg - 90f;

        rb.rotation = angle;
    }
}
