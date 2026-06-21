'--------------------------------------------------------------------------------------------------
' Win32_UserAccount class
' https://learn.microsoft.com/en-us/windows/win32/cimwin32prov/win32-useraccount
'
'    © Remus Rigo
'       v1.0 2026-06-21
'--------------------------------------------------------------------------------------------------

Imports System.ComponentModel
Imports System.Management
Imports System.Private
Imports System.Threading
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports SharedInterfaces

Public Class frmUsers
   Implements IModuleForm
   <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
   Public Property MainForm As IMainForm Implements IModuleForm.MainForm

   Public remoteHost, remoteUser, remotePass As String
   Private Sub frmUsers_Load(sender As Object, e As EventArgs) Handles MyBase.Load
      lvUsers.BackColor = Color.FromArgb(224, 234, 213)

      Dim backgroundWorker As New System.ComponentModel.BackgroundWorker()
      AddHandler backgroundWorker.DoWork, AddressOf BackgroundWorker_DoWork
      AddHandler backgroundWorker.RunWorkerCompleted, AddressOf BackgroundWorker_RunWorkerCompleted
      backgroundWorker.RunWorkerAsync()

      If MainForm IsNot Nothing Then
         If remoteHost <> "" Then
            MainForm.SetTitle("OSI: Users v1.0 on [" & remoteHost & "]")
         Else
            MainForm.SetTitle("OSI: Users v1.0")
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
         Dim myQuery As New ObjectQuery("SELECT * FROM Win32_UserAccount")
         Dim searcher As New ManagementObjectSearcher(scope, myQuery)
         Dim cnt As Integer = 0

         For Each obj As ManagementObject In searcher.Get()
            cnt += 1
            Dim grpUser As New ListViewGroup("User #" & cnt, HorizontalAlignment.Left)
            lvUsers.Groups.Add(grpUser)

            If obj.Properties.Cast(Of PropertyData)().Any(Function(p) p.Name = "Name") Then
               If (obj("Name") IsNot Nothing) Then
                  Dim item As New ListViewItem("Name")
                  item.SubItems.Add(obj("Name").ToString())
                  item.Group = grpUser
                  items.Add(item)
               End If
            End If

            If obj.Properties.Cast(Of PropertyData)().Any(Function(p) p.Name = "FullName") Then
               If (obj("FullName") IsNot Nothing) Then
                  Dim item As New ListViewItem("Full Name")
                  item.SubItems.Add(obj("FullName").ToString())
                  item.Group = grpUser
                  items.Add(item)
               End If
            End If

            If obj.Properties.Cast(Of PropertyData)().Any(Function(p) p.Name = "Domain") Then
               If (obj("Domain") IsNot Nothing) Then
                  Dim item As New ListViewItem("Domain")
                  item.SubItems.Add(obj("Domain").ToString())
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

            If obj.Properties.Cast(Of PropertyData)().Any(Function(p) p.Name = "Description") Then
               If (obj("Description") IsNot Nothing) Then
                  Dim item As New ListViewItem("Description")
                  item.SubItems.Add(obj("Description").ToString())
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

            If obj.Properties.Cast(Of PropertyData)().Any(Function(p) p.Name = "AccountType") Then
               If (obj("AccountType") IsNot Nothing) Then
                  Dim item As New ListViewItem("Account Type")
                  Dim strType As String = ""
                  Select Case obj("AccountType")
                     Case 256
                        strType = "Temporary duplicate account"
                     Case 512
                        strType = "Normal account"
                     Case 2048
                        strType = "Interdomain trust account"
                     Case 4096
                        strType = "Workstation trust account"
                     Case 8192
                        strType = "Server trust account"
                     Case Else
                        strType = "not defined"
                  End Select
                  item.SubItems.Add(strType)
                  item.Group = grpUser
                  items.Add(item)
               End If
            End If

            If obj.Properties.Cast(Of PropertyData)().Any(Function(p) p.Name = "LocalAccount") Then
               If (obj("LocalAccount") IsNot Nothing) Then
                  Dim item As New ListViewItem("Local Account")
                  item.SubItems.Add(obj("LocalAccount").ToString())
                  item.Group = grpUser
                  items.Add(item)
               End If
            End If

            If obj.Properties.Cast(Of PropertyData)().Any(Function(p) p.Name = "Disabled") Then
               If (obj("Disabled") IsNot Nothing) Then
                  Dim item As New ListViewItem("Disabled")
                  item.SubItems.Add(obj("Disabled").ToString())
                  item.Group = grpUser
                  items.Add(item)
               End If
            End If

            If obj.Properties.Cast(Of PropertyData)().Any(Function(p) p.Name = "Lockout") Then
               If (obj("Lockout") IsNot Nothing) Then
                  Dim item As New ListViewItem("Lockout")
                  item.SubItems.Add(obj("Lockout").ToString())
                  item.Group = grpUser
                  items.Add(item)
               End If
            End If

            If obj.Properties.Cast(Of PropertyData)().Any(Function(p) p.Name = "PasswordChangeable") Then
               If (obj("PasswordChangeable") IsNot Nothing) Then
                  Dim item As New ListViewItem("Password Changeable")
                  item.SubItems.Add(obj("PasswordChangeable").ToString())
                  item.Group = grpUser
                  items.Add(item)
               End If
            End If

            If obj.Properties.Cast(Of PropertyData)().Any(Function(p) p.Name = "PasswordExpires") Then
               If (obj("PasswordExpires") IsNot Nothing) Then
                  Dim item As New ListViewItem("Password Expires")
                  item.SubItems.Add(obj("PasswordExpires").ToString())
                  item.Group = grpUser
                  items.Add(item)
               End If
            End If

            If obj.Properties.Cast(Of PropertyData)().Any(Function(p) p.Name = "PasswordRequired") Then
               If (obj("PasswordRequired") IsNot Nothing) Then
                  Dim item As New ListViewItem("Password Required")
                  item.SubItems.Add(obj("PasswordRequired").ToString())
                  item.Group = grpUser
                  items.Add(item)
               End If
            End If

            If obj.Properties.Cast(Of PropertyData)().Any(Function(p) p.Name = "InstallDate") Then
               If (obj("InstallDate") IsNot Nothing) Then
                  Dim item As New ListViewItem("Install Date")
                  item.SubItems.Add(obj("InstallDate").ToString())
                  item.Group = grpUser
                  items.Add(item)
               End If
            End If

            If obj.Properties.Cast(Of PropertyData)().Any(Function(p) p.Name = "SID") Then
               If (obj("SID") IsNot Nothing) Then
                  Dim item As New ListViewItem("SID")
                  item.SubItems.Add(obj("SID").ToString())
                  item.Group = grpUser
                  items.Add(item)
               End If
            End If

            If obj.Properties.Cast(Of PropertyData)().Any(Function(p) p.Name = "SIDType") Then
               If (obj("SIDType") IsNot Nothing) Then
                  Dim item As New ListViewItem("SID Type")
                  Dim strType As String = ""
                  Select Case obj("SIDType")
                     Case 1
                        strType = "User"
                     Case 2
                        strType = "Group"
                     Case 3
                        strType = "Domain"
                     Case 4
                        strType = "Alias"
                     Case 5
                        strType = "Well Known Group"
                     Case 6
                        strType = "Deleted Account"
                     Case 7
                        strType = "Invalid"
                     Case 8
                        strType = "Unknown"
                     Case 9
                        strType = "Computer"
                     Case Else
                        strType = "not defined"
                  End Select
                  item.SubItems.Add(strType)
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
         If lvUsers.InvokeRequired Then
            lvUsers.Invoke(New Action(Of List(Of ListViewItem))(AddressOf UpdateListView), items)
         Else
            UpdateListView(items)
         End If

         ' Optional: Auto-resize columns for better display
         lvUsers.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent)
      End If
   End Sub

   ' Update the ListView with the retrieved items
   Private Sub UpdateListView(items As List(Of ListViewItem))
      lvUsers.Items.Clear()
      lvUsers.Items.AddRange(items.ToArray())
   End Sub

End Class