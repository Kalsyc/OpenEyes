using UnityEngine;using UnityEngine.UI;using System.Linq;namespace Vive.Plugin.SR.Experience{    public class ViveSR_Experience_Tutorial_InputHandler_Calibration : ViveSR_Experience_Tutorial_IInputHandler    {
        ViveSR_Experience_Calibration CalibrationScript;

        bool clockwise = false;

        protected override void StartToDo()
        {
            Button = ViveSR_Experience_Demo.instance.ButtonScripts[MenuButton.Calibration];
            if (CalibrationScript == null)
                CalibrationScript = ViveSR_Experience_Demo.instance.CalibrationScript;

        }

        public override void SetTouchpadText(Vector2 touchpad)        {
            // Run button animation and set default canvas text
            base.SetTouchpadText(touchpad); 
            tutorial.SetCanvas(TextCanvas.onTouchPad, true);

            if (CalibrationScript.isActiveAndEnabled)
            {
                if (CalibrationScript.isSpinning)
                {
                    tutorial.SetTouchpadImage(false);
                    Spin();
                }
                else if (CalibrationScript.isLongPressing)
                {
                    HideSpinnerImages();
                    tutorial.SetTouchpadImage(true);
                }
                else
                {
                    if (tutorial.currentInput != ControllerInputIndex.mid)
                    {
                        if (SubMenu.SelectedButton == (int)Calibration_SubBtn.Focus)
                            tutorial.SetCanvasText(TextCanvas.onTouchPad, "[Spin] or [Long Press] to adjust left camera plane");
                        else if (SubMenu.SelectedButton == (int)Calibration_SubBtn.Alignment)
                            tutorial.SetCanvasText(TextCanvas.onTouchPad, "[Spin] or [Long Press] to adjust camera planes");
                    }
                }
            }
        }

        public override void ResetTouchpadSprite()
        {
            if (CalibrationScript.isActiveAndEnabled)
            {
                ResetTouchpadImagesAndHideSpinnerImages();
            }
            base.ResetTouchpadSprite();
        }        protected override void LeftRightPressedDown()
        {
            if (!CalibrationScript.isActiveAndEnabled)
            {
                base.LeftRightPressedDown();
            }
        }        public override void ConfirmSelection()
        {
            if (tutorial.currentInput == ControllerInputIndex.left || tutorial.currentInput == ControllerInputIndex.right)
            {
                if (!CalibrationScript.isActiveAndEnabled)
                {
                    base.ConfirmSelection();
                }
            }
            else if (tutorial.currentInput == ControllerInputIndex.up || tutorial.currentInput == ControllerInputIndex.down)
            {
                if (!CalibrationScript.isActiveAndEnabled)
                {
                    tutorial.SetCanvas(TextCanvas.onRotator, true);

                    //Set rotator message for calibration
                    ViveSR_Experience_IButton CurrentButton = ViveSR_Experience_Demo.instance.Rotator.CurrentButton;
                    tutorial.SetCanvasText(TextCanvas.onRotator, tutorial.SubLineManagers[CurrentButton.ButtonType].SubBtns[SubMenu.HoverredButton].lines.First(x => x.messageType == "Available").text);
                }
            }
            else if (tutorial.currentInput == ControllerInputIndex.mid)
            {                                       
                MidPressedDown();
            }
        }

        protected override void MidPressedDown()
        {
            ResetTouchpadImagesAndHideSpinnerImages();
            if (CalibrationScript.isActiveAndEnabled)
            {
                tutorial.SetCanvas(TextCanvas.onRotator, false);
                tutorial.SetTouchpadSprite(true, true, ControllerInputIndex.left, ControllerInputIndex.right, ControllerInputIndex.up, ControllerInputIndex.down, ControllerInputIndex.mid);
            }
            else
            {
                tutorial.SetCanvas(TextCanvas.onRotator, true);
                tutorial.SetTouchpadSprite(true, false, ControllerInputIndex.left, ControllerInputIndex.right, ControllerInputIndex.up, ControllerInputIndex.down, ControllerInputIndex.mid);
                base.MidPressedDown();
            }
        }

        void ResetTouchpadImagesAndHideSpinnerImages()
        {
            tutorial.SetTouchpadImage(true);
            tutorial.ResetTouchpadImageSprite();
            tutorial.SetCanvas(TextCanvas.onTouchPad, false);
            HideSpinnerImages();
        }

        void HideSpinnerImages()
        {
            foreach (Image img in tutorial.spinngerImage)
                img.enabled = false;
        }

        void Spin()
        {
            clockwise = CalibrationScript.isClockwise;
            
            tutorial.spinngerImage[clockwise ? 0 : 1].gameObject.transform.localEulerAngles += new Vector3(0f, 0f, clockwise ? -0.8f : 0.8f);
            tutorial.SetCanvasText(TextCanvas.onTouchPad, clockwise ? "[Spin] Rotate Clockwise" : "[Spin] Rotate Counter Clockwise");
            tutorial.spinngerImage[0].enabled = clockwise;
            tutorial.spinngerImage[1].enabled = !clockwise;
        }
    }}