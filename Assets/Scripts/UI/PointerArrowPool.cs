using System.Linq;

namespace Game.UI
{
    using Game.Utilities;
    using Game.AI;

    /// <summary>
    /// Pool for arrow pointers in UI
    /// </summary>
    public class PointerArrowPool : GenericPool<PointerArrow, PointerArrowProperties>
    {
        /// <summary>
        /// Looks for activated element referencing given enemy and deactivates it
        /// </summary>
        /// <param name="enemy">Referenced enemy</param>
        public void DeactivatePointerReferencingEnemy(Enemy enemy)
        {
            PointerArrow element = _activated.FirstOrDefault(x => x.ReferencedEnemy == enemy);
            if(element != null)
            {
                element.Deactivate();
            }
        }
    }
}