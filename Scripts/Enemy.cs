using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float speed = 4f;
    [SerializeField]
    private GameObject _EnemyLaserPrefab;
    private Player player;
    private Animator Anim;
    private AudioSource audio_source1;
    private float _fireRate = 3.0f;
    private float _canFire = -1.0f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player ").GetComponent<Player>(); //make sure to have a space after name Player
        audio_source1 = GetComponent<AudioSource>(); 
        if (player == null)
        {
            Debug.LogError("player is Null");
        }
        Anim = GetComponent<Animator>();
        if (Anim == null)
        {
            Debug.LogError("Animator is Null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        if(Time.time > _canFire)
        {
            _fireRate = Random.Range(3f,7f);
            _canFire = Time.time + _fireRate;
           GameObject enemyLaser = Instantiate(_EnemyLaserPrefab,transform.position, Quaternion.identity);
            laser[] lasers = enemyLaser.GetComponentsInChildren<laser>();
            
            for (int i = 0;   i < lasers.Length; i++)
             {
                lasers[i].AssignEnemyLaser();
            }
            

        }
        
    }
    private void CalculateMovement()
    {
         //movedown 
        transform.Translate(Vector3.down *speed *Time.deltaTime);
        //respwan at top
        if(transform.position.y < -5f)
        {
            float randomX =Random.Range(-8f,8f);
          transform.position = new Vector3(randomX,7,0);
        }
      
    }
    private  void OnTriggerEnter2D(Collider2D other)
     {
        //if hits player
         if (other.tag == "Player")
         {
            Player player = other.transform.GetComponent<Player>();
         if(player!=null)
         {
             player.Damage();
         }
         Anim.SetTrigger("OnEnemyDeath");
         speed=0;
         //trigger animation 
            audio_source1.Play();
            Destroy (this.GetComponent<Collider2D>());
            Destroy(this.gameObject,2.35f);
             //audio_source1.Stop();
         }
        //if hits laser 
        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            //add score 
            if (player != null)
            {
                player.AddScore(10);
            }
            //trigger animation 
             Anim.SetTrigger("OnEnemyDeath");
             speed = 0;
              audio_source1.Play(); 
              Destroy (this.GetComponent<Collider2D>());
              Destroy(GetComponent<Enemy>());


            Destroy(this.gameObject,2.35f);
            //audio_source1.Stop();
        }
     }

}
