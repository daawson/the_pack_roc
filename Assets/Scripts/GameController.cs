using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
	// Start is called before the first frame update
	public static GameController instance;
	public Text infoObject;
	public Image controls;
	public PlayerController playerController;

	// VARS
	public int cigsFound = 0;
    void Start()
    {
		instance = this;
    }

    // Update is called once per frame
    void Update()
    {
		//infoObject.text = "Health: " + playerController.health + "\nCig's Found: " + cigsFound;
		infoObject.text = "Cig's Found: " + cigsFound;

		if(Input.GetKeyDown(KeyCode.Tab))
		{
			controls.gameObject.SetActive(!controls.gameObject.active);
		}
		else if (Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
		}
	}
}