using System;
using Sceelix.Mathematics.Data;

namespace Sceelix.Mathematics.Spatial
{
    public class PointOctTree : GenericOctTree<UnityEngine.Vector3>
    {
        public PointOctTree(int initialPartitionSize, int sectionItemMaxCount) : base(initialPartitionSize, sectionItemMaxCount)
        {
        }



        public bool Contains(BoundingBox box, UnityEngine.Vector3 point)
        {
            throw new NotImplementedException();
        }



        public bool Contains(BoundingSphere box, UnityEngine.Vector3 point)
        {
            throw new NotImplementedException();
        }



        /*protected override UnityEngine.Vector3 GetItemPoint(UnityEngine.Vector3 item)
		{
			return item;
		}

		protected override BoundingBox GetItemBoundary(UnityEngine.Vector3 item, float radius)
		{
			var min = item - new UnityEngine.Vector3(radius, radius, radius);
			var max = item + new UnityEngine.Vector3(radius, radius, radius);
			return new BoundingBox(min, max);
		}

        protected override bool SectionContains(GenericPartitionTreeSection<BoundingBox, UnityEngine.Vector3> section, UnityEngine.Vector3 item)
		{
			return section.Boundary.Contains(item);
		}*/



        protected override BoundingBox GetItemBoundary(UnityEngine.Vector3 item)
        {
            return new BoundingBox(item, item);
        }
    }
}