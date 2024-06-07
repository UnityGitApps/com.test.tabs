using UnityEngine;

public class CustomTabsPlugin : MonoBehaviour
{
    public void OpenCustomTab(string url)
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            {
                AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

                using (AndroidJavaClass customTabsIntentBuilderClass = new AndroidJavaClass("androidx.browser.customtabs.CustomTabsIntent$Builder"))
                {
                    AndroidJavaObject customTabsIntentBuilder = customTabsIntentBuilderClass.Call<AndroidJavaObject>("<init>");
                    AndroidJavaObject customTabsIntent = customTabsIntentBuilder.Call<AndroidJavaObject>("build");

                    using (AndroidJavaObject uri = new AndroidJavaObject("android.net.Uri", "parse", url))
                    {
                        customTabsIntent.Call("launchUrl", currentActivity, uri);
                    }
                }
            }
        }
        else
        {
            Debug.Log("Custom Tabs are only supported on Android.");
        }
    }
}