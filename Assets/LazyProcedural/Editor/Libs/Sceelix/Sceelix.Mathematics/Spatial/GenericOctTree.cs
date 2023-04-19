using System;
using System.Collections.Generic;
using Sceelix.Mathematics.Data;
using Sceelix.Mathematics.Geometry;

namespace Sceelix.Mathematics.Spatial
{
    public abstract class GenericOctTree<T> : GenericPartitionTree<T, UnityEngine.Vector3, BoundingBox>
    {
        protected GenericOctTree(int initialPartitionSize, int sectionItemMaxCount)
            : base(initialPartitionSize, sectionItemMaxCount)
        {
        }



        protected sealed override int SubsectionsPerSection => 8;



        protected override bool BoundaryContains(BoundingBox boundary, BoundingBox other)
        {
            return boundary.Contains(other);
        }



        protected sealed override bool BoundaryIntersects(BoundingBox boundary, BoundingBox target)
        {
            return boundary.Intersects(target);
        }



        private bool CheckPlanes(BoundingBox boundingBox, BoundingPlanes boundingPlanes)
        {
            return boundingPlanes.Contains(boundingBox);
        }



        protected sealed override GenericPartitionTreeSection<BoundingBox, T> Expand(UnityEngine.Vector3 director)
        {
            var min = Root.Boundary.Min;
            var max = Root.Boundary.Max;
            var size = new UnityEngine.Vector3(Root.Boundary.Width, Root.Boundary.Length, Root.Boundary.Height);
            var newRoot = new GenericPartitionTreeSection<BoundingBox, T>(new BoundingBox(
                UnityEngine.Vector3.Minimize(min, min + director * size),
                UnityEngine.Vector3.Maximize(max, max + director * size)));

            AddSubsection(newRoot, Root);
            for (var x = 0; x < 2; x++)
            for (var y = 0; y < 2; y++)
            for (var z = 0; z < 2; z++)
            {
                if (x == 0 && y == 0 && z == 0)
                    continue;

                var sectionMin = min + director * new UnityEngine.Vector3(x, y, z) * size;
                var sectionMax = max + director * new UnityEngine.Vector3(x, y, z) * size;
                var boundary = new BoundingBox(sectionMin, sectionMax);
                var subsection = new GenericPartitionTreeSection<BoundingBox, T>(boundary);
                AddSubsection(newRoot, subsection);
            }

            if (newRoot == null)
                throw new NullReferenceException();

            return newRoot;
        }



        protected sealed override UnityEngine.Vector3 GetExpandDirector(BoundingBox boundary)
        {
            var delta = boundary.Center - Root.Boundary.Center;
            var signX = Math.Sign(delta.x);
            var signY = Math.Sign(delta.y);
            var signZ = Math.Sign(delta.z);
            return new UnityEngine.Vector3(
                signX == 0 ? 1 : signX,
                signY == 0 ? 1 : signY,
                signZ == 0 ? 1 : signZ);
        }



        public IEnumerable<T> GetItemsBetweenPoints(UnityEngine.Vector3 point0, UnityEngine.Vector3 point1)
        {
            var direction = point1 - point0;
            var plane0 = new Plane3D(-direction, point0);
            var plane1 = new Plane3D(direction, point1);

            var boundingPlane = new BoundingPlanes(plane0, plane1);

            return GetItemsWithinShape(boundingPlane, CheckPlanes);
        }



        public override IEnumerable<T> GetItemsWithinRadius(UnityEngine.Vector3 point, float radius)
        {
            var min = point - new UnityEngine.Vector3(radius, radius, radius);
            var max = point + new UnityEngine.Vector3(radius, radius, radius);

            return GetItemsWithinBoundary(new BoundingBox(min, max));
        }



        protected sealed override BoundingBox GetSectionBoundary(BoundingBox boundary, int size)
        {
            var ix = (int) boundary.Center.x;
            var iy = (int) boundary.Center.y;
            var iz = (int) boundary.Center.z;

            var rx = Math.Abs(ix) % size;
            var ry = Math.Abs(iy) % size;
            var rz = Math.Abs(iz) % size;

            var min = new UnityEngine.Vector3(ix - rx, iy - ry, iz - rz);
            var max = new UnityEngine.Vector3(ix + size - rx, iy + size - ry, iz + size - rz);

            return new BoundingBox(min, max);
        }



        protected sealed override void Subdivide(GenericPartitionTreeSection<BoundingBox, T> section)
        {
            var box = section.Boundary;
            var min = box.Min;
            var halfSize = new UnityEngine.Vector3(box.Width, box.Length, box.Height) * 0.5f;

            for (var x = 0; x < 2; x++)
            for (var y = 0; y < 2; y++)
            for (var z = 0; z < 2; z++)
            {
                var sectionMin = min + new UnityEngine.Vector3(x, y, z) * halfSize;
                var sectionMax = min + new UnityEngine.Vector3(x + 1, y + 1, z + 1) * halfSize;
                var boundary = new BoundingBox(sectionMin, sectionMax);
                var subsection = new GenericPartitionTreeSection<BoundingBox, T>(boundary);
                AddSubsection(section, subsection);
            }

            RedistributeItemsWithinSubsections(section);
        }
    }
}