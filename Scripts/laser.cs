using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laser : MonoBehaviour
{
    //speed variable 
  [SerializeField]
   private float shoot = 8.0f;
 
   private bool isEnemyLaser =false;
    // Update is called once per frame
    void Update()
    {
      if (isEnemyLaser == false)
      {
        MoveUp();
      }
      else
      {
        MoveDown();
      }
    }
    void MoveUp()
    {
        //laser goes up 
        transform.Translate(Vector3.up *shoot*Time.deltaTime);
        //destroy object 
        if(transform.position.y > 7.0f)
        {
         Destroy(this.gameObject);
          
        }
    }
    void MoveDown()
    {
        //laser goes up 
        transform.Translate(Vector3.down *shoot*Time.deltaTime);
        //destroy object 
        if(transform.position.y < -7.0f)
        {
         Destroy(this.gameObject);
          
        }
    }
    public void AssignEnemyLaser()
    {
        isEnemyLaser = true;    
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player"&&isEnemyLaser == true)
        {
          Player player = other.GetComponent<Player>();
          if (player != null)
          {
            player.Damage();
          }
        }
    }
}



