using Firebase.Analytics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBaseSetting : MonoBehaviour
{
    public static FireBaseSetting fb;
    private Firebase.FirebaseApp app;
    // Start is called before the first frame update
    private void Awake()
    {
        if(fb == null)
        {
            fb = this;
        }
        DontDestroyOnLoad(this);
    }

    void Start()
    {
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                // Create and hold a reference to your FirebaseApp,
                // where app is a Firebase.FirebaseApp property of your application class.
                app = Firebase.FirebaseApp.DefaultInstance;
                Debug.Log("-------->FireBase is Running<-----------");
                // Set a flag here to indicate whether Firebase is ready to use by your app.
            }
            else
            {
                UnityEngine.Debug.LogError(System.String.Format(
                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                // Firebase Unity SDK is not safe to use here.
            }
        });
    }
    public void CustomEventManager(string eventName, string parameterName, long parameterValue)
    {
        FirebaseAnalytics.LogEvent(eventName, new Parameter(parameterName, parameterValue));
        Debug.Log(eventName);
    }

    public void CustomEventManager(string eventName, string parameterName, double parameterValue)
    {
        FirebaseAnalytics.LogEvent(eventName, new Parameter(parameterName, parameterValue));
        Debug.Log(eventName);

    }

    //FirebaseAnalytics.LogEvent("GameplayStart", "Gameplay", "1");
}
