using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SelectionManager : MonoBehaviour
{

    public Image TowerAvatar;
    public Text TowerName;
    public Text TowerDamage;
    public Text TowerRange;
    public Text ExplosionRadius;

    public GameObject UpgadePrefab;
    public Image UpgradePanel;

    void Start()
    {
        gameObject.SetActive(false);
    }

    public void UpdateTowerDetails(Tower t)
    {
        TowerAvatar.sprite = t.Image;
        TowerName.text = t.Name;
        TowerDamage.text = t.Damage.ToString();
        TowerRange.text = t.Range.ToString();
        ExplosionRadius.text = t.ProjectileExplosionRadius.ToString();

        for (var i = UpgradePanel.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(UpgradePanel.transform.GetChild(i).gameObject);
        }

        foreach (var upgrade in t.Upgrades)
        {
            var button = (GameObject)Instantiate(UpgadePrefab);

            button.GetComponent<Image>().sprite = upgrade.Image;

            var btn = button.GetComponent<Button>();
            var e = button.GetComponent<EventTrigger>();
            var ut = upgrade;
            

            EventTrigger.Entry enter = new EventTrigger.Entry();
            enter.eventID = EventTriggerType.PointerEnter;
            enter.callback.AddListener((data) =>
            {
                UIManager.Instance.DisplayPriceTooltip(ut.Cost, "Upgrade to " + ut.Name);
            });

            EventTrigger.Entry exit = new EventTrigger.Entry();
            exit.eventID = EventTriggerType.PointerExit;
            exit.callback.AddListener((data) =>
            {
                UIManager.Instance.HideTooltip();
            });

            e.triggers.Add(enter);
            e.triggers.Add(exit);

            btn.onClick.AddListener(() =>
            {
                if (WorldManager.Instance.Money >= ut.Cost)
                {
                    t.Upgrade(ut);
                    WorldManager.Instance.Money -= ut.Cost;
                    UIManager.Instance.HideTooltip();
                }
            });

            button.transform.SetParent(UpgradePanel.transform);
        }
    }
};
