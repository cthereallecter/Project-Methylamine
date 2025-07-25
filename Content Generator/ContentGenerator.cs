using ProjectMethylamine.Source.Utility;
using System.Reflection;

namespace ProjectMethylamine
{
    public partial class ContentGenerator : Form
    {
        private static ConsoleLogger logger = new ConsoleLogger();

        public ContentGenerator()
        {
            InitializeComponent();

            // Pre-load Server.dll
            try
            {
                string serverPath = Path.Combine(Application.StartupPath, "Server.dll");
                Assembly.LoadFrom(serverPath);
                logger.Log("INIT", "Server.dll pre-loaded successfully");
            }
            catch (Exception ex)
            {
                logger.Log("INIT", $"Failed to pre-load Server.dll: {ex.Message}");
            }


        }

        private void btnPAKRStart_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate inputs first
                if (string.IsNullOrWhiteSpace(txtArchiveName.Text))
                {
                    MessageBox.Show("Please fill in all required fields.", "Validation Error");
                    return;
                }

                string command = $"pakr -p /D /E Content Data {txtArchiveName.Text}";
                CommandHandler.InvokeCommand(logger, command);
                if (File.Exists($"./Data/{txtArchiveName.Text}.pakr"))
                {
                    logger.Log("INFO", $"Content packed successfully: {txtArchiveName.Text}.pakr");
                    MessageBox.Show($"Content packed successfully: {txtArchiveName.Text}.pakr", "Success");
                    lstDataPacks.Items.Add(txtArchiveName.Text);
                }
                else
                {
                    logger.Log("ERROR", $"Failed to pack content: {txtArchiveName.Text}.pakr");
                    MessageBox.Show($"Failed to pack content: {txtArchiveName.Text}.pakr", "Error");
                }
                lsLogBox.Items.Add($"Command executed: {command}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error executing command: {ex.Message}", "Error");
                logger.Log("ERROR", ex.ToString());
                lsLogBox.Items.Add($"Error: {ex.Message}");
            }
        }

        private void btnUNPAKRStart_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate inputs first
                if (string.IsNullOrWhiteSpace(txtExtractionSource.Text))
                {
                    MessageBox.Show("Please fill in all required fields.", "Validation Error");
                    return;
                }

                lstDataPacks.Items.Remove(txtExtractionSource.Text);

                string command = $"pakr -u /D /e ./Data/{txtExtractionSource.Text}.pakr Content";
                CommandHandler.InvokeCommand(logger, command);
                lsLogBox.Items.Add($"Command executed: {command}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error executing command: {ex.Message}", "Error");
                logger.Log("ERROR", ex.ToString());
                lsLogBox.Items.Add($"Error: {ex.Message}");
            }
        }

        private void btnMAPPRMake_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate inputs first
                if (string.IsNullOrWhiteSpace(txtMapName.Text) || nmdMapSize.Value == 0)
                {
                    MessageBox.Show("Please fill in all required fields.", "Validation Error");
                    return;
                }

                string command;
                if (chkMapSeasonal.Checked)
                {
                    command = $"mappr -c /S {txtMapName.Text} {nmdMapSize.Value}";
                }
                else
                {
                    command = $"mappr -c {txtMapName.Text} {nmdMapSize.Value}";
                }
                CommandHandler.InvokeCommand(logger, command);
                lsLogBox.Items.Add($"Command executed: {command}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error executing command: {ex.Message}", "Error");
                logger.Log("ERROR", ex.ToString());
                lsLogBox.Items.Add($"Error: {ex.Message}");
            }
        }

        private void btnMAPPRDelete_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate inputs first
                if (string.IsNullOrWhiteSpace(txtMapName.Text))
                {
                    MessageBox.Show("Please fill in all required fields.", "Validation Error");
                    return;
                }

                string command = $"mappr -e {txtMapName.Text}";
                CommandHandler.InvokeCommand(logger, command);
                lsLogBox.Items.Add($"Command executed: {command}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error executing command: {ex.Message}", "Error");
                logger.Log("ERROR", ex.ToString());
                lsLogBox.Items.Add($"Error: {ex.Message}");
            }
        }
    }
}
