using System;
using System.Collections;
using System.Collections.Generic;
using GoogleMobileAds.Api;
using GoogleMobileAds.Ump.Api;
using UnityEngine;

public class ConsentGathering : MonoBehaviour
{
   ConsentForm _consentForm;
    // Start is called before the first frame update
    void Start()
    {
        var debugSettings = new ConsentDebugSettings
        {
            // Geography appears as in EEA for debug devices.
            DebugGeography = DebugGeography.EEA,
            TestDeviceHashedIds = new List<string>
            {
                "965E4A26737DF85475A353251709C315"
            }
        };

        // Here false means users are not under age.
        ConsentRequestParameters request = new ConsentRequestParameters
        {
            TagForUnderAgeOfConsent = false,
            ConsentDebugSettings = debugSettings,
        };

        // Check the current consent information status.
        ConsentInformation.Update(request, OnConsentInfoUpdated);
    }

    void OnConsentInfoUpdated(FormError error)
    {
        if (error != null)
        {
            // Handle the error.
            UnityEngine.Debug.LogError(error);
            Debug.Log($"load consent form get this error {error}");
            return;
        }

        if (ConsentInformation.IsConsentFormAvailable())
        {
            LoadConsentForm();
        }
        // If the error is null, the consent information state was updated.
        // You are now ready to check if a form is available.
    }

    void LoadConsentForm()
    {
        // Loads a consent form.
        ConsentForm.Load(OnLoadConsentForm);
    }

    void OnLoadConsentForm(ConsentForm consentForm, FormError error)
    {
        if (error != null)
        {
            // Handle the error.
            UnityEngine.Debug.LogError(error);
            return;
        }

        // The consent form was loaded.
        // Save the consent form for future requests.
        _consentForm = consentForm;

        // You are now ready to show the form.
        if (ConsentInformation.ConsentStatus == ConsentStatus.Required)
        {
            _consentForm.Show(OnShowForm);
        }
    }


    void OnShowForm(FormError error)
    {
        if (error != null)
        {
            // Handle the error.
            UnityEngine.Debug.LogError(error);
            return;
        }

        // Handle dismissal by reloading form.
        LoadConsentForm();
    }

    // Update is called once per frame
    public void Reset()
    {
        ConsentInformation.Reset();
    }
}
