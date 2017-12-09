using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

    #region Variables
    private RectTransform inventoryRect;
   
    private float inventoryWidth, inventoryHight;

    public int Slot;
  
    public int rows;

    public float slotPaddingLeft, slotPaddingTop;

    public float slotSize;

    private float hoverYOffset;
   
    public GameObject slotPrefab;
    public GameObject slotBGPrefab;
    public GameObject trash;

    public GameObject Mana;
    public GameObject Health;

    public EventSystem eventSystem;
    public GameObject BgParent,SlotParent;
    private static Slot from, to;

    private List<GameObject> allSlots;
    private List<GameObject> allSlotsBG;
  
    private static GameObject clicked;
    public GameObject IconPrefab;
    private static GameObject hoverObject;
    public GameObject selectStackSize;
    private static GameObject selectStackSizeStatic;
    public Text StackText;
    private int spliteAmount;
    private int maxStackCount;
    private static Slot movingSlot;
   
    public Canvas canvas;
    private static CanvasGroup canvasGroup;
    private static Inventory instance;
    private bool fadingIn, fadingOut;
    public float fadeTime;
    private static int emptySlot;
    int DrawSlotBG;
    int DrawSlot;

    public GameObject tooltipObject;
    private static GameObject tooltip;
    public Text sizeTextObject;
    private static Text sizeText;
    public Text visialTextObject;
    private static Text visialText;

    public static int EmptySlot
    {
        get
        {
            return emptySlot;
        }

        set
        {
            emptySlot = value;
        }
    }

    public static CanvasGroup CanvasGroup
    {
        get
        {
            return Inventory.canvasGroup;
        }


    }

    public static Inventory Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<Inventory>();
            }
            return instance;
        }

    }

    #endregion

    #region Unity Metod

    void Start() {
        tooltip = tooltipObject;
        visialText = visialTextObject;
        sizeText = sizeTextObject;
        selectStackSizeStatic = selectStackSize;
        CreateBGLayout();
        canvasGroup = transform.parent.GetComponent<CanvasGroup>();
        movingSlot = GameObject.Find("MovingSlot").GetComponent<Slot>();
        
    }
    void Update() {

        if (Input.GetMouseButtonUp(0)) {
            if (!eventSystem.IsPointerOverGameObject(-1) && from != null)
            {
                from.GetComponent<Image>().color = Color.white;
                Destroy(GameObject.Find("Hover"));
                to = null;
                hoverObject = null;
            }

        }

        if (hoverObject != null)
        {
            Vector2 position;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, canvas.worldCamera, out position);
            position.Set(position.x, position.y - hoverYOffset);
            hoverObject.transform.position = canvas.transform.TransformPoint(position);
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            if (canvasGroup.alpha > 0)
            {
                StartCoroutine("FadeOut");
                PutItemBack();


            }
            else { StartCoroutine("FadeIn"); }
            
        }

        if (Input.GetMouseButton(2))
        {
            
            if (!eventSystem.IsPointerOverGameObject(-1))
            {
               // MoveInventory();
            }
        }
    }
    public void ShowToolTip(GameObject slot)
    {
        Slot tmpSlot = slot.GetComponent<Slot>();
        if (!tmpSlot.IsEmpty && hoverObject == null && !selectStackSizeStatic.activeSelf)
        {
            visialText.text = tmpSlot.CurrentItem.GetTooltip();
            sizeText.text = visialText.text;
            tooltip.SetActive(true);
            float xPos = slot.transform.position.x + slotPaddingLeft;
            float yPos = slot.transform.position.y - slot.GetComponent<RectTransform>().sizeDelta.y - slotPaddingTop;
            
            tooltip.transform.position = new Vector3(xPos, yPos);
        }
        
    }
    public void HideToolTip() {
        tooltip.SetActive(false);
    }
    public void SaveInventory() {
        string content = string.Empty;
        for (int i = 0; i < allSlots.Count; i++)
        {
            Slot tmp = allSlots[i].GetComponent<Slot>();
            if (!tmp.IsEmpty)
            {
                content += i + "-" + tmp.CurrentItem.type.ToString() + "-" + tmp.Items.Count.ToString() + ";";
            }
        }

        PlayerPrefs.SetString("content", content);
        PlayerPrefs.SetInt("slot", Slot);
        PlayerPrefs.SetInt("rows",rows);
        PlayerPrefs.SetFloat("slotPaddingLeft", slotPaddingLeft);
        PlayerPrefs.SetFloat("slotPaddingTop", slotPaddingTop);
        PlayerPrefs.SetFloat("slotSize", slotSize);
        PlayerPrefs.SetFloat("xPos", inventoryRect.position.x);
        PlayerPrefs.SetFloat("yPos", inventoryRect.position.y);

        PlayerPrefs.Save();
    }
    public void LoadInventory() {
        string content = PlayerPrefs.GetString("content");

        Slot = PlayerPrefs.GetInt("slot");
        rows = PlayerPrefs.GetInt("rows");
        slotPaddingLeft = PlayerPrefs.GetFloat("slotPaddingLeft");
        slotPaddingTop = PlayerPrefs.GetFloat("slotPaddingTop");

        inventoryRect.position = new Vector3(PlayerPrefs.GetFloat("xPos"), PlayerPrefs.GetFloat("yPos"), inventoryRect.position.z);

        CreateLayout();

        string[] splitContent = content.Split(';'); // 0-MANA-3

        for (int x = 0;  x < splitContent.Length-1;  x++)
        {
            string[] splitValues = splitContent[x].Split('-');
            int index = Int32.Parse(splitValues[0]); //0
            ItemType type = (ItemType)Enum.Parse(typeof(ItemType), splitValues[1]);//MANA
            int amount = Int32.Parse(splitValues[2]); //"3"

            for (int i = 0; i < amount; i++)
            {
                switch (type)
                {
                    case ItemType.MANA:
                        allSlots[index].GetComponent<Slot>().AddItem(Mana.GetComponent<Item>());
                        break;
                    case ItemType.HELTH:
                        allSlots[index].GetComponent<Slot>().AddItem(Health.GetComponent<Item>());
                        break;
                    default:
                        break;
                }
            }
        }

    }
    private void MoveInventory() {
        Vector2 mousePos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform,new Vector3( Input.mousePosition.x - (inventoryRect.sizeDelta.x / 2 * canvas.scaleFactor), Input.mousePosition.y + (inventoryRect.sizeDelta.y / 2 * canvas.scaleFactor)), canvas.worldCamera, out mousePos);

        transform.position = canvas.transform.TransformPoint(mousePos);
    }
    private void PutItemBack() {
        if (from != null)
        {
            from.GetComponent<Image>().color = Color.white;
            Destroy(GameObject.Find("Hover"));
            from = null;
        }

    }
    public void TrashItem() {
        if (hoverObject != null)
        {
            from.GetComponent<Image>().color = Color.white;
            from.ClearSlot();
            Destroy(GameObject.Find("Hover"));
            to = null;
            from = null;
            hoverObject = null;
            emptySlot++;
        }
    }
    private void CreateBGLayout()
    {
        
        allSlotsBG = new List<GameObject>();

        inventoryWidth = (Slot / rows) * (slotSize + slotPaddingLeft) + slotPaddingLeft;
        inventoryHight = rows * (slotSize + slotPaddingTop) + slotPaddingTop + 50;
        inventoryRect = GetComponent<RectTransform>();
        inventoryRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, inventoryWidth);
        inventoryRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, inventoryHight);
        int colomns = Slot / rows;
        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < colomns; x++)
            {
                
                GameObject newSlotBG = (GameObject)Instantiate(slotBGPrefab);
                newSlotBG.transform.SetParent(BgParent.transform);
                RectTransform slotRectBG = newSlotBG.GetComponent<RectTransform>();
                newSlotBG.name = "Slot";

                slotRectBG.localPosition = inventoryRect.localPosition + new Vector3(slotPaddingLeft * (x + 1) + (slotSize * x), -slotPaddingTop * (y + 1) - (slotSize * y));

                slotRectBG.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, slotSize * canvas.scaleFactor);
                slotRectBG.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, slotSize * canvas.scaleFactor);
                DrawSlotBG++;
                allSlotsBG.Add(newSlotBG);

                if (DrawSlotBG == Slot)
                {
                    CreateLayout();
                }
            }
        }

    }  
    private void CreateLayout()
    {
        if (allSlots != null)
        {
            foreach (GameObject go in allSlots) {
                Destroy(go);
            }
        }
        allSlots = new List<GameObject>();
        

        hoverYOffset = slotSize * 0.01f;
        EmptySlot = Slot;
        inventoryWidth = (Slot / rows) * (slotSize + slotPaddingLeft) + slotPaddingLeft;
        inventoryHight = rows * (slotSize + slotPaddingTop) + slotPaddingTop + 50;
        inventoryRect = GetComponent<RectTransform>();
        inventoryRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, inventoryWidth);
        inventoryRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, inventoryHight);
        int colomns = Slot / rows;
        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < colomns; x++)
            {
                GameObject newSlot = (GameObject)Instantiate(slotPrefab);
                newSlot.transform.SetParent(SlotParent.transform);
                RectTransform slotRect = newSlot.GetComponent<RectTransform>();
                newSlot.name = "Item Slot";

                slotRect.localPosition = inventoryRect.localPosition + new Vector3(slotPaddingLeft * (x + 1) + (slotSize * x), -slotPaddingTop * (y + 1) - (slotSize * y));

                slotRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, slotSize * canvas.scaleFactor);
                slotRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, slotSize * canvas.scaleFactor);

                allSlots.Add(newSlot);




            }

        }
    }
    private void CreateHoverIcon() {
        hoverObject = Instantiate(IconPrefab);
        hoverObject.GetComponent<Image>().sprite = clicked.GetComponent<Image>().sprite;
        hoverObject.name = "Hover";

        RectTransform hoverTransform = hoverObject.GetComponent<RectTransform>();
        RectTransform clickedTranform = clicked.GetComponent<RectTransform>();

        hoverTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, clickedTranform.sizeDelta.x);
        hoverTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, clickedTranform.sizeDelta.y);

        hoverObject.transform.SetParent(GameObject.Find("Canvas").transform, true);

        hoverObject.transform.localScale = clicked.gameObject.transform.localScale;

        hoverObject.transform.GetChild(0).GetComponent<Text>().text = movingSlot.Items.Count > 1 ? movingSlot.Items.Count.ToString() : string.Empty;
    }
    public void SetStackInfo(int MaxStackCount) {
        selectStackSize.SetActive(true);
        tooltip.SetActive(false);
        spliteAmount = 0;
        this.maxStackCount = MaxStackCount;
        StackText.text = spliteAmount.ToString();
    }
    public void SplitStack()
    {
        selectStackSize.SetActive(false);
        if (spliteAmount == maxStackCount)
        {
            MoveItem(clicked);
        }
        else if (spliteAmount > 0) {
            movingSlot.Items = clicked.GetComponent<Slot>().RemoveItem(spliteAmount);
            CreateHoverIcon();
        }
    }
    public void ChangeStackText(int i) {
        spliteAmount += i;
        if (spliteAmount < 0)
        {
            spliteAmount = 0;
        }
        if (spliteAmount > maxStackCount)
        {
            spliteAmount = maxStackCount;
        }
        StackText.text = spliteAmount.ToString();
    }
    public void MergeSize(Slot source, Slot destination) {
        int max = destination.CurrentItem.MaxSize - destination.Items.Count;
        int count = source.Items.Count < max ? source.Items.Count : max;
        for (int i = 0; i < count; i++)
        {
            destination.AddItem(source.RemoveItem());
        }
        if (source.Items.Count ==0)
        {
            source.ClearSlot();
            Destroy(GameObject.Find("Hover"));
        }
    }
    public bool AddItem(Item item) {
        if (item.MaxSize == 1)
        {
            PlaceEmpty(item);
            return true;
        } 
        else {
            foreach (GameObject slot in allSlots) {
                Slot tmp = slot.GetComponent<Slot>();

                if (!tmp.IsEmpty)
                {
                    if (tmp.CurrentItem.type==item.type&&tmp.IsAvailable)
                    {
                        if (!movingSlot.IsEmpty && clicked.GetComponent<Slot>() == tmp.GetComponent<Slot>())
                        {
                            continue;
                        }
                        else {
                            
                            tmp.AddItem(item);
                            
                            //EmptySlot--;
                            return true;
                        }
                    }
                }
            }
            if (EmptySlot>0)
            {
                PlaceEmpty(item);
            }
        }
        return false;
    }
    private bool PlaceEmpty(Item item)
    {
        if (EmptySlot>0)
        {
            foreach (GameObject slot in allSlots)
            {
                Slot tmp = slot.GetComponent<Slot>();
                if (tmp.IsEmpty)
                {
                    tmp.AddItem(item);
                    
                    //EmptySlot--;
                    return true;
                }
            }
        }return false;
    }
    public void MoveItem(GameObject clicked) {
        Inventory.clicked = clicked;

        if (!movingSlot.IsEmpty)
        {
            Slot tmp = clicked.GetComponent<Slot>();

            if (tmp.IsEmpty) {
                tmp.AddItems(movingSlot.Items);
                movingSlot.Items.Clear();
                Destroy(GameObject.Find("Hover"));
            }
            else if (!tmp.IsEmpty && movingSlot.CurrentItem.type == tmp.CurrentItem.type && tmp.IsAvailable)
            {
                MergeSize(movingSlot, tmp);
            }     

        }
        else if (from == null && canvasGroup.alpha == 1 && !Input.GetKey(KeyCode.LeftShift))
        {
            if (!clicked.GetComponent<Slot>().IsEmpty && !GameObject.Find("Hover"))
            {
                
                from = clicked.GetComponent<Slot>();
                from.GetComponent<Image>().color = Color.gray;
                CreateHoverIcon();
            }
        }
        else if (to == null && !Input.GetKey(KeyCode.LeftShift))
        {
            to = clicked.GetComponent<Slot>();
            Destroy(GameObject.Find("Hover"));
        }
        if (to != null && from != null)
        {
            Stack<Item> tmpTo = new Stack<Item>(to.Items);
            to.AddItems(from.Items);
            if (tmpTo.Count==0)
            {
                from.ClearSlot();
            }
            else
            {
                from.AddItems(tmpTo);
            }
            from.GetComponent<Image>().color = Color.white;
            to = null;
            from = null;
            Destroy(GameObject.Find("Hover"));
        }
    }

    private IEnumerator FadeOut() {
        if (!fadingOut)
        {
            fadingOut = true;
            fadingIn = false;
            StopCoroutine("FadeIn");
            float startAlfa = canvasGroup.alpha;
            float rate = 1.0f / fadeTime;
            float progress = 0.0f;
            while (progress<1.0)
            {
                canvasGroup.alpha = Mathf.Lerp(startAlfa, 0, progress);
                progress += rate * Time.deltaTime;
                yield return null;
            }
            canvasGroup.alpha = 0;
            fadingOut = false;
        }

    }
    private IEnumerator FadeIn()
    {
        if (!fadingIn)
        {
            fadingOut = false;
            fadingIn = true;
            StopCoroutine("FadeOut");
            float startAlfa = canvasGroup.alpha;
            float rate = 1.0f / fadeTime;
            float progress = 0.0f;
            while (progress < 1.0)
            {
                canvasGroup.alpha = Mathf.Lerp(startAlfa, 1, progress);
                progress += rate * Time.deltaTime;
                yield return null;
            }
            canvasGroup.alpha = 1;
            fadingIn = false;
        }

    }
    #endregion
}
