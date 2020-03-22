using UnityEngine;
using Valve.VR.InteractionSystem;
namespace Vive.Plugin.SR.Experience
{
    public class ViveSR_Experience_Dart : MonoBehaviour
    {
        [SerializeField] Throwable throwable;
        public ViveSR_Experience_DartGeneratorMgr dartGeneratorMgr;

        void Update()
        {
            if (transform.position.y < -3) Destroy(gameObject);
        }

        bool isNameAllowed(string Name)
        {
            return name.Contains("Model_cld")
                || name.Contains("Tile");
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (isNameAllowed(collision.gameObject.name) && !dartGeneratorMgr.DartGenerators[dartGeneratorMgr.dartPlacementMode].isHolding)
            {    
                if (throwable != null) Destroy(throwable);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.name.Contains("PlaneMeshCollider") && !dartGeneratorMgr.DartGenerators[dartGeneratorMgr.dartPlacementMode].isHolding)
            {
                if (gameObject.name.Contains("Dart_dart"))
                {
                    Rigidbody rigid = GetComponent<Rigidbody>();
                    rigid.useGravity = false;
                    rigid.isKinematic = true;
                    if(throwable != null) Destroy(throwable);
                }
            }
        }
    }
}
