using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "NewPage", menuName = "Book/NewPage", order = 1)]
public class NewPage : ScriptableObject
{
    [Header("TextPart")]
    [SerializeField]
    private LernTextPart[] textPart;

    [Header("ImagePart")]
    [SerializeField]
    private LernImagePart[] imagePart;


    public void Create()
    {
       
        for (int i = 0; i < textPart.Length; i++)
        {
            if (textPart[i].IsDiscovered == true)
            {
                GameObject hand = GameObject.Find("Canvas/Book/" + imagePart[i].Page);
                GameObject clone = Instantiate(textPart[i].Prefab, hand.transform.position + textPart[i].Position, Quaternion.identity) as GameObject;
                clone.transform.parent = hand.transform;
                clone.GetComponent<RectTransform>().sizeDelta = textPart[i].Size;
                clone.GetComponent<Text>().fontSize = textPart[i].FontSize;
                clone.GetComponent<Text>().text = textPart[i].Text;
            }

        }

        for (int j = 0; j < imagePart.Length; j++)
        {
            if (imagePart[j].IsDiscovered == true)
            {
                GameObject hand = GameObject.Find("Canvas/Book/"+ imagePart[j].Page);


                GameObject clone = Instantiate(imagePart[j].Prefab, hand.transform.position + imagePart[j].Position, Quaternion.identity) as GameObject;
                clone.transform.parent = hand.transform;
                clone.GetComponent<RectTransform>().sizeDelta = imagePart[j].Size;

                clone.GetComponent<Image>().sprite = imagePart[j].Image;
            }

        }


    }

    public void Destroy(Transform game)
    {
        
        if (game.childCount > 0)
        {
            while (game.childCount > 0)
            {
                Transform child = game.GetChild(0);
                child.parent = null;
                Destroy(child.gameObject);
            }
        }

    }



}


