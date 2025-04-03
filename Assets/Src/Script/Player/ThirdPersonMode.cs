using Unity.Cinemachine;
using UnityEngine;

public class ThirdPersonMode : IPlayerMode
{
    public Vector3 SimulateMovement(float dirX, float dirZ, float moveSpeed, float acceleration, float velPower, float cameraAngle,
                             float playerAngle, float turnSmoothTime, ref float turnSmoothVelocity, ref float rotateAngle, Vector3 velocity)
    {
        Vector3 result;
        if (Mathf.Abs(dirX) > 0f || Mathf.Abs(dirZ) > 0f)
        {

            float targetAngle = Mathf.Atan2(dirX, dirZ) * Mathf.Rad2Deg + cameraAngle;
            targetAngle = Mathf.Abs(targetAngle) < 0.0001f ? 0 : targetAngle;
            float angle = Mathf.SmoothDampAngle(playerAngle, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            angle = Mathf.Abs(angle) < 0.0001f ? 0 : angle;
                        
            rotateAngle = angle;

            Vector3 Dir = Quaternion.Euler(0.0f, targetAngle, 0.0f) * Vector3.forward;

            Vector3 targetVelocity = Dir.normalized * moveSpeed;

            Vector3 VelocityDiff = targetVelocity - velocity;

            float accelRate = acceleration;

            result = new Vector3(Mathf.Pow(Mathf.Abs(VelocityDiff.x) * accelRate, velPower) * Mathf.Sign(VelocityDiff.x), 0.0f,
                        Mathf.Pow(Mathf.Abs(VelocityDiff.z) * accelRate, velPower) * Mathf.Sign(VelocityDiff.z));
        }
        else
        {
            result = Vector3.zero;
        }
        return result;
    }
}
