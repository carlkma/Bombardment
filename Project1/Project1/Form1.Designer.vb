<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.TimerMaster = New System.Windows.Forms.Timer(Me.components)
        Me.PicPlayer1 = New System.Windows.Forms.PictureBox()
        Me.PicPlayer2 = New System.Windows.Forms.PictureBox()
        Me.TimerMove1 = New System.Windows.Forms.Timer(Me.components)
        Me.TimerMove2 = New System.Windows.Forms.Timer(Me.components)
        Me.TimerBomb1 = New System.Windows.Forms.Timer(Me.components)
        Me.TimerBomb2 = New System.Windows.Forms.Timer(Me.components)
        Me.TimerExplode = New System.Windows.Forms.Timer(Me.components)
        Me.lblStart = New System.Windows.Forms.Label()
        Me.lblMessage2 = New System.Windows.Forms.Label()
        Me.lblMessage1 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        CType(Me.PicPlayer1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PicPlayer2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TimerMaster
        '
        '
        'PicPlayer1
        '
        Me.PicPlayer1.BackColor = System.Drawing.SystemColors.Control
        Me.PicPlayer1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.PicPlayer1.Location = New System.Drawing.Point(180, 180)
        Me.PicPlayer1.Margin = New System.Windows.Forms.Padding(4)
        Me.PicPlayer1.Name = "PicPlayer1"
        Me.PicPlayer1.Size = New System.Drawing.Size(50, 50)
        Me.PicPlayer1.TabIndex = 1
        Me.PicPlayer1.TabStop = False
        '
        'PicPlayer2
        '
        Me.PicPlayer2.BackColor = System.Drawing.SystemColors.Control
        Me.PicPlayer2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.PicPlayer2.Location = New System.Drawing.Point(580, 580)
        Me.PicPlayer2.Margin = New System.Windows.Forms.Padding(4)
        Me.PicPlayer2.Name = "PicPlayer2"
        Me.PicPlayer2.Size = New System.Drawing.Size(50, 50)
        Me.PicPlayer2.TabIndex = 2
        Me.PicPlayer2.TabStop = False
        '
        'TimerMove1
        '
        Me.TimerMove1.Interval = 10
        '
        'TimerMove2
        '
        Me.TimerMove2.Interval = 10
        '
        'TimerBomb1
        '
        Me.TimerBomb1.Interval = 2000
        '
        'TimerBomb2
        '
        Me.TimerBomb2.Interval = 2000
        '
        'TimerExplode
        '
        Me.TimerExplode.Interval = 500
        '
        'lblStart
        '
        Me.lblStart.AutoSize = True
        Me.lblStart.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.lblStart.Font = New System.Drawing.Font("Arial", 22.875!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStart.Location = New System.Drawing.Point(850, 40)
        Me.lblStart.Name = "lblStart"
        Me.lblStart.Size = New System.Drawing.Size(333, 68)
        Me.lblStart.TabIndex = 3
        Me.lblStart.Text = "New Game"
        '
        'lblMessage2
        '
        Me.lblMessage2.AutoSize = True
        Me.lblMessage2.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.lblMessage2.Font = New System.Drawing.Font("Arial", 25.125!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMessage2.Location = New System.Drawing.Point(150, 350)
        Me.lblMessage2.Name = "lblMessage2"
        Me.lblMessage2.Size = New System.Drawing.Size(303, 75)
        Me.lblMessage2.TabIndex = 4
        Me.lblMessage2.Text = "Message"
        Me.lblMessage2.Visible = False
        '
        'lblMessage1
        '
        Me.lblMessage1.AutoSize = True
        Me.lblMessage1.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.lblMessage1.Font = New System.Drawing.Font("Arial", 25.125!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMessage1.Location = New System.Drawing.Point(150, 250)
        Me.lblMessage1.Name = "lblMessage1"
        Me.lblMessage1.Size = New System.Drawing.Size(303, 75)
        Me.lblMessage1.TabIndex = 5
        Me.lblMessage1.Text = "Message"
        Me.lblMessage1.Visible = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 10.125!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(780, 150)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(426, 576)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = resources.GetString("Label1.Text")
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(12.0!, 24.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.ClientSize = New System.Drawing.Size(2974, 1929)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lblMessage1)
        Me.Controls.Add(Me.lblMessage2)
        Me.Controls.Add(Me.lblStart)
        Me.Controls.Add(Me.PicPlayer2)
        Me.Controls.Add(Me.PicPlayer1)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "Form1"
        Me.Text = "Bombardment"
        CType(Me.PicPlayer1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PicPlayer2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TimerMaster As Timer
    Friend WithEvents PicPlayer1 As PictureBox
    Friend WithEvents PicPlayer2 As PictureBox
    Friend WithEvents TimerMove1 As Timer
    Friend WithEvents TimerMove2 As Timer
    Friend WithEvents TimerBomb1 As Timer
    Friend WithEvents TimerBomb2 As Timer
    Friend WithEvents TimerExplode As Timer
    Friend WithEvents lblStart As Label
    Friend WithEvents lblMessage2 As Label
    Friend WithEvents lblMessage1 As Label
    Friend WithEvents Label1 As Label
End Class
