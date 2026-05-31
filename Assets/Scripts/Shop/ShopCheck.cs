using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;

public class ShopCheck : MonoBehaviour, IInteractable
{
    public GameObject maxHealthText;
    public GameObject dumbassText;
    public GameObject shopCanvas;
  
    public string shopName;
    public TMP_Text shopNameText;
    public GameObject shopPanel;
    public GameObject[] levelNames;
    public GameObject[] descriptions;

    public GameObject junkText;
    public GameObject coinText;

    
    void Start()
    {
        
    }

    void Update()
    {
        

        if (Health.HealthInstance.healthAmount <= 0)
        {
            return;
        }
        NavigatePages();

        if (Keyboard.current.zKey.wasPressedThisFrame)
        {
            if (EventSystem.current.currentSelectedGameObject == levelNames[0])
            {
                OnAppleJuicePressed();
            }
            else if (EventSystem.current.currentSelectedGameObject == levelNames[1])
            {
                OnMilkShakePressed();
            }
            else if (EventSystem.current.currentSelectedGameObject == levelNames[2])
            {
                OnTeaPressed();
            }
            else if (EventSystem.current.currentSelectedGameObject == levelNames[3])
            {
                OnJunkPressed();
            }

        }
    }

    public void NavigatePages() //Grey out page text
    {
        for (int i = 0; i < levelNames.Length; i++)
        {
            TMP_Text levelText = levelNames[i].GetComponent<TMP_Text>();

            if (EventSystem.current.currentSelectedGameObject == levelNames[i])
            {


                levelText.color = Color.red;

            }

            else
            {
                levelText.color = Color.white;
               


            }

        }

    }

    IEnumerator AppleText()
    {
        descriptions[0].SetActive(true);
        yield return new WaitForSecondsRealtime(1f);
        descriptions[0].SetActive(false);
    }
    IEnumerator MilkShakeText()
    {
        descriptions[1].SetActive(true);
        yield return new WaitForSecondsRealtime(1f);
        descriptions[01].SetActive(false);
    }
    IEnumerator TeaText()
    {
        descriptions[2].SetActive(true);
        yield return new WaitForSecondsRealtime(1f);
        descriptions[2].SetActive(false);
    }
    IEnumerator MaxHealth()
    {
        maxHealthText.SetActive(true);
        yield return new WaitForSecondsRealtime(1f);
        maxHealthText.SetActive(false);

    }

    IEnumerator NoJunkForU()
    {
        junkText.SetActive(true);
        yield return new WaitForSecondsRealtime(1f);
        junkText.SetActive(false);

    }

    IEnumerator DumbAss()
    {
        dumbassText.SetActive(true);
        yield return new WaitForSecondsRealtime(1f);
        dumbassText.SetActive(false);

    }

    IEnumerator NoCoins()
    {
        coinText.SetActive(true);
        yield return new WaitForSecondsRealtime(1f);
        coinText.SetActive(false);

    }
    public void OnMilkShakePressed()
    {
        int healthToAdd = 20;
        if (Health.HealthInstance.healthAmount != 100 && Collectables.coins >= 2)
        {
            Health.HealthInstance.healthAmount = Mathf.Min(Health.HealthInstance.healthAmount + healthToAdd, 100);
            StartCoroutine(MilkShakeText());
            Collectables.coins -= 2;

        }
        else if (Health.HealthInstance.healthAmount == 100)
        {
            StartCoroutine(MaxHealth());
        }
        else if (Collectables.coins < 2)
        {
            StartCoroutine(NoCoins());
        }


    }

  

    public void OnAppleJuicePressed()
    {
        int healthToAdd = 10;
        if (Health.HealthInstance.healthAmount != 100 && Collectables.coins >= 1)
        {
            Health.HealthInstance.healthAmount = Mathf.Min(Health.HealthInstance.healthAmount + healthToAdd, 100);
           StartCoroutine(AppleText());
            Collectables.coins -= 1;

        }
       
        else if (Health.HealthInstance.healthAmount == 100)
        {
            StartCoroutine(MaxHealth());
        }
        else if (Collectables.coins < 1)
        {
            StartCoroutine(NoCoins());
        }

    }

    public void OnTeaPressed()
    {
        int healthToAdd = 30;
        if (Health.HealthInstance.healthAmount != 100 && Collectables.coins >= 3)
        {
            Health.HealthInstance.healthAmount = Mathf.Min(Health.HealthInstance.healthAmount + healthToAdd, 100);
            Collectables.coins -= 3;
            StartCoroutine(TeaText());

        }
        else if(Health.HealthInstance.healthAmount == 100)
        {
            StartCoroutine(MaxHealth());
        }
        else if (Collectables.coins < 3)
        {
            StartCoroutine(NoCoins());
        }
    }


    public void OnJunkPressed()
    {
        int healthToAdd = 30;
        if (Health.HealthInstance.healthAmount >= 30)
        {
            Health.HealthInstance.healthAmount = Mathf.Max(Health.HealthInstance.healthAmount - healthToAdd, 0);
            StartCoroutine(DumbAss());

        }
        else
        {
            StartCoroutine(NoJunkForU());
        }
    }

    public void Interact()
    {
        bool opening = !shopCanvas.activeSelf;

        // Block opening if paused by something else, but always allow closing
        if (!opening == false && PauseController.IsGamePaused) return;

        shopCanvas.SetActive(opening);
        shopPanel.SetActive(opening);

        if (opening)
        {
            shopNameText.text = shopName;
            PauseController.SetPause(true);
            EventSystem.current.SetSelectedGameObject(levelNames[0]);
        }
        else
        {
            PauseController.SetPause(false);
            EventSystem.current.SetSelectedGameObject(null);
        }
    }

    public bool CanInteract()
    {
        return true;
    }
}
