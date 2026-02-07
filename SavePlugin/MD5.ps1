# 获取拖入的文件路径
$files = $args

# 检查是否有文件被拖入
if ($files.Count -eq 0) {
    Write-Host "请将文件拖放到此脚本上" -ForegroundColor Yellow
    exit
}

# 处理每个文件
foreach ($file in $files) {
    try {
        # 检查文件是否存在
        if (-not (Test-Path $file)) {
            Write-Host "文件不存在: $file" -ForegroundColor Red
            continue
        }

        # 计算MD5哈希值
        $hash = Get-FileHash -Path $file -Algorithm MD5
        
        # 输出结果
        Write-Host "文件: $file" -ForegroundColor Cyan
        Write-Host "MD5: $($hash.Hash)" -ForegroundColor Green
        Write-Host ""
    }
    catch {
        Write-Host "处理文件 $file 时出错: $($_.Exception.Message)" -ForegroundColor Red
    }
}

# 暂停以查看结果
Write-Host "按任意键退出..."
$null = $Host.UI.RawUI.ReadKey("NoEcho,IncludeKeyDown")
