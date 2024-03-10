using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerX : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject powerupPrefab;

    private float spawnRangeX = 10;
    private float spawnZMin = 15; // set min spawn Z
    private float spawnZMax = 25; // set max spawn Z

    public int enemyCount;
    public int waveCount = 1;//dalga sayısı


    public GameObject player; 

    
    void Update()
    {
        //enemyCount = GameObject.FindGameObjectsWithTag("Powerup").Length;
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;

        if (enemyCount == 0)
        {
            SpawnEnemyWave(waveCount);
        }

    }

    // Generate random spawn position for powerups and enemy balls
    Vector3 GenerateSpawnPosition ()
    {
        float xPos = Random.Range(-spawnRangeX, spawnRangeX);
        float zPos = Random.Range(spawnZMin, spawnZMax);
        return new Vector3(xPos, 0, zPos);
    }


    void SpawnEnemyWave(int enemiesToSpawn)//düşman dalgası
    {
        Vector3 powerupSpawnOffset = new Vector3(0, 0, -15); // Güçlendirmelerin oyuncu tarafında ortaya çıkmasını sağlayın

        // Eğer hiç güçlendirme kalmamışsa,bir güçlendirme oluştur

        if (GameObject.FindGameObjectsWithTag("Powerup").Length == 0) // güç kaynağı 0 olup olmadığını kontrol et.
        {
            Instantiate(powerupPrefab, GenerateSpawnPosition() + powerupSpawnOffset, powerupPrefab.transform.rotation);
        }

        //Dalga sayısına bağlı olarak düşman toplarının ortaya çıkma sayısı
        //for (int i = 0; i < 2; i++)
        for(int i=0;i<enemiesToSpawn;i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
        }

        waveCount++;
        ResetPlayerPosition(); // oyuncu tekrar başlangıca koyulur

    }

    // Move player back to position in front of own goal
    void ResetPlayerPosition ()
    {
        player.transform.position = new Vector3(0, 1, -7);
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        player.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

    }

}
