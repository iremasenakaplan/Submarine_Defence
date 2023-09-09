using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_ANDROID
    using Google.Play.AppUpdate;
    using Google.Play.Common;
#endif

using TMPro;

public class UpdateManager : MonoBehaviour
{
    #if UNITY_ANDROID
     //[SerializeField] private TMP_Text inAppStatus;
     //[SerializeField] private GameObject inAppUpdatePanel;
    AppUpdateManager appUpdateManager;
  
    // Start is called before the first frame update
    void Start()
    {
        appUpdateManager = new AppUpdateManager();
        StartCoroutine(CheckForUpdate());

    }

    private IEnumerator CheckForUpdate()
    {
        PlayAsyncOperation<AppUpdateInfo, AppUpdateErrorCode> appUpdateInfoOperation =
            appUpdateManager.GetAppUpdateInfo();

        // wait until the asynchronous operation completes.
        yield return appUpdateInfoOperation;

        if (appUpdateInfoOperation.IsSuccessful)
        {
            var appUpdateInforResult = appUpdateInfoOperation.GetResult();
            // Check AppUpdateInfo's UpdateAvailability, UpdatePriority,
            // IsUpdateTypeAllowed(), etc. and decide whether to ask the user
            // to start an in-app update.

            //display if there is an update or not
            if (appUpdateInforResult.UpdateAvailability == UpdateAvailability.UpdateAvailable)
            {
                //inAppStatus.text = UpdateAvailability.UpdateAvailable.ToString();   
                //inAppUpdatePanel.SetActive(true);  
                var appUpdateOptions = AppUpdateOptions.ImmediateAppUpdateOptions();
                StartCoroutine(StartImmediateUpdate(appUpdateInforResult, appUpdateOptions));         
            }
            else
            {
                //inAppStatus.text = "No Update Available";
            }

            // Creates an AppUpdateOptions defining an immediate in-app
            // update flow and its parameters.
        }
    }

    IEnumerator StartImmediateUpdate(AppUpdateInfo appUpdateInfoOp_i, AppUpdateOptions appUpdateOptions_i)
    {
        // Creates an AppUpdateRequest that can be used to monitor the
        // requested in-app update flow.
        var startUpdateRequest = appUpdateManager.StartUpdate(
            // The result returned by PlayAsyncOperation.GetResult().
            appUpdateInfoOp_i,
                      // The AppUpdateOptions created defining the requested in-app update
                      // and its parameters.
                      appUpdateOptions_i
            );
        yield return startUpdateRequest;

        // If the update completes successfully, then the app restarts and this line
        // is never reached. If this line is reached, then handle the failure (for
        // example, by logging result.Error or by displaying a message to the user).
    }

    #endif

}
