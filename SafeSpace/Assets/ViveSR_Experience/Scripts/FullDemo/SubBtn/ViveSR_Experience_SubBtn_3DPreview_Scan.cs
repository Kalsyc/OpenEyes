using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

namespace Vive.Plugin.SR.Experience
{
    public class ViveSR_Experience_SubBtn_3DPreview_Scan : ViveSR_Experience_ISubBtn
    {
        [SerializeField] _3DPreview_SubBtn SubBtnType; 

        public ViveSR_Experience_Scan_ControllerDetection ControllerVisibilityDetector;

        ViveSR_Experience_StaticMesh StaticMeshScript;

        [SerializeField] ViveSR_Experience_GPUMemoryFullControlPanel GPUMemoryFullControlPanel;

        [SerializeField] GameObject GPUMemoryFullControllerHint;

        public UnityEvent OnGPUMemoryFullControlPanelOn, OnGPUMemoryFullControlPanel_Abort; //for tutorial UI to listen to
        UnityEvent OnStopScanning = new UnityEvent();

        protected override void AwakeToDo()
        {                                    
            OnStopScanning.AddListener(StopScanning);
            OnStopScanning.AddListener(ResetScanning);

            #if !UNITY_EDITOR
            ControllerVisibilityDetector.OnBecameVisibleEvent.AddListener(()=>
            {
                ViveSR_Experience_HintMessage.instance.SetHintMessage(hintType.onHeadSet, "Put the controller out of sight to start scanning.", false);
            });                                                           
                               
            ControllerVisibilityDetector.OnBecameInvisibleEvent.AddListener(() =>
            {
                ViveSR_Experience_HintMessage.instance.HintTextFadeOff(hintType.onHeadSet, 0f);
                StartScanning();
                ControllerVisibilityDetector.gameObject.SetActive(false);
            });
            #endif

            ThisButtonTypeNum = (int)SubBtnType;
            ViveSR_RigidReconstructionRenderer.LiveMeshDisplayMode = ReconstructionDisplayMode.ADAPTIVE_MESH;

            GPUMemoryFullControlPanel.SaveButton.onClick.AddListener(GPUMemoryFull_Save);
            GPUMemoryFullControlPanel.AbortButton.onClick.AddListener(GPUMemoryFull_Abort);                                                 
        }
            
        protected override void StartToDo()
        {
            StaticMeshScript = ViveSR_Experience_Demo.instance.StaticMeshScript;
        }

        void GPUMemoryFull()
        {
            //STOP; do not reset
            OnStopScanning.RemoveListener(ResetScanning);
            ViveSR_Experience_Demo.instance.SubButtonScripts[SubMenuButton._3DPreview_Scan].ForceExcute(false);
            OnStopScanning.AddListener(ResetScanning);

            RigidReconstruction.SRWork_Rigid_Reconstruciton.UnregisterDataErrorHandler((int)Error.GPU_MEMORY_FULL);

            //enable GPU Memory Full UI
            ViveSR_Experience.instance.ErrorHandlerScript.EnablePanel(ViveSR_Experience_ErrorHandler.ErrorIndex.GPUMemoryFull_Scan, true);

            //hide rotator & tutorial UI
            OnGPUMemoryFullControlPanelOn.Invoke();
            ViveSR_Experience_Demo.instance.Rotator.RenderButtons(false);
            SubMenu.RenderSubBtns(false);
        }
        void GPUMemoryFull_Save()
        {
            //hide GPU Memory Full UI
            ViveSR_Experience.instance.ErrorHandlerScript.DisablePanel(ViveSR_Experience_ErrorHandler.ErrorIndex.GPUMemoryFull_Scan);

            //Save
            ViveSR_Experience_Demo.instance.SubButtonScripts[SubMenuButton._3DPreview_Save].ForceExcute(true);
        }
        void GPUMemoryFull_Abort()
        {
            //hide GPU Memory Full UI
            ViveSR_Experience.instance.ErrorHandlerScript.DisablePanel(ViveSR_Experience_ErrorHandler.ErrorIndex.GPUMemoryFull_Scan);

            //show rotator & tutorial UI
            OnGPUMemoryFullControlPanel_Abort.Invoke();
            ViveSR_Experience_Demo.instance.Rotator.RenderButtons(true);
            SubMenu.RenderSubBtns(true);

            //Reset
            ResetScanning();
        }

        void StartScanning()
        {
            RigidReconstruction.SRWork_Rigid_Reconstruciton.RegisterDataErrorHandler((int)Error.GPU_MEMORY_FULL, GPUMemoryFull);
            ViveSR_Experience_Demo.instance.StaticMeshScript.EnableDepthProcessingAndScanning(true);
            ViveSR_Experience_Demo.instance.StaticMeshScript.SetSegmentation(true);
            ViveSR_Experience_Demo.instance.SubButtonScripts[SubMenuButton._3DPreview_Save].EnableButton(true);
            StaticMeshScript.ClearHintLocators();
        }
        void StopScanning()
        {
            ViveSR_Experience_Demo.instance.StaticMeshScript.EnableDepthProcessingAndScanning(false);
            ViveSR_Experience_Demo.instance.StaticMeshScript.SetSegmentation(false);
            ViveSR_Experience_Demo.instance.SubButtonScripts[SubMenuButton._3DPreview_Save].EnableButton(false);
        }
        void ResetScanning()
        {
            ViveSR_Experience_Demo.instance.StaticMeshScript.ResetScannedData();
        }

        public override void ExecuteToDo()
        {
            #if UNITY_EDITOR
            if (isOn) StartScanning();
            else OnStopScanning.Invoke();
            #else
            ControllerVisibilityDetector.gameObject.SetActive(isOn);
            if (!isOn)
            {
                ViveSR_Experience_HintMessage.instance.HintTextFadeOff(hintType.onHeadSet, 0f);
                if (ViveSR_RigidReconstruction.IsScanning) OnStopScanning.Invoke();
            }  
            #endif
        }

        void RenderMenuSubButtons(MenuButton menu, bool isOn)
        {
            foreach (ViveSR_Experience_ISubBtn sub in ViveSR_Experience_Demo.instance.ButtonScripts[menu].SubMenu.subBtnScripts)
            {
                sub.renderer.enabled = isOn;
            }
        }                                                      
    }
}