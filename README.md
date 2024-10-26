# ServiceWorker
Windows service app runs on schedule

## Create a service

1. From the Visual Studio **File** menu, select **New > Project** (or press `Ctrl`+`Shift`+`N`) to open the **New Project** window.
2. Find and select the **Windows Service (.NET Framework)** project template.
3. For Project name, enter **ServiceWorker**, and then select Create.
4. Rename the **Service1.cs** to **ServiceWorker.cs**

## Add installers to the service

Before you run a Windows service, you need to install it, which registers it with the Service Control Manager. Add installers to your project to handle the registration details.

![Add installers](https://www.ryadel.com/wp-content/uploads/2019/08/windows-service-c-sharp-create-new-project-visual-studio-2019-add-installer-768x331.jpg "Add installers to the service")

1. In **Solution Explorer**, from the shortcut menu for **ServiceWorker.cs**, choose **View Designer**.
2. In the Design view, select the background area, then choose **Add Installer** from the shortcut menu. By default, Visual Studio adds a component class named `ProjectInstaller`with two new components `serviceProcessInstaller1` and `serviceInstaller1`, which contains two installers, to your project. These installers are for your service and for the service's associated process.
3. Go to **Properties** from the shortcut menu of the **serviceProcessInstaller1** and change the `Account` type as **“LocalSystem”**. This setting installs the service and runs it by using the local system account.
4. Now go to **serviceInstaller1** and choose the Properties option from the shortcut menu.
   - Verify the `ServiceName` property is set to **ServiceWorker**
   - Add text to the `DisplayName` property. For example, **Service Worker Background**.
   - Add text to the `Description` property, such as **A service running in the background**.
   - Set the `StartType` property to **Automatic** from the drop-down list.

When you're finished, the Properties windows should look like the following figure:

![Configure the Installer](https://learn.microsoft.com/en-us/dotnet/framework/windows-services/media/windows-service-installer-properties.png "Configure the Installer for the service")

## Installing NLog

Install NLog nuget package for Windows Service .NET framework
- NLog is a logging platform for .NET with rich log routing and management capabilities.
- NLog supports traditional logging, structured logging and the combination of both.

Standard install
1. Install the latest NLog version 5.3.4 from [NuGet](https://www.nuget.org/packages/NLog/).
2. Setup initial NLog.config xml-file.
   - Choose "Add" > "New Item..."
   - In the "Add New Item" dialog, search for "XML File" or "XML" and select it.
   - Name the file NLog.config and click the "Add" button.
3. Set "Copy To Output Directory" to "Copy if newer" for the NLog.config.

That's it, you can now compile and run your application and it will be able to use NLog.

## Install the service

To install our service, build the project in **Release** mode: once done, copy all files from /bin/Release/ to a more "handy" folder - such as **C:\Public\WindowsService\**

Right after that, open a Command Prompt with administrative rights and type the following:

```bat
"C:\Windows\Microsoft.NET\Framework\v4.0.30319\InstallUtil.exe" "C:\Public\WindowsService\ServiceWorker.exe"
```

You should receive a quick confirmation message saying that everything went good.

```
Microsoft (R) .NET Framework Installation utility Version 4.8.9037.0
Copyright (C) Microsoft Corporation.  All rights reserved.


Running a transacted installation.

Beginning the Install phase of the installation.
See the contents of the log file for the C:\Public\WindowsService\ServiceWorker.exe assembly's progress.
The file is located at C:\Public\WindowsService\ServiceWorker.InstallLog.
Installing assembly 'C:\Public\WindowsService\ServiceWorker.exe'.
Affected parameters are:
   logtoconsole =
   logfile = C:\Public\WindowsService\ServiceWorker.InstallLog
   assemblypath = C:\Public\WindowsService\ServiceWorker.exe
Installing service ServiceWorker...
Service ServiceWorker has been successfully installed.
Creating EventLog source ServiceWorker in log Application...

The Install phase completed successfully, and the Commit phase is beginning.
See the contents of the log file for the C:\Public\WindowsService\ServiceWorker.exe assembly's progress.
The file is located at C:\Public\WindowsService\ServiceWorker.InstallLog.
Committing assembly 'C:\Public\WindowsService\ServiceWorker.exe'.
Affected parameters are:
   logtoconsole =
   logfile = C:\Public\WindowsService\ServiceWorker.InstallLog
   assemblypath = C:\Public\WindowsService\ServiceWorker.exe

The Commit phase completed successfully.

The transacted install has completed.
```

## Alternative install using SC CREATE

If you don't want to use installutil.exe, you can also install your service using the sc create command with the following syntax:

```bash
sc create "ServiceWorker" binPath="C:\Public\WindowsService\ServiceWorker.exe"
sc create "ServiceWorker" binPath= "C:\Public\WindowsService\ServiceWorker.exe" start= auto
sc start ServiceWorker
sc stop ServiceWorker
sc delete ServiceWorker
installutil.exe /u ServiceWorker.exe
```

However, if you do that, you'll have to manually specify the service name: also, the service Description won't be shown in the service list UI window.

## Testing the Service

In Windows, open the Services desktop app: Press `Windows`+`R` to open the **Run** box, enter `services.msc`, and then press `Enter` or select **OK**.

Or use **Control Panel > Administrative Tools > Services** to open the service list and scroll down until you'll find new service.

To start the service, choose **Start** from the service's shortcut menu.

> That's our boy! From here we can either manually start it or set it to automatic, so that it will be started whenever the system starts. However, I strongly suggest to not do that for now, since it would permanently drain your system resources if you forget to disable it - it's just a sample service after all!

### Verify the event log output of your service

Let's just start it and take a look at the log file, which should be found in the executable folder:  
C:\Public\WindowsService\logs

If everything went good, the log should be similar to this:

```
2024-10-26 13:13:13.1722 INFO ServiceWorker.ServiceWorker ============ Service worker on Start ============
2024-10-26 13:31:31.2859 INFO ServiceWorker.ServiceWorker ============ Service worker on Stop ============
```

Thanks for reading this article, hope you enjoyed. It.

-----
Reference

https://learn.microsoft.com/en-us/dotnet/framework/windows-services/walkthrough-creating-a-windows-service-application-in-the-component-designer  
https://www.c-sharpcorner.com/UploadFile/8a67c0/create-and-install-windows-service-step-by-step-in-C-Sharp/  
https://www.ryadel.com/en/create-windows-service-asp-net-c-sharp-how-to-tutorial-guide/  
https://nlog-project.org/download/