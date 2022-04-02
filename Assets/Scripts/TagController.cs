using UnityEngine;
using UnityEngine.UI;

public class TagController : MonoBehaviour
{
    bool show = false;

    // ------------------------------------------------------------------------
    public void ShowTags()
    {
        show = !show;
        gameObject.SetActive(show);
    }

    public void HideTags()
    {

        gameObject.SetActive(false);
    }

    
}
