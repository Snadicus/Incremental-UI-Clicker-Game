using UnityEngine;
using UnityEngine.UI;

public class ShowHideButtons : MonoBehaviour
{
    public CanvasGroup canvas;

    public LayoutElement layoutElement;

    public void ShowHide()
    {
        if (canvas.alpha >0)
        {
            canvas.alpha = 0;
            layoutElement.ignoreLayout = true;
        }
        else
        {
            canvas.alpha = 1;
            layoutElement.ignoreLayout = false;
        }
    }
}
