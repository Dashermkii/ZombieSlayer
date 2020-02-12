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
    public GameObject muzzleSparks;
    public int Health = 1000;
    public int rounds = 6;
    public float fullReloadTime = 2;
    public GameObject loseScreen;
    public GameObject effectDead;
    public GameObject effect;

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
        GameObject bulletSparks = Instantiate(muzzleSparks, muzzle.transform.position, muzzle.transform.rotation);
        Destroy(bulletSparks, 2f);
        bulletRB.AddForce(muzzle.transform.up * bulletForce, ForceMode2D.Impulse);
        flash.intensity = flashBrightness;
        rounds -= 1;
        Debug.Log("rounds " + rounds);
        yield return new WaitForSeconds(flashTime);
        flash.intensity = 0;
        yield return new WaitForSeconds(reloadTime - flashTime);
        loaded = true;
        
        if (rounds <= 0)
        {
            Debug.Log("Empty");
            loaded = false;
        }
        

    }
    IEnumerator reloadGun()
    {
        speed = speed / 2;
        rounds = 6;
        Debug.Log("reloading");
        yield return new WaitForSeconds(fullReloadTime);
        Debug.Log("Rounds: " + rounds);
        loaded = true;
        speed = speed * 2;
        
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && loaded)
        {
            loaded = false;
            StartCoroutine(Shoot());      
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            loaded = false;
            StartCoroutine(reloadGun());
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Zombie")
        {
            GameObject bloodsplatter = Instantiate(effect, transform.position, Quaternion.identity);
            Destroy(bloodsplatter, 20f);
            Health -= collision.gameObject.GetComponent<Zombie>().health;
            Destroy(collision.gameObject);
        }
        if (Health < 0)
        {
            GameObject bloodsplatterDead = Instantiate(effectDead, transform.position, Quaternion.identity);
            Destroy(bloodsplatterDead, 20f);
            Debug.Log("You Have Died");
            loseScreen.SetActive(true);
            Destroy(gameObject);
        }
    }

    private void reload()
    {

    }
}
