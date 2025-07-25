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
                if (string.IsNullOrWhiteSpace(txtSource.Text) ||
                    string.IsNullOrWhiteSpace(txtDestination.Text) ||
                    string.IsNullOrWhiteSpace(txtArchiveName.Text))
                {
                    MessageBox.Show("Please fill in all required fields.", "Validation Error");
                    return;
                }

                string command = $"pakr -p /D {txtSource.Text} {txtDestination.Text} {txtArchiveName.Text}";
                CommandHandler.InvokeCommand(logger, command);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error executing command: {ex.Message}", "Error");
                logger.Log("ERROR", ex.ToString());
            }
        }
    }
}
