using UnityEngine;
using Valve.VR.InteractionSystem;

namespace Vive.Plugin.SR.Experience
{
    public class ViveSR_Experience_SwitchMode : MonoBehaviour
    {
        [SerializeField] GameObject VRMode_bg;
        public DualCameraDisplayMode currentMode = DualCameraDisplayMode.MIX;
        [SerializeField] Material SkyboxMaterial;
        [SerializeField] bool setSkybox;

        private void Start()
        {
            Player.instance.hmdTransforms[0].gameObject.GetComponent<Camera>().enabled = true;
        }

        public void SwithMode(DualCameraDisplayMode mode)
        {
            if (mode != currentMode)
            {
                currentMode = mode;
                ViveSR_DualCameraRig.Instance.SetMode(mode);
                ViveSR_DualCameraRig.Instance.Mode = mode;

                if(VRMode_bg != null)
                    VRMode_bg.SetActive(mode == DualCameraDisplayMode.VIRTUAL);

                if (setSkybox)
                    SetSkybox(mode == DualCameraDisplayMode.VIRTUAL);
            }
        }

        private void OnDestroy()
        {
            if (setSkybox) SetSkybox(false);
        }

        public void SetSkybox(bool on)
        {
            SkyboxMaterial.SetFloat("_Exposure", on ? 1 : 0);
        }
    }
}