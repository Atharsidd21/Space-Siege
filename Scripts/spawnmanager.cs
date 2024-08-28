using System.Security.Cryptography;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnmanager : MonoBehaviour
{
    [SerializeField]
   private GameObject enemyPrefab;
   [SerializeField]
   private GameObject enemyContainer;
  
   [SerializeField]
   private GameObject [] PowerUps;
   private bool stopSpawn = false;
    // Start is called before the first frame update
    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerUpRoutine());

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //spawn objects every 5 seconds
    //use coroutine with IEnumerator -- Yield Events 
    //while loop 
    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(3.0f);

        while (stopSpawn == false)
        {
            Vector3 postToSpawn = new Vector3(Random.Range(-8f,8f),7,0);
          GameObject newEnemy = Instantiate(enemyPrefab , postToSpawn,Quaternion.identity);
          newEnemy.transform.parent = enemyContainer.transform;
          yield return new WaitForSeconds(4.0f);
        }
        

    }
    IEnumerator SpawnPowerUpRoutine()
    {
          yield return new WaitForSeconds(3.0f);
        while (stopSpawn == false)
        {
            Vector3 posToSpawn =new Vector3(Random.Range(-8f,8f),7,0);
            int RandomPowerUp = Random.Range(0, 3);
            Instantiate(PowerUps[RandomPowerUp],posToSpawn,Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3,8));
        }
    }
    public void PlayerDeath()
    {
        stopSpawn = true;
    }

}
