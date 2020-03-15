using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Code
{
    static class FloatExtensions
    {
        public static int Round(this float val)
        {
            return Mathf.RoundToInt(val * 1.0001f);
        }
    }
}
