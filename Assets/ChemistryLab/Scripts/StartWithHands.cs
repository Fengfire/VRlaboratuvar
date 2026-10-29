using UnityEngine;
using System.Collections;
using System.Reflection;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Simulation;
namespace ChemistryLab {
public class StartWithHands:MonoBehaviour {
 IEnumerator Start(){yield return null;var lifecycle=GetComponent<SimulatedDeviceLifecycleManager>();if(lifecycle&&lifecycle.deviceMode==SimulatedDeviceLifecycleManager.DeviceMode.Controller){var switchMode=typeof(SimulatedDeviceLifecycleManager).GetMethod("SwitchDeviceMode",BindingFlags.Instance|BindingFlags.NonPublic);if(switchMode!=null)switchMode.Invoke(lifecycle,null);else Debug.LogWarning("XRI hand mode unavailable; select hand mode in simulator.");}}
}
}
