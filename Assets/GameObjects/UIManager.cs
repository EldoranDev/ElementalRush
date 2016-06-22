using UnityEngine;
using UnityEngine.UI;
using System.Collections;

class UIManager : MonoBehaviour {
    public Text LifesCounter;
    public Text MoneyCounter;

    private WorldManager _world;
    private Collider _collider;

	// Use this for initialization
	void Start () {
        _world = GetComponent<WorldManager>();

        _world.LifesChanged += OnLifesChanged;
        _world.MoneyChanged += OnMoneyChanged;

	    OnLifesChanged(_world.Lifes);
	    OnMoneyChanged(_world.Money);

	    _collider = GetComponent<Collider>();
	}
	
    void Destroy()
    {
        _world.LifesChanged -= OnLifesChanged;
        _world.MoneyChanged -= OnMoneyChanged;
    }

    void OnLifesChanged(int value)
    {
        LifesCounter.text = value.ToString();
    }

    void OnMoneyChanged(int value)
    {
        MoneyCounter.text = value.ToString();
    }

	// Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision coll)
    {
        Debug.Log(coll);
    }
}
