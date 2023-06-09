using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using TMPro;

public class Ball : MonoBehaviour
{
    public Agent[] players;

    private Rigidbody rb;
    public int blueScore, redScore;

    public TMP_Text blueTeamScore;
    public TMP_Text redTeamScore;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.CompareTag("RED_GOAL"))
        {
            // 블루팀 +1
            players[0].AddReward(+1.0f);
            // 레드팀 -1
            players[1].AddReward(-1.0f);

            // 볼 초기화(위치)
            InitBall();
            // 플레이어 학습 초기화
            players[0].EndEpisode();
            players[1].EndEpisode();

            blueTeamScore.text = (++blueScore).ToString("000");
        }

        if (coll.collider.CompareTag("BLUE_GOAL"))
        {
            // 블루팀 -1
            players[0].AddReward(-1.0f);
            // 레드팀 +1
            players[1].AddReward(+1.0f);

            // 볼 초기화(위치)
            InitBall();
            // 플레이어 학습 초기화
            players[0].EndEpisode();
            players[1].EndEpisode();

            redTeamScore.text = $"{++redScore:000}";
        }
    }

    void InitBall()
    {
        rb.velocity = rb.angularVelocity = Vector3.zero;
        transform.localPosition = new Vector3(0, 1, 0);
    }
}
