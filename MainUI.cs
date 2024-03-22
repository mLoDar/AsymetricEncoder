using System.Diagnostics;

using AsymetricEncoder.Resources;





namespace AsymetricEncoder
{
    public partial class MainUI : Form
    {
        private static string[,] _preparedArray = new string[,] { };

        private static bool _decodeMessage = false;

        private static readonly string _pathFolderAppData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        private static readonly string _pathFileTableReference = Path.Combine(_pathFolderAppData, "AsymetricEncoderTableReference.txt");





        public MainUI()
        {
            InitializeComponent();
        }

        private void MainUI_Load(object sender, EventArgs e)
        {
            string charactersToUse = CharacterCollection.allCombined;

            if (charactersToUse.Length % 2 != 0)
            {
                string boxCaption = "Error while starting";
                string boxDescription = "The count of characters to use needs to be an even number, due to mathematical reasons.\r\nPlease remove or add a character.";

                MessageBoxIcon boxIcon = MessageBoxIcon.Error;
                MessageBoxButtons boxButtons = MessageBoxButtons.OK;

                MessageBox.Show(boxDescription, boxCaption, boxButtons, boxIcon);

                Environment.Exit(0);
            }
            


            _preparedArray = PrepareArray(charactersToUse);

            if (_preparedArray.Equals(new string[,] { }))
            {
                string boxCaption = "Error while preperation";
                string boxDescription = "Failed to prepare the array for en-/decoding.\r\nPlease try again.";

                MessageBoxIcon boxIcon = MessageBoxIcon.Error;
                MessageBoxButtons boxButtons = MessageBoxButtons.OK;

                MessageBox.Show(boxDescription, boxCaption, boxButtons, boxIcon);

                Environment.Exit(0);
            }
        }

        private void Button_Handle_Click(object sender, EventArgs e)
        {
            string inputMessage = Text_MessageToHandle.Text;
            string inputKey = Text_KeyToHandle.Text;

            bool validMessageProvided = !RegexPatterns.AllWhitespaces().Replace(inputMessage, string.Empty).Equals(string.Empty);
            bool validKeyProvided = !RegexPatterns.AllWhitespaces().Replace(inputKey, string.Empty).Equals(string.Empty);



            if (validMessageProvided == false || validKeyProvided == false)
            {
                string boxCaption = "Invalid input";
                string boxDescription = $"Please provide a valid {(validMessageProvided ? "key" : "message")}, for the {(_decodeMessage ? "decoding" : "encoding")} process.";

                MessageBoxIcon boxIcon = MessageBoxIcon.Warning;
                MessageBoxButtons boxButtons = MessageBoxButtons.OK;

                MessageBox.Show(boxDescription, boxCaption, boxButtons, boxIcon);

                return;
            }



            string handledMessage = EncodeMessage(inputMessage, inputKey);

            Text_FinalMessage.Text = handledMessage;
        }

        private void Button_DisplayTableReference_Click(object sender, EventArgs e)
        {
            string tableReference = string.Empty;

            for (int i = 0; i < _preparedArray.GetLength(0); i++)
            {
                for (int j = 0; j < _preparedArray.GetLength(1); j++)
                {
                    tableReference += _preparedArray[i, j].ToString() + " ";
                }
                tableReference += "\r\n";
            }



            try
            {
                if (File.Exists(_pathFileTableReference))
                {
                    File.Delete(_pathFileTableReference);
                }

                using var fileStream = new FileStream(_pathFileTableReference, FileMode.CreateNew);
                using var streamWriter = new StreamWriter(fileStream);

                streamWriter.AutoFlush = true;

                streamWriter.Write(tableReference);
                streamWriter.Close();
            }
            catch
            {
                string boxCaption = "An unexpected error appeared";
                string boxDescription = "Failed to create a save file to store the table reference.\r\nPlease start the application as an administrator and try again.";

                MessageBoxIcon boxIcon = MessageBoxIcon.Error;
                MessageBoxButtons boxButtons = MessageBoxButtons.OK;

                MessageBox.Show(boxDescription, boxCaption, boxButtons, boxIcon);

                return;
            }




            try
            {
                ProcessStartInfo fileToOpen = new(_pathFileTableReference)
                {
                    UseShellExecute = true,
                    WorkingDirectory = _pathFolderAppData
                };

                Process.Start(fileToOpen);
            }
            catch
            {
                string boxCaption = "An unexpected error appeared";
                string boxDescription = "Failed to open the save file for the table reference.\r\nPlease start the application as an administrator and try again.";

                MessageBoxIcon boxIcon = MessageBoxIcon.Error;
                MessageBoxButtons boxButtons = MessageBoxButtons.OK;

                MessageBox.Show(boxDescription, boxCaption, boxButtons, boxIcon);

                return;
            }
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

        private static string EncodeMessage(string inputMessage, string inputKey)
        {
            string handledMessage = string.Empty;



            int currentIndexAtKey = 0;

            for (int i = 0; i < inputMessage.Length; i++)
            {
                if (currentIndexAtKey + 1 > inputKey.Length)
                {
                    currentIndexAtKey = 0;
                }

                char arrayCharVertical = Convert.ToChar(inputKey.Substring(currentIndexAtKey, 1));
                char arrayCharHorizontal = Convert.ToChar(inputMessage.Substring(i, 1));

                int arrayPositionVertical = CharacterCollection.allCombined.IndexOf(arrayCharVertical);
                int arrayPositionHorizontal = CharacterCollection.allCombined.IndexOf(arrayCharHorizontal);



                handledMessage += _preparedArray[arrayPositionHorizontal, arrayPositionVertical];

                currentIndexAtKey++;
            }



            return handledMessage;
        }
    }
}