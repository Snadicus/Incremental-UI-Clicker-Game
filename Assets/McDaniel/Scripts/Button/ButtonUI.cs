using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonUI : MonoBehaviour
{
    // UI References
    public Slider attackSlider;

    // References
    BuildingsList buildingsUpgrade;
    TeammateManager teammateManager;

    int index;

    // Variables
    string type; // Log type of upgrade to change what will happen
    float time; // How fast attack moves

    // Getting index of the upgrade type
    #region
    void Start()
    {
        buildingsUpgrade = GameObject.Find("BuildingManager").GetComponent<BuildingsList>();
        teammateManager = GameObject.Find("TeamManager").GetComponent<TeammateManager>();
        int index = 0;
        // Establish type of upgrade
        foreach (BuildingsList.BuildingData buildings in buildingsUpgrade.buildings)
        {
            if (gameObject.name == buildings.name)
            { 
                type = "Buildings";
                this.index = index;
                StartCoroutine(RaiseAttackBar());
                return;
            }
            index++;
        }
        index = 0;
        foreach (TeammateManager.Teammates teammate in teammateManager.teammates)
        {
            if (gameObject.name == teammate.teammateType)
            {
                type = "Teammates";
                this.index = index;
                StartCoroutine(RaiseAttackBar());
                Debug.Log("Got It");
                Debug.Log(type);
                Debug.Log(this.index);
                return;
            }
            index++;
        }
    }
    #endregion

    // Raises attack bar/income bar to show players how long until next attck/income
    public IEnumerator RaiseAttackBar()
    {
        if (type == "Teammates")
        {
            while (teammateManager.teammates[index].level >= 1)
            {
                time = teammateManager.teammates[index].attackSpeed;
                attackSlider.value += Time.deltaTime / time;
                if (attackSlider.value >= attackSlider.maxValue)
                {
                    attackSlider.value = attackSlider.minValue;
                }
                yield return null;
            }
        }
        if (type == "Buildings")
        {
            while (buildingsUpgrade.buildings[index].level >= 1)
            {
                time = buildingsUpgrade.buildings[index].speed;
                attackSlider.value += Time.deltaTime / time;
                if (attackSlider.value >= attackSlider.maxValue)
                {
                    attackSlider.value = attackSlider.minValue;
                }
                yield return null;
            }
        }
        yield return null;
        StartCoroutine(RaiseAttackBar());
    }
}
