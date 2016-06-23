using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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

        for (var i = UpgradePanel.transform.childCount-1;i >= 0;i--)
        {
            Destroy(UpgradePanel.transform.GetChild(i).gameObject);
        }

        foreach (var upgrade in t.Upgrades)
        {
            var button = (GameObject) Instantiate(UpgadePrefab);

            button.GetComponent<Image>().sprite = upgrade.Image;

            var btn = button.GetComponent<Button>();
            var ut = upgrade;

            btn.onClick.AddListener(() =>
            {
                if (WorldManager.Instance.Money >= ut.Cost)
                {
                    t.Upgrade(ut);
                    WorldManager.Instance.Money -= t.Cost;
                }
            });

            button.transform.SetParent(UpgradePanel.transform);
        }
    }
}
