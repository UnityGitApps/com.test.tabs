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

                // ������� ��������� CustomTabsIntent.Builder ����� �����������
                using (AndroidJavaObject customTabsIntentBuilder = new AndroidJavaObject("androidx.browser.customtabs.CustomTabsIntent$Builder"))
                {
                    AndroidJavaObject customTabsIntent = customTabsIntentBuilder.Call<AndroidJavaObject>("build");

                    // ������� URI
                    using (AndroidJavaObject uri = new AndroidJavaClass("android.net.Uri").CallStatic<AndroidJavaObject>("parse", url))
                    {
                        // ��������� Custom Tab
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