# BTAF

Bluetooth audio fix

This service fixes the broken handling of bluetooth audio devices in Windows 11

## Addressed problems

- Random downgrade of audio quality
- Audio silence when entering online meetings (Especially in Cisco Webex)
- "Hands free Audio" BT service being re-enabled every time the bluetooth device is connected
- Audio cut off at the start of audio playback
- Playing a few garbage samples at the start of audio playback (often sounds like a brief pulse of white noise)

## Usage

Simply double click `BTAF.Service.exe` to run the user interface.
From there the application can be configured, service installed/uninstalled,
or it can be run in standalone mode without the service if desired.

## Configuration

The application currently provides two settings.

For the settings to take effect, the service (or manual monitor) has to be restarted

### Audio device

In this section, the audio devices to be monitored can be selected.
Only devices that would produce sound when an audio stream is played back are listed.
Devices which are disabled in the sound manager, disabled in device manager,
are disconnected (for example a bluetooth device),
or have their audio cable unplugged (where detection of this is supported) are not shown.

The device is considered present when any one of the configured devices is ready.

### Fix bad audio streams start

This option should be enabled if at least one of the two issues below are present when playing audio or video files:

- Audio playback occasionally starts with a brief burst of white noise
- The start of an audio stream is swallowed

## Service control

The application comes with its own service control panel built in.

Under normal circumstances, simply click "Install", followed by "Start",
then you can close the application. From now on,
the audio fix starts automatically whenever you start Windows.

Once the service is installed, the application should not be moved again or the service will fail to start.
If this happens, simply uninstall and reinstall the service.

If you uninstall the service without planning to reinstall it,
click the "Reset" button to restore the bluetooth audio gateway service to its default configuration.

To manually uninstall the service, run the two commands below in an elevated command prompt:

	NET STOP BTAF
	SC DELETE BTAF

Note: The "NET STOP" command will fail if the service is not running. This is normal.

