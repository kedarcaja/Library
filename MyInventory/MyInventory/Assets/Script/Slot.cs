using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerClickHandler
{

    #region Variables
    private Stack<Item> items;
    public Item iTem;
    public Stack<Item> Items
    {
        get
        {
            return items;
        }

        set
        {
            items = value;
        }
    }

    public Image QualityImage;
    public Sprite[] QualityList;
    int qualityCount;
    public Text stackTxt;
   
    public Sprite SlotEmpty;
    public Sprite slotHightlight;

    public bool IsEmpty {
        get { return items.Count == 0; }
    }

    public bool IsAvailable {
        get { return CurrentItem.MaxSize > items.Count; }
    }

    public Item CurrentItem {
        get { return items.Peek(); }
    }


    #endregion

    #region Unity Metod
    void Awake() {
        items = new Stack<Item>();

    }
    void Start()
    {

        RectTransform slotRect = GetComponent<RectTransform>();
        RectTransform txtRect = stackTxt.GetComponent<RectTransform>();

        int txtScaleFactor = (int)(slotRect.sizeDelta.x * 0.60);
        stackTxt.resizeTextMaxSize = txtScaleFactor;
        stackTxt.resizeTextMinSize = txtScaleFactor;

        txtRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, slotRect.sizeDelta.y);
        txtRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, slotRect.sizeDelta.x);

    }
  

    void Update() {
       
    }
    public void AddItem(Item item) {
        items.Push(item);
       
        iTem.Qualitint();
        qualityCount = iTem.qualita;
        if (items.Count > 1)
        {
            stackTxt.text = items.Count.ToString();
        }
        ChangeSprite(item.spriteNeutral, item.spriteHighlighted);
        QualityImage.sprite = QualityList[qualityCount];


    }

    public void AddItems(Stack<Item> items) {
        this.items = new Stack<Item>(items);
        
        stackTxt.text = items.Count > 1 ? items.Count.ToString() : string.Empty;
        ChangeSprite(CurrentItem.spriteNeutral, CurrentItem.spriteHighlighted);
    }

    private void ChangeSprite(Sprite neutral, Sprite highlight) {
        GetComponent<Image>().sprite = neutral;
        SpriteState st = new SpriteState();
        st.highlightedSprite = highlight;
        st.pressedSprite = neutral;
        GetComponent<Button>().spriteState = st;
    }

    private void UseItem() {
        if (!IsEmpty)
        {
            items.Pop().Use();
            stackTxt.text = items.Count > 1 ? items.Count.ToString() : string.Empty;

            if (IsEmpty)
            {
                ChangeSprite(SlotEmpty, slotHightlight);
                Inventory.EmptySlot++;
            }
        }
    }
    public void ClearSlot() {
        items.Clear();
        ChangeSprite(SlotEmpty, slotHightlight);
        stackTxt.text = string.Empty;
    }
    public Stack<Item> RemoveItem(int amount) {
        Stack<Item> tmp = new Stack<Item>();
        for (int i = 0; i < amount; i++)
        {
            tmp.Push(items.Pop());
        }
        stackTxt.text = items.Count > 1 ? items.Count.ToString() : string.Empty;
        return tmp;
    }
    public Item RemoveItem() {
        Item tmp;
        tmp = items.Pop();
        stackTxt.text = items.Count > 1 ? items.Count.ToString() : string.Empty;
        return tmp;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        // if (eventData.button == PointerEventData.InputButton.Right && !GameObject.Find("Hover") && Inventory.CanvasGroup.alpha > 0)
        if (Input.GetKey(KeyCode.E) && !GameObject.Find("Hover") && Inventory.CanvasGroup.alpha > 0)
        {
            UseItem();
        }
    
       else if (eventData.button==PointerEventData.InputButton.Left&&Input.GetKey(KeyCode.LeftShift)&&!IsEmpty&&!GameObject.Find("Hover"))
        {
            Vector2 position;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(Inventory.Instance.canvas.transform as RectTransform, Input.mousePosition, Inventory.Instance.canvas.worldCamera, out position);
            Inventory.Instance.selectStackSize.SetActive(true);
            Inventory.Instance.selectStackSize.transform.position = Inventory.Instance.canvas.transform.TransformPoint(position);
            Inventory.Instance.SetStackInfo(items.Count);
        }
    }
    
       
    #endregion
}
