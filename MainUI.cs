namespace AsymetricEncoder
{
    public partial class MainUI : Form
    {
        private string[,] _preparedArray = new string[,] { };

        private bool _decodeMessage = false;





        public MainUI()
        {
            InitializeComponent();
        }

        private void MainUI_Load(object sender, EventArgs e)
        {
            string charactersToUse = CharacterCollection.allCombined;

            if (charactersToUse.Length % 2 != 0)
            {
                string info_Caption = "Error while starting";
                string info_Description = "The count of characters to use needs to be an even number, due to mathematical reasons.\r\nPlease remove or add a character.";

                MessageBoxIcon info_Icon = MessageBoxIcon.Error;
                MessageBoxButtons info_Buttons = MessageBoxButtons.OK;

                MessageBox.Show(info_Description, info_Caption, info_Buttons, info_Icon);

                Environment.Exit(0);
            }



            _preparedArray = PrepareArray(charactersToUse);

            if (_preparedArray.Equals(new string[,] { }))
            {
                string info_Caption = "Error while preperation";
                string info_Description = "Failed to prepare the array for en-/decoding.\r\nPlease try again.";

                MessageBoxIcon info_Icon = MessageBoxIcon.Error;
                MessageBoxButtons info_Buttons = MessageBoxButtons.OK;

                MessageBox.Show(info_Description, info_Caption, info_Buttons, info_Icon);

                Environment.Exit(0);
            }
        }

        private static string[,] PrepareArray(string characters)
        {
            if (characters.Length <= 0)
            {
                return new string[,] { };
            }



            string[,] preparedArray = new string[characters.Length, characters.Length];



            for (int i = 0; i < characters.Length; i++)
            {
                string charToInsert = characters.Substring(i, 1);

                int incrementBy = i + 1;
                int currPos = 0;

                for (int j = 0; j < preparedArray.GetLength(1); j++)
                {
                    if (i + currPos > preparedArray.GetLength(1))
                    {
                        int belowZeroBalancing = 0 - i + 1;
                        int arrayLength = preparedArray.GetLength(1) + 1;
                        int overflow = currPos + i;

                        currPos = belowZeroBalancing + (overflow - arrayLength - 1);
                    }

                    preparedArray[i + currPos, j] = charToInsert;

                    currPos += incrementBy;
                }
            }



            return preparedArray;
        }

        private void CheckBox_SwitchMode_CheckedChanged(object sender, EventArgs e)
        {
            _decodeMessage = !_decodeMessage;

            if (_decodeMessage == true)
            {
                Label_KeyToHandle.Text = "Key for decoding:";
                Label_MessageToHandle.Text = "Message to decode:";
                Label_FinalMessage.Text = "Decoded message:";

                Button_Handle.Text = "Decode";

                return;
            }

            Label_KeyToHandle.Text = "Key for encoding:";
            Label_MessageToHandle.Text = "Message to encode:";
            Label_FinalMessage.Text = "Encoded message:";

            Button_Handle.Text = "Encode";
        }
    }
}