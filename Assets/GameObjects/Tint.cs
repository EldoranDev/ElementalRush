using UnityEngine;
using System.Collections;

public class Tint : MonoBehaviour {

	public Color TintColor;

	// Use this for initialization
	void Start () {
		var childs = GetComponentsInChildren<Renderer> ();

		foreach (var child in childs)
		{
			foreach (var material in child.materials)
			{
				material.color += TintColor;
			}
		}

		enabled = false;
	}
}
