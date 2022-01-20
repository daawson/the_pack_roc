using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cigarette : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		transform.Rotate(Vector3.up, 2f);
    }

    private void OnDestroy()
    {
        GameObject.Find("Player").GetComponent<AudioSource>().PlayOneShot(GameObject.Find("Player").GetComponent<PlayerController>().audioClips[0]);        
    }
}
