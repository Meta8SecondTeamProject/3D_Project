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
    [SerializeField]
    private float lower_speed;
    [SerializeField]
    private float upper_speed;


    private Vector3[] lower_Base_Rotation;
    private Vector3[] upper_Base_Position;
    private Vector3[] lower_Current_Angles;
    private Vector3[] upper_Current_Position;
    private Vector3[] lower_Clamp_Angles;
    private bool isBack = true;



    private void OnEnable()
    {
        lower_Base_Rotation = new Vector3[lower_Bones.Length];
        lower_Current_Angles = new Vector3[lower_Bones.Length];
        lower_Clamp_Angles = new Vector3[lower_Bones.Length];
        upper_Base_Position = new Vector3[upper_Bones.Length];
        upper_Current_Position = new Vector3[upper_Bones.Length];

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
            float randomz = Mathf.Clamp(lower_Current_Angles[i].z + Random.Range(-30f, 30f), lower_Base_Rotation[i].z - 30f, lower_Base_Rotation[i].z + 30f);

            lower_Clamp_Angles[i] = new Vector3(lower_Base_Rotation[i].x, lower_Base_Rotation[i].y, randomz);

            //float randomY = Mathf.Clamp(current_Angles[i].y + Random.Range(-20f, 20f), base_Rotation[i].y - 20f, base_Rotation[i].y + 20f);
            //float randomZ = Mathf.Clamp(current_Angles[i].z + Random.Range(-20f, 20f), base_Rotation[i].z - 20f, base_Rotation[i].z + 20f);
            //clamp_Angles[i] = new Vector3(randomX, randomY, randomZ);
        }
    }
    private void LowerClampAnglesSetting(int index)
    {
        if (lower_Bones.Length < index)
        {
            Debug.LogError("BoneController / LowerClampAnglesSetting / Index Error");
            return;
        }

        float randomZ = Mathf.Clamp(lower_Current_Angles[index].z + Random.Range(-30f, 30f), lower_Base_Rotation[index].z - 30f, lower_Base_Rotation[index].z + 30f);

        lower_Clamp_Angles[index] = new Vector3(lower_Base_Rotation[index].x, lower_Base_Rotation[index].y, randomZ);

        //float randomY = Mathf.Clamp(current_Angles[index].y + Random.Range(-20f, 20f), base_Rotation[index].y - 20f, base_Rotation[index].y + 20f);
        //float randomZ = Mathf.Clamp(current_Angles[index].z + Random.Range(-20f, 20f), base_Rotation[index].z - 20f, base_Rotation[index].z + 20f);
        //clamp_Angles[index] = new Vector3(randomX, randomY, randomZ);
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
            //current_Angles[i] = Vector3.Lerp(bones[i].localEulerAngles, clamp_Angles[i], speed * Time.fixedDeltaTime);
            //bones[i].localRotation = Quaternion.Euler(current_Angles[i]);

            float angleZ = Mathf.Lerp(lower_Current_Angles[i].z, lower_Clamp_Angles[i].z, lower_speed * Time.fixedDeltaTime);

            lower_Current_Angles[i] = new Vector3(lower_Base_Rotation[i].x, lower_Base_Rotation[i].y, angleZ);

            lower_Bones[i].localRotation = Quaternion.Euler(lower_Current_Angles[i]);


            //if (Vector3.Distance(current_Angles[i], clamp_Angles[i]) <= 0.1f)
            if (Mathf.Abs(lower_Current_Angles[i].z - lower_Clamp_Angles[i].z) <= 0.01f)
            {
                lower_Current_Angles[i] = lower_Clamp_Angles[i];
                lower_Bones[i].localRotation = Quaternion.Euler(lower_Current_Angles[i]);
                LowerClampAnglesSetting(i);
            }
        }

        for (int i = 0; i < upper_Bones.Length; i++)
        {
            upper_Bones[i].localPosition = Vector3.Lerp(upper_Bones[i].localPosition, upper_Current_Position[i], upper_speed * Time.deltaTime);

            if (Vector3.Distance(upper_Bones[i].localPosition, upper_Current_Position[i]) <= 0.01f)
            {
                upper_Bones[i].localPosition = upper_Current_Position[i];
                isBack = !isBack;
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
            float angleZ = Mathf.Lerp(lower_Current_Angles[i].z, lower_Base_Rotation[i].z, lower_speed * Time.fixedDeltaTime);

            //lower_Current_Angles[i] = new Vector3(angleX, lower_Base_Rotation[i].y, lower_Base_Rotation[i].z);
            lower_Current_Angles[i] = new Vector3(lower_Base_Rotation[i].x, lower_Base_Rotation[i].y, angleZ);

            lower_Bones[i].localRotation = Quaternion.Euler(lower_Current_Angles[i]);

            if (Mathf.Abs(lower_Current_Angles[i].z - lower_Base_Rotation[i].z) <= 0.01f)
            {
                lower_Current_Angles[i] = lower_Base_Rotation[i];
                lower_Bones[i].localRotation = Quaternion.Euler(lower_Current_Angles[i]);
            }
        }

        for (int i = 0; i < upper_Bones.Length; i++)
        {
            upper_Bones[i].localPosition = Vector3.Lerp(upper_Bones[i].localPosition, upper_Base_Position[i], upper_speed * Time.fixedDeltaTime);

            if (Vector3.Distance(upper_Bones[i].localPosition, upper_Base_Position[i]) <= 0.01f)
            {
                upper_Bones[i].localPosition = upper_Base_Position[i];
            }
        }
    }

}
