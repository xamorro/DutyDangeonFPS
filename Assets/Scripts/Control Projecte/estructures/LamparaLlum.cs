using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LamparaLlum : MonoBehaviour
{
    public bool isFlickering = false;
    public float timeDelay;
    public GameObject[] allChildren;

    private void Start()
    {
        allChildren = new GameObject[gameObject.transform.childCount];
    }

    void Update()
    {
        for (int i = 0; i < allChildren.Length; i++)
        {
            allChildren[i] = gameObject.transform.GetChild(i).gameObject;
        }

        if (isFlickering == false)
        {
            StartCoroutine(FlickeringLight());
        }


        //Corrutina de parpadetj llum
        IEnumerator FlickeringLight()
        {
            isFlickering = true;
            //Apagam sa llum dels fills del gameobject lampara
            foreach (GameObject lampara in allChildren) 
            {
                lampara.transform.GetChild(0).GetComponent<Light>().enabled = false;
                lampara.transform.GetChild(1).GetComponent<Light>().enabled = false;
            }
            timeDelay = Random.Range(0.01f, 0.2f);
            yield return new WaitForSeconds(timeDelay);

            //Encenem sa llum dels fills del gameobject lampara
            foreach (GameObject lampara in allChildren)
            {
                lampara.transform.GetChild(0).GetComponent<Light>().enabled = true;
                lampara.transform.GetChild(1).GetComponent<Light>().enabled = true;
            }
            timeDelay = Random.Range(0.01f, 1f);
            yield return new WaitForSeconds(timeDelay);
            isFlickering = false;
        }
    }
}



