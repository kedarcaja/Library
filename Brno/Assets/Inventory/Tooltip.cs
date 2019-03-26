using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class Tooltip : MonoBehaviour, IOpenable
{

	[SerializeField]
	private TextMeshProUGUI _name, type, mainValue, otherValue, quality, stats, description,gold,silver,copper;
	[SerializeField]
	private Text statsSizer, descriptionSizer;
	public static Tooltip Instance;
	private bool opened;
	public bool Opened { get { return opened; } }
	[SerializeField]
	private Image icon,backg;

	public event OpenHandler OnOpen;
	public event OpenHandler OnClose;

	//public Image gem;--- create array of gems
	private void Awake()
	{
		
		Instance = FindObjectOfType<Tooltip>();
	}
	public void SetTooltip(string name, string type, string mainValue, string mainValueLable,
						   string otherValue, string otherValueLable, string quality, Sprite icon,
						   string stats, string description, string gold, string silver, string copper/*,Sprite gem only for one gem=> need 4*/,Sprite bg)
	{
		ClearTooltip();
		this.type.text = type;
		this.mainValue.text = SetTextWithSizes(mainValue, mainValueLable, 50, 20,10);
		this.otherValue.text = SetTextWithSizes(otherValue, otherValueLable, 20, 20,20);
		this.quality.text = quality;
		this.stats.text = stats;
		this.description.text = description;
		//coins, icon
		this.statsSizer.text = stats;
		this.descriptionSizer.text = description;
		this._name.text = name;
		this.gold.text = gold;
		this.silver.text = silver;
		this.copper.text = copper;
		this.icon.sprite = icon;
		backg.sprite = bg;
		/*	if(gem!=null)
			this.gem.sprite = gem;// set gem to tooltip this is only one slot
			*/
	}
	private string SetTextWithSizes(string t1, string t2, int size1, int size2, int spaceSize)
	{
		return "<size=" + size1.ToString() + ">" + t1 + "</size>" + "<size=" + spaceSize.ToString() + ">" + " " + "</size>" + "<size=" + size2.ToString() + ">" + t2 + "</size>";
	}

	private void ClearTooltip()
	{
		this.type.text = "";
		this.mainValue.text = "";
		this.otherValue.text = "";
		this.quality.text = "";
		this.stats.text = "";
		this.description.text = "";
		//coins, icon
		this.statsSizer.text = "";
		this.descriptionSizer.text = "";
		this._name.text = "";
	}

	public void Open() { GetComponent<CanvasGroup>().alpha = 1; opened = true; if (OnOpen != null) { OnOpen(); } }
	public void Close() { GetComponent<CanvasGroup>().alpha = 0; opened = false; if (OnClose != null) { OnClose(); } }
}
