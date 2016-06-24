using UnityEngine;
using System.Collections;
using System.Security.Policy;

public class PlaceHolder : MonoBehaviour
{

    public GameObject Original;

    public bool CanBePlaced { 
		get {
			return IntersectTower || WorldManager.Instance.BuildArea (transform.position);
		}
	}

	private bool IntersectTower {get; set;}
	private Renderer[] _childs;
	private bool lastFramePlace = true;

    void Awake()
    {
        _childs = GetComponentsInChildren<Renderer>();
        
    }

	void Update() {
		if (lastFramePlace != CanBePlaced) {
			if (CanBePlaced) {
				UpdateColor(new Color(0.2f, 1f, 0.2f, 0.3f));
			} else {
				UpdateColor(new Color(1f, 0f, 0f, 0.3f));
			}
		}

		lastFramePlace = CanBePlaced;
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
			IntersectTower = false;
            //UpdateColor(new Color(1f, 0f, 0f, 0.3f));
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("BuildLayer"))
        {
			IntersectTower = true;
            //UpdateColor(new Color(0.2f, 1f, 0.2f, 0.3f));
        }
    }
}
