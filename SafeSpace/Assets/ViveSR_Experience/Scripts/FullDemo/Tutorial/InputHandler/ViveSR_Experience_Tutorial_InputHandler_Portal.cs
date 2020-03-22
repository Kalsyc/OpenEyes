using UnityEngine;
using System.Linq;
using System.Collections;

namespace Vive.Plugin.SR.Experience
{
    public class ViveSR_Experience_Tutorial_InputHandler_Portal : ViveSR_Experience_Tutorial_IInputHandler
    {
        ViveSR_Experience_DartGeneratorMgr dartGeneratorMgr_portal;
        ViveSR_Experience_IDartGenerator DartGenerator;
        protected override void StartToDo()
        {
            Button = ViveSR_Experience_Demo.instance.ButtonScripts[MenuButton.Portal];
            dartGeneratorMgr_portal = ViveSR_Experience_Demo.instance.DartGeneratorMgrs[DartGeneratorIndex.ForPortal];
        }

        public override void TriggerDown()
        {
            base.TriggerDown();
            SetTrigger();
        }
        public override void TriggerUp()
        {
            base.TriggerUp();
            SetTrigger();
        }

        void SetTrigger()
        {
            SetTriggerMessage(tutorial.isTriggerPressed);

            tutorial.SetTouchpadSprite(!tutorial.isTriggerPressed, ControllerInputIndex.mid);
            tutorial.SetTouchpadSprite(true, tutorial.isTriggerPressed, ControllerInputIndex.left, ControllerInputIndex.right);
        }

        protected override void MidPressedDown()
        {
            base.MidPressedDown();
            tutorial.SetCanvas(TextCanvas.onTrigger, Button.isOn);
            tutorial.SetTouchpadSprite(Button.isOn, Button.isOn, ControllerInputIndex.up, ControllerInputIndex.down);
        }

        void SetTriggerMessage(bool isTriggerDown)
        {
            if(!DartGenerator) 
                DartGenerator = dartGeneratorMgr_portal.DartGenerators[dartGeneratorMgr_portal.dartPlacementMode];

            string targetLine = "";
            if (isTriggerDown)
            {
                if (DartGenerator.isActiveAndEnabled)
                {
                    if (DartGenerator.currentDartPrefeb == 2) targetLine = "Trigger(Sword)";
                    else if (DartGenerator.currentDartPrefeb == 0) targetLine = "Trigger(Sphere)";
                    else if (DartGenerator.currentDartPrefeb == 1) targetLine = "Trigger(ViveDeer)";

                    ViveSR_Experience_IButton CurrentButton = ViveSR_Experience_Demo.instance.Rotator.CurrentButton;

                    tutorial.SetCanvasText(TextCanvas.onRotator, tutorial.MainLineManagers[CurrentButton.ButtonType].mainLines.First(x => x.messageType == targetLine).text);
                }
            }
            else
            {
                tutorial.SetMainMessage();
            }
        }
    }
}