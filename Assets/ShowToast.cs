using UnityEngine;

public class ToastMessageScript : MonoBehaviour
{
    string toastString;
    readonly string input;
    AndroidJavaObject currentActivity;
    AndroidJavaClass UnityPlayer;
    AndroidJavaObject context;

    void Start()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            UnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            currentActivity = UnityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            context = currentActivity.Call<AndroidJavaObject>("getApplicationContext");
        }
    }


    public void ShowToast(string toastString)
    {
        this.toastString = toastString;
        currentActivity.Call("runOnUiThread", new AndroidJavaRunnable(Toast));
    }

    void Toast()
    {
        Debug.Log(this + ": Running on UI thread");

        AndroidJavaClass Toast = new AndroidJavaClass("android.widget.Toast");
        AndroidJavaObject javaString = new AndroidJavaObject("java.lang.String", toastString);
        AndroidJavaObject toast = Toast.CallStatic<AndroidJavaObject>("makeText", context, javaString, Toast.GetStatic<int>("LENGTH_LONG"));
        toast.Call("show");
    }
}
