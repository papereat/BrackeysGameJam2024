using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using JetBrains.Annotations;
using UnityEngine.UI;

public class ShopInteractible : LocationInteractableComponent
{
    SoundController soundController;

    public GameObject ShopUI;
    public GameObject GeneralUI;
    public float depthprice;
    public float powerPrice;
    public float capacityPrice;
    bool inMenu;


    //Enables/Disables UI
    public override void OnActivate()
    {
        if (!ShopUI.GetComponent<Canvas>().enabled)
        {
            soundController.playOverSound(8, 0.5f);
        }

        ShopUI.GetComponent<Canvas>().enabled = !ShopUI.GetComponent<Canvas>().enabled;
        GeneralUI.GetComponent<Canvas>().enabled = !GeneralUI.GetComponent<Canvas>().enabled;

        ShopUI.transform.GetChild(4).GetChild(1).GetComponent<TMP_Text>().text = "Cost: " + depthprice;
        ShopUI.transform.GetChild(5).GetChild(1).GetComponent<TMP_Text>().text = "Cost: " + powerPrice;
        ShopUI.transform.GetChild(6).GetChild(1).GetComponent<TMP_Text>().text = "Cost: " + capacityPrice;
    }

    void Start()
    {
        soundController = SoundController.soundController;

        if (worldManager.inOverworld)
        {
            ShopUI.transform.GetChild(3).GetComponent<TMP_Text>().text = "Money: " + 0;
            UpdateGeneralUI();

        }
        else
        {
            ShopUI.transform.GetChild(2).GetComponent<TMP_Text>().text = "Money: " + underworldControler.Money;
            GeneralUI.transform.GetChild(0).GetComponent<TMP_Text>().text = "Money: " + underworldControler.Money;
        }

    }

    void UpdateGeneralUI()
    {
        GeneralUI.transform.GetChild(0).GetComponent<TMP_Text>().text = "Money: " + player.Money;
        GeneralUI.transform.GetChild(1).GetComponent<TMP_Text>().text = "Value on Ship: " + player.valueOnShip;
    }
    public override void Update()
    {
        base.Update();
        if (!worldManager.inOverworld)
        {
            ShopUI.transform.GetChild(2).GetComponent<TMP_Text>().text = "Money: " + underworldControler.Money;
            GeneralUI.transform.GetChild(0).GetComponent<TMP_Text>().text = "Money: " + underworldControler.Money;
        }
        else
        {
            UpdateGeneralUI();
        }

    }

    public void sellFish()
    {
        if (player.valueOnShip > 0)
        {
            soundController.playOverSound(7, 1);

            //Change later to depend on type of fish etc. etc.
            player.Money += player.valueOnShip;
            player.TotalMoney += player.valueOnShip;
            player.valueOnShip = 0;

            ShopUI.transform.GetChild(3).GetComponent<TMP_Text>().text = "Money: " + player.Money;
            GeneralUI.transform.GetChild(0).GetComponent<TMP_Text>().text = "Money: " + player.Money;
            GeneralUI.transform.GetChild(1).GetComponent<TMP_Text>().text = "Value on Ship: " + player.valueOnShip;
        }


    }

    //Changing Value based on button press
    public void upgradeDepth()
    {
        if (worldManager.inOverworld)
        {
            if (player.Money >= depthprice)
            {

                soundController.playOverSound(6, 1);

                player.Money -= depthprice;
                ShopUI.transform.GetChild(3).GetComponent<TMP_Text>().text = "Money: " + player.Money;

                depthprice *= 2;
                ShopUI.transform.GetChild(4).GetChild(1).GetComponent<TMP_Text>().text = "Cost: " + depthprice;

                player.fishingRod.Depth += 1;
                ShopUI.transform.GetChild(4).GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = "Level: " + player.fishingRod.Depth;

            }
        }
        else
        {
            if (underworldControler.Money >= depthprice)
            {

                soundController.playOverSound(6, 1);

                underworldControler.Money -= depthprice;
                ShopUI.transform.GetChild(3).GetComponent<TMP_Text>().text = "Money: " + underworldControler.Money;

                depthprice *= 2;
                ShopUI.transform.GetChild(4).GetChild(1).GetComponent<TMP_Text>().text = "Cost: " + depthprice;

                underworldControler.fishingRod.Depth += 1;
                ShopUI.transform.GetChild(4).GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = "Level: " + underworldControler.fishingRod.Depth;

            }
        }


    }

    public void upgradePower()
    {
        if (worldManager.inOverworld)
        {
            if (player.Money >= powerPrice)
            {
                soundController.playOverSound(6, 1);

                player.Money -= powerPrice;
                ShopUI.transform.GetChild(3).GetComponent<TMP_Text>().text = "Money: " + player.Money;

                powerPrice *= 2;
                ShopUI.transform.GetChild(5).GetChild(1).GetComponent<TMP_Text>().text = "Cost: " + powerPrice;

                player.fishingRod.Power += 1;
                ShopUI.transform.GetChild(5).GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = "Level: " + player.fishingRod.Power;
            }
        }
        else
        {
            if (underworldControler.Money >= powerPrice)
            {
                soundController.playOverSound(6, 1);

                underworldControler.Money -= powerPrice;
                ShopUI.transform.GetChild(3).GetComponent<TMP_Text>().text = "Money: " + underworldControler.Money;

                powerPrice *= 2;
                ShopUI.transform.GetChild(5).GetChild(1).GetComponent<TMP_Text>().text = "Cost: " + powerPrice;

                underworldControler.fishingRod.Power += 1;
                ShopUI.transform.GetChild(5).GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = "Level: " + underworldControler.fishingRod.Power;
            }
        }


    }

    public void upgradeCapacity()
    {
        if (worldManager.inOverworld)
        {
            if (player.Money >= capacityPrice)
            {

                soundController.playOverSound(6, 1);

                player.Money -= capacityPrice;
                ShopUI.transform.GetChild(3).GetComponent<TMP_Text>().text = "Money: " + player.Money;

                capacityPrice *= 2;
                ShopUI.transform.GetChild(6).GetChild(1).GetComponent<TMP_Text>().text = "Cost: " + capacityPrice;

                player.fishingRod.Capacity += 1;
                ShopUI.transform.GetChild(6).GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = "Level: " + player.fishingRod.Capacity;

            }
        }
        else
        {
            if (underworldControler.Money >= capacityPrice)
            {

                soundController.playOverSound(6, 1);

                underworldControler.Money -= capacityPrice;
                ShopUI.transform.GetChild(3).GetComponent<TMP_Text>().text = "Money: " + underworldControler.Money;

                capacityPrice *= 2;
                ShopUI.transform.GetChild(6).GetChild(1).GetComponent<TMP_Text>().text = "Cost: " + capacityPrice;

                underworldControler.fishingRod.Capacity += 1;
                ShopUI.transform.GetChild(6).GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = "Level: " + underworldControler.fishingRod.Capacity;

            }
        }


    }
}
