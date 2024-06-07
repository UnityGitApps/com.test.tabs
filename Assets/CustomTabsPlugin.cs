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

                // Создаем экземпляр CustomTabsIntent.Builder через конструктор
                using (AndroidJavaObject customTabsIntentBuilder = new AndroidJavaObject("androidx.browser.customtabs.CustomTabsIntent$Builder"))
                {
                    // Добавляем дополнительные параметры для минимизации видимости строки URL
                    customTabsIntentBuilder.Call<AndroidJavaObject>("addDefaultShareMenuItem");

                    // Настраиваем цвет панели инструментов
                    int color = new AndroidJavaClass("android.graphics.Color").CallStatic<int>("parseColor", "#000000"); // Черный цвет
                    customTabsIntentBuilder.Call<AndroidJavaObject>("setToolbarColor", color);

                    // Пытаемся скрыть строку URL (если возможно)
                    customTabsIntentBuilder.Call<AndroidJavaObject>("setUrlBarHidingEnabled", true);

                    // Настраиваем анимации перехода (опционально, чтобы сделать переход более плавным)
                    int enterAnimation = currentActivity.Get<int>("android.R.anim.fade_in");
                    int exitAnimation = currentActivity.Get<int>("android.R.anim.fade_out");
                    customTabsIntentBuilder.Call<AndroidJavaObject>("setExitAnimations", currentActivity, enterAnimation, exitAnimation);

                    AndroidJavaObject customTabsIntent = customTabsIntentBuilder.Call<AndroidJavaObject>("build");

                    // Создаем URI
                    using (AndroidJavaObject uri = new AndroidJavaClass("android.net.Uri").CallStatic<AndroidJavaObject>("parse", url))
                    {
                        // Запускаем Custom Tab
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