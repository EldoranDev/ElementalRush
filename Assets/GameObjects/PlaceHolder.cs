using UnityEngine;
using System.Collections;
using System.Security.Policy;

public class PlaceHolder : MonoBehaviour
{
    private Renderer[] _childs;

    public GameObject Original;

    public bool CanBePlaced { get; private set; }

    void Awake()
    {
        _childs = GetComponentsInChildren<Renderer>();
        
    }

    public void SetMaterial(Material m) {

        foreach (var child in _childs)
        {
            var mats = new Material[child.materials.Length];
            for (var i = 0; i < child.materials.Length; i++)
            {
                mats[i] = m;
            }

            child.materials = mats;
        }
    }

    void UpdateColor(Color clr)
    {
        foreach (var child in _childs)
        {
            foreach (var material in child.materials)
            {
                material.color = clr;
            }
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("BuildLayer"))
        {
            CanBePlaced = false;
            UpdateColor(new Color(1f, 0f, 0f, 0.3f));
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("BuildLayer"))
        {
            CanBePlaced = true;
            UpdateColor(new Color(0.2f, 1f, 0.2f, 0.3f));
        }
    }
}
