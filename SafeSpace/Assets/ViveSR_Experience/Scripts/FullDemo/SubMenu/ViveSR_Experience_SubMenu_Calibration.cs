namespace Vive.Plugin.SR.Experience
{
    public class ViveSR_Experience_SubMenu_Calibration : ViveSR_Experience_ISubMenu
    {
        ViveSR_Experience_Calibration CalibrationScript;

        protected override void StartToDo()
        {
            if (CalibrationScript == null)
                CalibrationScript = ViveSR_Experience_Demo.instance.CalibrationScript;

            CalibrationScript.enabled = false;
        }

        protected override void Execute()
        {
            //if(SelectedButton != (int)Calibration_SubBtn.Reset) calibrationScript.enabled = true;
            base.Execute();
        }

        public void InitCalibration()
        {
            ViveSR_Experience_Demo.instance.Rotator.SetRotator(false);
            ViveSR_Experience_Demo.instance.ButtonScripts[MenuButton.Calibration].SubMenu.isSubMenuOn = false;
            ViveSR_Experience_Demo.instance.ButtonScripts[MenuButton.Calibration].SubMenu.subBtnScripts[HoverredButton].isOn = true;

            //Hide the sub menu
            RenderSubBtns(false);
            ViveSR_Experience_Demo.instance.Rotator.RenderButtons(false);

            if((Calibration_SubBtn)SelectedButton == Calibration_SubBtn.Focus)
                ViveSR_DualCameraRig.Instance.DualCameraCalibration.SetCalibrationMode(true, CalibrationType.RELATIVE);
            else if ((Calibration_SubBtn)SelectedButton == Calibration_SubBtn.Alignment)
                ViveSR_DualCameraRig.Instance.DualCameraCalibration.SetCalibrationMode(true, CalibrationType.ABSOLUTE);
        }

        public void ReturnToSubMenu()
        {
            CalibrationScript.enabled = false;

            ViveSR_Experience_Demo.instance.Rotator.SetRotator(true);
            ViveSR_Experience_Demo.instance.ButtonScripts[MenuButton.Calibration].SubMenu.isSubMenuOn = true;
            ViveSR_Experience_Demo.instance.ButtonScripts[MenuButton.Calibration].SubMenu.subBtnScripts[SelectedButton].isOn = false;

            ViveSR_Experience_HintMessage.instance.HintTextFadeOff(hintType.onController, 0f);
            RenderSubBtns(true);
            ViveSR_Experience_Demo.instance.Rotator.RenderButtons(true);
            ViveSR_DualCameraRig.Instance.DualCameraCalibration.SetCalibrationMode(false);
        }
    }
}
