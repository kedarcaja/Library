using UnityEngine;
public class CharacterPanel : MonoBehaviour,IOpenable
{

	public CharBag[] Bags;
	public static CharacterPanel Instance;


	public event OpenHandler OnOpen;
	public event OpenHandler OnClose;


	private bool opened = false;
	public bool Opened { get { return opened; } }

	public Slot WeaponSlot
	{
		get
		{
			return weaponSlot;
		}
	}

	public Slot SecondHandSlot
	{
		get
		{
			return secondHandSlot;
		}
	}

	[SerializeField]
	private Slot weaponSlot, secondHandSlot;
	private void Awake()
	{
		Instance = FindObjectOfType<CharacterPanel>();
		Bags = FindObjectsOfType<CharBag>();
	}

	public void Open()
	{
		gameObject.GetComponent<CanvasGroup>().alpha = 1;
		gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
	}

	public void Close()
	{
		gameObject.GetComponent<CanvasGroup>().alpha = 0;
		gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
	}
}
