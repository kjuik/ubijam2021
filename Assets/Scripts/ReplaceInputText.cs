using UnityEngine;
using UnityEngine.UI;

public class ReplaceInputText : MonoBehaviour
{
    public string pcInputText = "Press Space";
    public string mobileInputText = "Tap Anywhere";

    void Awake()
    {
        var text = GetComponent<Text>();

        if (text.text.Contains(pcInputText) && Application.isMobilePlatform)
        {
            text.text = text.text.Replace(pcInputText, mobileInputText);
        }
    }
}
