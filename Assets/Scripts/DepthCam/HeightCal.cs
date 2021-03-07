using Astra;
using System;
public class HeightCal
{
    private static HeightCal _instance;
    public static HeightCal Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new HeightCal();
            }
            return _instance;
        }
    }
    public double GetStableBodyHeight(Body body, double ratio = 3.0, double padding = 10.0)
    {
        double current_height = GetBodyHeight(body, padding);
        if (Math.Abs(current_height - -1.0) < 0.001)
        {
            old_height = 0;
            num_of_stable_height = 0;
            return -1.0;
        }
        if (Math.Abs(old_height - 0.0) < 0.001)
        {
            old_height = current_height;
            return -1.0;
        }

        double diff = Math.Abs(old_height - current_height);
        if (diff <= ratio && num_of_stable_height < 10)
        {
            ++num_of_stable_height;
            old_height = current_height;
            return -1.0;
        }
        else if (diff <= ratio && num_of_stable_height >= 10)
        {
            old_height = 0;
            num_of_stable_height = 0;
            return current_height;
        }
        old_height = 0;
        num_of_stable_height = 0;
        return -1.0;
    }
    public double GetBodyHeight(Body body, double padding = 10.0)
    {


        //获取各个分关节点
        Astra.Joint head = body.Joints[(int)JointType.Head];
        if (head.Status == JointStatus.NotTracked)
            return -1.0;
        Astra.Joint shoulder_spine = body.Joints[(int)JointType.ShoulderSpine];
        if (shoulder_spine.Status == JointStatus.NotTracked)
            return -1.0;
        Astra.Joint mid_spine = body.Joints[(int)JointType.MidSpine];
        if (mid_spine.Status == JointStatus.NotTracked)
            return -1.0;
        Astra.Joint base_spine = body.Joints[(int)JointType.BaseSpine];
        if (base_spine.Status == JointStatus.NotTracked)
            return -1.0;
        Astra.Joint left_hip = body.Joints[(int)JointType.LeftHip];
        if (left_hip.Status == JointStatus.NotTracked)
            return -1.0;
        Astra.Joint left_knee = body.Joints[(int)JointType.LeftKnee];
        if (left_knee.Status == JointStatus.NotTracked)
            return -1.0;
        Astra.Joint left_foot = body.Joints[(int)JointType.LeftFoot];
        if (left_foot.Status == JointStatus.NotTracked)
            return -1.0;
        Astra.Joint right_hip = body.Joints[(int)JointType.RightHip];
        if (right_hip.Status == JointStatus.NotTracked)
            return -1.0;
        Astra.Joint right_knee = body.Joints[(int)JointType.RightKnee];
        if (right_knee.Status == JointStatus.NotTracked)
            return -1.0;
        Astra.Joint right_foot = body.Joints[(int)JointType.RightFoot];
        if (right_foot.Status == JointStatus.NotTracked)
            return -1.0;
        Astra.Joint neck = body.Joints[(int)JointType.Neck];
        if (neck.Status == JointStatus.NotTracked)
            return -1.0;
        Astra.Joint[] left_leg_joints, right_leg_joints;
        left_leg_joints = new Astra.Joint[3] { left_hip, left_knee, left_foot };
        right_leg_joints = new Astra.Joint[3] { right_hip, right_knee, right_foot };

        int left_leg_quality = NumberOfTrackedJoints(left_leg_joints);
        int right_leg_quality = NumberOfTrackedJoints(right_leg_joints);

        double leg_length = left_leg_quality > right_leg_quality ?
        BoneLength(left_hip, left_knee) + BoneLength(left_knee, left_foot)
        : BoneLength(right_hip, right_knee) + BoneLength(right_knee, right_foot);

        double body_length = 2 * BoneLength(head, neck) + BoneLength(neck, mid_spine) + BoneLength(mid_spine, base_spine)
            + BoneLength(base_spine, left_hip);

        return body_length + leg_length + padding;
    }

    private double BoneLength(Astra.Joint j1, Astra.Joint j2)
    {
        Vector3D j1_world_pt = j1.WorldPosition;
        Vector3D j2_world_pt = j2.WorldPosition;
        Vector3D result = new Vector3D(j1_world_pt.X - j2_world_pt.X, j1_world_pt.Y - j2_world_pt.Y, j1_world_pt.Z - j2_world_pt.Z);

        return Math.Sqrt(result.X * result.X + result.Y * result.Y + result.Z * result.Z);
    }
    private int NumberOfTrackedJoints(Astra.Joint[] joints)
    {
        int num_of_tracked = 0;
        //遍历关节数组，计算关节数组质量
        //tracked = 2, low_confidence = 1 , 
        foreach (var joint in joints)
        {
            num_of_tracked += (int)joint.Status;
        }

        return num_of_tracked;
    }

    private int num_of_stable_height = 0;
    private double old_height = 0;

}
