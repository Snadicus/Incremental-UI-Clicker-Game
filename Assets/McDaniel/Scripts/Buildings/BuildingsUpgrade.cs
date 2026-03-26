using UnityEngine;

public class BuildingsUpgrade : MonoBehaviour
{
    [SerializeField] BuildingsList buildingsList;

    public void Upgrade(string type)
    {
        BuildingsList.BuildingData? currentBuilding = buildingsList.GetBuildingData(type);
        if (currentBuilding.level <= 0)
        {
            currentBuilding.level = 1;
            StartCoroutine(currentBuilding.GainIncome());
            return;
        }
        currentBuilding.level++;
        currentBuilding.income += currentBuilding.income + (currentBuilding.level / currentBuilding.income);
    }
}
