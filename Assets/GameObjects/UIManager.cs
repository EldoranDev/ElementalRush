using UnityEngine;
using UnityEngine.UI;
using System.Collections;

class UIManager : MonoBehaviour {
    public Text LifesCounter;
    public Text MoneyCounter;

    private WorldManager _world; 

	// Use this for initialization
	void Start () {
        _world = GetComponent<WorldManager>();

        _world.LifesChanged += OnLifeChange;
        _world.MoneyChanged += OnMoneyChanged;
	}
	
    void Destroy()
    {
        _world.LifesChanged -= OnLifeChange;
        _world.MoneyChanged -= OnMoneyChanged;
    }

    void OnLifeChange(int value)
    {
        LifesCounter.text = value.ToString();
    }

    void OnMoneyChanged(int value)
    {
        MoneyCounter.text = value.ToString();
    }

	// Update is called once per frame
	void Update () {
	
	}
}
