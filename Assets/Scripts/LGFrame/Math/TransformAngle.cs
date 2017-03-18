using UnityEngine;
using System.Collections;
using UniRx;

namespace LGFrame
{
    public class TransformAngle : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {
            Vector3 originRotation = new Vector3(-30, 0, 0);

            var temp = this.transform.ObserveEveryValueChanged(x =>
            x.localEulerAngles = LockAngle(x.eulerAngles.y))
               //var temp = this.transform.ObserveEveryValueChanged(x => x.transform.eulerAngles)
               .TakeUntilDisable(this)
               .Subscribe();
               // .Subscribe(x => Debug.Log("localEulerAngles => " + x));
        }

        Vector3 LockAngle(float y)
        {
            float x = 0, z = 0;
            if (y <= 90)
            {
                x = accangle(-30, 0, y,0);
                z = accangle(0, -30, y,0);
            }
            else if (y > 90 && y <= 180)
            {
                x = accangle(0, 30, y,90);
                z = accangle(-30, 0, y,90);
            }
            else if (y > 180 && y <= 270)
            {
                x = accangle(30, 0, y,180);
                z = accangle(0, 30, y,180);
            }
            else if (y > 270 && y < 360)
            {
                x = accangle(0, -30, y,270);
                z = accangle(30, 0, y,270);
            }
          //  Debug.ULogChannel("角度", "原始Y{0} => 输出{1}", y, new Vector3(x, 0, z));
            return new Vector3(x, 0, z);
        }

        float accangle(float orign, float max, float y,float ymin)
        {
            return orign + (y-ymin) * (max - orign) / 90f;
        }
    }
}
