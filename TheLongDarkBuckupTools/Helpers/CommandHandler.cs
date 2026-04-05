using System.Windows.Input;
using System;

namespace TheLongDarkBuckupTools.Helpers
{
    /// <summary>
    /// 命令处理程序类，实现了ICommand接口，用于封装和执行操作
    /// </summary>
    public class CommandHandler : ICommand
    {
        /// <summary>
        /// 存储要执行的操作
        /// </summary>
        private Action _action;
        /// <summary>
        /// 标识命令是否可以执行
        /// </summary>
        private bool _canExecute;
        /// <summary>
        /// 构造函数，初始化命令处理程序
        /// </summary>
        /// <param name="action">要执行的操作</param>
        public CommandHandler(Action action)
        {
            _action = action;
            _canExecute = true;
        }

        /// <summary>
        /// 判断命令是否可以执行
        /// </summary>
        /// <param name="parameter">命令参数</param>
        /// <returns>如果命令可以执行则返回true，否则返回false</returns>
        public bool CanExecute(object parameter)
        {
            return _canExecute;
        }

        /// <summary>
        /// 当命令的可执行状态改变时触发的事件
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// 执行命令
        /// </summary>
        /// <param name="parameter">命令参数</param>
        public void Execute(object parameter)
        {
            _action();
        }
    }
}