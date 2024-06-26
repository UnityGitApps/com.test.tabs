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
                    // ��������� �������������� ��������� ��� ����������� ��������� ������ URL
                    customTabsIntentBuilder.Call<AndroidJavaObject>("addDefaultShareMenuItem");

                    // ����������� ���� ������ ������������
                    int color = new AndroidJavaClass("android.graphics.Color").CallStatic<int>("parseColor", "#000000"); // ������ ����
                    customTabsIntentBuilder.Call<AndroidJavaObject>("setToolbarColor", color);

                    // �������� ������ ������ URL (���� ��������)
                    customTabsIntentBuilder.Call<AndroidJavaObject>("setUrlBarHidingEnabled", true);

                    // ����������� �������� ��������
                    AndroidJavaObject resources = currentActivity.Call<AndroidJavaObject>("getResources");
                    int enterAnimation = resources.Call<int>("getIdentifier", "fade_in", "anim", currentActivity.Call<string>("getPackageName"));
                    int exitAnimation = resources.Call<int>("getIdentifier", "fade_out", "anim", currentActivity.Call<string>("getPackageName"));
                    customTabsIntentBuilder.Call<AndroidJavaObject>("setExitAnimations", currentActivity, enterAnimation, exitAnimation);

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