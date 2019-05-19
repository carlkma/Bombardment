Public Class Form1
    'Bombardment: a two-dimensional player-versus-player game
    'By Carl Ma And Kevin Cheng
    Public Declare Function GetAsyncKeyState Lib "user32" (ByVal vkey As Integer) As Short
    Dim canMove1, canMove2, canBomb1, canBomb2 As Boolean
    Dim ex, ey, upend, downend, leftend, rightend, range1, range2, p1x, p1y, p2x, p2y, countMove1, countMove2, p1Attack, p2Attack, bomb1x, bomb1y, bomb2x, bomb2y As Byte
    Dim board(14, 14) As Byte
    Dim dir1, dir2 As String
    Dim special As Double = 0
    Private Sub LblStart_Click(sender As Object, e As EventArgs) Handles lblStart.Click
        eraseBoard()
        genBoard()
        p1Attack = 0
        p2Attack = 0
        PicPlayer1.Left = 165
        PicPlayer1.Top = 165
        PicPlayer2.Left = 565
        PicPlayer2.Top = 565
        lblMessage1.Visible = False
        lblMessage2.Visible = False
        TimerMaster.Enabled = True
        range1 = 1
        range2 = 1
        p1x = 3
        p1y = 3
        p2x = 11
        p2y = 11
        canMove1 = True
        canMove2 = True
        canBomb1 = True
        canBomb2 = True
        PicPlayer1.BackgroundImage = My.Resources.blue
        PicPlayer2.BackgroundImage = My.Resources.red
    End Sub
    Sub PlayLoopingBackgroundSoundResource()
        My.Computer.Audio.Play(My.Resources.bgm, AudioPlayMode.BackgroundLoop)
    End Sub
    Private Sub TimerExplode_Tick(sender As Object, e As EventArgs) Handles TimerExplode.Tick
        'Restore exploded blocks
        TimerExplode.Enabled = False
        For i = upend To downend Step 1
            Me.Controls("picx" + CStr(ex) + "y" + CStr(i)).BackColor = Color.White
        Next
        For i = leftend To rightend Step 1
            Me.Controls("picx" + CStr(i) + "y" + CStr(ey)).BackColor = Color.White
        Next
    End Sub
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        PlayLoopingBackgroundSoundResource()
        PicPlayer1.BackgroundImage = My.Resources.blue
        PicPlayer2.BackgroundImage = My.Resources.red
        genBoard()
        p1x = 3
        p1y = 3
        p2x = 11
        p2y = 11
        canMove1 = True
        canMove2 = True
        canBomb1 = True
        canBomb2 = True
        TimerMaster.Enabled = True
        range1 = 1
        range2 = 1
    End Sub
    Private Sub TimerBomb1_Tick(sender As Object, e As EventArgs) Handles TimerBomb1.Tick
        explode(bomb1x, bomb1y, range1)
        canBomb1 = True
        TimerBomb1.Enabled = False 'only explode once
    End Sub
    Private Sub TimerBomb2_Tick(sender As Object, e As EventArgs) Handles TimerBomb2.Tick
        explode(bomb2x, bomb2y, range2)
        canBomb2 = True
        TimerBomb2.Enabled = False 'only explode once
    End Sub
    Sub explode(ByVal x As Byte, ByVal y As Byte, ByVal range As Byte)
        Dim i As Byte 'counter
        For i = 0 To range Step 1 'upward
            If i > y Then 'upper edge of the board reached
                upend = 0 'record end of explosion
                Exit For
            Else
                upend = y - i
                Select Case board(y - i, x)
                    Case 1 'explodable
                        Me.Controls("picx" + CStr(x) + "y" + CStr(y - i)).BackColor = Color.Orange
                        board(y - i, x) = 0 'block destroyed by bomb
                        Exit For
                    Case 2 'unexplodable
                        upend += 1
                        Exit For
                    Case Else 'others
                        Me.Controls("picx" + CStr(x) + "y" + CStr(y - i)).BackColor = Color.Orange
                        board(y - i, x) = 0 'block destroyed by bomb
                        Me.Controls("picx" + CStr(x) + "y" + CStr(y - i)).BackgroundImage = Nothing
                End Select
            End If
        Next
        For i = 1 To range Step 1 'downward
            If y + i > 14 Then 'lower edge of the board reached
                downend = 14 'record end of explosion
                Exit For
            Else
                downend = y + i
                Select Case board(y + i, x)
                    Case 1 'explodable
                        Me.Controls("picx" + CStr(x) + "y" + CStr(y + i)).BackColor = Color.Orange
                        board(y + i, x) = 0 'block destroyed by bomb
                        Exit For
                    Case 2 'unexplodable
                        downend -= 1
                        Exit For
                    Case Else 'others
                        Me.Controls("picx" + CStr(x) + "y" + CStr(y + i)).BackColor = Color.Orange
                        board(y + i, x) = 0 'block destroyed by bomb
                        Me.Controls("picx" + CStr(x) + "y" + CStr(y + i)).BackgroundImage = Nothing
                End Select
            End If
        Next
        For i = 1 To range Step 1 'leftward
            If i > x Then 'left edge of the board reached
                leftend = 0 'record end of explosion
                Exit For
            Else
                leftend = x - i
                Select Case board(y, x - i)
                    Case 1 'explodable
                        Me.Controls("picx" + CStr(x - i) + "y" + CStr(y)).BackColor = Color.Orange
                        board(y, x - i) = 0 'block destroyed by bomb
                        Exit For
                    Case 2 'unexplodable
                        leftend += 1
                        Exit For
                    Case Else 'others
                        Me.Controls("picx" + CStr(x - i) + "y" + CStr(y)).BackColor = Color.Orange
                        board(y, x - i) = 0 'block destroyed by bomb
                        Me.Controls("picx" + CStr(x - i) + "y" + CStr(y)).BackgroundImage = Nothing
                End Select
            End If
        Next
        For i = 1 To range Step 1 'rightward
            If x + i > 14 Then 'right edge of the board reached
                rightend = 14 'record end of explosion
                Exit For
            Else
                rightend = x + i
                Select Case board(y, x + i)
                    Case 1 'explodable
                        Me.Controls("picx" + CStr(x + i) + "y" + CStr(y)).BackColor = Color.Orange
                        board(y, x + i) = 0 'block destroyed by bomb
                        Exit For
                    Case 2 'unexplodable
                        rightend -= 1
                        Exit For
                    Case Else 'others
                        Me.Controls("picx" + CStr(x + i) + "y" + CStr(y)).BackColor = Color.Orange
                        board(y, x + i) = 0 'block destroyed by bomb
                        Me.Controls("picx" + CStr(x + i) + "y" + CStr(y)).BackgroundImage = Nothing
                End Select
            End If
        Next
        'Explosion over
        'Check player
        If (p1x = x And p1y >= upend And p1y <= downend) Or (p1y = y And p1x >= leftend And p1x <= rightend) Then 'Player 1 will be killed
            kill(1)
            lblMessage1.Text = “Player 1 is dead!"
            lblMessage1.Visible = True
        End If
        If (p2x = x And p2y >= upend And p2y <= downend) Or (p2y = y And p2x >= leftend And p2x <= rightend) Then 'Player 2 will be killed
            kill(2)
            lblMessage2.Text = "Player 2 is dead!"
            lblMessage2.Visible = True
        End If
        TimerExplode.Enabled = True
        ex = x
        ey = y
        board(y, x) = 0 'restore block
        Me.Controls("picx" + CStr(x) + "y" + CStr(y)).BackgroundImage = Nothing
    End Sub
    Sub kill(ByVal id As Byte)
        TimerMaster.Enabled = False
        TimerBomb1.Enabled = False
        TimerBomb2.Enabled = False
        TimerMove1.Enabled = False
        TimerMove2.Enabled = False
        TimerExplode.Enabled = False
    End Sub
    Private Sub TimerMove1_Tick(sender As Object, e As EventArgs) Handles TimerMove1.Tick
        If countMove1 < 5 Then 'Moves 50/10=5 times
            If dir1 = “u” Then
                PicPlayer1.Top -= 10
            End If
            If dir1 = “d” Then
                PicPlayer1.Top += 10
            End If
            If dir1 = “l” Then
                PicPlayer1.Left -= 10
            End If
            If dir1 = “r” Then
                PicPlayer1.Left += 10
            End If
            countMove1 += 1
        Else 'Moving process completed
            canMove1 = True 'Allow other moving attempts
            TimerMove1.Enabled = False 'Shuts down TimerMove1
        End If
    End Sub
    Private Sub TimerMove2_Tick(sender As Object, e As EventArgs) Handles TimerMove2.Tick
        If countMove2 < 5 Then 'Moves 50/10=5 times
            If dir2 = “u” Then
                PicPlayer2.Top -= 10
            End If
            If dir2 = “d” Then
                PicPlayer2.Top += 10
            End If
            If dir2 = “l” Then
                PicPlayer2.Left -= 10
            End If
            If dir2 = “r” Then
                PicPlayer2.Left += 10
            End If
            countMove2 += 1
        Else 'Moving process completed
            canMove2 = True 'Allow other moving attempts
            TimerMove2.Enabled = False 'Shuts down TimerMove2
        End If
    End Sub
    Function genBoard() As Byte(,)
        Dim r As New Random
        Dim temp As Byte
        temp = r.Next(4)
        If temp = 0 Then
            board = {{1, 1, 0, 2, 0, 0, 0, 1, 1, 1, 1, 2, 0, 2, 0}, {0, 2, 0, 1, 0, 0, 2, 2, 0, 2, 1, 0, 0, 1, 0}, {0, 0, 2, 0, 2, 1, 2, 1, 1, 1, 2, 0, 2, 2, 0}, {1, 1, 0, 0, 0, 1, 2, 2, 2, 1, 0, 0, 0, 0, 1}, {0, 2, 2, 0, 2, 1, 1, 0, 1, 0, 2, 0, 2, 2, 0}, {0, 1, 2, 1, 0, 1, 2, 2, 1, 1, 0, 0, 0, 1, 0}, {0, 1, 0, 1, 2, 0, 2, 2, 2, 2, 1, 2, 1, 2, 0}, {0, 2, 2, 0, 2, 0, 1, 1, 0, 0, 0, 0, 0, 2, 0}, {1, 0, 2, 0, 2, 1, 2, 1, 2, 2, 2, 0, 2, 2, 1}, {2, 1, 0, 0, 0, 1, 2, 1, 0, 1, 0, 0, 0, 0, 0}, {1, 1, 2, 0, 2, 0, 1, 2, 1, 2, 2, 0, 2, 2, 1}, {0, 2, 2, 0, 2, 0, 2, 2, 0, 1, 2, 0, 2, 0, 1}, {0, 1, 1, 1, 0, 1, 1, 2, 0, 1, 0, 0, 0, 1, 1}, {1, 2, 1, 1, 2, 0, 1, 1, 2, 1, 1, 0, 2, 1, 1}, {0, 0, 0, 1, 0, 1, 2, 1, 0, 1, 2, 0, 1, 1, 1}}
        End If
        If temp = 1 Then
            board = {{1, 2, 1, 1, 2, 1, 0, 0, 2, 1, 0, 1, 1, 2, 0}, {0, 1, 0, 1, 1, 1, 2, 1, 0, 1, 2, 1, 1, 1, 1}, {1, 2, 2, 0, 2, 0, 2, 0, 2, 0, 2, 0, 2, 1, 2}, {0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 0, 0, 0, 1, 1}, {1, 2, 2, 0, 2, 1, 2, 1, 2, 1, 2, 0, 2, 1, 2}, {1, 1, 1, 1, 1, 1, 2, 2, 2, 1, 1, 1, 2, 0, 0}, {2, 2, 1, 2, 0, 2, 2, 2, 0, 1, 2, 0, 2, 2, 1}, {1, 1, 0, 1, 0, 1, 0, 2, 1, 0, 1, 0, 0, 0, 1}, {2, 0, 2, 0, 2, 1, 2, 2, 2, 0, 2, 0, 2, 2, 1}, {0, 0, 0, 0, 0, 1, 2, 0, 1, 1, 0, 0, 0, 1, 1}, {1, 2, 2, 0, 2, 1, 2, 0, 2, 0, 2, 0, 2, 2, 1}, {1, 2, 1, 1, 1, 0, 1, 1, 1, 1, 2, 0, 1, 2, 0}, {0, 1, 1, 2, 0, 2, 0, 1, 2, 1, 1, 0, 0, 0, 1}, {2, 0, 2, 0, 2, 1, 0, 1, 1, 2, 2, 0, 2, 0, 2}, {1, 1, 0, 0, 0, 1, 2, 1, 0, 1, 0, 0, 0, 0, 0}}
        End If
        If temp = 2 Then
            board = {{1, 1, 1, 1, 2, 1, 2, 1, 2, 1, 1, 1, 1, 2, 1}, {1, 2, 1, 0, 0, 1, 2, 0, 1, 1, 2, 0, 0, 1, 1}, {1, 2, 2, 0, 2, 1, 1, 0, 2, 1, 2, 0, 2, 1, 2}, {1, 1, 0, 0, 0, 0, 2, 1, 1, 0, 0, 0, 0, 0, 1}, {0, 2, 1, 1, 2, 1, 2, 2, 1, 2, 0, 1, 2, 0, 2}, {0, 2, 2, 1, 2, 1, 2, 2, 1, 0, 1, 2, 2, 1, 1}, {0, 2, 2, 1, 0, 1, 2, 1, 0, 2, 2, 1, 0, 0, 1}, {0, 1, 0, 0, 2, 2, 1, 1, 1, 1, 0, 2, 1, 2, 1}, {2, 0, 2, 0, 2, 0, 1, 1, 0, 2, 2, 0, 2, 0, 0}, {2, 1, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 2, 1}, {2, 0, 2, 0, 2, 0, 1, 1, 1, 1, 2, 0, 1, 0, 1}, {0, 1, 0, 0, 0, 0, 2, 2, 1, 1, 0, 0, 0, 0, 1}, {2, 0, 2, 0, 2, 1, 1, 0, 1, 2, 2, 0, 2, 2, 1}, {0, 1, 1, 1, 1, 1, 2, 1, 2, 1, 1, 1, 2, 1, 1}, {1, 2, 1, 2, 1, 0, 2, 1, 1, 1, 2, 0, 1, 1, 1}}
        End If
        If temp = 3 Then
            board = {{0, 2, 1, 0, 2, 0, 2, 0, 0, 1, 1, 0, 0, 1, 2}, {1, 2, 0, 0, 0, 2, 0, 1, 1, 2, 1, 1, 2, 1, 1}, {1, 0, 1, 0, 1, 1, 0, 0, 2, 2, 1, 0, 2, 2, 0}, {2, 2, 0, 0, 0, 2, 0, 1, 1, 0, 0, 0, 0, 0, 2}, {2, 1, 1, 0, 2, 2, 0, 2, 1, 0, 2, 0, 1, 2, 2}, {2, 2, 2, 1, 0, 1, 2, 2, 2, 2, 2, 0, 1, 2, 2}, {1, 1, 0, 2, 2, 0, 1, 1, 0, 1, 1, 2, 1, 1, 0}, {2, 1, 2, 0, 1, 1, 2, 2, 2, 1, 1, 2, 0, 2, 1}, {1, 0, 1, 2, 2, 2, 2, 2, 1, 0, 1, 0, 1, 2, 1}, {0, 1, 2, 0, 1, 2, 0, 0, 2, 2, 1, 1, 0, 0, 0}, {0, 2, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 2, 1}, {1, 2, 0, 0, 0, 0, 2, 2, 0, 0, 0, 0, 0, 0, 0}, {1, 0, 2, 0, 1, 2, 0, 1, 2, 2, 1, 0, 1, 2, 1}, {2, 0, 0, 0, 2, 0, 2, 1, 1, 2, 2, 0, 2, 2, 1}, {0, 0, 1, 2, 2, 2, 0, 1, 2, 2, 0, 1, 0, 1, 2}}
        End If
        For x = 0 To 14
            For y = 0 To 14
                Dim pb As New PictureBox
                pb.Width = 50
                pb.Height = 50
                pb.Top = 15 + y * 50
                pb.Left = 15 + x * 50
                pb.BackColor = Color.Black
                pb.Name = "picx" + CStr(x) + "y" + CStr(y)
                Me.Controls.Add(pb)
                If board(y, x) = 0 Then
                    pb.BackColor = Color.White
                End If
                If board(y, x) = 1 Then
                    pb.BackColor = Color.LightBlue
                End If
                If board(y, x) = 2 Then
                    pb.BackColor = Color.Black
                End If
            Next
        Next
        Return board
    End Function
    Sub eraseBoard()
        For i = Me.Controls.Count - 1 To 6 Step -1
            If TypeOf Me.Controls(i) Is PictureBox Then
                Me.Controls.RemoveAt(i)
            End If
        Next
    End Sub
    Sub move1(ByVal d As String)
        countMove1 = 0 'Initialize count
        dir1 = d 'Record direction of movement for TimerMove
        canMove1 = False 'Shuts down moving attempts in other directions
        If dir1 = “u” Then
            p1y -= 1
        End If
        If dir1 = “d” Then
            p1y += 1
        End If
        If dir1 = “l” Then
            p1x -= 1
        End If
        If dir1 = “r” Then
            p1x += 1
        End If
        board(p1y, p1x) = 0
        Me.Controls("picx" + CStr(p1x) + "y" + CStr(p1y)).BackColor = Color.White
        Me.Controls("picx" + CStr(p1x) + "y" + CStr(p1y)).BackgroundImage = Nothing
        TimerMove1.Enabled = True
    End Sub
    Sub move2(ByVal d As String)
        countMove2 = 0 'Initialize count
        dir2 = d 'Record direction of movement for TimerMove
        canMove2 = False 'Shuts down moving attempts in other directions
        If dir2 = “u” Then
            p2y -= 1
        End If
        If dir2 = “d” Then
            p2y += 1
        End If
        If dir2 = “l” Then
            p2x -= 1
        End If
        If dir2 = “r” Then
            p2x += 1
        End If
        board(p2y, p2x) = 0
        Me.Controls("picx" + CStr(p2x) + "y" + CStr(p2y)).BackColor = Color.White
        Me.Controls("picx" + CStr(p2x) + "y" + CStr(p2y)).BackgroundImage = Nothing
        TimerMove2.Enabled = True
    End Sub
    Private Sub TimerMaster_Tick(sender As Object, e As EventArgs) Handles TimerMaster.Tick
        'Different Cases:
        '0: empty
        '1: explodable
        '2: unexplodable
        '3: bomb
        '4: DELETED
        '5: range tool
        '6: DELETED
        '7: mine tool
        '8: mine
        If (GetAsyncKeyState(Keys.Up)) Then
            If canMove1 = True And p1y <> 0 Then 'can move and not on top
                Select Case board(p1y - 1, p1x) 'check desired location
                    Case 0 'empty
                        move1("u")
                    Case 5
                        move1("u")
                        range1 += 1 'range increases
                    Case 7
                        move1("u")
                        PicPlayer1.BackgroundImage = My.Resources.bluemine
                        p1Attack = 7 'deploy mine mode
                    Case 8
                        move1("u")
                        PicPlayer1.Top -= 50
                        explode(p1x, p1y, 2) 'mine explode
                End Select
            End If
        End If
        If (GetAsyncKeyState(Keys.Down)) Then
            If canMove1 = True And p1y <> 14 Then 'can move and not on bottom
                Select Case board(p1y + 1, p1x) 'check desired location
                    Case 0 'empty
                        move1("d")
                    Case 5
                        move1("d")
                        range1 += 1 'range increases
                    Case 7
                        move1("d")
                        PicPlayer1.BackgroundImage = My.Resources.bluemine
                        p1Attack = 7 'deploy mine mode
                    Case 8
                        move1("d")
                        PicPlayer1.Top += 50
                        explode(p1x, p1y, 2) 'mine explode
                End Select
            End If
        End If
        If (GetAsyncKeyState(Keys.Left)) Then
            If canMove1 = True And p1x <> 0 Then
                Select Case board(p1y, p1x - 1)
                    Case 0
                        move1("l")
                    Case 5
                        move1("l")
                        range1 += 1
                    Case 7
                        move1("l")
                        PicPlayer1.BackgroundImage = My.Resources.bluemine
                        p1Attack = 7
                    Case 8
                        move1("l")
                        PicPlayer1.Left -= 50
                        explode(p1x, p1y, 2)
                End Select
            End If
        End If
        If (GetAsyncKeyState(Keys.Right)) Then
            If canMove1 = True And p1x <> 14 Then
                Select Case board(p1y, p1x + 1)
                    Case 0
                        move1("r")
                    Case 5
                        move1("r")
                        range1 += 1
                    Case 7
                        move1("r")
                        PicPlayer1.BackgroundImage = My.Resources.bluemine
                        p1Attack = 7
                    Case 8
                        move1("r")
                        PicPlayer1.Left += 50
                        explode(p1x, p1y, 2)
                End Select
            End If
        End If
        If (GetAsyncKeyState(Keys.W)) Then 'Player 2
            If canMove2 = True And p2y <> 0 Then 'can move and not on top
                Select Case board(p2y - 1, p2x) 'check desired location
                    Case 0 'empty
                        move2("u")
                    Case 5
                        move2("u")
                        range2 += 1 'range increases
                    Case 7
                        move2("u")
                        PicPlayer2.BackgroundImage = My.Resources.redmine
                        p2Attack = 7 'deploy mine mode
                    Case 8
                        move2("u")
                        PicPlayer2.Top -= 50
                        explode(p2x, p2y, 2) 'mine explode
                End Select
            End If
        End If
        If (GetAsyncKeyState(Keys.S)) Then
            If canMove2 = True And p2y <> 14 Then
                Select Case board(p2y + 1, p2x)
                    Case 0
                        move2("d")
                    Case 5
                        move2("d")
                        range2 += 1
                    Case 7
                        move2("d")
                        PicPlayer2.BackgroundImage = My.Resources.redmine
                        p2Attack = 7
                    Case 8
                        move2("d")
                        PicPlayer2.Top += 50
                        explode(p2x, p2y, 2)
                End Select
            End If
        End If
        If (GetAsyncKeyState(Keys.A)) Then
            If canMove2 = True And p2x <> 0 Then
                Select Case board(p2y, p2x - 1)
                    Case 0
                        move2("l")
                    Case 5
                        move2("l")
                        range2 += 1
                    Case 7
                        move2("l")
                        PicPlayer2.BackgroundImage = My.Resources.redmine
                        p2Attack = 7
                    Case 8
                        move2("l")
                        PicPlayer2.Left -= 50
                        explode(p2x, p2y, 2)
                End Select
            End If
        End If
        If (GetAsyncKeyState(Keys.D)) Then
            If canMove2 = True And p2x <> 14 Then
                Select Case board(p2y, p2x + 1)
                    Case 0
                        move2("r")
                    Case 5
                        move2("r")
                        range2 += 1
                    Case 7
                        move2("r")
                        PicPlayer2.BackgroundImage = My.Resources.redmine
                        p2Attack = 7
                    Case 8
                        move2("r")
                        PicPlayer2.Left += 50
                        explode(p2x, p2y, 2)
                End Select
            End If
        End If
        'Generate special attacks
        If FormatNumber(special, 1) Mod 15 = 0 Then
            Dim x, y, z As Byte
            Dim r As New Random
            Do
                x = r.Next(15)
                y = r.Next(15)
                If board(y, x) = 0 Then
                    z = r.Next(2)
                    If z = 0 Then
                        Me.Controls("picx" + CStr(x) + "y" + CStr(y)).BackgroundImage = My.Resources.power
                        board(y, x) = 5
                    End If
                    If z = 1 Then
                        Me.Controls("picx" + CStr(x) + "y" + CStr(y)).BackgroundImage = My.Resources.mine
                        board(y, x) = 7
                    End If
                    Exit Do
                End If
            Loop
        End If
        special += 0.1
        'Attack
        If (GetAsyncKeyState(Keys.Enter)) And canBomb1 = True Then
            Select Case p1Attack
                Case 0 'No special attack, deploy bomb
                    If board(p1y, p1x) = 0 Then 'check empty
                        bomb1x = p1x
                        bomb1y = p1y 'record bomb position
                        board(bomb1y, bomb1x) = 3 'add bomb
                        Me.Controls("picx" + CStr(bomb1x) + "y" + CStr(bomb1y)).BackgroundImage = My.Resources.bomb
                        canBomb1 = False
                        TimerBomb1.Enabled = True '2sec to explode
                    End If
                Case 7 'deploy mine
                    If board(p1y, p1x) = 0 Then
                        board(p1y, p1x) = 8
                        PicPlayer1.BackgroundImage = My.Resources.blue
                        p1Attack = 0
                    End If
            End Select
        End If
        If (GetAsyncKeyState(Keys.Space)) And canBomb2 = True Then
            Select Case p2Attack
                Case 0
                    If board(p2y, p2x) = 0 Then
                        bomb2x = p2x
                        bomb2y = p2y
                        board(bomb2y, bomb2x) = 3
                        Me.Controls("picx" + CStr(bomb2x) + "y" + CStr(bomb2y)).BackgroundImage = My.Resources.bomb
                        canBomb2 = False
                        TimerBomb2.Enabled = True
                    End If
                Case 7
                    If board(p2y, p2x) = 0 Then
                        board(p2y, p2x) = 8
                        PicPlayer2.BackgroundImage = My.Resources.red
                        p2Attack = 0
                    End If
            End Select
        End If
    End Sub
End Class