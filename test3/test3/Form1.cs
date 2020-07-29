using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Runtime.InteropServices;
using System.Diagnostics;


namespace test3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        [DllImport("kernel32.dll")] //声明API函数
        public static extern int VirtualAllocEx(IntPtr hwnd, int lpaddress, int size, int type, int tect);

        [DllImport("kernel32.dll")]
        public static extern int WriteProcessMemory(IntPtr hwnd, int baseaddress, string buffer, int nsize, int filewriten);

        [DllImport("kernel32.dll")]
        public static extern int GetProcAddress(int hwnd, string lpname);

        [DllImport("kernel32.dll")]
        public static extern int GetModuleHandleA(string name);

        [DllImport("kernel32.dll")]
        public static extern int CreateRemoteThread(IntPtr hwnd, int attrib, int size, int address, int par, int flags, int threadid);

        private void button1_Click(object sender, EventArgs e)
        {
            int ok1;
            //int ok2;
            //int hwnd;
            int baseaddress;
            int temp = 0;
            int hack;
            int yan;
            string dllname;

            dllname = @"E:\study\git\BuckUpToolsForTheLongDark\dlltest\dlltest\bin\Debug\netcoreapp2.1\dlltest.dll";
            int dlllength;
            dlllength = dllname.Length + 1;
            Process[] pname = Process.GetProcesses(); //取得所有进程

            foreach (Process name in pname) //遍历进程
            {
                //MessageBox.Show(name.ProcessName.ToLower());
                if (name.ProcessName.ToLower().IndexOf("notepad") != -1) //所示记事本，那么下面开始注入
                {
                    baseaddress = VirtualAllocEx(name.Handle, 0, dlllength, 4096, 4);   //申请内存空间
                    if (baseaddress == 0) //返回0则操作失败，下面都是
                    {
                        MessageBox.Show("申请内存空间失败！！");
                        Application.Exit();
                    }

                    ok1 = WriteProcessMemory(name.Handle, baseaddress, dllname, dlllength, temp); //写内存
                    if (ok1 == 0)
                    {
                        MessageBox.Show("写内存失败！！");
                        Application.Exit();
                    }

                    hack = GetProcAddress(GetModuleHandleA("Kernel32"), "LoadLibraryA"); //取得loadlibarary在kernek32.dll地址

                    if (hack == 0)
                    {
                        MessageBox.Show("无法取得函数的入口点！！");
                        Application.Exit();
                    }

                    yan = CreateRemoteThread(name.Handle, 0, 0, hack, baseaddress, 0, temp); //创建远程线程。

                    if (yan == 0)
                    {
                        MessageBox.Show("创建远程线程失败！！");
                        Application.Exit();
                    }
                    else
                    {
                        MessageBox.Show("已成功注入dll!!");
                    }
                }
            }
        }
    }
}