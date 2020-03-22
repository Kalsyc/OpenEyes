using UnityEngine;

namespace Vive.Plugin.SR.Experience
{
    /// <summary>
    /// Attach the script to `DualCamera (head)`, which is under `[ViveSR]`,
    /// to enable the new see through method.
    /// </summary>
    public class ViveSR_Experience_NewSeeThrough : MonoBehaviour
    {
        private bool newSeeThroughIsActive = true;

        /// <summary>
        /// The camera in the ViveSR plugin that renders virtual objects to the left eye.
        /// </summary>
        private Camera virtualCameraL;
        /// <summary>
        /// The camera in the ViveSR plugin that renders virtual objects to the right eye.
        /// </summary>
        private Camera virtualCameraR;
        private Camera frameCameraL;
        private Camera frameCameraR;
        private Camera eyeCameraL;
        private Camera eyeCameraR;
        private RenderTexture renderTextureL;
        private RenderTexture renderTextureR;
        private ViveSR_Experience_FramePlane renderPlaneL;
        private ViveSR_Experience_FramePlane renderPlaneR;
        private GameObject renderPlaneLQuad;
        private GameObject renderPlaneRQuad;

        private float frameCameraLHalfVerticalFOV;
        private float frameCameraRHalfVerticalFOV;

        private bool dualCameraRigIsInitialized = false;

        private const bool framePoseCorrectionIsEnabled = true;
        private ViveSR_TrackedCamera frameCameraLRoot;
        private ViveSR_TrackedCamera frameCameraRRoot;
        private GameObject frameCameraLRootShifter;
        private GameObject frameCameraRRootShifter;
        private Matrix4x4 wrongFrameCameraLToCorrectFrameCameraLMatrix;
        private Matrix4x4 wrongFrameCameraRToCorrectFrameCameraRMatrix;
        private Matrix4x4 wrongFrameHeadToCorrectFrameHeadMatrix;

        private DualCameraDisplayMode previousDualCameraRigDisplayMode;

        private const int renderPlaneLLayer = 26;
        private const int renderPlaneRLayer = 27;

        private ViveSR_Experience_Initialization experienceInitializationScript;
        private ViveSR_Experience_Portal experiencePortalScript;
        private ViveSR_PortalMgr portalManager;
        private Camera portalCameraL;
        private Camera portalCameraR;

        private void Start()
        {
            // Get the portal-related scripts.
            experienceInitializationScript = FindObjectOfType<ViveSR_Experience_Initialization>();
            if (experienceInitializationScript != null)
            {
                // Get the portal-related scripts after the scene initialization.
                experienceInitializationScript.postInitEvent.AddListener(OnPostPreGameTest);
            }

            // Set the frame pose correction value.
            var xrDeviceModelName = UnityEngine.XR.XRDevice.model;
            if (xrDeviceModelName.StartsWith("VIVE_Pro"))
            {
                wrongFrameHeadToCorrectFrameHeadMatrix = Matrix4x4.Translate(new Vector3(0.0325f, 0f, 0.0325f));
            }
            else
            {
                wrongFrameHeadToCorrectFrameHeadMatrix = Matrix4x4.Translate(Vector3.zero);
            }
        }

        private void Update()
        {
            if (!dualCameraRigIsInitialized && ViveSR_DualCameraRig.DualCameraStatus == DualCameraStatus.WORKING)
            {
                ActivateNewSeeThrough();
                dualCameraRigIsInitialized = true;
            }

            if (dualCameraRigIsInitialized)
            {
                EnableOrDisableCamerasAccordingToDisplayMode();
            }
        }

        private void LateUpdate()
        {
            if (framePoseCorrectionIsEnabled && dualCameraRigIsInitialized && newSeeThroughIsActive)
            {
                // Set the local transform of the camera pose shifters.
                var frameCameraLToWorldMatrix = ViveSR_DualCameraImageCapture.UndistortedPoseLeft;
                var frameCameraRToWorldMatrix = ViveSR_DualCameraImageCapture.UndistortedPoseRight;
                var frameCameraLRootShifterToParentMatrix =
                    frameCameraLToWorldMatrix *
                    wrongFrameCameraLToCorrectFrameCameraLMatrix.inverse *
                    frameCameraLToWorldMatrix.inverse;
                var frameCameraRRootShifterToParentMatrix =
                    frameCameraRToWorldMatrix *
                    wrongFrameCameraRToCorrectFrameCameraRMatrix.inverse *
                    frameCameraRToWorldMatrix.inverse;
                SetLocalPositionAndRotationByMatrix(frameCameraLRootShifter.transform, frameCameraLRootShifterToParentMatrix);
                SetLocalPositionAndRotationByMatrix(frameCameraRRootShifter.transform, frameCameraRRootShifterToParentMatrix);
            }
        }

        private void ActivateNewSeeThrough()
        {
            newSeeThroughIsActive = true;

            // Get existing cameras.
            // Make the render planes invisible in these cameras.
            virtualCameraL = ViveSR_DualCameraRig.Instance.VirtualCamera;
            frameCameraL = ViveSR_DualCameraRig.Instance.DualCameraLeft;
            frameCameraR = ViveSR_DualCameraRig.Instance.DualCameraRight;
            virtualCameraL.cullingMask &= ~(1 << renderPlaneLLayer);
            virtualCameraL.cullingMask &= ~(1 << renderPlaneRLayer);

            // Create `virtualCameraR` from `virtualCameraL`.
            // Set the target eye of them.
            // Make the render planes invisible in these cameras.
            if (virtualCameraR == null)
            {
                virtualCameraR = Camera.Instantiate(virtualCameraL);
                virtualCameraR.name = "Camera (eye) (right)";
            }
            else
            {
                virtualCameraR.enabled = true;
                virtualCameraR.gameObject.SetActive(true);
            }
            virtualCameraL.stereoTargetEye = StereoTargetEyeMask.Left;
            virtualCameraR.stereoTargetEye = StereoTargetEyeMask.Right;
            virtualCameraL.cullingMask &= ~(1 << renderPlaneLLayer);
            virtualCameraL.cullingMask &= ~(1 << renderPlaneRLayer);
            virtualCameraR.cullingMask &= ~(1 << renderPlaneLLayer);
            virtualCameraR.cullingMask &= ~(1 << renderPlaneRLayer);

            // Create cameras that render the render planes to the screen.
            if (eyeCameraL == null)
            {
                eyeCameraL = Camera.Instantiate(frameCameraL, Vector3.zero, Quaternion.identity, ViveSR_DualCameraRig.Instance.transform);
                eyeCameraL.name = "Eye Camera (left)";
                eyeCameraL.gameObject.hideFlags = HideFlags.HideInHierarchy;
            }
            else
            {
                eyeCameraL.enabled = true;
                eyeCameraL.gameObject.SetActive(true);
            }
            if (eyeCameraR == null)
            {
                eyeCameraR = Camera.Instantiate(frameCameraR, Vector3.zero, Quaternion.identity, ViveSR_DualCameraRig.Instance.transform);
                eyeCameraR.name = "Eye Camera (right)";
                eyeCameraR.gameObject.hideFlags = HideFlags.HideInHierarchy;
            }
            else
            {
                eyeCameraR.enabled = true;
                eyeCameraR.gameObject.SetActive(true);
            }
            SetLocalTransformToIdentity(eyeCameraL.transform);
            SetLocalTransformToIdentity(eyeCameraR.transform);
            eyeCameraL.cullingMask = 1 << renderPlaneLLayer;
            eyeCameraR.cullingMask = 1 << renderPlaneRLayer;

            // Calculate the eye camera focal lengths.
            var eyeCameraLHalfVerticalFOV = 0.5f * eyeCameraL.fieldOfView * Mathf.Deg2Rad;
            var eyeCameraRHalfVerticalFOV = 0.5f * eyeCameraR.fieldOfView * Mathf.Deg2Rad;
            var eyeCameraLFocalLength = 0.5f * (float) eyeCameraL.pixelHeight / Mathf.Tan(eyeCameraLHalfVerticalFOV);
            var eyeCameraRFocalLength = 0.5f * (float) eyeCameraR.pixelHeight / Mathf.Tan(eyeCameraRHalfVerticalFOV);

            // Calculate the frame camera FOV.
            var frameCameraLHalfHorizontalFOV = Mathf.Atan(0.5f * (float) ViveSR_DualCameraImageCapture.UndistortedImageWidth / (float) ViveSR_DualCameraImageCapture.FocalLength_L);
            var frameCameraRHalfHorizontalFOV = Mathf.Atan(0.5f * (float) ViveSR_DualCameraImageCapture.UndistortedImageWidth / (float) ViveSR_DualCameraImageCapture.FocalLength_R);
            frameCameraLHalfVerticalFOV = Mathf.Atan(0.5f * (float) ViveSR_DualCameraImageCapture.UndistortedImageHeight / (float) ViveSR_DualCameraImageCapture.FocalLength_L);
            frameCameraRHalfVerticalFOV = Mathf.Atan(0.5f * (float) ViveSR_DualCameraImageCapture.UndistortedImageHeight / (float) ViveSR_DualCameraImageCapture.FocalLength_R);

            // Calculate the render texture width and height according to the FOV,
            // using the focal length of the eye cameras.
            var renderTextureLWidth = (int) (2.0f * eyeCameraLFocalLength * Mathf.Tan(frameCameraLHalfHorizontalFOV));
            var renderTextureRWidth = (int) (2.0f * eyeCameraRFocalLength * Mathf.Tan(frameCameraRHalfHorizontalFOV));
            var renderTextureLHeight = (int) (2.0f * eyeCameraLFocalLength * Mathf.Tan(frameCameraLHalfVerticalFOV));
            var renderTextureRHeight = (int) (2.0f * eyeCameraRFocalLength * Mathf.Tan(frameCameraRHalfVerticalFOV));

            // Create render planes.
            if (renderPlaneLQuad == null)
            {
                renderPlaneLQuad = GameObject.CreatePrimitive(PrimitiveType.Quad);
                renderPlaneLQuad.name = "Render Plane (left)";
                renderPlaneLQuad.gameObject.hideFlags = HideFlags.HideInHierarchy;
                Destroy(renderPlaneLQuad.GetComponent<MeshCollider>());
                renderPlaneL = renderPlaneLQuad.AddComponent<ViveSR_Experience_FramePlane>() as ViveSR_Experience_FramePlane;
            }
            else
            {
                renderPlaneLQuad.SetActive(true);
            }
            if (renderPlaneRQuad == null)
            {
                renderPlaneRQuad = GameObject.CreatePrimitive(PrimitiveType.Quad);
                renderPlaneRQuad.name = "Render Plane (right)";
                renderPlaneLQuad.gameObject.hideFlags = HideFlags.HideInHierarchy;
                Destroy(renderPlaneRQuad.GetComponent<MeshCollider>());
                renderPlaneR = renderPlaneRQuad.AddComponent<ViveSR_Experience_FramePlane>() as ViveSR_Experience_FramePlane;
            }
            else
            {
                renderPlaneRQuad.SetActive(true);
            }
            renderPlaneLQuad.transform.parent = ViveSR_DualCameraRig.Instance.TrackedCameraLeft.Anchor;
            renderPlaneRQuad.transform.parent = ViveSR_DualCameraRig.Instance.TrackedCameraRight.Anchor;
            renderPlaneLQuad.layer = renderPlaneLLayer;
            renderPlaneRQuad.layer = renderPlaneRLayer;

            // Create render textures.
            // Make the frame cameras and the virtual cameras render to the render textures.
            // Draw the render textures on the render planes.
            var renderTextureDepth = 24;
            if (renderTextureL == null)
            {
                renderTextureL = new RenderTexture(renderTextureLWidth, renderTextureLHeight, renderTextureDepth);
                renderTextureL.name = "Render Texture L";
            }
            if (renderTextureR == null)
            {
                renderTextureR = new RenderTexture(renderTextureRWidth, renderTextureRHeight, renderTextureDepth);
                renderTextureR.name = "Render Texture R";
            }
            frameCameraL.targetTexture = renderTextureL;
            frameCameraR.targetTexture = renderTextureR;
            virtualCameraL.targetTexture = renderTextureL;
            virtualCameraR.targetTexture = renderTextureR;
            renderPlaneL.SetMainTexture(renderTextureL);
            renderPlaneR.SetMainTexture(renderTextureR);

            // After setting cameras' target texture,
            // set the vertical FOV of the frame cameras and the virtual cameras.
            frameCameraL.fieldOfView = 2.0f * frameCameraLHalfVerticalFOV * Mathf.Rad2Deg;
            frameCameraR.fieldOfView = 2.0f * frameCameraRHalfVerticalFOV * Mathf.Rad2Deg;
            virtualCameraL.fieldOfView = 2.0f * frameCameraLHalfVerticalFOV * Mathf.Rad2Deg;
            virtualCameraR.fieldOfView = 2.0f * frameCameraRHalfVerticalFOV * Mathf.Rad2Deg;

            // Move the frame cameras and the virtual cameras to the anchors.
            frameCameraL.transform.parent = ViveSR_DualCameraRig.Instance.TrackedCameraLeft.Anchor;
            frameCameraR.transform.parent = ViveSR_DualCameraRig.Instance.TrackedCameraRight.Anchor;
            virtualCameraL.transform.parent = ViveSR_DualCameraRig.Instance.TrackedCameraLeft.Anchor;
            virtualCameraR.transform.parent = ViveSR_DualCameraRig.Instance.TrackedCameraRight.Anchor;

            // Set the local transform of the frame cameras, the virtual cameras,
            // and the anchors to identity.
            SetLocalTransformToIdentity(frameCameraL.transform);
            SetLocalTransformToIdentity(frameCameraR.transform);
            SetLocalTransformToIdentity(virtualCameraL.transform);
            SetLocalTransformToIdentity(virtualCameraR.transform);
            SetLocalTransformToIdentity(ViveSR_DualCameraRig.Instance.TrackedCameraLeft.Anchor);
            SetLocalTransformToIdentity(ViveSR_DualCameraRig.Instance.TrackedCameraRight.Anchor);

            // Initialize the render planes.
            renderPlaneL.SetCameraIntrinsics(
                (float) renderTextureL.width,
                (float) renderTextureL.height,
                (float) renderTextureL.width * 0.5f,
                (float) renderTextureL.height * 0.5f,
                eyeCameraLFocalLength);
            renderPlaneR.SetCameraIntrinsics(
                (float) renderTextureR.width,
                (float) renderTextureR.height,
                (float) renderTextureR.width * 0.5f,
                (float) renderTextureR.height * 0.5f,
                eyeCameraRFocalLength);

            // Store the ViveSR dual camera rig display mode.
            previousDualCameraRigDisplayMode = ViveSR_DualCameraRig.Instance.Mode;

            if (framePoseCorrectionIsEnabled)
            {
                // Set up parent objects for the frame camera roots in order to
                // add local transforms on the camera poses.
                frameCameraLRoot = ViveSR_DualCameraRig.Instance.TrackedCameraLeft;
                frameCameraRRoot = ViveSR_DualCameraRig.Instance.TrackedCameraRight;
                if (frameCameraLRootShifter == null)
                {
                    frameCameraLRootShifter = new GameObject("TrackedCamera Shifter (Left)");
                }
                if (frameCameraRRootShifter == null)
                {
                    frameCameraRRootShifter = new GameObject("TrackedCamera Shifter (Right)");
                }
                frameCameraLRootShifter.transform.parent = ViveSR_DualCameraRig.Instance.transform;
                frameCameraRRootShifter.transform.parent = ViveSR_DualCameraRig.Instance.transform;
                frameCameraLRoot.transform.parent = frameCameraLRootShifter.transform;
                frameCameraRRoot.transform.parent = frameCameraRRootShifter.transform;

                // Calculate the transformation from the wrong poses to the correct poses.
                // They will be used to set the local transform of the camera pose shifters.
                var frameCameraLToFrameHeadMatrix = Matrix4x4.Translate(new Vector3(
                    ViveSR_DualCameraImageCapture.OffsetHeadToCamera[0],
                    ViveSR_DualCameraImageCapture.OffsetHeadToCamera[1],
                    ViveSR_DualCameraImageCapture.OffsetHeadToCamera[2]));
                var frameCameraRToFrameHeadMatrix = Matrix4x4.Translate(new Vector3(
                    ViveSR_DualCameraImageCapture.OffsetHeadToCamera[3],
                    ViveSR_DualCameraImageCapture.OffsetHeadToCamera[4],
                    ViveSR_DualCameraImageCapture.OffsetHeadToCamera[5]));
                wrongFrameCameraLToCorrectFrameCameraLMatrix =
                    frameCameraLToFrameHeadMatrix.inverse *
                    wrongFrameHeadToCorrectFrameHeadMatrix *
                    frameCameraLToFrameHeadMatrix;
                wrongFrameCameraRToCorrectFrameCameraRMatrix =
                    frameCameraRToFrameHeadMatrix.inverse *
                    wrongFrameHeadToCorrectFrameHeadMatrix *
                    frameCameraRToFrameHeadMatrix;
            }
        }

        private void DeactivateNewSeeThrough()
        {
            if (frameCameraL == null || frameCameraR == null ||
                virtualCameraR == null || virtualCameraL == null ||
                eyeCameraL == null || eyeCameraR == null ||
                renderPlaneLQuad == null || renderPlaneRQuad == null
                )
            {
                return;
            }

            newSeeThroughIsActive = false;

            // Disable `virtualCameraR`.
            virtualCameraR.enabled = false;
            virtualCameraR.gameObject.SetActive(false);
            // Reset the target eye of `virtualCameraL`.
            virtualCameraL.stereoTargetEye = StereoTargetEyeMask.Both;

            // Disable the cameras that render the render planes.
            eyeCameraL.enabled = false;
            eyeCameraR.enabled = false;
            eyeCameraL.gameObject.SetActive(false);
            eyeCameraR.gameObject.SetActive(false);

            // Deactivate the render planes.
            renderPlaneLQuad.SetActive(false);
            renderPlaneRQuad.SetActive(false);

            // Make the frame cameras and the virtual cameras not render
            // to the render textures.
            // The camera FOV will be reset since now they are under the control
            // of the VR system.
            frameCameraL.targetTexture = null;
            frameCameraR.targetTexture = null;
            virtualCameraL.targetTexture = null;
            virtualCameraR.targetTexture = null;

            // Move the frame cameras under `DualCamera (head)`.
            frameCameraL.transform.parent = ViveSR_DualCameraRig.Instance.transform;
            frameCameraR.transform.parent = ViveSR_DualCameraRig.Instance.transform;
            // Move the virtual cameras under `Camera (eye offset)`.
            virtualCameraL.transform.parent = ViveSR_DualCameraRig.Instance.HMDCameraShifter.transform;
            virtualCameraR.transform.parent = ViveSR_DualCameraRig.Instance.HMDCameraShifter.transform;

            // Reset the anchor local transform.
            ViveSR_DualCameraRig.Instance.TrackedCameraLeft.Anchor.localPosition = new Vector3(
                0,
                -1 * ViveSR_DualCameraImageCapture.OffsetHeadToCamera[1],
                -1 * ViveSR_DualCameraImageCapture.OffsetHeadToCamera[2]);
            ViveSR_DualCameraRig.Instance.TrackedCameraRight.Anchor.localPosition = new Vector3(
                0,
                -1 * ViveSR_DualCameraImageCapture.OffsetHeadToCamera[1],
                -1 * ViveSR_DualCameraImageCapture.OffsetHeadToCamera[2]);
            ViveSR_DualCameraRig.Instance.TrackedCameraLeft.Anchor.localRotation = Quaternion.identity;
            ViveSR_DualCameraRig.Instance.TrackedCameraRight.Anchor.localRotation = Quaternion.identity;
            ViveSR_DualCameraRig.Instance.TrackedCameraLeft.Anchor.localScale = Vector3.one;
            ViveSR_DualCameraRig.Instance.TrackedCameraRight.Anchor.localScale = Vector3.one;
        }

        private void SetLocalTransformToIdentity(Transform transform)
        {
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            transform.localScale = Vector3.one;
        }

        private void SetLocalPositionAndRotationByMatrix(Transform transform, Matrix4x4 matrix)
        {
            transform.localPosition = new Vector3(matrix[0, 3], matrix[1, 3], matrix[2, 3]);
            transform.localRotation = matrix.rotation;
        }

        /// <summary>
        /// Enable or disable the eye cameras according to the display mode
        /// of ViveSR_DualCameraRig.
        /// This part of code should be integrated into ViveSR_DualCameraRig.SetMode().
        /// </summary>
        private void EnableOrDisableCamerasAccordingToDisplayMode()
        {
            var currentDualCameraRigDisplayMode = ViveSR_DualCameraRig.Instance.Mode;
            if (currentDualCameraRigDisplayMode != previousDualCameraRigDisplayMode)
            {
                switch (currentDualCameraRigDisplayMode)
                {
                    case DualCameraDisplayMode.VIRTUAL:
                        eyeCameraL.enabled = false;
                        eyeCameraR.enabled = false;
                        break;
                    case DualCameraDisplayMode.REAL:
                        eyeCameraL.enabled = true;
                        eyeCameraR.enabled = true;
                        break;
                    case DualCameraDisplayMode.MIX:
                        eyeCameraL.enabled = true;
                        eyeCameraR.enabled = true;
                        break;
                }

                previousDualCameraRigDisplayMode = currentDualCameraRigDisplayMode;
            }
        }

        private void OnPostPreGameTest()
        {
            GetPortalScriptsByFindingObject();

            // Try to register the segmentation button events.
            var experienceButtonSegmentationScript = FindObjectOfType<ViveSR_Experience_Button_Segmentation>();
            if (experienceButtonSegmentationScript != null)
            {
                experienceButtonSegmentationScript.portalCamerasEnabledEvent.AddListener(OnSegmentationPortalCamerasEnabled);
                experienceButtonSegmentationScript.portalCamerasDisabledEvent.AddListener(OnSegmentationPortalCamerasDisabled);
            }
        }

        private void GetPortalScriptsByFindingObject()
        {
            // Find the portal scripts.
            experiencePortalScript = FindObjectOfType<ViveSR_Experience_Portal>();
            portalManager = FindObjectOfType<ViveSR_PortalMgr>();

            // If both the scripts are found, the portal function could be used in the scene.
            if (experiencePortalScript != null && portalManager != null)
            {
                // If the portal is already on, trigger the portal on event callback.
                if (experiencePortalScript.IsPortalOn)
                {
                    OnPortalOn();
                }

                // Register the portal events.
                experiencePortalScript.portalOnEvent.AddListener(OnPortalOn);
                experiencePortalScript.portalOffEvent.AddListener(OnPortalOff);
            }
        }

        private void OnPortalOn()
        {
            DeactivateNewSeeThrough();
        }

        private void OnPortalOff()
        {
            ActivateNewSeeThrough();
        }

        private void OnSegmentationPortalCamerasEnabled()
        {
            ActivatePortalRelatedNewSeeThroughObjects();
        }

        private void OnSegmentationPortalCamerasDisabled()
        {
            DeactivatePortalRelatedNewSeeThroughObjects();
        }

        private void ActivatePortalRelatedNewSeeThroughObjects()
        {
            if (portalCameraL == null)
            {
                // Find the portal camera, which should be attached on an active game object.
                portalCameraL = GameObject.Find("Camera (pure virtual)").GetComponent<Camera>();
            }
            portalCameraL.cullingMask &= ~(1 << renderPlaneLLayer);
            portalCameraL.cullingMask &= ~(1 << renderPlaneRLayer);

            if (portalCameraR == null)
            {
                // Camera `portalCameraL` will be set to render to the left eye.
                // Create the portal camera for rendering to the right eye.
                portalCameraR = Camera.Instantiate(portalCameraL);
                portalCameraR.name = "Camera (pure virtual) (right)";
                // The children of `portalCameraL` do not need to be copied.
                // Destroy the copied children.
                for (var i = 0; i < portalCameraR.transform.childCount; ++i)
                {
                    Destroy(portalCameraR.transform.GetChild(i).gameObject);
                }
            }

            // Activate the camera game objects.
            portalCameraL.gameObject.SetActive(true);
            portalCameraR.gameObject.SetActive(true);

            // Set the portal cameras' FOV, target eye, target texture, and local transform.
            portalCameraL.stereoTargetEye = StereoTargetEyeMask.Left;
            portalCameraR.stereoTargetEye = StereoTargetEyeMask.Right;
            portalCameraL.targetTexture = renderTextureL;
            portalCameraR.targetTexture = renderTextureR;
            portalCameraL.fieldOfView = 2.0f * frameCameraRHalfVerticalFOV * Mathf.Rad2Deg;
            portalCameraR.fieldOfView = 2.0f * frameCameraRHalfVerticalFOV * Mathf.Rad2Deg;
            portalCameraL.transform.parent = ViveSR_DualCameraRig.Instance.TrackedCameraLeft.Anchor;
            portalCameraR.transform.parent = ViveSR_DualCameraRig.Instance.TrackedCameraRight.Anchor;
            SetLocalTransformToIdentity(portalCameraL.transform);
            SetLocalTransformToIdentity(portalCameraR.transform);

            // Make the render planes invisible to the portal cameras.
            portalCameraL.cullingMask &= ~(1 << renderPlaneLLayer);
            portalCameraL.cullingMask &= ~(1 << renderPlaneRLayer);
            portalCameraR.cullingMask &= ~(1 << renderPlaneLLayer);
            portalCameraR.cullingMask &= ~(1 << renderPlaneRLayer);
        }

        private void DeactivatePortalRelatedNewSeeThroughObjects()
        {
            // Deactivate the camera game objects.
            portalCameraL.gameObject.SetActive(false);
            portalCameraR.gameObject.SetActive(false);
            // Reset the properties.
            portalCameraL.stereoTargetEye = StereoTargetEyeMask.Both;
            portalCameraR.stereoTargetEye = StereoTargetEyeMask.Both;
            portalCameraL.targetTexture = null;
            portalCameraR.targetTexture = null;
            portalCameraL.transform.parent = ViveSR_DualCameraRig.Instance.HMDCameraShifter.transform;
            portalCameraR.transform.parent = ViveSR_DualCameraRig.Instance.HMDCameraShifter.transform;
            SetLocalTransformToIdentity(portalCameraL.transform);
            SetLocalTransformToIdentity(portalCameraR.transform);
        }
    }
}
