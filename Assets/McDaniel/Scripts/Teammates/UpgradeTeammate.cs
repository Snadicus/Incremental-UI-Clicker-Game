using UnityEngine;

public class UpgradeTeammate : MonoBehaviour
{
    [SerializeField] TeammateManager teammateManager;

    public void Upgrading(string type)
    {
        TeammateManager.Teammates? currentTeammate = teammateManager.GetTeammate(type);
        if (currentTeammate == null)
        {
            return;
        }

        if (currentTeammate.level <= 0)
        {
            currentTeammate.level += 1;
            StartCoroutine(currentTeammate.Attack());
        }
        else
        {
            currentTeammate.level += 1;
            currentTeammate.attackPower += 1;
        }
    }
}
