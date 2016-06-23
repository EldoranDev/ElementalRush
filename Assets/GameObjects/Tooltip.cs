using UnityEngine;
using UnityEngine.UI;

class Tooltip : MonoBehaviour
{
    public Image Icon;
    public Text Title;
    public Text Description;

    void Update()
    {
        transform.position = Input.mousePosition + new Vector3(5, 5, 0);
    }

    public void SetData(TooltipData data)
    {
        if(data.Icon != null)
        {
            Icon.sprite = data.Icon;
        }else
        {
            Icon.enabled = false;
        }

        Title.text = data.Title;
        Description.text = data.Comment;
    }
}
