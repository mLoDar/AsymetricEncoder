namespace AsymetricEncoder
{
    partial class MainUI
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Button_Handle = new Button();
            Label_KeyToHandle = new Label();
            Text_KeyToHandle = new TextBox();
            Text_MessageToHandle = new TextBox();
            Label_MessageToHandle = new Label();
            Text_FinalMessage = new TextBox();
            Label_FinalMessage = new Label();
            CheckBox_SwitchMode = new CheckBox();
            Button_DisplayTableReference = new Button();
            SuspendLayout();
            // 
            // Button_Handle
            // 
            Button_Handle.Location = new Point(50, 200);
            Button_Handle.Name = "Button_Handle";
            Button_Handle.Size = new Size(100, 50);
            Button_Handle.TabIndex = 0;
            Button_Handle.Text = "Encode";
            Button_Handle.UseVisualStyleBackColor = true;
            // 
            // Label_KeyToHandle
            // 
            Label_KeyToHandle.AutoSize = true;
            Label_KeyToHandle.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            Label_KeyToHandle.Location = new Point(50, 50);
            Label_KeyToHandle.Name = "Label_KeyToHandle";
            Label_KeyToHandle.Size = new Size(130, 21);
            Label_KeyToHandle.TabIndex = 1;
            Label_KeyToHandle.Text = "Key for encoding:";
            // 
            // Text_KeyToHandle
            // 
            Text_KeyToHandle.Location = new Point(50, 74);
            Text_KeyToHandle.Name = "Text_KeyToHandle";
            Text_KeyToHandle.Size = new Size(150, 23);
            Text_KeyToHandle.TabIndex = 2;
            Text_KeyToHandle.TextAlign = HorizontalAlignment.Center;
            // 
            // Text_MessageToHandle
            // 
            Text_MessageToHandle.Location = new Point(50, 149);
            Text_MessageToHandle.Name = "Text_MessageToHandle";
            Text_MessageToHandle.Size = new Size(300, 23);
            Text_MessageToHandle.TabIndex = 4;
            Text_MessageToHandle.TextAlign = HorizontalAlignment.Center;
            // 
            // Label_MessageToHandle
            // 
            Label_MessageToHandle.AutoSize = true;
            Label_MessageToHandle.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            Label_MessageToHandle.Location = new Point(50, 125);
            Label_MessageToHandle.Name = "Label_MessageToHandle";
            Label_MessageToHandle.Size = new Size(146, 21);
            Label_MessageToHandle.TabIndex = 3;
            Label_MessageToHandle.Text = "Message to encode:";
            // 
            // Text_FinalMessage
            // 
            Text_FinalMessage.Location = new Point(50, 324);
            Text_FinalMessage.Name = "Text_FinalMessage";
            Text_FinalMessage.Size = new Size(300, 23);
            Text_FinalMessage.TabIndex = 6;
            Text_FinalMessage.TextAlign = HorizontalAlignment.Center;
            // 
            // Label_FinalMessage
            // 
            Label_FinalMessage.AutoSize = true;
            Label_FinalMessage.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            Label_FinalMessage.Location = new Point(50, 300);
            Label_FinalMessage.Name = "Label_FinalMessage";
            Label_FinalMessage.Size = new Size(137, 21);
            Label_FinalMessage.TabIndex = 5;
            Label_FinalMessage.Text = "Encoded message:";
            // 
            // CheckBox_SwitchMode
            // 
            CheckBox_SwitchMode.AutoSize = true;
            CheckBox_SwitchMode.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            CheckBox_SwitchMode.Location = new Point(206, 74);
            CheckBox_SwitchMode.Name = "CheckBox_SwitchMode";
            CheckBox_SwitchMode.Size = new Size(146, 25);
            CheckBox_SwitchMode.TabIndex = 7;
            CheckBox_SwitchMode.Text = "Decode message";
            CheckBox_SwitchMode.UseVisualStyleBackColor = true;
            CheckBox_SwitchMode.CheckedChanged += CheckBox_SwitchMode_CheckedChanged;
            // 
            // Button_DisplayTableReference
            // 
            Button_DisplayTableReference.Location = new Point(356, 300);
            Button_DisplayTableReference.Name = "Button_DisplayTableReference";
            Button_DisplayTableReference.Size = new Size(116, 50);
            Button_DisplayTableReference.TabIndex = 8;
            Button_DisplayTableReference.Text = "Display table reference";
            Button_DisplayTableReference.UseVisualStyleBackColor = true;
            // 
            // MainUI
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(514, 411);
            Controls.Add(Button_DisplayTableReference);
            Controls.Add(CheckBox_SwitchMode);
            Controls.Add(Text_FinalMessage);
            Controls.Add(Label_FinalMessage);
            Controls.Add(Text_MessageToHandle);
            Controls.Add(Label_MessageToHandle);
            Controls.Add(Text_KeyToHandle);
            Controls.Add(Label_KeyToHandle);
            Controls.Add(Button_Handle);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "MainUI";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Asymetric Encoder";
            Load += MainUI_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button Button_Handle;
        private Label Label_KeyToHandle;
        private TextBox Text_KeyToHandle;
        private TextBox Text_MessageToHandle;
        private Label Label_MessageToHandle;
        private TextBox Text_FinalMessage;
        private Label Label_FinalMessage;
        private CheckBox CheckBox_SwitchMode;
        private Button Button_DisplayTableReference;
    }
}
