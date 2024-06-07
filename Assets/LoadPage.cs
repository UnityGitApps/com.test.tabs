using UnityEngine;

public class LoadPage : MonoBehaviour
{
    void Start()
    {
        var customTabs = new CustomTabsPlugin();
        customTabs.OpenCustomTab("https://www.google.com");
    }
}
