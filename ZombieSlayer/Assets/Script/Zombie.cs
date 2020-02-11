using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{

    public GameObject effect;
    public Rigidbody2D rb;
    public Rigidbody2D player;
    public float speed= 5f;


    Vector2 movement;


    // Start is called before the first frame update
    void Start()
    {
        GameObject[] firstList = GameObject.FindObjectsOfType<GameObject>();
        List<Object> finalList = new List<Object>();
        string nameToLoogFor = "Player";
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

            Destroy(gameObject);
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
