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