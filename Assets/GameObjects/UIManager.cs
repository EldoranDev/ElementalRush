using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

class UIManager : MonoBehaviour {

    public static UIManager Instance { get; private set; }
    
    public Text LifesCounter;
    public Text MoneyCounter;

    public Sprite[] IconMap;

    public SelectionManager SelectionHandler;
    public Tooltip Tooltip;

    private WorldManager _world;

    void Awake()
    {
        Instance = this;
    }

	// Use this for initialization
	void Start () {
        _world = GetComponent<WorldManager>();

        _world.LifesChanged += OnLifesChanged;
        _world.MoneyChanged += OnMoneyChanged;

	    OnLifesChanged(_world.Lifes);
	    OnMoneyChanged(_world.Money);
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

    public void DisplaySelection(Tower t)
    {
        if (!ReferenceEquals(t, null))
        {
            SelectionHandler.gameObject.SetActive(true);
            SelectionHandler.UpdateTowerDetails(t);
        }
        else
        {
            SelectionHandler.gameObject.SetActive(false);
        }
    }

    public void DisplayPriceTooltip(int price, string text)
    {
        var data = new TooltipData
        {
            Icon = IconMap[0],
            Title = price.ToString(),
            Comment = text
        };

        DisplayTooltip(data);
    }

    public void DisplayTooltip(TooltipData data)
    {
        if(!Tooltip.gameObject.activeInHierarchy)
        {
            Tooltip.gameObject.SetActive(true);
        }

        Tooltip.SetData(data);
    }

    public void HideTooltip()
    {
        Tooltip.gameObject.SetActive(false);
    }
}
