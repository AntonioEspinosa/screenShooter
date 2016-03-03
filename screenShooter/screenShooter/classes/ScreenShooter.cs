using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace screenShooter
{
    class ScreenShootr : IDisposable
    {

        private static LowLevelKeyboardProc _proc = HookCallback;
        #region ConstantKeys
        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        private static IntPtr _hookID = IntPtr.Zero;
        private const int MOD_ALT = 0x1;
        private const int MOD_CONTROL = 0x2;
        private const int MOD_SHIFT = 0x4;
        private const int MOD_WIN = 0x8;
        //modifiers
        public const int NOMOD = 0x0000;
        public const int ALT = 0x0001;
        public const int CTRL = 0x0002;
        public const int SHIFT = 0x0004;
        public const int WIN = 0x0008;
        //windows message id for hotkey
        public const int WM_HOTKEY_MSG_ID = 0x0312;
        #endregion
        private IntPtr Handle = IntPtr.Zero;//link to dll event
        public ScreenShootr()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            NotifyIcon notifyIcon1 = new NotifyIcon();
            ContextMenu contextMenu1 = new ContextMenu();
            MenuItem menuItem1 = new MenuItem();
            contextMenu1.MenuItems.AddRange(new MenuItem[] { menuItem1 });
            menuItem1.Index = 0;
            menuItem1.Text = "E&xit";
            menuItem1.Click += new EventHandler(menuItem1_Click);
            notifyIcon1.Icon = screenShooter.Properties.Resources.Leica_Off_icon;
            notifyIcon1.Text = "Screen Shoot Tool";
            notifyIcon1.ContextMenu = contextMenu1;
            notifyIcon1.Visible = true;
            RegisterHotKey(this.Handle, 42, 0, (int)Keys.PrintScreen);
            Handle = SetHook(_proc);
            Application.Run();
        }
        private static IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            
                using (ProcessModule curModule = curProcess.MainModule)
                {
                    return SetWindowsHookEx(WH_KEYBOARD_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
                }
            
        }
        private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
            {
                int vkCode = Marshal.ReadInt32(lParam);

                if ((Keys)vkCode == Keys.PrintScreen)
                {
                    CaptureScreen();
                }
                Console.WriteLine((Keys)vkCode);
            }
            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }
        private static void CaptureScreen()
        {
            Console.WriteLine("shalala");
        }
        private void menuItem1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        #region DDl Imports
        [DllImport("user32")]
        public static extern int RegisterHotKey(IntPtr hwnd, int id, int fsModifiers, int vk);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);
        #endregion

        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);
        public void Dispose()
        {
            UnhookWindowsHookEx(Handle);
        }
    }
}