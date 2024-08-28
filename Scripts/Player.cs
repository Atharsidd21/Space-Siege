using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    //using speed variable 
    [SerializeField]
    private  float speed = 3.5f;
    private float speed_multiplier =2;
    [SerializeField]
    private GameObject LaserPrefab; 
    [SerializeField]
    private GameObject TripleShotPrefab;
    [SerializeField]
    private float fireRate = 0.5f;
    private float canFire = -1f;
    [SerializeField]
    private int life = 3;
    private spawnmanager  SpawnManager;
    private bool IsTripleShotActive = false;
    private bool IsSpeedBoostActive = false;
    private bool IsShieldActive = false;
    [SerializeField]
    private GameObject ShieldVisuals;
    [SerializeField]
    private GameObject RightEngine,LeftEngine;
   
    [SerializeField]
    private int Score;
    private uimanager UiManager;
    [SerializeField]
    private AudioClip laserSound;
    [SerializeField]
    private AudioSource Audio_source;

 
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0,0);
        
        SpawnManager = GameObject.Find("Spawn Manager ").GetComponent<spawnmanager>();
        UiManager = GameObject.Find("Canvas").GetComponent<uimanager>();
        Audio_source = GetComponent<AudioSource>();
        if (SpawnManager == null)
        {
            Debug.LogError("spawn is null");
        }
        if (UiManager == null)
        {
            Debug.LogError("ui is null");
        }
        if (Audio_source == null)
        {
            Debug.LogError("AudioScore is Null!!");
        }
        else
        {
            Audio_source.clip = laserSound;
        }
    }
    // Update is called once per frame
    void Update()
    {   
        CalculateMovement();

    if(Input.GetKeyDown(KeyCode.Space) && Time.time > canFire)
    {
        Shootlaser();
    }      
    }
        void CalculateMovement()
        {
           //horizontal variable
        float horizontalInput = Input.GetAxis("Horizontal");
        //vertical variable
        float verticalInput = Input.GetAxis("Vertical");    
        //common variable for both
        Vector3 direction = new Vector3(horizontalInput,verticalInput,0);   
         transform.Translate(direction *speed *Time.deltaTime);

        //player bounds
        //y-axis
       if (transform.position.y >=5)
       {
          transform.position = new Vector3(transform.position.x,5,0);
       }
       else if(transform.position.y<=-3.8f)
       {
          transform.position = new Vector3(transform.position.x,-3.8f,0);
       }
       //x-axis
       if (transform.position.x >9)
       {
        transform.position = new Vector3(-9,transform.position.y,0);   
       }
       else if (transform.position.x <-9)
       {
        transform.position = new Vector3(9, transform.position.y,0);
       }

        }
         void Shootlaser()
        {
             canFire= Time.time + fireRate;


             if (IsTripleShotActive  == true)
             {
                 Instantiate(TripleShotPrefab,transform.position,Quaternion.identity);
             }
             else
             {
              Instantiate(LaserPrefab, transform.position + new Vector3(0,1.075f,0), Quaternion.identity);  
             }
             //player audio
             Audio_source.Play();

              


        }
        public void Damage()
        {
            if(IsShieldActive == true)
            {
                IsShieldActive  = false;
                ShieldVisuals.SetActive(false);    
                return;
            }

   
            life --;
            if(life == 2)
            {
                RightEngine.SetActive(true);

            }
            else if (life == 1)
            {
                LeftEngine.SetActive(true);
            }
            UiManager.UpdateLives(life);
            //check death
            if (life < 1)
            {
                SpawnManager.PlayerDeath();
               
              Destroy(this.gameObject);
               
            }
        }
        public void TripleShotActive()
        {
            IsTripleShotActive = true;   
            StartCoroutine(TripleShotPowerDownRoutine());
        }
        IEnumerator TripleShotPowerDownRoutine()
        {
            yield return new WaitForSeconds(5.0f);
            IsTripleShotActive = false;
        }
        public void SpeedBoostIsActive()
        {
            IsSpeedBoostActive = true;
            speed*= speed_multiplier;
            StartCoroutine(SpeedBoostPowerDownRoutine());
        }
        IEnumerator SpeedBoostPowerDownRoutine()
        {
            yield return new  WaitForSeconds (5.0f);
            IsSpeedBoostActive = false;
            speed /= speed_multiplier;
        }
        public void ShieldIsActive ()
        {
            IsShieldActive = true;
            ShieldVisuals.SetActive(true);
        }
        //update score 
        public void AddScore(int points)
        {
             Score += points;
             UiManager.UpdateScore(Score);
        } 
}
