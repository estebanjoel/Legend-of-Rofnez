using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;

public class CannonBomb : MonoBehaviour
{
    [SerializeField] GameObject explosionVFX;
    [SerializeField] GameObject[] enemiesToSpawn;
    [SerializeField] GameObject[] trapsToSpawn;
    [SerializeField] GameObject crystal;
    [SerializeField] int spawnPercentageRate;
    bool canSpawnCrystal;
    [Header("Audio Clips")]
    AudioManager audioManager;
    [SerializeField] AudioClip launchSound;
    [SerializeField] AudioClip impactSound;

    void Start()
    {
        audioManager = GameObject.FindObjectOfType<AudioManager>();
        audioManager.TryToPlayClip(audioManager.trapSources, launchSound);
    }

    public void SetIfICanSpawnCrystal(bool option)
    {
        canSpawnCrystal = option;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Ground") Explode();
    }

    private void Explode()
    {
        float explosionYPos = transform.position.y + 1.75f;
        Vector3 explosionPos = new Vector3(transform.position.x, explosionYPos, transform.position.z);
        audioManager.TryToPlayClip(audioManager.trapSources, impactSound);
        Instantiate(explosionVFX, explosionPos, transform.rotation);
        if(canSpawnCrystal) Instantiate(crystal, transform.position, transform.rotation);
        else if(CheckIfCanSpawn()) SpawnFromExplosion();
        Destroy(gameObject);
    }

    private void SpawnFromExplosion()
    {
        int objectToSpawn = Random.Range(0, 2);
        int selectedGameObjectIndex = -1;
        switch (objectToSpawn)
        {
            case 0:
                selectedGameObjectIndex = Random.Range(0, enemiesToSpawn.Length);
                Instantiate(enemiesToSpawn[selectedGameObjectIndex], transform.position, transform.rotation);
                break;
            case 1:
                selectedGameObjectIndex = Random.Range(0, trapsToSpawn.Length);
                Instantiate(trapsToSpawn[selectedGameObjectIndex], transform.position, transform.rotation);
                break;
        }
    }

    private bool CheckIfCanSpawn()
    {
        int percentageToSpawn = Random.Range(0, 101);
        if(percentageToSpawn <= spawnPercentageRate) return true;
        else return false;
    }

    public void SetSpawnPercentageRate(int percentageRate)
    {
        spawnPercentageRate = percentageRate;
    }
}
