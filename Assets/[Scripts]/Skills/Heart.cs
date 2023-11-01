/* Author's Name:          Ikamjot Hundal
 * Last Modified By:       Ikamjot Hundal
 * Date Last Modified:     November 1st, 2023 
 * Description:            Child Class to TEMP_Buff.cs for managing Health
 * ------------------------------------------------------------------------
 * Revision History:       October 26th, 2023: Initial Heart script.
 *                         October 28th, 2023: Updated the script to be the child of TEMP_Buff
 *                         October 30th 2023: Changed to override method
 *                         November 1st, 2023: Setting the BuffType
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : TEMP_Buff
{
    [SerializeField] private float playerHealth = 100;
    [SerializeField] private float increaseHealthPercentage = 0.5f;

    public new BuffType Type { private get; set; }

    public Heart(BuffType type, int maxLevel) : base(type, maxLevel)
    {
        type = BuffType.Heart;
        Type = type;
    }

    public override void ApplyBuff()
    {
        playerHealth += increaseHealthPercentage;
        Debug.Log(playerHealth);
    }
}
