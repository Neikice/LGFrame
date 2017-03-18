using UnityEngine;
using System.Collections;

namespace LGFrame
{
    public interface IQuest
    {
        /// <summary>
        /// 更新任务状态
        /// </summary>
        bool Update();

        /// <summary>
        /// 检查是否状态
        /// </summary>
        /// <returns></returns>
        bool Check();
    }
}
