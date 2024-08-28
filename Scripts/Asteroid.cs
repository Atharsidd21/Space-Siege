using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private  float speed = 4f;
    [SerializeField]
    private GameObject ExplosionPrefab;
    private spawnmanager _spawnmanager;
    // Start is called before the first frame update
    void Start()
    {
       _spawnmanager = GameObject.Find("Spawn Manager ").GetComponent<spawnmanager>();
    }

    // Update is called once per frame
    void Update()
    {
         transform.Rotate(Vector3.forward*speed*Time.deltaTime);
       
          
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Laser")
        {
            Instantiate(ExplosionPrefab,transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            _spawnmanager.StartSpawning();
            Destroy(this.gameObject,0.25f);
        }
    }
}
