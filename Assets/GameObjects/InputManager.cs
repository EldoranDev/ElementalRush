using System;
using UnityEngine;
using System.Collections;
using System.Security.Policy;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    public Material PlaceholderMaterial;
    public float ScrollSpeed;

    private GameObject _courserAddition;
    private Vector3 _lastMousePosition;

	// Update is called once per frame
	void Update () {
	    if (!EventSystem.current.IsPointerOverGameObject())
	    {
	        UpdateCourser();
	        UpdateClicks();
            UpdateCamera();
	    }
	}

    void UpdateClicks()
    {
        if (Input.GetMouseButtonUp(1))
        {
            if (_courserAddition != null)
            {


                Destroy(_courserAddition.gameObject);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (_courserAddition != null)
            {
                
                var placeholder = _courserAddition.GetComponent<PlaceHolder>();
                var tower = placeholder.Original.GetComponent<Tower>();

                if (WorldManager.Instance.Money >= tower.Cost)
                {

                    WorldManager.Instance.Money -= tower.Cost;

                    var location = _courserAddition.transform.position;

                    Instantiate(placeholder.Original, location, Quaternion.identity);
                    Destroy(_courserAddition);
                }
            }
        }
    }

    void UpdateCourser()
    {

        if (_courserAddition != null)
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if (Physics.Raycast(ray.origin, ray.direction, out hit, LayerMask.GetMask("Mouse")))
            {
                _courserAddition.SetActive(true);
                _courserAddition.transform.position = hit.point;
            }
            else
            {
                _courserAddition.SetActive(false);
            }
        }
    }

    void UpdateCamera()
    {
        if(Input.GetMouseButton(2))
        {
            var delta = _lastMousePosition - Input.mousePosition;
            var pos = Camera.main.transform;

            pos.Translate(new Vector3(delta.x, 0, delta.y).normalized * ScrollSpeed * Time.deltaTime * -1, Space.World);
            pos.position = new Vector3(Mathf.Clamp(pos.position.x, -10, 10), pos.position.y, Mathf.Clamp(pos.position.z, -10, 10));

            //Camera.main.transform.position = pos.transform.position;

            _lastMousePosition = Input.mousePosition;
        }
    }

    public void TowerSelection(GameObject tower)
    {
        if (_courserAddition != null)
        {
            Destroy(_courserAddition.gameObject);
        }

        var addon = (GameObject)Instantiate(tower, Vector3.up, Quaternion.identity);

        Destroy(addon.GetComponent<Tower>());
        Destroy(addon.GetComponent<Animator>());

        var placeholder = addon.AddComponent<PlaceHolder>();
        placeholder.Original = tower;
        placeholder.SetMaterial(PlaceholderMaterial);

        addon.layer = 2;
        _courserAddition = addon;
    } 
}
