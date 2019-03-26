using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MouseManager : MonoBehaviour
{
	public LayerMask MoveLayer, OpenLayer, DoorLayer, ForbiddenLayer;
    [SerializeField]
    private Transform player;
	public Texture2D pointerUI;
	public Texture2D pointerMove;
	public Texture2D pointerOpen;
	public Texture2D pointerDoor;
	public Texture2D pointerForbidden;
	public bool CanClick { get; set; }
	public EventVector3 OnClickEnvironment;
	public static MouseManager Instance;
    [SerializeField]
    private float moveClickRange;
	private void Awake()
	{
		Instance = FindObjectOfType<MouseManager>();

        UnityEngine.SceneManagement.SceneManager.sceneLoaded += new UnityAction<Scene, LoadSceneMode>(Init);
	}
	public void Init(Scene s, LoadSceneMode mode)
	{
		OnClickEnvironment.AddListener(FindObjectOfType<PlayerScript>().SetDestination);
	}
	void Update()
	{

		if (EventSystem.current.IsPointerOverGameObject())
		{
			SetCursor(pointerUI);
			return;
		}

		RaycastHit hit;
		if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
		{
            
            if (Math.Pow(2, hit.collider.gameObject.layer) == MoveLayer.value)
			{
				CanClick = true;
				SetCursor(pointerMove);
			}
			else if (Math.Pow(2, hit.collider.gameObject.layer) == DoorLayer)
			{
				CanClick = true;
				SetCursor(pointerDoor);
			}
			else if (Math.Pow(2, hit.collider.gameObject.layer) == OpenLayer.value)
			{
				CanClick = true;
				SetCursor(pointerOpen);
			}
            else if ((Math.Pow(2, hit.collider.gameObject.layer) != MoveLayer.value && Math.Pow(2, hit.collider.gameObject.layer) != DoorLayer && Math.Pow(2, hit.collider.gameObject.layer) != OpenLayer.value)|| Vector3.Distance(PlayerScript.Instance.transform.position, hit.point) > moveClickRange)
            {
                CanClick = false;
                SetCursor(pointerForbidden);
            }

            if (Input.GetMouseButton(0) && CanClick && Vector3.Distance(PlayerScript.Instance.transform.position, hit.point) <= moveClickRange)
            {
                OnClickEnvironment.Invoke(hit.point);
            }
          

        }

        
	}
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(player.position, moveClickRange);
    }
    public static void SetCursor(Texture2D texture)
	{
		Cursor.SetCursor(texture, new Vector2(16, 16), CursorMode.Auto);
        
	}
	
}

[Serializable]
public class EventVector3 : UnityEvent<Vector3> { }
