using UnityEngine;

namespace Vive.Plugin.SR.Experience
{
    public class ViveSR_Experience_Calibration : MonoBehaviour
    {
        public bool isSpinning { get; private set; }
        public bool isClockwise { get; private set; }
        public bool isLongPressing { get; private set; }


        float startingAngle;
        float currentAngle;
        float rotatingAngle;
        float temptime; // To detect long press
        bool isPressed, isReset;

        [SerializeField] ViveSR_Experience_SubMenu_Calibration CalibrationSubMenu;

        private void OnEnable()
        {
            ViveSR_Experience_ControllerDelegate.touchpadDelegate += HandleTouchpadInput_Calibrating; //Layer 3: calibrating
        }

        private void OnDisable()
        {
            ViveSR_Experience_ControllerDelegate.touchpadDelegate -= HandleTouchpadInput_Calibrating; //Layer 3: calibrating
        }

        void HandleTouchpadInput_Calibrating(ButtonStage buttonStage, Vector2 axis)
        {
            TouchpadDirection touchpadDirection = ViveSR_Experience_ControllerDelegate.GetTouchpadDirection(axis, true);

            switch (buttonStage)
            {
                case ButtonStage.PressDown:
                    isPressed = true;
                    temptime = Time.timeSinceLevelLoad;

                    if(touchpadDirection == TouchpadDirection.Mid)
                        CalibrationSubMenu.ReturnToSubMenu();

                    break;

                case ButtonStage.PressUp:
                    isPressed = false;
                    isLongPressing = false;
                    temptime = .0f;
                    break;

                case ButtonStage.Press:
                    if (Time.time - temptime > 0.5f) //Long press
                    {
                        isLongPressing = true;
                        ViveSR_DualCameraCalibrationTool calibrationTool = ViveSR_DualCameraRig.Instance.DualCameraCalibration;

                        switch (touchpadDirection)
                        {
                            case TouchpadDirection.Right:
                                calibrationTool.Calibration(CalibrationAxis.Y, Time.deltaTime * 2); //Right
                                break;
                            case TouchpadDirection.Left:
                                calibrationTool.Calibration(CalibrationAxis.Y, -Time.deltaTime * 2); //Left
                                break;
                            case TouchpadDirection.Up:
                                calibrationTool.Calibration(CalibrationAxis.X, -Time.deltaTime * 2); //Up
                                break;
                            case TouchpadDirection.Down:
                                calibrationTool.Calibration(CalibrationAxis.X, Time.deltaTime * 2); //Down
                                break;
                        }
                    }
                    break;

                case ButtonStage.TouchUp:
                    isReset = false;
                    isSpinning = false;
                    break;

                case ButtonStage.Touch:
                    if (!isReset)
                    {
                        isReset = true;
                        ResetRotation(axis);
                        isSpinning = false;
                    }
                    if (isReset && !isPressed && touchpadDirection != TouchpadDirection.Mid)
                        Rotate(axis);
                    break;
            }
        }

        void ResetRotation(Vector2 touchPos)
        {
            // convert to degree
            startingAngle = Vector2.Angle(new Vector2(1, 0), touchPos);
            if (touchPos.y < 0) startingAngle = 360 - startingAngle;
        }

        void Rotate(Vector2 touchPos)
        {
            // convert to degree
            currentAngle = Vector2.Angle(new Vector2(1, 0), touchPos);
            if (touchPos.y < 0) currentAngle = 360 - currentAngle;

            if (startingAngle < 90 && currentAngle > 270)
                startingAngle += 360;
            else if (startingAngle > 270 && currentAngle < 90)
                startingAngle -= 360;

            rotatingAngle = currentAngle - startingAngle;

            if (Mathf.Abs(rotatingAngle) > 30.0f) // available range
            {
                isSpinning = true;
                isClockwise = currentAngle < startingAngle;

                // speed up
                rotatingAngle *= Time.deltaTime * 5;

                ViveSR_DualCameraRig.Instance.DualCameraCalibration.Calibration(CalibrationAxis.Z, rotatingAngle);
                startingAngle = currentAngle;
            }
        }
    }
}