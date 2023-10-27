/*  Author's Name:          Marcus Ngooi
 *  Last Modified By:       Marcus Ngooi
 *  Date Last Modified:     October 26, 2023
 *  Program Description:    Base class for a Buff.
 *  Revision History:       October 26, 2023: Initial Buff script.
 */


public class TEMP_Buff : Skill
{
    public BuffType Type { get; private set; }
    public TEMP_Buff(BuffType type, int maxLevel) : base(type.ToString(), maxLevel)
    {
        this.Type = type;
    }
}
