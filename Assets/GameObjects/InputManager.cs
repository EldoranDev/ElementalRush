using System;
using UnityEngine;
using System.Collections;
using System.Security.Policy;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    public Material PlaceholderMaterial;
    public GameObject RangeIndicator;
    public float CameraSpeed = 18;
    public float CameraZoonSpeed = 20;
    public float MinCameraZoom = 20;
    public float MaxCameraZoom = 90;

    private Vector3 _lastMousePosition;

    public bool PlacementMode
    {
        get { return _courserAddition != null; }
    }

    private GameObject _courserAddition;
	
    void Awake()
    {
        Instance = this;
    }

	// Update is called once per frame
	void Update () {
        UpdateCamera();

	    if (!EventSystem.current.IsPointerOverGameObject())
	    {
	        UpdateCourser();
	        UpdateClicks();

	        if (!PlacementMode)
	        {
	            UpdateSelection();
	        }
	    }

        
	}

    void UpdateCamera()
    {
        if(Input.GetMouseButton(2))
        {
            var movment = _lastMousePosition - Input.mousePosition;
            Camera.main.transform.Translate(new Vector3(movment.x, 0, movment.y) * CameraSpeed * Time.deltaTime * (Camera.main.fieldOfView / MaxCameraZoom), Space.World);
        }

        var zoom = Input.GetAxis("Mouse ScrollWheel");

        Camera.main.fieldOfView -= zoom * CameraZoonSpeed;

        Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView, MinCameraZoom, MaxCameraZoom);

        _lastMousePosition = Input.mousePosition;
    }

    void UpdateSelection()
    {
        if (Input.GetMouseButtonUp(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            Tower selection = null;

            if (Physics.Raycast(ray.origin, ray.direction, out hit, float.PositiveInfinity,
                LayerMask.GetMask("BuildLayer")))
            {
                selection = hit.transform.gameObject.GetComponent<Tower>();
            }
            
            UIManager.Instance.DisplaySelection(selection);
            
        }
    }

    void UpdateClicks()
    {
        if (Input.GetMouseButtonUp(1))
        {
            if (PlacementMode)
            {
                Destroy(_courserAddition.gameObject);
                _courserAddition = null;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (PlacementMode)
            {
                
                var placeholder = _courserAddition.GetComponent<PlaceHolder>();
                var tower = placeholder.Original.GetComponent<Tower>();

                if (WorldManager.Instance.Money >= tower.Cost && placeholder.CanBePlaced)
                {
                    WorldManager.Instance.Money -= tower.Cost;

                    var location = _courserAddition.transform.position;

                    var obj = (GameObject)Instantiate(placeholder.Original, location, Quaternion.identity);
                    obj.transform.position = new Vector3(obj.transform.position.x, 0.77f, obj.transform.position.z);
                    Destroy(_courserAddition.gameObject);
                    _courserAddition = null;
                }
            }
        }
    }

    void UpdateCourser()
    {

        if (PlacementMode)
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
			if(Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << 10))
            //if (Physics.Raycast(ray.origin, ray.direction, out hit, ))
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

    public void TowerSelection(GameObject tower)
    {
        if (PlacementMode)
        {
            Destroy(_courserAddition.gameObject);
        }

        var addon = (GameObject)Instantiate(tower, Vector3.up, Quaternion.identity);
        var towerRange = addon.GetComponent<Tower>().Range;
        Destroy(addon.GetComponent<Tower>());

        var placeholder = addon.AddComponent<PlaceHolder>();
        placeholder.Original = tower;
        placeholder.SetMaterial(PlaceholderMaterial);

        var indicator = (GameObject) Instantiate(RangeIndicator, new Vector3(0, 0.7f, 0), Quaternion.identity);
        indicator.transform.localScale = new Vector3(towerRange * 2, indicator.transform.localScale.y, towerRange * 2);
        indicator.transform.SetParent(placeholder.transform);

        addon.layer = 2;
        _courserAddition = addon;
    } 
}
