using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : MonoBehaviour
{


    public float speed;
    public Rigidbody2D body;
    public Camera cam;
    public GameObject bulletPF;
    public GameObject muzzle;
    public float reloadTime;
    public Light flash;
    public float flashTime = 0.05f;
    public float flashBrightness = 5f;
    public AudioSource bang;

    public float bulletForce = 100f;

    public bool loaded = true;

    Vector2 movement;
    Vector2 mousePos;
    // Update is called once per frame


     IEnumerator Shoot()
    {
        bang.Play();
        GameObject bullet = Instantiate(bulletPF, muzzle.transform.position, muzzle.transform.rotation);
        Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();
        bulletRB.AddForce(muzzle.transform.up * bulletForce, ForceMode2D.Impulse);
        flash.intensity = flashBrightness;
        yield return new WaitForSeconds(flashTime);
        flash.intensity = 0;
        yield return new WaitForSeconds(reloadTime - flashTime);

        loaded = true;

    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && loaded)
        {
            loaded = false;
            StartCoroutine(Shoot());
            
            
            
        }

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    private void FixedUpdate()
    {
        body.MovePosition(body.position + movement * speed * Time.fixedDeltaTime);
        

        Vector2 lookAt = mousePos - body.position;
        float angle = Mathf.Atan2(lookAt.y, lookAt.x) * Mathf.Rad2Deg -90f;
        
        body.rotation = angle;
    }



    private void reload()
    {

    }
}
