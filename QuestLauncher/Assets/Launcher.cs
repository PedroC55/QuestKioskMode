using UnityEngine;

public class Launcher : MonoBehaviour
{
    void Start()
    {
        LaunchApp();
    }

    void LaunchApp()
    {
        try
        {
            AndroidJavaClass up = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity = up.GetStatic<AndroidJavaObject>("currentActivity");

            AndroidJavaObject intent = new AndroidJavaObject("android.content.Intent");
            AndroidJavaObject componentName = new AndroidJavaObject(
                "android.content.ComponentName",
                "com.tdv.vrapp",
                "org.mozilla.vrbrowser.VRBrowserActivity"
            );
            intent.Call<AndroidJavaObject>("setComponent", componentName);
            intent.Call<AndroidJavaObject>("addFlags", 0x10000000); // FLAG_ACTIVITY_NEW_TASK

            currentActivity.Call("startActivity", intent);
            Debug.Log("LAUNCHER: 3DVista iniciado!");
        }
        catch (System.Exception e)
        {
            Debug.LogError("Erro: " + e.Message);
        }
    }
}