using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DecisionManager : MonoBehaviour
{
    public List<DecisionOptionValue> values = new List<DecisionOptionValue>();
    [SerializeField]
    private List<Sprite> icons = new List<Sprite>();
    public DecisionManager Instance { get; private set; }
    public List<Sprite> Icons { get => icons; }










    public Decision test;
    private void Awake()
    {
        Instance = FindObjectOfType<DecisionManager>();

    }
    public void SetDecision(Decision d)
    {
        values.ForEach(f => { f.gameObject.GetComponent<Button>().onClick.RemoveAllListeners(); f.gameObject.SetActive(false); });

        d.Options.ForEach(f =>
        {

            if (f.Enable)
            {
                DecisionOptionValue val = values[d.Options.IndexOf(f)];
                val.gameObject.SetActive(true);
                val.gameObject.GetComponent<TextMeshProUGUI>().text = f.Text;
               val.transform.GetComponentInChildren<Image>().sprite = icons[(int)f.Type];
                val.gameObject.GetComponent<DialogPlayer>().Dialog = d.Selected.Dialog;
                val.Option = f; val.gameObject.GetComponent<Button>().onClick.AddListener(() =>
                {
                    d.Selected = val.Option;
                    d.decided = true;
                    val.gameObject.GetComponent<DialogPlayer>().Play();
                });

            }
        });

    }




    private void Start()
    {
        SetDecision(test);
    }
}
