using System;
using System.IO;
using System.Windows.Forms;
namespace CountPV
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.AllowDrop = true;
            this.DragEnter += Form1_DragEnter;
            this.DragDrop += Form1_DragDrop;

        }
        private string _folderPath;

        //�ļ�������
        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var path = ((string[])e.Data.GetData(DataFormats.FileDrop))[0];

                if (Directory.Exists(path) || File.Exists(path))
                {
                    e.Effect = DragDropEffects.Copy;
                }
            }
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var path = ((string[])e.Data.GetData(DataFormats.FileDrop))[0];
                txtFolderPath.Text = path;
                _folderPath = path;
            }
            txtLog.AppendText($"��ѡ���ļ��С�{_folderPath}��\r\n");

        }

        private Dictionary<string, string> _renamedFolders; // ���ڱ�������ƺ������Ƶ�ӳ��

        private void btnSelect_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            if (folderDialog.ShowDialog() == DialogResult.OK)
            {
                _folderPath = folderDialog.SelectedPath;
                txtFolderPath.Text = _folderPath;
            }
            txtLog.AppendText($"��ѡ���ļ��С�{_folderPath}��\r\n");
        }

        private void btnActive_Click(object sender, EventArgs e)
        {
            txtLog.SelectionFont = new Font(txtLog.Font, FontStyle.Bold); // ����Ϊ����
            txtLog.SelectionColor = Color.Blue;
            txtLog.AppendText($"��{_folderPath}�� ���ڴ�����.....\r\n");
            if (!string.IsNullOrEmpty(_folderPath))
            {
                // ��ȡ�ļ����е����ļ���
                string[] subDirs = Directory.GetDirectories(_folderPath);

                // �������ļ��У�ͳ�ƺ��������ļ���
                _renamedFolders = new Dictionary<string, string>(); // ��ʼ���ֵ�
                foreach (string subDir in subDirs)
                {
                    int imageCount, videoCount, txtCount;
                    CountImagesAndVideos(subDir, out imageCount, out videoCount, out txtCount);

                    string originalName = Path.GetFileName(subDir);
                    string newName = $"{originalName} [{imageCount}P-{videoCount}V-{videoCount}T={GetFolderSizeString(subDir)}]";

                    // ��¼����ӳ���ϵ

                    txtLog.AppendText($"��{originalName}�� ==> {imageCount}ͼƬ��{videoCount}��Ƶ��{txtCount}�ı��ĵ�\r\n");

                    // �������ļ���
                    if (checkBox1.Checked)
                    {
                        try
                        {
                            Directory.Move(subDir, Path.Combine(Path.GetDirectoryName(subDir), newName));
                        }
                        catch (Exception ex)
                        {
                            txtLog.SelectionColor = Color.Red;
                            txtLog.AppendText($"����������: {ex.Message}\r\n");
                        }
                        txtLog.SelectionColor = Color.Brown;
                        txtLog.AppendText($"��{originalName}����������Ϊ��{newName}�� \r\n");
                    }

                }
                txtLog.SelectionFont = new Font(txtLog.Font, FontStyle.Bold); // ����Ϊ����
                txtLog.SelectionColor = Color.Green;
                txtLog.AppendText($"ִ�����\r\n");
            }
        }


        private void CountImagesAndVideos(string folderPath, out int imageCount, out int videoCount, out int txtCount)
        {
            // ��ʼ��������
            imageCount = 0;
            videoCount = 0;
            txtCount = 0;

            // ͳ��ͼƬ����Ƶ����
            foreach (string file in Directory.GetFiles(folderPath))
            {
                string extension = Path.GetExtension(file).ToLower();
                if (extension == ".jpg" || extension == ".png" || extension == ".jpeg")
                {
                    imageCount++;
                }
                else if (extension == ".mp4" || extension == ".avi" || extension == ".mov")
                {
                    videoCount++;
                }
                else if (extension == ".txt")
                {
                    txtCount++;
                }
            }

            // �������ļ��У�ͳ��ͼƬ����Ƶ����
            foreach (string subDir in Directory.GetDirectories(folderPath, "*", SearchOption.AllDirectories))
            {
                foreach (string file in Directory.GetFiles(subDir))
                {
                    string extension = Path.GetExtension(file).ToLower();
                    if (extension == ".jpg" || extension == ".png" || extension == ".jpeg")
                    {
                        imageCount++;
                    }
                    else if (extension == ".mp4" || extension == ".avi" || extension == ".mov")
                    {
                        videoCount++;
                    }
                    else if (extension == ".txt")
                    {
                        txtCount++;
                    }
                }
            }
        }

        private string GetFolderSizeString(string folderPath)
        {
            DirectoryInfo di = new DirectoryInfo(folderPath);
            long sizeBytes = 0;
            foreach (FileInfo fi in di.GetFiles("*", SearchOption.AllDirectories))
            {
                sizeBytes += fi.Length;
            }
            return GetFileSizeString(sizeBytes);
        }

        private string GetFileSizeString(long fileSizeBytes)
        {
            string[] sizes = { "B", "KB", "MB", "GB", "TB" };
            double len = fileSizeBytes;
            int order = 0;
            while (len >= 1024 && order < sizes.Length - 1)
            {
                order++;
                len /= 1024;
            }

            string fileSizeString = String.Format("{0:0.##} {1}", len, sizes[order]);
            return fileSizeString;
        }
        private void txtFolderPath_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            explainBox.AppendText("���򿪷���Ľ��\r\n");
            explainBox.Select(0, 8); // ����ѡ��Χ����Ҫ�Ӵֵ��ı�

            explainBox.SelectionFont = new Font(explainBox.Font.Name, 18, FontStyle.Bold);
            explainBox.SelectionAlignment = HorizontalAlignment.Center; // ���ı�ˮƽ���ж���

            // ���õڶ��е��ı�
            explainBox.AppendText("������������Ƶ��ͼƬ���ı��ĵ�Txt�������뽫����Ҫ��������ļ��С��ǡ�����һ���ļ�����������У�������Զ�����������Щ�ļ��е���ͼ�����Լ�������С����ѡ�����Ὣԭ�ļ�����Ϊ�����ļ������� [nP-nV-nT=nKB/MB/GB/]");

        }


        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void txtLog_TextChanged(object sender, EventArgs e)
        {

        }

        private void explainBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void share_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
