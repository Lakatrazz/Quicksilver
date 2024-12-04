using System.Collections.Generic;

using BoneLib;
using BoneLib.BoneMenu;

using MelonLoader;

using UnityEngine;

using System.Linq;

using Avatar = Il2CppSLZ.VRMK.Avatar;

using Il2CppSLZ.Marrow;

namespace Quicksilver;

public class QuicksilverMod : MelonMod
{
    public const string Version = "1.7.0";

    public struct RigidbodyState
    {
        public float drag;
        public float angularDrag;

        public RigidbodyState(Rigidbody rb)
        {
            drag = rb.drag;
            angularDrag = rb.angularDrag;
        }
    }

    public static QuicksilverMod Instance { get; private set; }

    public static bool IsEnabled { get; private set; }

    public static float TargetTimeScale { get; private set; } = 1f;

    private static bool _previousIsEnabled;

    public RigManager rigManager;

    public float lastTimeScale;
    public float lastCheckedTimeScale;

    public float timeOfLastAvatarUpdate;
    public bool checkForUpdate;

    public Dictionary<Rigidbody, RigidbodyState> rigidbodies = new Dictionary<Rigidbody, RigidbodyState>();

    public static MelonPreferences_Category MelonPrefCategory { get; private set; }
    public static MelonPreferences_Entry<bool> MelonPrefEnabled { get; private set; }

    private static bool _registeredPrefs = false;

    public static Page BoneMenuPage { get; private set; }
    public static BoolElement BoneMenuEnabledElement { get; private set; }

    public override void OnInitializeMelon()
    {
        Instance = this;

        Hooking.OnLevelLoaded += OnLevelLoaded;

        SetupMelonPrefs();
        SetupBoneMenu();
    }

    public static void SetupMelonPrefs()
    {
        MelonPrefCategory = MelonPreferences.CreateCategory("Quicksilver");
        MelonPrefEnabled = MelonPrefCategory.CreateEntry("IsEnabled", true);

        IsEnabled = MelonPrefEnabled.Value;
        _previousIsEnabled = IsEnabled;

        _registeredPrefs = true;
    }

    public static void SetupBoneMenu()
    {
        BoneMenuPage = Page.Root.CreatePage("Quicksilver", new Color(0.75f, 0.75f, 0.75f));
        BoneMenuEnabledElement = BoneMenuPage.CreateBool("Mod Toggle", Color.yellow, IsEnabled, OnSetEnabled);
    }

    public static void OnSetEnabled(bool value)
    {
        IsEnabled = value;
        MelonPrefEnabled.Value = value;
        MelonPrefCategory.SaveToFile(false);
    }

    public override void OnPreferencesLoaded()
    {
        if (!_registeredPrefs)
        {
            return;
        }

        IsEnabled = MelonPrefEnabled.Value;
        BoneMenuEnabledElement.Value = IsEnabled;
    }

    public void OnLevelLoaded(LevelInfo info)
    {
        rigManager = Player.RigManager;
        TargetTimeScale = Time.timeScale;
        lastTimeScale = Time.timeScale;
        lastCheckedTimeScale = lastTimeScale;

        rigidbodies.Clear();

        foreach (var rb in rigManager.GetComponentsInChildren<Rigidbody>(true))
        {
            rigidbodies.Add(rb, new RigidbodyState(rb));
        }
    }

    public void ResetPlayer()
    {
        var physicsRig = rigManager.physicsRig;

        physicsRig._pelvisForceInternalMult = 1f;
        physicsRig.leftHand.physHand.armInternalMult = 1f;
        physicsRig.rightHand.physHand.armInternalMult = 1f;

        rigManager.SwapAvatarCrate(rigManager.AvatarCrate.Barcode);

        OnUpdateRigidbodies(1f);
    }

    public bool HasRigManager() => !(rigManager is null || rigManager == null || rigManager.WasCollected);

    public override void OnFixedUpdate()
    {
        float timeScale = Time.timeScale;

        if (timeScale <= 0f)
        {
            return;
        }

        if (!HasRigManager())
        {
            return;
        }

        if (!rigManager.physicsRig.ballLocoEnabled)
        {
            return;
        }

        // Enabled event
        if (IsEnabled)
        {
            if (Mathf.Round(timeScale * 1000f) != Mathf.Round(lastCheckedTimeScale * 1000f))
            {
                OnUpdateVelocities(timeScale, lastCheckedTimeScale);

                timeOfLastAvatarUpdate = Time.realtimeSinceStartup;
                checkForUpdate = true;
                lastCheckedTimeScale = timeScale;
            }

            if (!_previousIsEnabled || (checkForUpdate && (Time.realtimeSinceStartup - timeOfLastAvatarUpdate) > 0.015f))
            {
                checkForUpdate = false;

                rigManager.SwapAvatarCrate(rigManager.AvatarCrate.Barcode);

                OnUpdateRigidbodies(timeScale);

                lastTimeScale = timeScale;

                TargetTimeScale = timeScale;
            }

            // Apply extra gravity
            ApplyGravity(timeScale);

            var physicsRig = rigManager.physicsRig;
            physicsRig._pelvisForceInternalMult = 1f / Mathf.Pow(timeScale, 3f);
            physicsRig.leftHand.physHand.armInternalMult = 1f / timeScale;
            physicsRig.rightHand.physHand.armInternalMult = 1f / timeScale;
        }
        else if (_previousIsEnabled)
        {
            ResetPlayer();
        }

        _previousIsEnabled = IsEnabled;
    }

    private void ApplyGravity(float timeScale)
    {
        float gravMult = 1f / timeScale;
        gravMult *= gravMult;

        var totalGravity = Physics.gravity;
        var grav = (totalGravity * gravMult) - totalGravity;

        var feetRb = rigManager.physicsRig._feetRb;
        var kneeRb = rigManager.physicsRig._kneeRb;

        Vector3 totalForce = grav;
        float totalMass = 0f;

        if (feetRb.useGravity)
        {
            feetRb.AddForce(grav, ForceMode.Acceleration);
            totalMass += feetRb.mass;
        }

        if (kneeRb.useGravity)
        {
            kneeRb.AddForce(grav, ForceMode.Acceleration);
            totalMass += kneeRb.mass;
        }

        var col = rigManager.physicsRig.physG._groundedCollider;
        if (col && col.attachedRigidbody)
        {
            col.attachedRigidbody.AddForceAtPosition(-totalForce * Mathf.Min(col.attachedRigidbody.mass, totalMass), feetRb.worldCenterOfMass, ForceMode.Force);
        }
    }

    public void OnUpdateVelocities(float timeScale, float lastTimeScale)
    {
        if (timeScale <= 0f || lastTimeScale <= 0f)
        {
            return;
        }

        float mlp = lastTimeScale / timeScale;

        for (var i = 0; i < rigidbodies.Keys.Count; i++)
        {
            var rb = rigidbodies.Keys.ElementAt(i);

            if (rb is null || rb == null || rb.WasCollected)
            {
                continue;
            }

            rb.velocity *= mlp;
            rb.angularVelocity *= mlp;
        }
    }

    public void OnUpdateRigidbodies(float timeScale)
    {
        if (timeScale <= 0f)
        {
            return;
        }

        for (var i = 0; i < rigidbodies.Keys.Count; i++)
        {
            var rb = rigidbodies.Keys.ElementAt(i);

            if (rb is null || rb == null || rb.WasCollected)
            {
                continue;
            }

            var state = rigidbodies[rb];
            rb.drag = state.drag * timeScale;
            rb.angularDrag = state.angularDrag * timeScale;
        }
    }

    public static bool IsMainRig(Avatar avatar)
    {
        bool isMain = false;

        try
        {
            if (Instance.rigManager.avatar == avatar)
            {
                isMain = true;
            }
        }
        catch { }

        return isMain;
    }

    public static bool IsMainRig(Rig rig)
    {
        bool isMain = false;

        try
        {
            if (rig.manager == Instance.rigManager)
            {
                isMain = true;
            }
        }
        catch { }

        return isMain;
    }
}