using UnityEngine;
using UnityEngine.UI;

namespace Vive.Plugin.SR.Experience
{
    public class Sample2_DepthImage : MonoBehaviour
    {
        [SerializeField] ViveSR_Experience_DepthControl DepthControlScript;
                                                    
        public void Init()
        {
            PlayerHandUILaserPointer.CreateLaserPointer();
            ViveSR_Experience_ControllerDelegate.touchpadDelegate += HandleTouchpad;
            ViveSR_Experience_ControllerDelegate.triggerDelegate += HandleTrigger;

            DepthControlScript.gameObject.SetActive(true);
        }

        void HandleTrigger(ButtonStage buttonStage, Vector2 axis)
        {
            switch (buttonStage)
            {
                case ButtonStage.PressDown:                
                    ViveSR_Experience_ControllerDelegate.touchpadDelegate -= HandleTouchpad;
                    break;
                case ButtonStage.PressUp:
                    ViveSR_Experience_ControllerDelegate.touchpadDelegate += HandleTouchpad;
                    break;
            }
        }

        void HandleTouchpad(ButtonStage buttonStage, Vector2 axis)
        {
            switch (buttonStage)
            {
                case ButtonStage.Press:
                    DepthControlScript.ResetPanelPos();
                    break;
            }
        }

        private void OnApplicationQuit()
        {
            DepthControlScript.LoadDefaultValue();
        }
    }
}