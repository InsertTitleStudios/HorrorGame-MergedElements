using UnityEngine;
using System.Collections;

public class MenuScript : MonoBehaviour {

    public Transform mainMenu;
    public Transform optionsMenu;
    public Transform helpMenu;

    public void LoadScene(string name)
    {
        Application.LoadLevel(name);
    }



    public void QuitGame()
    {
        Application.Quit();
    }

    public void  HelpMenu(bool clicked)
    {
        if (clicked == true)
        {
            helpMenu.gameObject.SetActive(clicked);
            mainMenu.gameObject.SetActive(false);
            optionsMenu.gameObject.SetActive(false);
        }
        else
        {
            helpMenu.gameObject.SetActive(clicked);
            mainMenu.gameObject.SetActive(true);
        }
    }

    public void OptionMenu(bool clicked)
    {
        if (clicked == true)
        {
            optionsMenu.gameObject.SetActive(clicked);
            helpMenu.gameObject.SetActive(false);
            mainMenu.gameObject.SetActive(false);
        }
        else
        {
            helpMenu.gameObject.SetActive(clicked);
            mainMenu.gameObject.SetActive(false);
        }

    }


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
