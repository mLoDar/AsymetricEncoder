using AsymetricEncoder.Resources;
using System.Diagnostics;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Xml.Linq;
using System.Text;





namespace AsymetricEncoder
{
    public partial class MainUI : Form
    {
        private static string[,] _preparedArray = new string[,] { };

        private static bool _decodeMessage = false;

        private static readonly string pathFolderAppData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        private static readonly string pathFileTableReference = Path.Combine(pathFolderAppData, "AsymetricEncoderTableReference.txt");




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

        private void Button_Handle_Click(object sender, EventArgs e)
        {
            string input_Message = Text_MessageToHandle.Text;
            string input_Key = Text_KeyToHandle.Text;

            bool validMessageProvided = !RegexPatterns.AllWhitespaces().Replace(input_Message, string.Empty).Equals(string.Empty);
            bool validKeyProvided = !RegexPatterns.AllWhitespaces().Replace(input_Key, string.Empty).Equals(string.Empty);



            if (validMessageProvided == false || validKeyProvided == false)
            {
                string info_Caption = "Invalid input";
                string info_Description = $"Please provide a valid {(validMessageProvided ? "key" : "message")}, for the {(_decodeMessage ? "decoding" : "encoding")} process.";

                MessageBoxIcon info_Icon = MessageBoxIcon.Warning;
                MessageBoxButtons info_Buttons = MessageBoxButtons.OK;

                MessageBox.Show(info_Description, info_Caption, info_Buttons, info_Icon);

                return;
            }



            string handledMessage = EncodeMessage(input_Message, input_Key);

            Text_FinalMessage.Text = handledMessage;
        }

        private string EncodeMessage(string input_Message, string input_Key)
        {
            string handledMessage = string.Empty;



            int currentIndexAtKey = 0;

            for (int i = 0; i < input_Message.Length; i++)
            {
                if (currentIndexAtKey + 1 >= input_Key.Length)
                {
                    currentIndexAtKey = 0;
                }

                char arrayCharVertical = Convert.ToChar(input_Key.Substring(currentIndexAtKey, 1));
                char arrayCharHorizontal = Convert.ToChar(input_Message.Substring(i, 1));

                int arrayPositionVertical = CharacterCollection.allCombined.IndexOf(arrayCharVertical);
                int arrayPositionHorizontal = CharacterCollection.allCombined.IndexOf(arrayCharHorizontal);

                handledMessage += _preparedArray[arrayPositionHorizontal, arrayPositionVertical];

                currentIndexAtKey++;
            }



            return handledMessage;
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
                if (File.Exists(pathFileTableReference))
                {
                    File.Delete(pathFileTableReference);
                }

                using var fileStream = new FileStream(pathFileTableReference, FileMode.CreateNew);
                using var streamWriter = new StreamWriter(fileStream);

                streamWriter.AutoFlush = true;

                streamWriter.Write(tableReference);
                streamWriter.Close();
            }
            catch
            {
                string info_Caption = "An unexpected error appeared";
                string info_Description = "Failed to create a save file to store the table reference.\r\nPlease start the application as an administrator and try again.";

                MessageBoxIcon info_Icon = MessageBoxIcon.Error;
                MessageBoxButtons info_Buttons = MessageBoxButtons.OK;

                MessageBox.Show(info_Description, info_Caption, info_Buttons, info_Icon);

                return;
            }




            try
            {
                ProcessStartInfo fileToOpen = new(pathFileTableReference)
                {
                    UseShellExecute = true,
                    WorkingDirectory = pathFolderAppData
                };

                Process.Start(fileToOpen);
            }
            catch
            {
                string info_Caption = "An unexpected error appeared";
                string info_Description = "Failed to open the save file for the table reference.\r\nPlease start the application as an administrator and try again.";

                MessageBoxIcon info_Icon = MessageBoxIcon.Error;
                MessageBoxButtons info_Buttons = MessageBoxButtons.OK;

                MessageBox.Show(info_Description, info_Caption, info_Buttons, info_Icon);

                return;
            }
        }
    }
}