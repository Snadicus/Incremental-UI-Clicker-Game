using UnityEngine;
using UnityEngine.UI;

public class ShowHideButtons : MonoBehaviour
{
    public Canvas canvas;

    public LayoutElement layoutElement;

    public void ShowHide()
    {
        if (canvas.enabled)
        {
            canvas.enabled = false;
            layoutElement.ignoreLayout = true;
        }
        else
        {
            canvas.enabled = true;
            layoutElement.ignoreLayout = false;
        }
    }
}
