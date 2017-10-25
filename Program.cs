using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

using CoreAudioApi;

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

        static void Main(string[] args)
        {
            NotifyIcon btIcon = new NotifyIcon();
            btIcon.Icon = Icon.ExtractAssociatedIcon(System.Windows.Forms.Application.ExecutablePath); // new Icon(args[0]);
            btIcon.Visible = true;
            ToolTipIcon btToolTipIcon = new ToolTipIcon();

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
                btIcon.ShowBalloonTip(1, "麦克风", "麦克风已被静音", btToolTipIcon);
            }
            else
            {
                btIcon.ShowBalloonTip(1, "麦克风", "麦克风取消静音", btToolTipIcon);
            }
            btIcon.Dispose();
        }
    }
}
