using UnityEngine;
using Random = UnityEngine.Random;

public class BoneController : MonoBehaviour
{
    [SerializeField]
    private Transform[] lower_Bones;
    [SerializeField]
    private Transform[] upper_Bones;
    [SerializeField]
    private Transform[] upper_Move_Positions;

    private Vector3[] upper_Base_Position;
    private Vector3[] upper_Current_Position;
    private Vector3[] lower_Base_Rotation;
    private Vector3[] lower_Current_Angles;
    private Vector3[] lower_Clamp_Angles;
    private bool isBack = true;

    public float upper_Timer;
    public float lower_Timer;
    private float[] upper_durations;
    private float[] lower_durations;

    private const float lowerClampValue = 30f;


    private void OnEnable()
    {
        lower_Base_Rotation = new Vector3[lower_Bones.Length];
        lower_Current_Angles = new Vector3[lower_Bones.Length];
        lower_Clamp_Angles = new Vector3[lower_Bones.Length];
        upper_Base_Position = new Vector3[upper_Bones.Length];
        upper_Current_Position = new Vector3[upper_Bones.Length];

        lower_durations = new float[lower_Bones.Length];
        upper_durations = new float[upper_Bones.Length];

        BaseSettings();
        LowerClampAnglesSetting();
        UpperCurentMovePosition();
    }

    private void BaseSettings()
    {
        for (int i = 0; i < lower_Bones.Length; i++)
        {
            lower_Base_Rotation[i] = lower_Bones[i].localEulerAngles;
            lower_Current_Angles[i] = lower_Base_Rotation[i];
        }

        for (int i = 0; i < upper_Bones.Length; i++)
        {
            upper_Base_Position[i] = upper_Bones[i].localPosition;
            upper_Current_Position[i] = upper_Base_Position[i];
        }
    }

    private void LowerClampAnglesSetting()
    {
        for (int i = 0; i < lower_Clamp_Angles.Length; i++)
        {
            float randomz = Mathf.Clamp(lower_Current_Angles[i].z + Random.Range(-lowerClampValue, lowerClampValue), lower_Base_Rotation[i].z - lowerClampValue, lower_Base_Rotation[i].z + lowerClampValue);

            lower_Clamp_Angles[i] = new Vector3(lower_Base_Rotation[i].x, lower_Base_Rotation[i].y, randomz);
        }
    }
    private void LowerClampAnglesSetting(int index)
    {
        if (lower_Bones.Length < index)
        {
            Debug.LogError("BoneController / LowerClampAnglesSetting / Index Error");
            return;
        }

        if (index % 2 == 0)
        {
            float randomZ = Mathf.Clamp(lower_Current_Angles[index].z + Random.Range(-lowerClampValue, lowerClampValue), lower_Base_Rotation[index].z - lowerClampValue, lower_Base_Rotation[index].z + lowerClampValue);

            lower_Clamp_Angles[index] = new Vector3(lower_Base_Rotation[index].x, lower_Base_Rotation[index].y, randomZ);
        }
        else
        {
            float randomX = Mathf.Clamp(lower_Current_Angles[index].x + Random.Range(-lowerClampValue, lowerClampValue), lower_Base_Rotation[index].x - lowerClampValue, lower_Base_Rotation[index].x + lowerClampValue);

            lower_Clamp_Angles[index] = new Vector3(randomX, lower_Base_Rotation[index].y, lower_Base_Rotation[index].z);
        }
    }

    private void UpperCurentMovePosition()
    {
        if (isBack)
        {
            upper_Current_Position[0] = upper_Move_Positions[1].localPosition;
            upper_Current_Position[1] = new Vector3(upper_Move_Positions[1].localPosition.x, upper_Move_Positions[1].localPosition.y, -upper_Move_Positions[1].localPosition.z);
        }
        else
        {
            upper_Current_Position[0] = upper_Move_Positions[0].localPosition;
            upper_Current_Position[1] = new Vector3(upper_Move_Positions[0].localPosition.x, upper_Move_Positions[0].localPosition.y, -upper_Move_Positions[0].localPosition.z);
        }
    }

    private void BonesMovement()
    {
        for (int i = 0; i < lower_Bones.Length; i++)
        {
            lower_durations[i] += Time.fixedDeltaTime;

            if (i % 2 == 0)
            {
                float angleZ = Mathf.LerpAngle(lower_Current_Angles[i].z, lower_Clamp_Angles[i].z, lower_durations[i] / lower_Timer);
                lower_Current_Angles[i] = new Vector3(lower_Base_Rotation[i].x, lower_Base_Rotation[i].y, angleZ);
            }
            else
            {
                float angleX = Mathf.LerpAngle(lower_Current_Angles[i].x, lower_Clamp_Angles[i].x, lower_durations[i] / lower_Timer);
                lower_Current_Angles[i] = new Vector3(angleX, lower_Base_Rotation[i].y, lower_Base_Rotation[i].z);
            }

            lower_Bones[i].localRotation = Quaternion.Euler(lower_Current_Angles[i]);

            if (i % 2 == 0)
            {
                if (Mathf.Abs(lower_Current_Angles[i].z - lower_Clamp_Angles[i].z) <= 0.001f)
                {
                    lower_Current_Angles[i] = lower_Clamp_Angles[i];
                    lower_Bones[i].localRotation = Quaternion.Euler(lower_Current_Angles[i]);
                    LowerClampAnglesSetting(i);
                    lower_durations[i] = 0f;
                }
            }
            else
            {
                if (Mathf.Abs(lower_Current_Angles[i].x - lower_Clamp_Angles[i].x) <= 0.001f)
                {
                    lower_Current_Angles[i] = lower_Clamp_Angles[i];
                    lower_Bones[i].localRotation = Quaternion.Euler(lower_Current_Angles[i]);
                    LowerClampAnglesSetting(i);
                    lower_durations[i] = 0f;
                }
            }
        }

        for (int i = 0; i < upper_Bones.Length; i++)
        {
            upper_durations[i] += Time.fixedDeltaTime;
            upper_Bones[i].localPosition = Vector3.Lerp(upper_Bones[i].localPosition, upper_Current_Position[i], upper_durations[i] / upper_Timer);

            if (Vector3.Distance(upper_Bones[i].localPosition, upper_Current_Position[i]) <= 0.01f)
            {
                upper_Bones[i].localPosition = upper_Current_Position[i];
                isBack = true;
                UpperCurentMovePosition();
            }
        }
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.player.frogMove.isGround)
        {
            BonesBaseMovement();
            isBack = true;
        }
        else
        {
            BonesMovement();
        }
    }

    private void BonesBaseMovement()
    {
        for (int i = 0; i < lower_Bones.Length; i++)
        {
            lower_durations[i] += Time.fixedDeltaTime;
            if (i % 2 == 0)
            {
                float angleZ = Mathf.LerpAngle(lower_Current_Angles[i].z, lower_Base_Rotation[i].z, lower_durations[i] / lower_Timer);
                lower_Current_Angles[i] = new Vector3(lower_Base_Rotation[i].x, lower_Base_Rotation[i].y, angleZ);
            }
            else
            {
                float angleX = Mathf.LerpAngle(lower_Current_Angles[i].x, lower_Base_Rotation[i].x, lower_durations[i] / lower_Timer);
                lower_Current_Angles[i] = new Vector3(angleX, lower_Base_Rotation[i].y, lower_Base_Rotation[i].z);
            }

            lower_Bones[i].localRotation = Quaternion.Euler(lower_Current_Angles[i]);

            if (i % 2 == 0)
            {
                if (Mathf.Abs(lower_Current_Angles[i].z - lower_Base_Rotation[i].z) <= 0.001f)
                {
                    lower_Current_Angles[i] = lower_Base_Rotation[i];
                    lower_Bones[i].localRotation = Quaternion.Euler(lower_Current_Angles[i]);
                    lower_durations[i] = 0f;
                }
            }
            else
            {
                if (Mathf.Abs(lower_Current_Angles[i].x - lower_Base_Rotation[i].x) <= 0.001f)
                {
                    lower_Current_Angles[i] = lower_Base_Rotation[i];
                    lower_Bones[i].localRotation = Quaternion.Euler(lower_Current_Angles[i]);
                    lower_durations[i] = 0f;
                }
            }
        }

        for (int i = 0; i < upper_Bones.Length; i++)
        {
            upper_durations[i] += Time.fixedDeltaTime;
            upper_Bones[i].localPosition = Vector3.Lerp(upper_Bones[i].localPosition, upper_Base_Position[i], upper_durations[i] / upper_Timer);

            if (Vector3.Distance(upper_Bones[i].localPosition, upper_Base_Position[i]) <= 0.01f)
            {
                upper_Bones[i].localPosition = upper_Base_Position[i];
            }
        }
    }

}
