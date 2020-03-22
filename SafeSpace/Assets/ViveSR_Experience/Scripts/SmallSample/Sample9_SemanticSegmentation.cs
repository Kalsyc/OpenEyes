using UnityEngine;
using UnityEngine.UI;

namespace Vive.Plugin.SR.Experience
{
    [RequireComponent(typeof(ViveSR_Experience))]
    public class Sample9_SemanticSegmentation : MonoBehaviour
    {
        Text ScanText, StopText, SaveText, LoadText, HintText;
        GameObject GripCanvas;

        ViveSR_Experience_StaticMesh StaticMeshScript;

        [SerializeField]  protected Color BrightColor, OriginalColor;

        int currentShowing = 0;
        //0 = all

        public void Init()
        {
            StaticMeshScript = FindObjectOfType<ViveSR_Experience_StaticMesh>();
            Init_Start();
        }

        private void Update()
        {
            ViveSR_SceneUnderstanding.SetIconLookAtPlayer(ViveSR_Experience.instance.PlayerHeadCollision.transform);
        }

        public void Init_Start()
        {
            GameObject attachPointCanvas = ViveSR_Experience.instance.AttachPoint.transform.GetChild(ViveSR_Experience.instance.AttachPointIndex).transform.gameObject;
            ScanText = attachPointCanvas.transform.Find("TouchpadCanvas/ScanText").GetComponent<Text>();
            StopText = attachPointCanvas.transform.Find("TouchpadCanvas/StopText").GetComponent<Text>();
            SaveText = attachPointCanvas.transform.Find("TouchpadCanvas/SaveText").GetComponent<Text>();
            LoadText = attachPointCanvas.transform.Find("TouchpadCanvas/LoadText").GetComponent<Text>();
            HintText = attachPointCanvas.transform.Find("HintText").GetComponent<Text>();

            GripCanvas = attachPointCanvas.transform.Find("GripCanvas").gameObject;
            GripCanvas.SetActive(false);

            LoadText.color = StaticMeshScript.CheckSemanticMeshDirExist() ? BrightColor : OriginalColor;

            ViveSR_RigidReconstructionRenderer.LiveMeshDisplayMode = ReconstructionDisplayMode.ADAPTIVE_MESH;
            ViveSR_Experience_ControllerDelegate.touchpadDelegate += HandleTouchpad_MeshOperation;

        }

        #region Mesh Control
        private void HandleTouchpad_MeshOperation(ButtonStage buttonStage, Vector2 axis)
        {
            switch (buttonStage)
            {
                case ButtonStage.PressDown:
                    TouchpadDirection touchpadDirection = ViveSR_Experience_ControllerDelegate.GetTouchpadDirection(axis, false);
                    HandleTouchpad_MeshOperation_PressDown(touchpadDirection);
                    break;
            }
        }

        void HandleTouchpad_MeshOperation_PressDown(TouchpadDirection touchpadDirection)
        {                                                                                        
            switch (touchpadDirection)
            {
                case TouchpadDirection.Up: Scan(); break;
                case TouchpadDirection.Left: StopAndReset(); break;
                case TouchpadDirection.Right: Save(); break;
                case TouchpadDirection.Down: Load(); break;
            }
        }

        void Scan()
        {
            bool allowScan = !ViveSR_RigidReconstruction.IsScanning
                && !ViveSR_RigidReconstruction.IsExportingMesh
                && !StaticMeshScript.SemanticMeshIsExporting
                && !StaticMeshScript.ModelIsLoading
                && !StaticMeshScript.SemanticMeshIsLoading;

            if (!allowScan) return;
            ViveSR_Experience_ControllerDelegate.gripDelegate -= HandleGrip_SemanticObjOperation;
            GripCanvas.SetActive(false);

            ViveSR_SceneUnderstanding.DestroySceneObjects();
            StaticMeshScript.LoadMesh(false);
            StaticMeshScript.ActivateSemanticMesh(false);

            HintText.text = "Scanning...";

            RigidReconstruction.SRWork_Rigid_Reconstruciton.RegisterDataErrorHandler((int)Error.GPU_MEMORY_FULL, GPUMemoryFull);
            StaticMeshScript.EnableDepthProcessingAndScanning(true);
            StaticMeshScript.SetSegmentation(true);
            LoadText.color = OriginalColor;
            ScanText.color = OriginalColor;
            SaveText.color = BrightColor;
            StopText.color = BrightColor;
        } 
        void Stop()
        {
            bool allowStop = ViveSR_RigidReconstruction.IsScanning;

            if (!allowStop) return;

            StaticMeshScript.EnableDepthProcessingAndScanning(false);
            StaticMeshScript.SetSegmentation(false);

            HintText.text = "Static Mesh";
            StopText.color = OriginalColor;
            SaveText.color = OriginalColor;
            LoadText.color = LoadText.color = StaticMeshScript.CheckSemanticMeshDirExist() ? BrightColor : OriginalColor;
            ScanText.color = BrightColor;
        } 
        void StopAndReset()
        {
            if (!ViveSR_RigidReconstruction.IsScanning) return;

            Stop();
            StaticMeshScript.ResetScannedData();
        }

        void Save()
        {
            bool allowSave = ViveSR_RigidReconstruction.IsScanning
                && !ViveSR_RigidReconstruction.IsExportingMesh
                && !StaticMeshScript.SemanticMeshIsExporting;

            if (!allowSave) return;

            LoadText.color = OriginalColor;
            ScanText.color = OriginalColor;
            StopText.color = OriginalColor;
            SaveText.color = OriginalColor;

            StaticMeshScript.SetSegmentation(false);
            ViveSR_SceneUnderstanding.SetAllCustomSceneUnderstandingConfig(10, true);

            StaticMeshScript.ExportSemanticMesh(Save_UpdatePercentage, Save_done);
        }
        void Save_UpdatePercentage(int percentage)
        {
            HintText.text = "Saving Objects...\n" + percentage + "%";
        }
        void Save_done()
        {
            HintText.text = "Mesh Saved!";
            ScanText.color = BrightColor;
            LoadText.color = BrightColor;
        }

        void Load()
        {
            bool allowLoad = !ViveSR_RigidReconstruction.IsScanning
                          && !ViveSR_RigidReconstruction.IsExportingMesh
                          && !StaticMeshScript.SemanticMeshIsExporting
                          && !StaticMeshScript.ModelIsLoading
                          && !StaticMeshScript.SemanticMeshIsLoading;

            if (!allowLoad) return;

            if (StaticMeshScript.CheckSemanticMeshDirExist())
            {
                LoadSemanticMesh();
                if (StaticMeshScript.collisionMesh != null)
                    StaticMeshScript.collisionMesh.SetActive(false);
            }
            else
            {
                ScanText.color = BrightColor;
                HintText.text = "No Object is Found.\nPlease Rescan!";
            }
        }
        void LoadSemanticMesh()
        {
            StaticMeshScript.LoadSemanticMesh(LoadSemanticMesh_before, LoadSemanticMesh_done);
        }        
        void LoadSemanticMesh_before()
        {
            HintText.text = "Loading Objects...";
        }                        
        void LoadSemanticMesh_done()
        {
            if (StaticMeshScript.collisionMesh) StaticMeshScript.collisionMesh.SetActive(false);

            HintText.text = "Mesh Loaded!";
            LoadText.color = OriginalColor;
            ViveSR_SceneUnderstanding.ShowAllSemanticBoundingBoxAndIcon();
            StaticMeshScript.ShowAllSemanticCollider();
            currentShowing = 0;
            ViveSR_Experience_ControllerDelegate.gripDelegate += HandleGrip_SemanticObjOperation;
            GripCanvas.SetActive(true);
        }

        #endregion

        void HandleGrip_SemanticObjOperation(ButtonStage buttonStage, Vector2 axis)
        {
            if(buttonStage == ButtonStage.PressDown) ShowNextSemanticType();
        }

        void ShowNextSemanticType()
        {
            ViveSR_SceneUnderstanding.HideAllSemanticBoundingBoxAndIcon();
            StaticMeshScript.HideAllSemanticCollider();

            int type;
            type = (int)currentShowing;
            do
            {
                type = ++type % (int)SceneUnderstandingObjectType.NumOfTypes;
                if (type == (int)SceneUnderstandingObjectType.NONE)
                {
                    ViveSR_SceneUnderstanding.ShowAllSemanticBoundingBoxAndIcon();
                    break;
                }
                else
                {
                    if(ViveSR_SceneUnderstanding.ShowSemanticBoundingBoxAndIconWithType((SceneUnderstandingObjectType)type, true, true))
                    break;
                } 
            } while (!ViveSR_SceneUnderstanding.ShowSemanticBoundingBoxAndIconWithType((SceneUnderstandingObjectType)type, true, true));

            StaticMeshScript.ShowSemanticColliderByType((SceneUnderstandingObjectType)type);
            if (type == (int)SceneUnderstandingObjectType.NONE)
                StaticMeshScript.ShowAllSemanticCollider();
            currentShowing = type;
            if(currentShowing == (int)SceneUnderstandingObjectType.NONE)
                HintText.text = "Showing All";
            else
                HintText.text = "Showing " + ViveSR_SceneUnderstanding.SemanticTypeToString((SceneUnderstandingObjectType)currentShowing);
        }
       
        #region GPUMemoryFullError
        void GPUMemoryFull()
        {
            ViveSR_Experience.instance.ErrorHandlerScript.EnablePanel(ViveSR_Experience_ErrorHandler.ErrorIndex.GPUMemoryFull_Scan, true);

            Stop();
            RigidReconstruction.SRWork_Rigid_Reconstruciton.UnregisterDataErrorHandler((int)Error.GPU_MEMORY_FULL);
        }
        void GPUMemoryFull_Abort()
        {
            ViveSR_Experience.instance.ErrorHandlerScript.DisablePanel(ViveSR_Experience_ErrorHandler.ErrorIndex.GPUMemoryFull_Scan);
            StaticMeshScript.ResetScannedData();
        }
        void GPUMemoryFull_Save()
        {
            ViveSR_Experience.instance.ErrorHandlerScript.DisablePanel(ViveSR_Experience_ErrorHandler.ErrorIndex.GPUMemoryFull_Scan);
            Save();
        }
        #endregion
    }


}