#if UNITY_EDITOR
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AnimatorController = UnityEditor.Animations.AnimatorController;

#if VRC_SDK_VRCSDK3
using VRCAvatarDescriptor = VRC.SDK3.Avatars.Components.VRCAvatarDescriptor;
using VRCContactReceiver = VRC.SDK3.Dynamics.Contact.Components.VRCContactReceiver;
using VRCContactReceiverType = VRC.SDK3.Dynamics.Contact.Components.VRCContactReceiver.ReceiverType;
using ExpressionParameters = VRC.SDK3.Avatars.ScriptableObjects.VRCExpressionParameters;
using ExpressionParameter = VRC.SDK3.Avatars.ScriptableObjects.VRCExpressionParameters.Parameter;
#endif

namespace Shadoki
{
    public class OWOSuit : EditorWindow
    {
        private string[] suitParts = {
            "owo_suit_Pectoral_R",
            "owo_suit_Pectoral_L",
            "owo_suit_Abdominal_R",
            "owo_suit_Abdominal_L",
            "owo_suit_Arm_R",
            "owo_suit_Arm_L",
            "owo_suit_Dorsal_R",
            "owo_suit_Dorsal_L",
            "owo_suit_Lumbar_R",
            "owo_suit_Lumbar_L",
        };
        private VRCAvatarDescriptor avatar = null;
        private AnimatorController fxLayer = null;
        [MenuItem("Shadoki/OWO Suit")]
        static void ShowWindow()
        {
            EditorWindow.GetWindow<OWOSuit>();
        }

        void OnGUI()
        {
            GUILayout.Space(5);
            GUILayout.Label("Avatar", EditorStyles.label);
            avatar = EditorGUILayout.ObjectField(avatar, typeof(VRCAvatarDescriptor), true) as VRCAvatarDescriptor;
            fxLayer = GetFXLayerFromDescriptor(avatar);
            if (avatar != null && avatar.expressionParameters == null)
            {
                GUILayout.Label("Your avatar is missing it's expression parameters, please add them.", EditorStyles.boldLabel);
            }
            if (avatar != null && fxLayer == null)
            {
                GUILayout.Label("Your avatar is missing it's FX Layer animator controller, please add it.", EditorStyles.boldLabel);
            }
            if (avatar != null && avatar.expressionParameters != null && fxLayer != null)
            {
                GUILayout.Space(5);
                GUILayout.Label("This will modify your existing expression parameters and FX Layer animator controller.", EditorStyles.label);
                GUILayout.Space(5);
                if (GUILayout.Button("Add"))
                {
                    HandleAdd();
                }
                if (GUILayout.Button("Remove"))
                {
                    HandleRemove();
                }
            }
            GUILayout.Space(5);
        }

        private void HandleAdd()
        {
            AddExpressionParameters(avatar.expressionParameters);
            AddOWOSuitParts(avatar);
            AddAnimatorControllerParameters(fxLayer);
        }

        private void HandleRemove()
        {
            RemoveExpressionParameters(avatar.expressionParameters);
            RemoveOWOSuitParts(avatar);
            RemoveAnimatorControllerParameters(fxLayer);
        }

        private AnimatorController GetFXLayerFromDescriptor(VRCAvatarDescriptor avatar)
        {
            if (!avatar) return null;
            foreach (VRCAvatarDescriptor.CustomAnimLayer animLayer in avatar.baseAnimationLayers)
            {
                if (animLayer.type == VRCAvatarDescriptor.AnimLayerType.FX)
                {
                    if (animLayer.animatorController != null)
                    {
                        return (AnimatorController)animLayer.animatorController;
                    }
                }
            }
            return null;
        }

        private void AddAnimatorControllerParameters(AnimatorController controller)
        {
            List<AnimatorControllerParameter> parameters = new List<AnimatorControllerParameter>();
            parameters.AddRange(controller.parameters);
            foreach (var suitPart in suitParts)
            {
                if (!parameters.Exists(p => p.name == suitPart))
                {
                    parameters.Add(new AnimatorControllerParameter()
                    {
                        name = suitPart,
                        type = AnimatorControllerParameterType.Bool,
                        defaultBool = false,
                    });
                }
            }
            controller.parameters = parameters.ToArray();
        }

        private void RemoveAnimatorControllerParameters(AnimatorController controller)
        {
            List<AnimatorControllerParameter> parameters = new List<AnimatorControllerParameter>();
            parameters.AddRange(controller.parameters);
            foreach (var suitPart in suitParts)
            {
                var param = parameters.Find(p => p.name == suitPart && p.type == AnimatorControllerParameterType.Bool);
                if (param != null)
                {
                    parameters.Remove(param);
                }
            }
            controller.parameters = parameters.ToArray();
        }

        private void AddOWOSuitParts(VRCAvatarDescriptor avatar)
        {
            var children = new List<VRCContactReceiver>();
            children.AddRange(avatar.transform.GetComponentsInChildren<VRCContactReceiver>(true));
            foreach (var suitPart in suitParts)
            {
                if (!children.Exists(contact => contact.transform.name == suitPart && contact.parameter == suitPart))
                {
                    var gameObject = CreateOWOSuitObject(suitPart);
                    gameObject.transform.parent = avatar.transform;
                }
            }
        }

        private void RemoveOWOSuitParts(VRCAvatarDescriptor avatar)
        {
            foreach (var suitPart in suitParts)
            {
                var children = new List<VRCContactReceiver>();
                children.AddRange(avatar.transform.GetComponentsInChildren<VRCContactReceiver>(true));
                var c = children.Find(contact => contact.transform.name == suitPart && contact.parameter == suitPart);
                if (c != null)
                {
                    DestroyImmediate(c.transform.gameObject);
                }
            }
        }

        private void AddExpressionParameters(ExpressionParameters vrcExpressionParameters)
        {
            List<ExpressionParameter> parameters = new List<ExpressionParameter>();
            parameters.AddRange(vrcExpressionParameters.parameters);
            foreach (var suitPart in suitParts)
            {
                if (!parameters.Exists(p => p.name == suitPart && p.valueType == ExpressionParameters.ValueType.Bool))
                {
                    parameters.Add(new ExpressionParameter()
                    {
                        name = suitPart,
                        valueType = ExpressionParameters.ValueType.Bool,
                        saved = false,
                    });
                }
            }
            vrcExpressionParameters.parameters = parameters.ToArray();
        }
        private void RemoveExpressionParameters(ExpressionParameters vrcExpressionParameters)
        {
            List<ExpressionParameter> parameters = new List<ExpressionParameter>();
            parameters.AddRange(vrcExpressionParameters.parameters);
            foreach (var suitPart in suitParts)
            {
                var param = parameters.Find(p => p.name == suitPart && p.valueType == ExpressionParameters.ValueType.Bool);
                if (param != null)
                {
                    parameters.Remove(param);
                }
            }
            vrcExpressionParameters.parameters = parameters.ToArray();
        }
        private GameObject CreateOWOSuitObject(string partName)
        {
            var gameObject = new GameObject(partName);
            VRCContactReceiver contact = gameObject.AddComponent<VRCContactReceiver>() as VRCContactReceiver;
            contact.rootTransform = gameObject.transform;
            contact.receiverType = VRCContactReceiverType.Constant;
            contact.shapeType = VRC.Dynamics.ContactBase.ShapeType.Capsule;
            contact.radius = 0.05f;
            contact.height = 0.15f;
            contact.position = Vector3.zero;
            contact.rotation = Quaternion.identity;
            contact.parameter = partName;
            contact.allowSelf = false;
            contact.allowOthers = true;
            contact.localOnly = false;
            contact.collisionTags = new List<string>()
                {
                    "Hand",
                    "Finger",
                    "Torso",
                    "Head",
                    "Foot"
                };
            return gameObject;
        }
    }
}
#endif
