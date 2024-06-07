using UnityEngine;

public class LoadPage : MonoBehaviour
{
    private CustomTabsPlugin customTabsPlugin;

    void Start()
    {
        customTabsPlugin = GetComponent<CustomTabsPlugin>();
    }

    public void OpenCustomTab()
    {
        customTabsPlugin.OpenCustomTab("https://convertio.co/ru/image-converter/");
    }
}
