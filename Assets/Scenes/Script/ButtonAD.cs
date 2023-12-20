using System.Collections;
using System.Collections.Generic;
using GoogleMobileAds.Ump.Api;
using UnityEngine;
using UnityEngine.UI;

public class ButtonAD : MonoBehaviour
{
    [SerializeField, Tooltip("Button to show the privacy options form.")]
    private Button _privacyButton;

    private void Start()
    {
        // Enable the privacy settings button.
        if (_privacyButton != null)
        {
            _privacyButton.onClick.AddListener(ShowPrivacyOptionsForm);
            // Disable the privacy settings button by default.
            _privacyButton.interactable = false;
        }
    }

 

    /// <summary>
    /// Shows the privacy options form to the user.
    /// </summary>
    public void ShowPrivacyOptionsForm()
    {
        Debug.Log("Showing privacy options form.");

        ConsentForm.ShowPrivacyOptionsForm((FormError showError) =>
        {
            if (showError != null)
            {
                Debug.LogError("Error showing privacy options form with error: " + showError.Message);
            }
            // Enable the privacy settings button.
            if (_privacyButton != null)
            {
                _privacyButton.interactable =
                    ConsentInformation.PrivacyOptionsRequirementStatus ==
                    PrivacyOptionsRequirementStatus.Required;
            }
        });
    }
}
