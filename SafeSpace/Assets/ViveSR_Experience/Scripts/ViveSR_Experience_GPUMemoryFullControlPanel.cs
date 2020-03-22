using UnityEngine;
using UnityEngine.UI;

namespace Vive.Plugin.SR.Experience
{
    public class ViveSR_Experience_GPUMemoryFullControlPanel : MonoBehaviour
    {
        public Button SaveButton, AbortButton;

        [SerializeField] GameObject GPUMemoryFullControllerHint;

        private void Awake()
        {
            GPUMemoryFullControllerHint.transform.SetParent(ViveSR_Experience.instance.AttachPoint.transform, false);
            gameObject.transform.SetParent(ViveSR_Experience.instance.PlayerHeadCollision.transform, false);
        }

        private void OnEnable()
        {
            GPUMemoryFullControllerHint.SetActive(true);
        }

        private void OnDisable()
        {
            GPUMemoryFullControllerHint.SetActive(false);
        }
    }
  

}