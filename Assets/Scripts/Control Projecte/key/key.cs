using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class key : MonoBehaviour
{
    [SerializeField] private GameObject clau;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //AudioManager.I.PlaySound(SoundName.KeyFind, transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        clau.SetActive(true);
        Debug.Log("Claus Recollides");
        Destroy(gameObject);
    }
}
