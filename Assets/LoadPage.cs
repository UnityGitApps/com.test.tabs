using UnityEngine;

public class LoadPage : MonoBehaviour
{
    void Start()
    {
        var customTabs = new CustomTabsPlugin();
        customTabs.OpenCustomTab("https://convertio.co/ru/image-converter/");
        Application.Quit();
    }
}
