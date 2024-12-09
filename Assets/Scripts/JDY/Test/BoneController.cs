using UnityEngine;
using Random = UnityEngine.Random;

public class BoneController : MonoBehaviour
{
    [SerializeField]
    private Transform[] lower_Bones;
    [SerializeField]
    private Transform[] upper_Bones;
    [SerializeField]
    private float lower_speed;
    [SerializeField]
    private float upper_speed;

    private Vector3[] lower_Base_Rotation;
    private Vector3[] upper_Base_Rotation;
    private Vector3[] lower_Current_Angles;
    private Vector3[] upper_Current_Angles;
    private Vector3[] lower_Clamp_Angles;
    private Vector3[] upper_Clamp_Angles;

    private void Start()
    {
        lower_Base_Rotation = new Vector3[lower_Bones.Length];
        lower_Current_Angles = new Vector3[lower_Bones.Length];
        lower_Clamp_Angles = new Vector3[lower_Bones.Length];
        upper_Base_Rotation = new Vector3[upper_Bones.Length];
        upper_Current_Angles = new Vector3[upper_Bones.Length];
        upper_Clamp_Angles = new Vector3[upper_Bones.Length];

        BaseSettings();
        LowerClampAnglesSetting();
        UpperClampAnglesSetting();
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
            upper_Base_Rotation[i] = upper_Bones[i].localEulerAngles;
            upper_Current_Angles[i] = upper_Base_Rotation[i];
        }
    }

    private void LowerClampAnglesSetting()
    {
        for (int i = 0; i < lower_Clamp_Angles.Length; i++)
        {
            float randomX = Mathf.Clamp(lower_Current_Angles[i].x + Random.Range(-30f, 30f), lower_Base_Rotation[i].x - 30f, lower_Base_Rotation[i].x + 30f);

            lower_Clamp_Angles[i] = new Vector3(randomX, lower_Base_Rotation[i].y, lower_Base_Rotation[i].z);

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

        float randomX = Mathf.Clamp(lower_Current_Angles[index].x + Random.Range(-30f, 30f), lower_Base_Rotation[index].x - 30f, lower_Base_Rotation[index].x + 30f);

        lower_Clamp_Angles[index] = new Vector3(randomX, lower_Base_Rotation[index].y, lower_Base_Rotation[index].z);

        //float randomY = Mathf.Clamp(current_Angles[index].y + Random.Range(-20f, 20f), base_Rotation[index].y - 20f, base_Rotation[index].y + 20f);
        //float randomZ = Mathf.Clamp(current_Angles[index].z + Random.Range(-20f, 20f), base_Rotation[index].z - 20f, base_Rotation[index].z + 20f);
        //clamp_Angles[index] = new Vector3(randomX, randomY, randomZ);
    }

    private void UpperClampAnglesSetting()
    {
        for (int i = 0; i < lower_Clamp_Angles.Length; i++)
        {
            if (i % 2 == 0)
            {
                float randomX = Mathf.Clamp(lower_Current_Angles[i].x + Random.Range(-20f, 30f), lower_Base_Rotation[i].x - 20f, lower_Base_Rotation[i].x + 30f);

                upper_Clamp_Angles[i] = new Vector3(randomX, lower_Base_Rotation[i].y, lower_Base_Rotation[i].z);
            }
            else
            {
                float randomZ = Mathf.Clamp(upper_Current_Angles[i].z + Random.Range(-20f, 30f), upper_Base_Rotation[i].z - 20f, upper_Base_Rotation[i].z + 30f);
                upper_Clamp_Angles[i] = new Vector3(upper_Bones[i].transform.rotation.x, upper_Bones[i].transform.rotation.y, randomZ);
            }
        }
    }

    private void UpperClampAnglesSetting(int index)
    {
        if (index > upper_Bones.Length)
        {
            Debug.LogError("BoneController / UpperClampAnglesSetting / Index Error");
            return;
        }

        if (index % 2 == 0)
        {
            float randomY = Mathf.Clamp(upper_Current_Angles[index].y + Random.Range(-20f, 30f), upper_Base_Rotation[index].y - 20f, upper_Base_Rotation[index].y + 30f);

            upper_Clamp_Angles[index] = new Vector3(upper_Base_Rotation[index].x, randomY, upper_Base_Rotation[index].z);
        }
        else
        {
            float randomZ = Mathf.Clamp(upper_Current_Angles[index].z + Random.Range(-20f, 30f), upper_Base_Rotation[index].z - 20f, upper_Base_Rotation[index].z + 30f);
            upper_Clamp_Angles[index] = new Vector3(upper_Base_Rotation[index].x, upper_Base_Rotation[index].y, randomZ);
        }

    }

    private void BonesMovement()
    {
        for (int i = 0; i < lower_Bones.Length; i++)
        {
            //current_Angles[i] = Vector3.Lerp(bones[i].localEulerAngles, clamp_Angles[i], speed * Time.fixedDeltaTime);
            //bones[i].localRotation = Quaternion.Euler(current_Angles[i]);

            float angleX = Mathf.Lerp(lower_Current_Angles[i].x, lower_Clamp_Angles[i].x, lower_speed * Time.fixedDeltaTime);

            lower_Current_Angles[i] = new Vector3(angleX, lower_Base_Rotation[i].y, lower_Base_Rotation[i].z);

            lower_Bones[i].localRotation = Quaternion.Euler(lower_Current_Angles[i]);


            //if (Vector3.Distance(current_Angles[i], clamp_Angles[i]) <= 0.1f)
            if (Mathf.Abs(lower_Current_Angles[i].x - lower_Clamp_Angles[i].x) <= 0.1f)
            {
                lower_Current_Angles[i] = lower_Clamp_Angles[i];
                lower_Bones[i].localRotation = Quaternion.Euler(lower_Current_Angles[i]);
                LowerClampAnglesSetting(i);
            }
        }

        for (int i = 0; i < upper_Bones.Length; i++)
        {
            if (i % 2 == 0)
            {
                float angleY = Mathf.Lerp(upper_Clamp_Angles[i].y, upper_Clamp_Angles[i].y, upper_speed * Time.fixedDeltaTime);

                upper_Current_Angles[i] = new Vector3(upper_Bones[i].transform.rotation.x, angleY, upper_Bones[i].transform.rotation.z);
                //upper_Current_Angles[i] = upper_Bones[i].transform.eulerAngles + Vector3.up * angleY;
            }
            else
            {
                float angleZ = Mathf.Lerp(upper_Clamp_Angles[i].z, upper_Clamp_Angles[i].z, upper_speed * Time.fixedDeltaTime);

                upper_Current_Angles[i] = new Vector3(upper_Bones[i].transform.rotation.x, upper_Bones[i].transform.rotation.y, angleZ);
                //upper_Current_Angles[i] = upper_Bones[i].transform.eulerAngles + Vector3.forward * angleZ;
            }

            upper_Bones[i].localRotation = Quaternion.Euler(upper_Current_Angles[i]);

            if (Mathf.Abs(upper_Current_Angles[i].y - upper_Clamp_Angles[i].y) <= 0.02f && i % 2 == 0)
            {
                upper_Current_Angles[i] = upper_Clamp_Angles[i];
                upper_Bones[i].localRotation = Quaternion.Euler(upper_Current_Angles[i]);
                UpperClampAnglesSetting(i);
            }
            else if (Mathf.Abs(upper_Current_Angles[i].z - upper_Clamp_Angles[i].z) <= 0.02f && i % 2 == 1)
            {
                upper_Current_Angles[i] = upper_Clamp_Angles[i];
                upper_Bones[i].localRotation = Quaternion.Euler(upper_Current_Angles[i]);
                UpperClampAnglesSetting(i);
            }
        }
    }

    private void FixedUpdate()
    {
        BonesMovement();
    }


    #region Test
    //[SerializeField]
    //private Transform[] upper_Bones;
    //[SerializeField]
    //private Transform[] lower_Bones;

    //private float[] upper_Base_Rotations_X;
    //private float[] lower_Base_Rotations_X;

    //private float speed = 1f;
    //private float[] upper_Current_Angles;
    //private float[] upper_Clamp_Angles;
    //private float[] lower_Current_Angles;
    //private float[] lower_Clamp_Angles;


    //private void Start()
    //{
    //    upper_Current_Angles = new float[upper_Bones.Length];
    //    lower_Current_Angles = new float[lower_Bones.Length];
    //    upper_Clamp_Angles = new float[upper_Bones.Length];
    //    lower_Clamp_Angles = new float[lower_Bones.Length];
    //    upper_Base_Rotations_X = new float[upper_Bones.Length];
    //    lower_Base_Rotations_X = new float[lower_Bones.Length];
    //    RandomAngleSettings();
    //}

    //private void Update()
    //{
    //    BonesMoveMent();
    //}

    //private void RandomAngleSettings()
    //{
    //    for (int i = 0; i < upper_Bones.Length; i++)
    //    {
    //        upper_Clamp_Angles[i] = Mathf.Clamp(upper_Bones[i].localPosition.y + Random.Range(-15f, 30f), -30f, 30f);
    //    }

    //    for (int i = 0; i < lower_Bones.Length; i++)
    //    {
    //        lower_Clamp_Angles[i] = Mathf.Clamp(lower_Bones[i].localPosition.y + Random.Range(-15f, 30f), -30f, 30f);
    //    }
    //}

    //private void BonesMoveMent()
    //{
    //    for (int i = 0; i < upper_Bones.Length; i++)
    //    {
    //        upper_Current_Angles[i] = Mathf.Lerp(upper_Current_Angles[i], upper_Clamp_Angles[i], Time.fixedDeltaTime * speed);
    //        upper_Bones[i].localRotation = Quaternion.Euler(upper_Current_Angles[i], 0, 0);
    //    }

    //    for (int i = 0; i < lower_Bones.Length; i++)
    //    {
    //        lower_Current_Angles[i] = Mathf.Lerp(lower_Current_Angles[i], lower_Clamp_Angles[i], Time.fixedDeltaTime * speed);
    //        lower_Bones[i].localRotation = Quaternion.Euler(lower_Current_Angles[i], 0, 0);
    //    }
    //}

    //private void UpperAngleSettings(int index)
    //{
    //    if (upper_Bones.Length < 0 || upper_Bones.Length < index)
    //    {
    //        Debug.LogError("BoneController / UpperAngleSettings / Upper_Bones Index Error");
    //        return;
    //    }

    //    upper_Clamp_Angles[index] = Mathf.Clamp(upper_Bones[index].localPosition.x, )
    //}

    //private void LowerAngleSettings(int index)
    //{

    //}
    #endregion
}
