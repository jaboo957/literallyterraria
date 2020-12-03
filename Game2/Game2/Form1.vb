Public Class Terraria
    Private Sub Timer1_Tick(sender As Object, e As EventArgs)
        SCal.Location = New Point(SCal.Location.X + 5, SCal.Location.Y + 5)
    End Sub
    Sub Move1(P As PictureBox, X As Integer, Y As Integer)
        P.Location = New Point(P.Location.X, P.Location.Y)
    End Sub
    Private Sub Timer1_Tick_1(sender As Object, e As EventArgs)
        follow(SCal)
        PC.Location = New Point(PC.Location.X, PC.Location.Y)
    End Sub
    Private Sub Form1_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.R
                SCal.Image.RotateFlip(RotateFlipType.Rotate180FlipY)
                Me.Refresh()
            Case Keys.Up
                MoveTo(PC, 0, -5)
            Case Keys.Down
                MoveTo(PC, 0, 5)
            Case Keys.Left
                MoveTo(PC, -5, 0)
            Case Keys.Right
                MoveTo(PC, 5, 0)
            Case Keys.Space
                CreateNew("bullet", Projectile, PC.Location)

            Case Else
                Projectile.Location = PC.Location
                Projectile.Visible = True
                Timer1.Enabled = True
        End Select
    End Sub
    Sub CreateNew(name As String, pic As PictureBox, location As Point)
        Dim p As New PictureBox
        p.Location = location
        p.Image = pic.Image
        p.BackColor = pic.BackColor
        p.Name = name
        p.Width = pic.Width
        p.Height = pic.Height
        p.SizeMode = PictureBoxSizeMode.StretchImage
        Controls.Add(p)
    End Sub
    Sub follow(p As PictureBox)
        Static headstart As Integer
        Static c As New Collection
        c.Add(SCal.Location)
        headstart = headstart + 1
        If headstart > 10 Then
            p.Location = c.Item(1)
            c.Remove(1)
        End If
    End Sub
    Public Sub follow(PC)
        Dim x, y As Integer
        If PC.Location.X > SCal.Location.X Then
            x = -5
        Else
            x = 5
        End If
        MoveTo(PC, x, 0)
        If PC.Location.Y < SCal.Location.Y Then
            y = 5
        Else
            y = -5
        End If
        MoveTo(PC, x, y)
    End Sub
    Function Collision(p As PictureBox, t As String)
        Dim col As Boolean

        For Each c In Controls
            Dim obj As Control
            obj = c
            If p.Bounds.IntersectsWith(obj.Bounds) And (obj.Name.ToUpper.EndsWith(t.ToUpper) Or obj.Name.ToUpper.StartsWith(t.ToUpper)) Then
                col = True
            End If
        Next
        Return col
    End Function
    'Return true or false if moving to the new location is clear of objects ending with t
    Function IsClear(p As PictureBox, distx As Integer, disty As Integer, t As String) As Boolean
        Dim b As Boolean

        p.Location += New Point(distx, disty)
        b = Not Collision(p, t)
        p.Location -= New Point(distx, disty)
        Return b
    End Function
    'Moves and object (won't move onto objects containing  "wall" and shows green if object ends with "win"
    Function Collision(p As String, t As String, Optional ByRef other As Object = vbNull)
        Return Collision(getObject(p), t, other)
    End Function
    Sub MoveTo(p As PictureBox, distx As Integer, disty As Integer)
        If p Is Nothing Then
            Return
        End If
        If IsClear(p, distx, disty, "WALL") Then
            p.Location += New Point(distx, disty)
        End If
        Dim other As Object = Nothing
        If Collision("BSDart", "PC", other) Then
            getObject("BSDart").Visible = False
            getObject("PC").Visible = False
            Me.BackColor = Color.IndianRed
        End If
        If IsClear(p, distx, disty, "WALL") Then
            p.Location += New Point(distx, disty)
        End If
        If Collision("SCal", "Projectile", other) Then
            getObject("SCal").Visible = False
            getObject("Projectile").Visible = False
            other.visible = False
            Me.BackColor = Color.Green
            Return
        End If
    End Sub
    Function Collision(p As PictureBox, t As String, Optional ByRef other As Object = vbNull)
        Dim col As Boolean

        For Each c In Controls
            Dim obj As Control
            obj = c
            If obj.Visible AndAlso p.Bounds.IntersectsWith(obj.Bounds) And obj.Name.ToUpper.Contains(t.ToUpper) Then
                col = True
                other = obj
            End If
        Next
        Return col
    End Function
    Sub MoveTo(p As String, distx As Integer, disty As Integer)
        For Each c In Controls
            If c.name.toupper.ToString.Contains(p.ToUpper) Then
                MoveTo(c, distx, disty)
            End If
        Next
    End Sub
    Public Sub Timer2_Tick_1(sender As Object, e As EventArgs)
        follow(SCal)
        Static headstart As Integer
        Static c As New Collection
        c.Add(PC.Location)
        headstart = headstart + 1
        If headstart > 10 Then
            PC.Location = c.Item(1)
            c.Remove(1)
        End If
    End Sub
    Public Sub chase(p As PictureBox)
        Dim x, y As Integer
        If p.Location.X > PC.Location.X Then
            x = -5
        Else
            x = 5
        End If
        MoveTo(p, x, 0)
        If p.Location.Y < SCal.Location.Y Then
            y = 5
        Else
            y = -5
        End If
        MoveTo(p, x, y)
    End Sub
    Private Sub Timer1_Tick_2(sender As Object, e As EventArgs) Handles Timer1.Tick
        MoveTo("bullet", -10, 0)
    End Sub
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SCal.Image.RotateFlip(RotateFlipType.Rotate90FlipX)
        PC.Image.RotateFlip(RotateFlipType.Rotate180FlipY)
    End Sub
End Class