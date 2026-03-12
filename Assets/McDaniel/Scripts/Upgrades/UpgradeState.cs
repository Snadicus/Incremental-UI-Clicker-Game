using UnityEngine;

public class UpgradeState
{
    public class State
    {
        public string name;
        public System.Action onFrame;
        public System.Action onEnter;
        public System.Action onExit;
        public override string ToString()
        {
            return name;
        }
    }
}
