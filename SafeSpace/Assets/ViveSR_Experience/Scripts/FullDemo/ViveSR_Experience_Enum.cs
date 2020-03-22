namespace Vive.Plugin.SR.Experience
{
    public enum DeviceType
    {
        NotSupport,
        Vive,
        ViveT2
    }              

    public enum TextCanvas
    {
        onTouchPad = 0,
        onRotator = 1,
        onTrigger,
        onGrip
    }

    public enum MenuButton
    {
        DepthControl = 0,
        _3DPreview = 1,
        EnableMesh = 2,
        Segmentation = 3,
        Portal = 4,
        Effects = 5,
        CameraControl = 6,
        Calibration = 7,
        MaxNum
    }

    public enum SubMenuButton
    {
        _3DPreview_Save,
        _3DPreview_Scan,
        Calibration_Alignment,
        Calibration_Focus,
        Calibration_Reset,
        EnableMesh_StaticMR,
        EnableMesh_StaticVR,
        EnableMesh_Dynamic
    }

    public enum DartGeneratorIndex
    {
        ForStatic,
        ForDynamic,
        ForPortal,
        MaxNum
    }

    public enum ColorType
    {
        Original = 0,
        Bright,
        Disable,
        Attention
    }

    public enum Calibration_SubBtn
    {
        Focus = 0,
        Alignment = 1,
        Reset = 2,
        MaxNum,
    }

    public enum _3DPreview_SubBtn
    {
        Scan = 0,
        Save = 1,
        MaxNum,
    }

    public enum EnableMesh_SubBtn
    {
        StaticMR = 0,
        StaticVR = 1,
        Dynamic = 2,
        MaxNum
    }

    public enum StageIndex
    {
        None,
        Test,
        Demo,     
        MaxNum
    }

    public enum SceneType
    {
        None,
        Demo,
        Sample1,
        Sample2,
        Sample3,
        Sample4,
        Sample5,
        Sample6,
        Sample7,
        Sample8,
        Sample9,
    }
}