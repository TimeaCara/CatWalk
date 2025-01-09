/* 
*   Pedometer
*   Copyright (c) 2018 Yusuf Olokoba
*/

namespace PedometerU.Tests
{
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;

    public class StepCounter : MonoBehaviour
    {

        public TMP_Text stepText, distanceText;
        private Pedometer pedometer;
        private int _steps;

        private void Start()
        {
            // Create a new pedometer
            pedometer = new Pedometer(OnStep);

            // Reset UI
            OnStep(_steps, 0);
            Debug.Log("UNITYYYYYYYYYY " + Pedometer.Implementation.IsSupported);
            Debug.Log("UNITYYYYYYYYYY Stored step:" + _steps);
            stepText.text = _steps.ToString();
        }


        private void OnStep(int steps, double distance)
        {
            MoneyManagerHandle.Instance.IncreaseBy(steps - _steps, true);

            // Display the values // Distance in feet
            _steps = steps;
            stepText.text = _steps.ToString();
            distanceText.text = (distance * 3.28084).ToString("F2") + " ft";
            Debug.Log(stepText.text);
        }

        private void OnDisable()
        {
            // Release the pedometer
            pedometer.Dispose();
            pedometer = null;
        }

    }
}