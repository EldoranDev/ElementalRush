using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuManger : MonoBehaviour {

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void StartGame()
    {
        SceneManager.LoadScene("GameWorld");
    }

    public void EndGame()
    {
        Application.Quit();
    }
}
