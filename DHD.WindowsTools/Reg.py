# 计算机\HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\CommandStore


import os
from pickletools import pydict;
import sys;
import winreg;


# 创建命令
print(os.getcwd());
print(sys.argv[0]);
pyPath = sys.argv[0];
pyDir = os.path.dirname(pyPath);
exFilePath = "{0}\DHD.WindowsTools.exe".format(pyDir);
print(exFilePath);

print("=" * 50)
print("开始创建命令")
print("=" * 50)


regCommandStoreRoot = winreg.OpenKey(winreg.HKEY_LOCAL_MACHINE, r"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\CommandStore\Shell", 0, access=winreg.KEY_ALL_ACCESS);

print(winreg.QueryInfoKey(regCommandStoreRoot));

# 日期前缀注册表
regCommandDHDAddDatePrefix = winreg.CreateKeyEx(regCommandStoreRoot, "DHD.AddDatePrefix", 0, access=winreg.KEY_ALL_ACCESS);
winreg.SetValueEx(regCommandDHDAddDatePrefix, "MUIVerb", 0, winreg.REG_SZ, "添加日期前缀");
regCommandDHDAddDatePrefix_Command = winreg.CreateKeyEx(regCommandDHDAddDatePrefix, "command", 0, access=winreg.KEY_ALL_ACCESS);
winreg.SetValue(regCommandDHDAddDatePrefix, "command", winreg.REG_SZ, "{0} -o ad:\"%1\"".format(exFilePath))
winreg.CloseKey(regCommandDHDAddDatePrefix_Command);
winreg.CloseKey(regCommandDHDAddDatePrefix);

# 复制路径
regCommand_DHDCopyPath = winreg.CreateKeyEx(regCommandStoreRoot, "DHD.CopyPath", 0, access=winreg.KEY_ALL_ACCESS);
winreg.SetValueEx(regCommand_DHDCopyPath, "MUIVerb", 0, winreg.REG_SZ, "复制路径");
winreg.SetValueEx(regCommand_DHDCopyPath, "Icon", 0, winreg.REG_SZ, "pifmgr.dll,-4");
winreg.SetValue(regCommand_DHDCopyPath, "command", winreg.REG_SZ, "{0} -o cp:\"%1\"".format(exFilePath));
winreg.CloseKey(regCommand_DHDCopyPath)

# 移动到
regCommand_MoveTo = winreg.CreateKeyEx(regCommandStoreRoot, "DHD.MoveTo", 0, access=winreg.KEY_ALL_ACCESS);
winreg.SetValueEx(regCommand_MoveTo, "MUIVerb", 0, winreg.REG_SZ, "移动至...");
winreg.SetValueEx(regCommand_MoveTo, "Icon", 0, winreg.REG_SZ, "pifmgr.dll,-29");
winreg.SetValue(regCommand_MoveTo, "command", winreg.REG_SZ, "{0} -o mt:\"%1\"".format(exFilePath));
winreg.CloseKey(regCommand_MoveTo)

# 添加到移动目录
regCommand_AddToMoveList = winreg.CreateKeyEx(regCommandStoreRoot, "DHD.AddToMoveList", 0, access=winreg.KEY_ALL_ACCESS);
winreg.SetValueEx(regCommand_AddToMoveList, "MUIVerb", 0, winreg.REG_SZ, "添加至移动目录");
winreg.SetValueEx(regCommand_AddToMoveList, "Icon", 0, winreg.REG_SZ, "pifmgr.dll,-33");
winreg.SetValue(regCommand_AddToMoveList, "command", winreg.REG_SZ, "{0} -o amt:\"%1\"".format(exFilePath));
winreg.CloseKey(regCommand_AddToMoveList)


regCommand_DHDHelper = winreg.CreateKeyEx(regCommandStoreRoot, "DHD.Helper", 0, access=winreg.KEY_ALL_ACCESS);
winreg.SetValueEx(regCommand_DHDHelper, "MUIVerb", 0, winreg.REG_SZ, "帮助");
winreg.SetValueEx(regCommand_DHDHelper, "Icon", 0, winreg.REG_SZ, "imageres.dll,-81");
winreg.SetValue(regCommand_DHDHelper, "command", winreg.REG_SZ, "{0} -help".format(exFilePath));
winreg.CloseKey(regCommand_DHDHelper)


winreg.CloseKey(regCommandStoreRoot);
print("命令创建完成！");

print("=" * 50)
print("开始创建右键菜单")
print("=" * 50)

# 所有文件右键菜单命令
regAllFile = winreg.CreateKeyEx(winreg.HKEY_CLASSES_ROOT, r"*\shell\DHDTools", 0, winreg.KEY_ALL_ACCESS);
winreg.SetValueEx(regAllFile, "MUIVerb",0, winreg.REG_SZ, "【海东工具箱】");
winreg.SetValueEx(regAllFile, "SubCommands", 0, winreg.REG_SZ, "DHD.AddDatePrefix;DHD.CopyPath;DHD.MoveTo;DHD.Helper;");
winreg.SetValueEx(regAllFile, "Icon", 0, winreg.REG_SZ, exFilePath);
winreg.CloseKey(regAllFile)

# 文件夹右键菜单命令
regDirectory = winreg.CreateKeyEx(winreg.HKEY_CLASSES_ROOT, r"Directory\shell\DHDTools", 0, winreg.KEY_ALL_ACCESS)
winreg.SetValueEx(regDirectory, "MUIVerb",0, winreg.REG_SZ, "【海东工具箱】");
winreg.SetValueEx(regDirectory, "SubCommands", 0, winreg.REG_SZ, "DHD.AddDatePrefix;DHD.CopyPath;DHD.MoveTo;DHD.AddToMoveList;DHD.Helper;");
winreg.SetValueEx(regDirectory, "Icon", 0, winreg.REG_SZ, exFilePath);
winreg.CloseKey(regDirectory)


print("创建右键菜单完成！");




