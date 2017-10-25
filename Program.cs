using System;
using System.Windows.Forms;
using System.Media;

using CoreAudioApi;
using ToastNotifications;

namespace MicrophoneMute
{
    class Program
    {
        private static MMDevice defaultDevice = null;


        //判斷當前系統揚聲器狀態
        static bool isMuted()
        {
            return defaultDevice.AudioEndpointVolume.Mute;
        }
        //靜音
        static void setMute()
        {
            defaultDevice.AudioEndpointVolume.Mute = true;
        }
        //解除靜音
        static void setUnMute()
        {
            defaultDevice.AudioEndpointVolume.Mute = false;
        }
        //初始化MMDevice
        static void muteInit()
        {
            MMDeviceEnumerator devEnum = new MMDeviceEnumerator();
            defaultDevice = devEnum.GetDefaultAudioEndpoint(EDataFlow.eCapture, ERole.eMultimedia);
        }

        static void PlayNotificationSound(string sound)
        {
            SoundPlayer sp = new SoundPlayer(MicrophoneMute.Properties.Resources.normal);
            sp.Play();
        }

        static void ShowNotification(string tipTitle, string tipBody)
        {
            int duration = 1;
            var animationMethod = FormAnimator.AnimationMethod.Slide;
            var animationDirection = FormAnimator.AnimationDirection.Up;
            var toastNotification = new Notification(tipTitle, tipBody, duration, animationMethod, animationDirection);
            PlayNotificationSound("normal");
            toastNotification.Show();
        }

        public static bool Delay(int delayTime)
        {
            DateTime now = DateTime.Now;
            int s;
            do
            {
                TimeSpan spand = DateTime.Now - now;
                s = spand.Seconds;
                Application.DoEvents();
            }
            while (s < delayTime);
            return true;
        }
        static void Main(string[] args)
        {
            muteInit();
            if (defaultDevice.AudioEndpointVolume.Mute)
            {
                setUnMute();
            }
            else
            {
                setMute();
            }
            if (defaultDevice.AudioEndpointVolume.Mute)
            {
                ShowNotification("麦克风", "麦克风静音");
            }
            else
            {
                ShowNotification("麦克风", "麦克风恢复");
            }
            Delay(3);
        }
    }
}
