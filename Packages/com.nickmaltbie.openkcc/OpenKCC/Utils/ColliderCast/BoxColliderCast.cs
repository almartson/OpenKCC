﻿// Copyright (C) 2023 Nicholas Maltbie
//
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and
// associated documentation files (the "Software"), to deal in the Software without restriction,
// including without limitation the rights to use, copy, modify, merge, publish, distribute,
// sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all copies or
// substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING
// BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY
// CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
// ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace nickmaltbie.OpenKCC.Utils.ColliderCast
{
    /// <summary>
    /// ColliderCast behaviour intended to work with any box collider shape.
    /// </summary>
    [RequireComponent(typeof(BoxCollider))]
    public class BoxColliderCast : AbstractPrimitiveColliderCast
    {
        /// <summary>
        /// Box collider associated with this object.
        /// </summary>
        private BoxCollider _boxCollider;

        /// <summary>
        /// Box Collider associated with this object.
        /// </summary>
        internal BoxCollider BoxCollider => _boxCollider = _boxCollider ?? GetComponent<BoxCollider>();

        /// <inheritdoc/>
        public override Collider Collider => BoxCollider;

        /// <summary>
        /// Gets transformed parameters describing this sphere collider for a given position and rotation
        /// </summary>
        /// <param name="boxCenter">Center of box relative to position.</param>
        /// <param name="boxSize">Size of the box collider.</param>
        /// <param name="position">Position of the object.</param>
        /// <param name="rotation">Rotation of the object.</param>
        /// <param name="buffer">Buffer space around the object.</param>
        /// <returns>Returns the center of the collider in world space
        /// and the size along each axis.</returns>
        public static (Vector3, Vector3) GetParams(Vector3 boxCenter, Vector3 boxSize, Vector3 position, Quaternion rotation, float buffer = 0)
        {
            Vector3 center = rotation * boxCenter + position;
            Vector3 size = boxSize + Vector3.one * buffer * 2;

            return (center, size);
        }

        /// <summary>
        /// Gets transformed parameters describing this sphere collider for a given position and rotation
        /// </summary>
        /// <param name="position">Position of the object.</param>
        /// <param name="rotation">Rotation of the object.</param>
        /// <param name="buffer">Buffer space around the object.</param>
        /// <returns>Returns the center of the collider in world space
        /// and the size along each axis.</returns>
        public (Vector3, Vector3) GetParams(Vector3 position, Quaternion rotation, float buffer = 0)
        {
            return GetParams(BoxCollider.center, BoxCollider.size, position, rotation, buffer);
        }

        /// <inheritdoc/>
        public override IEnumerable<Collider> GetOverlapping(
            Vector3 position,
            Quaternion rotation,
            int layerMask = RaycastHelperConstants.DefaultLayerMask,
            QueryTriggerInteraction queryTriggerInteraction = RaycastHelperConstants.DefaultQueryTriggerInteraction)
        {
            (Vector3 center, Vector3 size) = GetParams(position, rotation);
            return Physics
                .OverlapBox(center, size / 2, rotation, layerMask, queryTriggerInteraction)
                .Where(c => c.transform != transform);
        }

        /// <inheritdoc/>
        public override IEnumerable<RaycastHit> GetHits(
            Vector3 position,
            Quaternion rotation,
            Vector3 direction,
            float distance,
            int layerMask = RaycastHelperConstants.DefaultLayerMask,
            QueryTriggerInteraction queryTriggerInteraction = RaycastHelperConstants.DefaultQueryTriggerInteraction)
        {
            (Vector3 center, Vector3 size) = GetParams(position, rotation, -KCCUtils.Epsilon);
            return Physics.BoxCastAll(center, size / 2, direction, rotation, distance, layerMask, queryTriggerInteraction)
                .Where(hit => hit.collider.transform != transform);
        }

        /// <inheritdoc/>
        public override Vector3 GetBottom(Vector3 position, Quaternion rotation)
        {
            (Vector3 center, Vector3 size) = GetParams(position, rotation, -KCCUtils.Epsilon);
            return center + rotation * Vector3.down * size.y / 2;
        }
    }
}
