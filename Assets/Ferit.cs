using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ferit : MonoBehaviour
{

    [SerializeField] private GameObject Canvas;
    private bool canvashit = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CanvasFerit()
    {
        if (!canvashit)
        {
            StartCoroutine(DañoRecibido());
        }
        
    }

    private IEnumerator DañoRecibido()
    {
        canvashit = true;
        Canvas.SetActive(true);
        yield return new WaitForSeconds(.1f);
        Canvas.SetActive(false);
        canvashit = false;
    }
}
