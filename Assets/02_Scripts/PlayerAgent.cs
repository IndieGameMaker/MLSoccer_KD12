using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Policies;

public class PlayerAgent : Agent
{
    public enum Team
    {
        BLUE = 0, RED
    }

    public Team team = Team.BLUE;

    // 플레이어의 초기 위치
    public Vector3 initPosBlue = new Vector3(-5.5f, 0.5f, 0.0f);
    public Vector3 initPosRed = new Vector3(5.5f, 0.5f, 0.0f);

    // 플레이어의 초기 각도
    public Quaternion initRotBlue = Quaternion.Euler(Vector3.up * 90);
    public Quaternion initRotRed = Quaternion.Euler(Vector3.up * -90);

    // 플레이어 색상
    public Material[] materials;

    // Behaviour Parameters 컴포넌트
    [SerializeField] private BehaviorParameters bps;
    [SerializeField] private Rigidbody rb;

    public override void Initialize()
    {
        // 팀 설정
        bps = GetComponent<BehaviorParameters>();
        bps.TeamId = (int)team;
        // 물리 설정
        rb = GetComponent<Rigidbody>();
        rb.mass = 10.0f;
        rb.constraints = RigidbodyConstraints.FreezePositionY
                        | RigidbodyConstraints.FreezeRotationX
                        | RigidbodyConstraints.FreezeRotationZ;

        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;

        // 플레이어 색상 설정
        GetComponent<Renderer>().material = materials[(int)team];
        // 플레이어의 위치 및 각도 설정
        InitPlayer();
        // 최대 스텝수 설정
        MaxStep = 10000;
    }

    // 플레이어 위치와 회전을 최기화
    public void InitPlayer()
    {
        transform.localPosition = (team == Team.BLUE) ? initPosBlue : initPosRed;
        transform.localRotation = (team == Team.BLUE) ? initRotBlue : initRotRed;
    }
}
