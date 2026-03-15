using UnityEngine;

public class Upgrades : MonoBehaviour
{
    [SerializeField] TeammateManager teammateManager;

    public float cost;
    
    void Upgrade(string name, string type)
    {
        switch (name)
        {
            case "Player":
                return;
            case "Teammate":
                return;
            case "Buildings":
                return;
        }
    }
}
