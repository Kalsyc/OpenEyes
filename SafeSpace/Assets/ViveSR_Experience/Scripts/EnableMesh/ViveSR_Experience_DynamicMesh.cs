using UnityEngine;

namespace Vive.Plugin.SR.Experience
{
    public class ViveSR_Experience_DynamicMesh : MonoBehaviour
    {
        public bool ShowDynamicCollision { get; private set; }
        [SerializeField] Material CollisionMaterial;
        [SerializeField] Material ControllerMaterial;

        private void Awake()
        {
            ShowDynamicCollision = false;
            ViveSR_DualCameraDepthCollider.ChangeColliderMaterial(CollisionMaterial);
        }

        public void SetDynamicMesh(bool isOn)
        {               
            ViveSR_DualCameraImageCapture.EnableDepthProcess(isOn);  
            ViveSR_DualCameraDepthCollider.UpdateDepthCollider = isOn;
            ViveSR_DualCameraDepthCollider.SetColliderEnable(isOn);

            ViveSR_DualCameraDepthCollider.ColliderMeshVisibility = isOn ? ShowDynamicCollision : false;
        }

        public void SetMeshDisplay(bool isOn)
        {
            ShowDynamicCollision = isOn;

            ViveSR_DualCameraDepthCollider.ColliderMeshVisibility = isOn;
        }   
    }
}