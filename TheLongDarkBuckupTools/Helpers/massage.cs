using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheLongDarkBuckupTools.Helpers
{
    class Massage
    {
        /// <summary>
        /// 给用户的提示
        /// </summary>
        public static string tishi =
            @"漫漫长夜备份工具
作者：绘梦璃
请注意以下事项：

1. 自动备份页面打开后，程序才会在存档文件夹内容修改时进行自动备份

2. steam同步存档的时候不要打开自动备份页面（不会出错但是会导致大量备份）

3. 游戏关闭时候steam也会进行云存。。。（所以关闭游戏前先关闭自动备份页面）

4. 可能会有杀毒软件报 “程序调用了cmd.exe” 这是正常的，本程序的基础备份就是通过cmd命令实现的。

5. 请不要删除bfFolder文件夹下的zipPath文件夹（隐藏的），这个文件夹存放的也是存档文件。

6. 读取存档的时候，直接把备份工具产生的截图直接拖进去也是可以的

7. 读取存档文件的功能做好了,但是只能读取少量数据...

8. 成功读取了存档内的截图,但是存档内的截图分辨率只有320X200,而且在程序外不可见.

其他：

祝你玩的愉快，别被游戏的删档机制搞崩心态。

修改内容：

1. bfFolder文件夹空的时候，读取备份会导致程序崩溃（这个修好了，不用担心崩溃）。



";



    }
}
