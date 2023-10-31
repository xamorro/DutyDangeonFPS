using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankSpawner : MonoBehaviour
{
    [SerializeField] private GameObject tankPrefab;
    [SerializeField] private float spawnTimer = 2.5f;

    private float timeCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        timeCounter += Time.deltaTime;

        if(timeCounter > spawnTimer)
        {
            SpawnTank();
            timeCounter = 0;
        }

    }

    private void SpawnTank()
    {
        GameObject tanke = Instantiate(tankPrefab, transform.position, transform.rotation);
        Destroy(tanke, 10);
    }
}
