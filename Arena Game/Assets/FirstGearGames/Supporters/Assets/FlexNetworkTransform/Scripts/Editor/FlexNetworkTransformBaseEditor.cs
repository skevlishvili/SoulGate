﻿#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace FirstGearGames.Mirrors.Assets.FlexNetworkTransforms.Editors
{

    [CustomEditor(typeof(FlexNetworkTransformBase), true)]
    public class FlexNetworkTransformBaseEditor : Editor
    {
        private SerializedProperty _useLocalSpace;

        private SerializedProperty _intervalType;
        private SerializedProperty _synchronizeInterval;

        private SerializedProperty _reliable;
        private SerializedProperty _resendUnreliable;

        private SerializedProperty _smoothingLoop;
        private SerializedProperty _interpolationFallbehind;
        private SerializedProperty _extrapolationSpan;

        private SerializedProperty _teleportThreshold;

        private SerializedProperty _clientAuthoritative;
        private SerializedProperty _synchronizeToOwner;

        private SerializedProperty _compressSmall;

        private SerializedProperty _synchronizePosition;
        private SerializedProperty _snapPosition;

        private SerializedProperty _synchronizeRotation;
        private SerializedProperty _snapRotation;

        private SerializedProperty _synchronizeScale;
        private SerializedProperty _snapScale;

        protected virtual void OnEnable()
        {
            _useLocalSpace = serializedObject.FindProperty("_useLocalSpace");

            _intervalType = serializedObject.FindProperty("_intervalType");

            _synchronizeInterval = serializedObject.FindProperty("_synchronizeInterval");

            _reliable = serializedObject.FindProperty("_reliable");
            _resendUnreliable = serializedObject.FindProperty("_resendUnreliable");

            _smoothingLoop = serializedObject.FindProperty("_smoothingLoop");
            _interpolationFallbehind = serializedObject.FindProperty("_interpolationFallbehind");
            _extrapolationSpan = serializedObject.FindProperty("_extrapolationSpan");

            _teleportThreshold = serializedObject.FindProperty("_teleportThreshold");

            _clientAuthoritative = serializedObject.FindProperty("_clientAuthoritative");
            _synchronizeToOwner = serializedObject.FindProperty("_synchronizeToOwner");

            _compressSmall = serializedObject.FindProperty("_compressSmall");

            _synchronizePosition = serializedObject.FindProperty("_synchronizePosition");
            _snapPosition = serializedObject.FindProperty("_snapPosition");

            _synchronizeRotation = serializedObject.FindProperty("_synchronizeRotation");
            _snapRotation = serializedObject.FindProperty("_snapRotation");

            _synchronizeScale = serializedObject.FindProperty("_synchronizeScale");
            _snapScale = serializedObject.FindProperty("_snapScale");
        }

        public override void OnInspectorGUI()
        {
            //base.OnInspectorGUI();

            FlexNetworkTransformBase data = (FlexNetworkTransformBase)target;

            serializedObject.Update();
            EditorGUI.BeginChangeCheck();
            //EditorGUILayout.HelpBox("Experimental Release", MessageType.Warning);
            EditorGUILayout.HelpBox("Recommended defaults: Interpolation Fallbehind 0.04, Extrapolation Span between 0 and 0.1. If Reliable, Timing of 0.05 - 0.1. If Unreliable, Timing of FixedUpdate.", MessageType.Info);

            //Space.
            EditorGUILayout.LabelField("Space", EditorStyles.boldLabel);
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(_useLocalSpace, new GUIContent("Use LocalSpace", "True to synchronize using LocalSpace values. False to use WorldSpace."));
            EditorGUI.indentLevel--;
            EditorGUILayout.Space();


            //Timing.
            EditorGUILayout.LabelField("Network", EditorStyles.boldLabel);
            EditorGUI.indentLevel++;

            //If not reliable and interval type is set to timedd.
            if (!_reliable.boolValue && _intervalType.intValue == 0)
                EditorGUILayout.HelpBox("For best results use FixedUpdate Interval Type when using unreliable messages.", MessageType.Warning);
            //If reliable and interval is set to fixed update.
            if (_reliable.boolValue && _intervalType.intValue == 1)
                EditorGUILayout.HelpBox("Using FixedUpate with a reliable transport may cause network disruptions for those with unstable connections.", MessageType.Warning);
            EditorGUILayout.PropertyField(_intervalType, new GUIContent("Interval Type", "How to operate synchronization timings. Timed will synchronized every specified interval while FixedUpdate will synchronize every FixedUpdate."));
            if (_intervalType.intValue == 0)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(_synchronizeInterval, new GUIContent("Synchronize Interval", "How often to synchronize this transform."));
                EditorGUI.indentLevel--;
            }
            EditorGUILayout.PropertyField(_reliable, new GUIContent("Reliable", "True to synchronize using the reliable channel. False to synchronize using the unreliable channel. Your project must use 0 as reliable, and 1 as unreliable for this to function properly. This feature is not supported on TCP transports."));
            if (_reliable.boolValue == false)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(_resendUnreliable, new GUIContent("Resend Unreliable", "True to synchronize using the reliable channel. False to synchronize using the unreliable channel. Your project must use 0 as reliable, and 1 as unreliable for this to function properly. This feature is not supported on TCP transports."));
                EditorGUI.indentLevel--;
            }
            EditorGUI.indentLevel--;
            EditorGUILayout.Space();


            //Smoothing
            EditorGUILayout.LabelField("Smoothing", EditorStyles.boldLabel);
            EditorGUI.indentLevel++;

            EditorGUILayout.PropertyField(_smoothingLoop, new GUIContent("Smoothing Loop", "When to move towards latest values received from the server."));
            EditorGUILayout.PropertyField(_interpolationFallbehind, new GUIContent("Interpolation Fallbehind", "How far in the past objects should be for interpolation. Higher values will result in smoother movement with network fluctuations but lower values will result in objects being closer to their actual position. Lower values can generally be used for longer synchronization intervalls."));
            EditorGUILayout.PropertyField(_extrapolationSpan, new GUIContent("Extrapolation Span", "How long to extrapolate when data is expected but does not arrive. Smaller values are best for fast synchronization intervals. For precision or fast reaction games you may want to use no extrapolation or only one or two synchronization intervals worth. Extrapolation is client-side only."));

            EditorGUILayout.PropertyField(_teleportThreshold, new GUIContent("Teleport Threshold", "Teleport the transform if the distance between received data exceeds this value. Use 0f to disable."));
            EditorGUI.indentLevel--;
            EditorGUILayout.Space();


            //Authority.
            EditorGUILayout.LabelField("Authority", EditorStyles.boldLabel);
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(_clientAuthoritative, new GUIContent("Client Authoritative", "True if using client authoritative movement."));
            if (_clientAuthoritative.boolValue == false)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(_synchronizeToOwner, new GUIContent("Synchronize To Owner", "True to synchronize server results back to owner. Typically used when you are sending inputs to the server and are relying on the server response to move the transform."));
                EditorGUI.indentLevel--;
            }
            EditorGUI.indentLevel--;
            EditorGUILayout.Space();


            //Synchronize Properties.
            EditorGUILayout.LabelField("Properties", EditorStyles.boldLabel);
            EditorGUI.indentLevel++;

            EditorGUILayout.PropertyField(_compressSmall, new GUIContent("Compress Small Values", "True to compress small values on position and scale. Values will be rounded to the hundredth decimal place, eg: 102.12f."));

            EditorGUILayout.PropertyField(_synchronizePosition, new GUIContent("Position", "Synchronize options for position."));
            if (_synchronizePosition.intValue == 0)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(_snapPosition, new GUIContent("Snap Position", "Euler axes on the position to snap into place rather than move towards over time."));
                EditorGUI.indentLevel--;
            }

            EditorGUILayout.PropertyField(_synchronizeRotation, new GUIContent("Rotation", "Synchronize options for position."));
            if (_synchronizeRotation.intValue == 0)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(_snapRotation, new GUIContent("Snap Rotation", "Euler axes on the rotation to snap into place rather than move towards over time."));
                EditorGUI.indentLevel--;
            }

            EditorGUILayout.PropertyField(_synchronizeScale, new GUIContent("Scale", "Synchronize options for scale."));
            if (_synchronizeScale.intValue == 0)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(_snapScale, new GUIContent("Snap Scale", "Euler axes on the scale to snap into place rather than move towards over time."));
                EditorGUI.indentLevel--;
            }

            if (EditorGUI.EndChangeCheck())
            {
                data.SetSnapPosition((Vector3Axes)_snapPosition.intValue);
                data.SetSnapRotation((Vector3Axes)_snapRotation.intValue);
                data.SetSnapScale((Vector3Axes)_snapScale.intValue);
            }

            serializedObject.ApplyModifiedProperties();
        }
    }

}
#endif