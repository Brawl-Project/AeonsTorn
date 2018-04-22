using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

// holds the refs to all the Text, Images on the card
public class OneCardManager : MonoBehaviour {

    public CardAsset cardAsset;
    public OneCardManager PreviewManager;
    [Header("Text Component References")]
	// If we are using default text, uncomment the following
	/*
    public Text NameText;
    public Text TPCostText;
    public Text DescriptionText;
    public Text HealthText;
    public Text AttackText;
	*/
	// If we are using TextMeshPro, use the following
	public GameObject CardNameText;
	public GameObject TPCostText;
	public GameObject DescriptionText;
	public GameObject HealthText;
	public GameObject AttackText;

    [Header ("GameObject References")]
    public GameObject HealthIcon;
    public GameObject AttackIcon;
    [Header("Image References")]
    public Image CardTopRibbonImage;
    public Image CardLowRibbonImage;
    public Image CardGraphicImage;
    public Image CardBodyImage;
    public Image CardFaceFrameImage;
    public Image CardFaceGlowImage;
    public Image CardBackGlowImage;

    void Awake()
    {
        if (cardAsset != null)
            ReadCardFromAsset();
    }

    private bool canBePlayedNow = false;
    public bool CanBePlayedNow
    {
        get
        {
            return canBePlayedNow;
        }

        set
        {
            canBePlayedNow = value;

            CardFaceGlowImage.enabled = value;
        }
    }

    public void ReadCardFromAsset()
    {
        // universal actions for any Card
        // 1) apply tint
        if (cardAsset.characterAsset != null)
        {
            CardBodyImage.color = cardAsset.characterAsset.ClassCardTint;
            CardFaceFrameImage.color = cardAsset.characterAsset.ClassCardTint;
            CardTopRibbonImage.color = cardAsset.characterAsset.ClassRibbonsTint;
            CardLowRibbonImage.color = cardAsset.characterAsset.ClassRibbonsTint;
        }
        else
        {
            //CardBodyImage.color = GlobalSettings.Instance.CardBodyStandardColor;
            CardFaceFrameImage.color = Color.white;
            //CardTopRibbonImage.color = GlobalSettings.Instance.CardRibbonsStandardColor;
            //CardLowRibbonImage.color = GlobalSettings.Instance.CardRibbonsStandardColor;
        }
        // 2) add card name
        //NameText.text = cardAsset.name;
		CardNameText.GetComponent<TextMeshProUGUI>().SetText(cardAsset.cardName);
        // 3) add mana cost
        //TPCostText.text = cardAsset.TPCost.ToString();
		TPCostText.GetComponent<TextMeshProUGUI>().SetText(cardAsset.TPCost.ToString());
        // 4) add description
        //DescriptionText.text = cardAsset.Description;
		DescriptionText.GetComponent<TextMeshProUGUI>().SetText(cardAsset.description);
        // 5) Change the card graphic sprite
        CardGraphicImage.sprite = cardAsset.cardImage;

        if (cardAsset.maxHealth != 0)
        {
            // this is a creature
            //AttackText.text = cardAsset.Attack.ToString();
            //HealthText.text = cardAsset.MaxHealth.ToString();
			AttackText.GetComponent<TextMeshProUGUI>().SetText(cardAsset.attack.ToString());
			HealthText.GetComponent<TextMeshProUGUI>().SetText (cardAsset.maxHealth.ToString ());
        }

        if (PreviewManager != null)
        {
            // this is a card and not a preview
            // Preview GameObject will have OneCardManager as well, but PreviewManager should be null there
            PreviewManager.cardAsset = cardAsset;
            PreviewManager.ReadCardFromAsset();
        }
    }
}
