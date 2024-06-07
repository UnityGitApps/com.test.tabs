using UnityEngine;

public class CustomTabsPlugin : MonoBehaviour
{
    private static AndroidJavaObject currentActivity;

    void Start()
    {
        if (currentActivity == null)
        {
            using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            {
                currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            }
        }
    }

    public void OpenCustomTab(string url)
    {
        if (currentActivity != null)
        {
            using (AndroidJavaClass customTabsHandler = new AndroidJavaClass("google.android.fileslibrary.CustomTabsHandler"))
            {
                customTabsHandler.CallStatic("openCustomTab", currentActivity, url);
            }
        }
    }
}