using UnityEngine;

public class MoveByPosition : MonoBehaviour
{

    [SerializeField] private float velocitat = 0.01f;
    //[SerializeField] private Vector3 direccio;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * velocitat * Time.deltaTime);
    }
}
