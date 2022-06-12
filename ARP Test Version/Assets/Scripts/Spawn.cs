using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class Spawn : MonoBehaviour
{
    public Transform prefab;
    public Transform newParent;
    Transform newCharacter;
    public bool start;
    public static Spawn current;

    private void Awake()
    {
        current = this;
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(start == true)
        {
            //Instantiate(prefab, transform.position, transform.rotation);
            newCharacter.Translate(new Vector3(0, 0, 0.5f) * Time.deltaTime);
        }

    }

    public void StartGenerator(float np)
    {
        newCharacter = Instantiate(prefab, transform.position, transform.rotation);
        newCharacter.SetParent(newParent);
        newCharacter.localPosition = new Vector3(0.379f+np, 0, np);
        newCharacter.GetComponent<Animation>().Play();
        start = true;
        //gameObject.GetComponent<Rigidbody>().useGravity = true;
        //Instantiate(prefab, new Vector3(0.4f, 0, 0), transform.rotation);
    }

    public void DesactivatedCharacter()
    {
        Destroy(newCharacter);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Wall")
        {
            //Destroy(newCharacter);
            newCharacter.GetComponent<Animation>().Stop();
        }
    }
}
