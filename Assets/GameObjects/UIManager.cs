using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

class UIManager : MonoBehaviour {

    public static UIManager Instance { get; private set; }
    
    public Text LifesCounter;
    public Text MoneyCounter;

    public Sprite[] IconMap;

    public GameObject ButtonPrefab;

    public SelectionManager SelectionHandler;
    public Tooltip Tooltip;
    public GameObject TowerSeletion;

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

        UpdateAvailableTower();
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

    public void UpdateAvailableTower()
    {
        for(var i = TowerSeletion.transform.childCount-1; i >= 0; i--)
        {
            Destroy(TowerSeletion.transform.GetChild(i).gameObject);
        }

        foreach(var tower in _world.AvailableTower)
        {
            var button = Instantiate(ButtonPrefab);
            var btn = button.GetComponent<Button>();
            var e = button.GetComponent<EventTrigger>();
            var t = tower;

            btn.GetComponent<Image>().sprite = t.Image;

            EventTrigger.Entry enter = new EventTrigger.Entry();
            enter.eventID = EventTriggerType.PointerEnter;
            enter.callback.AddListener((data) =>
            {
                DisplayPriceTooltip(tower.Cost, "Buy " + t.Name);
            });

            EventTrigger.Entry exit = new EventTrigger.Entry();
            exit.eventID = EventTriggerType.PointerExit;
            exit.callback.AddListener((data) =>
            {
                HideTooltip();
            });

            e.triggers.Add(enter);
            e.triggers.Add(exit);


            btn.onClick.AddListener(() =>
           {
               InputManager.Instance.TowerSelection(t.gameObject);
           });

            btn.transform.SetParent(TowerSeletion.transform);
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
        Tooltip.SetData(data);

        if (!Tooltip.gameObject.activeInHierarchy)
        {
            Tooltip.gameObject.SetActive(true);
        }

        Tooltip.UpdatePosition();
    }

    public void HideTooltip()
    {
        Tooltip.gameObject.SetActive(false);
    }
}
