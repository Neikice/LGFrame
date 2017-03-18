using UnityEngine;
using System.Collections;

namespace LGFrame {
    public interface IBattleEnity {
        /// <summary>
        /// 攻击造成伤害时调用
        /// </summary>
        /// <param name="Attack">伤害值</param>
        void NomalAttack(EnityBehaviour target) ;
        /// <summary>
        /// 被伤害是调用
        /// </summary>
        /// <param name="damaged">被伤害值</param>
        int BeDamaged(int damaged , EnityBehaviour attacker);
        /// <summary>
        /// 检查是否死亡
        /// </summary>
        bool DeathAction();
        /// <summary>
        /// 获得当前的HP
        /// </summary>
        int HP { get; set; }
        /// <summary>
        /// 重置数据
        /// </summary>
        void ResetEnityData();
        int GetInt(int index);
        void AddInt(int index,int point);
    }
}
