using UnityEngine;
namespace Vive.Plugin.SR.Experience
{
    public class ViveSR_Experience_SubBtn_Calibration_Alignment : ViveSR_Experience_ISubBtn
    {
        [SerializeField] ViveSR_Experience_SubMenu_Calibration CalibrationSubMenu;

        [SerializeField] Calibration_SubBtn SubBtnType;

        protected override void AwakeToDo()
        {
            ThisButtonTypeNum = (int)SubBtnType;
        }

        public override void Execute()
        {
            CalibrationSubMenu.InitCalibration();
            ViveSR_Experience_Demo.instance.CalibrationScript.enabled = true;
            ViveSR_Experience_HintMessage.instance.SetHintMessage(hintType.onController, "[Calibration]\nAlignment Mode", false);
        }
    }
}