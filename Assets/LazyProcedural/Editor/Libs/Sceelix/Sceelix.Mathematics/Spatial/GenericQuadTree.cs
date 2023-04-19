using System;
using System.Collections.Generic;
using Sceelix.Mathematics.Data;

namespace Sceelix.Mathematics.Spatial
{
    public abstract class GenericQuadTree<T> : GenericPartitionTree<T, UnityEngine.Vector2, BoundingRectangle>
    {
        protected GenericQuadTree(int initialPartitionSize, int sectionItemMaxCount)
            : base(initialPartitionSize, sectionItemMaxCount)
        {
        }



        protected sealed override int SubsectionsPerSection => 4;



        protected sealed override bool BoundaryContains(BoundingRectangle boundary, BoundingRectangle other)
        {
            return boundary.Contains(other);
        }



        protected sealed override bool BoundaryIntersects(BoundingRectangle boundary, BoundingRectangle target)
        {
            return boundary.Intersects(target);
        }



        protected sealed override GenericPartitionTreeSection<BoundingRectangle, T> Expand(UnityEngine.Vector2 director)
        {
            var min = Root.Boundary.Min;
            var max = Root.Boundary.Max;
            var size = new UnityEngine.Vector2(Root.Boundary.Width, Root.Boundary.Height);
            var newMin = UnityEngine.Vector2.Minimize(min, min + director * size);
            var newMax = UnityEngine.Vector2.Maximize(max, max + director * size);
            var newRoot = new GenericPartitionTreeSection<BoundingRectangle, T>(new BoundingRectangle(newMin, newMax));

            AddSubsection(newRoot, Root);
            for (var x = 0; x < 2; x++)
            for (var y = 0; y < 2; y++)
            {
                if (x == 0 && y == 0)
                    continue;

                var sectionMin = min + director * new UnityEngine.Vector2(x, y) * size;
                var sectionMax = max + director * new UnityEngine.Vector2(x, y) * size;
                var boundary = new BoundingRectangle(sectionMin, sectionMax);
                var subsection = new GenericPartitionTreeSection<BoundingRectangle, T>(boundary);
                AddSubsection(newRoot, subsection);
            }

            if (newRoot == null)
                throw new NullReferenceException();

            return newRoot;
        }



        protected sealed override UnityEngine.Vector2 GetExpandDirector(BoundingRectangle boundary)
        {
            var delta = boundary.Center - Root.Boundary.Center;
            var signX = Math.Sign(delta.x);
            var signY = Math.Sign(delta.y);
            return new UnityEngine.Vector2(
                signX == 0 ? 1 : signX,
                signY == 0 ? 1 : signY);
        }



        public override IEnumerable<T> GetItemsWithinRadius(UnityEngine.Vector2 point, float radius)
        {
            var min = point - new UnityEngine.Vector2(radius, radius);
            var max = point + new UnityEngine.Vector2(radius, radius);

            return GetItemsWithinBoundary(new BoundingRectangle(min, max));
        }



        protected sealed override BoundingRectangle GetSectionBoundary(BoundingRectangle boundary, int size)
        {
            var ix = (int) boundary.Center.x;
            var iy = (int) boundary.Center.y;

            var rx = Math.Abs(ix) % size;
            var ry = Math.Abs(iy) % size;

            var min = new UnityEngine.Vector2(ix - rx, iy - ry);
            var max = new UnityEngine.Vector2(ix + size - rx, iy + size - ry);

            return new BoundingRectangle(min, max);
        }



        protected sealed override void Subdivide(GenericPartitionTreeSection<BoundingRectangle, T> section)
        {
            var min = section.Boundary.Min;
            var halfSize = new UnityEngine.Vector2(section.Boundary.Width, section.Boundary.Height) * 0.5f;

            for (var x = 0; x < 2; x++)
            for (var y = 0; y < 2; y++)
            {
                var sectionMin = min + new UnityEngine.Vector2(x, y) * halfSize;
                var sectionMax = min + new UnityEngine.Vector2(x + 1, y + 1) * halfSize;
                var boundary = new BoundingRectangle(sectionMin, sectionMax);
                var subsection = new GenericPartitionTreeSection<BoundingRectangle, T>(boundary);
                AddSubsection(section, subsection);
            }

            RedistributeItemsWithinSubsections(section);
        }
    }
}