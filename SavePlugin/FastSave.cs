using BepInEx.Unity.IL2CPP;
using System.IO.Pipes;
using System.Text;

namespace SavePlugin
{
    /// <summary>
    /// 快速保存插件
    /// </summary>
    public class FastSave : BasePlugin
    {
        /// <summary>
        /// 启动管道服务器
        /// </summary>
        private NamedPipeServerStream _pipeServer;
        /// <summary>
        /// 管道服务器名称
        /// </summary>
        private const string PipeName = "FastSavePipe";

        public override void Load()
        {
            // 初始化命名管道服务器
            _pipeServer = new NamedPipeServerStream(PipeName, PipeDirection.In);

            // 在后台线程中监听连接
            System.Threading.Tasks.Task.Run(() => ListenForCommands());

            Log.LogInfo("FastSave plugin loaded and listening for commands");
        }

        /// <summary>
        /// 监听命令
        /// </summary>
        private void ListenForCommands()
        {
            try
            {
                while (true)
                {
                    // 等待客户端连接
                    _pipeServer.WaitForConnection();

                    // 读取命令
                    var buffer = new byte[256];
                    var bytesRead = _pipeServer.Read(buffer, 0, buffer.Length);
                    var command = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                    // 处理命令
                    ProcessCommand(command);

                    // 断开连接并准备接受新的连接
                    _pipeServer.Disconnect();
                }
            }
            catch (System.Exception ex)
            {
                Log.LogError($"Pipe server error: {ex.Message}");
            }
        }

        /// <summary>
        /// 处理命令
        /// </summary>
        /// <param name="command"></param>
        private void ProcessCommand(string command)
        {
            Log.LogInfo($"Received command: {command}");

            // 根据命令执行相应操作
            switch (command.ToLower())
            {
                case "save":
                    // 调用 uControl.save() 方法
                    CallSaveMethod();
                    break;
                    // 可以添加更多命令
            }
        }

        /// <summary>
        /// 调用 uControl.save() 方法
        /// </summary>
        private void CallSaveMethod()
        {
            try
            {
                // 这里实现调用 uControl.save() 的逻辑
                // 具体实现取决于 uControl 的访问方式
                Log.LogInfo("Calling uControl.save() method");
            }
            catch (System.Exception ex)
            {
                Log.LogError($"Error calling save method: {ex.Message}");
            }
        }
    }
}
