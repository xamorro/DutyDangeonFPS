using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerDianes : MonoBehaviour
{
    [SerializeField] private GameObject dianaPrefab;
    [SerializeField] private float spawnTimer = 2.5f;
    [SerializeField] private float TempsViu = 6.5f;
    //[SerializeField] private float velocitat = 2f;


    private float timeCounter = 0;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        timeCounter += Time.deltaTime;


        if (timeCounter > spawnTimer)
        {
            SpawnDiana();
            timeCounter = 0;
        }


    }

    private void SpawnDiana()
    {
        GameObject diana = Instantiate(dianaPrefab, transform.position + transform.forward * 0 , Quaternion.Euler(new Vector3(0, 0, 0)));
        //diana.transform.Translate(Vector3.forward * velocitat * Time.deltaTime);
        Destroy(diana, TempsViu);
    }
}
