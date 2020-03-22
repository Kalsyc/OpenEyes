using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

namespace Vive.Plugin.SR.Experience
{
    [RequireComponent(typeof(ViveSR_Experience))]
    public class Sample5_ChairSegmentation : MonoBehaviour
    {   
        Text ScanText, StopText, SaveText, PlayText, HintText;
        [SerializeField] GameObject RaycastStartPoint;
        ViveSR_Experience_NPCGenerator npcGenerator;

        [SerializeField] GameObject NavFloor;

        List<ViveSR_Experience_Chair> MR_Chairs = new List<ViveSR_Experience_Chair>();

        ViveSR_Experience_StaticMesh StaticMeshScript;

        List<SceneUnderstandingObjects.Element> SegResults;

        Color Color_Bright = new Color(0.75f, 0.92f, 1);
        Color Color_Dark = new Color(0.33f, 0.4f, 0.44f);

        public void Init()
        {
            StaticMeshScript = FindObjectOfType<ViveSR_Experience_StaticMesh>();

            npcGenerator = GetComponent<ViveSR_Experience_NPCGenerator>();

            GameObject attachPointCanvas = ViveSR_Experience.instance.AttachPoint.transform.GetChild(ViveSR_Experience.instance.AttachPointIndex).transform.gameObject;

            ScanText = attachPointCanvas.transform.Find("TouchpadCanvas/ScanText").GetComponent<Text>();
            StopText = attachPointCanvas.transform.Find("TouchpadCanvas/StopText").GetComponent<Text>();
            SaveText = attachPointCanvas.transform.Find("TouchpadCanvas/SaveText").GetComponent<Text>();
            PlayText = attachPointCanvas.transform.Find("TouchpadCanvas/PlayText").GetComponent<Text>();
            HintText = attachPointCanvas.transform.Find("HintText").GetComponent<Text>();

            PlayText.text = "[Load]";
            if (StaticMeshScript.GetSegmentationInfo(SceneUnderstandingObjectType.CHAIR).Count > 0)
                PlayText.color = Color_Bright;

            ViveSR_DualCameraRig.Instance.VirtualCamera.cullingMask &= ~(1 << LayerMask.NameToLayer("UI"));
            FindObjectOfType<ViveSR_PortalMgr>().TurnOnCamera();

            RaycastStartPoint = ViveSR_Experience.instance.AttachPoint.transform.Find("RaycastStartPoint").gameObject;

            ViveSR_Experience_ControllerDelegate.touchpadDelegate += handleTouchpad_MRChair;
        }

        public void SetColor(Color color, params Text[] texts)
        {
            foreach(Text text in texts) text.color = color;
        }

        #region Mesh Control
        public void handleTouchpad_MRChair(ButtonStage buttonStage, Vector2 axis)
        {
            switch (buttonStage)
            {
                case ButtonStage.PressDown:
                    TouchpadDirection touchpadDirection = ViveSR_Experience_ControllerDelegate.GetTouchpadDirection(axis, false);
                    handleTouchpad_MRChair_Pressdown(touchpadDirection);
                    break;
            }
        }

        void handleTouchpad_MRChair_Pressdown(TouchpadDirection touchpadDirection)
        {
            switch (touchpadDirection)
            {
                case TouchpadDirection.Up: Scan(); break;
                case TouchpadDirection.Left: StopAndReset(); break;
                case TouchpadDirection.Right: CheckAndSave(); break;
                case TouchpadDirection.Down: Play(); break;
            }
        }

        void Scan()
        {
            bool allowScan = !ViveSR_RigidReconstruction.IsScanning
                           && !ViveSR_RigidReconstruction.IsExportingMesh
                           && !StaticMeshScript.SemanticMeshIsExporting
                           && !StaticMeshScript.ModelIsLoading
                           && !StaticMeshScript.SemanticMeshIsLoading;

            if(!allowScan) return;

            HintText.text = "";

            // Clear
            StaticMeshScript.LoadMesh(false);
            StaticMeshScript.ActivateSemanticMesh(false);
            StaticMeshScript.ClearHintLocators();
            npcGenerator.ClearScene();
            NavFloor.SetActive(false);

            RigidReconstruction.SRWork_Rigid_Reconstruciton.RegisterDataErrorHandler((int)Error.GPU_MEMORY_FULL, GPUMemoryFull);
            StaticMeshScript.EnableDepthProcessingAndScanning(true);
            StaticMeshScript.SetSegmentation(true);

            ViveSR_SceneUnderstanding.SetCustomSceneUnderstandingConfig(SceneUnderstandingObjectType.CHAIR, 10, true);

            SetColor(Color_Dark, ScanText, StopText, PlayText);
            SetColor(Color_Bright, SaveText, StopText);
        }

        void StopAndReset()
        {
            if (!ViveSR_RigidReconstruction.IsScanning) return;

            Stop();
            StaticMeshScript.ResetScannedData();
        }

        void Stop()
        {
            if (!ViveSR_RigidReconstruction.IsScanning) return;

            StaticMeshScript.SetSegmentation(false);
            StaticMeshScript.EnableDepthProcessingAndScanning(false);
            StaticMeshScript.LoadMesh(true);
            StaticMeshScript.ActivateSemanticMesh(true);

            SetColor(Color_Bright, ScanText);
            SetColor(Color_Dark, SaveText, StopText);

            if (StaticMeshScript.GetSegmentationInfo(SceneUnderstandingObjectType.CHAIR).Count > 0) SetColor(Color_Bright, PlayText);

            RigidReconstruction.SRWork_Rigid_Reconstruciton.UnregisterDataErrorHandler((int)Error.GPU_MEMORY_FULL);
        }

        void CheckAndSave()
        {
            bool allowSave = ViveSR_RigidReconstruction.IsScanning 
                && !ViveSR_RigidReconstruction.IsExportingMesh 
                && !StaticMeshScript.SemanticMeshIsExporting;

            if (!allowSave) return;
          
            SetColor(Color_Dark, ScanText, StopText, SaveText, PlayText);
            StaticMeshScript.SetSegmentation(false);

            StaticMeshScript.ExportSemanticMesh(CheckAndSave_UpdatePercentage, ExportSemanticMesh_done);
        }
        void CheckAndSave_UpdatePercentage(int percentage)
        {
            HintText.text = "Saving Objects...\n" + percentage + "%";
        }
        void ExportSemanticMesh_done()
        {
            SegResults = StaticMeshScript.GetSegmentationInfo(SceneUnderstandingObjectType.CHAIR);

            if (SegResults.Count == 0)
            {
                HintText.text = "No Chair Identified";

                SetColor(Color_Bright, StopText, SaveText);

                StaticMeshScript.EnableDepthProcessingAndScanning(true);
                StaticMeshScript.SetSegmentation(true);
            }
            else
            {
                StaticMeshScript.ExportModel(ExportSemanticMesh_UpdatePercentage, ExportModel_done);
            }
        }
        void ExportSemanticMesh_UpdatePercentage(int percentage)
        {
            if (percentage < 100) HintText.text = SegResults.Count + " Chairs Identified.\n" + "Saving Scene..." + percentage.ToString() + "%";
            else HintText.text = "Scene Saved!";
        }
        void ExportModel_done()
        {
            StaticMeshScript.GenerateHintLocators(SegResults);
            PlayText.text = "[Load]";
            SetColor(Color_Bright, ScanText, PlayText);
            SetColor(Color_Dark, StopText, SaveText);
        }
                     
        void Play()
        {
            SegResults = StaticMeshScript.GetSegmentationInfo(SceneUnderstandingObjectType.CHAIR);

            bool allowPlay = !ViveSR_RigidReconstruction.IsScanning
                && !ViveSR_RigidReconstruction.IsExportingMesh 
                && !StaticMeshScript.SemanticMeshIsExporting 
                && !StaticMeshScript.ModelIsLoading 
                && !StaticMeshScript.SemanticMeshIsLoading && SegResults.Count > 0;

            if (!allowPlay) return;

            if (StaticMeshScript.CheckModelLoaded()) GenerateNPC();
            else if(StaticMeshScript.CheckSemanticMeshDirExist()) LoadSemanticMesh(); 
            else
            {
                ScanText.color = Color_Bright;
                HintText.text = "Object Not Found.";
            }
        }        
        void GenerateNPC()
        {
            if (MR_Chairs.Count <= 0) return;

            HintText.text = "";

            Vector3 lineStartPos = RaycastStartPoint.transform.position;
            Vector3 spawnFwd = RaycastStartPoint.transform.forward;
            Vector3 spawnPos = lineStartPos + spawnFwd * 8;
            npcGenerator.Play(spawnPos, RaycastStartPoint.transform.forward, MR_Chairs);
        }
        void LoadSemanticMesh()
        {
            StaticMeshScript.LoadSemanticMesh(LoadSemanticMesh_before, LoadSemanticMesh_done);
        }
        void LoadSemanticMesh_before()
        {
            MR_Chairs.Clear();
            HintText.text = "Loading Objects...";
        }
        void LoadSemanticMesh_done()
        {
            StaticMeshScript.LoadMesh(true, false, LoadMesh_before, LoadMesh_done);
        }
        void LoadMesh_before()
        {
            HintText.text = "Loading Scene...";
            SetColor(Color.gray, ScanText, StopText, SaveText, PlayText);
        }
        void LoadMesh_done()
        {         
            if (StaticMeshScript.collisionMesh) StaticMeshScript.collisionMesh.SetActive(false);

            HintText.text = "Mesh Loaded!";
            PlayText.text = "[Play]";
            SetColor(Color_Bright, ScanText, PlayText);
            StaticMeshScript.ClearHintLocators();

            if (!LoadChair())
            {
                HintText.text = "No Chair Identified!";
                Debug.Log("No Chair Identified!");
                return;
            }
            if (NavFloor == null)
            {
                HintText.text = "No Floor Identified!";
                Debug.Log("No Floor Identified!");
                return;
            }
            else
            {
                NavFloor.SetActive(true);
            }
            GenerateNPC();
        }
        bool LoadChair()
        {
            StaticMeshScript.ClearHintLocators();

            GameObject[] chairs = StaticMeshScript.GetSemanticObjects(SceneUnderstandingObjectType.CHAIR);

            if (chairs.Length == 0) return false;
         
            for (int i = 0; i < chairs.Length; i++)
            {
                GameObject go = chairs[i];
                ViveSR_Experience_Chair chair = go.AddComponent<ViveSR_Experience_Chair>();
                ViveSR_SceneUnderstanding.SceneObject obj = ViveSR_SceneUnderstanding.GetCorrespondingSceneObject(go.name);
                if (obj.objTypeID == SceneUnderstandingObjectType.NONE)
                {
                    Debug.Log("[SemanticSegmentation] Cannot find corresponding game object.");
                    return false;
                }
                chair.CreateChair(obj.positions[0], obj.forward); //move chair
                MR_Chairs.Add(chair);
            }
 
            return true;
        }
        #endregion

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
            CheckAndSave();
        }
        #endregion
    }
}