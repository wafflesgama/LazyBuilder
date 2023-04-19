using Sceelix.Mathematics.Data;

namespace Sceelix.Mathematics.Spatial
{
    public class PointQuadTree : GenericQuadTree<UnityEngine.Vector2>
    {
        public PointQuadTree(int initialPartitionSize, int sectionItemMaxCount)
            : base(initialPartitionSize, sectionItemMaxCount)
        {
        }



        /*protected override UnityEngine.Vector2 GetItemPoint(UnityEngine.Vector2 item)
		{
			return item;
		}

        protected override BoundingRectangle GetItemBoundary(UnityEngine.Vector2 item, float radius)
		{
			var min = item - new UnityEngine.Vector2(radius, radius);
			var max = item + new UnityEngine.Vector2(radius, radius);
			return new BoundingRectangle(min, max);
		}

        protected override bool SectionContains(GenericPartitionTreeSection<BoundingRectangle, UnityEngine.Vector2> section, UnityEngine.Vector2 item)
		{
			return section.Boundary.Contains(item);
		}*/



        protected override BoundingRectangle GetItemBoundary(UnityEngine.Vector2 item)
        {
            return new BoundingRectangle(item, item);
        }
    }
}