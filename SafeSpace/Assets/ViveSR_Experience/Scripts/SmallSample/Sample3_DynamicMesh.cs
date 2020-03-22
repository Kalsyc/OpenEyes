using UnityEngine;
using UnityEngine.UI;

namespace Vive.Plugin.SR.Experience
{
    [RequireComponent(typeof(ViveSR_Experience))]
    public class Sample3_DynamicMesh : MonoBehaviour
    {       
        protected Text LeftText, RightText, ThrowableText, DisplayMesh;
        GameObject TriggerCanvas;
        [SerializeField] ViveSR_Experience_IDartGenerator dartGenerator;

        ViveSR_Experience_DynamicMesh DynamicMeshScript;

        public void Init()
        {
            DynamicMeshScript = GetComponent<ViveSR_Experience_DynamicMesh>();

            GameObject attachPointCanvas = ViveSR_Experience.instance.AttachPoint.transform.GetChild(ViveSR_Experience.instance.AttachPointIndex).transform.gameObject;

            DisplayMesh = attachPointCanvas.transform.Find("TouchpadCanvas/DisplayText").GetComponent<Text>();
            LeftText = attachPointCanvas.transform.Find("TouchpadCanvas/LeftText").GetComponent<Text>();
            RightText = attachPointCanvas.transform.Find("TouchpadCanvas/RightText").GetComponent<Text>();
            ThrowableText = attachPointCanvas.transform.Find("TriggerCanvas/TriggerText").GetComponent<Text>();
            TriggerCanvas = attachPointCanvas.transform.Find("TriggerCanvas").gameObject;

            DynamicMeshScript.SetDynamicMesh(true);
            ViveSR_Experience_ControllerDelegate.triggerDelegate += HandleTrigger;
            ViveSR_Experience_ControllerDelegate.touchpadDelegate += HandleTouchpad;
        }

        void HandleTrigger(ButtonStage buttonStage, Vector2 axis)
        {
            switch (buttonStage)
            {
                case ButtonStage.PressDown:
                    LeftText.enabled = true;
                    RightText.enabled = true;
                    TriggerCanvas.SetActive(false);
                    break;
                case ButtonStage.PressUp:
                    LeftText.enabled = false;
                    RightText.enabled = false;
                    TriggerCanvas.SetActive(true);
                    break;
            }
        }
        void HandleTouchpad(ButtonStage buttonStage, Vector2 axis)
        {
            switch (buttonStage)
            {
                case ButtonStage.PressDown:

                    TouchpadDirection touchpadDirection = ViveSR_Experience_ControllerDelegate.GetTouchpadDirection(axis, false);

                    if (touchpadDirection == TouchpadDirection.Up)
                    {
                        DynamicMeshScript.SetMeshDisplay(!DynamicMeshScript.ShowDynamicCollision);
                        DisplayMesh.text = DisplayMesh.text == "[Show]" ? "[Hide]" : "[Show]";
                    }
                    else if (touchpadDirection == TouchpadDirection.Down)
                        dartGenerator.DestroyObjs();
                    break;
            }
        }
    }
}