using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class Spawn : MonoBehaviour
{
    public Transform prefab;
    public Transform newParent;
    Transform newCharacter;
    Vector3 initialPosition;
    public bool start;
    public static Spawn current;

    private void Awake()
    {
        current = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        start = false;
        initialPosition = new Vector3(0.379f, 0, 0);
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
        //newCharacter.localRotation = Quaternion.identity;
        //newCharacter.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        newCharacter.GetComponent<Animation>().Play();
        start = true;
    }

    public void DesactivatedCharacter()
    {
        newCharacter.GetComponent<Animation>().Stop();
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Wall")
        {
            newCharacter.localPosition = initialPosition;
            //newCharacter.GetComponent<Animation>().Stop();
            //start = false;
        }
    }
}
