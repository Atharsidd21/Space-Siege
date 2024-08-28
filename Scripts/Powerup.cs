using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;


public class Powerup : MonoBehaviour
{
  [SerializeField]   
  private float speed = 3.0f;

  //ID for Power upss
   //0 = Triple shots 
   //1 = Speed
   //2 = Shields
   [SerializeField] // 0 = Triple shots //1 = Speed //2 = Shields 
   private int powerUpID;
   [SerializeField]
     private AudioClip Clip;
     
     

    // Update is called once per frame
    void Update()
    {
        // move down at the speed of 3
          transform.Translate(Vector3.down *speed *Time.deltaTime);
          if(transform.position.y < -4.5f)
        {
           Destroy(this.gameObject);
        }
      
    }
     private void OnTriggerEnter2D(Collider2D other)
    {
       if(other.tag == "Player")
       {
        Player player = other.transform.GetComponent<Player>();
       AudioSource.PlayClipAtPoint(Clip,transform.position);
        if(player != null)
        {   
           //power upIDs
           switch(powerUpID)
           {
            case 0:
            player.TripleShotActive();
            break;
            case 1:
            player.SpeedBoostIsActive();
            break;

            case 2:
            player.ShieldIsActive();
            break;
            default:
            break;

           }
            
            
            
        }
        Destroy(this.gameObject);
       }
    }
}
