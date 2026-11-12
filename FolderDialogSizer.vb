Imports System.Runtime.InteropServices
Imports System.Text

''' <summary>
''' Shows a FolderBrowserDialog at a chosen size and reports the size the user left it at.
''' The classic "Browse For Folder" window has no size property, so a thread-local CBT hook
''' resizes it as it is first activated (before it paints) and reads its size as it is destroyed.
''' </summary>
Friend NotInheritable Class FolderDialogSizer

    Private Const WH_CBT As Integer = 5
    Private Const HCBT_DESTROYWND As Integer = 4
    Private Const HCBT_ACTIVATE As Integer = 5
    Private Const SWP_NOZORDER As UInteger = &H4UI
    Private Const SWP_NOACTIVATE As UInteger = &H10UI
    Private Const DialogClassName As String = "#32770"

    ' Smallest size worth restoring; anything below falls back to the default.
    Private Shared ReadOnly MinimumSize As New Drawing.Size(250, 300)

    Private Delegate Function CbtProc(nCode As Integer, wParam As IntPtr, lParam As IntPtr) As IntPtr

    <StructLayout(LayoutKind.Sequential)>
    Private Structure RECT
        Public Left As Integer
        Public Top As Integer
        Public Right As Integer
        Public Bottom As Integer
    End Structure

    <DllImport("user32.dll", SetLastError:=True)>
    Private Shared Function SetWindowsHookEx(idHook As Integer, lpfn As CbtProc, hMod As IntPtr, dwThreadId As Integer) As IntPtr
    End Function

    <DllImport("user32.dll")>
    Private Shared Function UnhookWindowsHookEx(hhk As IntPtr) As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function

    <DllImport("user32.dll")>
    Private Shared Function CallNextHookEx(hhk As IntPtr, nCode As Integer, wParam As IntPtr, lParam As IntPtr) As IntPtr
    End Function

    <DllImport("kernel32.dll")>
    Private Shared Function GetCurrentThreadId() As Integer
    End Function

    <DllImport("user32.dll", CharSet:=CharSet.Unicode)>
    Private Shared Function GetClassName(hWnd As IntPtr, lpClassName As StringBuilder, nMaxCount As Integer) As Integer
    End Function

    <DllImport("user32.dll")>
    Private Shared Function SetWindowPos(hWnd As IntPtr, hWndInsertAfter As IntPtr, x As Integer, y As Integer, cx As Integer, cy As Integer, uFlags As UInteger) As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function

    <DllImport("user32.dll")>
    Private Shared Function GetWindowRect(hWnd As IntPtr, ByRef lpRect As RECT) As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function

    Private ReadOnly hookCallback As CbtProc   ' held in a field so the GC can't collect it mid-dialog
    Private hookHandle As IntPtr = IntPtr.Zero
    Private dialogHandle As IntPtr = IntPtr.Zero
    Private ReadOnly targetBounds As Drawing.Rectangle
    Private finalSize As Drawing.Size = Drawing.Size.Empty

    Private Sub New(bounds As Drawing.Rectangle)
        targetBounds = bounds
        hookCallback = AddressOf HookProc
    End Sub

    ''' <param name="size">In: the size to open at (falls back to defaultSize if unusable).
    ''' Out: the size the user left the window at, if it could be read.</param>
    Public Shared Function ShowDialog(dialog As FolderBrowserDialog, owner As Form, ByRef size As Drawing.Size, defaultSize As Drawing.Size) As DialogResult
        If size.Width < MinimumSize.Width OrElse size.Height < MinimumSize.Height Then size = defaultSize

        ' Fit within, and center on, the working area of the screen the owner is on.
        Dim area As Drawing.Rectangle = Screen.FromControl(owner).WorkingArea
        Dim w As Integer = Math.Min(size.Width, area.Width)
        Dim h As Integer = Math.Min(size.Height, area.Height)
        Dim bounds As New Drawing.Rectangle(area.Left + (area.Width - w) \ 2, area.Top + (area.Height - h) \ 2, w, h)

        Dim sizer As New FolderDialogSizer(bounds)
        sizer.hookHandle = SetWindowsHookEx(WH_CBT, sizer.hookCallback, IntPtr.Zero, GetCurrentThreadId())
        Try
            Dim result As DialogResult = dialog.ShowDialog(owner)
            If Not sizer.finalSize.IsEmpty Then size = sizer.finalSize
            Return result
        Finally
            If sizer.hookHandle <> IntPtr.Zero Then UnhookWindowsHookEx(sizer.hookHandle)
            GC.KeepAlive(sizer.hookCallback)
        End Try
    End Function

    Private Function HookProc(nCode As Integer, wParam As IntPtr, lParam As IntPtr) As IntPtr
        Try
            If nCode = HCBT_ACTIVATE AndAlso dialogHandle = IntPtr.Zero AndAlso IsDialogWindow(wParam) Then
                ' First dialog activated on this thread during ShowDialog is the folder browser.
                dialogHandle = wParam
                SetWindowPos(wParam, IntPtr.Zero, targetBounds.X, targetBounds.Y, targetBounds.Width, targetBounds.Height, SWP_NOZORDER Or SWP_NOACTIVATE)
            ElseIf nCode = HCBT_DESTROYWND AndAlso wParam = dialogHandle AndAlso dialogHandle <> IntPtr.Zero Then
                Dim r As RECT
                If GetWindowRect(wParam, r) Then finalSize = New Drawing.Size(r.Right - r.Left, r.Bottom - r.Top)
            End If
        Catch
            ' Never let a sizing problem break the dialog itself.
        End Try
        Return CallNextHookEx(hookHandle, nCode, wParam, lParam)
    End Function

    Private Shared Function IsDialogWindow(hWnd As IntPtr) As Boolean
        Dim name As New StringBuilder(32)
        GetClassName(hWnd, name, name.Capacity)
        Return name.ToString() = DialogClassName
    End Function

End Class
