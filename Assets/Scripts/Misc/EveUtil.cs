using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EveTravel
{
    public static class EveUtil
    {
        static public bool IsIn180Angle(Vector3 source, Vector3 target, Vector3 compare)
        {
            return Vector3.Dot(target - source, compare - source) > 0f;
        }

        static public void DamageSet(Character attacker, Character target)
        {

        }
    }
}
