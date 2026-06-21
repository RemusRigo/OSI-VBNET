'--------------------------------------------------------------------------------------------------
' Win32_DiskDrive class
' https://learn.microsoft.com/en-us/windows/win32/cimwin32prov/win32-diskdrive
'
'    © Remus Rigo
'       v1.0 2026-06-21
'--------------------------------------------------------------------------------------------------

Imports System.Collections.Concurrent
Imports System.ComponentModel
Imports System.Management
Imports System.Private
Imports System.Security.Cryptography.Xml
Imports System.Threading
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports SharedInterfaces

Public Class frmDiskDrive
   Implements IModuleForm
   <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
   Public Property MainForm As IMainForm Implements IModuleForm.MainForm

   Public remoteHost, remoteUser, remotePass As String

   Private Sub frmDiskDrive_Load(sender As Object, e As EventArgs) Handles MyBase.Load
      lvDiskDrive.BackColor = Color.FromArgb(224, 234, 213)

      Dim backgroundWorker As New System.ComponentModel.BackgroundWorker()
      AddHandler backgroundWorker.DoWork, AddressOf BackgroundWorker_DoWork
      AddHandler backgroundWorker.RunWorkerCompleted, AddressOf BackgroundWorker_RunWorkerCompleted
      backgroundWorker.RunWorkerAsync()

      If MainForm IsNot Nothing Then
         If remoteHost <> "" Then
            MainForm.SetTitle("OSI: DiskDrive v1.0 on [" & remoteHost & "]")
         Else
            MainForm.SetTitle("OSI: DiskDrive v1.0")
         End If
      End If

   End Sub

   Private Sub BackgroundWorker_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs)
      Dim myConnection As New ConnectionOptions()
      Dim scopePath As String
      Dim items As New List(Of ListViewItem)

      If (remoteHost <> "" And remoteUser <> "") Then
         myConnection.Username = remoteUser
         myConnection.Password = remotePass
         myConnection.Impersonation = ImpersonationLevel.Impersonate
         myConnection.Authentication = AuthenticationLevel.PacketPrivacy
         scopePath = $"\\{remoteHost}\root\cimv2"
      Else
         scopePath = "\\.\root\cimv2"
      End If

      Dim scope As New ManagementScope(scopePath, myConnection)

      Try
         scope.Connect()
         Dim myQuery As New ObjectQuery("SELECT * FROM Win32_DiskDrive")
         Dim searcher As New ManagementObjectSearcher(scope, myQuery)
         Dim cnt As Integer = 0

         For Each obj As ManagementObject In searcher.Get()
            cnt += 1
            Dim grpUser As New ListViewGroup("Disk Drive #" & cnt, HorizontalAlignment.Left)
            lvDiskDrive.Groups.Add(grpUser)

            If obj.Properties.Cast(Of PropertyData)().Any(Function(p) p.Name = "Name") Then
               If (obj("Name") IsNot Nothing) Then
                  Dim item As New ListViewItem("Name")
                  item.SubItems.Add(obj("Name").ToString())
                  item.Group = grpUser
                  items.Add(item)
               End If
            End If

            If obj.Properties.Cast(Of PropertyData)().Any(Function(p) p.Name = "Availability") Then
               If (obj("Availability") IsNot Nothing) Then
                  Dim item As New ListViewItem("Availability")
                  item.SubItems.Add(obj("Availability").ToString())
                  item.Group = grpUser
                  items.Add(item)
               End If
            End If

            If obj.Properties.Cast(Of PropertyData)().Any(Function(p) p.Name = "BytesPerSector") Then
               If (obj("BytesPerSector") IsNot Nothing) Then
                  Dim item As New ListViewItem("BytesPerSector")
                  item.SubItems.Add(obj("BytesPerSector").ToString())
                  item.Group = grpUser
                  items.Add(item)
               End If
            End If

            If obj.Properties.Cast(Of PropertyData)().Any(Function(p) p.Name = "Caption") Then
               If (obj("Caption") IsNot Nothing) Then
                  Dim item As New ListViewItem("Caption")
                  item.SubItems.Add(obj("Caption").ToString())
                  item.Group = grpUser
                  items.Add(item)
               End If
            End If

            If obj.Properties.Cast(Of PropertyData)().Any(Function(p) p.Name = "Capabilities") Then
               If (obj("Capabilities") IsNot Nothing) Then
                  Dim item As New ListViewItem("Capabilities")
                  item.SubItems.Add(obj("Capabilities").ToString())
                  item.Group = grpUser
                  items.Add(item)
               End If
            End If


            If obj.Properties.Cast(Of PropertyData)().Any(Function(p) p.Name = "Caption") Then
               If (obj("Caption") IsNot Nothing) Then
                  Dim item As New ListViewItem("Caption")
                  item.SubItems.Add(obj("Caption").ToString())
                  item.Group = grpUser
                  items.Add(item)
               End If
            End If

            If obj.Properties.Cast(Of PropertyData)().Any(Function(p) p.Name = "CompressionMethod") Then
               If (obj("CompressionMethod") IsNot Nothing) Then
                  Dim item As New ListViewItem("CompressionMethod")
                  item.SubItems.Add(obj("CompressionMethod").ToString())
                  item.Group = grpUser
                  items.Add(item)
               End If
            End If

            If obj.Properties.Cast(Of PropertyData)().Any(Function(p) p.Name = "ConfigManagerErrorCode") Then
               If (obj("ConfigManagerErrorCode") IsNot Nothing) Then
                  Dim item As New ListViewItem("ConfigManagerErrorCode")
                  item.SubItems.Add(obj("ConfigManagerErrorCode").ToString())
                  item.Group = grpUser
                  items.Add(item)
               End If
            End If

            If obj.Properties.Cast(Of PropertyData)().Any(Function(p) p.Name = "ConfigManagerUserConfig") Then
               If (obj("ConfigManagerUserConfig") IsNot Nothing) Then
                  Dim item As New ListViewItem("ConfigManagerUserConfig")
                  item.SubItems.Add(obj("ConfigManagerUserConfig").ToString())
                  item.Group = grpUser
                  items.Add(item)
               End If
            End If

            If obj.Properties.Cast(Of PropertyData)().Any(Function(p) p.Name = "DefaultBlockSize") Then
               If (obj("DefaultBlockSize") IsNot Nothing) Then
                  Dim item As New ListViewItem("DefaultBlockSize")
                  item.SubItems.Add(obj("DefaultBlockSize").ToString())
                  item.Group = grpUser
                  items.Add(item)
               End If
            End If

            If obj.Properties.Cast(Of PropertyData)().Any(Function(p) p.Name = "Description") Then
               If (obj("Description") IsNot Nothing) Then
                  Dim item As New ListViewItem("Description")
                  item.SubItems.Add(obj("Description").ToString())
                  item.Group = grpUser
                  items.Add(item)
               End If
            End If

            If obj.Properties.Cast(Of PropertyData)().Any(Function(p) p.Name = "DeviceID") Then
               If (obj("DeviceID") IsNot Nothing) Then
                  Dim item As New ListViewItem("DeviceID")
                  item.SubItems.Add(obj("DeviceID").ToString())
                  item.Group = grpUser
                  items.Add(item)
               End If
            End If

            If obj.Properties.Cast(Of PropertyData)().Any(Function(p) p.Name = "ErrorCleared") Then
               If (obj("ErrorCleared") IsNot Nothing) Then
                  Dim item As New ListViewItem("ErrorCleared")
                  item.SubItems.Add(obj("ErrorCleared").ToString())
                  item.Group = grpUser
                  items.Add(item)
               End If
            End If

            If obj.Properties.Cast(Of PropertyData)().Any(Function(p) p.Name = "ErrorDescription") Then
               If (obj("ErrorDescription") IsNot Nothing) Then
                  Dim item As New ListViewItem("ErrorDescription")
                  item.SubItems.Add(obj("ErrorDescription").ToString())
                  item.Group = grpUser
                  items.Add(item)
               End If
            End If

            If obj.Properties.Cast(Of PropertyData)().Any(Function(p) p.Name = "ErrorMethodology") Then
               If (obj("ErrorMethodology") IsNot Nothing) Then
                  Dim item As New ListViewItem("ErrorMethodology")
                  item.SubItems.Add(obj("ErrorMethodology").ToString())
                  item.Group = grpUser
                  items.Add(item)
               End If
            End If

            If obj.Properties.Cast(Of PropertyData)().Any(Function(p) p.Name = "FirmwareRevision") Then
               If (obj("FirmwareRevision") IsNot Nothing) Then
                  Dim item As New ListViewItem("FirmwareRevision")
                  item.SubItems.Add(obj("FirmwareRevision").ToString())
                  item.Group = grpUser
                  items.Add(item)
               End If
            End If

            If obj.Properties.Cast(Of PropertyData)().Any(Function(p) p.Name = "Index") Then
               If (obj("Index") IsNot Nothing) Then
                  Dim item As New ListViewItem("Index")
                  item.SubItems.Add(obj("Index").ToString())
                  item.Group = grpUser
                  items.Add(item)
               End If
            End If

            If obj.Properties.Cast(Of PropertyData)().Any(Function(p) p.Name = "InstallDate") Then
               If (obj("InstallDate") IsNot Nothing) Then
                  Dim item As New ListViewItem("InstallDate")
                  item.SubItems.Add(obj("InstallDate").ToString())
                  item.Group = grpUser
                  items.Add(item)
               End If
            End If

            If obj.Properties.Cast(Of PropertyData)().Any(Function(p) p.Name = "InterfaceType") Then
               If (obj("InterfaceType") IsNot Nothing) Then
                  Dim item As New ListViewItem("InterfaceType")
                  item.SubItems.Add(obj("InterfaceType").ToString())
                  item.Group = grpUser
                  items.Add(item)
               End If
            End If

            If obj.Properties.Cast(Of PropertyData)().Any(Function(p) p.Name = "LastErrorCode") Then
               If (obj("LastErrorCode") IsNot Nothing) Then
                  Dim item As New ListViewItem("LastErrorCode")
                  item.SubItems.Add(obj("LastErrorCode").ToString())
                  item.Group = grpUser
                  items.Add(item)
               End If
            End If

            If obj.Properties.Cast(Of PropertyData)().Any(Function(p) p.Name = "Manufacturer") Then
               If (obj("Manufacturer") IsNot Nothing) Then
                  Dim item As New ListViewItem("Manufacturer")
                  item.SubItems.Add(obj("Manufacturer").ToString())
                  item.Group = grpUser
                  items.Add(item)
               End If
            End If

            If obj.Properties.Cast(Of PropertyData)().Any(Function(p) p.Name = "MaxBlockSize") Then
               If (obj("MaxBlockSize") IsNot Nothing) Then
                  Dim item As New ListViewItem("MaxBlockSize")
                  item.SubItems.Add(obj("MaxBlockSize").ToString())
                  item.Group = grpUser
                  items.Add(item)
               End If
            End If

            If obj.Properties.Cast(Of PropertyData)().Any(Function(p) p.Name = "MaxMediaSize") Then
               If (obj("MaxMediaSize") IsNot Nothing) Then
                  Dim item As New ListViewItem("MaxMediaSize")
                  item.SubItems.Add(obj("MaxMediaSize").ToString())
                  item.Group = grpUser
                  items.Add(item)
               End If
            End If

            If obj.Properties.Cast(Of PropertyData)().Any(Function(p) p.Name = "MediaLoaded") Then
               If (obj("MediaLoaded") IsNot Nothing) Then
                  Dim item As New ListViewItem("MediaLoaded")
                  item.SubItems.Add(obj("MediaLoaded").ToString())
                  item.Group = grpUser
                  items.Add(item)
               End If
            End If

            If obj.Properties.Cast(Of PropertyData)().Any(Function(p) p.Name = "MediaType") Then
               If (obj("MediaType") IsNot Nothing) Then
                  Dim item As New ListViewItem("MediaType")
                  item.SubItems.Add(obj("MediaType").ToString())
                  item.Group = grpUser
                  items.Add(item)
               End If
            End If

            If obj.Properties.Cast(Of PropertyData)().Any(Function(p) p.Name = "MinBlockSize") Then
               If (obj("MinBlockSize") IsNot Nothing) Then
                  Dim item As New ListViewItem("MinBlockSize")
                  item.SubItems.Add(obj("MinBlockSize").ToString())
                  item.Group = grpUser
                  items.Add(item)
               End If
            End If

            If obj.Properties.Cast(Of PropertyData)().Any(Function(p) p.Name = "Model") Then
               If (obj("Model") IsNot Nothing) Then
                  Dim item As New ListViewItem("Model")
                  item.SubItems.Add(obj("Model").ToString())
                  item.Group = grpUser
                  items.Add(item)
               End If
            End If

            If obj.Properties.Cast(Of PropertyData)().Any(Function(p) p.Name = "Name") Then
               If (obj("Name") IsNot Nothing) Then
                  Dim item As New ListViewItem("Name")
                  item.SubItems.Add(obj("Name").ToString())
                  item.Group = grpUser
                  items.Add(item)
               End If
            End If

            If obj.Properties.Cast(Of PropertyData)().Any(Function(p) p.Name = "NeedsCleaning") Then
               If (obj("NeedsCleaning") IsNot Nothing) Then
                  Dim item As New ListViewItem("NeedsCleaning")
                  item.SubItems.Add(obj("NeedsCleaning").ToString())
                  item.Group = grpUser
                  items.Add(item)
               End If
            End If

            If obj.Properties.Cast(Of PropertyData)().Any(Function(p) p.Name = "NumberOfMediaSupported") Then
               If (obj("NumberOfMediaSupported") IsNot Nothing) Then
                  Dim item As New ListViewItem("xNumberOfMediaSupportedxx")
                  item.SubItems.Add(obj("NumberOfMediaSupported").ToString())
                  item.Group = grpUser
                  items.Add(item)
               End If
            End If

            If obj.Properties.Cast(Of PropertyData)().Any(Function(p) p.Name = "Partitions") Then
               If (obj("Partitions") IsNot Nothing) Then
                  Dim item As New ListViewItem("Partitions")
                  item.SubItems.Add(obj("Partitions").ToString())
                  item.Group = grpUser
                  items.Add(item)
               End If
            End If

            If obj.Properties.Cast(Of PropertyData)().Any(Function(p) p.Name = "PNPDeviceID") Then
               If (obj("PNPDeviceID") IsNot Nothing) Then
                  Dim item As New ListViewItem("PNPDeviceID")
                  item.SubItems.Add(obj("PNPDeviceID").ToString())
                  item.Group = grpUser
                  items.Add(item)
               End If
            End If

            If obj.Properties.Cast(Of PropertyData)().Any(Function(p) p.Name = "PowerManagementCapabilities") Then
               If (obj("PowerManagementCapabilities") IsNot Nothing) Then
                  Dim item As New ListViewItem("PowerManagementCapabilities")
                  item.SubItems.Add(obj("PowerManagementCapabilities").ToString())
                  item.Group = grpUser
                  items.Add(item)
               End If
            End If

            If obj.Properties.Cast(Of PropertyData)().Any(Function(p) p.Name = "PowerManagementSupported") Then
               If (obj("PowerManagementSupported") IsNot Nothing) Then
                  Dim item As New ListViewItem("PowerManagementSupported")
                  item.SubItems.Add(obj("PowerManagementSupported").ToString())
                  item.Group = grpUser
                  items.Add(item)
               End If
            End If

            If obj.Properties.Cast(Of PropertyData)().Any(Function(p) p.Name = "SCSIBus") Then
               If (obj("SCSIBus") IsNot Nothing) Then
                  Dim item As New ListViewItem("SCSIBus")
                  item.SubItems.Add(obj("SCSIBus").ToString())
                  item.Group = grpUser
                  items.Add(item)
               End If
            End If

            If obj.Properties.Cast(Of PropertyData)().Any(Function(p) p.Name = "SCSILogicalUnit") Then
               If (obj("SCSILogicalUnit") IsNot Nothing) Then
                  Dim item As New ListViewItem("SCSILogicalUnit")
                  item.SubItems.Add(obj("SCSILogicalUnit").ToString())
                  item.Group = grpUser
                  items.Add(item)
               End If
            End If

            If obj.Properties.Cast(Of PropertyData)().Any(Function(p) p.Name = "SCSIPort") Then
               If (obj("SCSIPort") IsNot Nothing) Then
                  Dim item As New ListViewItem("SCSIPort")
                  item.SubItems.Add(obj("SCSIPort").ToString())
                  item.Group = grpUser
                  items.Add(item)
               End If
            End If

            If obj.Properties.Cast(Of PropertyData)().Any(Function(p) p.Name = "SCSITargetId") Then
               If (obj("SCSITargetId") IsNot Nothing) Then
                  Dim item As New ListViewItem("SCSITargetId")
                  item.SubItems.Add(obj("SCSITargetId").ToString())
                  item.Group = grpUser
                  items.Add(item)
               End If
            End If

            If obj.Properties.Cast(Of PropertyData)().Any(Function(p) p.Name = "SectorsPerTrack") Then
               If (obj("SectorsPerTrack") IsNot Nothing) Then
                  Dim item As New ListViewItem("SectorsPerTrack")
                  item.SubItems.Add(obj("SectorsPerTrack").ToString())
                  item.Group = grpUser
                  items.Add(item)
               End If
            End If

            If obj.Properties.Cast(Of PropertyData)().Any(Function(p) p.Name = "SerialNumber") Then
               If (obj("SerialNumber") IsNot Nothing) Then
                  Dim item As New ListViewItem("SerialNumber")
                  item.SubItems.Add(obj("SerialNumber").ToString())
                  item.Group = grpUser
                  items.Add(item)
               End If
            End If

            If obj.Properties.Cast(Of PropertyData)().Any(Function(p) p.Name = "Signature") Then
               If (obj("Signature") IsNot Nothing) Then
                  Dim item As New ListViewItem("Signature")
                  item.SubItems.Add(obj("Signature").ToString())
                  item.Group = grpUser
                  items.Add(item)
               End If
            End If

            If obj.Properties.Cast(Of PropertyData)().Any(Function(p) p.Name = "Size") Then
               If (obj("Size") IsNot Nothing) Then
                  Dim item As New ListViewItem("Size")
                  item.SubItems.Add(obj("Size").ToString())
                  item.Group = grpUser
                  items.Add(item)
               End If
            End If

            If obj.Properties.Cast(Of PropertyData)().Any(Function(p) p.Name = "Status") Then
               If (obj("Status") IsNot Nothing) Then
                  Dim item As New ListViewItem("Status")
                  item.SubItems.Add(obj("Status").ToString())
                  item.Group = grpUser
                  items.Add(item)
               End If
            End If

            If obj.Properties.Cast(Of PropertyData)().Any(Function(p) p.Name = "StatusInfo") Then
               If (obj("StatusInfo") IsNot Nothing) Then
                  Dim item As New ListViewItem("StatusInfo")
                  item.SubItems.Add(obj("StatusInfo").ToString())
                  item.Group = grpUser
                  items.Add(item)
               End If
            End If

            If obj.Properties.Cast(Of PropertyData)().Any(Function(p) p.Name = "SystemName") Then
               If (obj("SystemName") IsNot Nothing) Then
                  Dim item As New ListViewItem("SystemName")
                  item.SubItems.Add(obj("SystemName").ToString())
                  item.Group = grpUser
                  items.Add(item)
               End If
            End If

            If obj.Properties.Cast(Of PropertyData)().Any(Function(p) p.Name = "TotalCylinders") Then
               If (obj("TotalCylinders") IsNot Nothing) Then
                  Dim item As New ListViewItem("TotalCylinders")
                  item.SubItems.Add(obj("TotalCylinders").ToString())
                  item.Group = grpUser
                  items.Add(item)
               End If
            End If

            If obj.Properties.Cast(Of PropertyData)().Any(Function(p) p.Name = "TotalHeads") Then
               If (obj("TotalHeads") IsNot Nothing) Then
                  Dim item As New ListViewItem("TotalHeads")
                  item.SubItems.Add(obj("TotalHeads").ToString())
                  item.Group = grpUser
                  items.Add(item)
               End If
            End If

            If obj.Properties.Cast(Of PropertyData)().Any(Function(p) p.Name = "TotalSectors") Then
               If (obj("TotalSectors") IsNot Nothing) Then
                  Dim item As New ListViewItem("TotalSectors")
                  item.SubItems.Add(obj("TotalSectors").ToString())
                  item.Group = grpUser
                  items.Add(item)
               End If
            End If

            If obj.Properties.Cast(Of PropertyData)().Any(Function(p) p.Name = "TotalTracks") Then
               If (obj("TotalTracks") IsNot Nothing) Then
                  Dim item As New ListViewItem("TotalTracks")
                  item.SubItems.Add(obj("TotalTracks").ToString())
                  item.Group = grpUser
                  items.Add(item)
               End If
            End If

            If obj.Properties.Cast(Of PropertyData)().Any(Function(p) p.Name = "TracksPerCylinder") Then
               If (obj("TracksPerCylinder") IsNot Nothing) Then
                  Dim item As New ListViewItem("TracksPerCylinder")
                  item.SubItems.Add(obj("TracksPerCylinder").ToString())
                  item.Group = grpUser
                  items.Add(item)
               End If
            End If

         Next
      Catch ex As Exception
         MsgBox(ex.Message)
      End Try
      e.Result = items
   End Sub

   ' Update ListView when background work is completed
   Private Sub BackgroundWorker_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs)
      If e.Error IsNot Nothing Then
         MessageBox.Show("Error: " & e.Error.Message, "BackgroundWorker Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
         Return
      End If

      If e.Cancelled Then
         MessageBox.Show("Operation was cancelled.", "BackgroundWorker Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Information)
         Return
      End If

      ' result is valid
      If e.Result IsNot Nothing Then
         Dim items As List(Of ListViewItem) = CType(e.Result, List(Of ListViewItem))

         ' Safely update the ListView on the UI thread
         If lvDiskDrive.InvokeRequired Then
            lvDiskDrive.Invoke(New Action(Of List(Of ListViewItem))(AddressOf UpdateListView), items)
         Else
            UpdateListView(items)
         End If

         ' Optional: Auto-resize columns for better display
         lvDiskDrive.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent)
      End If
   End Sub

   ' Update the ListView with the retrieved items
   Private Sub UpdateListView(items As List(Of ListViewItem))
      lvDiskDrive.Items.Clear()
      lvDiskDrive.Items.AddRange(items.ToArray())
   End Sub

End Class