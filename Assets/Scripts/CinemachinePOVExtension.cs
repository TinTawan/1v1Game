using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachinePOVExtension : CinemachineExtension
{
    private InputManager inputManager;
    private Vector3 startingRotation;

    [SerializeField] float clampAngle = 80f;
    [SerializeField] float horiSpeed = 10f;
    [SerializeField] float vertSpeed = 10f;


    protected override void Awake()
    {
        inputManager = InputManager.Instance;
        base.Awake();
    }

    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        //throw new System.NotImplementedException();

        if (vcam.Follow)
        {
            if(stage == CinemachineCore.Stage.Aim)
            {
                if(startingRotation == null)
                {
                    startingRotation = transform.localRotation.eulerAngles;
                }

                Vector2 look = inputManager.GetPlayerLook();
                startingRotation.x += look.x * Time.deltaTime * vertSpeed;
                startingRotation.y += look.y * Time.deltaTime * horiSpeed;

                startingRotation.y = Mathf.Clamp(startingRotation.y, -clampAngle, clampAngle);

                state.RawOrientation = Quaternion.Euler(-startingRotation.y, startingRotation.x, 0f);

            }
        }
    }

}
