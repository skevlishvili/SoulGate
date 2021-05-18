﻿using FirstGearGames.Utilities.Networks;
using FirstGearGames.Utilities.Objects;
#if MIRROR
using Mirror;
#elif MIRAGE
using Mirage;
#endif
using UnityEngine;
using UnityEngine.Serialization;

namespace FirstGearGames.Mirrors.Assets.FlexNetworkTransforms
{


    /// <summary>
    /// A component to synchronize the position of child transforms of networked objects.
    /// <para>There must be a NetworkTransform on the root object of the hierarchy. There can be multiple NetworkTransformChild components on an object. This does not use physics for synchronization, it simply synchronizes the localPosition and localRotation of the child transform and lerps towards the recieved values.</para>
    /// </summary>
    public class FlexNetworkTransformChild : FlexNetworkTransformBase
    {
#region Serialized.
        /// <summary>
        /// Transform to synchronize.
        /// </summary>
        [FormerlySerializedAs("Target")]
        [SerializeField]
        private Transform _target = null;
#endregion

#region Public.
        /// <summary>
        /// Transform to synchronize.
        /// </summary>
        public override Transform TargetTransform => _target;
#endregion

        protected override void OnEnable()
        {
            base.OnEnable();
        }
        protected override void OnDisable()
        {
            base.OnDisable();
        }

        //TODO Delete tis if checks pass.
        //public override bool OnSerialize(NetworkWriter writer, bool initialState)
        //{
        //    if (initialState)
        //    {
        //        writer.WriteVector3(TargetTransform.GetPosition(base.UseLocalSpace));
        //        writer.WriteUInt32(Quaternions.CompressQuaternion(TargetTransform.GetRotation(base.UseLocalSpace)));
        //        writer.WriteVector3(TargetTransform.GetScale());
        //    }
        //    return base.OnSerialize(writer, initialState);
        //}
        //public override void OnDeserialize(NetworkReader reader, bool initialState)
        //{
        //    if (initialState)
        //    {
        //        TargetTransform.SetPosition(base.UseLocalSpace, reader.ReadVector3());
        //        TargetTransform.SetRotation(base.UseLocalSpace, Quaternions.DecompressQuaternion(reader.ReadUInt32()));
        //        TargetTransform.SetScale(reader.ReadVector3());
        //    }
        //    base.OnDeserialize(reader, initialState);
        //}


    }

}

