using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace Vive.Plugin.SR.Experience
{
    public class ViveSR_Experience_ErrorHandler : MonoBehaviour {

        public enum ErrorIndex
        {
            InitialFail,
            GPUMemoryFull_Scan,
            GPUMemoryFull_Save
        }

        [SerializeField] List<GameObject> _errorHandlingPanels;    
        
        Dictionary<ErrorIndex, GameObject> ErrorHandlingPanels = new Dictionary<ErrorIndex, GameObject>();

        public ViveSR_Experience_GPUMemoryFullControlPanel GPUMemoryFullControlPanel;

        private void Awake()
        {
            for (int i = 0; i < _errorHandlingPanels.Count; ++i)
                ErrorHandlingPanels[(ErrorIndex)i] = _errorHandlingPanels[i];
        }

        public void EnablePanel(ErrorIndex index, bool enableLaserPointer = false)
        {
            ErrorHandlingPanels[index].SetActive(true);

            if (enableLaserPointer)
            {
                PlayerHandUILaserPointer.SetColors(Color.red, Color.white);
                PlayerHandUILaserPointer.EnableLaserPointer(enableLaserPointer);
            }            
        }

        public void DisablePanel(ErrorIndex index)
        {
            ErrorHandlingPanels[index].SetActive(false);

            if (PlayerHandUILaserPointer.LaserPointer.isActiveAndEnabled)
            {
                PlayerHandUILaserPointer.ResetColors();
                PlayerHandUILaserPointer.EnableLaserPointer(false);
            }
        }
    }
}